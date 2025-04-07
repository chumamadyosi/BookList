import React, { useEffect, useState } from 'react';
import BookService from '../Services/BookService';
import { Book } from '../Interfaces/uthorisationInterfaces/Book';
import { 
  Container, Typography, Paper, Table, TableBody, TableCell, 
  TableContainer, TableHead, TableRow, IconButton, CircularProgress, 
  Box, Button, Stack 
} from '@mui/material';
import { Edit, Delete, Add, ExitToApp } from '@mui/icons-material';
import BookForm from './BookForm';

const BookList: React.FC = () => {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>('');
  const [editingBook, setEditingBook] = useState<Book | null>(null);
  const [showForm, setShowForm] = useState<boolean>(false);

  useEffect(() => {
    fetchBooks();
  }, []);

  const fetchBooks = async () => {
    setLoading(true);
    try {
      const books = await BookService.getBooks();
      setBooks(books);
    } catch (error) {
      setError('Failed to load books');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleEdit = (book: Book) => {
    setEditingBook(book);
    setShowForm(true);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this book?')) {
      try {
        await BookService.deleteBook(id);
        setBooks(books.filter((book) => book.id !== id));
      } catch (error) {
        console.error('Failed to delete book:', error);
      }
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('jwtExpiresAt');
    window.location.href = '/login';
  };

  const handleCreateNewBook = () => {
    setEditingBook(null);
    setShowForm(true);
  };

  const handleCancel = () => {
    setShowForm(false);
  };

  const handleSave = () => {
    setShowForm(false);
    fetchBooks();
  };

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="100vh">
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Container>
        <Typography variant="h6" color="error" align="center">
          {error}
        </Typography>
      </Container>
    );
  }

  return (
    <Container>
      {showForm ? (
        <BookForm editingBook={editingBook} onCancel={handleCancel} onSave={handleSave} />
      ) : (
        <>
          <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ mb: 2 }}>
            <Typography variant="h4">Book List</Typography>
            <Stack direction="row" spacing={2}>
              <Button variant="contained" color="primary" startIcon={<Add />} onClick={handleCreateNewBook}>
                Create New Book
              </Button>
              <Button variant="contained" color="secondary" startIcon={<ExitToApp />} onClick={handleLogout}>
                Logout
              </Button>
            </Stack>
          </Stack>
          
          <Paper elevation={3} sx={{ padding: 2 }}>
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell><strong>Title</strong></TableCell>
                    <TableCell><strong>Author</strong></TableCell>
                    <TableCell><strong>ISBN</strong></TableCell>
                    <TableCell><strong>Year</strong></TableCell>
                    <TableCell align="center"><strong>Actions</strong></TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {books.map((book) => (
                    <TableRow key={book.id}>
                      <TableCell>{book.title}</TableCell>
                      <TableCell>{book.authorName}</TableCell>
                      <TableCell>{book.isbn}</TableCell>
                      <TableCell>{book.publishedYear}</TableCell>
                      <TableCell align="center">
                        <IconButton color="primary" onClick={() => handleEdit(book)}>
                          <Edit />
                        </IconButton>
                        <IconButton color="error" onClick={() => handleDelete(book.id)}>
                          <Delete />
                        </IconButton>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Paper>
        </>
      )}
    </Container>
  );
};

export default BookList;
