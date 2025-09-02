import { loginUrl } from '../../config/apiConfig.js';

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("login-form");
    const errorDiv = document.getElementById("login-error");
    const token = localStorage.getItem("jwtToken");
    const isLoggedIn = token && !isTokenExpired(token);

    document.querySelectorAll(".auth-only").forEach(el => {
        el.style.display = isLoggedIn ? "block" : "none";
    });

    if (isLoggedIn) {
        const logoutLink = document.querySelector(".auth-only a[href='javascript:void(0);']");
        if (logoutLink) {
            logoutLink.addEventListener("click", logout);
        }
    }

    if (form) {
        form.addEventListener("submit", async function (e) {
            e.preventDefault();

            const username = document.getElementById("username").value.trim();
            const password = document.getElementById("password").value;

            errorDiv.style.display = "none";
            errorDiv.textContent = "";

            const params = new URLSearchParams(window.location.search);
            const returnUrl = params.get("ReturnUrl") || "/Pokemon";

            const token = await login(username, password, returnUrl);

            if (token) {
                window.location.href = returnUrl;
            } else {
                errorDiv.textContent = "Login failed. Please check your credentials.";
                errorDiv.style.display = "block";
            }
        });
    }
});

async function login(username, password, returnUrl = "/Pokemon") {
    try {
        const response = await fetch(`${loginUrl}?returnUrl=${encodeURIComponent(returnUrl)}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ username, password })
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Login failed: ${response.status} - ${errorText}`);
        }

        const { token } = await response.json();

        if (!token) throw new Error("No token received from server.");

        localStorage.setItem("jwtToken", token);
        return token;
    } catch (error) {
        console.error("Login error:", error);
        return null;
    }
}

export function isTokenExpired(token) {
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        return Date.now() >= payload.exp * 1000;
    } catch {
        return true;
    }
}

export async function logout() {
    //try {
    //    await fetch('/api/logout', { method: 'POST' }); // optional
    //} catch (err) {
    //    console.warn("Logout API failed:", err);
    //}

    localStorage.removeItem("jwtToken");
    window.location.href = "/Login";
}
