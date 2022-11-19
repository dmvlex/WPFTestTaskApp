using Microsoft.AspNetCore.StaticFiles;
using System;
using MimeMapping;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirMimeTypeParser.DirectoryDataParser
{
    public interface INode
    {
        public DirNode ParentNode { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Реализует узел дерева, содержащий файл
    /// </summary>
    public class FileNode : INode
    {
        public DirNode ParentNode { get; set; }
        public string Name { get; set; }
        public string MimeType
        {
            get
            {
                return MimeMapping.MimeUtility.GetMimeMapping(Name);
            }
        }

        /// <summary>
        /// Размер файла в байтах
        /// </summary>
        public long Length { get; set; }

        public FileNode(string fileName, long fileLength, DirNode parentNode)
        {
            ParentNode = parentNode;
            Name = fileName;
            Length = fileLength;
        }
    }

    /// <summary>
    /// Реализует узел дерева, содержащий папку
    /// </summary>
    public class DirNode : INode
    {
        ///Все начинается с папки, потому корневой узел дерева должен иметь этот тип
        public DirNode ParentNode { get; set; } = null;
        public string Name { get; set; }
        public List<INode> childNodes { get; set; }

        public DirNode()
        {
            childNodes = new List<INode>();
        }

    }
}
