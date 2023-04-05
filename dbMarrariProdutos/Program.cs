using dbMarrariProdutos.Database;
using dbMarrariProdutos.Models;

//connections to different databases
//SqlServerManager connString = new SqlServerManager(@"Server=.;Database=master;Trusted_connection=True;Connection Timeout=5;Encrypt=False");
//SqlServerManager connString = new SqlServerManager(@"Data Source=MARRARI;Initial Catalog=master;User ID=sa;Password=12345");//acesso a outra máquina (liberar o firewall e abrir as portas)
SqlServerDapperManager connString = new SqlServerDapperManager(@"Server=.;Database=master;Trusted_connection=True;Connection Timeout=5;Encrypt=False"); ;

//Main
connString.CreateDataBase();
Show();

void Show()
{
	Console.BackgroundColor = ConsoleColor.Blue;
	Console.WriteLine("\n\n***MENU INICIAL***"); Console.ResetColor();
	Console.WriteLine("[1] Produtos");
	Console.WriteLine("[2] Lotes");
	Console.WriteLine("[3] Peças");
	Console.WriteLine("[0] Sair");
	Console.Write("Escolha um comando: ");

	var option = (Console.ReadLine());
	switch (option)
	{
		case "1": ShowProduct(); break;
		case "2": ShowBatch(); break;
		case "3": ShowPart(); break;
		case "0": Environment.Exit(0); break;
		default: Show(); break;
	}
}

#region"Product"
void ShowProduct()
{
	Console.BackgroundColor = ConsoleColor.Blue;
	Console.WriteLine("\n\n***PRODUTOS***"); Console.ResetColor();
	Console.WriteLine("[1] Criar Tabela");
	Console.WriteLine("[2] Inserir Produtos");
	Console.WriteLine("[3] Atualizar Produtos");
	Console.WriteLine("[4] Visualizar Produto");
	Console.WriteLine("[5] Visualizar Tabela");
	Console.WriteLine("[6] Deletar Produto");
	Console.WriteLine("[7] Deletar Tabela Produtos");
	Console.WriteLine("[8] Voltar");
	Console.WriteLine("[0] Sair");
	Console.Write("Escolha um comando: ");

	var option = Console.ReadLine();
	switch (option)
	{
		case "1":
			connString.CreateTableProduct();
			Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine("\nTabela produtos criada");
			break;
		case "2": ShowInsertProdut(); break;
		case "3": ShowUpdateProduct(); break;
		case "4": ShowSelectProduct(); break;
		case "5": ShowSelectTableProduct(); break;
		case "6": ShowDeleteProduct(); break;
		case "7":
			connString.DropTableProduct();
			Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine("Tabela produtos excluida!");
			break;
		case "8": Show(); break;
		case "0": Environment.Exit(0); break;
		default: Console.WriteLine("Comando inválido"); break;
	}
	ShowProduct();
}

void ShowInsertProdut()
{
	Console.Write("\nInsira o nome do produto: ");
	var productName = Console.ReadLine();
	Console.Write("Insira o código do produto: ");
	var productCode = Console.ReadLine();
	Console.Write("Insira o usário: ");
	var userName = Console.ReadLine();

	Product product = new Product(productName, productCode, userName);

	if (Product.IsValid(product) is false || connString.InsertProduct(product) is false)
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.WriteLine("\nErro ao inserir produto");
	}
	else
	{
		Console.BackgroundColor = ConsoleColor.Green;
		Console.WriteLine("\nProdutos adicionado!");	
	}
}

void ShowUpdateProduct()
{
	Console.Write("\nInserir o ID: ");
	int idProduct = int.Parse(Console.ReadLine());
	Console.Write("Insira o nome do produto: ");
	var productName = Console.ReadLine();
	Console.Write("Insira o código do produto: ");
	var productCode = Console.ReadLine();
	Console.Write("Insira o usário: ");
	var userName = Console.ReadLine();

	Product product = new Product(idProduct, productName, productCode, userName);

	if (Product.IsValid(product) is false || connString.UpdateProduct(product) is false)
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.WriteLine("\nErro ao atualizar produto");
	}
	else
	{
		Console.BackgroundColor = ConsoleColor.Green;
		Console.WriteLine("\nProduto atualizado!");
	}
}

