# Определение функции process, принимающей список A и число B
def process(A, B):
    # Инициализация переменной для суммы положительных чисел
    sum_positive = 0
    # Инициализация счётчика положительных чисел
    count_positive = 0
    # Инициализация счётчика чисел, превышающих B
    count_greater_B = 0
    # Инициализация переменной для произведения чисел, превышающих B (начинается с 1)
    mult_greater_B = 1

    # Проход по каждому элементу массива A
    for x in A:
        # Если число положительное
        if x > 0:
            sum_positive += x          # Добавляем его к сумме положительных
            count_positive += 1        # Увеличиваем счётчик положительных

        # Если число больше B
        if x > B:
            count_greater_B += 1       # Увеличиваем счётчик чисел, превышающих B
            mult_greater_B *= x        # Умножаем на него переменную произведения

    # Возвращаем результаты в виде словаря
    return {
        "sum_positive": sum_positive,           # Сумма положительных чисел
        "count_positive": count_positive,       # Кол-во положительных чисел
        "count_greater_B": count_greater_B,     # Кол-во чисел больше B
        "mult_greater_B": mult_greater_B        # Произведение чисел больше B
    }


# Тестирование функции на различных входных данных
test_cases = [
    # Смешанные числа, есть положительные и > B
    {"A": [1, -2, 3, 4, -5], "B": 2},
    # Только отрицательные, ничего не больше B
    {"A": [-1, -2, -3], "B": 0},
    {"A": [], "B": 5},                    # Пустой список, все значения по умолчанию
    {"A": [10, 20, 30], "B": 5},          # Все элементы > B
    {"A": [5, -3, 6, 2, 1], "B": 10},     # Все элементы <= B
]

# Перебор тестов и вывод результатов
for i, test in enumerate(test_cases):
    # Вызов функции с текущим тестовым набором
    result = process(test["A"], test["B"])
    print(f"Тест {i+1}:")
    print("Массив:", test["A"], "| B =", test["B"])
    print("Результат:", result)
    print()


# Тест 1:
# Массив: [1, -2, 3, 4, -5] | B = 2
# Результат: {'sum_positive': 8, 'count_positive': 3, 'count_greater_B': 2, 'mult_greater_B': 12}
# Тест 2:
# Массив: [-1, -2, -3] | B = 0
# Результат: {'sum_positive': 0, 'count_positive': 0, 'count_greater_B': 0, 'mult_greater_B': 1}
# Тест 3:
# Массив: [] | B = 5
# Результат: {'sum_positive': 0, 'count_positive': 0, 'count_greater_B': 0, 'mult_greater_B': 1}
# Тест 4:
# Массив: [10, 20, 30] | B = 5
# Результат: {'sum_positive': 60, 'count_positive': 3, 'count_greater_B': 3, 'mult_greater_B': 6000}
# Тест 5:
# Массив: [5, -3, 6, 2, 1] | B = 10
# Результат: {'sum_positive': 14, 'count_positive': 4, 'count_greater_B': 0, 'mult_greater_B': 1}