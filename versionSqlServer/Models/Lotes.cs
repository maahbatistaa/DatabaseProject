using System.Data.SqlClient;

namespace versionSqlServer.Models
{
	public class Lotes
	{
		/// <summary> Checks the existence of the lots table, and if it does not exist, creates one </summary>
		/// <param name="conn"></param>
		public void CreateLotsTable(SqlConnection conn)
		{
			if (ExistTableLots(conn) is true)
			{
				Console.WriteLine("\n**Tabela de lotes existente**");
			}
			else
			{
				string sql = "CREATE TABLE Lotes(idLote INT IDENTITY(1,1), cdProduto VARCHAR(6), cdLote VARCHAR(5) PRIMARY KEY, CONSTRAINT FK_Produto FOREIGN KEY (cdProduto) REFERENCES CadastroProduto(cdProduto))";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Tabela de lotes criada**");
			}
		}

		/// <summary> Insert or update data </summary>
		/// <param name="conn"></param>
		public void LotsData(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Inserir Dados");
			Console.WriteLine("[2] Atualizar Dados");
			Console.Write("Escolha um comando: ");

			var opcao = int.Parse(Console.ReadLine());
			if (opcao == 1)
			{
				string sql = "INSERT INTO Lotes(cdLote, cdProduto) VALUES (@cdLote, @cdProduto)";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o código do produto: ");
				string produto = Console.ReadLine();
				Console.Write("Insira o código do lote: ");
				string lote = Console.ReadLine();

				cmd.Parameters.AddWithValue("@cdProduto", produto);
				cmd.Parameters.AddWithValue("@cdLote", lote);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dados inseridos na tabela de lotes**");
			}
			else
			{
				string sql = "UPDATE Lotes SET cdProduto = @cdProduto, cdLote = @cdLote WHERE idLote = @idLote";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o ID do lote: ");
				var id = int.Parse(Console.ReadLine());
				Console.Write("Insira o código do produto: ");
				string produto = Console.ReadLine();
				Console.Write("Insira o código do lote: ");
				string lote = Console.ReadLine();

				cmd.Parameters.AddWithValue("@idLote", id);
				cmd.Parameters.AddWithValue("@cdProduto", produto);
				cmd.Parameters.AddWithValue("@cdLote", lote);

				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dados alterados na tabela de lotes**");
			}
		}

		/// <summary> View data of table </summary>
		/// <param name="conn"></param>
		public void ViewLots(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Visualizar Tabela");
			Console.WriteLine("[2] Visualizar Dado");
			Console.Write("Escolha um comando: ");
			var opcao = int.Parse(Console.ReadLine());

			if (opcao == 1)
			{
				string sql = "SELECT * FROM Lotes";
				SqlCommand cmd = new SqlCommand(sql, conn);
				SqlDataReader r = cmd.ExecuteReader();

				Console.WriteLine("\nidLote | cdProduto | cdLote");
				while (r.Read())
				{
					Console.WriteLine($"{r["idLote"]} | {r["cdProduto"]} | {r["cdLote"]}  ");
				}
				r.Close();
			}
			else
			{
				string sql = "SELECT * FROM Lotes WHERE idLote = @idLote";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o ID:");
				var id = int.Parse(Console.ReadLine());

				cmd.Parameters.AddWithValue("@idLote", id);

				SqlDataReader r = cmd.ExecuteReader();
				Console.WriteLine("\nidLote | cdProduto | cdLote");
				while (r.Read())
				{
					Console.WriteLine($"{r["idLote"]} | {r["cdProduto"]} | {r["cdLote"]}  ");
				}
				r.Close();
			}
		}

		/// <summary> Delete date or table </summary>
		/// <param name="conn"></param>
		public void DeleteLots(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Deletar Tabela");
			Console.WriteLine("[2] Deletar Dados");
			Console.Write("Escolha um comando: ");
			var opcao = int.Parse(Console.ReadLine());

			if (opcao == 1)
			{
				string sql = "DROP TABLE Lotes";
				SqlCommand cmd = new SqlCommand(sql, conn);

				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Tabela de lotes deletada**");
			}
			else
			{
				string sql = "DELETE FROM Lotes WHERE idLote=@idLote";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInforma o ID do Lote: ");
				var id = int.Parse(Console.ReadLine());

				cmd.Parameters.AddWithValue("@idLote", id);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dado excluido da tabela de lotes!**");
			}
		}

		/// <summary> Checks the existence of the table </summary>
		/// <param name="conn"></param>
		/// <returns>true ou false</returns>
		public static bool ExistTableLots(SqlConnection conn)
		{
			string sql = "SELECT * FROM dbo.sysobjects WHERE id=object_id('Lotes')";
			SqlCommand cmd = new SqlCommand(sql, conn);
			SqlDataReader cmd2 = cmd.ExecuteReader();
			var retorno = cmd2.HasRows;
			cmd2.Close();
			return retorno;
		}
	}
}
