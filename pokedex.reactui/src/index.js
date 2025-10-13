import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import PokemonList from './components/PokemonList';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <PokemonList />
  </React.StrictMode>
);