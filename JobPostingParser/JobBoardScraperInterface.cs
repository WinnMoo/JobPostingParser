using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostingParser
{
    public interface JobBoardScraperInterface
    {
        void ExtractAndPrintJobDetails(string url);
    }
}
