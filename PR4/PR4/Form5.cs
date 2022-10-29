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
    public partial class Form5 : Form
    {
        SqlConnection sqlConnection;
        public Form5()
        {
            InitializeComponent();
        }

        private async void Form5_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\egori\Source\Repos\PR4\PR4\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Partic]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_partic"]) + "  " + Convert.ToString(sqlReader["name_p"]) + "  " + Convert.ToString(sqlReader["sname_p"]) + "  " + Convert.ToString(sqlReader["id_group"]) + "  " + Convert.ToString(sqlReader["phone_p"]));
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

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Partic]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id_partic"]) + "  " + Convert.ToString(sqlReader["name_p"]) + "  " + Convert.ToString(sqlReader["sname_p"]) + "  " + Convert.ToString(sqlReader["id_group"]) + "  " + Convert.ToString(sqlReader["phone_p"]));
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
                SqlCommand command = new SqlCommand("INSERT INTO [Partic] (name_p, sname_p, id_group, phone_p)VALUES(@name_p, @sname_p, @id_group, @phone_p)", sqlConnection);
                command.Parameters.AddWithValue("name_p", textBox1.Text);
                command.Parameters.AddWithValue("sname_p", textBox2.Text);
                command.Parameters.AddWithValue("id_group", textBox3.Text);
                command.Parameters.AddWithValue("phone_p", textBox4.Text);

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
                SqlCommand command = new SqlCommand("UPDATE [Partic] SET [name_p]=@name_p, [sname_p]=@sname_p, [id_group]=@id_group, [phone_p]=@phone_p WHERE [id_partic]=@id_partic", sqlConnection);

                command.Parameters.AddWithValue("id_partic", textBox5.Text);
                command.Parameters.AddWithValue("name_p", textBox6.Text);
                command.Parameters.AddWithValue("sname_p", textBox7.Text);
                command.Parameters.AddWithValue("id_group", textBox8.Text);
                command.Parameters.AddWithValue("phone_p", textBox9.Text);


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
                SqlCommand command = new SqlCommand("DELETE FROM [Partic] WHERE [id_partic]=@id_partic", sqlConnection);
                command.Parameters.AddWithValue("id_partic", textBox10.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label15.Visible = true;
                label15.Text = "ID должен быть заполнен!";
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
