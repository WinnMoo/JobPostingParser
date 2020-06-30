using HtmlAgilityPack;
using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace JobPostingParser
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Paste the link to the job posting from LinkedIn or Indeed.");
                var inputURL = Console.ReadLine();
                JobBoardFactory jbf = new JobBoardFactory();
                var scraper = jbf.getScraper(inputURL); // Selects the proper scraper
                scraper.ExtractAndPrintJobDetails(inputURL); // Prints the job details
            }
        }
    }
}
