using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookRep
{
	public class BookRepository
	{
		private int _nextId = 1;
		private readonly List<Book> _books = new();

		public BookRepository()
		{
			_books.Add(new Book() { Id = _nextId++, Title = "The Shining", Price = 399 });
			_books.Add(new Book() { Id = _nextId++, Title = "Jurassic Park", Price = 459 });
			_books.Add(new Book() { Id = _nextId++, Title = "The Lord of the Rings", Price = 1099 });
			_books.Add(new Book() { Id = _nextId++, Title = "The Maze Runner", Price = 699 });
			_books.Add(new Book() { Id = _nextId++, Title = "Fight Club", Price = 239 });
		}

		public IEnumerable<Book> Get(int? minPrice = null, int? maxPrice = null, string? titleIncludes = null, string? orderBy = null)
		{
			IEnumerable<Book> result = new List<Book>(_books);

			if (orderBy != null)
			{
				orderBy = orderBy.ToLower();
				switch (orderBy)
				{
					case "title":
					case "title_asc":
						result = result.OrderBy(m => m.Title);
						break;
					case "title_desc":
						result = result.OrderByDescending(m => m.Title);
						break;
					case "price":
					case "price_asc":
						result = result.OrderBy(m => m.Price);
						break;
					case "price_desc":
						result = result.OrderByDescending(m => m.Price);
						break;
					default:
						break;
				}
			}

			if (titleIncludes != null)
			{
				result = result.Where(m => m.Title.Contains(titleIncludes));
			}
			if (minPrice.HasValue && maxPrice.HasValue)
			{
				result = result.Where(m => m.Price >= minPrice && m.Price <= maxPrice);
			}
			else if (maxPrice.HasValue)
			{
				result = result.Where(m => m.Price <= maxPrice);
			}
			else if (minPrice.HasValue)
			{
				result = result.Where(m => m.Price >= minPrice);
			}

			return result;
		}

		public Book? GetById(int id)
		{
			return _books.Find(book => book.Id == id);
		}

		public Book Add(Book book)
		{
			book.Validate();
			book.Id = _nextId++;
			_books.Add(book);
			return book;
		}

		public Book? Remove(int id)
		{
			Book? book = GetById(id);
			if (book == null)
			{
				return null;
			}
			_books.Remove(book);
			return book;
		}

		public Book? Update(int id, Book book)
		{
			book.Validate();
			Book? existingBook = GetById(id);
			if (existingBook == null)
			{
				return null;
			}
			existingBook.Title = book.Title;
			existingBook.Price = book.Price;
			return existingBook;
		}
	}
}
