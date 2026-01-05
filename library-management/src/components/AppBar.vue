<template>
  <header class="fixed top-0 left-0 right-0 w-full z-50 bg-white shadow-md min-h-[80px] flex items-center justify-center">
    <nav class="container max-w-6xl w-full px-4 md:px-6 relative">
      <div class="flex items-center justify-between h-full">
        <!-- Logo/Brand -->
        <div class="flex-shrink-0 max-w-[200px] md:max-w-[220px]">
          <router-link to="/" class="flex items-center gap-2 group">
            <svg
              class="w-8 h-8 md:w-10 md:h-10 text-gray-900 group-hover:text-gray-700 transition-colors flex-shrink-0"
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
            <span class="text-lg md:text-xl font-bold text-gray-900 group-hover:text-gray-700 transition-colors whitespace-nowrap truncate">
              Library
            </span>
          </router-link>
        </div>

        <!-- Desktop Navigation - Centered -->
        <div class="hidden lg:flex gap-4 md:gap-6 lg:gap-8 xl:gap-12 absolute left-1/2 transform -translate-x-1/2 items-center z-10">
          <router-link
            v-for="route in navRoutes"
            :key="route.path"
            :to="route.path"
            class="text-center text-sm lg:text-base transition-colors duration-200 pb-1 whitespace-nowrap"
            :class="isActive(route.path)
              ? 'text-gray-900 font-bold border-b-2 border-gray-900'
              : 'text-gray-500 hover:text-gray-700'"
          >
            {{ route.label }}
          </router-link>
        </div>

        <!-- Mobile Menu Button -->
        <button
          @click="toggleMobileMenu"
          class="lg:hidden p-2 rounded-md text-gray-600 hover:text-gray-900 hover:bg-gray-100 transition-colors flex-shrink-0"
          aria-label="Toggle menu"
        >
          <svg
            v-if="!isMobileMenuOpen"
            class="w-6 h-6"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M4 6h16M4 12h16M4 18h16"
            />
          </svg>
          <svg
            v-else
            class="w-6 h-6"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M6 18L18 6M6 6l12 12"
            />
          </svg>
        </button>

        <!-- Spacer for desktop (to balance the centered nav) -->
        <div class="flex-shrink-0 w-[200px] md:w-[220px] hidden lg:block"></div>
      </div>

      <!-- Mobile Navigation Menu -->
      <div
        v-if="isMobileMenuOpen"
        class="lg:hidden border-t border-gray-200 py-4 mt-2"
      >
        <div class="flex flex-col gap-2">
          <router-link
            v-for="route in navRoutes"
            :key="route.path"
            :to="route.path"
            @click="closeMobileMenu"
            class="text-base font-medium transition-colors duration-200 px-4 py-3 rounded-md"
            :class="isActive(route.path)
              ? 'text-gray-900 bg-gray-100'
              : 'text-gray-600 hover:text-gray-900 hover:bg-gray-50'"
          >
            {{ route.label }}
          </router-link>
        </div>
      </div>
    </nav>
  </header>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { navRoutes } from '../router';

const route = useRoute();
const router = useRouter();
const isMobileMenuOpen = ref(false);

const isActive = (path: string) => {
  if (path === '/') {
    return route.path === '/';
  }
  return route.path.startsWith(path);
};

const toggleMobileMenu = () => {
  isMobileMenuOpen.value = !isMobileMenuOpen.value;
};

const closeMobileMenu = () => {
  isMobileMenuOpen.value = false;
};

// Close mobile menu when route changes
router.afterEach(() => {
  closeMobileMenu();
});

// Close mobile menu on window resize to desktop size
let handleResize: (() => void) | null = null;

onMounted(() => {
  handleResize = () => {
    if (window.innerWidth >= 1024) {
      closeMobileMenu();
    }
  };
  window.addEventListener('resize', handleResize);
});

onUnmounted(() => {
  if (handleResize) {
    window.removeEventListener('resize', handleResize);
  }
});
</script>
