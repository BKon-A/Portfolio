using Microsoft.Data.SqlClient;
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

namespace DBO_AWP_Computer_Service_Director
{
    /// <summary>
    /// Логика взаимодействия для ServiceView.xaml
    /// </summary>
    public partial class ServiceView : Window
    {
        SqlConnection connection;
        public ServiceView(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            FillComboBox();
        }

        private void FillComboBox()
        {
            try
            {
                string query = "SELECT * FROM Performed_Work";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string ID = reader.GetString(0); // отримуємо значення стовпця Company_ID з об'єкту SqlDataReader
                    string name = reader.GetString(3); // отримуємо значення стовпця Company_Name з об'єкту SqlDataReader
                    string comboBoxItem = $"{ID}{name}"; // формуємо рядок, який буде доданий до ComboBox
                    ComboBoxServises.Items.Add(comboBoxItem); // додаємо рядок до ComboBox
                }
                reader.Close(); // закриваємо об'єкт SqlDataReader
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_ClickShowInfo(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedString = ComboBoxServises.SelectedItem.ToString();
                string[] substring = selectedString.Split(" ");
                string code = substring[0].Trim();

                string query = "SELECT * FROM Performed_Work WHERE Work_ID = @code"; // SQL запит для витягування даних

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string ID = reader.GetString(0);
                    string repairman = reader.GetString(2);
                    string name = reader.GetString(3);
                    float cost = reader.GetFloat(4);
                    string listBoxItem = $"\nID: {ID}\nID майстра: {repairman}\nНазва робіт: {name}\nВартість: {cost}"; // формуємо рядок, який буде доданий до ComboBox
                    ListBoxServises.Items.Add(listBoxItem); // додаємо рядок до ComboBox
                }
                reader.Close(); // закриваємо об'єкт SqlDataReader
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
