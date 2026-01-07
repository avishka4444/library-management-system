import { ref, computed, type Ref } from 'vue';

export interface ValidationRule {
  required?: boolean;
  minLength?: number;
  maxLength?: number;
  pattern?: RegExp;
  custom?: (value: unknown) => string | null;
}

export interface ValidationRules {
  [key: string]: ValidationRule;
}

export function useFormValidation<T extends Record<string, unknown>>(
  formData: Ref<T>,
  rules: ValidationRules
) {
  const errors = ref<Partial<Record<keyof T, string>>>({});

  const validateField = (field: keyof T, value: unknown): string | null => {
    const rule = rules[field as string];
    if (!rule) return null;

    // Required validation
    if (rule.required && (!value || (typeof value === 'string' && value.trim() === ''))) {
      return `${String(field)} is required`;
    }

    if (!value && !rule.required) return null;

    // Min length validation
    if (rule.minLength && typeof value === 'string' && value.length < rule.minLength) {
      return `${String(field)} must be at least ${rule.minLength} characters`;
    }

    // Max length validation
    if (rule.maxLength && typeof value === 'string' && value.length > rule.maxLength) {
      return `${String(field)} must be at most ${rule.maxLength} characters`;
    }

    // Pattern validation
    if (rule.pattern && typeof value === 'string' && !rule.pattern.test(value)) {
      return `${String(field)} format is invalid`;
    }

    // Custom validation
    if (rule.custom) {
      return rule.custom(value);
    }

    return null;
  };

  const validate = (): boolean => {
    errors.value = {};
    let isValid = true;

    for (const field in rules) {
      const error = validateField(field as keyof T, formData.value[field]);
      if (error) {
        errors.value[field as keyof T] = error;
        isValid = false;
      }
    }

    return isValid;
  };

  const validateSingle = (field: keyof T): boolean => {
    const error = validateField(field, formData.value[field]);
    if (error) {
      errors.value[field] = error;
      return false;
    } else {
      delete errors.value[field];
      return true;
    }
  };

  const clearErrors = () => {
    errors.value = {};
  };

  const hasErrors = computed(() => Object.keys(errors.value).length > 0);

  return {
    errors,
    validate,
    validateSingle,
    clearErrors,
    hasErrors,
  };
}

