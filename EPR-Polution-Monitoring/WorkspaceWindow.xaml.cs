using Npgsql;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DB_Reports_TPD_Lab_6
{
    /// <summary>
    /// Логика взаимодействия для WorkspaceWindow.xaml
    /// </summary>
    public partial class WorkspaceWindow : Window
    {
        public DBConnector Connection { get; set; }
        public WorkspaceWindow(DBConnector connection)
        {
            InitializeComponent();
            Connection = connection;
        }
        public async void OnClickStationInfo(object sender, RoutedEventArgs args)
        {
            var command = new NpgsqlCommand();
            command.Connection = Connection.ConnectionOpen();

            command.CommandText = $"SELECT s.station_name, s.city, s.station_status, c.Station_Coordinate\r\n" +
                $"FROM Station AS s\r\n" +
                $"INNER JOIN Coordinates AS c ON s.ID_Station = c.ID_Station_FK;";
                //$"WHERE c.Station_Coordinate ~= Point(50.4003378,30.4966682);";
            NpgsqlDataReader dataReader = await command.ExecuteReaderAsync();

            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);

            dataGrid.ItemsSource = dataTable.DefaultView;
        }
        public async void OnClickOptimalParameters(object sender, RoutedEventArgs args)
        {
            var command = new NpgsqlCommand();
            command.Connection = Connection.ConnectionOpen();

            command.CommandText = $"SELECT DISTINCT mu.Title, Category.Designation, ov.Upper_Border, ov.Bottom_Border\r\n" +
                $"FROM Measured_Unit AS mu\r\n" +
                $"INNER JOIN Optimal_Value AS ov ON mu.ID_Measured_Unit = ov.ID_Measured_Unit_FK\r\n" +
                $"INNER JOIN Category ON Category.id_category = ov.id_category_fk;\r\n";
            NpgsqlDataReader dataReader = await command.ExecuteReaderAsync();

            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);

            dataGrid.ItemsSource = dataTable.DefaultView;
        }
        public async void OnClickKyivStations(object sender, RoutedEventArgs args)
        {
            var command = new NpgsqlCommand();
            command.Connection = Connection.ConnectionOpen();

            command.CommandText = $"SELECT s.Station_Name, mu.Title, Category.Designation, m.Measurment_Value, m.Measurment_Time\r\n" +
                $"FROM Station AS s\r\n" +
                $"INNER JOIN Measurment AS m ON s.ID_Station = m.ID_Station_FK\r\n" +
                $"INNER JOIN Measured_Unit AS mu ON mu.ID_Measured_Unit = m.ID_Measured_Unit_fk\r\n" +
                $"INNER JOIN Optimal_Value ON mu.ID_Measured_Unit = Optimal_Value.ID_Measured_Unit_fk\r\n" +
                $"INNER JOIN Category ON Category.id_category = Optimal_Value.id_category_fk;";
                
            NpgsqlDataReader dataReader = await command.ExecuteReaderAsync();

            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);

            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        public void OnClickReports(object sender, RoutedEventArgs args)
        {
            var reportWindow = new ReportWindow(Connection);
            reportWindow.ShowDialog();

        }
    }
}
