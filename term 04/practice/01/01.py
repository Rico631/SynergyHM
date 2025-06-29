def process(A, B):
    sum_positive = 0
    count_positive = 0
    count_greater_B = 0
    mult_greater_B = 1

    for x in A:
        if x > 0:
            sum_positive += x
            count_positive += 1
        if x > B:
            count_greater_B += 1
            mult_greater_B *= x

    return {
        "sum_positive": sum_positive,
        "count_positive": count_positive,
        "count_greater_B": count_greater_B,
        "mult_greater_B": mult_greater_B
    }

# Тестирование
test_cases = [
    {"A": [1, -2, 3, 4, -5], "B": 2},     # Все ветки: положительные есть, элементы > B есть
    {"A": [-1, -2, -3], "B": 0},          # Положительных нет, элементов > B тоже нет
    {"A": [], "B": 5},                    # Пустой массив
    {"A": [10, 20, 30], "B": 5},          # Все элементы > B
    {"A": [5, -3, 6, 2, 1], "B": 10},     # Элементы > B отсутствуют
]

for i, test in enumerate(test_cases):
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