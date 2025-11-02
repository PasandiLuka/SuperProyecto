using Microsoft.Extensions.Hosting;

namespace SuperProyecto.Services.Service;

public class DatabaseInitHostedService : IHostedService
{
    private readonly DataBaseCreationService _dbCreator;

    public DatabaseInitHostedService(DataBaseCreationService dbCreator)
    {
        _dbCreator = dbCreator;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _dbCreator.CreateDataBase();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}