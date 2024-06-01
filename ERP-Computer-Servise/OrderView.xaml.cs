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
    /// Логика взаимодействия для OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        SqlConnection connection;
        public OrderView(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            FillComboBox();
        }

        public void FillComboBox()
        {
            try
            {
                string query = "SELECT Broken_Device.Device_ID, Computer_Service_Client.Client_Name\r\nFROM Broken_Device\r\n" +
                    "INNER JOIN Computer_Service_Client ON Broken_Device.Client_ID_FK = Computer_Service_Client.Client_ID;";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string deviceID = reader.GetString(0); // отримуємо значення стовпця Company_ID з об'єкту SqlDataReader
                    string clientUsername = reader.GetString(1); // отримуємо значення стовпця Company_Name з об'єкту SqlDataReader
                    string comboBoxItem = $"{deviceID}{clientUsername}"; // формуємо рядок, який буде доданий до ComboBox
                    ComboBoxClients.Items.Add(comboBoxItem); // додаємо рядок до ComboBox
                }
                reader.Close(); // закриваємо об'єкт SqlDataReader
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedString = ComboBoxClients.SelectedItem.ToString();
                string[] substring = selectedString.Split(" ");
                string code = substring[0].Trim();

                string query = "SELECT * FROM Broken_Device WHERE Device_ID = @code"; // SQL запит для витягування даних

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string deviceID = reader.GetString(0); // отримуємо значення стовпця Company_ID з об'єкту SqlDataReader
                    string clientUsername = reader.GetString(1); // отримуємо значення стовпця Company_Name з об'єкту SqlDataReader
                    string deviceModel = reader.GetString(2);
                    string deviceTrouble = reader.GetString(3);
                    string listBoxItem = $"\nЗамовлення№: {deviceID}\nПрізвище. Ім'я: {clientUsername}\nМодель: {deviceModel}\nДеталі поломки: {deviceTrouble}"; // формуємо рядок, який буде доданий до ComboBox
                    ListBoxOrders.Items.Add(listBoxItem); // додаємо рядок до ComboBox
                }
                reader.Close(); // закриваємо об'єкт SqlDataReader
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
