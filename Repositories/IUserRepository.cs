
using System.Security.Cryptography.X509Certificates;

public interface IUserRepository
{
    User GetUser(string username,string password);
}

