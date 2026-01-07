<template>
  <div class="mb-8 md:mb-12 px-4 max-w-[1200px] mx-auto">
    <!-- Header Section with Statistics -->
    <div class="mb-8 md:mb-12">
      <div class="grid grid-cols-2 md:grid-cols-4 gap-4 md:gap-6">
        <!-- Books Card -->
        <router-link
          to="/"
          class="bg-white rounded-xl shadow-md hover:shadow-lg transition-shadow duration-200 p-4 md:p-6 border border-gray-200 hover:border-gray-300 group"
        >
          <div class="flex items-center justify-between mb-3">
            <div class="p-2 bg-blue-100 rounded-lg group-hover:bg-blue-200 transition-colors">
              <svg class="w-6 h-6 md:w-8 md:h-8 text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
              </svg>
            </div>
            <svg class="w-5 h-5 text-gray-400 group-hover:text-gray-600 transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
            </svg>
          </div>
          <div>
            <p class="text-2xl md:text-3xl font-bold text-gray-900 mb-1">{{ stats.booksCount }}</p>
            <p class="text-sm md:text-base text-gray-600">Books</p>
          </div>
        </router-link>

        <!-- Authors Card -->
        <router-link
          to="/authors"
          class="bg-white rounded-xl shadow-md hover:shadow-lg transition-shadow duration-200 p-4 md:p-6 border border-gray-200 hover:border-gray-300 group"
        >
          <div class="flex items-center justify-between mb-3">
            <div class="p-2 bg-green-100 rounded-lg group-hover:bg-green-200 transition-colors">
              <svg class="w-6 h-6 md:w-8 md:h-8 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
              </svg>
            </div>
            <svg class="w-5 h-5 text-gray-400 group-hover:text-gray-600 transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
            </svg>
          </div>
          <div>
            <p class="text-2xl md:text-3xl font-bold text-gray-900 mb-1">{{ stats.authorsCount }}</p>
            <p class="text-sm md:text-base text-gray-600">Authors</p>
          </div>
        </router-link>

        <!-- Members Card -->
        <router-link
          to="/members"
          class="bg-white rounded-xl shadow-md hover:shadow-lg transition-shadow duration-200 p-4 md:p-6 border border-gray-200 hover:border-gray-300 group"
        >
          <div class="flex items-center justify-between mb-3">
            <div class="p-2 bg-purple-100 rounded-lg group-hover:bg-purple-200 transition-colors">
              <svg class="w-6 h-6 md:w-8 md:h-8 text-purple-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0m6 0a3 3 0 11-6 0 3 3 0 016 0z" />
              </svg>
            </div>
            <svg class="w-5 h-5 text-gray-400 group-hover:text-gray-600 transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
            </svg>
          </div>
          <div>
            <p class="text-2xl md:text-3xl font-bold text-gray-900 mb-1">{{ stats.membersCount }}</p>
            <p class="text-sm md:text-base text-gray-600">Members</p>
          </div>
        </router-link>

        <!-- Borrowings Card -->
        <router-link
          to="/borrowings"
          class="bg-white rounded-xl shadow-md hover:shadow-lg transition-shadow duration-200 p-4 md:p-6 border border-gray-200 hover:border-gray-300 group"
        >
          <div class="flex items-center justify-between mb-3">
            <div class="p-2 bg-orange-100 rounded-lg group-hover:bg-orange-200 transition-colors">
              <svg class="w-6 h-6 md:w-8 md:h-8 text-orange-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
              </svg>
            </div>
            <svg class="w-5 h-5 text-gray-400 group-hover:text-gray-600 transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
            </svg>
          </div>
          <div>
            <p class="text-2xl md:text-3xl font-bold text-gray-900 mb-1">{{ stats.borrowingsCount }}</p>
            <p class="text-sm md:text-base text-gray-600">Borrowings</p>
          </div>
        </router-link>
      </div>
    </div>

    <!-- Search bar -->
    <div class="mb-6 max-w-[600px] mx-auto">
      <form @submit.prevent="handleSearch" class="relative">
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
    </div>

    <div class="flex justify-between items-center mb-6 flex-col md:flex-row gap-4">
      <h2 class="text-2xl md:text-3xl font-bold text-gray-800">
        Books
      </h2>
      <div class="flex items-center gap-3">
        <button
          @click="openCreateModal"
          class="px-4 py-2 bg-gray-900 text-white rounded-md hover:bg-gray-800 transition-colors text-sm font-medium"
        >
          + Add Book
        </button>
        <div v-if="sortedBooks.length > 0" class="flex items-center gap-2">
          <span class="text-sm text-gray-600 whitespace-nowrap">Sort by:</span>
          <select
            v-model="sortBy"
            @change="handleSortChange"
            class="px-3 py-2 rounded-md border-none bg-white text-sm w-[150px] cursor-pointer appearance-none pr-8 dropdown-select"
          >
            <option value="default">Default</option>
            <option value="title">Title</option>
            <option value="author">Author</option>
            <option value="year">Year</option>
          </select>
        </div>
      </div>
    </div>

    <div v-if="isLoading" class="flex items-center justify-center min-h-[400px]">
      <LoadingSpinner />
    </div>

    <div v-else-if="error" class="flex items-center justify-center min-h-[400px]">
      <p class="text-red-500" role="alert">{{ error }}</p>
    </div>

    <div v-else-if="sortedBooks.length === 0" class="flex items-center justify-center min-h-[400px]">
      <p class="text-gray-500">No books found.</p>
    </div>

    <div
      v-else
      class="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 md:gap-6"
    >
      <BookCard
        v-for="book in paginatedBooks"
        :key="book.id"
        :book="book"
      />
    </div>

    <div
      v-if="sortedBooks.length > itemsPerPage"
      class="mt-16 mb-12 flex justify-center items-center"
    >
      <div class="flex gap-2">
        <button
          @click="handlePageChange(currentPage - 1)"
          :disabled="currentPage === 1"
          class="w-10 h-10 flex items-center justify-center bg-gray-100 text-black rounded-md hover:bg-gray-200 disabled:bg-gray-300 disabled:cursor-not-allowed transition-colors"
        >
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7" />
          </svg>
        </button>

        <button
          v-for="page in visiblePages"
          :key="page"
          @click="handlePageChange(page)"
          class="min-w-[40px] px-3 py-2 rounded-md text-sm font-medium transition-colors"
          :class="currentPage === page
            ? 'bg-gray-300 text-black border border-gray-300'
            : 'bg-white text-black border border-gray-200 hover:bg-gray-50 hover:border-gray-300'"
        >
          {{ page === '...' ? '...' : page }}
        </button>

        <button
          @click="handlePageChange(currentPage + 1)"
          :disabled="currentPage === totalPages"
          class="w-10 h-10 flex items-center justify-center bg-gray-100 text-black rounded-md hover:bg-gray-200 disabled:bg-gray-300 disabled:cursor-not-allowed transition-colors"
        >
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
        </button>
      </div>
    </div>

    <!-- Create/Edit Book Modal -->
    <Modal :is-open="isModalOpen" :title="selectedBook ? 'Edit Book' : 'Create Book'" @close="closeModal">
      <BookForm :book="selectedBook" @saved="handleBookSaved" @cancel="closeModal" />
    </Modal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { api, type Book, type Author, type Member, type Borrowing } from '../services/api';
