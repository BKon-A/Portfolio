using Microsoft.Reporting.WinForms;
using System.Data;
using Npgsql;
using System.Windows;
using System;
using Microsoft.ReportingServices.Interfaces;
using System.Xml.Linq;

namespace DB_Reports_TPD_Lab_6
{
    /// <summary>
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportViewer reportViewer;
        public DBConnector Connection { get; set; }
        public ReportWindow(DBConnector connection)
        {
            InitializeComponent();
            Connection = connection;

            reportViewer = new()
            {
                ProcessingMode = ProcessingMode.Local
            };
            

            winFormsHost.Child = reportViewer;

            reports.SelectedIndex = 0;

            ComboBoxLoadStations();
        }

        public void ReportStationInfo()
        {
            try
            {
                ReportDataSource reportDataSource;
                DataTable dataTable = new();

                Connection.ConnectionOpen();

                var command = new NpgsqlCommand(@"SELECT 
                        s.station_name,
                        s.designation,
                        s.title,
                        s.measured_parameters,
                        s.measurment_time
                    FROM stations s", Connection.Connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);
                dataReader.Close();

                reportViewer.LocalReport.ReportEmbeddedResource = "DB_Reports_TPD_Lab_6.ReportStationsInfo.rdlc";
                reportDataSource = new ("DataSetStationsInfo", dataTable);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                reportViewer.RefreshReport();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ReportMeasuredResourcesByTime()
        {
            try
            {
                ReportDataSource reportDataSource;
                DataTable dataTable = new();
                string name = stationName.SelectedItem.ToString();

                Connection.ConnectionOpen();

                var command = new NpgsqlCommand($"SELECT\r\n\t" +
                    $"mrt.station_name,\r\n\t" +
                    $"MIN(mrt.measurment_value) min_value,\r\n\t" +
                    $"MAX(mrt.measurment_value) max_value,\r\n\t" +
                    $"AVG(mrt.measurment_value) avg_value,\r\n\t" +
                    $"mrt.param\r\n" +
                    $"FROM measured_resources_by_time mrt\r\n" +
                    $"WHERE mrt.station_name = @stationName AND mrt.measurment_time BETWEEN @dateStart AND @dateEnd\r\n" +
                    $"GROUP BY mrt.station_name, mrt.param"
                    , Connection.Connection);

                command.Parameters.AddWithValue("@stationName", name);
                command.Parameters.AddWithValue("@dateStart", dateStart.SelectedDate);
                command.Parameters.AddWithValue("@dateEnd", dateEnd.SelectedDate);

                NpgsqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);
                dataReader.Close();

                reportViewer.LocalReport.ReportEmbeddedResource = "DB_Reports_TPD_Lab_6.ReportMeasuredResourcesByTime.rdlc";
                reportDataSource = new("DataSetMeasuredResourcesByTime", dataTable);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                reportViewer.RefreshReport();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ReportDangerousPieces()
        {
            try
            {
                ReportDataSource reportDataSource;
                DataTable dataTable = new();

                Connection.ConnectionOpen();

                var command = new NpgsqlCommand(@"SELECT 
                        MAX(dp.measurment_value) AS max_value, 
	                    dp.title,
	                    dp.city 
                    FROM dangerous_pieces dp
                    WHERE dp.measurment_time BETWEEN @dateStartDangerousPieces AND @dateEndDangerousPieces
                    GROUP BY dp.title, dp.city;"
                    , Connection.Connection);

                command.Parameters.AddWithValue("@dateStartDangerousPieces", dateStartDangerousPieces.SelectedDate);
                command.Parameters.AddWithValue("@dateEndDangerousPieces", dateEndDangerousPieces.SelectedDate);

                NpgsqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);
                dataReader.Close();

                reportViewer.LocalReport.ReportEmbeddedResource = "DB_Reports_TPD_Lab_6.ReportDangerousPieces.rdlc";
                reportDataSource = new("DataSetDangerousPieces", dataTable);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                reportViewer.RefreshReport();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReportAvgDangerousPieces()
        {
            try
            {
                ReportDataSource reportDataSource;
                DataTable dataTable = new();
                string name = stationNameAvgDangerousPieces.SelectedItem.ToString();

                Connection.ConnectionOpen();

                var command = new NpgsqlCommand(@"
                SELECT
                    adp.quality,
                    adp.counted_values,
                    adp.station_name
                FROM avg_dangerous_pieces adp
                WHERE adp.station_name = @stationName;"
                    , Connection.Connection) ;

                command.Parameters.AddWithValue("@stationName", name);

                NpgsqlDataReader dataReader = command.ExecuteReader();
                

                dataTable.Load(dataReader);
                dataReader.Close();

                reportViewer.LocalReport.ReportEmbeddedResource = "DB_Reports_TPD_Lab_6.ReportAvgDangerousPieces.rdlc";
                reportDataSource = new("DataSetAvgDangerousPieces", dataTable);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReportAmountOfCarbonMonoxideAndSulfurDioxide()
        {
            try
            {
                ReportDataSource reportDataSource;
                DataTable dataTable = new();

                string name = stationNameAmountOfCarbonMonoxideAndSulfurDioxide.SelectedItem.ToString();

                Connection.ConnectionOpen();

                var command = new NpgsqlCommand(@"
                    SELECT
                        aocms.counted_values,
                        aocms.station_name,
                        aocms.quality,
                        aocms.title
                    FROM amount_of_carbon_monoxide_sulfur aocms
                    WHERE aocms.station_name = @stationName;"
                    , Connection.Connection);

                command.Parameters.AddWithValue("@stationName", name);

                NpgsqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);
                dataReader.Close();

                reportViewer.LocalReport.ReportEmbeddedResource = "DB_Reports_TPD_Lab_6.ReportAmountOfCarbonMonoxideAndSulfurDioxide.rdlc";
                reportDataSource = new("DataSetAmountOfCarbonMonoxideAndSulfurDioxide", dataTable);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Розробити візуалізацію у табличному та графічному виглядах кількості разів,
        //коли було зафіксовано середньодобові значення твердих частинок PM2.5,
        //значення яких належать до шкідливого рівня на певній станції за весь час

        private void ReportChangedHandler(object sender, EventArgs e)
        {
            reportViewer.Clear();

            if (reports.SelectedIndex == 0)
            {
                ReportStationInfo();
            }
            else if(reports.SelectedIndex == 1)
            {
                
            }
            else if(reports.SelectedIndex == 2)
            {
                
            }
            else if(reports.SelectedIndex == 3)
            {
                
            }
            else if(reports.SelectedIndex == 4)
            {
                
            }
        }

        private void ComboBoxLoadStations()
        {
            try
            {
                var command = new NpgsqlCommand($"SELECT station.station_name FROM station", Connection.Connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();

                stationName.Items.Clear();
                stationNameAvgDangerousPieces.Items.Clear();
                stationNameAmountOfCarbonMonoxideAndSulfurDioxide.Items.Clear();

                while(dataReader.Read())
                {
                    string name = dataReader.GetString(0);
                    stationName.Items.Add(name);
                    stationNameAvgDangerousPieces.Items.Add(name);
                    stationNameAmountOfCarbonMonoxideAndSulfurDioxide.Items.Add(name);
                }

                dataReader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void LoadReportClickMeasuredResourcesByTime(object sender, RoutedEventArgs e)
        {
            ReportMeasuredResourcesByTime(); 
        }
        private void LoadReportClickDangerousPieces(object sender, RoutedEventArgs e)
        {
            ReportDangerousPieces();
        }
        private void LoadReportClickAvgDangerousPieces(object sender, RoutedEventArgs e)
        {
            ReportAvgDangerousPieces();
        }
        private void LoadComboBoxStations(object sender, RoutedEventArgs e)
        {
            ComboBoxLoadStations();
        }
        private void LoadReportClickAmountOfCarbonMonoxideAndSulfurDioxide(object sender, RoutedEventArgs e)
        {
            ReportAmountOfCarbonMonoxideAndSulfurDioxide();
        }
    }
}
