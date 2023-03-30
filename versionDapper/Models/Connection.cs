using Dapper;
using Microsoft.Data.SqlClient;

namespace versionDapper.Models
{
	public class Connection
	{
		public void CreateDatabase(SqlConnection conn)
		{
			string checkDatabase = "SELECT COUNT(*) FROM sys.databases WHERE Name= 'dbMarrariProdutos'";

			int databaseExists = conn.ExecuteScalar<int>(checkDatabase);

			if (databaseExists == 0)
			{
				conn.Execute("CREATE DATABASE dbMarrariProdutos");
				Console.WriteLine("\n**DataBase criado**");
			}
			else
				Console.WriteLine("**DataBase existente**");
		}
	}
}
