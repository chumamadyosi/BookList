import React, { useEffect, useState } from 'react';
import BookService from '../Services/BookService';
import { Book } from '../Interfaces/Book'; // Import the Book type
import { Author } from '../Interfaces/Author';
import { 
  Container, Typography, TextField, Button, Paper, Stack, Box, MenuItem, Select, InputLabel, FormControl, SelectChangeEvent 
} from '@mui/material';
import AuthorService from '../Services/AuthorService';

interface BookFormProps {
  editingBook: Book | null;
  onCancel: () => void;
  onSave: () => void;
}

const BookForm: React.FC<BookFormProps> = ({ editingBook, onCancel, onSave }) => {
  const [book, setBook] = useState<Book>({
    id: 0,
    title: '',
    authorName: '',
    publishedYear: new Date().getFullYear(),
    authorId: 0,
    isbn: '',
    description: '',
  });

  const [authors, setAuthors] = useState<Author[]>([]);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    fetchAuthors();

    if (editingBook) {
      setBook(editingBook);
    }
  }, [editingBook]);

  const fetchAuthors = async () => {
    try {
      const authorsData = await AuthorService.getAuthors();
      setAuthors(authorsData);
    } catch (err) {
      console.error('Error fetching authors:', err);
      setError('Failed to load authors');
    }
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement | { name?: string | undefined; value: unknown }>) => {
    const { name, value } = event.target;
    setBook((prevBook) => ({
      ...prevBook,
      [name as string]: value,
    }));
  };

  const handleSelectChange = (event: SelectChangeEvent<number>) => {
    const value = Number(event.target.value); 
    setBook((prevBook) => ({
      ...prevBook,
      authorId: value,
    }));
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (!book.title || !book.authorId || !book.publishedYear || !book.isbn || !book.description) {
      setError('All fields are required');
      return;
    }

    try {
      if (editingBook) {
        await BookService.updateBook(book.id, book);
      } else {
        await BookService.createBook(book);
      }
      onSave(); 
    } catch (err) {
      console.error('Error saving book:', err);
      setError('Failed to save book');
    }
  };

  return (
    <Container maxWidth="sm">
      <Paper elevation={3} sx={{ padding: 3 }}>
        <Typography variant="h5" gutterBottom>
          {editingBook ? 'Edit Book' : 'Add New Book'}
        </Typography>

        {error && (
          <Typography color="error" variant="body1">
            {error}
          </Typography>
        )}

        <Box component="form" onSubmit={handleSubmit}>
          <TextField fullWidth label="Title" name="title" value={book.title ?? ''}
            onChange={handleChange} margin="normal" required
          />

          <TextField
            fullWidth
            label="ISBN"
            name="isbn"
            value={book.isbn}
            onChange={handleChange}
            margin="normal"  
          />

          <TextField
            fullWidth
            label="Description"
            name="description"
            value={book.description}
            onChange={handleChange}
            margin="normal"
          />

          <TextField
            fullWidth
            label="Published Year"
            name="publishedYear"
            type="number"
            value={book.publishedYear}
            onChange={handleChange}
            margin="normal"
            required
          />

          <FormControl fullWidth margin="normal" required>
            <InputLabel>Author</InputLabel>
            <Select
              name="authorId"
              value={book.authorId}
              onChange={handleSelectChange}
              label="Author"
            >
              <MenuItem value={0}>Select an Author</MenuItem>
              {authors.map((author) => (
                <MenuItem key={author.authorId} value={author.authorId}>
                  {author.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          <Stack direction="row" spacing={2} justifyContent="flex-end" sx={{ mt: 2 }}>
            <Button variant="outlined" onClick={onCancel}>
              Cancel
            </Button>
            <Button variant="contained" color="primary" type="submit">
              {editingBook ? 'Update Book' : 'Create Book'}
            </Button>
          </Stack>
        </Box>
      </Paper>
    </Container>
  );
};

export default BookForm;
