import { isLeapYear, getDaysUntilNewYear } from "./utils";

describe("isLeapYear", () => {
  test("2020 — високосный год", () => {
    expect(isLeapYear(2020)).toBe(true);
  });

  test("1900 — не високосный год", () => {
    expect(isLeapYear(1900)).toBe(false);
  });

  test("2000 — високосный год", () => {
    expect(isLeapYear(2000)).toBe(true);
  });

  test("2023 — не високосный год", () => {
    expect(isLeapYear(2023)).toBe(false);
  });
});

describe("getDaysUntilNewYear", () => {
  test("01.01.2023 → 364 дня до Нового года", () => {
    expect(getDaysUntilNewYear("01.01.2023")).toBe(364);
  });

  test("31.12.2023 → 0 дней до Нового года", () => {
    expect(getDaysUntilNewYear("31.12.2023")).toBe(0);
  });

  test("29.02.2020 → корректная дата", () => {
    expect(getDaysUntilNewYear("29.02.2020")).toBe(306);
  });

  test("29.02.2021 → некорректная дата", () => {
    expect(() => getDaysUntilNewYear("29.02.2021")).toThrow(
      "Некорректная дата"
    );
  });

  test("неправильный формат → ошибка", () => {
    expect(() => getDaysUntilNewYear("2023-01-01")).toThrow(
      "Неверный формат даты"
    );
  });
});