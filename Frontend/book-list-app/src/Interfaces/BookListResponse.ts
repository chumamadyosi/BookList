import { Book } from './Book';

export interface BookListResponse {
  books: Book[];
  totalCount: number;
}