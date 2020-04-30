using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public class FrameworkConstruction
    {
        protected IServiceCollection _services;

        #region Public props

        public IServiceProvider Provider { get; set; }
        public IServiceCollection Services
        {
            get => _services;
            set
            {
                _services = value;
                if (_services != null)
                {
                    Services.AddSingleton(Environment);
                }
            }
        }
        public IFrameworkEnvironment Environment { get; set; }
        public IConfiguration Configuration { get; set; }
        #endregion
        public FrameworkConstruction()
        {
            Services = new ServiceCollection();
            Environment = new FrameworkEnvironment();
        }

        public void Build(IServiceProvider provider = null)
        {
            Provider = provider ?? Services.BuildServiceProvider();
        }

    }
}
