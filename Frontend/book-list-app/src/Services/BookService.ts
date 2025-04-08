import axios from 'axios';
import { Book } from '../Interfaces/Book';
import { BookListResponse } from '../Interfaces/BookListResponse';

const API_URL = process.env.REACT_APP_Book_List_API + 'book';

class BookService {
  static getToken(): string | null {
    const token = localStorage.getItem('token');
    return token;
  }

  static getRequestHeaders() {
    const token = this.getToken();
    return token ? { Authorization: `Bearer ${token}` } : {};
  }

  static async getBooks(page: number = 1, pageSize: number = 5): Promise<BookListResponse> {
    try {
      const response = await axios.get(API_URL+`?page=${page}&pageSize=${pageSize}`, {
        headers: this.getRequestHeaders(),
      });
      const { books, totalCount } = response.data.data; 
      const data: BookListResponse = {
        books,
        totalCount,
      };
      return data;
    } catch (error) {
      console.error('Error fetching books:', error);
      throw new Error('Failed to fetch books');
    }
  }

  static async getBookById(id: number): Promise<Book> {
    try {
      const response = await axios.get(`${API_URL}/${id}`, {
        headers: this.getRequestHeaders(),
      });
      return response.data;
    } catch (error) {
      throw new Error('Error fetching book');
    }
  }

  static async createBook(book: Book): Promise<Book> {
    try {
      const response = await axios.post(API_URL, book, {
        headers: this.getRequestHeaders(),
      });
      return response.data;
    } catch (error) {
      throw new Error('Error creating book');
    }
  }

  static async updateBook(id: number, book: Book): Promise<void> {
    try {
      await axios.put(`${API_URL}/${id}`, book, {
        headers: this.getRequestHeaders(),
      });
    } catch (error) {
      throw new Error('Error updating book');
    }
  }

  static async deleteBook(id: number): Promise<void> {
    try {
      await axios.delete(`${API_URL}/${id}`, {
        headers: this.getRequestHeaders(),
      });
    } catch (error) {
      throw new Error('Error deleting book');
    }
  }
}

export default BookService;
