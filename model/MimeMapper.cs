using System;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.model
{
    public interface IMimeMapper
    {
        string Map(string fileName);
    }

    internal class MimeMapper : IMimeMapper
    {
        private readonly FileExtensionContentTypeProvider contentTypeProvider;

        public MimeMapper(FileExtensionContentTypeProvider contentTypeProvider)
        {
            this.contentTypeProvider = contentTypeProvider;
        }

        public string Map(string fileName)
        {
            string contentType;
            if (!contentTypeProvider.TryGetContentType(fileName,out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
