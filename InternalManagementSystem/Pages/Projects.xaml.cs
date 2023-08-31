using System;
using System.Collections.Generic;
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
    /// Interaction logic for Projects.xaml
    /// </summary>
    public partial class Projects : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=LP-CYCXZH3;Initial Catalog=arulWing;Integrated Security=True");
        public Projects()
        {
            InitializeComponent();
            LoadDate();            
        }

        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }

        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTime.Focus();
        }

        private void txtNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0)
                lblNote.Visibility = Visibility.Collapsed;
            else
                lblNote.Visibility = Visibility.Visible;
        }

        private void txtTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.Length > 0)
                lblTime.Visibility = Visibility.Collapsed;
            else
                lblTime.Visibility = Visibility.Visible;
        }

        private void LoadDate()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            lblDate.Text = dateTime.ToString("dd");
            lblMonth.Text = dateTime.ToString("MMMM");
            lblWeek.Text = dateTime.ToString("dddd");
            showSchedule(dateTime.ToString("ddMMyyyy"));
        }

        private void ShowSelectedDate(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                DateTime dateTime = calendar.SelectedDate.Value;
                lblDate.Text = dateTime.ToString("dd");
                lblMonth.Text = dateTime.ToString("MMMM");
                lblWeek.Text = dateTime.ToString("dddd");
                showSchedule(dateTime.ToString("ddMMyyyy"));
            }
            else
            {
                txtNote.Clear();
                txtTime.Clear();
            }
        }

        private void AddProject(object sender, RoutedEventArgs e)
        {
            DateTime dateTime;
            string ID;
            if (calendar.SelectedDate.HasValue)
            {
                dateTime = calendar.SelectedDate.Value;
                ID = dateTime.ToString("ddMMyyyy");
            }
            else
            {
                dateTime = DateTime.UtcNow.Date;
                ID = dateTime.ToString("ddMMyyyy");
            }
            try
            {
                string status = "0";
                string client = txtNote.Text;
                string description = txtTime.Text;

                if(client.Equals("") || description.Equals(""))
                {
                    MessageBox.Show("Please fill the required fields", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SqlCommand command = new SqlCommand("insert into Projects values (@ID, @client, @description, @status)", con);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@client", client);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@status", status);
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Project Scheduled!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);                    
                }
                
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show("Already a Project is Scheduled for this day!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }                
            }
            Schedule.Title = null;
            Schedule.Time = null;
        }

        private void showSchedule(string id)
        {
            SqlCommand command = new SqlCommand("select * from Projects where ID =" + id, con);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                if (reader.Read())
                {
                    Schedule.Title = reader[1].ToString();
                    Schedule.Time = reader[2].ToString();
                }
                else
                {
                    Schedule.Title = null;
                    Schedule.Time = null;
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}
