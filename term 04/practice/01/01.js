function processArray(A, B) {
    let sumPositive = 0;
    let countPositive = 0;
    let countGreaterB = 0;
    let multGreaterB = 1;

    for (let x of A) {
        if (x > 0) {
            sumPositive += x;
            countPositive += 1;
        }
        if (x > B) {
            countGreaterB += 1;
            multGreaterB *= x;
        }
    }

    return {
        sum_positive: sumPositive,
        count_positive: countPositive,
        count_greater_B: countGreaterB,
        mult_greater_B: multGreaterB
    };
}

// Тестовые данные
const testCases = [
    { A: [1, -2, 3, 4, -5], B: 2 },
    { A: [-1, -2, -3], B: 0 },
    { A: [], B: 5 },
    { A: [10, 20, 30], B: 5 },
    { A: [5, -3, 6, 2, 1], B: 10 }
];

testCases.forEach((test, index) => {
    const result = processArray(test.A, test.B);
    console.log(`Тест ${index + 1}:`);
    console.log("Массив:", test.A, "| B =", test.B);
    console.log("Результат:", result);
    console.log();
});

// Тест 1:
// Массив: [ 1, -2, 3, 4, -5 ] | B = 2
// Результат: {
//   sum_positive: 8,
//   count_positive: 3,
//   count_greater_B: 2,
//   mult_greater_B: 12
// }
// Тест 2:
// Массив: [ -1, -2, -3 ] | B = 0
// Результат: {
//   sum_positive: 0,
//   count_positive: 0,
//   count_greater_B: 0,
//   mult_greater_B: 1
// }
// Тест 3:
// Массив: [] | B = 5
// Результат: {
//   sum_positive: 0,
//   count_positive: 0,
//   count_greater_B: 0,
//   mult_greater_B: 1
// }
// Тест 4:
// Массив: [ 10, 20, 30 ] | B = 5
// Результат: {
//   sum_positive: 60,
//   count_positive: 3,
//   count_greater_B: 3,
//   mult_greater_B: 6000
// }
// Тест 5:
// Массив: [ 5, -3, 6, 2, 1 ] | B = 10
// Результат: {
//   sum_positive: 14,
//   count_positive: 4,
//   count_greater_B: 0,
//   mult_greater_B: 1
// }
