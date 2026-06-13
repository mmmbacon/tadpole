using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Tadpole.Client.Shared.Models;

namespace Tadpole.Client.Shared.Auth;

public sealed class TadpoleAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthSession _session;

    public TadpoleAuthStateProvider(AuthSession session)
    {
        _session = session;
        _session.Changed += OnSessionChanged;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var session = _session.Current;
        if (session is null)
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, session.UserId.ToString()),
            new Claim(ClaimTypes.Name, session.DisplayName),
            new Claim(ClaimTypes.Role, session.Role.ToString())
        ], "tadpole");

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    private void OnSessionChanged() =>
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
