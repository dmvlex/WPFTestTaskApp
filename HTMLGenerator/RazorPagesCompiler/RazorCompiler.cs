using System;
using RazorLight;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirMimeTypeParser.HTMLGenerator.RazorPagesCompiler
{
    public class RazorCompiler<T> where T : class
    {
        private static RazorLightEngine engine;
        private T viewModel;

        public RazorCompiler(T model)
        {
            engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(Assembly.GetExecutingAssembly(), "DirMimeTypeParser.ReportTemplate")
                .UseMemoryCachingProvider()
                .Build();

            viewModel = model;
        }

        public async Task<string> Compile()
        {
            string html = await engine.CompileRenderAsync("ReportTemplate.cshtml", viewModel);

            return html;
        }
    }
}
