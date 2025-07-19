// Объявление функции processArray, принимающей массив A и число B
function processArray(A, B) {
  // Инициализация суммы положительных чисел
  let sumPositive = 0;
  // Инициализация счётчика положительных чисел
  let countPositive = 0;
  // Инициализация счётчика чисел больше B
  let countGreaterB = 0;
  // Инициализация переменной для произведения чисел больше B (начальное значение: 1)
  let multGreaterB = 1;

  // Цикл по всем элементам массива A
  for (let x of A) {
    // Если элемент положительный
    if (x > 0) {
      sumPositive += x; // Добавляем его к сумме
      countPositive += 1; // Увеличиваем счётчик положительных
    }
    // Если элемент больше B
    if (x > B) {
      countGreaterB += 1; // Увеличиваем счётчик чисел больше B
      multGreaterB *= x; // Умножаем произведение на этот элемент
    }
  }

  if(countGreaterB == 0)
    multGreaterB = 0

  // Возвращаем объект с результатами
  return {
    sum_positive: sumPositive, // Сумма положительных элементов
    count_positive: countPositive, // Количество положительных элементов
    count_greater_B: countGreaterB, // Количество элементов больше B
    mult_greater_B: multGreaterB, // Произведение элементов больше B
  };
}

// Массив тестовых случаев
const testCases = [
  { A: [1, -2, 3, 4, -5], B: 2 }, // Есть положительные, есть элементы > B
  { A: [-1, -2, -3], B: 0 }, // Только отрицательные, > B нет
  { A: [], B: 5 }, // Пустой массив
  { A: [10, 20, 30], B: 5 }, // Все элементы > B
  { A: [5, -3, 6, 2, 1], B: 10 }, // Нет элементов > B
];

// Проход по каждому тестовому случаю
testCases.forEach((test, index) => {
  // Вызов функции processArray для текущего случая
  const result = processArray(test.A, test.B);
  // Вывод информации о тесте
  console.log(`Тест ${index + 1}:`);
  console.log("Массив:", test.A, "| B =", test.B);
  console.log("Результат:", result);
  console.log();
});

// Тест 1:
// Массив: [1, -2, 3, 4, -5] | B = 2
// Результат:
// {
//   sum_positive: 8,
//   count_positive: 3,
//   count_greater_B: 2,
//   mult_greater_B: 12
// }

// Тест 2:
// Массив: [-1, -2, -3] | B = 0
// Результат:
// {
//   sum_positive: 0,
//   count_positive: 0,
//   count_greater_B: 0,
//   mult_greater_B: 1
// }

// Тест 3:
// Массив: [] | B = 5
// Результат:
// {
//   sum_positive: 0,
//   count_positive: 0,
//   count_greater_B: 0,
//   mult_greater_B: 0
// }

// Тест 4:
// Массив: [10, 20, 30] | B = 5
// Результат:
// {
//   sum_positive: 60,
//   count_positive: 3,
//   count_greater_B: 3,
//   mult_greater_B: 6000
// }

// Тест 5:
// Массив: [5, -3, 6, 2, 1] | B = 10
// Результат:
// {
//   sum_positive: 14,
//   count_positive: 4,
//   count_greater_B: 0,
//   mult_greater_B: 0
// }
