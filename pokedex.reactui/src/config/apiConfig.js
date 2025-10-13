export const apiVersion = "2.0";
const versionSegment = `v${apiVersion.split('.')[0]}`;

export const pokemonsUrl = `https://localhost:7224/api/${versionSegment}/Pokemons`;
export const loginUrl = `https://localhost:7224/api/v1/Auth/login`;
