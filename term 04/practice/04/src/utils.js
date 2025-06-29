export const isLeapYear = (year) => {
  return (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;
};

export const getDaysUntilNewYear = (dateString) => {
  const dateRegex = /^(\d{1,2})\.(\d{1,2})\.(\d{4})$/;
  const match = dateString.match(dateRegex);

  if (!match) throw new Error("Неверный формат даты");

  const day = parseInt(match[1], 10);
  const month = parseInt(match[2], 10);
  const year = parseInt(match[3], 10);

  const userDate = new Date(year, month - 1, day); // Месяцы в JS с 0
  const newYearsDate = new Date(year, 11, 31); // 31 декабря

  if (
    isNaN(userDate.getTime()) ||
    userDate.getDate() !== day ||
    userDate.getMonth() !== month - 1 ||
    userDate.getFullYear() !== year
  ) {
    throw new Error("Некорректная дата");
  }

  const diffTime = newYearsDate - userDate;
  const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

  return diffDays;
};
