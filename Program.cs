// See https://aka.ms/new-console-template for more information
using MongoDB.Driver;

await RunAsync();

async Task RunAsync()
{
    var connectionString = Environment.GetEnvironmentVariable("MONGO_CONN");
    var client = new MongoClient(connectionString);

    var database = client.GetDatabase("CRUDLivrosDB");
    var collectionAuthor = database.GetCollection<Author>("Authors");
    var collectionBook = database.GetCollection<Book>("Books");

    var NewAuthors = new List<Author> {
    new Author("Jorge Amado", "Brasil"),
    new Author("Machado de Assis", "Brasil"),
    new Author("Monteiro Lobato", "Brasil"),
    new Author("J.K. Rowling", "Reino Unido"),
    new Author("Dan Brown", "US")
    };
    //try
    //{
    //    await collectionAuthor.InsertManyAsync(NewAuthors);
    //    Console.WriteLine("Authors sucessufully added!\n");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine("Error: " + ex.Message);
    //}

    await collectionAuthor.UpdateOneAsync(
        a => a.Id == NewAuthors[1].Id,
        Builders<Author>.Update.Set(a => a.Name, "Jorge Amado")
    );
    Console.WriteLine($"Author {NewAuthors[1].Name} Sucessfully updated!\n");


    var authors = await collectionAuthor.Find(_ => true).ToListAsync();
    Console.WriteLine("-----Authors in Database-----");
    foreach (var a in authors)
    {
        Console.WriteLine("---Author information---");
        Console.WriteLine(a.ToString());
    }
    Console.WriteLine("------------------------\n\n");

    var NewBooks = new List<Book> {
    new Book("Dom Casmurro", NewAuthors[1].Id, 1899),
    new Book("Harry Potter and the Chamber of Secrets",NewAuthors[3].Id, 1988),
    new Book("The Da Vinci Code", NewAuthors[4].Id, 2003)
};
    //try
    //{
    //    await collectionBook.InsertManyAsync(NewBooks);
    //    Console.WriteLine("\nBooks sucessufully added!\n");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine("Error: " + ex.Message);
    //}

    await collectionBook.UpdateOneAsync(
        b => b.Id == NewBooks[0].Id,
        Builders<Book>.Update.Set(b => b.Title, "Capitões da areia")
    );
    Console.WriteLine($"Book {NewBooks[0].Title} Successfully updated!\n");

    await collectionBook.UpdateOneAsync(
        b => b.Id == NewBooks[0].Id,
        Builders<Book>.Update.Set(b => b.Year, 1937)
    );
    Console.WriteLine($"{NewBooks[0].Title} book year {NewBooks[0].Year} successfully updated\n");

    var books = await collectionBook.Find(_ => true).ToListAsync();
    Console.WriteLine("-----Books in DataBase-----");
    foreach (var b in books)
    {
        Console.WriteLine("---Book information---");
        Console.WriteLine(b.ToString());
    }
    Console.WriteLine("------------------------\n\n");


    foreach (var a in authors)  //Authores e seus livros
    {
        var booksOfAuthors = books.Where(b => b.AuthorId == a.Id).ToList();

        if (booksOfAuthors.Count() > 0)
        {
            Console.WriteLine("---Author and their book---");
            Console.WriteLine($"Author: {a.Name}");
            foreach (var b in booksOfAuthors)
            {
                Console.WriteLine($"Book:{b.Title}");
            }
            Console.WriteLine("---------------------------\n");
        }
        else
        {
            Console.WriteLine($"Author {a.Name} has no books registered");
        }

    }

    //----------------------------------------------Deletes-----------------------------------------------
    //var AuthorsToDelete = new List<string> {
    //    "Jorge Amado",
    //    "Monteiro Lobato"
    //};


    //var filtro = Builders<Author>.Filter.In(a => a.Name, AuthorsToDelete);
    //var deletados = await collectionAuthor.DeleteManyAsync(filtro);
    //Console.WriteLine("Authors sucessfully deleted!");

    //await collectionAuthor.DeleteManyAsync(Builders<Author>.Filter.Empty);  //Para  deletar todos
    //Console.WriteLine("Authors sucessfully deleted!");
    //await collectionBook.DeleteManyAsync(Builders<Book>.Filter.Empty);
    //Console.WriteLine("Books sucessfully deleted!");
    //-----------------------------------------------------------------------------------------------------
}

