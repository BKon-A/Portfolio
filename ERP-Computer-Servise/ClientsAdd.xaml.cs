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
    /// Логика взаимодействия для ClientsAdd.xaml
    /// </summary>
    public partial class ClientsAdd : Window
    {
        SqlConnection connection;
        public ClientsAdd(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("EXECUTE Add_Computer_Service_Client @Client_ID, @Client_Name, @Client_Number ;", connection);

                command.Parameters.AddWithValue("@Client_ID", TextBoxClientID.Text);
                command.Parameters.AddWithValue("@Client_Name", TextBoxClientUsername.Text);
                command.Parameters.AddWithValue("@Client_Number", TextBoxClientPhoneNumber.Text);

                command.ExecuteNonQuery();

                TextBoxClientID.Text = "";
                TextBoxClientUsername.Text = "";
                TextBoxClientPhoneNumber.Text = "";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
