namespace DemoQATests.APITests.Models
{
    public class BooksResponse
    {
        public List<Book> books { get; set; } = new List<Book>();
    }

    public class Book
    {
        public string isbn { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string subTitle { get; set; } = string.Empty;
        public string author { get; set; } = string.Empty;
        public DateTime publish_date { get; set; }
        public string publisher { get; set; } = string.Empty;
        public int pages { get; set; }
        public string description { get; set; } = string.Empty;
        public string website { get; set; } = string.Empty;
    }
}