void ShowSelectProduct()
{
	Console.Write("\nInsira o Id: ");
	int idProduct = int.Parse(Console.ReadLine());

	Product product = new Product(idProduct);

	var productSelect = connString.SelectProduct(idProduct);

	foreach (var products in productSelect)
	{
		Console.WriteLine($"IdProduct: {products.IdProduct}, ProductName: {products.ProductName}, ProductCode: {products.ProductCode}, UserName: {products.UserName}");
	}
}

void ShowSelectTableProduct()
{
	Product product = new Product();

	var productSelect = connString.SelectTableProduct();

	foreach (var products in productSelect)
	{
		Console.WriteLine($"IdProduct: {products.IdProduct}, ProductName: {products.ProductName}, ProductCode: {products.ProductCode}, UserName: {products.UserName}");
	}
}

void ShowDeleteProduct()
{
	Console.Write("\nInsira o Id: ");
	int idProduct = int.Parse(Console.ReadLine());

	Product product = new Product(idProduct);
	connString.DeleteProduct(idProduct);

	Console.BackgroundColor = ConsoleColor.Green;
	Console.WriteLine("Produto excluido!");
}
#endregion

#region"Batch"
void ShowBatch()
{
	Console.BackgroundColor = ConsoleColor.Blue;
	Console.WriteLine("\n\n***LOTES***"); Console.ResetColor();
	Console.WriteLine("[1] Criar Tabela");
	Console.WriteLine("[2] Inserir Lote");
	Console.WriteLine("[3] Atualizar Lote");
	Console.WriteLine("[4] Visualizar Lote");
	Console.WriteLine("[5] Visualizar Tabela");
	Console.WriteLine("[6] Deletar Lote");
	Console.WriteLine("[7] Deletar Tabela Lotes");
	Console.WriteLine("[8] Voltar");
	Console.WriteLine("[0] Sair");
	Console.Write("Escolha um comando: ");

	var option = Console.ReadLine();
	switch (option)
	{
		case "1":
			connString.CreateTableBatch();
			Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine("\nTabela lotes criada");
			break;
		case "2": ShowInsertBatch(); break;
		case "3": ShowUpdateBatch(); break;
		case "4": ShowSelectBatch(); break;
		case "5": ShowSelectTableBatch(); break;
		case "6": ShowDeleteBatch(); break;
		case "7":
			connString.DropTableBatch();
			Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine("Tabela lote excluida!");
			break;
		case "8": Show(); break;
		case "0": Environment.Exit(0); break;
		default: Console.WriteLine("Comando inválido"); break;
	}
	ShowBatch();
}

void ShowInsertBatch()
{
	Console.Write("Insira o código do produto: ");
	var productCode = Console.ReadLine();
	Console.Write("Insira o código do lote: ");
	var batchCode = Console.ReadLine();

	Batch batch = new Batch(productCode, batchCode);

	if (Batch.IsValid(batch) is false || connString.InsertBatch(batch) is false)
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.WriteLine("\nErro ao inserir lote");
	}
	else
	{
		Console.BackgroundColor = ConsoleColor.Green;
		Console.WriteLine("\nLote adicionado!");
	}
}

void ShowUpdateBatch()
{
	Console.Write("\nInserir o ID: ");
	int idBatch = int.Parse(Console.ReadLine());
	Console.Write("Insira o código do produto: ");
	var productCode = Console.ReadLine();
	Console.Write("Insira o código do lote: ");
	var batchCode = Console.ReadLine();

	Batch batch = new Batch(idBatch, productCode, batchCode);

	if (Batch.IsValid(batch) is false || connString.UpdateBatch(batch) is false)
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.WriteLine("\nErro ao atualizar lote");
	}
	else
	{
		Console.BackgroundColor = ConsoleColor.Green;
		Console.WriteLine("\nLote atualizado!");
	}
}

void ShowSelectBatch()
{
	Console.Write("\nInsira o Id: ");
	int idBatch = int.Parse(Console.ReadLine());

	Batch batch = new Batch(idBatch);

	var batchSelect = connString.SelectBatch(idBatch);

	foreach (var batchs in batchSelect)
	{
		Console.WriteLine($"IdBatch: {batchs.IdBatch}, ProductCode: {batchs.ProductCode}, BatchCode: {batchs.BatchCode}");
	}
}

void ShowSelectTableBatch()
{
	Batch batch = new Batch();

	var batchSelect = connString.SelectTableBatch();

	foreach (var batchs in batchSelect)
	{
		Console.WriteLine($"IdBatch: {batchs.IdBatch}, ProductCode: {batchs.ProductCode}, BatchCode: {batchs.BatchCode}");
	}
}

