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
using System.Data.Entity;
using System.Text.RegularExpressions;
using WpfApp.Tables;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для StaffAdd.xaml
    /// </summary>
    public partial class StaffAdd : Window

    { 
        const int reqfild = 6, nonreqfild = 1;
        String[] namereqfild = new String[reqfild] { "ID", "Фамилия", "Имя", "Дата Рождения", "Номер телефона", "Отдел" };



        public StaffAdd()
        {
            InitializeComponent();
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
        }

        private void surname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^А-Яа-яЁё]");
        }
        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^А-Яа-яЁё]");
        }
        private void patronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^А-Яа-яЁё]");
        }
        private void dtBirth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9/.] ");
        }
        private void department_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^А-Яа-яЁё]");
        }
        private void ButtonStaff_Click(object sender, RoutedEventArgs e)
        {
            String[] infreqfild = new String[reqfild] { id.Text, surname.Text, name.Text, dtBirth.Text, number.Text, department.Text };
            String[] infnonreqfild = new String[nonreqfild] { patronymic.Text };
            if (!firstValidations(infreqfild))
                return;


            infreqfild[3] = infreqfild[3].Replace('.', '-');
            DateTime date;
            if ((DateTime.TryParse(infreqfild[3], out date)))
            {
                dtBirth.ToolTip = "\"Дата рождения\" указан не верно. Данное поле заполняется по шаблону День.Месяц.Год или День-Месяц-Год";
                dtBirth.Background = Brushes.LightPink;
            }


            if ((int.TryParse(infreqfild[4], out int value))) 
            {
                number.ToolTip = ("Значение \"Номер телефона\" указано не верно.");
                dtBirth.Background = Brushes.LightPink;
                return;
            }


            using (var db = new Data.MyDbContext())
            {
                var person = new Person{Id = int.Parse(id.Text), Name = name.Text, Surname = surname.Text, Patronymic = patronymic.Text, DtBirth = dtBirth.Text, Number = number.Text, Department = department.Text };
                db.Persons.Add(person);
                db.SaveChanges();

            }
            StaffWindow staffwin = new StaffWindow();
            staffwin.Show();

        }

        private bool firstValidations(String[] infreqfild)
        {
            for (int i = 0; i < reqfild; i++)
                if (infreqfild[i] == "")
                {
                    ToolTip = ("Значение в поле \"" + namereqfild[i] + "\" должно быть заполнено");
                    Background = Brushes.LightPink;
                    return false;
                }
            return true;

        
        }

        private void Buttonesc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
