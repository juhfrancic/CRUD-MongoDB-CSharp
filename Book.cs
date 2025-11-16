// See https://aka.ms/new-console-template for more information
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Reflection;

public class Book
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string AuthorId { get; set; }

    public int Year { get; set; }

    public Book(string title, string authorId, int year)
    {
        Title = title;
        AuthorId = authorId;
        Year = year;
    }

    public Book() { }

    public override string ToString()
    {
        return $"Book Id: {Id}\nBook Title: {Title}\nAuthor Id: {AuthorId}\nYear: {Year}\n\n";
    }
}