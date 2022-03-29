using System.ComponentModel.DataAnnotations;

namespace VHSStore.Domain.Models.MovieModels
{
    public class AddMovieModel
    {
        [Required(ErrorMessage = "Missing: MovieName")]
        public string MovieName { get; set; }
        [Required(ErrorMessage = "Missing: GenreId")]
        public string GenreId { get; set; }
        [Required(ErrorMessage = "Missing: Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Missing: StockNumber")]
        public int StockNumber { get; set; }
        [Required(ErrorMessage = "Missing: StockPrice")]
        public decimal StockPrice { get; set; }
        [Required(ErrorMessage = "Missing: Year")]
        public int Year { get; set; }
    }
}
