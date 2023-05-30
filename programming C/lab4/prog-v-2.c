#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct Person {
    char first_name[50];
    char last_name[50];
    char phone[20];
    char birthdate[20];
    struct Person* prev;  // указатель на предыдущий узел списка
    struct Person* next;  // указатель на следующий узел списка
};

// функция добавления нового элемента в список 
struct Person* add_person(struct Person* head, char* first_name, char* last_name, char* phone, char* birthdate) {
    struct Person* new_person = (struct Person*)malloc(sizeof(struct Person));
    strcpy(new_person->first_name, first_name);
    strcpy(new_person->last_name, last_name);
    strcpy(new_person->phone, phone);
    strcpy(new_person->birthdate, birthdate);
    new_person->prev = NULL;
    new_person->next = head;
    if (head != NULL) {
        head->prev = new_person;
    }
    return new_person;
}

// функция поиска элемента по заданному условию.
struct Person* find_person_by_lastname(struct Person* head, char* last_name) {
    struct Person* current = head;
    while (current != NULL) {
        if (strcmp(current->last_name, last_name) == 0) {
            return current;
        }
        current = current->next;
    }
    return NULL;
}

// функция вывода элементов списка на экран
void print_list(struct Person* head) {
    struct Person* current = head;
    while (current != NULL) {
        printf("%s %s, phone: %s, birthdate: %s\n", current->first_name, current->last_name, current->phone, current->birthdate);
        current = current->next;
    }
}

// главная функция 
int main() {
    struct person *head = NULL;
    char name[50], surname[50], phone[20], birthday[20];
    int n, i;

    printf("Enter the number of people: ");
    scanf("%d", &n);

    for (i = 0; i < n; i++) {
        printf("Enter name: ");
        scanf("%s", name);
        printf("Enter surname: ");
        scanf("%s", surname);
        printf("Enter phone: ");
        scanf("%s", phone);
        printf("Enter birthday: ");
        scanf("%s", birthday);
        add_person(&head, name, surname, phone, birthday);
    }

    printf("\nUnsorted list:\n");
    print_list(head);

    sort_list(&head);

    printf("\nSorted list:\n");
    print_list(head);

    return 0;
}