using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.StaticFiles;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestTask.model;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateHtmlReport(object sender, RoutedEventArgs e)
        {
            List<string> errorLogs = new List<string>();
            StringBuilder errorsList = new StringBuilder();
            DirectoryAnalyzer directoryAnalyzer = new DirectoryAnalyzer(@"E:\OTHER\PROGRAMMING\Projects\TETS");
            DirNode parsedData = directoryAnalyzer.GetParsedData(out errorLogs);

            if (errorLogs != null)
            {
                foreach (var item in errorLogs)
                {
                    errorsList.Append($"{item}\n");
                }

                MessageBox.Show(errorsList.ToString());
            }
            
        }

        private void ChangeReportPath(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeParsedDirectoryPath(object sender, RoutedEventArgs e)
        {

        }
    }
}
