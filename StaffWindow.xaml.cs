using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            List<WpfApp.Tables.Person> workerList = new List<WpfApp.Tables.Person>();
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

    }
}
