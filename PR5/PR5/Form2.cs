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
    public partial class Form2 : Form
    {
        SqlConnection sqlConnection;
        public Form2()
        {
            InitializeComponent();
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\egori\Source\Repos\PR4\PR4\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [workers]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_worker"]) + "  " + Convert.ToString(sqlReader["name_w"]) + "  " + Convert.ToString(sqlReader["sname_w"]) + " " + Convert.ToString(sqlReader["res_address"]) + " " + Convert.ToString(sqlReader["phone_w"]) + " " + Convert.ToString(sqlReader["salary"]) + " " + Convert.ToString(sqlReader["date_birth"]) + " " + Convert.ToString(sqlReader["id_passport"]) + " " + Convert.ToString(sqlReader["id_job"]));
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

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (label13.Visible)
                label13.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
                !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [workers] (name_w, sname_w, res_address, phone_w, salary, date_birth, id_passport, id_job)VALUES(@name_w, @sname_w, @res_address, @phone_w, @salary, @date_birth, @id_passport, @id_job)", sqlConnection);
                command.Parameters.AddWithValue("name_w", textBox1.Text);
                command.Parameters.AddWithValue("sname_w", textBox2.Text);
                command.Parameters.AddWithValue("res_address", textBox3.Text);
                command.Parameters.AddWithValue("phone_w", textBox4.Text);
                command.Parameters.AddWithValue("salary", textBox5.Text);
                command.Parameters.AddWithValue("date_birth", textBox6.Text);
                command.Parameters.AddWithValue("id_passport", textBox7.Text);
                command.Parameters.AddWithValue("id_job", textBox8.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label13.Visible = true;
                label13.Text = "Все поля должны быть заполнены!";

            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [workers]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_worker"]) + "  " + Convert.ToString(sqlReader["name_w"]) + "  " + Convert.ToString(sqlReader["sname_w"]) + " " + Convert.ToString(sqlReader["res_address"]) + " " + Convert.ToString(sqlReader["phone_w"]) + " " + Convert.ToString(sqlReader["salary"]) + " " + Convert.ToString(sqlReader["date_birth"]) + " " + Convert.ToString(sqlReader["id_passport"]) + " " + Convert.ToString(sqlReader["id_job"]));
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

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label14.Visible)
                label14.Visible = false;

            if (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text) &&
                !string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text) &&
                !string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text) &&
                !string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text) &&
                !string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrWhiteSpace(textBox13.Text) &&
                !string.IsNullOrEmpty(textBox14.Text) && !string.IsNullOrWhiteSpace(textBox14.Text) &&
                !string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text) &&
                !string.IsNullOrEmpty(textBox17.Text) && !string.IsNullOrWhiteSpace(textBox17.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [workers] SET [name_w]=@name_w, [sname_w]=@sname_w, [res_address]=@res_address, [phone_w]=@phone_w, [salary]=@salary, [date_birth]=@date_birth, [id_passport]=@id_passport, [id_job]=@id_job WHERE [id_worker]=@id_worker", sqlConnection);

                command.Parameters.AddWithValue("id_worker", textBox9.Text);
                command.Parameters.AddWithValue("name_w", textBox10.Text);
                command.Parameters.AddWithValue("sname_w", textBox11.Text);
                command.Parameters.AddWithValue("res_address", textBox12.Text);
                command.Parameters.AddWithValue("phone_w", textBox13.Text);
                command.Parameters.AddWithValue("salary", textBox14.Text);
                command.Parameters.AddWithValue("date_birth", textBox15.Text);
                command.Parameters.AddWithValue("id_passport", textBox16.Text);
                command.Parameters.AddWithValue("id_job", textBox17.Text);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
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

            if (!string.IsNullOrEmpty(textBox18.Text) && !string.IsNullOrWhiteSpace(textBox18.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [workers] WHERE [id_worker]=@id_worker", sqlConnection);
                command.Parameters.AddWithValue("id_worker", textBox18.Text);

                await command.ExecuteNonQueryAsync();
            }    
            else
            {
                label15.Visible = true;
                label15.Text = "ID должен быть заполнен!";
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 newForm = new Form4();
            newForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5();
            newForm.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
