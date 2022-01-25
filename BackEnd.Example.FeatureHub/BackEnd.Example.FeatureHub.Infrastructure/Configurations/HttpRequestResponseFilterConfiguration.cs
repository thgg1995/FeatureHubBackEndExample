using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BackEnd.Example.FeatureHub.Infrastructure.Configurations
{
    public class HttpRequestResponseFilterConfiguration
    {
        private IEnumerable<Regex> ignoredPathsRegexes;
        public bool IsActive { get; set; }
        public IEnumerable<string> IgnoreHttpMethods { get; set; }
        public IEnumerable<string> IgnoreHttpPattern { get; set; }

        public IEnumerable<Regex> IgnoredPathsRegexes
        {
            get
            {
                if (ignoredPathsRegexes == null)
                    ignoredPathsRegexes = IgnoreHttpPattern.Select(pathPattern => ToRegex(pathPattern));
                return ignoredPathsRegexes;
            }
        }

        private Regex ToRegex(string ignorePath)
        {
            string replacedPath = ignorePath.Replace("/*", "[/]?.*");
            return new Regex($"(.*{replacedPath}$)", RegexOptions.Compiled & RegexOptions.IgnoreCase);
        }

        public bool ShouldLog(string method, string path)
        {
            return IsActive
                && !(IgnoreHttpMethods != null && IgnoreHttpMethods.Any(x => x.ToUpper() == method.ToUpper()))
                && !(ignoredPathsRegexes != null && ignoredPathsRegexes.Any(regex => regex.IsMatch(path)));
        }
    }
}
