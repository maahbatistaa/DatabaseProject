namespace dbMarrariProdutos.Models
{
	internal class Batch
	{
		public int IdBatch { get; set; }
		public string? ProductCode { get; set; }
		public string? BatchCode { get; set; }

		public Batch(int idBatch, string? productCode, string? batchCode)
		{
			IdBatch = idBatch;
			ProductCode = productCode;
			BatchCode = batchCode;
		}
		public Batch(string? productCode, string? batchCode)
		{
			ProductCode = productCode;
			BatchCode = batchCode;
		}

		public Batch(int idBatch)
		{
			IdBatch = idBatch;
		}

		public Batch() { }

		public static bool IsValid(Batch batch)
		{
			if(batch is null) return false;
			if(string.IsNullOrWhiteSpace(batch.ProductCode)) return false;
			if(string.IsNullOrWhiteSpace(batch.BatchCode)) return false;
			return true;
		}
	}

}
