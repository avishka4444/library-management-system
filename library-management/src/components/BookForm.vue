<template>
  <form @submit.prevent="handleSubmit" class="space-y-4">
    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Title *</label>
      <input
        v-model="formData.title"
        type="text"
        required
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">ISBN *</label>
      <input
        v-model="formData.isbn"
        type="text"
        required
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Author</label>
      <select
        v-model="formData.authorId"
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      >
        <option :value="null">No Author</option>
        <option v-for="author in authors" :key="author.id" :value="author.id">
          {{ author.fullName }}
        </option>
      </select>
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Published Date</label>
      <input
        v-model="formData.publishedDate"
        type="date"
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Total Copies *</label>
      <input
        v-model.number="formData.totalCopies"
        type="number"
        min="1"
        required
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
        {{ isSubmitting ? 'Saving...' : (book ? 'Update' : 'Create') }}
      </button>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { api, type Book, type CreateBookDto, type UpdateBookDto, type Author } from '../services/api';

interface Props {
  book?: Book | null;
}

const props = defineProps<Props>();
const emit = defineEmits<{
  saved: [];
  cancel: [];
}>();

const authors = ref<Author[]>([]);
const isSubmitting = ref(false);
const error = ref<string | null>(null);

const formData = ref<CreateBookDto | UpdateBookDto>({
  title: props.book?.title || '',
  isbn: props.book?.isbn || '',
  authorId: props.book?.authorId || null,
  publishedDate: props.book?.publishedDate ? props.book.publishedDate.split('T')[0] : null,
  totalCopies: props.book?.totalCopies || 1,
});

onMounted(async () => {
  try {
    authors.value = await api.getAuthors();
  } catch (err) {
    // Authors will remain empty if load fails
    // Error is handled silently as form can work without authors list
  }
});

const handleSubmit = async () => {
  try {
    isSubmitting.value = true;
    error.value = null;

    const submitData = {
      ...formData.value,
      publishedDate: formData.value.publishedDate || null,
    };

    if (props.book) {
      await api.updateBook(props.book.id, submitData);
    } else {
      await api.createBook(submitData as CreateBookDto);
    }

    emit('saved');
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to save book';
  } finally {
    isSubmitting.value = false;
  }
};
</script>

