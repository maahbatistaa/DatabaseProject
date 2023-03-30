using Dapper;
using Microsoft.Data.SqlClient;

namespace versionDapper.Models
{
	internal class Products
	{
		/// <summary> Checks the existence of the products table, and if it does not exist, creates one </summary>
		/// <param name="conn"></param>
		public void CreateProdutcsTable(SqlConnection conn)
		{
			var existTable = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CadastroProduto'";
			var result = conn.ExecuteScalar<int>(existTable);

			if (result > 0)
			{
				Console.WriteLine("\n**Tabela de produtos existente**");
			}
			else
			{
				conn.Execute("CREATE TABLE CadastroProduto(idProduto INT IDENTITY(1, 1) NOT NULL, cdUsuario VARCHAR(10), descricao VARCHAR(10), cdProduto VARCHAR(6) PRIMARY KEY)");
				Console.WriteLine("\n**Tabela de produtos criada**");
			}
		}

		/// <summary> Insert or update data </summary>
		/// <param name="conn"></param>
		public async void ProductData(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Inserir Dados");
			Console.WriteLine("[2] Atualizar Dados");
			Console.Write("Escolha um comando: ");

			var option = Console.ReadLine();
			if (option == "1")
			{
				Console.WriteLine("Informe Usuário, Descrição e Código do Produto (Separados com Enter");
				await conn.ExecuteAsync("INSERT INTO [CadastroProduto] VALUES (@cdUsuario, @descricao, @cdProduto)",
					new
					{
						cdUsuario = Console.ReadLine(),
						descricao = Console.ReadLine(),
						cdProduto = Console.ReadLine(),
					});
				Console.WriteLine("\n**Dados inseridos na tabela de produtos**");
			}
			else
			{
				Console.WriteLine("Atualize os dados inserindo o Id e informando os dados na ordem (Usuário, Descrição e Código do Produto");
				await conn.ExecuteAsync("UPDATE [CadastroProduto] SET [cdUsuario] = @cdUsuario, [descricao] = @descricao, [cdProduto]=@cdProduto WHERE [idProduto] = @idProduto",
					new
					{
						idProduto = Console.ReadLine(),
						cdUsuario = Console.ReadLine(),
						descricao = Console.ReadLine(),
						cdProduto = Console.ReadLine(),
					});
				Console.WriteLine("\n**Dados atualizados na tabela de produtos**");
			}
		}

		/// <summary> View data of table </summary>
		/// <param name="conn"></param>
		public async void ViewProdutcs(SqlConnection conn)
		{
			var product = await conn.QueryAsync("SELECT [idProduto], [cdUsuario], [descricao], [cdProduto] FROM [CadastroProduto]");
			foreach (var products in product)
				Console.WriteLine(products);
			Thread.Sleep(2000);
		}

		/// <summary> Delete date or table </summary>
		/// <param name="conn"></param>
		public async void DeleteProducts(SqlConnection conn)
		{
			Console.WriteLine("\n[1] Deletar Tabela");
			Console.WriteLine("[2] Deletar Dados");
			Console.Write("Escolha um comando: ");
			var option = Console.ReadLine();

			if (option == "1")
			{
				await conn.ExecuteAsync("DROP TABLE CadastroProduto");
				Console.WriteLine("\n**Tabela de produtos deletada**");
			}
			else
			{
				Console.WriteLine("Informe o Id do Produto a ser deletada");
				await conn.ExecuteAsync("DELETE FROM [CadastroProduto] WHERE [idProduto]=@idProduto",
					new
					{
						idProduto = Console.ReadLine()
					});
				Console.WriteLine("\n**Dado excluido da tabela de produtos!**");
			}
		}
	}
}
