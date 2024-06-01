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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ReceptionistWindow : Window
    {
        SqlConnection connection;
        public ReceptionistWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void MenuItem_ClickOrder(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem.Header.ToString() == "Додати")
            {
                OrderAdd orderAdd = new OrderAdd(connection);
                orderAdd.ShowDialog();
            }
            else if (menuItem.Header.ToString() == "Переглянути")
            {
                OrderView orderView = new OrderView(connection);
                orderView.ShowDialog();
            }
            else
            {
                OrderDelete orderDelete = new OrderDelete(connection);
                orderDelete.ShowDialog();
            }
        }

        private void MenuItem_ClickMasters(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem.Header.ToString() == "Переглянути")
            {
                OrderView orderView = new OrderView(connection);
                orderView.ShowDialog();
            }
        }

        private void MenuItem_ClickReportProblems(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportViewCustomers(connection);
            reportWindow.ShowDialog();
        }
        private void MenuItem_ClickReportEndedWorks(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportViewRepairmans(connection);
            reportWindow.ShowDialog();
        }

        private void MenuItem_ClickServise(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem.Header.ToString() == "Переглянути")
            {
                ServiceView serviceView = new ServiceView(connection);
                serviceView.ShowDialog();
            }
        }

        private void MenuItem_ClickClients(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem.Header.ToString() == "Додати")
            {
                ClientsAdd clientsAdd = new ClientsAdd(connection);
                clientsAdd.ShowDialog();
            }
            else if (menuItem.Header.ToString() == "Редагувати")
            {
                ClientsEdit clientsEdit = new ClientsEdit(connection);
                clientsEdit.ShowDialog();
            }
            else
            {
                ClientsView clientsView = new ClientsView(connection);
                clientsView.ShowDialog();
            }
        }

        private void MenuItem_ClickExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

