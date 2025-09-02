import { pokemonsUrl, apiVersion } from '../../config/apiConfig.js';
import { isTokenExpired } from '../../services/vanilla/login.js';

document.addEventListener("DOMContentLoaded", () => {
    const token = localStorage.getItem("jwtToken");

    if (!token || isTokenExpired(token)) {
        const returnUrl = encodeURIComponent(window.location.pathname);
        window.location.replace(`/Login?ReturnUrl=${returnUrl}`);
        return;
    }

    fetchPokemons();
});

async function fetchPokemons() {
    const token = localStorage.getItem("jwtToken");

    try {
        showLoading(true);

        const response = await fetch(pokemonsUrl, {
            headers: {
                "Authorization": `Bearer ${token}`,
                "X-API-Version": apiVersion
            }
        });

        if (response.status === 401 || response.status === 403) {
            localStorage.removeItem("jwtToken"); // clear invalid token
            const returnUrl = encodeURIComponent(window.location.pathname);
            window.location.replace(`/Login?ReturnUrl=${returnUrl}`);
            return;
        }

        if (!response.ok) {
            throw new Error(`Fetch failed: ${response.status}`);
        }

        const data = await response.json();
        showData(data);

    } catch (error) {
        console.error("Fetch error:", error);
        showError("Failed to fetch Pokémon data.");
    } finally {
        showLoading(false);
    }
}

function showData(data) {
    const tableBody = document.getElementById('pokemon-table-body');
    const errorMessage = document.getElementById('error-message');

    if (!tableBody || !errorMessage) {
        console.warn("Missing DOM elements for rendering data.");
        return;
    }

    errorMessage.style.display = "none";
    tableBody.innerHTML = "";

    data.forEach(p => {
        const row = document.createElement('tr');
        row.innerHTML = `<td>${p.name}</td><td>${p.givenName}</td>`;
        tableBody.appendChild(row);
    });
}

function showError(message) {
    const errorMessage = document.getElementById('error-message');
    if (errorMessage) {
        errorMessage.textContent = message;
        errorMessage.style.display = "block";
    } else {
        alert(message);
    }
}

function showLoading(isLoading) {
    const loadingMessage = document.getElementById('loading-message');
    if (loadingMessage) {
        loadingMessage.style.display = isLoading ? "block" : "none";
    }
}