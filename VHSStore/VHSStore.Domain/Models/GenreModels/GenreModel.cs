using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Domain.Models.GenreModels
{
    public class GenreModel
    {
        public string IndexId { get; set; }
        public string GenreName { get; set; }

        public GenreModel()
        {

        }

        public GenreModel(string genreName)
        {
            this.GenreName = genreName;
        }
    }
}
