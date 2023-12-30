namespace WebAppLibraryManager.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        public ServiceManager(IConfiguration configuration)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(configuration));
        }

        public IUserService UserService => _lazyUserService.Value;
    }
}