import BookCard from '../components/BookCard.vue';
import LoadingSpinner from '../components/LoadingSpinner.vue';
import Modal from '../components/Modal.vue';
import BookForm from '../components/BookForm.vue';

const router = useRouter();
const books = ref<Book[]>([]);
const authors = ref<Author[]>([]);
const members = ref<Member[]>([]);
const borrowings = ref<Borrowing[]>([]);
const isLoading = ref(true);
const error = ref<string | null>(null);
const sortBy = ref<'default' | 'title' | 'author' | 'year'>('default');
const currentPage = ref(1);
const searchQuery = ref('');
const itemsPerPage = 20;
const isModalOpen = ref(false);
const selectedBook = ref<Book | null>(null);

const stats = computed(() => ({
  booksCount: books.value.length,
  authorsCount: authors.value.length,
  membersCount: members.value.length,
  borrowingsCount: borrowings.value.length,
}));

const sortedBooks = computed(() => {
  if (sortBy.value === 'default') {
    return books.value;
  }

  const sorted = [...books.value];

  if (sortBy.value === 'title') {
    return sorted.sort((a, b) => a.title.localeCompare(b.title));
  }

  if (sortBy.value === 'author') {
    return sorted.sort((a, b) => {
      const authorA = a.authorName || '';
      const authorB = b.authorName || '';
      return authorA.localeCompare(authorB);
    });
  }

  if (sortBy.value === 'year') {
    return sorted.sort((a, b) => {
      const yearA = a.publishedDate ? new Date(a.publishedDate).getFullYear() : 0;
      const yearB = b.publishedDate ? new Date(b.publishedDate).getFullYear() : 0;
      if (yearB === yearA) {
        return a.title.localeCompare(b.title);
      }
      return yearB - yearA;
    });
  }

  return sorted;
});

const totalPages = computed(() => Math.ceil(sortedBooks.value.length / itemsPerPage));

const paginatedBooks = computed(() => {
  const startIndex = (currentPage.value - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  return sortedBooks.value.slice(startIndex, endIndex);
});

const visiblePages = computed(() => {
  const pages: (number | string)[] = [];
  const total = totalPages.value;

  if (total <= 7) {
    for (let i = 1; i <= total; i++) {
      pages.push(i);
    }
  } else {
    pages.push(1);

    if (currentPage.value > 3) {
      pages.push('...');
    }

    const start = Math.max(2, currentPage.value - 1);
    const end = Math.min(total - 1, currentPage.value + 1);

    for (let i = start; i <= end; i++) {
      pages.push(i);
    }

    if (currentPage.value < total - 2) {
      pages.push('...');
    }

    pages.push(total);
  }

  return pages;
});

const handleSortChange = () => {
  currentPage.value = 1;
};

const handlePageChange = (page: number | string) => {
  if (typeof page === 'number') {
    currentPage.value = page;
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
};

const handleSearch = () => {
  if (searchQuery.value.trim()) {
    router.push(`/search?q=${encodeURIComponent(searchQuery.value.trim())}`);
    searchQuery.value = '';
  }
};

const openCreateModal = () => {
  selectedBook.value = null;
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
  selectedBook.value = null;
};

const handleBookSaved = async () => {
  closeModal();
  await loadData();
};

const loadData = async () => {
  try {
    isLoading.value = true;
    error.value = null;
    
    // Load all data in parallel for statistics
    const [booksData, authorsData, membersData, borrowingsData] = await Promise.all([
      api.getBooks().catch(() => []),
      api.getAuthors().catch(() => []),
      api.getMembers().catch(() => []),
      api.getBorrowings().catch(() => []),
    ]);
    
    books.value = booksData;
    authors.value = authorsData;
    members.value = membersData;
    borrowings.value = borrowingsData;
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to load data';
  } finally {
    isLoading.value = false;
  }
};

onMounted(async () => {
  await loadData();
});
</script>
