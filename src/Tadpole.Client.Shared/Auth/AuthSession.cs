using System.Text.Json;
using Microsoft.JSInterop;
using Tadpole.Client.Shared.Models;

namespace Tadpole.Client.Shared.Auth;

public sealed class AuthSession
{
    private const string StorageKey = "tadpole.session";
    private readonly IJSRuntime _jsRuntime;
    private AuthSessionInfo? _current;

    public AuthSession(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;

    public AuthSessionInfo? Current => _current;

    public bool IsAuthenticated => _current is not null;

    public event Action? Changed;

    public async Task InitializeAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (string.IsNullOrWhiteSpace(json))
            {
                _current = null;
                return;
            }

            _current = JsonSerializer.Deserialize<AuthSessionInfo>(json);
        }
        catch
        {
            _current = null;
        }
    }

    public async Task SignInAsync(AuthSessionInfo session)
    {
        _current = session;
        await _jsRuntime.InvokeVoidAsync(
            "localStorage.setItem",
            StorageKey,
            JsonSerializer.Serialize(session));
        Changed?.Invoke();
    }

    public async Task SignOutAsync()
    {
        _current = null;
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", StorageKey);
        Changed?.Invoke();
    }
}
