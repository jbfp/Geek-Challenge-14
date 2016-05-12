using System.ComponentModel.DataAnnotations;

namespace SGC14.Web.Models
{
    public class SearchQuery
    {
        public SearchQuery()
        {
            Query = string.Empty;
            Language = string.Empty;
            Page = 1;
        }

        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Query { get; set; }
        public int Page { get; set; }
        public string Language { get; set; }
    }
}