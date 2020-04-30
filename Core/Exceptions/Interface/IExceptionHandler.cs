
using System;

namespace Core
{
    public interface IExceptionHandler
    {
        void HandleError(Exception exception);
    }
}
