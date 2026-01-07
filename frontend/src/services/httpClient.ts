import { API_CONFIG } from '../utils/constants';
import { handleApiError, ApiException } from '../utils/errorHandler';

class HttpClient {
  private baseURL: string;
  private defaultHeaders: HeadersInit;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
    this.defaultHeaders = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    };
  }

  private async retryRequest(
    requestFn: () => Promise<Response>,
    retries = 2,
    delay = 1000
  ): Promise<Response> {
    try {
      return await requestFn();
    } catch (error) {
      if (retries > 0 && this.isRetryableError(error)) {
        await this.delay(delay);
        return this.retryRequest(requestFn, retries - 1, delay * 2);
      }
      throw error;
    }
  }

  private isRetryableError(error: unknown): boolean {
    if (error instanceof TypeError && error.message.includes('fetch')) {
      return true; // Network error
    }
    if (error instanceof ApiException) {
      return error.statusCode !== undefined && error.statusCode >= 500;
    }
    return false;
  }

  private delay(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  private async handleResponse<T>(response: Response): Promise<T> {
    if (!response.ok) {
      await handleApiError(response);
    }

    if (response.status === 204) {
      return undefined as T;
    }

    const contentType = response.headers.get('content-type');
    if (!contentType?.includes('application/json')) {
      return undefined as T;
    }

    try {
      const text = await response.text();
      if (!text) {
        return undefined as T;
      }
      return JSON.parse(text) as T;
    } catch (error) {
      throw new ApiException('Invalid JSON response', response.status);
    }
  }

  private buildUrl(endpoint: string, params?: Record<string, string | number>): string {
    const cleanEndpoint = endpoint.startsWith('/') ? endpoint.slice(1) : endpoint;
    const baseUrl = this.baseURL.endsWith('/') ? this.baseURL.slice(0, -1) : this.baseURL;
    const url = new URL(cleanEndpoint, `${baseUrl}/`);
    
    if (params) {
      Object.entries(params).forEach(([key, value]) => {
        if (value !== undefined && value !== null) {
          url.searchParams.append(key, String(value));
        }
      });
    }
    
    return url.toString();
  }

  async get<T>(
    endpoint: string,
    params?: Record<string, string | number>,
    options?: RequestInit
  ): Promise<T> {
    const url = this.buildUrl(endpoint, params);
    
    const requestFn = () => fetch(url, {
      method: 'GET',
      headers: { ...this.defaultHeaders, ...options?.headers },
      ...options,
    });

    const response = await this.retryRequest(requestFn);
    return this.handleResponse<T>(response);
  }

  async post<T>(
    endpoint: string,
    data?: unknown,
    options?: RequestInit
  ): Promise<T> {
    const url = this.buildUrl(endpoint);
    
    const requestFn = () => fetch(url, {
      method: 'POST',
      headers: { ...this.defaultHeaders, ...options?.headers },
      body: data ? JSON.stringify(data) : undefined,
      ...options,
    });

    const response = await this.retryRequest(requestFn);
    return this.handleResponse<T>(response);
  }

  async put<T>(
    endpoint: string,
    data?: unknown,
    options?: RequestInit
  ): Promise<T> {
    const url = this.buildUrl(endpoint);
    
    const requestFn = () => fetch(url, {
      method: 'PUT',
      headers: { ...this.defaultHeaders, ...options?.headers },
      body: data ? JSON.stringify(data) : undefined,
      ...options,
    });

    const response = await this.retryRequest(requestFn);
    return this.handleResponse<T>(response);
  }

  async delete<T>(
    endpoint: string,
    options?: RequestInit
  ): Promise<T> {
    const url = this.buildUrl(endpoint);
    
    const requestFn = () => fetch(url, {
      method: 'DELETE',
      headers: { ...this.defaultHeaders, ...options?.headers },
      ...options,
    });

    const response = await this.retryRequest(requestFn);
    return this.handleResponse<T>(response);
  }
}

export const httpClient = new HttpClient(API_CONFIG.BASE_URL);

