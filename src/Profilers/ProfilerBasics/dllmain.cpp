// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "ClassFactory.h"
#include <string>
#include <Windows.h>
#include <iostream>
#include <Logger.h>
using namespace std;

const IID IID_IUnknown = { 0x00000000, 0x0000, 0x0000, { 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46 } };

const IID IID_IClassFactory = { 0x00000001, 0x0000, 0x0000, { 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46 } };

BOOL STDMETHODCALLTYPE DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    int processId = ::GetCurrentProcessId();

    switch (ul_reason_for_call)
    {
        case DLL_PROCESS_ATTACH:
            Logger::Log("DllMain DLL_PROCESS_ATTACH\n");
            // printf("Waiting for debugger attach...pid: %i\n", processId);
            // cin.ignore();
            break;

        case DLL_THREAD_ATTACH:
            // Logger::Log("DllMain DLL_THREAD_ATTACH\n");
            break;

        case DLL_THREAD_DETACH:
            // Logger::Log("DllMain DLL_THREAD_DETACH\n");
            break;

        case DLL_PROCESS_DETACH:
            // Logger::Log("DllMain DLL_PROCESS_DETACH\n");
            break;
    }
    return TRUE;
}

extern "C" HRESULT STDMETHODCALLTYPE DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID * ppv)
{
    Logger::Log("DllGetClassObject\n");

    // {cf0d821e-299b-5307-a3d8-b283c03916dd}
    const GUID CLSID_CorProfiler = { 0xcf0d821e, 0x299b, 0x5307, { 0xa3, 0xd8, 0xb2, 0x83, 0xc0, 0x39, 0x16, 0xdd } };

    if (ppv == nullptr || rclsid != CLSID_CorProfiler)
    {
        return E_FAIL;
    }

    auto factory = new ClassFactory;
    if (factory == nullptr)
    {
        return E_FAIL;
    }

    return factory->QueryInterface(riid, ppv);
}

extern "C" HRESULT STDMETHODCALLTYPE DllCanUnloadNow()
{
    Logger::Log("DllCanUnloadNow\n");
    return S_OK;
}
