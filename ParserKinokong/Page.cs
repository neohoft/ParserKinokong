using System.Collections.Generic;
using Leaf.xNet;

namespace ParserKinokong
{
    public class Page
    {
        private readonly List<string> urls;

        public Page(string url, string start, string end)
        {
            urls = GenerateUrlPage(url, start, end);
        }

        public List<string> GetPages()
        {
            var pages = new List<string>();

            using (var request = new HttpRequest())
            {
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                request.AddHeader(HttpHeader.AcceptLanguage, "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                request.AddHeader("Cache-Control", "max-age=0");
                request.AddHeader("DNT", "1");
                request.AddHeader("Host", "kinokong.org");
                request.AddHeader("Upgrade-Insecure-Requests", "1");
                request.KeepAlive = true;
                request.UserAgentRandomize();

                foreach(var html in urls)
                {
                    pages.Add(request.Get(html).ToString());
                }
            }


            return pages;
        }

        private List<string> GenerateUrlPage(string url, string start, string end)
        {
            List<string> urls = new List<string>();

            for(int i = int.Parse(start); i <= int.Parse(end); i++)
            {
                urls.Add(item: $"{url}page/{i.ToString()}/");
            }

            return urls;
        }
    }
}