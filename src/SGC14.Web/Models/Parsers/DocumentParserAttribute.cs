using System;

namespace SGC14.Web.Models.Parsers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DocumentParserAttribute : Attribute
    {
        private readonly string host;

        public DocumentParserAttribute(string host)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }

            this.host = host;
        }

        public string Host
        {
            get { return this.host; }
        }
    }
}