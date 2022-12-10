using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR4
{
    public partial class Form4 : Form
    {
        SqlConnection sqlConnection;
        public Form4()
        {
            InitializeComponent();
        }

        private async void Form4_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\egori\Source\Repos\PR4\PR4\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [address]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_address"]) + "  " + Convert.ToString(sqlReader["id_worker"]) + " " + Convert.ToString(sqlReader["region"]) + " " + Convert.ToString(sqlReader["city"]) + " " + Convert.ToString(sqlReader["street"]) + " " + Convert.ToString(sqlReader["number_home"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (label13.Visible)
                label13.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [address] (id_worker, region, city, street, number_home)VALUES(@id_worker, @region, @city, @street, @number_home)", sqlConnection);
                command.Parameters.AddWithValue("id_worker", textBox1.Text);
                command.Parameters.AddWithValue("region", textBox2.Text);
                command.Parameters.AddWithValue("city", textBox3.Text);
                command.Parameters.AddWithValue("street", textBox4.Text);
                command.Parameters.AddWithValue("number_home", textBox5.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label13.Visible = true;
                label13.Text = "Все поля должны быть заполнены!";

            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label14.Visible)
                label14.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
                !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox9.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox10.Text) &&
                !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox11.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [address] SET [id_worker]=@id_worker, [region]=@region, [city]=@city, [street]=@street, [number_home]=@number_home WHERE [id_address]=@id_address", sqlConnection);

                command.Parameters.AddWithValue("id_address", textBox6.Text);
                command.Parameters.AddWithValue("id_worker", textBox7.Text);
                command.Parameters.AddWithValue("region", textBox8.Text);
                command.Parameters.AddWithValue("city", textBox9.Text);
                command.Parameters.AddWithValue("street", textBox10.Text);
                command.Parameters.AddWithValue("number_home", textBox11.Text);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                label14.Visible = true;
                label14.Text = "Id должно быть заполнено!";
            }
            else
            {
                label14.Visible = true;
                label14.Text = "Необходимо заполнить все поля!";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label15.Visible)
                label15.Visible = false;

            if (!string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [address] WHERE [id_address]=@id_address", sqlConnection);
                command.Parameters.AddWithValue("id_address", textBox12.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label15.Visible = true;
                label15.Text = "ID должен быть заполнен!";
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [address]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_address"]) + "  " + Convert.ToString(sqlReader["id_worker"]) + " " + Convert.ToString(sqlReader["region"]) + " " + Convert.ToString(sqlReader["city"]) + " " + Convert.ToString(sqlReader["street"]) + " " + Convert.ToString(sqlReader["number_home"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
