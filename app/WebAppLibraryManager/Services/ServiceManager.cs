namespace WebAppLibraryManager.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<ILoanService> _lazyLoanService;
        public ServiceManager(IConfiguration configuration)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(configuration));
            _lazyLoanService = new Lazy<ILoanService>(() => new LoanService(configuration));
        }

        public IUserService UserService => _lazyUserService.Value;
        public ILoanService LoanService => _lazyLoanService.Value;
    }
}
