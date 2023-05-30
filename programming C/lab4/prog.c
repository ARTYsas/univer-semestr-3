#include <stdio.h>
#include <string.h>
#include <locale.h>

struct note {
    char surname[20];
    char name[20];
    char phone[11];
    int birthday[3];
};

int main() {

    setlocale(LC_ALL, "Russian"); // установка локали для корректной работы функций ввода-вывода

    struct note arr[8], temp;
    int i, j, n;

    wprintf(L"Введите количество записей (максимум 8): ");
    wscanf(L"%d", &n);

    wprintf(L"Введите данные в формате: Фамилия Имя Номер_телефона День_рождения Месяц_рождения Год_рождения\n");

    for (i = 0; i < n; i++) {
        wprintf(L"Введите данные для записи %d: ", i+1);
        if (wscanf(L"%s %s %s %d %d %d", arr[i].surname, arr[i].name, arr[i].phone, &arr[i].birthday[0], &arr[i].birthday[1], &arr[i].birthday[2]) != 6) {
            break;
        }
    }

    // Сортировка по первым трем цифрам номера телефона
    for (i = 0; i < n - 1; i++) {
        for (j = i + 1; j < n; j++) {
            if (strncmp(arr[i].phone, arr[j].phone, 3) > 0) {
                temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
    }

    // Вывод информации о человеке по фамилии
    char search_surname[20];
    wprintf(L"Введите фамилию для поиска: ");
    wscanf(L"%s", search_surname);
    int found = 0;
    for (i = 0; i < n; i++) {
        if (strcmp(arr[i].surname, search_surname) == 0) {
            wprintf(L"Информация о %s %s:\n", arr[i].surname, arr[i].name);
            wprintf(L"  Номер телефона: %s\n", arr[i].phone);
            wprintf(L"  Дата рождения: %d.%d.%d\n", arr[i].birthday[0], arr[i].birthday[1], arr[i].birthday[2]);
            found = 1;
            break;
        }
    }

    if (!found) {
        wprintf(L"Запись с фамилией %s не найдена.\n", search_surname);
    }

    getch();
    return 0;
}
