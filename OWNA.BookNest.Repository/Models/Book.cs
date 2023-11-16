using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWNA.BookNest.Repository.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> SelectedAuthorIds { get; set; }
        
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }

        public List<Author>? Authors { get; set; }
    }
}
