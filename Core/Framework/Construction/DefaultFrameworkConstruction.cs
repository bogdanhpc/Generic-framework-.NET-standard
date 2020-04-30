namespace Core
{
    public class DefaultFrameworkConstruction : FrameworkConstruction
    {
        public DefaultFrameworkConstruction()
        {
            this.Configure().
                UseDefaultServices();
        }
    }
}
