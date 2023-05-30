#include <stdio.h>
#include <math.h>
#include <windows.h>

int main() {

	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);

    // объявление переменных
    float x, y, z, a, b;
    float sum, cinus, cosinus;
    x = y = z = a = b = sum = 0.0;

    // ввод переменных с клавиатуры
    printf("print x: ");
    scanf("%f", &x);
    printf("print y: ");
    scanf("%f", &y);
    printf("print z: ");
    scanf("%f", &z);

    // проверка
    if (z == 0) {
        printf("imposible to count\n");
    } else {
        cinus = sin(x + y);
        a = ((1 + cinus * cinus) / (2 + fabs((x - 2 * x) / (1 + x * x * y * y)))) + x;
        printf("a = %f\n", a);

        cosinus = cos(atan(1 / z));
        b = cosinus * cosinus;
        printf("b = %f\n", b);
    }

    getch();
    return 0;
}
