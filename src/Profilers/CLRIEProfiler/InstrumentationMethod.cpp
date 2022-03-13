
#include "common.h"
#include "InstrumentationMethod.h"
#include <string>

HRESULT CInstrumentationMethod::Initialize(_In_ IProfilerManager* pProfilerManager)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::OnAppDomainCreated(_In_ IAppDomainInfo* pAppDomainInfo)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::OnAppDomainShutdown(_In_ IAppDomainInfo* pAppDomainInfo)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::OnAssemblyLoaded(_In_ IAssemblyInfo* pAssemblyInfo)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::OnAssemblyUnloaded(_In_ IAssemblyInfo* pAssemblyInfo)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::OnModuleLoaded(_In_ IModuleInfo* pModuleInfo)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::OnModuleUnloaded(_In_ IModuleInfo* pModuleInfo)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::OnShutdown()
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ShouldInstrumentMethod(_In_ IMethodInfo* pMethodInfo, _In_ BOOL isRejit, _Out_ BOOL* pbInstrument)
{
    BSTR fullName = 0;
    pMethodInfo->GetFullName(&fullName);
    std::wstring str = fullName;

    if (str.find(L"Main") != std::string::npos) {
        *pbInstrument = TRUE;
    }
    else
    {
        *pbInstrument = FALSE;
    }

    SysFreeString(fullName);


    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::BeforeInstrumentMethod(_In_ IMethodInfo* pMethodInfo, _In_ BOOL isRejit)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::InstrumentMethod(_In_ IMethodInfo* pMethodInfo, _In_ BOOL isRejit)
{
    CComPtr<IInstructionGraph> pInstructionGraph;
    pMethodInfo->GetInstructions(&pInstructionGraph);

    CComPtr<IInstructionFactory> sptrInstructionFactory;
    pMethodInfo->GetInstructionFactory(&sptrInstructionFactory);

    CComPtr<IInstruction> firstInstr;
    pInstructionGraph->GetFirstInstruction(&firstInstr);

    CComPtr<IInstruction> nop;
    sptrInstructionFactory->CreateInstruction(Cee_Nop, &nop);

    // Inject a nop as a first op
    pInstructionGraph->InsertBefore(firstInstr, nop);

    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::OnInstrumentationComplete(_In_ IMethodInfo* pMethodInfo, _In_ BOOL isRejit)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::AllowInlineSite(_In_ IMethodInfo* pMethodInfoInlinee, _In_ IMethodInfo* pMethodInfoCaller, _Out_ BOOL* pbAllowInline)
{
    *pbAllowInline = FALSE;
    return S_OK;
}


HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionCatcherEnter(_In_ IMethodInfo* pMethodInfo, _In_ UINT_PTR   objectId)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionCatcherLeave()
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionSearchCatcherFound(_In_ IMethodInfo* pMethodInfo
)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionSearchFilterEnter(_In_ IMethodInfo* pMethodInfo
)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionSearchFilterLeave()
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionSearchFunctionEnter(_In_ IMethodInfo* pMethodInfo)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionSearchFunctionLeave()
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionThrown(_In_ UINT_PTR thrownObjectId)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionUnwindFinallyEnter(_In_ IMethodInfo* pMethodInfo
)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionUnwindFinallyLeave()
{
    return S_OK;
}


HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionUnwindFunctionEnter(_In_ IMethodInfo* pMethodInfo)
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::ExceptionUnwindFunctionLeave()
{
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CInstrumentationMethod::QueryInterface(REFIID riid, void** ppvObject)
{
    PVOID implemensIInstrumentationMethod = static_cast<IUnknown*>(static_cast<IInstrumentationMethod*>(this));
    PVOID implemensIInstrumentationMethodExceptionEvents = static_cast<IUnknown*>(static_cast<IInstrumentationMethodExceptionEvents*>(this));

    if (!implemensIInstrumentationMethod)
    {
        return E_NOINTERFACE;
    }

    if (!implemensIInstrumentationMethodExceptionEvents)
    {
        return E_NOINTERFACE;
    }

    *ppvObject = this;
    this->AddRef();

    return S_OK;
}

ULONG STDMETHODCALLTYPE CInstrumentationMethod::AddRef()
{
    return std::atomic_fetch_add(&this->refCount, 1) + 1;
}

ULONG STDMETHODCALLTYPE CInstrumentationMethod::Release()
{
    int count = std::atomic_fetch_sub(&this->refCount, 1) - 1;
    if (count <= 0)
    {
        delete this;
    }

    return count;
}
