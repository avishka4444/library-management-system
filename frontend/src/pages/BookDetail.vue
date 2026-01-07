<template>
  <div>
    <div v-if="isLoading" class="flex items-center justify-center min-h-[400px]">
      <LoadingSpinner />
    </div>

    <div v-else-if="error || !book" class="flex items-center justify-center min-h-[400px]">
      <div class="flex flex-col items-center gap-4">
        <p class="text-red-500">{{ error || 'Book not found' }}</p>
        <button
          @click="$router.push('/')"
          class="px-4 py-2 bg-gray-200 text-black rounded-md hover:bg-gray-300 transition-colors"
        >
          Back to Home
        </button>
      </div>
    </div>

    <div v-else>
      <div class="flex flex-col md:flex-row gap-6 md:gap-8 items-start mb-6 md:mb-8">
        <div class="flex-shrink-0 w-full md:w-auto">
          <div
            class="bg-gray-200 w-full md:w-[300px] h-[250px] md:h-[300px] max-w-full md:max-w-[300px] flex items-center justify-center rounded-2xl shadow-lg"
          >
            <svg
              class="w-24 h-24 text-gray-400"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253"
              />
            </svg>
          </div>
        </div>

        <div class="flex flex-col items-start gap-3 md:gap-4 flex-1 w-full">
          <div class="w-full">
            <h1 class="text-2xl sm:text-3xl md:text-4xl font-bold text-gray-800 mb-2 break-words">
              {{ book.title }}
            </h1>
            <p class="text-lg md:text-xl text-gray-600 mb-3 md:mb-4">
              {{ book.authorName || 'No Author' }}
            </p>
            <div class="space-y-2">
              <p v-if="book.publishedDate" class="text-sm md:text-base text-gray-500">
                <span class="font-medium">Published:</span> {{ new Date(book.publishedDate).getFullYear() }}
              </p>
              <p class="text-sm md:text-base text-gray-500">
                <span class="font-medium">ISBN:</span> {{ book.isbn }}
              </p>
              <p class="text-sm md:text-base text-gray-500">
                <span class="font-medium">Total Copies:</span> {{ book.totalCopies }}
              </p>
              <p class="text-sm md:text-base text-gray-500">
                <span class="font-medium">Available Copies:</span> {{ book.availableCopies }}
              </p>
            </div>
            <div class="flex gap-3 mt-4">
              <button
                @click="openEditModal"
                class="px-4 py-2 bg-gray-900 text-white rounded-md hover:bg-gray-800 transition-colors text-sm font-medium"
              >
                Edit Book
              </button>
              <button
                @click="handleDelete"
                class="px-4 py-2 bg-red-100 text-red-700 rounded-md hover:bg-red-200 transition-colors text-sm font-medium"
              >
                Delete Book
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Edit Book Modal -->
    <Modal :is-open="isModalOpen" title="Edit Book" @close="closeModal">
      <BookForm :book="book" @saved="handleBookSaved" @cancel="closeModal" />
    </Modal>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { api, type Book } from '../services/api';
import LoadingSpinner from '../components/LoadingSpinner.vue';
import Modal from '../components/Modal.vue';
import BookForm from '../components/BookForm.vue';

const route = useRoute();
const router = useRouter();
const book = ref<Book | null>(null);
const isLoading = ref(true);
const error = ref<string | null>(null);
const isModalOpen = ref(false);

const loadBook = async () => {
  try {
    isLoading.value = true;
    error.value = null;
    const bookId = parseInt(route.params.id as string);
    book.value = await api.getBook(bookId);
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to load book';
  } finally {
    isLoading.value = false;
  }
};

const openEditModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};

const handleBookSaved = async () => {
  closeModal();
  await loadBook();
};

const handleDelete = async () => {
  if (!book.value) return;
  
  if (!confirm('Are you sure you want to delete this book?')) {
    return;
  }

  try {
    await api.deleteBook(book.value.id);
    router.push('/');
  } catch (err) {
    alert(err instanceof Error ? err.message : 'Failed to delete book');
  }
};

onMounted(async () => {
  await loadBook();
});
</script>

