using System.Data.SqlClient;
using versionSqlServer.Models;

using SqlConnection conn = new SqlConnection(@"Server=.;Database=dbMarrariProdutos;Trusted_connection=True;Connection Timeout=5;Encrypt=False");
//user=sa;passaword=123abcABC
//habilitar conexões externas
Menus menu = new Menus();

try
{
	conn.Open();
	menu.HomeMenu(conn);
	conn.Close();
}
catch (Exception e)
{
	Console.WriteLine("Erro:" + e.Message + "\t" + e.GetType());
	menu.HomeMenu(conn);
}
Console.ReadKey();