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
        private void ButtonStaff_Click(object sender, RoutedEventArgs e)
        {
            String[] infreqfild = new String[reqfild] { id.Text, surname.Text, name.Text, dtBirth.Text, number.Text, department.Text };
            String[] infnonreqfild = new String[nonreqfild] { patronymic.Text };
            if (!firstValidations(infreqfild))
                return;


            infreqfild[3] = infreqfild[3].Replace('.', '-') + " 0:00:0 PM";
            DateTime date;
            if (!(DateTime.TryParse(infreqfild[3], out date)))
            {
                dtBirth.ToolTip = "\"День рождения\" указан не верно. Данное поле заполняется по шаблону День.Месяц.Год или День-Месяц-Год";
                SolidColorBrush lightPink = Brushes.LightPink;
            }

            else
            {
                dtBirth.ToolTip = "Значение в поле Дата рождения должно быть заполнено";
                dtBirth.Background = Brushes.LightPink;

            }
            
            if ((int.TryParse(infreqfild[4].Replace('+', ' '), out int value))) 
            {
                number.ToolTip = ("Значение \"Номер телефона\" указано не верно. Номер должен начинаться с +7 или 8");
                return;
            }


            using (var db = new Data.MyDbContext())
            {
                var worker = new WpfApp.Tables.Person { Name = name.Text, Surname = surname.Text, Patronymic = patronymic.Text, DtBirth = dtBirth.Text, Number = number.Text, Department = department.Text };
                db.Persons.Add(worker);
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
                    id.ToolTip = "Значение в поле ID должно быть заполнено";
                    id.Background = Brushes.LightPink;
                    name.ToolTip = "Значение в поле Имя должно быть заполнено";
                    name.Background = Brushes.LightPink;
                    surname.ToolTip = "Значение в поле Фамилия должно быть заполнено";
                    surname.Background = Brushes.LightPink;
                    department.ToolTip = "Значение в поле Отдел должно быть заполнено";
                    department.Background = Brushes.LightPink;
                    number.ToolTip = "Значение в поле Номер должно быть заполнено";
                    number.Background = Brushes.LightPink;

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
