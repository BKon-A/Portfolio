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
    /// Логика взаимодействия для ServiceEdit.xaml
    /// </summary>
    public partial class ServiceEdit : Window
    {
        SqlConnection connection;
        public ServiceEdit(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            FillComboBox();
            FillComboBoxServises();
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

        private void FillComboBoxServises()
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedString = ComboBoxMasters.SelectedItem.ToString();
                string[] substring = selectedString.Split(" ");
                string masterCode = substring[0].Trim();
                string selectedStr = ComboBoxServises.SelectedItem.ToString();
                string[] substr = selectedStr.Split(" ");
                string code = substr[0].Trim();
                string name = TextBoxName.Text;
                float cost = Convert.ToSingle(TextBoxCost.Text);

                string query = "UPDATE Performed_Work SET Repairman_ID_FK = @masterCode, Work_Name = @name, Work_Cost = @cost WHERE Work_ID = @code"; // SQL запит для витягування даних

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@masterCode", masterCode);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@cost", cost);
                command.Parameters.AddWithValue("@code", code);

                command.ExecuteNonQuery();

                TextBoxName.Text = "";
                TextBoxCost.Text = "";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxFill()
        {
            try
            {
                string selectedString = ComboBoxServises.SelectedItem.ToString();
                string[] substring = selectedString.Split(" ");
                string code = substring[0].Trim();
                string query = "SELECT * FROM Performed_Work WHERE Work_ID = @code";
                string name = "";
                float cost = 0;

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    name = reader.GetString(3);
                    cost = reader.GetFloat(4);
                }
                reader.Close();
                TextBoxName.Text = name;
                TextBoxCost.Text = cost.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ComboBoxMasters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBoxFill();
        }
    }
}