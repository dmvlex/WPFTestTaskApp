using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DirMimeTypeParser.HTMLGenerator.RazorPagesCompiler.model;
using DirMimeTypeParser.HTMLGenerator;
using System.Diagnostics;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string directoryPath = Environment.CurrentDirectory;
        private string reportPath = Environment.CurrentDirectory;

        public MainWindow()
        {
            InitializeComponent();
            ParsedDirectoryForm.Text = directoryPath;
            ReportPathForm.Text = reportPath;
        }

        private async void GenerateHtmlReport(object sender, RoutedEventArgs e)
        {
            directoryPath = ReportPathForm.Text;
            reportPath = ParsedDirectoryForm.Text;
            try
            {
                List<string> logs = new List<string>();
                ReportModel reportModel = new ReportModel(directoryPath, out logs);
                HtmlReportGenerator<ReportModel> reportGenerator = new HtmlReportGenerator<ReportModel>(reportPath, reportModel);
                await reportGenerator.Generate();
                MessageBox.Show("Файл успешно сгенерирован", "Успех!");

                var result = MessageBox.Show("Хотите открыть папку с отчетом?", "Открытие папки", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Process.Start("explorer.exe", reportPath);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
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
