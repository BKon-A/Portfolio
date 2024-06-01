using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace DBO_AWP_Computer_Service_Director
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBConnection dbConnection = new DBConnection();
                dbConnection.ConnectionFileString = DBConnection.getConnectionFile();
                dbConnection.ConnectionString = DBConnection.getConnectionString(dbConnection.ConnectionFileString);
                dbConnection.Login = "User ID=" + TextBoxLogin.Text + ";";
                dbConnection.Password = "Password=" + TextBoxPassword.Password + ";";
                dbConnection.ConnectionString = dbConnection.ConnectionString + dbConnection.Login + dbConnection.Password;
                SqlConnection connection = new SqlConnection(dbConnection.ConnectionString);
                connection.Open();

                if (dbConnection.Login.Contains("Director"))
                {
                    DirectorWindow directorWindow = new DirectorWindow(connection);
                    directorWindow.ShowDialog();
                }
                else if (dbConnection.Login.Contains("Master"))
                {
                    MasterWindow masterWindow = new MasterWindow(connection);
                    masterWindow.ShowDialog();
                }
                else
                {
                    ReceptionistWindow receptionistWindow = new ReceptionistWindow(connection);
                    receptionistWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
