using Microsoft.Data.Sqlite;

public class SqliteUserRepository : IUserRepository
{
    private readonly string? _connectionString;

    public SqliteUserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public User GetUser(string username, string password)
    {   
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT Username, Password
                                    FROM Users
                                    WHERE Username = @username AND Password = @password";
            
            
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User
                    {
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        Password = reader.GetString(reader.GetOrdinal("Password"))
                    };
                }
            }
           
        }

        return null;
    }
}
