using InternalManagementSystem.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InternalManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
        }
        string filename;
        SqlConnection con = new SqlConnection(@"Data Source=LP-CYCXZH3;Initial Catalog=arulWing;Integrated Security=True");

        public bool IsValid()
        {
            if (txtName.Equals(null))
            {
                System.Windows.MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string email = txtEmail.Text;
                string phone = txtMobile.Text;

                SqlCommand command = new SqlCommand("insert into Clients values (@Profile, @Name, @Email, @Phone)", con);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Profile", filename);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Phone", int.Parse(phone));
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                System.Windows.MessageBox.Show("User Created Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Clients clients = new Clients();
                clients.LoadGrid();
                this.Close();
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Text documents (.jpg)|*.jpg";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                filename = dialog.FileName;
            }
        }
    }
}