void ShowDeleteBatch()
{
	Console.Write("\nInsira o Id: ");
	int idBatch = int.Parse(Console.ReadLine());

	Batch batch = new Batch(idBatch);
	connString.DeleteBatch(idBatch);

	Console.BackgroundColor = ConsoleColor.Green;
	Console.WriteLine("Lote excluido!");
}
#endregion

#region "Part"
void ShowPart()
{
	Console.BackgroundColor = ConsoleColor.Blue;
	Console.WriteLine("\n\n***PEÇAS***"); Console.ResetColor();
	Console.WriteLine("[1] Criar Tabela");
	Console.WriteLine("[2] Inserir Peças");
	Console.WriteLine("[3] Atualizar Peça");
	Console.WriteLine("[4] Visualizar Peça");
	Console.WriteLine("[5] Visualizar Tabela");
	Console.WriteLine("[6] Deletar Peça");
	Console.WriteLine("[7] Deletar Tabela Peças");
	Console.WriteLine("[8] Voltar");
	Console.WriteLine("[0] Sair");
	Console.Write("Escolha um comando: ");

	var option = Console.ReadLine();
	switch (option)
	{
		case "1":
			connString.CreateTablePart();
			Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine("\nTabela peças criada");
			break;
		case "2": ShowInsertPart(); break;
		case "3": ShowUpdatePart(); break;
		case "4": ShowSelectPart(); break;
		case "5": ShowSelectTablePart(); break;
		case "6": ShowDeletePart(); break;
		case "7":
			connString.DropTablePart();
			Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine("Tabela peças excluida!");
			break;
		case "8": Show(); break;
		case "0": Environment.Exit(0); break;
		default: Console.WriteLine("Comando inválido"); break;
	}
	ShowPart();
}

void ShowInsertPart()
{
	Console.Write("\nInsira o código do lote: ");
	var batchCode = Console.ReadLine();

	Random random = new Random();
	float length = random.Next(1, 20);
	float width = random.Next(1, 20);
	float thickness = random.Next(1, 20);

	Part part = new Part(batchCode, length, width, thickness);

	if (Part.IsValid(part) is false || connString.InsertPart(part) is false)
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.WriteLine("\nErro ao inserir peça");
	}
	else
	{
		Console.BackgroundColor = ConsoleColor.Green;
		Console.WriteLine("\nPeça adicionado!");
	}
}

void ShowUpdatePart()
{
	Console.Write("\nInsira o ID: ");
	int idPart = int.Parse(Console.ReadLine());
	Console.Write("Insira o código do lote: ");
	var batchCode = Console.ReadLine();

	Random random = new Random();
	float length = random.Next(1, 20);
	float width = random.Next(1, 20);
	float thickness = random.Next(1, 20);

	Part part = new Part(idPart, batchCode, length, width, thickness);

	if (Part.IsValid(part) is false || connString.UpdatePart(part) is false)
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.WriteLine("\nErro ao atualizar peça");
	}
	else
	{
		Console.BackgroundColor = ConsoleColor.Green;
		Console.WriteLine("\nPeça atualizado!");
	}
}

void ShowSelectPart()
{
	Console.Write("\nInsira o Id: ");
	var idPart = int.Parse(Console.ReadLine());

	Part part = new Part(idPart);

	var partSelect = connString.SelectPart(idPart);

	foreach (var parts in partSelect)
	{
		Console.WriteLine($"IdParts: {parts.IdPart}, BatchCode: {parts.BatchCode}, Length: {parts.Length}, Width: {parts.Width}, Thickness: {parts.Thickness}, DateTime: {parts.DateTime}");
	}
}

void ShowSelectTablePart()
{
	Part part = new Part();

	var partSelect = connString.SelectTablePart();

	foreach (var parts in partSelect)
	{
		Console.WriteLine($"IdParts: {parts.IdPart}, BatchCode: {parts.BatchCode}, Length: {parts.Length}, Width: {parts.Width}, Thickness: {parts.Thickness}, DateTime: {parts.DateTime}");
	}
}

void ShowDeletePart()
{
	Console.Write("\nInsira o Id: ");
	int idPart = int.Parse(Console.ReadLine());

	Part part = new Part(idPart);
	connString.DeletePart(idPart);

	Console.BackgroundColor = ConsoleColor.Green;
	Console.WriteLine("Peça excluida!");
}
#endregion