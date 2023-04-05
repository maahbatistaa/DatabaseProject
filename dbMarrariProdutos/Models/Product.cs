namespace dbMarrariProdutos.Models
{
	internal class Product
	{
		public int IdProduct { get; set; }
		public string? ProductName { get; set; }
		public string? ProductCode { get; set; }
		public string? UserName { get; set; }

		public Product(int idProduct, string productName, string productCode, string userName)
		{
			IdProduct = idProduct;
			ProductName = productName;
			ProductCode = productCode;
			UserName = userName;
		}

		public Product(string productName, string productCode, string userName) {
			ProductName = productName;
			ProductCode = productCode;
			UserName = userName;
		}

		public Product(int idProduct) 
		{
			IdProduct = idProduct;
		}

		public Product() { }

		public static bool IsValid(Product product)
		{
			if(product is null) return false;
			if(string.IsNullOrWhiteSpace(product.ProductName)) return false;
			if(string.IsNullOrWhiteSpace(product.ProductCode)) return false;
			if(string.IsNullOrWhiteSpace(product.UserName)) return false;
			return true;
		}
	}


}