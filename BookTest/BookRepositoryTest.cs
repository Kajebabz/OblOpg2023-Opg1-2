using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BookRep;

namespace BookRepTests
{
	[TestClass]
	public class BookRepositoryTest
	{
		private BookRepository _repo;

		private readonly Book _badBook = new() { Title = "Fjenden fra nord", Price = 1201 };

		[TestInitialize]
		public void Init()
		{
			_repo = new BookRepository();
		}


		[TestMethod()]
		public void GetTest()
		{
			IEnumerable<Book> books = _repo.Get();
			Assert.AreEqual(5, books.Count());
			Assert.AreEqual(books.First().Title, "The Shining");

			IEnumerable<Book> sortedBooksAsc = _repo.Get(orderBy: "title");
			Assert.AreEqual(sortedBooksAsc.First().Title, "Fight Club");

			IEnumerable<Book> sortedBooksAsc2 = _repo.Get(orderBy: "price");
			Assert.AreEqual(sortedBooksAsc2.First().Title, "Fight Club");

			IEnumerable<Book> sortedBooksDesc = _repo.Get(orderBy: "title_desc");
			Assert.AreEqual(sortedBooksDesc.First().Title, "The Shining");

			IEnumerable<Book> sortedBooksDesc2 = _repo.Get(orderBy: "price_desc");
			Assert.AreEqual(sortedBooksDesc2.First().Title, "The Lord of the Rings");
		}

		[TestMethod()]
		public void GetTest2()
		{
			IEnumerable<Book> books = _repo.Get(titleIncludes: "The", orderBy: "title");
			Assert.AreEqual(3, books.Count());
			Assert.AreEqual(books.First().Title, "The Lord of the Rings");
		}

		[TestMethod()]
		public void GetPriceTest()
		{
			IEnumerable<Book> booksRange400And800 = _repo.Get(minPrice: 400, maxPrice: 800);
			Assert.IsTrue(booksRange400And800.All(b => b.Price >= 400 && b.Price <= 800));
			Assert.AreEqual(2, booksRange400And800.Count());
			Assert.AreEqual("Jurassic Park", booksRange400And800.First().Title);
			Assert.AreEqual("The Maze Runner", booksRange400And800.Last().Title);

			IEnumerable<Book> booksRangeNullAnd300 = _repo.Get(minPrice: null, maxPrice: 300);
			Assert.IsTrue(booksRangeNullAnd300.All(b => b.Price <= 300));
			Assert.AreEqual(1, booksRangeNullAnd300.Count());
			Assert.AreEqual("Fight Club", booksRangeNullAnd300.First().Title);

			IEnumerable<Book> booksRange450AndNull = _repo.Get(minPrice: 450, maxPrice: null);
			Assert.IsTrue(booksRange450AndNull.All(b => b.Price >= 450));
			Assert.AreEqual(3, booksRange450AndNull.Count());

			IEnumerable<Book> allBooks = _repo.Get(minPrice: null, maxPrice: null);
			Assert.AreEqual(_repo.Get().Count(), allBooks.Count());
			Assert.AreEqual(5, allBooks.Count());
		}

		[TestMethod()]
		public void GetPriceWithIncludeTest()
		{
			IEnumerable<Book> books = _repo.Get(minPrice: 400, maxPrice: 800, titleIncludes: "Park");
			Assert.AreEqual(1, books.Count());
			Assert.AreEqual("Jurassic Park", books.First().Title);


		}


			[TestMethod()]
		public void GetByIdTest()
		{
			Book book = _repo.GetById(2);
			Assert.IsNotNull(_repo.GetById(2));
			Assert.AreEqual("Jurassic Park", book.Title);

			Assert.IsNull(_repo.GetById(100));
		}

		[TestMethod()]
		public void AddTest()
		{
			Book m = new() { Title = "Test", Price = 700 };
			Assert.AreEqual(6, _repo.Add(m).Id);
			Assert.AreEqual(6, _repo.Get().Count());

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Add(_badBook));
		}

		[TestMethod()]
		public void RemoveTest()
		{
			Assert.IsNull(_repo.Remove(100));
			Assert.AreEqual(1, _repo.Remove(1)?.Id);
			Assert.AreEqual(4, _repo.Get().Count());
		}

		[TestMethod()]
		public void UpdateTest()
		{
			Assert.AreEqual(5, _repo.Get().Count());
			Book m = new() { Title = "Test", Price = 700 };
			Assert.IsNull(_repo.Update(100, m));
			Assert.AreEqual(1, _repo.Update(1, m)?.Id);
			Assert.AreEqual(5, _repo.Get().Count());

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Update(1, _badBook));
		}
	}
}
