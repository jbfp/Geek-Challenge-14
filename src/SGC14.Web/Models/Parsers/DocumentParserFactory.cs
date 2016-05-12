using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SGC14.Web.Models.Parsers
{
    internal class DocumentParserFactory : IDocumentParserFactory
    {
        private static readonly Lazy<IReadOnlyDictionary<string, IDocumentParser>> Parsers = new Lazy<IReadOnlyDictionary<string, IDocumentParser>>(LoadParsersFromAssembly);
        
        public IDocumentParser Get(string host)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }            

            foreach (var key in Parsers.Value.Keys)
            {
                if (Regex.IsMatch(host, key))
                {
                    return Parsers.Value[key];
                }
            }

            return new DefaultParser();
        }

        private static IReadOnlyDictionary<string, IDocumentParser> LoadParsersFromAssembly()
        {
            var parsers = new Dictionary<string, IDocumentParser>();
            var assembly = Assembly.GetExecutingAssembly();

            // Get all types, that implement IDocumentParser.
            var types = assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i == typeof(IDocumentParser)) && !t.IsAbstract);

            foreach (var type in types)
            {
                // Get all DocumentParserAttributes.Host values (e.g. 'bbc.co.uk', 'bbc.in', 'bbc.com'.)
                var hosts = type.GetCustomAttributes(typeof(DocumentParserAttribute))
                                .OfType<DocumentParserAttribute>()
                                .Select(documentParserAttribute => documentParserAttribute.Host)
                                .Distinct();

                // Create instance of our current document parser.
                var parser = Activator.CreateInstance(type) as IDocumentParser;

                if (parser == null)
                {
                    continue;
                }

                foreach (var host in hosts)
                {
                    parsers.Add(host, parser);
                }
            }

            return new ReadOnlyDictionary<string, IDocumentParser>(parsers);
        }
    }
}