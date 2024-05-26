#include <cstdio>
#include <windows.h>
#include <iostream>
#include <thread>
#include <mutex>
#include <list>
#include <stack>

using namespace std; 

mutex mt;

void print(int& el) {
	cout << this_thread::get_id() << "\tel:\t" << el << '\n';
}

void printArr(int arr[], int size, bool odd) {
	for (int i = 0; i < size; i++) {
		Sleep(1);
		{
			lock_guard<mutex> lg(mt);
			if (odd && arr[i] % 2 == 0) continue;
			if (!odd && arr[i] % 2 != 0) continue;
			print(arr[i]);
		}
	}
}

int main() {
	const int size = 10;
	int arr[size] = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

	thread th1(printArr, arr, size, false);
	thread th2(printArr, arr, size, true);

	th1.join();
	th2.join();
}

