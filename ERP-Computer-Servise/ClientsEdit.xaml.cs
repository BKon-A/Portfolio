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
    /// Логика взаимодействия для ClientsEdit.xaml
    /// </summary>
    public partial class ClientsEdit : Window
    {
        SqlConnection connection;
        public ClientsEdit(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            
            FillTextBox();
        }

        private void FillTextBox()
        {
            try
            {
                string query = "SELECT * FROM Computer_Service_Client";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string clientID = reader.GetString(0); // отримуємо значення стовпця Company_ID з об'єкту SqlDataReader
                    string clientUsername = reader.GetString(1); // отримуємо значення стовпця Company_Name з об'єкту SqlDataReader
                    string phoneNumber = reader.GetString(2);
                    string comboBoxItem = $"|{clientID}|{clientUsername}|{phoneNumber}|"; // формуємо рядок, який буде доданий до ComboBox
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
                string[] substring = selectedString.Split("|");
                string code = substring[1].Trim();
                string phone = Phone.Text;

                string query = "UPDATE Computer_Service_Client SET Phone_Number = @phone WHERE Client_ID = @code";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@phone", phone);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBoxClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxClients.SelectedItem != null)
            {
                string selectedText = ComboBoxClients.SelectedItem.ToString();
                string[] substring = selectedText.Split("|");
                Phone.Text = substring[3].Trim();
            }
        }
    }
}
