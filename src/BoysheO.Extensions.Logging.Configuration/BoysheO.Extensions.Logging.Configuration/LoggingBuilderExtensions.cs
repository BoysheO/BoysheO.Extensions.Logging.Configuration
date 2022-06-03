// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace BoysheO.Extensions.Logging
{
    /// <summary>
    /// Extension methods for setting up logging services in an <see cref="ILoggingBuilder" />.
    /// </summary>
    public static class LoggingBuilderExtensions
    {
        /// <summary>
        /// Configures <see cref="LoggerFilterOptions" /> from an instance of <see cref="IConfiguration" />.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configuration">The <see cref="IConfiguration" /> to add.</param>
        /// <returns>The builder.</returns>
        public static ILoggingBuilder AddBoysheOConfiguration(this ILoggingBuilder builder, IConfiguration configuration,Func<Type,string> GetAliasFunc)
        {
            ProviderAliasUtilities.GetAliasFunc = GetAliasFunc ?? throw new ArgumentNullException(nameof(GetAliasFunc));
            builder.AddConfiguration();

            builder.Services.AddSingleton<IConfigureOptions<LoggerFilterOptions>>(new LoggerFilterConfigureOptions(configuration));
            builder.Services.AddSingleton<IOptionsChangeTokenSource<LoggerFilterOptions>>(new ConfigurationChangeTokenSource<LoggerFilterOptions>(configuration));

            builder.Services.AddSingleton(new BoysheO.Extensions.Logging.Configuration.LoggingConfiguration(configuration));

            return builder;
        }
    }
}
