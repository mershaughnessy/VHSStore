namespace VHSStore.Domain.Models.MovieModels
{
    public class MovieModel
    {
        public string IndexId { get; set; }
        public string MovieName { get; set; }
        public string GenreId { get; set; }
        public string Description { get; set; }
        public int StockNumber { get; set; }
        public decimal StockPrice { get; set; }
        public int Year { get; set; }

        public MovieModel()
        {
        }

        public MovieModel(AddMovieModel newMovie)
        {
            this.MovieName = newMovie.MovieName;
            this.GenreId = newMovie.GenreId;
            this.Description = newMovie.Description;
            this.StockNumber = newMovie.StockNumber;
            this.StockPrice = newMovie.StockPrice;
            this.Year = newMovie.Year;
        }
    }
}
