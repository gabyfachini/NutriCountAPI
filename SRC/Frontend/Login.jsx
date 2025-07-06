import React, { useState } from 'react';
import axios from 'axios';
import './Login.css';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [token, setToken] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    try {
      const response = await axios.post('https://localhost:7094/swagger/index.html', {
        email,
        password
      });

      const token = response.data.token || response.data.accessToken || 'Token recebido!';
      setToken(token);
    } catch (err) {
      console.error(err);
      setError('Email ou senha inválidos');
    }
  };

  return (
    <div className="login-container">
      <h2>Login - NutriCount</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="email"
          placeholder="E-mail"
          value={email}
          onChange={e => setEmail(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Senha"
          value={password}
          onChange={e => setPassword(e.target.value)}
          required
        />
        <button type="submit">Entrar</button>
      </form>
      {error && <p className="error">{error}</p>}
      {token && <p className="token">Token: {token}</p>}
    </div>
  );
}

export default Login;