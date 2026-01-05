import { API_CONFIG } from '../utils/constants';
import { handleApiError } from '../utils/errorHandler';

const API_BASE_URL = API_CONFIG.BASE_URL;

export interface Book {
  id: number;
  title: string;
  isbn: string;
  authorId: number | null;
  authorName: string | null;
  publishedDate: string | null;
  totalCopies: number;
  availableCopies: number;
  createdAt: string;
}

export interface CreateBookDto {
  title: string;
  isbn: string;
  authorId: number | null;
  publishedDate: string | null;
  totalCopies: number;
}

export interface UpdateBookDto {
  title?: string;
  isbn?: string;
  authorId?: number | null;
  publishedDate?: string | null;
  totalCopies?: number;
}

export interface Author {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  dateOfBirth: string | null;
  biography: string | null;
  createdAt: string;
}

export interface CreateAuthorDto {
  firstName: string;
  lastName: string;
  dateOfBirth: string | null;
  biography: string | null;
}

export interface UpdateAuthorDto {
  firstName?: string;
  lastName?: string;
  dateOfBirth?: string | null;
  biography?: string | null;
}

export interface Member {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  email: string;
  phoneNumber: string | null;
  address: string | null;
  createdAt: string;
}

export interface CreateMemberDto {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string | null;
  address: string | null;
}

export interface UpdateMemberDto {
  firstName?: string;
  lastName?: string;
  email?: string;
  phoneNumber?: string | null;
  address?: string | null;
}

export interface Borrowing {
  id: number;
  bookId: number;
  bookTitle: string;
  memberId: number;
  memberName: string;
  borrowedDate: string;
  returnedDate: string | null;
  dueDate: string;
  status: string;
  createdAt: string;
}

export interface CreateBorrowingDto {
  bookId: number;
  memberId: number;
  dueDate: string;
}

export interface ReturnBookDto {
  borrowingId: number;
}

async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    await handleApiError(response);
  }

  // Handle 204 No Content
  if (response.status === 204) {
    return undefined as T;
  }

  try {
    return await response.json();
  } catch {
    return undefined as T;
  }
}

export const api = {
  // Books
  async getBooks(): Promise<Book[]> {
    try {
      const response = await fetch(`${API_BASE_URL}/Books`);
      return handleResponse<Book[]>(response);
    } catch (error) {
      if (error instanceof TypeError && error.message.includes('fetch')) {
        throw new Error(`Cannot connect to API at ${API_BASE_URL}. Is the backend server running?`);
      }
      throw error;
    }
  },

  async getBook(id: number): Promise<Book> {
    const response = await fetch(`${API_BASE_URL}/Books/${id}`);
    return handleResponse<Book>(response);
  },

  async createBook(dto: CreateBookDto): Promise<Book> {
    const response = await fetch(`${API_BASE_URL}/Books`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return handleResponse<Book>(response);
  },

  async updateBook(id: number, dto: UpdateBookDto): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Books/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return handleResponse<void>(response);
  },

  async deleteBook(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Books/${id}`, {
      method: 'DELETE',
    });
    return handleResponse<void>(response);
  },

  // Authors
  async getAuthors(): Promise<Author[]> {
    const response = await fetch(`${API_BASE_URL}/Authors`);
    return handleResponse<Author[]>(response);
  },

  async getAuthor(id: number): Promise<Author> {
    const response = await fetch(`${API_BASE_URL}/Authors/${id}`);
    return handleResponse<Author>(response);
  },

  async createAuthor(dto: CreateAuthorDto): Promise<Author> {
    const response = await fetch(`${API_BASE_URL}/Authors`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return handleResponse<Author>(response);
  },

  async updateAuthor(id: number, dto: UpdateAuthorDto): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Authors/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return handleResponse<void>(response);
  },

  async deleteAuthor(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Authors/${id}`, {
      method: 'DELETE',
    });
    return handleResponse<void>(response);
  },

  // Members
  async getMembers(): Promise<Member[]> {
    const response = await fetch(`${API_BASE_URL}/Members`);
    return handleResponse<Member[]>(response);
  },

  async getMember(id: number): Promise<Member> {
    const response = await fetch(`${API_BASE_URL}/Members/${id}`);
    return handleResponse<Member>(response);
  },

  async createMember(dto: CreateMemberDto): Promise<Member> {
    const response = await fetch(`${API_BASE_URL}/Members`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return handleResponse<Member>(response);
  },

  async updateMember(id: number, dto: UpdateMemberDto): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Members/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return handleResponse<void>(response);
  },

  async deleteMember(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Members/${id}`, {
      method: 'DELETE',
    });
    return handleResponse<void>(response);
  },

  // Borrowings
  async getBorrowings(): Promise<Borrowing[]> {
    const response = await fetch(`${API_BASE_URL}/Borrowings`);
    return handleResponse<Borrowing[]>(response);
  },

  async getBorrowing(id: number): Promise<Borrowing> {
    const response = await fetch(`${API_BASE_URL}/Borrowings/${id}`);
    return handleResponse<Borrowing>(response);
  },

  async getBorrowingsByMember(memberId: number): Promise<Borrowing[]> {
    const response = await fetch(`${API_BASE_URL}/Borrowings/member/${memberId}`);
    return handleResponse<Borrowing[]>(response);
  },

  async createBorrowing(dto: CreateBorrowingDto): Promise<Borrowing> {
    const response = await fetch(`${API_BASE_URL}/Borrowings`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return handleResponse<Borrowing>(response);
  },

  async returnBook(dto: ReturnBookDto): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Borrowings/return`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return handleResponse<void>(response);
  },

  async deleteBorrowing(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Borrowings/${id}`, {
      method: 'DELETE',
    });
    return handleResponse<void>(response);
  },
};
