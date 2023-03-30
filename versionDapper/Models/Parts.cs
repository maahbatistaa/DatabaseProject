using Dapper;
using Microsoft.Data.SqlClient;

namespace versionDapper.Models
{
	internal class Parts
	{
		/// <summary> Checks the existence of the parts table, and if it does not exists, creates one </summary>
		/// <param name="conn"></param>
		public void CreatePartsTable(SqlConnection conn)
		{
			var existTable = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Pecas'";
			var result = conn.ExecuteScalar<int>(existTable);

			if (result > 0)
			{
				Console.WriteLine("\n**Tabela de peças existente**");
			}
			else
			{
				conn.Execute("CREATE TABLE Pecas(idPeca INT IDENTITY(1,1), cdLote VARCHAR(5), comprimento FLOAT(4), largura FLOAT(4), expessura FLOAT(4), data_hora DATETIME CONSTRAINT FK_Lote FOREIGN KEY (cdLote) REFERENCES Lotes(cdLote) ON DELETE CASCADE)");
				Console.WriteLine("\n**Tabela de peças criada**");
			}
		}

		/// <summary> Insert or update data </summary>
		/// <param name="conn"></param>
		public async void PartsData(SqlConnection conn)
		{
			Random random = new Random();

			Console.WriteLine("\n[1] Inserir Dados");
			Console.WriteLine("[2] Atualizar Dados");
			Console.Write("Escolha um comando: ");
			var option = Console.ReadLine();

			if (option == "1")
			{
				Console.WriteLine("Informe o Código do Lote");
				await conn.ExecuteAsync("INSERT INTO [Pecas] VALUES (@cdLote, @comprimento, @largura, @expessura, @data_hora)",
					new
					{
						cdLote = Console.ReadLine(),
						comprimento = random.Next(1, 20),
						largura = random.Next(1, 20),
						expessura = random.Next(1, 20),
						data_hora = DateTime.Now
					});
				Console.WriteLine("\n**Dados inseridos na tabela de peças**");
			}
			else
			{
				Console.WriteLine("Atualize os dados inserindo o Id da Peça e informando o Código do Lote (Separados com Enter)");
				await conn.ExecuteAsync("UPDATE [Pecas] SET [cdLote]=@cdLote, [comprimento] = @comprimento, [largura] = @largura, [expessura] = @expessura, [data_hora] = @data_hora WHERE [idPeca] = @idPeca",
					new
					{
						idPeca = Console.ReadLine(),
						cdLote = Console.ReadLine(),
						comprimento = random.Next(1, 20),
						largura = random.Next(1, 20),
						expessura = random.Next(1, 20),
						data_hora = DateTime.Now
					});
				Console.WriteLine("\n**Dados inseridos na tabela de peças**");
			}
		}

		/// <summary> View data of table </summary>
		/// <param name="conn"></param>
		public async void ViewParts(SqlConnection conn)
		{
			var part = await conn.QueryAsync("SELECT [idPeca], [cdLote], [comprimento], [largura], [expessura], [data_hora] FROM [Pecas]");
			foreach (var parts in part)
				Console.WriteLine(parts);
			Thread.Sleep(2500);
		}

		/// <summary> Delete date or table </summary>
		/// <param name="conn"></param>
		public async void DeleteParts(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Deletar Tabela");
			Console.WriteLine("[2] Deletar Dados");
			Console.Write("Escolha um comando: ");
			var option = Console.ReadLine();

			if (option == "1")
			{
				await conn.ExecuteAsync("DROP TABLE Pecas");
				Console.WriteLine("\n**Tabela de peças deletada**");
			}
			else
			{
				Console.WriteLine("Informe o Id da Peça a ser deletada");
				await conn.ExecuteAsync("DELETE FROM [Pecas] WHERE [idPeca] = @idPeca",
					new
					{
						idPeca = Console.ReadLine(),
					});
				Console.WriteLine("\n**Dado excluido da tabela de peças!**");
			}
		}
	}
}
