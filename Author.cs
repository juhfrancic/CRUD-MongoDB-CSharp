// See https://aka.ms/new-console-template for more information
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Author
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }

    public Author(string name, string country)
    {
        Name = name;
        Country = country;
    }

    public Author() { }
    public override string ToString()
    {
        return $"Author Id: {Id}\nAuthor name: {Name}\nCountry: {Country}\n\n";
    }
}