using Microsoft.Data.SqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DBO_AWP_Computer_Service_Director
{
    /// <summary>
    /// Логика взаимодействия для MasterEdit.xaml
    /// </summary>
    public partial class MasterEdit : Window
    {
        SqlConnection connection;
        public MasterEdit(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            JobTitleFill();
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

        private void JobTitleFill()
        {
            try
            {
                ComboBoxTitles.Items.Add("Помічник майстера");
                ComboBoxTitles.Items.Add("Молодший майстер");
                ComboBoxTitles.Items.Add("Майстер");
                ComboBoxTitles.Items.Add("Старший майстер");
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
                string code = substring[0].Trim();
                string title = ComboBoxTitles.SelectedItem.ToString();
                float seniority = Convert.ToSingle(TextBoxSeniority.Text);
                int reputation = Convert.ToInt32(TextBoxReputation.Text);
                float salary = Convert.ToSingle(TextBoxSalary.Text);

                string query = "UPDATE Repairman SET Job_Title = @title, Seniority = @seniority, Repairman_Reputation = @reputation," +
                    " Repairman_Salary = @salary WHERE Repairman_ID = @code"; // SQL запит для витягування даних

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@seniority", seniority);
                command.Parameters.AddWithValue("@reputation", reputation);
                command.Parameters.AddWithValue("@salary", salary);
                command.ExecuteNonQuery();

                TextBoxReputation.Text = "";
                TextBoxSalary.Text = "";
                TextBoxSeniority.Text = "";
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
                string selectedString = ComboBoxMasters.SelectedItem.ToString();
                string[] substring = selectedString.Split(" ");
                string code = substring[0].Trim();
                string query = "SELECT * FROM Repairman WHERE Repairman_ID = @code";
                int seniority = 0;
                float salary = 0, reputation = 0;

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    seniority = reader.GetInt32(4);
                    reputation = reader.GetFloat(5);
                    salary = reader.GetFloat(6);
                }
                reader.Close();
                TextBoxReputation.Text = reputation.ToString();
                TextBoxSalary.Text = salary.ToString();
                TextBoxSeniority.Text = seniority.ToString();
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
