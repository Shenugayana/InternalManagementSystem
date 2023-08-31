using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternalManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Page
    {
        public Invoice()
        {
            InitializeComponent();
            LoadGrid();
        }

        public void LoadGrid()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            lblDate.Text = dateTime.ToString("MMM dd, yyyy");
            ObservableCollection<Records> records = new ObservableCollection<Records>();
            records.Add(new Records { product = "", quantity = "", amount = "" });
            records.Add(new Records { product = "", quantity = "", amount = "" });
            records.Add(new Records { product = "", quantity = "", amount = "" });
            records.Add(new Records { product = "", quantity = "", amount = "" });
            records.Add(new Records { product = "", quantity = "", amount = "" });
            invoiceDataGrid.ItemsSource = records;
        }

        private void printToPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if(printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "Invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        public class Records
        {
            public string product { get; set; }
            public string quantity { get; set; }
            public string amount { get; set; }
        }
    }
}
