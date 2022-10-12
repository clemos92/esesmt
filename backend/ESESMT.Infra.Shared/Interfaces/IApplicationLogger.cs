using System;

namespace ESESMT.Infra.Shared.Interfaces
{
    public interface IApplicationLogger
    {
        void LogError(Exception ex, string message, params object[] propertyValues);
        void LogInformation(string message, params object[] propertyValues);
        void LogWarning(string message, params object[] propertyValues);
    }
}