using BooksApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BooksDb"));
            var database = client.GetDatabase("BooksDb");
            _books = database.GetCollection<Book>("Books");
        }

        public List<Book> Get()
        {
            return _books.Find(book => true).ToList();
        }

        public Book Get(string id)
        {
            return _books.Find(book => book.Id == id).FirstOrDefault();
        }
    }
}
