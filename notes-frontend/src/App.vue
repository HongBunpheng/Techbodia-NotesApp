<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import {
  getNotes,
  createNote,
  updateNote,
  deleteNote,
} from "./services/noteApi";

type Note = {
  id: number;
  title: string;
  content?: string;
  createdAt: string;
};

const notes = ref<Note[]>([]);
const title = ref("");
const content = ref("");
const editingId = ref<number | null>(null);

const search = ref("");
const sort = ref<"new" | "old">("new");

const selectedNote = ref<Note | null>(null);

const loadNotes = async () => {
  const res = await getNotes();
  notes.value = res.data;
};

onMounted(loadNotes);

const saveNote = async () => {
  if (!title.value.trim()) return;

  if (editingId.value) {
    await updateNote(editingId.value, {
      title: title.value,
      content: content.value,
    });
  } else {
    await createNote({
      title: title.value,
      content: content.value,
    });
  }

  title.value = "";
  content.value = "";
  editingId.value = null;
  loadNotes();
};

const openNote = (note: Note) => {
  selectedNote.value = note;
};

const closeNote = () => {
  selectedNote.value = null;
};

const editNote = (note: Note) => {
  editingId.value = note.id;
  title.value = note.title;
  content.value = note.content ?? "";
  closeNote();
};

const removeNote = async (id: number) => {
  await deleteNote(id);
  closeNote();
  loadNotes();
};

const filteredNotes = computed(() => {
  return notes.value
    .filter((n) =>
      n.title.toLowerCase().includes(search.value.toLowerCase())
    )
    .sort((a, b) =>
      sort.value === "new"
        ? new Date(b.createdAt).getTime() -
        new Date(a.createdAt).getTime()
        : new Date(a.createdAt).getTime() -
        new Date(b.createdAt).getTime()
    );
});
</script>

<template>
  <div class="min-h-screen bg-slate-100 px-4 py-10 flex justify-center items-start">
    <div class="w-full max-w-3xl rounded-2xl bg-white border border-slate-200 shadow-sm p-8">
      <div class="mb-8">
        <h1 class="text-xl font-semibold text-slate-800 flex items-center gap-2">
          Notes
        </h1>
        <p class="text-sm text-slate-500">
          Click a note to view details
        </p>
      </div>

      <div class="mb-6 grid grid-cols-1 gap-3 sm:grid-cols-[1fr_160px]">
        <input v-model="search" placeholder="Search notes..." class="w-full rounded-lg border border-slate-200 bg-white px-4 py-2.5 text-sm
                focus:outline-none focus:ring-2 focus:ring-blue-300" />

        <div class="relative">
          <select v-model="sort" class="w-full appearance-none rounded-lg border border-slate-200 bg-white
                  px-4 py-2.5 pr-9 text-sm
                  focus:outline-none focus:ring-2 focus:ring-blue-300">
            <option value="new">Newest</option>
            <option value="old">Oldest</option>
          </select>

          <svg class="pointer-events-none absolute right-3 top-1/2 h-4 w-4 -translate-y-1/2 text-slate-400"
            xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
          </svg>
        </div>
      </div>

      <div class="mb-8 rounded-xl border border-slate-200 bg-slate-50 p-5 space-y-4">
        <input v-model="title" placeholder="Note title" class="w-full rounded-lg border border-slate-200 px-4 py-2 text-sm
                focus:outline-none focus:ring-2 focus:ring-blue-300" />

        <textarea v-model="content" rows="4" placeholder="Write your note..." class="w-full resize-none rounded-lg border border-slate-200 px-4 py-2 text-sm
                focus:outline-none focus:ring-2 focus:ring-blue-300" />

        <div class="flex justify-end">
          <button @click="saveNote" :disabled="!title.trim()" class="rounded-lg bg-blue-600 px-5 py-2 text-sm font-medium text-white
                  hover:bg-blue-700 disabled:opacity-50 transition">
            {{ editingId ? "Update Note" : "Create Note" }}
          </button>
        </div>
      </div>

      <div v-if="filteredNotes.length === 0" class="text-center text-sm text-slate-400">
        No notes yet
      </div>

      <div class="space-y-3">
        <div v-for="note in filteredNotes" :key="note.id" @click="openNote(note)" class="cursor-pointer rounded-lg border border-slate-200 bg-white px-5 py-4
                hover:bg-slate-50 hover:shadow-sm transition">
          <h2 class="text-sm font-semibold text-slate-800">
            {{ note.title }}
          </h2>
          <p class="mt-1 text-xs text-slate-500">
            {{ new Date(note.createdAt).toLocaleString() }}
          </p>
        </div>
      </div>
    </div>
  </div>

  <div v-if="selectedNote" class="fixed inset-0 bg-black/30 flex items-center justify-center px-4">
    <div class="w-full max-w-lg rounded-2xl bg-white p-6 shadow-lg">
      <div class="flex justify-between items-start mb-4">
        <div>
          <h2 class="text-lg font-semibold text-slate-800">
            {{ selectedNote.title }}
          </h2>
          <p class="text-xs text-slate-500">
            {{ new Date(selectedNote.createdAt).toLocaleString() }}
          </p>
        </div>

        <button @click="closeNote" class="text-slate-400 hover:text-slate-600 text-xl">
          ✕
        </button>
      </div>

      <p class="text-sm text-slate-700 whitespace-pre-wrap mb-6">
        {{ selectedNote.content || "No content" }}
      </p>

      <div class="flex justify-end gap-3">
        <button @click="editNote(selectedNote)"
          class="rounded-md bg-blue-100 px-4 py-1.5 text-sm text-blue-700 hover:bg-blue-200">
          Edit
        </button>

        <button @click="removeNote(selectedNote.id)"
          class="rounded-md bg-red-100 px-4 py-1.5 text-sm text-red-700 hover:bg-red-200">
          Delete
        </button>
      </div>
    </div>
  </div>
</template>
