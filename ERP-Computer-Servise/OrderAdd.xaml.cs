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
    /// Логика взаимодействия для OrderAdd.xaml
    /// </summary>
    public partial class OrderAdd : Window
    {
        SqlConnection connection;
        public OrderAdd(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            FillComboBox();
        }

        public void FillComboBox()
        {
            try
            {
                string query = "SELECT Computer_Service_Client.Client_ID, Computer_Service_Client.Client_Name FROM Computer_Service_Client";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string clientID = reader.GetString(0); // отримуємо значення стовпця Company_ID з об'єкту SqlDataReader
                    string clientUsername = reader.GetString(1); // отримуємо значення стовпця Company_Name з об'єкту SqlDataReader
                    string comboBoxItem = $"{clientID}{clientUsername}"; // формуємо рядок, який буде доданий до ComboBox
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
                string value1 = TextBoxDeviceID.Text;
                string value2 = TextBoxModel.Text;
                string value3 = TextBoxTrouble.Text;

                SqlCommand command = new SqlCommand("INSERT INTO Broken_Device (Device_ID, Client_ID_FK, Device_Model, Trouble) VALUES (@value1, @code, @value2, @value3)", connection);

                command.Parameters.AddWithValue("@value1", value1);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@value2", value2);
                command.Parameters.AddWithValue("@value3", value3);

                command.ExecuteNonQuery();

                TextBoxDeviceID.Text = "";
                TextBoxModel.Text = "";
                TextBoxTrouble.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
