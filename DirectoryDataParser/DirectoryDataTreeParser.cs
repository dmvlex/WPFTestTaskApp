using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DirMimeTypeParser.DirectoryDataParser;
using System.Text;
using System.Threading.Tasks;

namespace DirMimeTypeParser.DirectoryDataParser
{
    /// <summary>
    /// Собирает всю необходимую информацию из директории и сохраняет в виде дерева
    /// </summary>
    public class DirectoryDataTreeParser
    {
        public string DirectoryPath
        {
            get => directoryPath;
            set
            {
                if (!string.IsNullOrEmpty(value) || Directory.Exists(value))
                {
                    directoryPath = Path.GetFullPath(value);
                }
                else
                {
                    directoryPath = Environment.CurrentDirectory;
                }
            }
        }


        private List<string> logs = new List<string>(); //логи ошибок при анализе директории
        private string directoryPath;

        public DirectoryDataTreeParser(string path)
        {
            DirectoryPath = path;
        }

        /// <summary>
        /// Собирает информацию для конечного отчета и сохраняет в виде дерева
        /// </summary>
        /// <param name="errorLogs">Сообщения об ошибках возникших в ходе сбора информации</param>
        /// <returns>Дерево с информацией</returns>
        public DirNode GetParsedData(out List<string> errorLogs)
        {
            DirNode rootNode = new DirNode();
            DirectoryInfo rootDirInfo = new DirectoryInfo(DirectoryPath);
            ParseDirectoryTree(rootDirInfo, rootNode);
            errorLogs = logs;
            return rootNode;
        }

        /// <summary>
        /// Собирает информацию для конечного отчета и сохраняет в виде дерева
        /// </summary>
        /// <param name="root">Информация о корневой папке</param>
        /// <param name="rootNode">Корневой узел дерева</param>
        private void ParseDirectoryTree(DirectoryInfo root, DirNode rootNode)
        {
            DirectoryInfo[] subDirs = null;
            FileInfo[] files = null;

            rootNode.Name = root.Name;
            DirNode subDirNode = new DirNode() { ParentNode = rootNode };
            rootNode.childNodes.Add(subDirNode);

            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                logs.Add(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                logs.Add(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    rootNode.childNodes.Add(new FileNode(file.Name, file.Length, rootNode));
                }
            }
            subDirs = root.GetDirectories();

            foreach (DirectoryInfo dirInfo in subDirs)
            {
                
                ParseDirectoryTree(dirInfo, subDirNode);
            }
        }


    }
}
