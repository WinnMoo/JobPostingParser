﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobPostingParser.JobBoardScrapers
{
    public class IndeedScraper : JobBoardScraperInterface
    {
        const string BASE_JOB_URL = "https://www.indeed.com/viewjob?jk=";
        const string BASE_JOB_URL_ALT = "https://www.indeed.com/viewjob?cmp=";
        const string BASE_JOB_SEARCH_URL = "https://www.indeed.com/jobs";
        public void ExtractAndPrintJobDetails(string url)
        {
            string urlToScrapeFrom = "";
            string jobTitle = "";
            string companyName = "";
            string location = "";
            string description = "";
            if (url.Contains(BASE_JOB_SEARCH_URL)) // Link is from search page
            {
                string jobId = "";
                string[] queries = url.Split('&');
                foreach(var query in queries)
                {
                    if (query.Contains("vjk"))
                    {
                        jobId = query.Substring(query.Length - 16);
                    }
                }
                urlToScrapeFrom = BASE_JOB_URL + jobId;
            } 
            else if (url.Contains(BASE_JOB_URL))// Link is direct link to job posting with extra information in URL
            {
                urlToScrapeFrom = url.Substring(0, 50);
            } 
            else if(url.Contains(BASE_JOB_URL_ALT)) // Link is direct link to job posting with extra information in URL, but contains alternative query parameters
            {
                string jobId = "";
                string[] queries = url.Split('&');
                foreach (var query in queries)
                {
                    if (query.Contains("jk"))
                    {
                        jobId = query.Substring(query.Length - 16);
                    }
                }
                urlToScrapeFrom = BASE_JOB_URL + jobId;
            }

            var web = new HtmlWeb();
            var htmlDoc = web.Load(urlToScrapeFrom);

            Console.WriteLine("URL to scrape from: " + urlToScrapeFrom);
            try
            {
                var rating = htmlDoc.DocumentNode.SelectSingleNode(
                "/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[2]/div/a"); // Selects Review
                var banner = htmlDoc.DocumentNode.SelectSingleNode(
                    "/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/img[1]"); // Selects banner
                if (rating != null && banner != null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[2]/h3").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[1]/a").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[4]").InnerText;
                    Console.WriteLine("1");
                }
                else if (rating != null && banner == null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[1]").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[1]/a").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[4]").InnerText;
                    Console.WriteLine("2");
                }
                else if (rating == null && banner != null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[4]/div[1]/h1").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[4]/div[2]/div/div/div[1]/div[1]").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[4]/div[2]/div/div/div[1]/div[3]").InnerText;
                    Console.WriteLine("3");
                }
                else if (rating == null && banner == null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[1]/h1").InnerHtml;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[1]").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[3]").InnerHtml;
                    Console.WriteLine("4");
                }
                string city;
                string state;
                string zipCode;
                string locationToParse = location;
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Job Title: " + jobTitle);
                Console.WriteLine("Company Name: " + companyName);
                Console.WriteLine("Location: " + location);
                if (!location.Equals("-"))
                {
                    var splitByComma = locationToParse.Split(",");
                    city = splitByComma[0];
                    Console.WriteLine("City: " + city);
                    location = splitByComma[1];
                    location = location.Trim();
                    var splitLocation = location.Split(" ");
                    if(splitLocation.Length == 1)
                    {
                        state = splitLocation[0];
                        Console.WriteLine("State: " + state);
                    } else if(splitLocation.Length == 2)
                    {
                        state = splitLocation[0];
                        Console.WriteLine("State: " + state);
                        zipCode = splitLocation[1];
                        Console.WriteLine("ZipCode " + zipCode);
                    }
                }
            } catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
