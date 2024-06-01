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
    /// Логика взаимодействия для ResourcesEmployees.xaml
    /// </summary>
    public partial class MasterAdd : Window
    {
        SqlConnection connection;
        public MasterAdd(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            JobTitleFill();
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

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                string value1 = TextBoxMasterID.Text;
                string value2 = "CB0001";
                string value3 = TextBoxMasterName.Text;
                string value4 = ComboBoxTitles.SelectedItem.ToString();
                string value5 = TextBoxReputation.Text;
                string value6 = TextBoxSeniority.Text;
                string value7 = TextBoxSalary.Text;

                SqlCommand command = new SqlCommand("INSERT INTO Repairman (Repairman_ID, Company_Branch_ID_FK, Repairman_Name," +
                    " Job_Title, Seniority, Repairman_Reputation, Repairman_Salary) VALUES (@value1, @value2, @value3," +
                    " @value4, @value6, @value5, @value7)", connection);

                command.Parameters.AddWithValue("@value1", value1);
                command.Parameters.AddWithValue("@value2", value2);
                command.Parameters.AddWithValue("@value3", value3);
                command.Parameters.AddWithValue("@value4", value4);
                command.Parameters.AddWithValue("@value5", value5);
                command.Parameters.AddWithValue("@value6", value6);
                command.Parameters.AddWithValue("@value7", value7);

                command.ExecuteNonQuery();

                TextBoxMasterID.Text = "";
                TextBoxMasterName.Text = "";
                TextBoxReputation.Text = "";
                TextBoxSeniority.Text = "";
                TextBoxSalary.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
