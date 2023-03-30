using Microsoft.Data.SqlClient;

namespace versionDapper.Models
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
			Console.WriteLine("[4] Filtros");
			Console.Write("Escolha um comando: ");
			var option = Console.ReadLine();

			switch (option)
			{
				case "1": ProductsMenu(conn); break;
				case "2": LotsMenu(conn); break;
				case "3": PartsMenu(conn); break;
				case "4": FiltersMenu(conn); break;
				default: HomeMenu(conn); break;
			}
		}

		/// <summary> Displays the options menu for the product table</summary>
		/// <param name="conn"></param>
		public void ProductsMenu(SqlConnection conn)
		{
			Console.WriteLine("\n\n***PRODUTOS***");
			Console.WriteLine("[1] Criar Tabela");
			Console.WriteLine("[2] Dados (inserir ou atualizar)");
			Console.WriteLine("[3] Visualizar tabela");
			Console.WriteLine("[4] Deletar (dados ou tabela)");
			Console.WriteLine("[5] Voltar");
			Console.Write("Escolha um comando: ");
			var option = Console.ReadLine();

			Products products = new Products();
			switch (option)
			{
				case "1": products.CreateProdutcsTable(conn); break;
				case "2": products.ProductData(conn); break;
				case "3": products.ViewProdutcs(conn); break;
				case "4": products.DeleteProducts(conn); break;
				case "5": HomeMenu(conn); break;
				default: ProductsMenu(conn); break;
			}

			Thread.Sleep(2000);
			ProductsMenu(conn);
		}

		/// <summary> Displays the options for the lots table </summary>
		/// <param name="conn"></param>
		public void LotsMenu(SqlConnection conn)
		{
			Console.WriteLine("\n\n***LOTES***");
			Console.WriteLine("[1] Criar Tabela");
			Console.WriteLine("[2] Dados (inserir ou atualizar)");
			Console.WriteLine("[3] Visualizar tabela");
			Console.WriteLine("[4] Deletar (dados ou tabela)");
			Console.WriteLine("[5] Voltar");
			Console.Write("Escolha um comando: ");
			var option = Console.ReadLine();

			Lots lots = new Lots();
			switch (option)
			{
				case "1": lots.CreateLotsTable(conn); break;
				case "2": lots.LotsData(conn); break;
				case "3": lots.ViewLots(conn); break;
				case "4": lots.DeleteLots(conn); break;
				case "5": HomeMenu(conn); break;
				default: LotsMenu(conn); break;
			}

			Thread.Sleep(2000);
			LotsMenu(conn);
		}

		/// <summary> Displays the options menu for the parts table </summary>
		/// <param name="conn"></param>
		public void PartsMenu(SqlConnection conn)
		{
			Console.WriteLine("\n\n***PEÇAS***");
			Console.WriteLine("[1] Criar Tabela");
			Console.WriteLine("[2] Dados (inserir ou atualizar)");
			Console.WriteLine("[3] Visualizar tabela");
			Console.WriteLine("[4] Deletar (dados ou tabela)");
			Console.WriteLine("[5] Voltar");
			Console.Write("Escolha um comando: ");
			var option = Console.ReadLine();

			Parts parts = new Parts();
			switch (option)
			{
				case "1": parts.CreatePartsTable(conn); break;
				case "2": parts.PartsData(conn); break;
				case "3": parts.ViewParts(conn); break;
				case "4": parts.DeleteParts(conn); break;
				case "5": HomeMenu(conn); break;
				default: PartsMenu(conn); break;
			}

			Thread.Sleep(2000);
			PartsMenu(conn);
		}

		/// <summary>Displays the options menu for filters </summary>
		/// <param name="conn"></param>
		public void FiltersMenu(SqlConnection conn)
		{
			Console.WriteLine("\n\n***FILTROS***");
			Console.WriteLine("[1] Lotes com o Produto");
			Console.WriteLine("[2] Quantidade de Lotes");
			Console.WriteLine("[3] Peças do Lote");
			Console.WriteLine("[4] Voltar");
			Console.Write("Escolha um comando: ");

			var option = Console.ReadLine();

			Filters filters = new Filters();
			switch (option)
			{
				case "1": filters.LotsProduct(conn); break;
				case "2": filters.SumLots(conn); break;
				case "3": filters.PartsLots(conn); break;
				case "4": HomeMenu(conn); break;
				default: FiltersMenu(conn); break;
			}

			Thread.Sleep(2000);
			FiltersMenu(conn);
		}
	}
}
