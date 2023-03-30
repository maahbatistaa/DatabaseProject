using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace versionSqlServer.Models
{
	internal class Pecas
	{
		/// <summary> Checks the existence of the parts table, and if it does not exists, creates one </summary>
		/// <param name="conn"></param>
		public void CreatePartsTable(SqlConnection conn)
		{
			if (ExistTableParts(conn) is true)
			{
				Console.WriteLine("\n**Tabela de peças existente**");
			}
			else
			{
				string sql = "CREATE TABLE Pecas(idPeca INT IDENTITY(1,1), cdLote VARCHAR(5), comprimento FLOAT(4), largura FLOAT(4), expessura FLOAT(4), data_hora DATETIME CONSTRAINT FK_Lote FOREIGN KEY (cdLote) REFERENCES Lotes(cdLote))";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Tabela de peças criada**");
			}
		}

		/// <summary> Insert or update data </summary>
		/// <param name="conn"></param>
		public void PartsData(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Inserir Dados");
			Console.WriteLine("[2] Atualizar Dados");
			Console.Write("Escolha um comando: ");

			var opcao = int.Parse(Console.ReadLine());
			if (opcao == 1)
			{
				string sql = "INSERT INTO Pecas(cdLote, comprimento, largura, expessura, data_hora) VALUES (@cdLote, @comprimento, @largura, @expessura, @data_hora)";

				Random random = new Random();
				float comprimento = random.Next(1, 20);
				float largura = random.Next(1, 20);
				float expessura = random.Next(1, 20);

				SqlDateTime date = new SqlDateTime(DateTime.Now);

				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o código do lote: ");
				string lote = Console.ReadLine(); ;

				cmd.Parameters.AddWithValue("@cdLote", lote);
				cmd.Parameters.AddWithValue("@comprimento", comprimento * 0.1);
				cmd.Parameters.AddWithValue("@largura", largura * 0.1);
				cmd.Parameters.AddWithValue("@expessura", expessura * 0.1);
				cmd.Parameters.AddWithValue("@data_hora", date);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dados inseridos na tabela de peças**");
			}
			else
			{
				string sql = "UPDATE Pecas SET cdLote = @cdLote, comprimento = @comprimento, largura = @largura, expessura= @expessura, data_hora=@data_hora WHERE idPeca = @idPeca";

				Random random = new Random();
				float comprimento = random.Next(1, 20);
				float largura = random.Next(1, 20);
				float expessura = random.Next(1, 20);

				SqlDateTime date = new SqlDateTime(DateTime.Now);

				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o ID: ");
				var id = int.Parse(Console.ReadLine());
				Console.Write("Insira o código do lote: ");
				var lote = int.Parse(Console.ReadLine());

				cmd.Parameters.AddWithValue("@idPeca", id);
				cmd.Parameters.AddWithValue("@cdLote", lote);
				cmd.Parameters.AddWithValue("@comprimento", comprimento * 0.1);
				cmd.Parameters.AddWithValue("@largura", largura * 0.1);
				cmd.Parameters.AddWithValue("@expessura", expessura * 0.1);
				cmd.Parameters.AddWithValue("@data_hora", date);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dados inseridos na tabela de peças**");
			}
		}

		/// <summary> View data of table </summary>
		/// <param name="conn"></param>
		public void ViewParts(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Visualizar Tabela");
			Console.WriteLine("[2] Visualizar Dado");
			Console.Write("Escolha um comando: ");
			var opcao = int.Parse(Console.ReadLine());

			if (opcao == 1)
			{
				string sql = "SELECT * FROM Pecas";
				SqlCommand cmd = new SqlCommand(sql, conn);
				SqlDataReader r = cmd.ExecuteReader();

				Console.WriteLine("\nidPeca | cdLote | Comprimento | Largura | Expessura | Data/Hora");
				while (r.Read())
				{
					Console.WriteLine($"{r["idPeca"]} | {r["cdLote"]} | {r["comprimento"]} | {r["largura"]} | {r["expessura"]} | {r["data_hora"]}");
				}
				r.Close();
			}
			else
			{
				string sql = "SELECT * FROM Pecas WHERE idPeca=@idPeca";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInsira o ID: ");
				var id = int.Parse(Console.ReadLine());

				cmd.Parameters.AddWithValue("@idPeca", id);

				SqlDataReader r = cmd.ExecuteReader();
				Console.WriteLine("\nIDPeca | cdLote | Comprimento | Largura | Expessura | Data/Hora");
				while (r.Read())
				{
					Console.WriteLine($"{r["idPeca"]} | {r["cdLote"]} | {r["comprimento"]} | {r["largura"]} | {r["expessura"]} | {r["data_hora"]}");
				}
				r.Close();
			}
		}

		/// <summary> Delete date or table </summary>
		/// <param name="conn"></param>
		public void DeleteParts(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Deletar Tabela");
			Console.WriteLine("[2] Deletar Dados");
			Console.Write("Escolha um comando: ");
			var opcao = int.Parse(Console.ReadLine());

			if (opcao == 1)
			{
				string sql = "DROP TABLE Pecas";
				SqlCommand cmd = new SqlCommand(sql, conn);

				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Tabela de peças deletada**");
			}
			else
			{
				string sql = "DELETE FROM Pecas WHERE idPeca=@idPeca";
				SqlCommand cmd = new SqlCommand(sql, conn);

				Console.Write("\nInforma o ID do peça: ");
				var id = int.Parse(Console.ReadLine());

				cmd.Parameters.AddWithValue("@idPeca", id);
				cmd.ExecuteNonQuery();
				Console.WriteLine("\n**Dado excluido da tabela de peças!**");
			}
		}

		/// <summary> Checks the existence of the table </summary>
		/// <param name="conn"></param>
		/// <returns>true ou false</returns>
		public static bool ExistTableParts(SqlConnection conn)
		{
			string sql = "SELECT * FROM dbo.sysobjects WHERE id=object_id('Pecas')";
			SqlCommand cmd = new SqlCommand(sql, conn);
			SqlDataReader cmd2 = cmd.ExecuteReader();
			var retorno = cmd2.HasRows;
			cmd2.Close();
			return retorno;
		}
	}
}
