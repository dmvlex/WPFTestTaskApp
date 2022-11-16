using System;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.model
{
    public interface INode
    {
        public DirNode ParentNode { get; set; }
        public string Name { get; set; }
    }

    public class FileNode : INode
    {
        public DirNode ParentNode { get; set; }
        public string Name { get; set; }
        public string MimeType
        {
            get
            {
                return mimeMapper.Map(Name);
            }
        }

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long Length { get; set; }

        private IMimeMapper mimeMapper = new MimeMapper(new FileExtensionContentTypeProvider());

        public FileNode(string fileName, long fileLength, DirNode parentNode)
        {
            ParentNode = parentNode;
            Name = fileName;
            Length = fileLength;
        }
    }

    public class DirNode : INode
    {
        public DirNode ParentNode { get; set; } = null;
        public string Name { get; set; }
        public List<INode> childNodes { get; set; }

        public DirNode()
        {
            childNodes = new List<INode>();
        }

    }
}
