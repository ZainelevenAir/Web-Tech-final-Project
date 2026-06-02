
//DB ionitilizer is used to create forced table (table create ho raha tha par connect nahi hoi raha tha thats why i created this)


using Microsoft.Data.Sqlite;

namespace StudentManagementAPI.Data
{
    public class DbInitializer
    {
        private readonly string _connectionString;

        public DbInitializer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            CreateTables();
        }

        public void CreateTables()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string query = @"
            CREATE TABLE IF NOT EXISTS Students (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                RollNo TEXT NOT NULL,
                Email TEXT,
                Phone TEXT,
                Department TEXT,
                Semester INTEGER
            );";

            using var command = new SqliteCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }
}