#include <cstdio>
#include <windows.h>
#include <iostream>
#include <thread>
#include <mutex>
#include <list>

using namespace std;

int main()
{
	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);

	list<int> myList;
	int value;

	cout << "Введите 5 элементов списка: ";
	for (int i = 0; i < 5; i++) {
		cin >> value;
		myList.push_back(value);
	}

	int valueToRemove;
	cout << "Введите значение для удаления: ";
	cin >> valueToRemove;

	myList.remove(valueToRemove);

	cout << "Обновленный список: ";
	for (int x : myList) {
		cout << x << ' ';
	}
}