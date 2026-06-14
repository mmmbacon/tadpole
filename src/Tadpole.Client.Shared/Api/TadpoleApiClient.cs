using System.Net.Http.Headers;
using Tadpole.Client.Shared.Auth;

namespace Tadpole.Client.Shared.Api;

public sealed class TadpoleApiClient
{
    private readonly HttpClient _httpClient;
    private readonly AuthSession _session;

    public TadpoleApiClient(HttpClient httpClient, AuthSession session)
    {
        _httpClient = httpClient;
        _session = session;
    }

    public async Task<bool> CheckHealthAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var response = await SendAsync(
                () => _httpClient.GetAsync("health", cancellationToken),
                cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private async Task<HttpResponseMessage> SendAsync(
        Func<Task<HttpResponseMessage>> send,
        CancellationToken cancellationToken)
    {
        ApplyAuthHeader();
        return await send();
    }

    private void ApplyAuthHeader()
    {
        var token = _session.Current?.AccessToken;
        _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(token)
            ? null
            : new AuthenticationHeaderValue("Bearer", token);
    }
}
