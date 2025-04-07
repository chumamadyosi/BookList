import React, { useState } from "react";
import { Container, TextField, Button, Typography, Box, Paper } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { LoginPayload } from "../Interfaces/uthorisationInterfaces/LoginPayload";
import { AuthenticateUser } from "../Services/AuthorisationService";

const Login: React.FC = () => {
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const handleLogin = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setError(null);

        try {
            const payload: LoginPayload = { username, password };
            const response = await AuthenticateUser(payload);

            localStorage.setItem("token", response.token);
            localStorage.setItem('jwtExpiresAt', response.expiresAt.toString());
            navigate("/books");
        } catch (err) {
            console.log(err);
            setError("Invalid username or password. Please try again.");
        }
    };

    return (
        <Container maxWidth="xs">
            <Paper elevation={3} sx={{ padding: 4, marginTop: 10 }}>
                <Typography variant="h5" align="center" gutterBottom>
                    Login
                </Typography>

                {error && <Typography color="error" align="center">{error}</Typography>}

                <Box component="form" onSubmit={handleLogin} sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
                    <TextField label="Username" variant="outlined" fullWidth required value={username} onChange={(e) => setUsername(e.target.value)} />
                    <TextField label="Password" type="password" variant="outlined" fullWidth required value={password} onChange={(e) => setPassword(e.target.value)} />
                    <Button type="submit" variant="contained" color="primary" fullWidth>Login</Button>
                </Box>
            </Paper>
        </Container>
    );
};

export default Login;