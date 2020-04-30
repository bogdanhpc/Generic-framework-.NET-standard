namespace Core
{
    public interface IFrameworkEnvironment
    {
        string Configuration { get; }

        bool IsDevelopment { get; }

        bool IsMobile { get; }
    }
}
