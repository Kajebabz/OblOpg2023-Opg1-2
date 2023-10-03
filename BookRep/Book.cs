namespace BookRep
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Price { get; set; }

		public override string ToString()
		{
			return $"{Id} {Title} {Price}";
		}

		public void ValidateTitle()
		{

			if (Title == null)
			{
				throw new ArgumentNullException(nameof(Title), "Title cannot be null");
			}
			if (Title.Length < 3)
			{
				throw new ArgumentException("Title must be at least 3 character", nameof(Title));
			}
		}

		public void ValidatePrice()
		{
			if (Price < 0 || Price > 1200)
			{
				throw new ArgumentOutOfRangeException(nameof(Price), "Price must be between 0 and 1200");
			}
		}

		public void Validate()
		{
			ValidateTitle();
			ValidatePrice();
		}
	}
}