using Dapper;
using dbMarrariProdutos.Models;
using System.Data.Common;
using System.Data.SqlClient;


namespace dbMarrariProdutos.Database
{
	internal class SqlServerDapperManager : IDbManager
	{
		#region "Connection"
		private readonly SqlConnection _conn;
		public SqlServerDapperManager(string connString)
		{
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
			int databaseExists = _conn.ExecuteScalar<int>(checkDatabase);

			if (databaseExists == 0)
				_conn.Execute("CREATE DATABASE dbMarrariProdutos");

			_conn.ChangeDatabase("dbMarrariProdutos");
			return true;
		}
		#endregion

		#region "Product"
		public bool CreateTableProduct()
		{
			string existTable = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ProductRegister'";
			var result = _conn.ExecuteScalar<int>(existTable);

			if (result == 0)
			{
				_conn.Execute("CREATE TABLE ProductRegister(idProduct INT IDENTITY(1,1) NOT NULL, ProductName VARCHAR(10), ProductCode VARCHAR(6) PRIMARY KEY , UserName VARCHAR(10))");
			}
			return true;
		}

		public bool InsertProduct(Product product)
		{
			try
			{
				string sqlInsert = "INSERT INTO ProductRegister(ProductName, ProductCode, UserName) VALUES (@ProductName, @ProductCode, @UserName)";
				_conn.Execute(sqlInsert,
					new
					{
						product.ProductName,
						product.ProductCode,
						product.UserName,
					});
				return true;
			}
			catch { return false; }
		}

		public bool UpdateProduct(Product product)
		{
			try
			{
				string sqlUpdate = "UPDATE ProductRegister SET ProductName = @ProductName, ProductCode = @ProductCode, UserName = @UserName WHERE IdProduct = @IdProduct";
				_conn.Execute(sqlUpdate,
					new
					{
						product.IdProduct,
						product.ProductName,
						product.ProductCode,
						product.UserName,
					});
				return true;
			}
			catch { return false; }
		}

		public IEnumerable<Product> SelectProduct(int idProduct)
		{
			string sqlSelect = "SELECT * FROM ProductRegister WHERE IdProduct = @IdProduct";
			var product = _conn.Query<Product>(sqlSelect, new { IdProduct = idProduct})
				.Select(product => new Product()
				{
					IdProduct = product.IdProduct,
					ProductName = product.ProductName,
					ProductCode = product.ProductCode,
					UserName = product.UserName,
				})
				.ToList();
			return product;
		}

		public IEnumerable<Product> SelectTableProduct()
		{
			string sqlSelect = "SELECT * FROM ProductRegister";
			var product = _conn.Query<Product>(sqlSelect)
				.Select(product => new Product()
				{
					IdProduct = product.IdProduct,
					ProductName = product.ProductName,
					ProductCode = product.ProductCode,
					UserName = product.UserName,
				})
				.ToList();
			return product;
		}

		public bool DeleteProduct(int idProduct)
		{
			string sqlDelete = "DELETE FROM ProductRegister WHERE IdProduct = @IdProduct";
			_conn.Execute(sqlDelete,
				new
				{
					idProduct
				});
			return true;
		}

		public bool DropTableProduct()
		{
			_conn.Execute("DROP TABLE ProductRegister");
			return true;
		}
		#endregion

		#region "Batch"
		public bool CreateTableBatch()
		{
			string existTable = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Batch'";
			var result = _conn.ExecuteScalar<int>(existTable);

			if (result == 0)
			{
				_conn.Execute("CREATE TABLE Batch(idBatch INT IDENTITY(1,1) NOT NULL, ProductCode VARCHAR(6), BatchCode VARCHAR(5) PRIMARY KEY, CONSTRAINT FK_Product FOREIGN KEY (ProductCode) REFERENCES ProductRegister (ProductCode))");
			}
			return true;
		}

		public bool InsertBatch(Batch batch)
		{
			try
			{
				string sqlInsert = "INSERT INTO Batch(ProductCode, BatchCode) VALUES (@ProductCode, @BatchCode)";
				_conn.Execute(sqlInsert,
					new
					{
						batch.ProductCode,
						batch.BatchCode
					});
				return true;
			}
			catch { return false; }
		}

