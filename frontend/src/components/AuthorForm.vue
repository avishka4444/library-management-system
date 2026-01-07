<template>
  <form @submit.prevent="handleSubmit" class="space-y-4">
    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">First Name *</label>
      <input
        v-model="formData.firstName"
        type="text"
        required
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Last Name *</label>
      <input
        v-model="formData.lastName"
        type="text"
        required
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Date of Birth</label>
      <input
        v-model="formData.dateOfBirth"
        type="date"
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Biography</label>
      <textarea
        v-model="formData.biography"
        rows="4"
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      ></textarea>
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
        {{ isSubmitting ? 'Saving...' : (author ? 'Update' : 'Create') }}
      </button>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { api, type Author, type CreateAuthorDto, type UpdateAuthorDto } from '../services/api';

interface Props {
  author?: Author | null;
}

const props = defineProps<Props>();
const emit = defineEmits<{
  saved: [];
  cancel: [];
}>();

const isSubmitting = ref(false);
const error = ref<string | null>(null);

const formData = ref<CreateAuthorDto | UpdateAuthorDto>({
  firstName: props.author?.firstName || '',
  lastName: props.author?.lastName || '',
  dateOfBirth: props.author?.dateOfBirth ? props.author.dateOfBirth.split('T')[0] : null,
  biography: props.author?.biography || null,
});

const handleSubmit = async () => {
  try {
    isSubmitting.value = true;
    error.value = null;

    const submitData = {
      ...formData.value,
      dateOfBirth: formData.value.dateOfBirth || null,
    };

    if (props.author) {
      await api.updateAuthor(props.author.id, submitData);
    } else {
      await api.createAuthor(submitData as CreateAuthorDto);
    }

    emit('saved');
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to save author';
  } finally {
    isSubmitting.value = false;
  }
};
</script>

