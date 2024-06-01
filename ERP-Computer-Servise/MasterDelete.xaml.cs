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
    /// Логика взаимодействия для MasterDelete.xaml
    /// </summary>
    public partial class MasterDelete : Window
    {
        SqlConnection connection;
        public MasterDelete(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            FillComboBox();
        }

        public void FillComboBox()
        {
            try
            {
                string query = "SELECT * FROM Repairman";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader(); // виконуємо запит та отримуємо результат у вигляді об'єкту SqlDataReader
                while (reader.Read())
                {
                    string masterID = reader.GetString(0); // отримуємо значення стовпця Company_ID з об'єкту SqlDataReader
                    string name = reader.GetString(2); // отримуємо значення стовпця Company_Name з об'єкту SqlDataReader
                    string comboBoxItem = $"{masterID}{name}"; // формуємо рядок, який буде доданий до ComboBox
                    ComboBoxMasters.Items.Add(comboBoxItem); // додаємо рядок до ComboBox
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
            string selectedString = ComboBoxMasters.SelectedItem.ToString();
            string[] substring = selectedString.Split(" ");
            string code = substring[0].Trim();

            string query = "DELETE FROM Repairman WHERE Repairman_ID = @code";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@code", code);
            command.ExecuteNonQuery();
        }
    }
}
