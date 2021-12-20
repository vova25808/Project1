using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private static string connect = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = Basa.accdb;";
        private OleDbConnection connection = new OleDbConnection(connect);
        private OleDbDataAdapter adapter = null;
        private DataSet dataSet = null;
        private DataTable table = null;
        private int id = 1;
        public Form2()
        {
            InitializeComponent();
            connection.Open();
        }

        private void dataup()
        {
            dataSet.Tables["Писатели"].Clear();
            adapter.Fill(dataSet, "Писатели");
        }
        private void Initialization()
        {
            try
            {
                textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
                textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                textBox5.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            }
            catch
            {
                MessageBox.Show("Данного элемента нет!");
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            adapter = new OleDbDataAdapter("SELECT * FROM Писатели", connection);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "Писатели");
            table = dataSet.Tables["Писатели"];
            dataGridView1.DataSource = table;
            Initialization();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Initialization();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int year = int.Parse(textBox1.Text);
                string place = textBox2.Text.ToString();
                string field = textBox3.Text.ToString();
                string name = textBox4.Text.ToString();
                OleDbCommand update = connection.CreateCommand();
                update.CommandText = $"UPDATE [Писатели] SET [год рождения]='{year}' ,[место рождения]='{place}' ,[сфера деятельности]='{field}', [ФИО]='{name}' WHERE ID={id}";
                update.ExecuteNonQuery();
                dataup();
            }
            catch
            {
                MessageBox.Show("Неверный формат данных!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand delete = connection.CreateCommand();
            delete.CommandText = $"DELETE FROM [Писатели] WHERE [ID] = {id}";
            delete.ExecuteNonQuery();
            dataup();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // int ID = dataGridView.Rows.Count;
                
                int year = int.Parse(textBox6.Text);
                string place = textBox7.Text.ToString();
                string field = textBox8.Text.ToString();
                string name = textBox9.Text.ToString();
                OleDbCommand Insert = connection.CreateCommand();
                Insert.CommandText = $"INSERT INTO [Писатели] ([Год рождения],[Место рождения],[Сфера деятельности],[ФИО]) VALUES('{year}','{place}','{field}','{name}')";
                Insert.ExecuteNonQuery();
                dataup();
                textBox6.Text ="";
                textBox7.Text ="";
                textBox8.Text ="";
                textBox9.Text ="";
            }
            catch
            {
                MessageBox.Show("Неверный формат данных!");
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
