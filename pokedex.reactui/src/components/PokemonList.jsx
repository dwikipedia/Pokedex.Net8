import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { pokemonsUrl } from '../config/apiConfig';

const PokemonList = () => {
  const [pokemons, setPokemons] = useState([]);

  useEffect(() => {
    axios.get(pokemonsUrl)
      .then(res => setPokemons(res.data))
      .catch(err => console.error('API error:', err));
  }, []);

  return (
    <div>
      <h2>Pokémon List</h2>
      <ul>
        {pokemons.map(p => (
          <li key={p.id}>{p.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default PokemonList;