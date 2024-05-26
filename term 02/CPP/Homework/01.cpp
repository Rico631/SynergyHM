#include <cstdio>
#include <windows.h>
#include <iostream>
#include <thread>
#include <mutex>

using namespace std;


int main()
{
	unique_ptr<int> ptr(new int(5));
	cout << *ptr << endl;
	*ptr = 10;
	cout << *ptr << endl;
}
