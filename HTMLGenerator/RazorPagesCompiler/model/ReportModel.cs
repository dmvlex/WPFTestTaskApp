using DirMimeTypeParser.DirectoryDataParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirMimeTypeParser.HTMLGenerator.RazorPagesCompiler.model
{
    public class ParsedMimeTypesInfo
    {
        public string Name { get; set; }
        public int Num { get; set; } = 0;
        public double PercentNum { get; set; }

        /// <summary>
        /// Средний вес файла этого значение в байтах
        /// </summary>
        public double AverageTypeLength { get => Math.Round(TypeFilesLengths.Average(), 2);}

        /// <summary>
        /// Размеры всех файлов данного типа
        /// </summary>
        public List<long> TypeFilesLengths = new List<long>();
    }

    public class ReportModel
    {
        /// <summary>
        /// Вся информация о Mime-типах всех файлов директории в виде списка
        /// </summary>
        public List<ParsedMimeTypesInfo> ParsedMimeTypes { get; set; }
        public string RootFolderFullPath { get; set;}

        private DirNode parsedDirTree;

        public ReportModel(string dirPath,out List<string> logs)
        {
            RootFolderFullPath = dirPath;
            this.parsedDirTree = new DirectoryDataTreeParser(dirPath).GetParsedData(out logs);
            ParsedMimeTypes = new List<ParsedMimeTypesInfo>();
            GetMimeTypesInfo(parsedDirTree);
        }

        public ReportModel(DirNode parsedDirTree)
        {
            RootFolderFullPath = Path.GetFullPath(parsedDirTree.Name);
            this.parsedDirTree = parsedDirTree;
            ParsedMimeTypes = new List<ParsedMimeTypesInfo>();
            GetMimeTypesInfo(parsedDirTree);

        }

        /// <summary>
        /// Достает из дерева нужную информацию о Mime-типах фалов
        /// </summary>
        /// <param name="rootNode">Дерево с информацией о директории</param>
        private void GetMimeTypesInfo(DirNode rootNode)
        {
            foreach (var node in rootNode.childNodes)
            {
                if (node is FileNode)
                {
                    FileNode currentFile = (FileNode)node;

                    if (ParsedMimeTypes.Exists(x => x.Name == currentFile.MimeType))
                    {
                        var findedType = ParsedMimeTypes.Find(x => x.Name == currentFile.MimeType);
                        findedType.Num++;
                        findedType.TypeFilesLengths.Add(currentFile.Length);
                    }
                    else
                    {
                        var newType = new ParsedMimeTypesInfo()
                        {
                            Name = currentFile.MimeType,
                            Num = 1,
                        };

                        newType.TypeFilesLengths.Add(currentFile.Length);

                        ParsedMimeTypes.Add(newType);
                    }

                }
                else if (node is DirNode)
                {
                    DirNode nextNode = (DirNode)node;
                    GetMimeTypesInfo(nextNode);
                }
            }

            //Сортируем типы алфавитном порядке
            ParsedMimeTypes.Sort((x, y) => String.Compare(x.Name, y.Name));

            //Считаем кол-во файлов каждого типа в процентном соотношении
            GetPercentNumsOfTypes();
        }

        //Получаем общее кол-во файлов 
        private int GetTotalFilesNum()
        {
            int total = 0;
            foreach (var type in ParsedMimeTypes)
            {
                total += type.Num;
            }
            return total;
        }

        private void GetPercentNumsOfTypes()
        {
            foreach (var type in ParsedMimeTypes)
            {
                type.PercentNum = Math.Round(((double)type.Num / (double)GetTotalFilesNum()) * 100, 2);
            }
        }
    }
}
