<template>
  <div>
    <h1 class="text-2xl md:text-3xl font-bold text-gray-800 mb-6">Search</h1>

    <form @submit.prevent="handleSearch" class="mb-6 relative">
      <div class="absolute left-5 top-1/2 transform -translate-y-1/2 z-10 pointer-events-none">
        <svg class="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
        </svg>
      </div>
      <input
        v-model="searchQuery"
        type="text"
        placeholder="Search for books by title or author"
        class="w-full bg-white border border-gray-300 rounded-full pl-12 pr-6 py-3 focus:outline-none focus:border-gray-500 focus:ring-1 focus:ring-gray-500 placeholder-gray-400"
      />
    </form>

    <div v-if="isLoading" class="flex items-center justify-center min-h-[400px]">
      <LoadingSpinner />
    </div>

    <div v-else-if="query.trim()">
      <div v-if="filteredBooks.length === 0" class="flex items-center justify-center min-h-[200px]">
        <p class="text-gray-500">No books found.</p>
      </div>
      <div
        v-else
        class="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 md:gap-6"
      >
        <BookCard
          v-for="book in filteredBooks"
          :key="book.id"
          :book="book"
        />
      </div>
    </div>

    <div v-else-if="error" class="flex items-center justify-center min-h-[400px]">
      <p class="text-red-500" role="alert">{{ error }}</p>
    </div>

    <div v-else class="flex items-center justify-center min-h-[400px]">
      <p class="text-gray-500">Search to find books</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { api, type Book } from '../services/api';
import BookCard from '../components/BookCard.vue';
import LoadingSpinner from '../components/LoadingSpinner.vue';

const route = useRoute();
const router = useRouter();
const books = ref<Book[]>([]);
const searchQuery = ref('');
const isLoading = ref(true);
const error = ref<string | null>(null);

const query = computed(() => {
  return (route.query.q as string) || '';
});

watch(query, (newQuery) => {
  searchQuery.value = newQuery;
});

const filteredBooks = computed(() => {
  if (!query.value.trim()) {
    return [];
  }

  const searchTerm = query.value.toLowerCase().trim();
  return books.value.filter((book) => {
    const titleMatch = book.title.toLowerCase().includes(searchTerm);
    const authorMatch = book.authorName?.toLowerCase().includes(searchTerm);
    return titleMatch || authorMatch;
  });
});

const handleSearch = () => {
  if (searchQuery.value.trim()) {
    router.push(`/search?q=${encodeURIComponent(searchQuery.value.trim())}`);
  }
};

onMounted(async () => {
  try {
    isLoading.value = true;
    books.value = await api.getBooks();
    if (query.value) {
      searchQuery.value = query.value;
    }
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to load books';
  } finally {
    isLoading.value = false;
  }
});
</script>

