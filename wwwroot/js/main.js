﻿
    // ✅ Auto-logout when token is expired
    function checkTokenExpiration() {
        const token = "@ViewBag.Token";

    if (!token) {
        window.location.href = "/Login/Index";
    return;
        }

    try {
            const payload = JSON.parse(atob(token.split('.')[1])); // Decode JWT payload
    const exp = payload.exp * 1000; // Convert expiration time to milliseconds

    const currentTime = Date.now();

            if (currentTime >= exp) {
        // Token has expired, clear the cookie and redirect to login
        document.cookie = "jwt=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    window.location.href = "/Login/Index"; // Redirect to login
            } else {
                // Token is still valid, calculate remaining time
                const remainingTime = exp - currentTime;
    const remainingMinutes = Math.floor(remainingTime / 60000); // minutes
    const remainingSeconds = Math.floor((remainingTime % 60000) / 1000); // seconds

    // Display remaining expiration time
    const expiryMessage = `Your token will expire in ${remainingMinutes} minutes and ${remainingSeconds} seconds.`;
    document.getElementById("token-expiry-info").innerText = expiryMessage;
            }
        } catch (error) {
        console.error("Error decoding token:", error);
    window.location.href = "/Login/Index"; // In case of any error, redirect to login
        }
    }

    // Run the expiration check immediately and then at regular intervals (e.g., every 30 seconds)
    checkTokenExpiration();  // Check immediately on page load

    setInterval(checkTokenExpiration, 1000);  // Check every 30 seconds

