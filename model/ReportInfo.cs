using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTask.model
{
    internal class ParsedFilesTypeInfo
    {
        public string MimeTypeName { get; set; }
        public int Num { get; set; } = 0;
        public double PercentNum { get; set;}
        public double AverageTypeLength { get => Math.Round(TypeFilesLengths.Average(),2); }

        public List<long> TypeFilesLengths = new List<long>();
    }

    public class ReportInfo
    {
        public int NumOfTypes
        {
            get => parsedFilesTypesInfo.Count;
        }

        private DirNode parsedDirTree;

        private List<ParsedFilesTypeInfo> parsedFilesTypesInfo = new List<ParsedFilesTypeInfo>();

        public ReportInfo(DirNode parsedDirectoryTree)
        {
            parsedDirTree = parsedDirectoryTree;
            GetFilesTypesInfo(parsedDirTree);
            GetPercentNumsOfTypes();
        }

        private void GetFilesTypesInfo(DirNode rootNode)
        {
            foreach (var node in rootNode.childNodes)
            {
                if (node is FileNode)
                {
                    FileNode currentFile = (FileNode)node;

                    if (parsedFilesTypesInfo.Exists(x => x.MimeTypeName == currentFile.MimeType))
                    {
                        var findedType = parsedFilesTypesInfo.Find(x => x.MimeTypeName == currentFile.MimeType);
                        findedType.Num++;
                        findedType.TypeFilesLengths.Add(currentFile.Length);
                    }
                    else
                    {
                        var newType = new ParsedFilesTypeInfo()
                        {
                            MimeTypeName = currentFile.MimeType,
                            Num = 1,
                        };

                        newType.TypeFilesLengths.Add(currentFile.Length);

                        parsedFilesTypesInfo.Add(newType);
                    }

                }
                else if (node is DirNode)
                {
                    DirNode nextNode = (DirNode)node;
                    GetFilesTypesInfo(nextNode);
                }
            }
        }

        private int GetTotalFilesNum()
        {
            int total = 0;
            foreach (var file in parsedFilesTypesInfo)
            {
                total += file.Num;
            }
            return total;
        }

        private void GetPercentNumsOfTypes()
        {
            foreach (var type in parsedFilesTypesInfo)
            {
                type.PercentNum = Math.Round(((double)type.Num / (double)GetTotalFilesNum()) * 100,2);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in parsedFilesTypesInfo)
            {
                stringBuilder.Append($"Type:{item.MimeTypeName}\nNum:{item.Num}\nAverage:{item.AverageTypeLength}\n{item.PercentNum}%\n\n");
            }

            return stringBuilder.ToString();
        }

    }
}
