#include <cstdio>
#include <windows.h>
#include <iostream>
#include <thread>
#include <mutex>

using namespace std;

struct Node {
    int data;
    struct Node* next;
} Node;

struct Node* swap(struct Node* ptr1, struct Node* ptr2)
{
    struct Node* tmp = ptr2->next;
    ptr2->next = ptr1;
    ptr1->next = tmp;
    return ptr2;
}

void bubbleSort(struct Node** head, int count)
{
    struct Node** h;
    int i, j, swapped;

    for (i = 0; i < count; i++) {

        h = head;
        swapped = 0;

        for (j = 0; j < count - i - 1; j++) {

            struct Node* p1 = *h;
            struct Node* p2 = p1->next;

            if (p1->data > p2->data) {
                *h = swap(p1, p2);
                swapped = 1;
            }

            h = &(*h)->next;
        }

        if (swapped == 0)
            break;
    }
}


void printList(struct Node* n)
{
    while (n != NULL) {
        cout << n->data << " -> ";
        n = n->next;
    }
    cout << endl;
}

void insertAtTheBegin(struct Node** start_ref, int data)
{
    struct Node* ptr1
        = (struct Node*)malloc(sizeof(struct Node));

    ptr1->data = data;
    ptr1->next = *start_ref;
    *start_ref = ptr1;
}


int main()
{
	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);

    int arr[] = { 78, 20, 10, 32, 1, 5 };
    int list_size, i;

    struct Node* start = NULL;
    list_size = sizeof(arr) / sizeof(arr[0]);

    for (i = list_size - 1; i >= 0; i--)
        insertAtTheBegin(&start, arr[i]);

    cout << "До сортировки\n";
    printList(start);

    bubbleSort(&start, list_size);

    cout << "После сортировки\n";
    printList(start);

	return 0;
}