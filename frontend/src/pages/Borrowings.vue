<template>
  <div>
    <div class="flex justify-between items-center mb-6 flex-col md:flex-row gap-4">
      <h1 class="text-2xl md:text-3xl font-bold text-gray-800">Borrowings</h1>
      <button
        @click="openCreateModal"
        class="px-4 py-2 bg-gray-900 text-white rounded-md hover:bg-gray-800 transition-colors text-sm font-medium"
      >
        + New Borrowing
      </button>
    </div>

    <div v-if="isLoading" class="flex items-center justify-center min-h-[400px]">
      <LoadingSpinner />
    </div>

    <div v-else-if="error" class="flex items-center justify-center min-h-[400px]">
      <p class="text-red-500" role="alert">{{ error }}</p>
    </div>

    <div v-else-if="borrowings.length === 0" class="flex items-center justify-center min-h-[400px]">
      <p class="text-gray-500">No borrowings found.</p>
    </div>

    <div v-else class="border border-gray-200 rounded-lg overflow-hidden overflow-x-auto">
      <table class="w-full border-collapse min-w-[600px]">
        <thead class="bg-gray-100">
          <tr>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base">
              Book
            </th>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base">
              Member
            </th>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base hidden md:table-cell">
              Borrowed Date
            </th>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base hidden md:table-cell">
              Due Date
            </th>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base">
              Status
            </th>
            <th class="px-2 md:px-4 py-2 md:py-3 text-left font-semibold text-gray-700 border-b border-gray-200 text-sm md:text-base">
              Actions
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="borrowing in borrowings"
            :key="borrowing.id"
            class="hover:bg-gray-50 border-b border-gray-100"
          >
            <td class="px-2 md:px-4 py-2 md:py-3 text-gray-700 font-medium text-sm md:text-base">
              {{ borrowing.bookTitle }}
            </td>
            <td class="px-2 md:px-4 py-2 md:py-3 text-gray-600 text-sm md:text-base">
              {{ borrowing.memberName }}
            </td>
            <td class="px-2 md:px-4 py-2 md:py-3 text-gray-600 text-sm md:text-base hidden md:table-cell">
              {{ new Date(borrowing.borrowedDate).toLocaleDateString() }}
            </td>
            <td class="px-2 md:px-4 py-2 md:py-3 text-gray-600 text-sm md:text-base hidden md:table-cell">
              {{ new Date(borrowing.dueDate).toLocaleDateString() }}
            </td>
            <td class="px-2 md:px-4 py-2 md:py-3 text-sm md:text-base">
              <span
                class="px-2 py-1 rounded-full text-xs font-medium"
                :class="borrowing.status === 'Returned'
                  ? 'bg-green-100 text-green-800'
                  : 'bg-yellow-100 text-yellow-800'"
              >
                {{ borrowing.status }}
              </span>
            </td>
            <td class="px-2 md:px-4 py-2 md:py-3 text-sm md:text-base">
              <div class="flex gap-2">
                <button
                  v-if="borrowing.status !== 'Returned'"
                  @click="handleReturn(borrowing.id)"
                  class="px-3 py-1 text-xs bg-green-100 text-green-700 rounded hover:bg-green-200 transition-colors"
                >
                  Return
                </button>
                <button
                  @click="handleDelete(borrowing.id)"
                  class="px-3 py-1 text-xs bg-red-100 text-red-700 rounded hover:bg-red-200 transition-colors"
                >
                  Delete
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Create Borrowing Modal -->
    <Modal :is-open="isModalOpen" title="Create Borrowing" @close="closeModal">
      <BorrowingForm @saved="handleBorrowingSaved" @cancel="closeModal" />
    </Modal>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { api, type Borrowing } from '../services/api';
import LoadingSpinner from '../components/LoadingSpinner.vue';
import Modal from '../components/Modal.vue';
import BorrowingForm from '../components/BorrowingForm.vue';

const borrowings = ref<Borrowing[]>([]);
const isLoading = ref(true);
const error = ref<string | null>(null);
const isModalOpen = ref(false);

const loadBorrowings = async () => {
  try {
    isLoading.value = true;
    error.value = null;
    borrowings.value = await api.getBorrowings();
  } catch (err) {
    error.value = err instanceof Error ? err.message : 'Failed to load borrowings';
  } finally {
    isLoading.value = false;
  }
};

const openCreateModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};

const handleBorrowingSaved = async () => {
  closeModal();
  await loadBorrowings();
};

const handleReturn = async (id: number) => {
  if (!confirm('Mark this book as returned?')) {
    return;
  }

  try {
    await api.returnBook({ borrowingId: id });
    await loadBorrowings();
  } catch (err) {
    alert(err instanceof Error ? err.message : 'Failed to return book');
  }
};

const handleDelete = async (id: number) => {
  if (!confirm('Are you sure you want to delete this borrowing record?')) {
    return;
  }

  try {
    await api.deleteBorrowing(id);
    await loadBorrowings();
  } catch (err) {
    alert(err instanceof Error ? err.message : 'Failed to delete borrowing');
  }
};

onMounted(async () => {
  await loadBorrowings();
});
</script>
