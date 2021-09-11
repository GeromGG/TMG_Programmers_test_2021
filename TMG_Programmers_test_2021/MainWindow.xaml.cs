using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TMG_Programmers_test_2021.Model;

namespace TMG_Programmers_test_2021
{
    public partial class MainWindow : Window
    {
        //Ширина колонок относительно всей таблицы
        private const double COL1 = 0.05;
        private const double COL2 = 0.64;
        private const double COL3 = 0.15;
        private const double COL4 = 0.16;

        private readonly char[] separators = new char[] { ';', ',' };

        public MainWindow()
        {
            InitializeComponent();
        }

        private List<SummaryTable> SummaryTables { get; set; } = new List<SummaryTable>();

        private async void Calculate_Click(object sender, RoutedEventArgs e)
        {
            SummaryTables.Clear();

            var listOfInvalidIdentifiers = new List<string>();

            string[] allIdentifiers = enteringIdentifiers.Text.Replace(" ", String.Empty).Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var identifiers = allIdentifiers.Distinct().ToArray();

            for (int i = 0; i < identifiers.Length; i++)
            {
                if (identifiers[i] is null)
                {
                    MessageBox.Show($"идентификатор под индексом {i} содержит пустое значение", "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }

                if (!Int32.TryParse(identifiers[i], out var number) || number > 20 || number <= 0)
                {
                    listOfInvalidIdentifiers.Add(identifiers[i]);
                    continue;
                }

                var response = await RequestService.HttpGetRequestAsync(identifiers[i]);
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
                MessageBox.Show($"Данные идентификаторы либо не входят в диапозон от 1 до 20, либо не являются чилом: {String.Join(", ", listOfInvalidIdentifiers)}",
                    "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Calculate_Click(sender, e);
            }
        }

        private void ProductsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            GridView gView = (GridView)listView.View;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;

            gView.Columns[0].Width = workingWidth * COL1;
            gView.Columns[1].Width = workingWidth * COL2;
            gView.Columns[2].Width = workingWidth * COL3;
            gView.Columns[3].Width = workingWidth * COL4;
        }
    }
}
