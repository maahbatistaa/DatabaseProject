using Microsoft.Data.SqlClient;
using versionDapper.Models;

using SqlConnection conn = new SqlConnection(@"Server=.;Database=master;Trusted_connection=True;Connection Timeout=5;Encrypt=False");

Menus menu = new Menus();
Connection database = new Connection();

try
{
	conn.Open();
	database.CreateDatabase(conn);
	conn.ChangeDatabase("dbMarrariProdutos");
	menu.HomeMenu(conn);
	conn.Close();
}
catch (Exception e)
{
	Console.WriteLine("Erro:" + e.Message + "\t" + e.GetType());
	menu.HomeMenu(conn);
}
Console.ReadKey();