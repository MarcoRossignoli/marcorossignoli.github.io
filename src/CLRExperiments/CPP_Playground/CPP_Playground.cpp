#include <iostream>

using namespace std;

class ClassTmp
{
    public:
        ClassTmp();
        ClassTmp(string s);
        void Print(std::ostream& out);

    private:    
        string _s;
};
ClassTmp::ClassTmp() {}
ClassTmp::ClassTmp(string s)
{
    this->_s = s;
}

void ClassTmp::Print(std::ostream& out)
{
    out << this->_s;
}

int main()
{
    ClassTmp tmp("Hello");
    tmp.Print(cout);
}




