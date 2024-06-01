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
    /// Логика взаимодействия для ServiceAdd.xaml
    /// </summary>
    public partial class ServiceAdd : Window
    {
        SqlConnection connection;
        public ServiceAdd(SqlConnection connection)
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

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedString = ComboBoxMasters.SelectedItem.ToString();
                string[] substring = selectedString.Split(" ");
                string code = substring[0].Trim();
                string value1 = TextBoxID.Text;
                string value2 = "W0001";
                string value3 = TextBoxName.Text;
                string value4 = TextBoxCost.Text;

                SqlCommand command = new SqlCommand("INSERT INTO Performed_Work (Work_ID, WAC_ID_FK, Repairman_ID_FK, Work_Name," +
                    " Work_Cost) VALUES (@value1, @value2, @code, @value3, @value4)", connection);

                command.Parameters.AddWithValue("@value1", value1);
                command.Parameters.AddWithValue("@value2", value2);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@value3", value3);
                command.Parameters.AddWithValue("@value4", value4);

                command.ExecuteNonQuery();

                TextBoxID.Text = "";
                TextBoxName.Text = "";
                TextBoxCost.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
