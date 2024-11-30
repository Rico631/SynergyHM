document.addEventListener("DOMContentLoaded", () => {
  const searchButton = document.querySelector(".form-search-button");
  const searchInput = document.querySelector(".form-search-input");
  const searchClear = document.querySelector(".form-search-clear");

  const downloadButton = document.querySelector(".download-notes-button");

  const newNoteTitle = document.querySelector(".create-note-title");
  const newNoteText = document.querySelector(".create-note-text");
  const newNoteClose = document.querySelector(".create-note-close");
  const createNote = document.querySelector(".create-note");
  const noteList = document.querySelector(".note-list");

  let editingNoteIndex = null;
  let searchQuery = "";

  // Фильтрация заметок по запросу
  const filterNotes = (notes, query) => {
    if (!query) return notes;
    return notes.filter((note) => {
      return (
        note.title.toLowerCase().includes(query.toLowerCase()) ||
        note.text.toLowerCase().includes(query.toLowerCase())
      );
    });
  };

  // Поиск заметок
  searchButton.addEventListener("click", () => searchInput.focus());

  // Очистка поля поиска и фокус
  searchClear.addEventListener("click", () => {
    searchInput.value = "";
    searchInput.focus();
    searchQuery = "";
    renderNotes();
  });

  // Обработка ввода в поле поиска
  searchInput.addEventListener("input", () => {
    searchQuery = searchInput.value.trim();
    renderNotes();
  });

  // Скачивание заметок
  downloadButton.addEventListener("click", () => {
    const notes = JSON.parse(localStorage.getItem("notes")) || [];
    if (notes.length === 0) {
      alert("Нет заметок для скачивания.");
      return;
    }

    const downloadDateTime = new Date()
      .toLocaleString()
      .replace(/[^\w\s]/g, "-")
      .replace(/\s+/g, "");

    const notesJSON = JSON.stringify(notes, null, 2);
    const blob = new Blob([notesJSON], { type: "application/json" });

    const link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = `notes_${downloadDateTime}.json`; // Имя файла
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  });

  const updateHeight = (el) => {
    el.style.height = "auto";
    el.style.height = Math.min(el.scrollHeight, 500) + "px";
  };

  // Автоматическое изменение высоты текстового поля
  newNoteText.addEventListener("input", (e) => updateHeight(e.target));

  // Получаем заметки из localStorage
  const getNotes = () => {
    const notes = JSON.parse(localStorage.getItem("notes"));
    return notes ? notes : [];
  };

  // Сохранение заметок в localStorage
  const saveNotes = (notes) => {
    localStorage.setItem("notes", JSON.stringify(notes));
  };

  // Экранирование текста
  const escapeHTML = (str) => {
    const element = document.createElement("div");
    if (str) {
      element.innerText = str;
      element.textContent = str;
    }
    return element.innerHTML;
  };

  // Отображение заметок
  const renderNotes = () => {
    const notes = getNotes();
    const filteredNotes = filterNotes(notes, searchQuery);
    noteList.innerHTML = "";

    const isToday = (date) => {
      const today = new Date();
      return (
        date.getDate() === today.getDate() &&
        date.getMonth() === today.getMonth() &&
        date.getFullYear() === today.getFullYear()
      );
    };

    const formatDate = (date) =>
      date.toLocaleDateString("ru-RU", {
        day: "numeric",
        month: "long",
      });

    const formatTime = (date) =>
      date.toLocaleTimeString("ru-RU", {
        hour: "2-digit",
        minute: "2-digit",
      });

    filteredNotes.forEach((note, index) => {
      const updatedAt = new Date(note.updatedAt);
      const createdAt = new Date(note.createdAt);

      const updatedDisplay = isToday(updatedAt)
        ? formatTime(updatedAt)
        : formatDate(updatedAt);

      const noteHTML = `
          <div class="note-card">
            <h2>${escapeHTML(note.title)}</h2>
            <p>${escapeHTML(note.text)}</p>
            <div class="note-card-actions">
              <button class="edit-button" onclick="editNote(${index})">
                <i class="material-icons">edit</i>
              </button>
              <button class="delete-button" onclick="deleteNote(${index})">
                <i class="material-icons">delete</i>
              </button>
            </div>
            <div class="note-updated" title="Создано: ${formatDate(createdAt)}">
              Изменено: ${updatedDisplay}
            </div>
          </div>
        `;

      noteList.innerHTML += noteHTML;
    });
  };

  // Закрытие формы
  const closeForm = () => {
    newNoteTitle.value = "";
    newNoteText.value = "";
    editingNoteIndex = null;
    updateHeight(newNoteText);
  };

  // Обработка создания или редактирования заметки
  const saveNoteHandler = () => {
    const title = newNoteTitle.value.trim();
    const text = newNoteText.value.trim();

    const currentDate = new Date().toISOString();

    if (title || text) {
      const notes = getNotes();

      if (editingNoteIndex !== null) {
        // Если редактируем, то обновляем заметку
        notes[editingNoteIndex] = { title, text, updatedAt: currentDate };
      } else {
        // Иначе создаем новую заметку
        notes.push({
          title,
          text,
          createdAt: currentDate,
          updatedAt: currentDate,
        });
      }

      saveNotes(notes);
      renderNotes();
      closeForm();
    }
  };

  newNoteClose.addEventListener("click", saveNoteHandler);

  // Редактирование заметки
  window.editNote = (index) => {
    const notes = getNotes();
    const note = notes[index];
    newNoteTitle.value = note.title;
    newNoteText.value = note.text;
    editingNoteIndex = index;
    createNote.style.display = "block";
    updateHeight(newNoteText);
  };

  // Удаление заметки
  window.deleteNote = (index) => {
    if (confirm("Вы уверены, что хотите удалить эту заметку?")) {
      const notes = getNotes();
      notes.splice(index, 1);
      saveNotes(notes);
      renderNotes();
    }
  };

  document.addEventListener("click", (event) => {
    if (
      !createNote.contains(event.target) &&
      !event.target.closest(".edit-button") &&
      !event.target.closest(".delete-button")
    ) {
      saveNoteHandler();
    }
  });

  // Рендерим заметки при загрузке страницы
  renderNotes();
});
