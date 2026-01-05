<template>
  <form @submit.prevent="handleSubmit" class="space-y-4">
    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Book *</label>
      <select
        v-model="formData.bookId"
        required
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      >
        <option value="">Select a book</option>
        <option v-for="book in availableBooks" :key="book.id" :value="book.id">
          {{ book.title }} {{ book.authorName ? `- ${book.authorName}` : '' }} ({{ book.availableCopies }} available)
        </option>
      </select>
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Member *</label>
      <select
        v-model="formData.memberId"
        required
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      >
        <option value="">Select a member</option>
        <option v-for="member in members" :key="member.id" :value="member.id">
          {{ member.fullName }} ({{ member.email }})
        </option>
      </select>
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Due Date *</label>
      <input
        v-model="formData.dueDate"
        type="date"
        required
        :min="minDate"
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div v-if="error" class="text-red-500 text-sm">{{ error }}</div>

    <div class="flex gap-3 justify-end pt-4">
      <button
        type="button"
        @click="$emit('cancel')"
        class="px-4 py-2 text-gray-700 bg-gray-200 rounded-md hover:bg-gray-300 transition-colors"
      >
        Cancel
      </button>
      <button
        type="submit"
        :disabled="isSubmitting"
        class="px-4 py-2 text-white bg-gray-900 rounded-md hover:bg-gray-800 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
      >
        {{ isSubmitting ? 'Creating...' : 'Create Borrowing' }}
      </button>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { api, type Book, type Member, type CreateBorrowingDto } from '../services/api';

const emit = defineEmits<{
  saved: [];
  cancel: [];
}>();

const books = ref<Book[]>([]);
const members = ref<Member[]>([]);
const isSubmitting = ref(false);
const error = ref<string | null>(null);

const availableBooks = computed(() => {
  return books.value.filter(book => book.availableCopies > 0);
});

const minDate = computed(() => {
  const tomorrow = new Date();
  tomorrow.setDate(tomorrow.getDate() + 1);
  return tomorrow.toISOString().split('T')[0];
});

const formData = ref<CreateBorrowingDto>({
  bookId: 0,
  memberId: 0,
  dueDate: minDate.value || new Date().toISOString(),
});

onMounted(async () => {
  try {
    [books.value, members.value] = await Promise.all([
      api.getBooks(),
      api.getMembers(),
    ]);
  } catch (err) {
    // Data will remain empty if load fails
    // Error will be shown when user tries to submit
  }
});

const handleSubmit = async () => {
  try {
    isSubmitting.value = true;
    error.value = null;

    await api.createBorrowing(formData.value);

    emit('saved');
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to create borrowing';
  } finally {
    isSubmitting.value = false;
  }
};
</script>

