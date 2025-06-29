import React, { useState } from "react";
import { isLeapYear, getDaysUntilNewYear } from "../utils";

const NewYearCounter = () => {
  const [input, setInput] = useState("");
  const [daysLeft, setDaysLeft] = useState("");
  const [leapYearInfo, setLeapYearInfo] = useState("");

  const handleCalculate = () => {
    try {
      const days = getDaysUntilNewYear(input);
      const year = parseInt(input.split(".")[2], 10);
      setDaysLeft(`До Нового года осталось ${days} дней`);
      setLeapYearInfo(
        isLeapYear(year) ? "Год високосный" : "Год не високосный"
      );
    } catch (error) {
      setDaysLeft(error.message);
      setLeapYearInfo("");
    }
  };

  return (
    <div className="container">
      <h2>Счетчик до Нового Года</h2>
      <input
        type="text"
        value={input}
        onChange={(e) => setInput(e.target.value)}
        placeholder="дд.мм.гггг"
      />
      <button onClick={handleCalculate}>Рассчитать</button>

      <div className="result">{daysLeft}</div>
      <div className="result">{leapYearInfo}</div>
    </div>
  );
};

export default NewYearCounter;
