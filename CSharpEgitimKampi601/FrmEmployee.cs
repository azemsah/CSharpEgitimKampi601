using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CSharpEgitimKampi601
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;Password=965700";

        void EmployeeList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Employess";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        void DepartmentList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeeDepartment.ValueMember = "DepartmentId";
            cmbEmployeeDepartment.DataSource = dataTable;
            connection.Close();
        }
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeList();
            DepartmentList();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string EmployeeName = txtEmployeeName.Text;
            string EmployeeSurname = txtEmployeeSurname.Text;
            decimal EmployeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int DepartmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Employess (EmployeeName, EmployeeSurname, EmployeeSalary, DepartmentId) VALUES (@EmployeeName, @EmployeeSurname, @EmployeeSalary, @DepartmentId)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeName", EmployeeName);
            command.Parameters.AddWithValue("@EmployeeSurname", EmployeeSurname);
            command.Parameters.AddWithValue("@EmployeeSalary", EmployeeSalary);
            command.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Ekleme İşlemi Başarılı!");
            connection.Close();
            EmployeeList();
        }
    }
}
