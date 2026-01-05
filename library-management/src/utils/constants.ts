// API Configuration
export const API_CONFIG = {
  BASE_URL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5150/api',
  TIMEOUT: 30000, // 30 seconds
} as const;

// Validation Rules
export const VALIDATION_RULES = {
  TITLE: {
    MIN_LENGTH: 1,
    MAX_LENGTH: 200,
  },
  ISBN: {
    MIN_LENGTH: 1,
    MAX_LENGTH: 50,
  },
  NAME: {
    MIN_LENGTH: 1,
    MAX_LENGTH: 100,
  },
  EMAIL: {
    MAX_LENGTH: 200,
    PATTERN: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
  },
  PHONE: {
    MAX_LENGTH: 20,
  },
  ADDRESS: {
    MAX_LENGTH: 500,
  },
  BIOGRAPHY: {
    MAX_LENGTH: 5000,
  },
  TOTAL_COPIES: {
    MIN: 1,
  },
} as const;

// Error Messages
export const ERROR_MESSAGES = {
  NETWORK_ERROR: 'Network error: Could not connect to server',
  NOT_FOUND: 'Resource not found',
  VALIDATION_ERROR: 'Validation failed',
  UNAUTHORIZED: 'Unauthorized access',
  SERVER_ERROR: 'An error occurred on the server',
  UNKNOWN_ERROR: 'An unexpected error occurred',
} as const;

// Success Messages
export const SUCCESS_MESSAGES = {
  CREATED: 'Successfully created',
  UPDATED: 'Successfully updated',
  DELETED: 'Successfully deleted',
  RETURNED: 'Book returned successfully',
} as const;