		public bool UpdateBatch(Batch batch)
		{
			try
			{
				string sqlUpdate = "UPDATE Batch SET ProductCode = @ProductCode, BatchCode = @BatchCode WHERE idBatch = @idBatch";
				_conn.Execute(sqlUpdate,
					new
					{
						batch.IdBatch,
						batch.ProductCode,
						batch.BatchCode
					});
				return true;
			}
			catch { return false; }
		}

		public IEnumerable<Batch> SelectBatch(int idBatch)
		{
			string sqlSelect = "SELECT * FROM Batch WHERE IdBatch = @IdBatch";
			var batch = _conn.Query<Batch>(sqlSelect, new { IdBatch = idBatch })
				.Select(batch => new Batch()
				{
					IdBatch = batch.IdBatch,
					ProductCode = batch.ProductCode,
					BatchCode = batch.BatchCode,
				})
				.ToList();
			return batch;
		}

		public IEnumerable<Batch> SelectTableBatch()
		{
			string sqlSelect = "SELECT * FROM Batch";
			var batch = _conn.Query<Batch>(sqlSelect)
				.Select(batch
				=> new Batch()
				{
					IdBatch = batch.IdBatch,
					ProductCode = batch.ProductCode,
					BatchCode = batch.BatchCode,
				})
				.ToList();
			return batch;
		}

		public bool DeleteBatch(int idBatch)
		{
			string sqlDelete = "DELETE FROM Batch WHERE IdBatch = @IdBatch";
			_conn.Execute(sqlDelete,
				new
				{
					idBatch
				});
			return true;
		}

		public bool DropTableBatch()
		{
			_conn.Execute("DROP TABLE Batch");
			return true;
		}
		#endregion

		#region "Part"
		public bool CreateTablePart()
		{
			string existTable = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Part'";
			var result = _conn.ExecuteScalar<int>(existTable);

			if (result == 0)
			{
				_conn.Execute("CREATE TABLE Part (idPart INT IDENTITY(1,1), BatchCode VARCHAR(5), Length FLOAT(4), Width FLOAT(4), Thickness FLOAT(4), DateTime DATETIME CONSTRAINT FK_Batch FOREIGN KEY (BatchCode) REFERENCES Batch(BatchCode))");
			}
			return true;
		}

		public bool InsertPart(Part part)
		{
			try
			{
				string sqlInsert = "INSERT INTO Part (BatchCode, Length, Width, Thickness, DateTime) VALUES (@BatchCode, @Length, @Width, @Thickness, @DateTime)";
				_conn.Execute(sqlInsert,
					new
					{
						part.BatchCode,
						part.Length,
						part.Width,
						part.Thickness,
						DateTime = DateTime.Now
					});
				return true;
			}
			catch { return false; }
		}

		public bool UpdatePart(Part part)
		{
			try
			{
				string sqlUpdate = "UPDATE Part SET BatchCode = @BatchCode, Length = @Length, Width = @Width, Thickness = @Thickness WHERE idPart = @idPart";
				_conn.Execute(sqlUpdate,
					new
					{
						part.IdPart,
						part.BatchCode,
						part.Length,
						part.Width,
						part.Thickness,
						DateTime = DateTime.Now
					});
				return true;
			}
			catch { return false; }
		}

		public IEnumerable<Part> SelectPart(int idPart)
		{
			string sqlSelect = "SELECT * FROM Part WHERE IdPart = @IdPart";
			var part = _conn.Query<Part>(sqlSelect, new { IdPart = idPart })
				.Select(part => new Part()
				{
					IdPart = part.IdPart,
					BatchCode = part.BatchCode,
					Length = part.Length,
					Width = part.Width,
					Thickness = part.Thickness,
					DateTime = part.DateTime
				})
				.ToList();
			return part;
		}

		public IEnumerable<Part> SelectTablePart()
		{
			string sqlSelect = "SELECT * FROM Part";
			var part = _conn.Query<Part>(sqlSelect)
				.Select(part
				=> new Part()
				{
					IdPart = part.IdPart,
					BatchCode = part.BatchCode,
					Length = part.Length,
					Width = part.Width,
					Thickness = part.Thickness,
					DateTime = part.DateTime
				})
				.ToList();
			return part;
		}

		public bool DeletePart(int idPart)
		{
			string sqlDelete = "DELETE FROM Part WHERE IdPart = @IdPart";
			_conn.Execute(sqlDelete,
				new
				{
					idPart
				});
			return true;
		}

		public bool DropTablePart()
		{
			_conn.Execute("DROP TABLE Part");
			return true;
		}
		#endregion
	}
}