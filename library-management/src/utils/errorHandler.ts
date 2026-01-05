import { ERROR_MESSAGES } from './constants';

export interface ApiError {
  message: string;
  errors?: string[];
  statusCode?: number;
}

export class ApiException extends Error {
  statusCode?: number;
  errors?: string[];

  constructor(message: string, statusCode?: number, errors?: string[]) {
    super(message);
    this.name = 'ApiException';
    this.statusCode = statusCode;
    this.errors = errors;
  }
}

export async function handleApiError(response: Response): Promise<never> {
  let errorMessage: string = ERROR_MESSAGES.SERVER_ERROR;
  let errors: string[] | undefined;

  try {
    const contentType = response.headers.get('content-type');
    if (contentType?.includes('application/json')) {
      const errorData = await response.json();
      errorMessage = errorData.message || errorData.error?.message || errorMessage;
      errors = errorData.errors || errorData.error?.errors;
    } else {
      const errorText = await response.text();
      if (errorText) {
        errorMessage = errorText;
      }
    }
  } catch {
    // If parsing fails, use default message
    errorMessage = ERROR_MESSAGES.SERVER_ERROR;
  }

  // Map HTTP status codes to user-friendly messages
  switch (response.status) {
    case 400:
      errorMessage = errorMessage || ERROR_MESSAGES.VALIDATION_ERROR;
      break;
    case 401:
      errorMessage = ERROR_MESSAGES.UNAUTHORIZED;
      break;
    case 404:
      errorMessage = ERROR_MESSAGES.NOT_FOUND;
      break;
    case 500:
    case 502:
    case 503:
      errorMessage = ERROR_MESSAGES.SERVER_ERROR;
      break;
  }

  throw new ApiException(errorMessage, response.status, errors);
}

export function handleNetworkError(error: unknown): string {
  if (error instanceof TypeError && error.message.includes('fetch')) {
    return ERROR_MESSAGES.NETWORK_ERROR;
  }
  if (error instanceof ApiException) {
    return error.message;
  }
  if (error instanceof Error) {
    return error.message;
  }
  return ERROR_MESSAGES.UNKNOWN_ERROR;
}

