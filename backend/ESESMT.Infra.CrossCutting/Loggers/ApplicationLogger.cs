using Serilog;
using System;
using ESESMT.Infra.Shared.Interfaces;

namespace ESESMT.Infra.CrossCutting.Loggers
{
    public class ApplicationLogger : IApplicationLogger
    {
        public void LogInformation(string message, params object[] propertyValues)
        {
            Log.Logger.Information(message, propertyValues);
        }

        public void LogWarning(string message, params object[] propertyValues)
        {
            Log.Logger.Warning(message, propertyValues);
        }

        public void LogError(Exception ex, string message, params object[] propertyValues)
        {
            Log.Logger.Error(ex, message, propertyValues);
        }
    }
}
