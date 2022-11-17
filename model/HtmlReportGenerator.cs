using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.model
{
    public interface IReportGenerator
    {
        void Generate(string path);
    }

    internal class HtmlReportGenerator : IReportGenerator
    {

        public void Generate(string path)
        {

        }

        //PATH - ПУТЬ К ОТЧЕТУ
        private void Generate(string reportPath, ReportInfo parsedDirectoryTree)
        {
            
        }



    }
}
