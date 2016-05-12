using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace SGC14.Web.Helpers
{
    public static class Blacklist
    {
        private static readonly Lazy<IEnumerable<string>> Patterns = new Lazy<IEnumerable<string>>(() =>
        {
            string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string blacklistPath = Path.Combine(applicationBase, "App_Data", "article_blacklist.txt");

            if (!File.Exists(blacklistPath))
            {
                throw new FileNotFoundException("Blacklist database does not exist.", blacklistPath);
            }

            return File.ReadAllLines(blacklistPath).ToList().AsReadOnly();
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static bool IsValid(string input)
        {
            return Patterns.Value.All(pattern => !Regex.IsMatch(input, (string) pattern));
        }
    }
}