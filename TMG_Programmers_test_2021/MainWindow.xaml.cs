using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMG_Programmers_test_2021.Model;

namespace TMG_Programmers_test_2021
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

        private List<SummaryTable> SummaryTables { get; set; } = new List<SummaryTable>();

        private async void Calculate_Click(object sender, RoutedEventArgs e)
        {
            SummaryTables.Clear();

            var listOfInvalidIdentifiers = new List<string>();

            char[] separators = new char[] {';', ','};

            string[] allIdentifiers = enteringIdentifiers.Text.Replace(" ", String.Empty).Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var identifiers = allIdentifiers.Distinct().ToArray();

            for (int i = 0; i < identifiers.Length; i++)
            {
                if (!Int32.TryParse(identifiers[i], out var number) || number > 20 || number <= 0)
                {
                    listOfInvalidIdentifiers.Add(identifiers[i]);
                    continue;
                }

                var response = await RequestService.HttpGetRequest(identifiers[i]);

                if (response.IsError)
                {
                    MessageBox.Show(response.MessageError, "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }

                SummaryTables.Add(new SummaryTable(identifiers[i], response.Message));
            }
            tableView.ItemsSource = new ObservableCollection<SummaryTable>(SummaryTables);
            if (listOfInvalidIdentifiers.Count > 0)
            {
                MessageBox.Show($"Данные идентификаторы либо не входят в диапозон от 1 до 20, либо не являются чилом: {String.Join(", ", listOfInvalidIdentifiers)}", "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            var col1 = 0.05;
            var col2 = 0.64;
            var col3 = 0.15;
            var col4 = 0.16;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
        }
    }
}
