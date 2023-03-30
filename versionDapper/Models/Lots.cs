using Dapper;
using Microsoft.Data.SqlClient;

namespace versionDapper.Models
{
	internal class Lots
	{
		/// <summary> Checks the existence of the lots table, and if it does not exist, creates one </summary>
		/// <param name="conn"></param>
		public void CreateLotsTable(SqlConnection conn)
		{
			var existTable = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Lotes'";
			var result = conn.ExecuteScalar<int>(existTable);

			if (result > 0)
			{
				Console.WriteLine("\n**Tabela de produtos existente**");
			}
			else
			{
				conn.Execute("CREATE TABLE Lotes(idLote INT IDENTITY(1,1), cdProduto VARCHAR(6), cdLote VARCHAR(5) PRIMARY KEY, CONSTRAINT FK_Produto FOREIGN KEY (cdProduto) REFERENCES CadastroProduto(cdProduto) ON DELETE CASCADE)");
				Console.WriteLine("\n**Tabela de lotes criada**");
			}
		}

		/// <summary> Insert or update data </summary>
		/// <param name="conn"></param>
		public async void LotsData(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Inserir Dados");
			Console.WriteLine("[2] Atualizar Dados");
			Console.Write("Escolha um comando: ");

			var option = Console.ReadLine();
			if (option == "1")
			{
				Console.WriteLine("Informe Código do Produto e Lote (Separados com Enter)");
				await conn.ExecuteAsync("INSERT INTO [Lotes] VALUES (@cdProduto, @cdLote)",
					new
					{
						cdProduto = Console.ReadLine(),
						cdLote = Console.ReadLine(),
					});
				Console.WriteLine("\n**Dados inseridos na tabela de lotes**");
			}
			else
			{
				Console.WriteLine("Atualize os dados inserindo o Id e informando os dados na ordem (Código do Produto e Código do Lote");
				await conn.ExecuteAsync("UPDATE [Lotes] SET [cdProduto] = @cdProduto, [cdLote] = @cdLote WHERE [idLote] = @idLote",
					new
					{
						idLote = Console.ReadLine(),
						cdProduto = Console.ReadLine(),
						cdLote = Console.ReadLine()
					});
				Console.WriteLine("\n**Dados alterados na tabela de lotes**");
			}
		}

		/// <summary> View data of table </summary>
		/// <param name="conn"></param>
		public async void ViewLots(SqlConnection conn)
		{
			var lot = await conn.QueryAsync("SELECT [idLote], [cdProduto], [cdLote] FROM [Lotes]");
			foreach (var lots in lot)
				Console.WriteLine(lots);
			Thread.Sleep(3000);
		}

		/// <summary> Delete date or table </summary>
		/// <param name="conn"></param>
		public async void DeleteLots(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Deletar Tabela");
			Console.WriteLine("[2] Deletar Dados");
			Console.Write("Escolha um comando: ");
			var option = Console.ReadLine();

			if (option == "1")
			{
				await conn.ExecuteAsync("DROP TABLE Lotes");
				Console.WriteLine("\n**Tabela de lotes deletada**");
			}
			else
			{
				Console.WriteLine("Informe o Código do Lote a ser deletado");
				await conn.ExecuteAsync("DELETE FROM [Lotes] WHERE [cdLote]=@cdLote",
					new
					{
						cdLote = Console.ReadLine(),
					});
				Console.WriteLine("\n**Dado excluido da tabela de lotes!**");
			}
		}
	}
}
