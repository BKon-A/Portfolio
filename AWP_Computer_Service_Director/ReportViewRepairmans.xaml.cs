using Microsoft.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace DBO_AWP_Computer_Service_Director
{
    public partial class ReportViewRepairmans : Window
    {
        SqlConnection connection;
        private ReportViewer reportViewer;
        public ReportViewRepairmans(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

            reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportEmbeddedResource = "DBO_AWP_Computer_Service_Director.Report2.rdlc"; // Замените на путь к вашему файлу .rdlc

            WindowsFormsHost host = new WindowsFormsHost();
            host.Child = reportViewer;

            // Добавьте созданный host в главной контейнер окна
            mainGrid.Children.Add(host);

            Window_Loaded();
        }

        private void LoadReport()
        {
            try
            {
                // Загрузка данных из SQL-запроса в DataSet
                string query = "SELECT * FROM Repairman_Performance";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                // Привязка данных к отчету
                ReportDataSource reportDataSource = new ReportDataSource("DataSet2", ds.Tables[0]);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded()
        {
            LoadReport();
        }
    }
}
