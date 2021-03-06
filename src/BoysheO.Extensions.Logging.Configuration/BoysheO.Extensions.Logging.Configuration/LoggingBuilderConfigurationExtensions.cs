// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace BoysheO.Extensions.Logging.Configuration
{
    /// <summary>
    /// Extension methods for setting up logging services in an <see cref="ILoggingBuilder" />.
    /// </summary>
    public static class LoggingBuilderConfigurationExtensions
    {
        /// <summary>
        /// Adds services required to consume <see cref="ILoggerProviderConfigurationFactory"/> or <see cref="ILoggerProviderConfiguration{T}"/>
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to register services on.</param>
        public static void AddBoysheOConfiguration(this ILoggingBuilder builder,Func<Type,string> GetAliasFunc)
        {
            ProviderAliasUtilities.GetAliasFunc = GetAliasFunc ?? throw new ArgumentNullException(nameof(GetAliasFunc));
            builder.Services.TryAddSingleton<ILoggerProviderConfigurationFactory, LoggerProviderConfigurationFactory>();
            builder.Services.TryAddSingleton(typeof(ILoggerProviderConfiguration<>), typeof(LoggerProviderConfiguration<>));
        }
    }
}
