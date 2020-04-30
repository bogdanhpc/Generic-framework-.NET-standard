using System;

namespace Core
{
    public class BaseExceptionHandler : IExceptionHandler
    {
        public void HandleError(Exception exception)
        {
            Framework.Logger.LogCriticalSource("Unhadled exception occured!", exception: exception);
        }
    }
}
