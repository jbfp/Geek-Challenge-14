using System;

namespace SGC14.Web.Models
{
    public interface ISGC14Item
    {
        object Id { get; }
        string Type { get; }
        DateTime? Created { get; }
    }
}