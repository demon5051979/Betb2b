using B2B.Domain;
using B2B.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace B2B.Services
{
	public class ReloadService: BaseHostedService
	{
        private readonly IUserCacheService _userCache;
        private readonly IServiceProvider _services;
        private readonly ILogger<ReloadService> _logger;

        public ReloadService(
           IUserCacheService userCache,
           IServiceProvider services,
           ILogger<ReloadService> logger)
        {
            _userCache = userCache;
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            ReloadUsers();

            while (!cancellationToken.IsCancellationRequested)
            {
                var milliseconds = 600000; // Each 10 minutes
                _logger.LogInformation("Reloading process started.");
                await Task.Delay(milliseconds, cancellationToken);
                _logger.LogInformation("Reload users...");

                try
                {
                    ReloadUsers();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Reload users exception: {ex}");
                }
                 
            }
        }

        public void ReloadUsers()
        {
            ConcurrentDictionary<int, User> newDictionary = new ConcurrentDictionary<int, User>();

            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IB2BRepository>();

                foreach (User user in repo.GetUsers())
                {
                    newDictionary[user.Id] = user;
                }
            }

            _logger.LogInformation($"Users reloaded {DateTime.Now}");
            _userCache.UpdateUsers(newDictionary);
        }
    }
}
