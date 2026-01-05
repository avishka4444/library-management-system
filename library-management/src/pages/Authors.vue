<template>
  <div>
    <div class="flex justify-between items-center mb-6 flex-col md:flex-row gap-4">
      <h1 class="text-2xl md:text-3xl font-bold text-gray-800">Authors</h1>
      <button
        @click="openCreateModal"
        class="px-4 py-2 bg-gray-900 text-white rounded-md hover:bg-gray-800 transition-colors text-sm font-medium"
      >
        + Add Author
      </button>
    </div>

    <div v-if="isLoading" class="flex items-center justify-center min-h-[400px]">
      <LoadingSpinner />
    </div>

    <div v-else-if="error" class="flex items-center justify-center min-h-[400px]">
      <p class="text-red-500" role="alert">{{ error }}</p>
    </div>

    <div v-else-if="authors.length === 0" class="flex items-center justify-center min-h-[400px]">
      <p class="text-gray-500">No authors found.</p>
    </div>

    <div v-else class="border border-gray-200 rounded-lg overflow-hidden overflow-x-auto">
      <table class="w-full border-collapse min-w-[600px]">
        <thead class="bg-gray-100">
          <tr>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base">
              Name
            </th>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base">
              Date of Birth
            </th>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base hidden lg:table-cell">
              Biography
            </th>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base">
              Actions
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="author in authors"
            :key="author.id"
            class="hover:bg-gray-50 border-b border-gray-100"
          >
            <td class="px-2 md:px-4 py-2 md:py-3 text-gray-700 font-medium text-sm md:text-base">
              {{ author.fullName }}
            </td>
            <td class="px-2 md:px-4 py-2 md:py-3 text-gray-600 text-sm md:text-base">
              {{ author.dateOfBirth ? new Date(author.dateOfBirth).toLocaleDateString() : '-' }}
            </td>
            <td class="px-2 md:px-4 py-2 md:py-3 text-gray-600 text-sm md:text-base hidden lg:table-cell line-clamp-2">
              {{ author.biography || '-' }}
            </td>
            <td class="px-2 md:px-4 py-2 md:py-3 text-sm md:text-base">
              <div class="flex gap-2">
                <button
                  @click="openEditModal(author)"
                  class="px-3 py-1 text-xs bg-gray-200 text-gray-700 rounded hover:bg-gray-300 transition-colors"
                >
                  Edit
                </button>
                <button
                  @click="handleDelete(author.id)"
                  class="px-3 py-1 text-xs bg-red-100 text-red-700 rounded hover:bg-red-200 transition-colors"
                >
                  Delete
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Create/Edit Author Modal -->
    <Modal :is-open="isModalOpen" :title="selectedAuthor ? 'Edit Author' : 'Create Author'" @close="closeModal">
      <AuthorForm :author="selectedAuthor" @saved="handleAuthorSaved" @cancel="closeModal" />
    </Modal>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { api, type Author } from '../services/api';
import LoadingSpinner from '../components/LoadingSpinner.vue';
import Modal from '../components/Modal.vue';
import AuthorForm from '../components/AuthorForm.vue';

const authors = ref<Author[]>([]);
const isLoading = ref(true);
const error = ref<string | null>(null);
const isModalOpen = ref(false);
const selectedAuthor = ref<Author | null>(null);

const loadAuthors = async () => {
  try {
    isLoading.value = true;
    error.value = null;
    authors.value = await api.getAuthors();
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to load authors';
  } finally {
    isLoading.value = false;
  }
};

const openCreateModal = () => {
  selectedAuthor.value = null;
  isModalOpen.value = true;
};

const openEditModal = (author: Author) => {
  selectedAuthor.value = author;
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
  selectedAuthor.value = null;
};

const handleAuthorSaved = async () => {
  closeModal();
  await loadAuthors();
};

const handleDelete = async (id: number) => {
  if (!confirm('Are you sure you want to delete this author?')) {
    return;
  }

  try {
    await api.deleteAuthor(id);
    await loadAuthors();
  } catch (err) {
    alert(err instanceof Error ? err.message : 'Failed to delete author');
  }
};

onMounted(async () => {
  await loadAuthors();
});
</script>
