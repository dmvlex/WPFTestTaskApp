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

        private string directoryPath;

        public void GetData()
        {

        }
        //path - путь к директории для парса
        public DirectoryAnalyzer(string path)
        {
            DirectoryPath = path;
        }

        
        private static void WalkDirectoryTree(DirectoryInfo root, DirNode rootNode)
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

            }
            catch (DirectoryNotFoundException e)
            {
                
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    rootNode.childNodes.Add(new FileNode(fi.Name,fi.Length,rootNode));
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();


                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo,subDirNode);
                }
            }
        }
    } 
}
