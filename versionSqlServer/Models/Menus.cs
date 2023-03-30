using System.Data.SqlClient;

namespace versionSqlServer.Models
{
	internal class Menus
	{
		/// <summary> Displays the home menu to the user </summary>
		/// <param name="conn"></param>
		public void HomeMenu(SqlConnection conn)
		{
			Console.WriteLine("\n\n***MENU INICIAL***");
			Console.WriteLine("[1] Produtos");
			Console.WriteLine("[2] Lotes");
			Console.WriteLine("[3] Peças");
			Console.Write("Escolha um comando: ");

			var opcao = int.Parse(Console.ReadLine());

			switch (opcao)
			{
				case 1:
					ProductsMenu(conn);
					break;
				case 2:
					LotsMenu(conn);
					break;
				case 3:
					PartsMenu(conn);
					break;
				default:
					Console.WriteLine("Comando inválido\n");
					HomeMenu(conn);
					break;
			}
		}

		/// <summary> Displays the options menu for the product table</summary>
		/// <param name="conn"></param>
		public void ProductsMenu(SqlConnection conn)
		{
			Console.WriteLine("\n\n***PRODUTOS***");
			Console.WriteLine("[1] Criar Tabela");
			Console.WriteLine("[2] Dados (inserir ou atualizar)");
			Console.WriteLine("[3] Visualizar (dados ou tabela)");
			Console.WriteLine("[4] Deletar (dados ou tabela)");
			Console.WriteLine("[5] Voltar");
			Console.Write("Escolha um comando: ");

			var opcao = int.Parse(Console.ReadLine());

			Produtos cadastro = new Produtos();
			switch (opcao)
			{
				case 1:
					cadastro.CreateProdutcsTable(conn);
					break;
				case 2:
					cadastro.ProductData(conn);
					break;
				case 3:
					cadastro.ViewProdutcs(conn);
					break;
				case 4:
					cadastro.DeleteProducts(conn);
					break;
				case 5:
					HomeMenu(conn);
					break;
				default:
					Console.WriteLine("Comando inválido");
					ProductsMenu(conn);
					break;
			}
			ProductsMenu(conn);
		}

		/// <summary> Displays the options for the lots table </summary>
		/// <param name="conn"></param>
		public void LotsMenu(SqlConnection conn)
		{
			Console.WriteLine("\n\n***LOTES***");
			Console.WriteLine("[1] Criar Tabela");
			Console.WriteLine("[2] Dados (inserir ou atualizar)");
			Console.WriteLine("[3] Visualizar (dados ou tabela)");
			Console.WriteLine("[4] Deletar (dados ou tabela)");
			Console.WriteLine("[5] Voltar");
			Console.Write("Escolha um comando: ");

			var opcao = int.Parse(Console.ReadLine());

			Lotes lote = new Lotes();
			switch (opcao)
			{
				case 1:
					lote.CreateLotsTable(conn);
					break;
				case 2:
					lote.LotsData(conn);
					break;
				case 3:
					lote.ViewLots(conn);
					break;
				case 4:
					lote.DeleteLots(conn);
					break;
				case 5:
					HomeMenu(conn);
					break;
				default:
					Console.WriteLine("Comando inválido");
					LotsMenu(conn);
					break;
			}
			LotsMenu(conn);
		}

		/// <summary> Displays the options menu for the parts table </summary>
		/// <param name="conn"></param>
		public void PartsMenu(SqlConnection conn)
		{
			Console.WriteLine("\n\n***PEÇAS***");
			Console.WriteLine("[1] Criar Tabela");
			Console.WriteLine("[2] Dados (inserir ou atualizar)");
			Console.WriteLine("[3] Visualizar (dados ou tabela)");
			Console.WriteLine("[4] Deletar (dados ou tabela)");
			Console.WriteLine("[5] Voltar");
			Console.Write("Escolha um comando: ");

			var opcao = int.Parse(Console.ReadLine());

			Pecas pecas = new Pecas();
			switch (opcao)
			{
				case 1:
					pecas.CreatePartsTable(conn);
					break;
				case 2:
					pecas.PartsData(conn);
					break;
				case 3:
					pecas.ViewParts(conn);
					break;
				case 4:
					pecas.DeleteParts(conn);
					break;
				case 5:
					HomeMenu(conn);
					break;
				default:
					Console.WriteLine("Comando inválido");
					PartsMenu(conn);
					break;
			}
			PartsMenu(conn);
		}
	}
}
