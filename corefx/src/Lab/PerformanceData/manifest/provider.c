//
// Common function implementation generated by CTRPP tool.
//

#define __INIT_provider_IMP
#include "provider.h"

ULONG
PerfAutoInitialize(
    void
)
{
    ULONG Status = ERROR_SUCCESS;

    Status = PerfStartProvider(
            (LPGUID) & ProviderGuid_provider_1,
            NULL,
            & hDataSource_provider_1);
    if (Status != ERROR_SUCCESS) {
        goto Cleanup;
    }

    Status = PerfSetCounterSetInfo(
            hDataSource_provider_1,
            (PPERF_COUNTERSET_INFO) & CtrSet_provider_1_1,
            dwCtrSet_provider_1_1);
    if (Status != ERROR_SUCCESS) {
        goto Cleanup;
    }

Cleanup:
    if (Status != ERROR_SUCCESS) {
        PerfStopProvider(hDataSource_provider_1);
    }
    return Status;
}

ULONG
PerfAutoCleanup(
    void
)
{
    ULONG Status;

    Status = PerfStopProvider(hDataSource_provider_1);
    return ERROR_SUCCESS;
}
