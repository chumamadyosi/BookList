import React from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import Login from "./Pages/Login";
import BookList from "./Pages/Books";
import TokenCheck from "./Components/TokenCheck";
import { Container, Typography, Box, Button } from "@mui/material";

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/login" element={<Login />} />
        <Route
          path="/books"
          element={
            <TokenCheck>
              <BookList />
            </TokenCheck>
          }
        />
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  );
};

const LandingPage: React.FC = () => {
  return (
    <Container maxWidth="md" sx={{ textAlign: "center", marginTop: 10 }}>
      <Typography variant="h3" gutterBottom>Welcome to the Book Library</Typography>
      <Typography variant="h6" color="textSecondary">
        Manage your books easily with our simple CRUD system.
      </Typography>
      <Box mt={4}>
        <Button variant="contained" color="primary" href="/login">Go to Login</Button>
      </Box>
    </Container>
  );
};

export default App;
