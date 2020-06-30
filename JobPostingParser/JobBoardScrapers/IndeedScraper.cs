using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostingParser.JobBoardScrapers
{
    public class IndeedScraper : JobBoardScraperInterface
    {
        public void ExtractAndPrintJobDetails(string url)
        {
            string jobTitle = "";
            string companyName = "";
            string location = "";

            string[] infos = url.Split('=');
            string jobId = infos[8];
            string baseURL = "https://www.indeed.com/viewjob?jk=" + jobId;
            var web = new HtmlWeb();
            var htmlDoc = web.Load(baseURL);

            
            try
            {
                var rating = htmlDoc.DocumentNode.SelectSingleNode(
                "/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[2]/div/a/div[2]"); // Selects Review
                var banner = htmlDoc.DocumentNode.SelectSingleNode(
                    "/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[1]/img[1]"); // Selects banner
                if (rating != null && banner != null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[2]/h3").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[1]/a").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[4]").InnerText;
                    Console.WriteLine("1");
                }
                else if (rating == null && banner != null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[2]/h3").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[1]").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[3]").InnerText;
                    Console.WriteLine("2");
                }
                else if (rating != null && banner == null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[1]/h3").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[2]/div/div/div[1]/a").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[2]/div/div/div[4]").InnerText;
                    Console.WriteLine("3");

                }
                else if (rating == null && banner == null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[1]/h3").InnerHtml;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[2]/div/div/div[1]").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[2]/div/div/div[3]").InnerHtml;
                    Console.WriteLine("4");
                }
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Job Title: " + jobTitle);
                Console.WriteLine("Company Name: " + companyName);
                Console.WriteLine("Location: " + location);
            } catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
