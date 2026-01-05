import { ref, type Ref } from 'vue';
import { handleNetworkError } from '../utils/errorHandler';

export interface UseApiOptions<T> {
  immediate?: boolean;
  onSuccess?: (data: T) => void;
  onError?: (error: string) => void;
}

export function useApi<T>(
  apiCall: () => Promise<T>,
  options: UseApiOptions<T> = {}
) {
  const data: Ref<T | null> = ref(null);
  const isLoading = ref(false);
  const error = ref<string | null>(null);

  const execute = async () => {
    try {
      isLoading.value = true;
      error.value = null;
      const result = await apiCall();
      data.value = result;
      options.onSuccess?.(result);
      return result;
    } catch (err) {
      const errorMessage = handleNetworkError(err);
      error.value = errorMessage;
      options.onError?.(errorMessage);
      throw err;
    } finally {
      isLoading.value = false;
    }
  };

  if (options.immediate) {
    execute();
  }

  return {
    data,
    isLoading,
    error,
    execute,
    reset: () => {
      data.value = null;
      error.value = null;
      isLoading.value = false;
    },
  };
}

