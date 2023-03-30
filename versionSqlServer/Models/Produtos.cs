using System.Data.SqlClient;

namespace versionSqlServer.Models
{
	internal class Produtos
	{
		/// <summary> Checks the existence of the products table, and if it does not exist, creates one </summary>
		/// <param name="conn"></param>
		public void CreateProdutcsTable(SqlConnection conn)
		{
			if (ExistTableProduct(conn) is true)
			{
				Console.WriteLine("\n**Tabela de produtos existente**");
			}
			else
			{
				string sql = "CREATE TABLE CadastroProduto(idProduto INT IDENTITY(1,1) NOT NULL, cdUsuario VARCHAR(10), descricao VARCHAR(10), cdProduto VARCHAR(6) PRIMARY KEY)";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Tabela de produtos criada**");
			}
		}

		/// <summary> Insert or update data </summary>
		/// <param name="conn"></param>
		public void ProductData(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Inserir Dados");
			Console.WriteLine("[2] Atualizar Dados");
			Console.Write("Escolha um comando: ");

			var opcao = int.Parse(Console.ReadLine());
			if (opcao == 1)
			{
				string sql = "INSERT INTO CadastroProduto(cdProduto, descricao, cdUsuario) VALUES (@cdProduto, @descricao, @cdUsuario)";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o usuário: ");
				string usuario = Console.ReadLine();
				Console.Write("Insira o descrição do produto: ");
				string descricao = Console.ReadLine();
				Console.Write("Insira o código do produto: ");
				string produto = Console.ReadLine();

				cmd.Parameters.AddWithValue("@cdUsuario", usuario);
				cmd.Parameters.AddWithValue("@descricao", descricao);
				cmd.Parameters.AddWithValue("@cdProduto", produto);

				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dados inseridos na tabela de produtos**");
			}
			else
			{
				string sql = "UPDATE CadastroProduto SET cdProduto = @cdProduto, descricao = @descricao, cdUsuario = @cdUsuario WHERE idProduto = @idProduto";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o ID: ");
				int id = int.Parse(Console.ReadLine());
				Console.Write("Insira o usuário: ");
				string usuario = Console.ReadLine();
				Console.Write("Insira o descrição do produto: ");
				string descricao = Console.ReadLine();
				Console.Write("Insira o código do produto: ");
				string produto = Console.ReadLine();

				cmd.Parameters.AddWithValue("@idProduto", id);
				cmd.Parameters.AddWithValue("@cdUsuario", usuario);
				cmd.Parameters.AddWithValue("@descricao", descricao);
				cmd.Parameters.AddWithValue("@cdProduto", produto);

				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dados alterados na tabela de produtos**");
			}
		}

		/// <summary> View data of table </summary>
		/// <param name="conn"></param>
		public void ViewProdutcs(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Visualizar Tabela");
			Console.WriteLine("[2] Visualizar Dado");
			Console.Write("Escolha um comando: ");
			var opcao = int.Parse(Console.ReadLine());

			if (opcao == 1)
			{
				string sql = "SELECT * FROM CadastroProduto";
				SqlCommand cmd = new SqlCommand(sql, conn);
				SqlDataReader r = cmd.ExecuteReader();

				Console.WriteLine("\nIDProduto | CdProduto | Descrição | USUARIO |");
				while (r.Read())
				{
					Console.WriteLine($"{r["idProduto"]} | {r["cdProduto"]} | {r["descricao"]} | {r["cdUsuario"]} ");
				}
				r.Close();
			}
			else
			{
				string sql = "SELECT * FROM CadastroProduto WHERE idProduto = @idProduto";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o Id");
				var id = int.Parse(Console.ReadLine());

				cmd.Parameters.AddWithValue("@idProduto", id);

				SqlDataReader r = cmd.ExecuteReader();
				Console.WriteLine("\nIDProduto | CdProduto | Descrição | USUARIO |");
				while (r.Read())
				{
					Console.WriteLine($"{r["idProduto"]} | {r["cdProduto"]} | {r["descricao"]} | {r["cdUsuario"]} ");
				}
				r.Close();
			}
		}

		/// <summary> Delete date or table </summary>
		/// <param name="conn"></param>
		public void DeleteProducts(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Deletar Tabela");
			Console.WriteLine("[2] Deletar Dados");
			Console.Write("Escolha um comando: ");
			var opcao = int.Parse(Console.ReadLine());

			if (opcao == 1)
			{
				string sql = "DROP TABLE CadastroProduto";
				SqlCommand cmd = new SqlCommand(sql, conn);

				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Tabela de produtos deletada**");
			}
			else
			{
				string sql = "DELETE FROM CadastroProduto WHERE idProduto=@idProduto";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInforme o ID do Produto: ");
				var idProduto = int.Parse(Console.ReadLine());

				cmd.Parameters.AddWithValue("@idProduto", idProduto);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dado excluido da tabela de produtos!**");
			}
		}

		/// <summary> Checks the existence of the table </summary>
		/// <param name="conn"></param>
		/// <returns>true ou false</returns>
		public static bool ExistTableProduct(SqlConnection conn)
		{
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.sysobjects WHERE id=object_id('CadastroProduto')", conn);
			SqlDataReader r = cmd.ExecuteReader();
			var retorno = r.HasRows;
			r.Close();
			return retorno;
		}
	}
}
