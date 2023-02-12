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
    public partial class Form7 : Form
    {
        SqlConnection sqlConnection;
        public Form7()
        {
            InitializeComponent();
        }

        private async void Form7_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\egori\Source\Repos\PR4\PR4\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Perform]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_perform"]) + "  " + Convert.ToString(sqlReader["name_perform"]) + "  " + Convert.ToString(sqlReader["date_perf"]) + "  " + Convert.ToString(sqlReader["id_group"]));
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

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Perform]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_perform"]) + "  " + Convert.ToString(sqlReader["name_perform"]) + "  " + Convert.ToString(sqlReader["date_perf"]) + "  " + Convert.ToString(sqlReader["id_group"]));
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
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Perform] (name_perform, date_perf, id_group)VALUES(@name_perform, @date_perf, @id_group)", sqlConnection);
                command.Parameters.AddWithValue("name_perform", textBox1.Text);
                command.Parameters.AddWithValue("date_perf", textBox2.Text);
                command.Parameters.AddWithValue("id_group", textBox3.Text);

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
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))

            {
                SqlCommand command = new SqlCommand("UPDATE [Perform] SET [name_perfrom]=@name_perform, [date_perf]=@date_perf, [id_group]=@id_group WHERE [id_perform]=@id_perform", sqlConnection);

                command.Parameters.AddWithValue("id_perform", textBox5.Text);
                command.Parameters.AddWithValue("name_perform", textBox6.Text);
                command.Parameters.AddWithValue("date_perf", textBox7.Text);
                command.Parameters.AddWithValue("id_group", textBox8.Text);


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

            if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Perform] WHERE [id_perform]=@id_perform", sqlConnection);
                command.Parameters.AddWithValue("id_perform", textBox8.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label15.Visible = true;
                label15.Text = "ID должен быть заполнен!";
            }
        }
    }
}
