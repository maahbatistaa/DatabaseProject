using dbMarrariProdutos.Models;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace dbMarrariProdutos.Database
{
	internal class SqlServerManager : IDbManager
	{
		#region "Connection"
		private readonly SqlConnection _conn;
		public SqlServerManager(string connString) {
			if (string.IsNullOrWhiteSpace(connString))
				throw new ArgumentNullException(nameof(connString));

			_conn = new(connString);
			_conn.Open();
		}
		#endregion

		#region "Database"
		public bool CreateDataBase()
		{
			string checkDatabase = "SELECT COUNT(*) FROM sys.databases WHERE Name= 'dbMarrariProdutos'";
			using SqlCommand cmd = new SqlCommand(checkDatabase, _conn);
			int count = (int)cmd.ExecuteScalar();

			if (count == 0) {
				string sqlCreate = "CREATE DATABASE dbMarrariProdutos";
				using SqlCommand cmdCreate = new SqlCommand(sqlCreate, _conn);
				cmdCreate.ExecuteNonQuery();
			}

			string sqlUse = "USE dbMarrariProdutos";
			using SqlCommand cmdUse = new SqlCommand(sqlUse, _conn);
			cmdUse.ExecuteNonQuery();
			return true;
		}
		#endregion

		#region "Product"
		public bool CreateTableProduct()
		{
			string sqlSelect = "SELECT * FROM dbo.sysobjects WHERE id=object_id('ProductRegister')";
			using SqlCommand cmdSelect = new SqlCommand(sqlSelect, _conn);
			SqlDataReader reader = cmdSelect.ExecuteReader();
			var retorno = reader.HasRows;
			reader.Close();

			if (retorno is false) {
				string sqlCreate = "CREATE TABLE ProductRegister(idProduct INT IDENTITY(1,1) NOT NULL, ProductName VARCHAR(10), ProductCode VARCHAR(6) PRIMARY KEY , UserName VARCHAR(10))";
				using SqlCommand cmdCreate = new SqlCommand(sqlCreate, _conn);
				cmdCreate.ExecuteNonQuery();
			}
			return true;
		}

		public bool InsertProduct(Product product)
		{
			try {
				string sqlInsert = "INSERT INTO ProductRegister(ProductName, ProductCode, UserName) VALUES (@ProductName, @ProductCode, @UserName)";
				using SqlCommand cmdInsert = new SqlCommand(sqlInsert, _conn);

				cmdInsert.Parameters.AddWithValue("@ProductName", product.ProductName);
				cmdInsert.Parameters.AddWithValue("@ProductCode", product.ProductCode);
				cmdInsert.Parameters.AddWithValue("@UserName", product.UserName);
				cmdInsert.ExecuteNonQuery();
				return true;
			}
			catch { return false; }
		}

		public bool UpdateProduct(Product product)
		{
			try
			{
				string sqlUpdate = "UPDATE ProductRegister SET ProductName = @ProductName, ProductCode = @ProductCode, UserName = @UserName WHERE IdProduct = @IdProduct";
				SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, _conn);

				cmdUpdate.Parameters.AddWithValue("@IdProduct", product.IdProduct);
				cmdUpdate.Parameters.AddWithValue("@ProductName", product.ProductName);
				cmdUpdate.Parameters.AddWithValue("@ProductCode", product.ProductCode);
				cmdUpdate.Parameters.AddWithValue("@UserName", product.UserName);
				cmdUpdate.ExecuteNonQuery();
				return true;
			}
			catch { return false; }
		}

		public IEnumerable<Product> SelectProduct(int idProduct)
		{
			List<Product> products = new();

			string sqlSelect = "SELECT * FROM ProductRegister WHERE IdProduct = @IdProduct";
			using SqlCommand cmdSelect = new SqlCommand(sqlSelect, _conn);

			cmdSelect.Parameters.AddWithValue("@IdProduct", idProduct);
			using SqlDataReader reader = cmdSelect.ExecuteReader();
			while (reader.Read())
			{
				products.Add(
					new Product
					{
						IdProduct = (int)reader["IdProduct"],
						ProductName = (string)reader["ProductName"],
						ProductCode = (string)reader["ProductCode"],
						UserName = (string)reader["UserName"]
					});
			}
			return products;
		}

		public IEnumerable<Product> SelectTableProduct()
		{
			List<Product> products = new();

			string sqlSelect = "SELECT * FROM ProductRegister";
			using SqlCommand cmdSelect = new SqlCommand( sqlSelect, _conn);
			using SqlDataReader reader = cmdSelect.ExecuteReader();

			while (reader.Read())
			{
				products.Add(
					new Product
					{
						IdProduct = (int)reader["IdProduct"],
						ProductName = (string)reader["ProductName"],
						ProductCode = (string)reader["ProductCode"],
						UserName = (string)reader["UserName"]
					});
			}
			return products;
		}

		public bool DeleteProduct(int idProduct)
		{
			string sqlDelete = "DELETE FROM ProductRegister WHERE IdProduct = @IdProduct";
			using SqlCommand cmdDelete = new SqlCommand(sqlDelete, _conn);

			cmdDelete.Parameters.AddWithValue("@IdProduct", idProduct);
			cmdDelete.ExecuteNonQuery();
			return true;
		}

		public bool DropTableProduct()
		{
			string sqlDrop = "DROP TABLE ProductRegister";
			using SqlCommand cmdDrop = new SqlCommand(sqlDrop, _conn);

			cmdDrop.ExecuteNonQuery();
			return true;
		}
		#endregion

		#region "Batch"
		public bool CreateTableBatch()
		{
			string sqlSelect = "SELECT * FROM dbo.sysobjects WHERE id=object_id('Batch')";
			using SqlCommand cmdSelect = new SqlCommand(sqlSelect, _conn);
			SqlDataReader reader = cmdSelect.ExecuteReader();
			var retorno = reader.HasRows;
			reader.Close();

			if (retorno is false)
			{
				string sqlCreate = "CREATE TABLE Batch(idBatch INT IDENTITY(1,1) NOT NULL, ProductCode VARCHAR(6), BatchCode VARCHAR(5) PRIMARY KEY, CONSTRAINT FK_Product FOREIGN KEY (ProductCode) REFERENCES ProductRegister (ProductCode))";
				using SqlCommand cmdCreate = new SqlCommand(sqlCreate, _conn);
				cmdCreate.ExecuteNonQuery();
			}
			return true;
		}

		public bool InsertBatch(Batch batch)
		{
			try
			{
				string sqlInsert = "INSERT INTO Batch(ProductCode, BatchCode) VALUES (@ProductCode, @BatchCode)";
				using SqlCommand cmdInsert = new SqlCommand(sqlInsert, _conn);

				cmdInsert.Parameters.AddWithValue("@ProductCode", batch.ProductCode);
				cmdInsert.Parameters.AddWithValue("@BatchCode", batch.BatchCode);
				cmdInsert.ExecuteNonQuery();
				return true;
			}
			catch { return false; }
		}

		public bool UpdateBatch(Batch batch)
		{
			try
			{
				string sqlUpdate = "UPDATE Batch SET ProductCode = @ProductCode, BatchCode = @BatchCode WHERE idBatch = @idBatch";
				using SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, _conn);

				cmdUpdate.Parameters.AddWithValue("@idBatch", batch.IdBatch);
				cmdUpdate.Parameters.AddWithValue("@ProductCode", batch.ProductCode);
				cmdUpdate.Parameters.AddWithValue("@BatchCode", batch.BatchCode);
				cmdUpdate.ExecuteNonQuery();
				return true;
			}
			catch { return false; }
		}

		public IEnumerable<Batch> SelectBatch(int idBatch)
		{
			List<Batch> batchs = new();

			string sqlSelect = "SELECT * FROM Batch WHERE IdBatch = @IdBatch";
			using SqlCommand cmdSelect = new SqlCommand(sqlSelect, _conn);

			cmdSelect.Parameters.AddWithValue("@IdBatch", idBatch);
			using SqlDataReader reader = cmdSelect.ExecuteReader();
			while (reader.Read())
			{
				batchs.Add(
					new Batch
					{
						IdBatch = (int)reader["IdBatch"],
						ProductCode = (string)reader["ProductCode"],
						BatchCode = (string)reader["BatchCode"]
					});
			}
			return batchs;
		}

		public IEnumerable<Batch> SelectTableBatch()
		{
			List<Batch> batchs = new();

			string sqlSelect = "SELECT * FROM Batch";
			using SqlCommand cmdSelect = new SqlCommand(sqlSelect, _conn);
			using SqlDataReader reader = cmdSelect.ExecuteReader();

			while (reader.Read())
			{
				batchs.Add(
					new Batch
					{
						IdBatch = (int)reader["IdBatch"],
						ProductCode = (string)reader["ProductCode"],
						BatchCode = (string)reader["BatchCode"]
					});
			}
			return batchs;
		}

		public bool DeleteBatch(int idBatch)
		{
			string sqlDelete = "DELETE FROM Batch WHERE IdBatch = @IdBatch";
			using SqlCommand cmdDelete = new SqlCommand(sqlDelete, _conn);

			cmdDelete.Parameters.AddWithValue("@IdBatch", idBatch);
			cmdDelete.ExecuteNonQuery();
			return true;
		}

		public bool DropTableBatch()
		{
			string sqlDrop = "DROP TABLE Batch";
			using SqlCommand cmdDrop = new SqlCommand(sqlDrop, _conn);

			cmdDrop.ExecuteNonQuery();
			return true;
		}
		#endregion

		#region "Part"
		public bool CreateTablePart()
		{
			string sqlSelect = "SELECT * FROM dbo.sysobjects WHERE id=object_id('Part')";
			using SqlCommand cmdSelect = new SqlCommand(sqlSelect, _conn);
			SqlDataReader reader = cmdSelect.ExecuteReader();
			var retorno = reader.HasRows;
			reader.Close();

			if (retorno is false)
			{
				string sqlCreate = "CREATE TABLE Part (idPart INT IDENTITY(1,1), BatchCode VARCHAR(5), Length FLOAT(4), Width FLOAT(4), Thickness FLOAT(4), DateTime DATETIME CONSTRAINT FK_Batch FOREIGN KEY (BatchCode) REFERENCES Batch(BatchCode))"; ;
				using SqlCommand cmdCreate = new SqlCommand(sqlCreate, _conn);
				cmdCreate.ExecuteNonQuery();
			}
			return true;
		}

		public bool InsertPart(Part part)
		{
			try
			{
				string sqlInsert = "INSERT INTO Part (BatchCode, Length, Width, Thickness, DateTime) VALUES (@BatchCode, @Length, @Width, @Thickness, @DateTime)";
				using SqlCommand cmdInsert = new SqlCommand(sqlInsert, _conn);
				SqlDateTime dateTime = new SqlDateTime(DateTime.Now);

				cmdInsert.Parameters.AddWithValue("@BatchCode", part.BatchCode);
				cmdInsert.Parameters.AddWithValue("@Length", part.Length);
				cmdInsert.Parameters.AddWithValue("@Width", part.Width);
				cmdInsert.Parameters.AddWithValue("@Thickness", part.Thickness);
				cmdInsert.Parameters.AddWithValue("@DateTime", dateTime);

				cmdInsert.ExecuteNonQuery();
				return true;
			}
			catch { return false; }
		}

		public bool UpdatePart(Part part)
		{
			try
			{
				string sqlUpdate = "UPDATE Part SET BatchCode = @BatchCode, Length = @Length, Width = @Width, Thickness = @Thickness WHERE idPart = @idPart";
				using SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, _conn);
				SqlDateTime dateTime = new SqlDateTime(DateTime.Now);

				cmdUpdate.Parameters.AddWithValue("@idPart", part.IdPart);
				cmdUpdate.Parameters.AddWithValue("@BatchCode", part.BatchCode);
				cmdUpdate.Parameters.AddWithValue("@Length", part.Length);
				cmdUpdate.Parameters.AddWithValue("@Width", part.Width);
				cmdUpdate.Parameters.AddWithValue("@Thickness", part.Thickness);
				cmdUpdate.Parameters.AddWithValue("@DateTime", dateTime);
				cmdUpdate.ExecuteNonQuery();
				return true;
			}
			catch { return false; }
		}

		public IEnumerable<Part> SelectPart(int idPart)
		{
			List<Part> parts = new();

			string sqlSelect = "SELECT * FROM Part WHERE IdPart = @IdPart";
			using SqlCommand cmdSelect = new SqlCommand(sqlSelect, _conn);

			cmdSelect.Parameters.AddWithValue("@IdPart", idPart);
			using SqlDataReader reader = cmdSelect.ExecuteReader();
			while (reader.Read())
			{
				parts.Add(
					new Part
					{
						IdPart = (int)reader["IdPart"],
						BatchCode = (string)reader["BatchCode"],
						Length = (float)reader["Length"],
						Width = (float)reader["Width"], 
						Thickness = (float)reader["Thickness"],
						DateTime = (DateTime)reader["DateTime"]
					});
			}
			return parts;
		}

		public IEnumerable<Part> SelectTablePart()
		{
			List<Part> parts = new();

			string sqlSelect = "SELECT * FROM Part";
			using SqlCommand cmdSelect = new SqlCommand(sqlSelect, _conn);
			using SqlDataReader reader = cmdSelect.ExecuteReader();

			while (reader.Read())
			{
				parts.Add(
					new Part
					{
						IdPart = (int)reader["IdPart"],
						BatchCode = (string)reader["BatchCode"],
						Length = (float)reader["Length"],
						Width = (float)reader["Width"],
						Thickness = (float)reader["Thickness"],
						DateTime = (DateTime)reader["DateTime"]
					});
			}
			return parts;
		}

		public bool DeletePart(int idPart)
		{
			string sqlDelete = "DELETE FROM Part WHERE IdPart = @IdPart";
			using SqlCommand cmdDelete = new SqlCommand(sqlDelete, _conn);

			cmdDelete.Parameters.AddWithValue("@IdPart", idPart);
			cmdDelete.ExecuteNonQuery();
			return true;
		}

		public bool DropTablePart()
		{
			string sqlDrop = "DROP TABLE Part";
			using SqlCommand cmdDrop = new SqlCommand(sqlDrop, _conn);

			cmdDrop.ExecuteNonQuery();
			return true;
		}
		#endregion
	}
}