<template>
  <div
    class="cursor-pointer transition-transform duration-300"
    :class="{ 'scale-105': isHovered }"
    @mouseenter="isHovered = true"
    @mouseleave="isHovered = false"
    @click="handleBookClick"
  >
      <div
        class="bg-gray-100 rounded-2xl overflow-hidden mb-2 relative w-full book-card-container"
        :class="{ 'shadow-xl': isHovered, 'shadow-md': !isHovered }"
      >
      <div
        class="absolute top-0 left-0 w-full h-full bg-gray-200 rounded-2xl flex items-center justify-center"
      >
        <svg
          class="w-16 h-16 text-gray-400"
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
      <button
        v-if="isHovered"
        @click.stop="handleViewClick"
        class="absolute bottom-3 right-3 opacity-100 transition-opacity duration-300 cursor-pointer z-10 bg-gray-400 text-white rounded-full w-12 h-12 flex items-center justify-center shadow-lg hover:bg-gray-500 hover:scale-110 transition-all duration-200"
      >
        <svg
          class="w-6 h-6 text-black"
          fill="currentColor"
          viewBox="0 0 20 20"
        >
          <path
            d="M10 12a2 2 0 100-4 2 2 0 000 4z"
          />
          <path
            fill-rule="evenodd"
            d="M.458 10C1.732 5.943 5.522 3 10 3s8.268 2.943 9.542 7c-1.274 4.057-5.064 7-9.542 7S1.732 14.057.458 10zM14 10a4 4 0 11-8 0 4 4 0 018 0z"
            clip-rule="evenodd"
          />
        </svg>
      </button>
    </div>
    <div>
      <p
        class="font-semibold text-sm text-gray-800 mb-1 line-clamp-2"
      >
        {{ book.title }}
      </p>
      <p v-if="book.authorName" class="text-xs text-gray-500">
        {{ book.authorName }}
      </p>
      <p v-else class="text-xs text-gray-500">
        No Author
      </p>
      <p v-if="book.publishedDate" class="text-xs text-gray-500 mt-1">
        {{ new Date(book.publishedDate).getFullYear() }}
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import type { Book } from '../services/api';

interface Props {
  book: Book;
}

const props = defineProps<Props>();
const router = useRouter();
const isHovered = ref(false);

const handleBookClick = () => {
  router.push(`/book/${props.book.id}`);
};

const handleViewClick = () => {
  router.push(`/book/${props.book.id}`);
};
</script>

