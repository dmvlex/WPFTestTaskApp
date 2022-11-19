using System;
using DirMimeTypeParser.HTMLGenerator.RazorPagesCompiler;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirMimeTypeParser.HTMLGenerator
{
    
    public interface IHtmlGenerator
    {
        public Task Generate();
    }


    public class HtmlReportGenerator<T> : IHtmlGenerator 
        where T : class 
    {
        public string ReportPath { get; set; }
        private T viewModel;

        public HtmlReportGenerator(string reportPath,T model)
        {
            ReportPath = reportPath;
            viewModel = model;
        }

        public async Task Generate()
        {
            await Generate(ReportPath);
        }

        private async Task Generate(string reportDocPath)
        {
            RazorCompiler<T> razorCompiler = new RazorCompiler<T>(viewModel);

            var htmlPage = await razorCompiler.Compile();

            File.WriteAllText(Path.Combine(reportDocPath,$"report{DateTime.Now.ToString("yyyyMMddhhmmss")}.html"),htmlPage);
        }
    }
}
