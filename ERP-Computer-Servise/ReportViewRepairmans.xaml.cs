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

            reportViewer = new()
            {
                ProcessingMode = ProcessingMode.Local
            };

            winFormsHost.Child = reportViewer;

            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                ReportDataSource reportDataSource;
                DataTable dataTable = new();

                // Загрузка данных из SQL-запроса в DataSet
                var command = new SqlCommand("SELECT * FROM Repairman_Performance", connection);
                SqlDataReader dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);
                dataReader.Close();

                reportViewer.LocalReport.ReportEmbeddedResource = "DBO_AWP_Computer_Service_Director.Report2.rdlc";
                reportDataSource = new("DataSet2", dataTable);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
