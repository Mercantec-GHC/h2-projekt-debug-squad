using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Client.Auth
{
    // Custom AuthenticationStateProvider that manages JWT authentication in Blazor WebAssembly
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;   // HttpClient to make API requests
        private readonly IJSRuntime _jsRuntime;     // JS interop to access browser localStorage
        private const string TokenKey = "authToken"; // Key used in localStorage

        // Constructor receives HttpClient and IJSRuntime via dependency injection
        public JwtAuthenticationStateProvider(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        // This method returns the current AuthenticationState (called by Blazor)
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Default anonymous state if user is not authenticated
            var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            // Get token from localStorage
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return anonymous; // If no token, return anonymous state

            // Parse claims from the JWT payload
            var claims = ParseClaimsFromJwt(token);

            // Check the "exp" claim to see if token has expired
            var expClaim = claims.FirstOrDefault(c => c.Type == "exp");
            if (expClaim != null && long.TryParse(expClaim.Value, out var expSeconds))
            {
                var expDate = DateTimeOffset.FromUnixTimeSeconds(expSeconds);
                if (expDate <= DateTimeOffset.UtcNow)
                {
                    // Token is expired: remove it from localStorage and clear authorization header
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                    return anonymous; // Return anonymous state
                }
            }

            // Token is valid: set Authorization header for HttpClient
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Create ClaimsIdentity with parsed claims and authentication type "jwt"
            var identity = new ClaimsIdentity(claims, "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        // Call this method to log in: save token and update authentication state
        public async Task LoginAsync(string token)
        {
            // Save token in browser localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);

            // Add Authorization header to HttpClient
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Notify Blazor that authentication state has changed
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // Call this method to log out: remove token and update authentication state
        public async Task LogoutAsync()
        {
            // Remove token from localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);

            // Clear Authorization header
            _httpClient.DefaultRequestHeaders.Authorization = null;

            // Notify Blazor that authentication state has changed
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // Helper method to retrieve JWT token from localStorage
        private async Task<string?> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }

        // Parse JWT and extract claims from payload
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            // JWT format: header.payload.signature
            var payload = jwt.Split('.')[1];

            // Convert Base64 payload to byte array
            var jsonBytes = ParseBase64WithoutPadding(payload);

            // Parse JSON payload
            var json = JsonDocument.Parse(jsonBytes);

            // Create Claim objects for each property in JWT payload
            return json.RootElement.EnumerateObject().Select(prop => new Claim(prop.Name, prop.Value.ToString()));
        }

        // Decode Base64 string (handling missing padding)
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/'); // Replace URL-safe characters
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break; // Add padding if needed
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}