using System;
using System.Net;

namespace SGC14.Web.Models
{
    public class Document
    {
        private readonly string title;
        private readonly string description;
        private readonly DateTime? created;

        public Document(string title, string description, DateTime? created)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title");
            }

            this.title = WebUtility.HtmlDecode(title);
            this.description = WebUtility.HtmlDecode(description ?? string.Empty);
            this.created = created;
        }

        public string Title
        {
            get { return this.title; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public DateTime? Created
        {
            get { return this.created; }
        }
    }
}