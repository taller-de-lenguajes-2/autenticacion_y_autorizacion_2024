
public interface IAuthenticationService
{
    bool Login( string username, string password);
    void Logout();
    bool IsAuthenticated();
}


public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpContext context;

    public AuthenticationService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        context = _httpContextAccessor.HttpContext;
    }

    public bool Login(string username, string password)
    {
     
        var user = _userRepository.GetUser(username,password);
        if (user != null)
        {
            context.Session.SetString("IsAuthenticated", "true");
            context.Session.SetString("User", username);
            context.Session.SetString("AccessLevel", user.AccessLevel.ToString());
            return true;
        }

        return false;
    }

    public void Logout()
    {
        context.Session.Remove("IsAuthenticated");
        context.Session.Remove("User");
        context.Session.Remove("AccessLevel");
    }

    public bool IsAuthenticated()
    {
        var context = _httpContextAccessor.HttpContext;

        if (context == null)
        {
            throw new InvalidOperationException("HttpContext no está disponible.");
        }

        return context.Session.GetString("IsAuthenticated") == "true";
    }
}
