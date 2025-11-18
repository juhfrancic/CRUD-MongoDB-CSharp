// See https://aka.ms/new-console-template for more information
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

await RunAsync();

async Task RunAsync()
{
    var connectionString = Environment.GetEnvironmentVariable("MONGO_CONN");
    var client = new MongoClient(connectionString);

    var database = client.GetDatabase("CRUDLivrosDB");
    var collectionAuthor = database.GetCollection<Author>("Authors");
    var collectionBook = database.GetCollection<Book>("Books");


    async Task CreateAuthor(IMongoCollection<Author> collectionAuthor)
    {
        List<Author> authors = new List<Author>();

        Console.WriteLine("How many authors do you want to insert?");
        int quantity = int.Parse(Console.ReadLine());
        for (int i = 0; i < quantity; i++)
        {
            Console.WriteLine($"\nEnter the author's {i + 1} name:");
            string name = Console.ReadLine();

            Console.WriteLine($"\nEnter the author's country:");
            string country = Console.ReadLine();

            authors.Add(new Author(name, country));
        }

        await collectionAuthor.InsertManyAsync(authors);
        Console.WriteLine("\nAuthors added!\n");
    }

    async Task<List<Author>> GetAuthors(IMongoCollection<Author> collectionAuthor)
    {
        return await collectionAuthor.Find(_ => true).ToListAsync();
    }


    async Task UpdateAuthorName(IMongoCollection<Author> collectionAuthor)
    {
        Console.WriteLine("Enter the Id of the author whose name you want to update: ");
        string authorId = Console.ReadLine();

        var id = ObjectId.Parse(authorId);

        Console.WriteLine("Enter the new author name: ");
        string newName = Console.ReadLine();

        await collectionAuthor.UpdateOneAsync(
            a => a.Id == authorId,
            Builders<Author>.Update.Set(a => a.Name, newName)
        );
        Console.WriteLine("Author name updated!");
    }

    async Task DeleteAuthor(IMongoCollection<Author> collectionAuthor)
    {
        Console.WriteLine("Enter the Id of the author whose name you want to update: ");
        string authorId = Console.ReadLine();
        var id = ObjectId.Parse(authorId);

        await collectionAuthor.DeleteOneAsync(
            a => a.Id == authorId
        );
        Console.WriteLine("Author deleted!");
    }


    async Task CreateBook(IMongoCollection<Book> collectionBook)
    {
        List<Book> books = new List<Book>();

        Console.WriteLine("How many books do you want to insert?");
        int quantity = int.Parse(Console.ReadLine());

        for (int i = 0; i < quantity; i++)
        {
            Console.WriteLine($"Enter the book {i + 1} title: ");
            string title = Console.ReadLine();

            Console.WriteLine($"Enter the author's Id of the book: ");
            string authorId = Console.ReadLine();

            Console.WriteLine("Enter the book's year: ");
            int year = int.Parse(Console.ReadLine());

            books.Add(new Book(title, authorId, year));
        }
        await collectionBook.InsertManyAsync(books);
        Console.WriteLine("\nBook added!\n");
    }

    async Task<List<Book>> GetBooks(IMongoCollection<Book> collectionBook)
    {
        return await collectionBook.Find(_ => true).ToListAsync();
    }


    async Task UpdateBookTitle(IMongoCollection<Book> collectionBook)
    {
        Console.WriteLine("Enter the book's Id that you want to update the title: ");
        string bookId = Console.ReadLine();

        var id = ObjectId.Parse(bookId);

        Console.WriteLine("Enter the new title: ");
        string newTitle = Console.ReadLine();

        await collectionBook.UpdateOneAsync(
            a => a.Id == bookId,
            Builders<Book>.Update.Set(a => a.Title, newTitle)
        );
        Console.WriteLine("Book title updated!");
    }

    async Task DeleteBook(IMongoCollection<Book> collectionBook)
    {
        Console.WriteLine("Enter the Id of the author whose name you want to update: ");
        string bookId = Console.ReadLine();
        var id = ObjectId.Parse(bookId);

        await collectionBook.DeleteOneAsync(
            a => a.Id == bookId
        );
        Console.WriteLine("Book deleted!");
    }

    async Task ShowAuthorsWithBooks(IMongoCollection<Author> collectionAuthor,
                          IMongoCollection<Book> collectionBook)
    {
        var authors = await collectionAuthor.Find(_ => true).ToListAsync();
        var books = await collectionBook.Find(_ => true).ToListAsync();

        foreach (var a in authors)
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
    }
    ;

    //--------------------------------------------Switch------------------------------------------------
    int optionAuthor;
    do
    {
        Console.WriteLine("----Authors menu----\n");
        Console.WriteLine("Enter an option:");
        Console.WriteLine("1. Insert Authors");
        Console.WriteLine("2. List authors");
        Console.WriteLine("3. Update author's name");
        Console.WriteLine("4. Delete one author");
        Console.WriteLine("5. Exit the authors menu, and go to the books menu");

        optionAuthor = int.Parse(Console.ReadLine());
        switch (optionAuthor)
        {
            case 1:
                await CreateAuthor(collectionAuthor);
                break;
            case 2:
                var authors = await GetAuthors(collectionAuthor);
                Console.WriteLine("\n----Author list----\n");
                foreach (var a in authors)
                {
                    Console.WriteLine(a.ToString());
                }
                Console.WriteLine("-------------------");
                break;
            case 3:
                await UpdateAuthorName(collectionAuthor);
                break;
            case 4:
                await DeleteAuthor(collectionAuthor);
                break;
            case 5:
                Console.WriteLine("Exiting the authors menu...");
                break;
            default:
                Console.WriteLine("Invalid option, try again");
                break;
        }
    } while (optionAuthor != 5);

    int optionBook;
    do
    {
        Console.WriteLine("\n----Books menu----\n");
        Console.WriteLine("Enter an option:");
        Console.WriteLine("1. Insert books");
        Console.WriteLine("2. List books");
        Console.WriteLine("3. Update book's title");
        Console.WriteLine("4. Delete one book");
        Console.WriteLine("5. Show authors and their books");
        Console.WriteLine("6. Exit the authors menu, and close the program");

        optionBook = int.Parse(Console.ReadLine());
        switch (optionBook)
        {
            case 1:
                await CreateBook(collectionBook);
                break;
            case 2:
                var books = await GetBooks(collectionBook);
                Console.WriteLine("\n----Books list----\n");
                foreach (var b in books)
                {
                    Console.WriteLine(b.ToString());
                }
                Console.WriteLine("-------------------");
                break;
            case 3:
                await UpdateBookTitle(collectionBook);
                break;
            case 4:
                await DeleteBook(collectionBook);
                break;
            case 5:
                await ShowAuthorsWithBooks(collectionAuthor, collectionBook);
                break;
            case 6:
                Console.WriteLine("Exiting the books menu...");
                break;
            default:
                Console.WriteLine("Invalid option, try again");
                break;
        }
    } while (optionBook != 6);
}

