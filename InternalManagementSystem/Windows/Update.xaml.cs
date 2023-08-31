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
using System.Windows.Shapes;

namespace InternalManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        int a;
        string filename;
        SqlConnection con = new SqlConnection(@"Data Source=LP-CYCXZH3;Initial Catalog=arulWing;Integrated Security=True");
        public Update(int id)
        {
            a = id;
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            SqlCommand command = new SqlCommand("select * from Clients where ID =" + a, con);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                if (reader.Read())
                {
                    txtName.Text = reader[2].ToString();
                    txtEmail.Text = reader[3].ToString();
                    txtMobile.Text = reader[4].ToString();
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Update(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtMobile.Text;
            string sql;

            if (string.IsNullOrEmpty(filename))
            {
                sql = "update Clients set name = '" + name + "', Email = '" + email + "', Phone = '" + int.Parse(phone) + "' where ID =" + a;
            }
            else
            {
                sql = "update Clients set profile = '" + filename + "', name = '" + name + "', Email = '" + email + "', Phone = '" + int.Parse(phone) + "' where ID =" + a;
            }
            
            try
            {
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Successfully Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
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
