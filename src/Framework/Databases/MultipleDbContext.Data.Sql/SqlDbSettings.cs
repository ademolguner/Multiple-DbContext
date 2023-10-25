namespace MultipleDbContext.Data.Sql;

public class SqlDbSettings
{
    public SqlDbSettings(string queryableDatabaseConnection, string executableDatabaseConnection)
    {
        QueryableDatabaseConnection = queryableDatabaseConnection;
        ExecutableDatabaseConnection = executableDatabaseConnection;
    }

    public string QueryableDatabaseConnection { get; set; }
    public string ExecutableDatabaseConnection { get; set; }
}