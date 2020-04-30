using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Core
{
    /// <summary>
    /// details about the current system environment
    /// </summary>
    public class FrameworkEnvironment: IFrameworkEnvironment
    {
        #region Public methods

        public string Configuration => IsDevelopment ? "Developemnt" : "Production";


        public bool IsMobile => RuntimeInformation.FrameworkDescription?.ToLower().Contains("mono") == true;

        public bool IsDevelopment => Assembly.GetEntryAssembly()?.GetCustomAttribute<DebuggableAttribute>()?.IsJITTrackingEnabled == true;

        #endregion

        public FrameworkEnvironment()
        {

        }
    }
}
