using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp.Tables;
using WpfApp;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public StaffWindow()
        {
            InitializeComponent();
            Loaded += getValue;
        }
        private void getValue(object sender, RoutedEventArgs e)
        {
            List<Person> workerList = new List<Person>();
            using (var db = new Data.MyDbContext())
            {
                var query = from b in db.Persons select b;
                foreach (var item in query)
                {
                    workerList.Add(item);
                }
            }
            dataGrid1.ItemsSource = workerList;
        }

        private void Button_SaveExcel(object sender, RoutedEventArgs e)
        {
            new Excel().createFile();
        }
        private void Button_SaveJson(object sender, RoutedEventArgs e)
        {
            new Json().getJson();

        }
    }
}
