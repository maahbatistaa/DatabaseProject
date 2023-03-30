using Dapper;
using Microsoft.Data.SqlClient;

namespace versionDapper.Models
{
	internal class Filters
	{
		/// <summary> Shows all batches of the selected product </summary>
		/// <param name="conn"></param>
		public void LotsProduct(SqlConnection conn)
		{
			Console.WriteLine("Informe o Código do Produto para a verificação de lotes");
			var products = conn.Query("SELECT Lotes.idLote, Lotes.cdProduto, Lotes.cdLote FROM Lotes, CadastroProduto WHERE Lotes.cdProduto = CadastroProduto.cdProduto AND Lotes.cdProduto = @cdProduto",
				new
				{
					cdProduto = Console.ReadLine(),
				});
			foreach (var lots in products)
			{
				Console.WriteLine(lots);
			}
		}

		/// <summary> Shows the number of batches in the database</summary>
		/// <param name="conn"></param>
		public void SumLots(SqlConnection conn)
		{
			var count = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Lotes");

			Console.WriteLine($"\n**A tabela contém {count} lotes**");
		}

		/// <summary> Existings parts in the selected batch</summary>
		/// <param name="conn"></param>
		public void PartsLots(SqlConnection conn)
		{
			Console.WriteLine("Informe o Id do Lote para verificação de peças");
			var lots = conn.Query("SELECT Pecas.idPeca, Pecas.cdLote, Pecas.comprimento, Pecas.largura, Pecas.expessura, Pecas.data_hora FROM Pecas, Lotes WHERE Pecas.cdLote = Lotes.cdLote AND Pecas.cdLote = @cdLote",
				new
				{
					cdLote = Console.ReadLine(),
				});
			foreach (var parts in lots)
			{
				Console.WriteLine(parts);
			}
		}
	}
}
