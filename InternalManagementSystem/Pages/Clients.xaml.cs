using InternalManagementSystem.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace InternalManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
        SqlConnection connection = new SqlConnection(@"Data Source=LP-CYCXZH3;Initial Catalog=arulWing;Integrated Security=True");

        public Clients()
        {
            InitializeComponent();
            LoadGrid();
        }

        public void LoadGrid()
        {
            SqlCommand command = new SqlCommand("select * from Clients", connection);
            DataTable table = new DataTable();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
            connection.Close();
            membersDataGrid.ItemsSource = table.DefaultView;
        }

        private void Create_User(object sender, RoutedEventArgs e)
        {
            Create create = new Create();
            create.Show();
        }

        private void Update_User(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)membersDataGrid.SelectedItem;
            int id = int.Parse(dataRowView["ID"].ToString());

            Update update = new Update(id);
            update.Show();
        }

        private void Btn_Remove(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)membersDataGrid.SelectedItem;
            string s = dataRowView["ID"].ToString();
            int id = int.Parse(s);

            SqlCommand command = new SqlCommand("delete from Clients where ID = " + id, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Record has been deleted!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadGrid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
