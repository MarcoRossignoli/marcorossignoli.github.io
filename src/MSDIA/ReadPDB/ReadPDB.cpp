// ReadPDB.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <dia2.h>
#include <atlbase.h>

using namespace std;

int main()
{
	string pdb = "C:\\git\\marcorossignoli.github.io\\src\\MSDIA\\ConsoleApp1\\bin\\Debug\\net6.0\\ConsoleApp1.exe";
	cout << pdb;

	CoInitialize(nullptr);

	CComPtr<IDiaDataSource> pSource;
	HRESULT hr = CoCreateInstance(CLSID_DiaSource,
		NULL,
		CLSCTX_INPROC_SERVER,
		__uuidof(IDiaDataSource),
		(void**)&pSource);

	if (FAILED(hr))
	{
		printf("Could not CoCreate CLSID_DiaSource. Register msdia80.dll.");
	}

	wchar_t wszFilename[_MAX_PATH];
	mbstowcs(wszFilename, pdb.c_str(), sizeof(wszFilename) / sizeof(wszFilename[0]));
	if (FAILED(pSource->loadDataFromPdb(wszFilename)))
	{
		if (FAILED(pSource->loadDataForExe(wszFilename, NULL, NULL)))
		{
			printf("loadDataFromPdb/Exe");
		}
	}

	CComPtr<IDiaSession> psession;
	if (FAILED(pSource->openSession(&psession)))
	{
		printf("openSession");
	}

	CComPtr<IDiaSymbol> pglobal;
	if (FAILED(psession->get_globalScope(&pglobal)))
	{
		printf("get_globalScope");
	}

	CComPtr<IDiaEnumTables> pTables;
	if (FAILED(psession->getEnumTables(&pTables)))
	{
		printf("getEnumTables");
	}

	CComPtr<IDiaTable> pTable;
	DWORD celt = 0;
	while (pTables->Next(1, &pTable, &celt) == S_OK && celt == 1)
	{
		BSTR tableName;
		pTable->get_name(&tableName);
		pTable = NULL;
	}
}
