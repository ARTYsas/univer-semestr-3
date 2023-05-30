#include <stdio.h>
#include <locale.h>

int main(void) {

	setlocale(LC_ALL, "Russian");

	//объявление переменных
	int num, counter, sum, sr;
	sum = 0;
	counter = 0;

	wprintf(L"ввод чисел (конец ввода '0')\n");
	wscanf(L"%d", &num);

	while (num != 0) {
		sum += num;
		counter++;
		wscanf(L"%d", &num);
	}

	sr = sum / counter;

	//отслеживание косяков
	/*wprintf(L"сумма: %d\n", sum);
	wprintf(L"коло-во: %d\n", counter);*/

	wprintf(L"среднее среднее арифметическое: %d\n", sr);

	//ожидание ввода
	getch();
	return 0;
}
