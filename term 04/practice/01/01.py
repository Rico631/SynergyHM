# Объявление функции processArray, принимающей массив A и число B
def processArray(A, B):
    # Инициализация суммы положительных чисел
    sum_positive = 0
    # Инициализация счётчика положительных чисел
    count_positive = 0
    # Инициализация счётчика чисел больше B
    count_greater_B = 0
    # Инициализация переменной для произведения чисел больше B (начальное значение: 1)
    mult_greater_B = 1

    # Цикл по всем элементам массива A
    for x in A:
        # Если элемент положительный
        if x > 0:
            sum_positive += x   # Добавляем его к сумме
            count_positive += 1 # Увеличиваем счётчик положительных

        # Если элемент больше B
        if x > B:
            count_greater_B += 1 # Увеличиваем счётчик чисел больше B
            mult_greater_B *= x  # Умножаем произведение на этот элемент

    # Если счётчик чисел больше B равен 0
    if count_greater_B == 0:
        mult_greater_B = 0 # Произведение = 0
        
    # Возвращаем объект с результатами
    return {
        "sum_positive": sum_positive,           # Сумма положительных элементов
        "count_positive": count_positive,       # Количество положительных элементов
        "count_greater_B": count_greater_B,     # Количество элементов больше B
        "mult_greater_B": mult_greater_B        # Произведение элементов больше B
    }


# Массив тестовых случаев
test_cases = [
    {"A": [1, -2, 3, 4, -5], "B": 2}, # Есть положительные, есть элементы > B
    {"A": [-1, -2, -3], "B": 0},      # Только отрицательные, > B нет
    {"A": [], "B": 5},                # Пустой массив
    {"A": [10, 20, 30], "B": 5},      # Все элементы > B
    {"A": [5, -3, 6, 2, 1], "B": 10}, # Нет элементов > B
]

# Проход по каждому тестовому случаю
for i, test in enumerate(test_cases):
    # Вызов функции processArray для текущего случая
    result = processArray(test["A"], test["B"])
    # Вывод информации о тесте
    print(f"Тест {i+1}:")
    print("Массив:", test["A"], "| B =", test["B"])
    print("Результат:", result)
    print()


# Тест 1:
# Массив: [1, -2, 3, 4, -5] | B = 2
# Результат: {'sum_positive': 8, 'count_positive': 3, 'count_greater_B': 2, 'mult_greater_B': 12}
# Тест 2:
# Массив: [-1, -2, -3] | B = 0
# Результат: {'sum_positive': 0, 'count_positive': 0, 'count_greater_B': 0, 'mult_greater_B': 0}
# Тест 3:
# Массив: [] | B = 5
# Результат: {'sum_positive': 0, 'count_positive': 0, 'count_greater_B': 0, 'mult_greater_B': 0}
# Тест 4:
# Массив: [10, 20, 30] | B = 5
# Результат: {'sum_positive': 60, 'count_positive': 3, 'count_greater_B': 3, 'mult_greater_B': 6000}
# Тест 5:
# Массив: [5, -3, 6, 2, 1] | B = 10
# Результат: {'sum_positive': 14, 'count_positive': 4, 'count_greater_B': 0, 'mult_greater_B': 0}