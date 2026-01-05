<template>
  <div class="min-h-screen flex flex-col">
    <AppBar />
    <main class="flex-1">
      <div class="container max-w-6xl py-4 md:py-6 px-4 md:px-6 pt-24 md:pt-28">
        <router-view v-slot="{ Component }">
          <Suspense>
            <component :is="Component" />
            <template #fallback>
              <LoadingSpinner />
            </template>
          </Suspense>
        </router-view>
      </div>
    </main>
    <Footer />
  </div>
</template>

<script setup lang="ts">
import AppBar from '../components/AppBar.vue';
import Footer from '../components/Footer.vue';
import LoadingSpinner from '../components/LoadingSpinner.vue';
</script>

