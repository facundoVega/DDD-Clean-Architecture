using System.Data;

namespace CleanArchitecture.Applications.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}