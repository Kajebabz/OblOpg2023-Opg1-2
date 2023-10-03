using BookRep;

namespace BookTest
{
	[TestClass]
	public class UnitTest1
	{
		private readonly Book _book = new() { Id = 1, Title = "The Shining", Price = 399 };
		private readonly Book _nullBook = new() { Id = 2, Price = 449 };
		private readonly Book _emptyBook = new() { Id = 3, Title = "", Price = 799 };
		private readonly Book _twoCharBook = new() { Id = 4, Title = "HH", Price = 549 };
		private readonly Book _bookPriceLow = new() { Id = 5, Title = "Jurassic Park", Price = -1 };
		private readonly Book _bookPriceHigh = new() { Id = 6, Title = "The Lord of the Rings", Price = 1201 };

		[TestMethod]
		public void ToStringTest()
		{
			Assert.AreEqual("1 The Shining 399", _book.ToString());
		}

		[TestMethod()]
		public void ValidateTitleTest()
		{
			_book.ValidateTitle();
			Assert.ThrowsException<ArgumentNullException>(() => _nullBook.ValidateTitle());
			Assert.ThrowsException<ArgumentException>(() => _emptyBook.ValidateTitle());
			Assert.ThrowsException<ArgumentException>(() => _twoCharBook.ValidateTitle());
		}

		[TestMethod()]
		public void ValidatePriceTest()
		{
			_book.ValidatePrice();
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => _bookPriceLow.ValidatePrice());
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => _bookPriceHigh.ValidatePrice());
		}

		[TestMethod()]
		public void ValidateTest()
		{
			_book.Validate();
		}
	}
}