using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Tadpole.Client.Shared.Auth;
using Tadpole.Client.Shared.Models;

namespace Tadpole.Client.Shared.RealTime;

public sealed class MessageHubClient : IAsyncDisposable
{
    private readonly TadpoleClientOptions _options;
    private readonly AuthSession _session;
    private HubConnection? _connection;

    public MessageHubClient(IOptions<TadpoleClientOptions> options, AuthSession session)
    {
        _options = options.Value;
        _session = session;
    }

    public HubConnectionState ConnectionState =>
        _connection?.State ?? HubConnectionState.Disconnected;

    public event Action<MessageSummary>? MessageReceived;

    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is { State: HubConnectionState.Connected })
            return;

        if (string.IsNullOrWhiteSpace(_session.Current?.AccessToken))
            throw new InvalidOperationException("Sign in before connecting to the message hub.");

        _connection ??= new HubConnectionBuilder()
            .WithUrl($"{_options.ApiBaseUrl.TrimEnd('/')}/hub/msg", options =>
            {
                options.AccessTokenProvider = () =>
                    Task.FromResult<string?>(_session.Current?.AccessToken);
            })
            .WithAutomaticReconnect()
            .Build();

        _connection.Remove("ReceiveMessage");
        _connection.On<HubMessagePayload>("ReceiveMessage", payload =>
        {
            MessageReceived?.Invoke(new MessageSummary(
                Guid.Empty,
                Guid.Empty,
                Guid.Empty,
                payload.Body,
                payload.SentAt,
                false));
        });

        await _connection.StartAsync(cancellationToken);
    }

    public async Task SendMessageAsync(Guid recipientId, string body, CancellationToken cancellationToken = default)
    {
        if (_connection is null)
            throw new InvalidOperationException("Connect to the message hub before sending.");

        await _connection.InvokeAsync("SendMessage", recipientId, body, cancellationToken);
    }

    public async Task DisconnectAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is not null)
            await _connection.StopAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection is not null)
            await _connection.DisposeAsync();
    }

    private sealed record HubMessagePayload(string Body, DateTimeOffset SentAt);
}
