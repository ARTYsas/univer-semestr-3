#include <stdio.h>
#include <stdbool.h>
#include <locale.h>


int main(void)
{
    setlocale(LC_ALL, "Russian"); // установка локали для корректной работы функций ввода-вывода

    int n, sum = 0;

    wprintf(L"Введите размерность массива, а затем его элементы: ");
    wscanf(L"%d", &n);

    bool first_negative = true;
    bool first_positive = true;
    bool second_positive = true;

    int* a = (int*)malloc(n * sizeof(int)); // выделение динамической памяти в куче

    for (int i = 0; i < n; ++i)
    {
        wscanf(L"%d", &a[i]);
    }

    // Нахождение среднего
    int mid = 0;
    if (n % 2 == 1)
        mid = a[n / 2];
    else
        mid = a[n / 2 - 1] + a[n / 2];

    // Добавление среднего за нулем
    for (int i = 0; i < n; ++i)
    {
        if (a[i] == 0)
        {
            for (int j = n; j > i; --j)
                a[j] = a[j - 1];
            a[++i] = mid;
            ++n;
        }
    }

    // Удаление первого отрицательного
    for (int i = 0; i < n; ++i)
    {
        if ((a[i] < 0) && first_negative)
        {
            first_negative = false;
            for (int j = i; j < n - 1; ++j)
                a[j] = a[j + 1];
            --i;
            --n;
        }
    }

    // Удаление второго положительного
    for (int i = 0; i < n; ++i)
    {
        // Поиск первого положительного
        if ((a[i] > 0) && first_positive)
            first_positive = false;

        if ((a[i] > 0) && !first_negative && second_positive)
        {
            second_positive = false;
            for (int j = i; j < n - 1; ++j)
                a[j] = a[j + 1];
            --i;
            --n;
        }
    }

    wprintf(L"полученный массив:\n");

    for (int i = 0; i < n; ++i)
    {
        wprintf(L"%d ", a[i]);
    }

    free(a); // освобождение выделенной динамической памяти
    
    getch(); // ожидание ввода пользователя
    return 0;
}
