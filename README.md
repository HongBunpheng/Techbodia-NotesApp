<img width="1238" height="853" alt="image" src="https://github.com/user-attachments/assets/d0a00c3c-62a9-4a84-9350-40e8f07a0dbb" /># Notes App

A full-stack Notes application built using modern web technologies.  
This project demonstrates basic CRUD operations, REST API integration, and responsive UI design.

---

## Tech Stack

### Frontend
- Vue 3 (Composition API)
- TypeScript
- Vite
- Tailwind CSS v4
- Axios

### Backend
- ASP.NET Core Web API
- C#
- Dapper (ORM)

### Database
- SQL Server

---

## Features

- Create notes (title required, content optional)
- View a list of notes (title and creation date)
- Click a note to view full details
- Edit and delete notes
- Search notes by title
- Sort notes by newest or oldest
- Responsive UI using Tailwind CSS

---

## How to Run the Application

### 1Database Setup (SQL Server)

1. Open **SQL Server Management Studio/VScode**
2. Create a new query
3. Run the SQL script:

This will create the database and required tables.

---

### Run Backend (ASP.NET Core)

```bash
cd NotesApp
dotnet restore
dotnet run
```

Backend API will be available at:

http://localhost:5247

Swagger UI:

http://localhost:5247/swagger

### Run Frontend (Vue)
```bash
cd notes-frontend
npm install
npm run dev
```
Frontend application will be available at:

http://localhost:5173

### screenshot 
<img width="1471" height="880" alt="image" src="https://github.com/user-attachments/assets/46e1698a-66c9-4bcd-a3e4-7c78524aec3c" />

<img width="1238" height="853" alt="image" src="https://github.com/user-attachments/assets/fe14a7e6-805f-4beb-b917-35064c16cdaf" />



