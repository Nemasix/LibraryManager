namespace WebAppLibraryManager.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<ILoanService> _lazyLoanService;
        private readonly Lazy<IBookService> _lazyBookService;
        public ServiceManager(IConfiguration configuration)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(configuration));
            _lazyLoanService = new Lazy<ILoanService>(() => new LoanService(configuration));
            _lazyBookService = new Lazy<IBookService>(() => new BookService(configuration));
        }

        public IUserService UserService => _lazyUserService.Value;
        public ILoanService LoanService => _lazyLoanService.Value;
        public IBookService BookService => _lazyBookService.Value;
    }
}
