using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLibrary.Data.EF.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public string CoverImageName { get; set; }
        public int ReleaseYear { get; set; }
    }
}
