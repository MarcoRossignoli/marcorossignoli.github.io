#pragma once

#include "common.h"

class __declspec(uuid("D2959618-F9B6-4CB6-80CF-F3B0E3263888"))
    CInstrumentationMethod :
    public IInstrumentationMethod,
    public IInstrumentationMethodExceptionEvents
{
private:
    std::atomic<int> refCount;

public:
    virtual HRESULT STDMETHODCALLTYPE QueryInterface(REFIID riid, void** ppvObject);
    virtual ULONG   STDMETHODCALLTYPE AddRef(void);
    virtual ULONG   STDMETHODCALLTYPE Release(void);

public:
    virtual HRESULT STDMETHODCALLTYPE Initialize(_In_ IProfilerManager* pProfilerManager);

    virtual HRESULT STDMETHODCALLTYPE OnAppDomainCreated(
        _In_ IAppDomainInfo* pAppDomainInfo);

    virtual HRESULT STDMETHODCALLTYPE OnAppDomainShutdown(
        _In_ IAppDomainInfo* pAppDomainInfo);

    virtual HRESULT STDMETHODCALLTYPE OnAssemblyLoaded(_In_ IAssemblyInfo* pAssemblyInfo);
    virtual HRESULT STDMETHODCALLTYPE OnAssemblyUnloaded(_In_ IAssemblyInfo* pAssemblyInfo);

    virtual HRESULT STDMETHODCALLTYPE OnModuleLoaded(_In_ IModuleInfo* pModuleInfo);
    virtual HRESULT STDMETHODCALLTYPE OnModuleUnloaded(_In_ IModuleInfo* pModuleInfo);

    virtual HRESULT STDMETHODCALLTYPE OnShutdown();

    virtual HRESULT STDMETHODCALLTYPE ShouldInstrumentMethod(_In_ IMethodInfo* pMethodInfo, _In_ BOOL isRejit, _Out_ BOOL* pbInstrument);

    virtual HRESULT STDMETHODCALLTYPE BeforeInstrumentMethod(_In_ IMethodInfo* pMethodInfo, _In_ BOOL isRejit);

    virtual HRESULT STDMETHODCALLTYPE InstrumentMethod(_In_ IMethodInfo* pMethodInfo, _In_ BOOL isRejit);

    virtual HRESULT STDMETHODCALLTYPE OnInstrumentationComplete(_In_ IMethodInfo* pMethodInfo, _In_ BOOL isRejit);

    virtual HRESULT STDMETHODCALLTYPE AllowInlineSite(_In_ IMethodInfo* pMethodInfoInlinee, _In_ IMethodInfo* pMethodInfoCaller, _Out_ BOOL* pbAllowInline);

public:
    virtual HRESULT STDMETHODCALLTYPE ExceptionCatcherEnter(
        _In_ IMethodInfo* pMethodInfo,
        _In_ UINT_PTR   objectId
    );

    virtual HRESULT STDMETHODCALLTYPE ExceptionCatcherLeave();

    virtual HRESULT STDMETHODCALLTYPE ExceptionSearchCatcherFound(
        _In_ IMethodInfo* pMethodInfo
    );

    virtual HRESULT STDMETHODCALLTYPE ExceptionSearchFilterEnter(
        _In_ IMethodInfo* pMethodInfo
    );

    virtual HRESULT STDMETHODCALLTYPE ExceptionSearchFilterLeave();

    virtual HRESULT STDMETHODCALLTYPE ExceptionSearchFunctionEnter(
        _In_ IMethodInfo* pMethodInfo
    );

    virtual HRESULT STDMETHODCALLTYPE ExceptionSearchFunctionLeave();

    virtual HRESULT STDMETHODCALLTYPE ExceptionThrown(
        _In_ UINT_PTR thrownObjectId
    );

    virtual HRESULT STDMETHODCALLTYPE ExceptionUnwindFinallyEnter(
        _In_ IMethodInfo* pMethodInfo
    );

    virtual HRESULT STDMETHODCALLTYPE ExceptionUnwindFinallyLeave();

    virtual HRESULT STDMETHODCALLTYPE ExceptionUnwindFunctionEnter(
        _In_ IMethodInfo* pMethodInfo
    );

    virtual HRESULT STDMETHODCALLTYPE ExceptionUnwindFunctionLeave();
};
