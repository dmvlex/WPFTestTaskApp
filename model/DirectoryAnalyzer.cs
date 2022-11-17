using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestTask.model
{
    internal class DirectoryAnalyzer
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

        private List<string> logs = new List<string>();
        private string directoryPath;
        
        //path - путь к директории для парса
        public DirectoryAnalyzer(string path)
        {
            DirectoryPath = path;
        }
        public DirNode GetParsedData(out List<string> errorLogs)
        {
            DirNode rootNode = new DirNode();
            DirectoryInfo rootDirInfo = new DirectoryInfo(DirectoryPath);
            ParseDirectoryTree(rootDirInfo, rootNode);
            errorLogs = logs;
            return rootNode;
        }
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
                    // Resursive call for each subdirectory.
                    ParseDirectoryTree(dirInfo, subDirNode);
                }         
        }
    } 
}