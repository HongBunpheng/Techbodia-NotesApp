import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5247/api",
});

export const getNotes = () => api.get("/notes");
export const createNote = (data: { title: string; content?: string }) =>
  api.post("/notes", data);
export const updateNote = (
  id: number,
  data: { title: string; content?: string }
) => api.put(`/notes/${id}`, data);
export const deleteNote = (id: number) => api.delete(`/notes/${id}`);
