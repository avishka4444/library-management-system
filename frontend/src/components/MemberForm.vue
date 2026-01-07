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
      <label class="block text-sm font-medium text-gray-700 mb-1">Email *</label>
      <input
        v-model="formData.email"
        type="email"
        required
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Phone Number</label>
      <input
        v-model="formData.phoneNumber"
        type="tel"
        class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-500"
      />
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Address</label>
      <textarea
        v-model="formData.address"
        rows="3"
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
        {{ isSubmitting ? 'Saving...' : (member ? 'Update' : 'Create') }}
      </button>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { api, type Member, type CreateMemberDto, type UpdateMemberDto } from '../services/api';

interface Props {
  member?: Member | null;
}

const props = defineProps<Props>();
const emit = defineEmits<{
  saved: [];
  cancel: [];
}>();

const isSubmitting = ref(false);
const error = ref<string | null>(null);

const formData = ref<CreateMemberDto | UpdateMemberDto>({
  firstName: props.member?.firstName || '',
  lastName: props.member?.lastName || '',
  email: props.member?.email || '',
  phoneNumber: props.member?.phoneNumber || null,
  address: props.member?.address || null,
});

const handleSubmit = async () => {
  try {
    isSubmitting.value = true;
    error.value = null;

    if (props.member) {
      await api.updateMember(props.member.id, formData.value);
    } else {
      await api.createMember(formData.value as CreateMemberDto);
    }

    emit('saved');
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to save member';
  } finally {
    isSubmitting.value = false;
  }
};
</script>

