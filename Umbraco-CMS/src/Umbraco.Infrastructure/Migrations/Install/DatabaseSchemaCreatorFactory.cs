using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Infrastructure.Persistence;

namespace Umbraco.Cms.Infrastructure.Migrations.Install;

/// <summary>
///     Creates the initial database schema during install.
/// </summary>
public class DatabaseSchemaCreatorFactory
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IOptionsMonitor<InstallDefaultDataSettings> _installDefaultDataSettings;
    private readonly ILogger<DatabaseSchemaCreator> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IUmbracoVersion _umbracoVersion;

        public DatabaseSchemaCreatorFactory(
            ILogger<DatabaseSchemaCreator> logger,
            ILoggerFactory loggerFactory,
            IUmbracoVersion umbracoVersion,
            IEventAggregator eventAggregator,
            IOptionsMonitor<InstallDefaultDataSettings> installDefaultDataSettings)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            _umbracoVersion = umbracoVersion;
            _eventAggregator = eventAggregator;
            _installDefaultDataSettings = installDefaultDataSettings;
        }

    public DatabaseSchemaCreator Create(IUmbracoDatabase? database) => new DatabaseSchemaCreator(database, _logger,
        _loggerFactory, _umbracoVersion, _eventAggregator, _installDefaultDataSettings);
}
