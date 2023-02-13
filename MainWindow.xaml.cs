using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD_Operations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadAllrecords();
        }
       
        SqlConnection sqlCon = 
            new SqlConnection("Data Source=RYZEN-3;Initial Catalog=CRUD_SP_DB;Integrated Security=True");

        private void Form1_Load (object sender, EventArgs e)
        {
            LoadAllrecords();
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            string status = "";
            if (radRunning.IsChecked == true)
            {
                status = radRunning.Name;
            }
            else if (radUnused.IsChecked == true)
            {
                status = radUnused.Name;
            }
            try
            {
                SqlCommand comm = new SqlCommand("exec dbo.SP_InsertProduct '" +
                    +int.Parse(txtProdId.Text) + "', '" + txtName.Text + "', '" + comboxColor.Text + "', '" + "', '" +
                    DateTime.Parse(datePick.Text) + "'", sqlCon);
                comm.ExecuteNonQuery();
                MessageBox.Show("Create Successfully");
                LoadAllrecords();
                sqlCon.Close();
            } catch(Exception ex) {MessageBox.Show(ex.Message); }
        }
        void LoadAllrecords()
        {
            SqlCommand comm = new SqlCommand("exec dbo.SP_ProductView", sqlCon);
            MessageBox.Show("Display Successfully");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(comm);
            DataTable dataTable= new DataTable("Products");
            sqlAdapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            string status = "";
            if (radRunning.IsChecked == true)
            {
                status = radRunning.Name;
            }
            else if (radUnused.IsChecked == true)
            {
                status = radUnused.Name;
            }
            SqlCommand comm = new SqlCommand("exec dbo.SP_UpdateProduct '" +
                +int.Parse(txtProdId.Text) + "', '" + txtName.Text + "', '" + comboxColor.Text + "', '" + status +"', '" +
                DateTime.Parse(datePick.Text) + "'", sqlCon);
            comm.ExecuteNonQuery();
            MessageBox.Show("Update Successfully");
            LoadAllrecords();
            sqlCon.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            
            SqlCommand comm = new SqlCommand("exec dbo.SP_DeleteProduct '" +
                +int.Parse(txtProdId.Text) + "'", sqlCon);
            comm.ExecuteNonQuery();
            MessageBox.Show("Deleted Successfully");
            LoadAllrecords();
            sqlCon.Close();
        }
    }
}
