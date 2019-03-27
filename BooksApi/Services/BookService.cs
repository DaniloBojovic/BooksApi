using BooksApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
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

        public async Task<List<Book>> GetBookAsync()
        {
            return await _books.Find(book => true).ToListAsync();
        }

        public async Task<Book> Get(string id)
        {
            return await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Book> Create(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }

        public async Task Replace(string id, Book replaceBook)
        {
            await _books.ReplaceOneAsync(b => b.Id == id, replaceBook);
        }

        public async Task Update(string id, Book updateBook)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);
            var update = Builders<Book>.Update.Set(x => x.Price, updateBook.Price);
            await _books.UpdateOneAsync(filter, update);
        }

        public async Task Remove(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
        }

    }
}
