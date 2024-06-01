using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System.Windows;
using System.Windows.Controls;

namespace DBO_AWP_Computer_Service_Director
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class DirectorWindow : Window
    {
        SqlConnection connection;
        public DirectorWindow(SqlConnection connection)
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
            if (menuItem.Header.ToString() == "Додати")
            {
                MasterAdd masterAdd = new MasterAdd(connection);
                masterAdd.ShowDialog();
            }
            else if (menuItem.Header.ToString() == "Переглянути")
            {
                MasterView masterView = new MasterView(connection);
                masterView.ShowDialog();
            }
            else if (menuItem.Header.ToString() == "Редагувати")
            {
                MasterEdit masterEdit = new MasterEdit(connection);
                masterEdit.ShowDialog();
            }
            else
            {
                MasterDelete masterDelete = new MasterDelete(connection);
                masterDelete.ShowDialog();
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
            if (menuItem.Header.ToString() == "Додати")
            {
                ServiceAdd serviceAdd = new ServiceAdd(connection);
                serviceAdd.ShowDialog();
            }
            else if (menuItem.Header.ToString() == "Переглянути")
            {
                ServiceView serviceView = new ServiceView(connection);
                serviceView.ShowDialog();
            }
            else if (menuItem.Header.ToString() == "Редагувати")
            {
                ServiceEdit serviceEdit = new ServiceEdit(connection);
                serviceEdit.ShowDialog();
            }
            else
            {
                ServiceDelete serviceDelete = new ServiceDelete(connection);
                serviceDelete.ShowDialog();
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

        private void MenuItem_ClickPerformedWorks(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem.Header.ToString() == "Проблеми клієнтів")
            {
                ReportViewCustomers reportsView = new ReportViewCustomers(connection);
                reportsView.ShowDialog();
            }
            else
            {
                ReportViewRepairmans reportView = new ReportViewRepairmans(connection);
                reportView.ShowDialog();
            }
        }

        private void MenuItem_ClickExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
