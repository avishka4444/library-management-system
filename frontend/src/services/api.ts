import { httpClient } from './httpClient';

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

export const api = {
  // Books
  async getBooks(): Promise<Book[]> {
    return httpClient.get<Book[]>('/Books');
  },

  async getBook(id: number): Promise<Book> {
    return httpClient.get<Book>(`/Books/${id}`);
  },

  async createBook(dto: CreateBookDto): Promise<Book> {
    return httpClient.post<Book>('/Books', dto);
  },

  async updateBook(id: number, dto: UpdateBookDto): Promise<void> {
    return httpClient.put<void>(`/Books/${id}`, dto);
  },

  async deleteBook(id: number): Promise<void> {
    return httpClient.delete<void>(`/Books/${id}`);
  },

  // Authors
  async getAuthors(): Promise<Author[]> {
    return httpClient.get<Author[]>('/Authors');
  },

  async getAuthor(id: number): Promise<Author> {
    return httpClient.get<Author>(`/Authors/${id}`);
  },

  async createAuthor(dto: CreateAuthorDto): Promise<Author> {
    return httpClient.post<Author>('/Authors', dto);
  },

  async updateAuthor(id: number, dto: UpdateAuthorDto): Promise<void> {
    return httpClient.put<void>(`/Authors/${id}`, dto);
  },

  async deleteAuthor(id: number): Promise<void> {
    return httpClient.delete<void>(`/Authors/${id}`);
  },

  // Members
  async getMembers(): Promise<Member[]> {
    return httpClient.get<Member[]>('/Members');
  },

  async getMember(id: number): Promise<Member> {
    return httpClient.get<Member>(`/Members/${id}`);
  },

  async createMember(dto: CreateMemberDto): Promise<Member> {
    return httpClient.post<Member>('/Members', dto);
  },

  async updateMember(id: number, dto: UpdateMemberDto): Promise<void> {
    return httpClient.put<void>(`/Members/${id}`, dto);
  },

  async deleteMember(id: number): Promise<void> {
    return httpClient.delete<void>(`/Members/${id}`);
  },

  // Borrowings
  async getBorrowings(): Promise<Borrowing[]> {
    return httpClient.get<Borrowing[]>('/Borrowings');
  },

  async getBorrowing(id: number): Promise<Borrowing> {
    return httpClient.get<Borrowing>(`/Borrowings/${id}`);
  },

  async getBorrowingsByMember(memberId: number): Promise<Borrowing[]> {
    return httpClient.get<Borrowing[]>(`/Borrowings/member/${memberId}`);
  },

  async createBorrowing(dto: CreateBorrowingDto): Promise<Borrowing> {
    return httpClient.post<Borrowing>('/Borrowings', dto);
  },

  async returnBook(dto: ReturnBookDto): Promise<void> {
    return httpClient.post<void>('/Borrowings/return', dto);
  },

  async deleteBorrowing(id: number): Promise<void> {
    return httpClient.delete<void>(`/Borrowings/${id}`);
  },
};
