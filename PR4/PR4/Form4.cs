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

            SqlCommand command = new SqlCommand("SELECT * FROM [CashReg]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_cashreg"]) + "  " + Convert.ToString(sqlReader["id_worker"]) + " " + Convert.ToString(sqlReader["id_ticket"]) + " " + Convert.ToString(sqlReader["opentime"]) + " " + Convert.ToString(sqlReader["closetime"]));
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
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [CashReg] (id_worker, id_ticket, opentime, closetime)VALUES(@id_worker, @id_ticket, @opentime, @closetime)", sqlConnection);
                command.Parameters.AddWithValue("id_worker", textBox1.Text);
                command.Parameters.AddWithValue("id_ticket", textBox2.Text);
                command.Parameters.AddWithValue("opentime", textBox3.Text);
                command.Parameters.AddWithValue("closetime", textBox4.Text);

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

            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
                !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) &&
                !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [CashReg] SET [id_worker]=@id_worker, [id_ticket]=@id_ticket, [opentime]=@opentime, [closetime]=@closetime WHERE [id_cashreg]=@id_cashreg", sqlConnection);

                command.Parameters.AddWithValue("id_cashreg", textBox5.Text);
                command.Parameters.AddWithValue("id_worker", textBox6.Text);
                command.Parameters.AddWithValue("id_ticket", textBox7.Text);
                command.Parameters.AddWithValue("opentime", textBox8.Text);
                command.Parameters.AddWithValue("closetime", textBox9.Text);


                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
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

            if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [CashReg] WHERE [id_cashreg]=@id_cashreg", sqlConnection);
                command.Parameters.AddWithValue("id_cashreg", textBox10.Text);

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

            SqlCommand command = new SqlCommand("SELECT * FROM [CashReg]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_cashreg"]) + "  " + Convert.ToString(sqlReader["id_worker"]) + " " + Convert.ToString(sqlReader["id_ticket"]) + " " + Convert.ToString(sqlReader["opentime"]) + " " + Convert.ToString(sqlReader["closetime"]));
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
