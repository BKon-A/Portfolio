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
    /// Логика взаимодействия для ClientsView.xaml
    /// </summary>
    public partial class ClientsView : Window
    {
        SqlConnection connection;
        public ClientsView(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            FillComboBox();
            
        }

        private void FillComboBox()
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_ClickShowInfo(object sender, RoutedEventArgs e)
        {

            try
            {
                string selectedString = ComboBoxClients.SelectedItem.ToString();
                string[] substring = selectedString.Split(" ");
                string code = substring[0].Trim();

                string query = "SELECT * FROM Computer_Service_Client WHERE Client_ID = @code"; // SQL запит для витягування даних

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string clientID = reader.GetString(0); // отримуємо значення стовпця Company_ID з об'єкту SqlDataReader
                    string clientUsername = reader.GetString(1); // отримуємо значення стовпця Company_Name з об'єкту SqlDataReader
                    string clientPhoneNumber = reader.GetString(2);
                    string listBoxItem = $"\nID: {clientID}\nName: {clientUsername}\nPhone number: {clientPhoneNumber}"; // формуємо рядок, який буде доданий до ComboBox
                    ListBoxClients.Items.Add(listBoxItem); // додаємо рядок до ComboBox
                }
                reader.Close(); // закриваємо об'єкт SqlDataReader
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
