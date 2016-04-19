using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class Movie
    {
        /*Code First ensures that the validation rules you specify on a model class 
        *are  enforced before the application saves changes in the database. 
        */
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)] //Validation rule for Title
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)] //The DataType attributes do not provide any validation. attribute is used to specify a data type that is more specific than  the database intrinsic type. The DataType attribute conveys the semantics of the data as  opposed to how to render it on a screen, and provides benefits that you don't get with DisplayFormat.
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //The DisplayFormat attribute is used to explicitly specify the  date format
        // DataType and DispalyFormat same line [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(5)]
        public string Rating { get; set; }
    }

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}