using JobPostingParser.JobBoardScrapers;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostingParser
{
    public class JobBoardFactory
    {
        public JobBoardScraperInterface getScraper(string url)
        {
            string[] urlSplit = url.Split('.');

            switch (urlSplit[1])
            {
                case "linkedin":
                    return new LinkedInScraper();
                case "indeed":
                    return new IndeedScraper();
                default:
                    return null;
            }
        }
    }
}