//Para deletar tudo da lista:
//await collectionAuthor.DeleteManyAsync(Builders<Author>.Filter.Empty); 
//Console.WriteLine("Authors sucessfully deleted!");
//await collectionBook.DeleteManyAsync(Builders<Book>.Filter.Empty);
//Console.WriteLine("Books sucessfully deleted!");



#region TestesAntigos
//--------------------------------------------------------------------------------------------------
//    await collectionAuthor.UpdateOneAsync(
//        a => a.Id == NewAuthors[1].Id,
//        Builders<Author>.Update.Set(a => a.Name, "Jorge Amado")
//    );
//    Console.WriteLine($"Author {NewAuthors[1].Name} Sucessfully updated!\n");


//    var authors = await collectionAuthor.Find(_ => true).ToListAsync();
//    Console.WriteLine("-----Authors in Database-----");
//    foreach (var a in authors)
//    {
//        Console.WriteLine("---Author information---");
//        Console.WriteLine(a.ToString());
//    }
//    Console.WriteLine("------------------------\n\n");

//    var NewAuthors = new List<Author> {
//    new Author("Jorge Amado", "Brasil"),
//    new Author("Machado de Assis", "Brasil"),
//    new Author("Monteiro Lobato", "Brasil"),
//    new Author("J.K. Rowling", "Reino Unido"),
//    new Author("Dan Brown", "US")
//    };

//    var NewBooks = new List<Book> {
//    new Book("Dom Casmurro", NewAuthors[1].Id, 1899),
//    new Book("Harry Potter and the Chamber of Secrets",NewAuthors[3].Id, 1988),
//    new Book("The Da Vinci Code", NewAuthors[4].Id, 2003)
//};
//    //try
//    //{
//    //    await collectionBook.InsertManyAsync(NewBooks);
//    //    Console.WriteLine("\nBooks sucessufully added!\n");
//    //}
//    //catch (Exception ex)
//    //{
//    //    Console.WriteLine("Error: " + ex.Message);
//    //}

//    await collectionBook.UpdateOneAsync(
//        b => b.Id == NewBooks[0].Id,
//        Builders<Book>.Update.Set(b => b.Title, "Capitões da areia")
//    );
//    Console.WriteLine($"Book {NewBooks[0].Title} Successfully updated!\n");

//    await collectionBook.UpdateOneAsync(
//        b => b.Id == NewBooks[0].Id,
//        Builders<Book>.Update.Set(b => b.Year, 1937)
//    );
//    Console.WriteLine($"{NewBooks[0].Title} book year {NewBooks[0].Year} successfully updated\n");

//    var books = await collectionBook.Find(_ => true).ToListAsync();
//    Console.WriteLine("-----Books in DataBase-----");
//    foreach (var b in books)
//    {
//        Console.WriteLine("---Book information---");
//        Console.WriteLine(b.ToString());
//    }
//    Console.WriteLine("------------------------\n\n");


//    foreach (var a in authors)  //Authores e seus livros
//    {
//        var booksOfAuthors = books.Where(b => b.AuthorId == a.Id).ToList();

//        if (booksOfAuthors.Count() > 0)
//        {
//            Console.WriteLine("---Author and their book---");
//            Console.WriteLine($"Author: {a.Name}");
//            foreach (var b in booksOfAuthors)
//            {
//                Console.WriteLine($"Book:{b.Title}");
//            }
//            Console.WriteLine("---------------------------\n");
//        }
//        else
//        {
//            Console.WriteLine($"Author {a.Name} has no books registered");
//        }

//    }

//    //----------------------------------------------Deletes-----------------------------------------------
//    //var AuthorsToDelete = new List<string> {
//    //    "Jorge Amado",
//    //    "Monteiro Lobato"
//    //};


//    //var filtro = Builders<Author>.Filter.In(a => a.Name, AuthorsToDelete);
//    //var deletados = await collectionAuthor.DeleteManyAsync(filtro);
//    //Console.WriteLine("Authors sucessfully deleted!");

//    //await collectionAuthor.DeleteManyAsync(Builders<Author>.Filter.Empty);  //Para  deletar todos
//    //Console.WriteLine("Authors sucessfully deleted!");
//    //await collectionBook.DeleteManyAsync(Builders<Book>.Filter.Empty);
//    //Console.WriteLine("Books sucessfully deleted!");
//    //-----------------------------------------------------------------------------------------------------
//}
#endregion
