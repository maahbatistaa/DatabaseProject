namespace dbMarrariProdutos.Models
{
	internal class Part
	{
		public int IdPart { get; set; }
		public string? BatchCode { get; set; }
		public float Length { get; set; }
		public float Width { get; set; }
		public float Thickness { get; set; }
		public DateTime? DateTime { get; set; }

		public Part(int idPart, string? batchCode, float length, float width, float thickness)
		{
			IdPart = idPart;
			BatchCode = batchCode;
			Length = length;
			Width = width;
			Thickness = thickness;
		}

		public Part(string? batchCode, float length, float width, float thickness)
		{
			BatchCode = batchCode;
			Length = length;
			Width = width;
			Thickness = thickness;
		}

		public Part(int idPart)
		{
			IdPart = idPart;
		}

		public Part() { }

		public static bool IsValid(Part part)
		{
			if (part is null) return false;
			if (string.IsNullOrWhiteSpace(part.BatchCode)) return false;
			return true;
		}
	}
}
