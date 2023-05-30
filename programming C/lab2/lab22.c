#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

int main() {
  setlocale(LC_ALL, "Russian"); // устанавливаем русскую локаль

  int n, max, max_index;
  wprintf(L"Введите размерность массива: ");
  wscanf(L"%d", &n);

  int* a = (int*)malloc(n * sizeof(int)); // выделяем память под массив

  // считываем элементы массива
  for (int i = 0; i < n; i++) {
    wprintf(L"Введите a[%d]: ", i);
    wscanf(L"%d", &a[i]);
  }

  // находим максимальный элемент и его индекс
  max = a[0];
  max_index = 0;
  for (int i = 1; i < n; i++) {
    if (a[i] > max) {
      max = a[i];
      max_index = i;
    }
  }

  // умножаем элементы с четными индексами до первого максимального на максимальный элемент
  for (int i = 0; i < max_index; i += 2) {
    a[i] *= max;
  }

  // выводим измененный массив на экран
  wprintf(L"\nИзмененный массив:\n");
  for (int i = 0; i < n; i++) {
    wprintf(L"%d ", a[i]);
  }

  free(a); // освобождаем память
  
  getch(); // ожидание ввода пользователя
  return 0;
}
