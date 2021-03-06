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
        //Column width relative to the entire table/Ширина колонок относительно всей таблицы
        private const double COL1 = 0.05;
        private const double COL2 = 0.64;
        private const double COL3 = 0.15;
        private const double COL4 = 0.16;

        private readonly char[] separators = new char[] { ';', ',' };

        public MainWindow()
        {
            InitializeComponent();
        }

        private List<StringResult> SummaryTables { get; set; } = new List<StringResult>();


        private async void Calculate_Click(object sender, RoutedEventArgs e)
        {
            SummaryTables.Clear();

            var listOfInvalidIdentifiers = new List<string>();

            //remove extra spaces and create an array of identifiers / убираем лишние пробелы и создаем массив идентификаторов
            string[] allIdentifiers = enteringIdentifiers.Text.Replace(" ", String.Empty).Split(separators, StringSplitOptions.RemoveEmptyEntries);
            //remove duplicated elements / удаляем дублированные элементы
            var identifiers = allIdentifiers.Distinct().ToArray();

            for (int i = 0; i < identifiers.Length; i++)
            {

                if (identifiers[i] is null)
                {
                    MessageBox.Show($"идентификатор под индексом {i} содержит пустое значение", "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }
                //checking for the condition that the identifier is a digit from 1 to 20 /проверка на условие что идентификатор цифра от 1 до 20
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

                SummaryTables.Add(new StringResult(identifiers[i], response.Message));
            }
            //display the table / выводим таблицу
            tableView.ItemsSource = new ObservableCollection<StringResult>(SummaryTables);

            if (listOfInvalidIdentifiers.Count > 0)
            {
                MessageBox.Show($"Данные идентификаторы либо не входят в диапозон от 1 до 20, либо не являются чилом: {String.Join(", ", listOfInvalidIdentifiers)}",
                    "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// enter press test / проверка нажатия на энтер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Calculate_Click(sender, e);
            }
        }

        /// <summary>
        /// fits the width of the columns to the window size / подгоняет ширену столбцов под размер окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
