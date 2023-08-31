using System;
using System.Collections.Generic;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=LP-CYCXZH3;Initial Catalog=arulWing;Integrated Security=True");
        public Dashboard()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            con.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(ID) FROM Projects");
            sqlCommand.Connection = con;
            string RecordCount = Convert.ToString(sqlCommand.ExecuteScalar());
            lblOrders.Text = RecordCount;
        }
    }
}
