using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostingParser.JobBoardScrapers
{
    public class LinkedInScraper : JobBoardScraperInterface
    {
        public void ExtractAndPrintJobDetails(string url)
        {
            string[] infos = url.Split('=');
            string jobId = infos[1].Substring(0, infos[1].IndexOf('&'));
            string baseURL = "https://www.linkedin.com/jobs/view/" + jobId;

            var web = new HtmlWeb();
            var htmlDoc = web.Load(baseURL);

            string jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section[1]/section[2]/div[1]/div[1]/h1").InnerHtml;
            string companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section[1]/section[2]/div[1]/div[1]/h3[1]/span[1]/a").InnerHtml;
            string location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section[1]/section[2]/div[1]/div[1]/h3[1]/span[2]").InnerHtml;

            Console.WriteLine(jobTitle);
            Console.WriteLine(companyName);
            Console.WriteLine(location);
        }
    }
}
