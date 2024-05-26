#include <cstdio>
#include <windows.h>
#include <iostream>
#include <thread>
#include <mutex>
#include <list>
#include <stack>

using namespace std;

static bool isPalindrome(const string& str) {
	stack<char> s;
	int n = str.length();

	for (int i = 0; i < n / 2; i++) {
		s.push(str[i]);
	}

	int start = (n + 1) / 2;

	for (int i = start; i < n; i++) {
		if (str[i] != s.top()) {
			return false;
		}
		s.pop();
	}
	return true;
}

int main() {
	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);
	string str;
	cout << "Введите строку: ";
	getline(cin, str);

	cout << str << " is a palindrome: " << boolalpha << isPalindrome(str) << '\n';
}
