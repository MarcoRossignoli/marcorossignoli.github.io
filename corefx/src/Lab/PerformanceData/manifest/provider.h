#ifndef _provider_H
#define _provider_H

#ifdef __cplusplus
extern "C" {
#endif

#include <windows.h>
#include <perflib.h>
#include <winperf.h>

#ifdef __INIT_provider_IMP
#define PROVIDER_GUID_provider_1 { 0x51D1685C, 0x35ED, 0x45BE, 0x99, 0xFE, 0x17, 0x26, 0x1A, 0x4F, 0x27, 0xF3 }
GUID ProviderGuid_provider_1 = PROVIDER_GUID_provider_1;
#else
extern GUID ProviderGuid_provider_1;
#endif

typedef struct _CTRSET_provider_1_1 {
    PERF_COUNTERSET_INFO CtSet_provider_1_1;
    PERF_COUNTER_INFO    Ctr_provider_1_1_1;
} CTRSET_provider_1_1, * PCTRSET_provider_1_1;

#ifdef __INIT_provider_IMP
#define CTRSET_GUID_provider_1_1 { 0x582803C9, 0xAACD, 0x45E5, 0x8C, 0x30, 0x57, 0x11, 0x41, 0xA2, 0x20, 0x92 }
GUID CtrSetGuid_provider_1_1 = CTRSET_GUID_provider_1_1;
CTRSET_provider_1_1 CtrSet_provider_1_1 = {
    { CTRSET_GUID_provider_1_1, PROVIDER_GUID_provider_1, 1, PERF_COUNTERSET_SINGLE_INSTANCE },
    { 1, PERF_COUNTER_DELTA, 0, sizeof(DWORD), PERF_DETAIL_NOVICE, 0, 0 }
};

ULONG dwCtrSet_provider_1_1 = sizeof(CTRSET_provider_1_1);
#else
extern GUID CtrSetGuid_provider_1_1;
extern CTRSET_provider_1_1 CtrSet_provider_1_1;
extern ULONG dwCtrSet_provider_1_1;
#endif

#ifdef __INIT_provider_IMP
HANDLE hDataSource_provider_1 = NULL;
#else
extern HANDLE hDataSource_provider_1;
#endif


ULONG PerfAutoInitialize(void);
ULONG PerfAutoCleanup(void);

#ifdef __cplusplus
}
#endif

#endif // _provider_H

