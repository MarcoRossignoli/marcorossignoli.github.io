#include <iostream>

using namespace std;

extern "C" int AsmFunc();

int main()
{
    cout << "hello asm:" << AsmFunc() << endl;

    return 0;
}
