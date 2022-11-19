using System;
using System.Collections.Generic;
using System.Windows;
using WinForms = System.Windows.Forms;
using TestTask.HTMLGenerator.RazorPagesCompiler.model;
using TestTask.HTMLGenerator;
using System.Diagnostics;
using System.Threading.Tasks;

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

        private async Task Start(string directoryPath, string reportPath)
        {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private async void GenerateHtmlReport(object sender, RoutedEventArgs e)
        {
            GenerateHtmlButton.IsEnabled = false;
            directoryPath = ParsedDirectoryForm.Text;
            reportPath = ReportPathForm.Text;

            await Start(directoryPath, reportPath);
            GenerateHtmlButton.IsEnabled = true;
        }

        private void ChangeReportPath(object sender, RoutedEventArgs e)
        {
            var dialogFileChoise = new WinForms.FolderBrowserDialog();
            var dialogResult = dialogFileChoise.ShowDialog();

            if (dialogResult == WinForms.DialogResult.OK)
            {
               ReportPathForm.Text = dialogFileChoise.SelectedPath;
            }
        }

        private void ChangeParsedDirectoryPath(object sender, RoutedEventArgs e)
        {
            var dialogFileChoise = new WinForms.FolderBrowserDialog();
            var dialogResult = dialogFileChoise.ShowDialog();

            if (dialogResult == WinForms.DialogResult.OK)
            {
                ParsedDirectoryForm.Text = dialogFileChoise.SelectedPath;
            }
        }
    }
}
