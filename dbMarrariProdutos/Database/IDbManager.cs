using dbMarrariProdutos.Models;

namespace dbMarrariProdutos.Database
{
	internal interface IDbManager
	{
		#region "DataBase"
		bool CreateDataBase();
		#endregion

		#region "Product"
		bool CreateTableProduct();
		bool InsertProduct(Product product);
		bool UpdateProduct(Product product);
		public IEnumerable<Product> SelectProduct(int idProduct);
		public IEnumerable<Product> SelectTableProduct();
		bool DeleteProduct(int idProduct);
		bool DropTableProduct();
		#endregion

		#region "Batch"
		bool CreateTableBatch();
		bool InsertBatch(Batch batch);
		bool UpdateBatch(Batch batch);
		public IEnumerable<Batch> SelectBatch(int idBatch);
		public IEnumerable<Batch> SelectTableBatch();
		bool DeleteBatch(int idBatch);
		bool DropTableBatch();
		#endregion

		#region "Part"
		bool CreateTablePart();
		bool InsertPart(Part part);
		bool UpdatePart(Part part);
		public IEnumerable<Part> SelectPart(int idPart);
		public IEnumerable<Part> SelectTablePart();
		public bool DeletePart(int idPart);
		public bool DropTablePart();
		#endregion
	}
}