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
    /// Логика взаимодействия для MasterView.xaml
    /// </summary>
    public partial class MasterView : Window
    {
        SqlConnection connection;
        public MasterView(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            FillComboBox();
        }

        private void FillComboBox()
        {
            try
            {
                string query = "SELECT Repairman_ID, Repairman_Name FROM Repairman";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string masterID = reader.GetString(0); // отримуємо значення стовпця Company_ID з об'єкту SqlDataReader
                    string masterName = reader.GetString(1); // отримуємо значення стовпця Company_Name з об'єкту SqlDataReader
                    string comboBoxItem = $"{masterID}{masterName}"; // формуємо рядок, який буде доданий до ComboBox
                    ComboBoxMasters.Items.Add(comboBoxItem); // додаємо рядок до ComboBox
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
                string selectedString = ComboBoxMasters.SelectedItem.ToString();
                string[] substring = selectedString.Split(" ");
                string code = substring[0].Trim();

                string query = "SELECT * FROM Repairman WHERE Repairman_ID = @code"; // SQL запит для витягування даних

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string ID = reader.GetString(0);
                    string name = reader.GetString(2);
                    string title = reader.GetString(3);
                    int seniority = reader.GetInt32(4);
                    float reputarion = reader.GetFloat(5);
                    float salary = reader.GetFloat(6);
                    string listBoxItem = $"\nID: {ID}\nПрізвище, ім'я: {name}\nПосада: {title}\nСтаж: {seniority}" +
                        $"\nРепутація: {reputarion}\nЗаробітня плата: {salary}"; // формуємо рядок, який буде доданий до ComboBox
                    ListBoxMasters.Items.Add(listBoxItem); // додаємо рядок до ComboBox
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
