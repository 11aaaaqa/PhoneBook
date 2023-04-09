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

namespace PhoneBook
{
    public partial class Form1 : Form
    {

        private string connectionString =
            @"Server=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Kepa\Выполненные проекты\PhoneBook\Phones.mdf;Integrated Security=True";
        private SqlConnection sqlConnection;

        public void Update()
        {
            listBox1.Items.Clear();

            sqlConnection = new SqlConnection(connectionString);

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM Phones", sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                listBox1.Items.Add(sqlReader["PhoneNumber"] + "                        " + sqlReader["Name"]);
            }

            sqlReader.Close();
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (label6.Visible == true)
                label6.Visible = false;
            if (label9.Visible == true)
                label9.Visible = false;

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Phones WHERE PhoneNumber=@PhoneNumber",sqlConnection);
            sqlCommand.Parameters.AddWithValue("PhoneNumber", textBox1.Text);
            reader = sqlCommand.ExecuteReader();
            dynamic result = null;
            while (reader.Read())
            {
                result = reader["PhoneNumber"];
            }
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();


            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && result == null)
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Phones (PhoneNumber, Name)VALUES(@PhoneNumber, @Name)", sqlConnection);

                command.Parameters.AddWithValue("PhoneNumber", textBox1.Text);

                command.Parameters.AddWithValue("Name", textBox2.Text);

                command.ExecuteNonQuery();

                label9.Visible = true;
                label9.Text = "Контакт успешно добавлен!";

                Update();

            }
            else if (result != null)
            {
                label6.Visible = true;
                label6.Text = "Ошибка! Такой номер телефона уже существует!";
            }
            else
            {
                label6.Visible = true;
                label6.Text = "Ошибка! Заполните поля!";
            }

            textBox1.Clear();
            textBox2.Clear();

            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM Phones", sqlConnection);

            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                listBox1.Items.Add(sqlReader["PhoneNumber"] + "                        " + sqlReader["Name"]);
            }

            sqlReader.Close();
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //if (label10.Visible == true)
            //    label10.Visible = false;

            //listBox1.Items.Clear();

            //sqlConnection = new SqlConnection(connectionString);

            //if (sqlConnection.State == ConnectionState.Closed)
            //    sqlConnection.Open();

            //SqlDataReader sqlReader = null;

            //SqlCommand command = new SqlCommand("SELECT * FROM Phones",sqlConnection);

            //sqlReader = command.ExecuteReader();

            //while (sqlReader.Read())
            //{
            //    listBox1.Items.Add(sqlReader["PhoneNumber"] + "                        " + sqlReader["Name"]);
            //}

            //label10.Visible = true;
            //label10.Text = "База данных успешно обновлена";

            //sqlReader.Close();
            //if (sqlConnection.State == ConnectionState.Open)
            //    sqlConnection.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (label7.Visible == true)
                label7.Visible = false;
            if (label8.Visible = true)
                label8.Visible = false;

            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                    !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                try
                {
                    sqlConnection = new SqlConnection(connectionString);
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    SqlCommand command =
                        new SqlCommand(
                            $"UPDATE Phones SET Name=@Name WHERE PhoneNumber=@PhoneNumber",
                            sqlConnection);

                    command.Parameters.AddWithValue("Name", textBox7.Text);
                    command.Parameters.AddWithValue("PhoneNumber",textBox5.Text);

                    command.ExecuteNonQuery();

                    if (command.ExecuteNonQuery() == 0)
                    {
                        label7.Visible = true;
                        label7.Text = "Ошибка! Такого номера телефона не существует!";
                    }
                    else
                    {
                        label8.Visible = true;
                        label8.Text = "Контакт успешно обновлен.";

                        Update();

                        textBox7.Clear();
                        textBox5.Clear();
                    }
                }
                catch(Exception exception)
                {
                    label7.Visible = true;
                    label7.Text = exception.Message;
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Ошибка! Заполните все поля!";
            }

            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (label11.Visible == true)
                label11.Visible = false;

            if (label12.Visible == true)
                label12.Visible = false;

            sqlConnection = new SqlConnection(connectionString);
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            SqlDataReader reader = null;
            SqlCommand com = new SqlCommand("SELECT * FROM Phones WHERE PhoneNumber=@PhoneNumber", sqlConnection);
            com.Parameters.AddWithValue("PhoneNumber", textBox3.Text);
            reader = com.ExecuteReader();
            dynamic result = null;
            while (reader.Read())
                result = reader["PhoneNumber"];

            if (result == null)
            {
                label11.Visible = true;
                label11.Text = "Ошибка! Такого номера телефона не существует!";
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            else
            {
                try
                {
                    sqlConnection = new SqlConnection(connectionString);
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();

                    SqlCommand command = new SqlCommand("DELETE FROM Phones WHERE PhoneNumber=@PhoneNumber",
                        sqlConnection);

                    command.Parameters.AddWithValue("PhoneNumber", textBox3.Text);

                    command.ExecuteNonQuery();

                    label12.Visible = true;
                    label12.Text = "Телефонный номер успешно удален.";

                    Update();

                    textBox3.Clear();
                }
                catch (Exception exception)
                {
                    label11.Visible = true;
                    label11.Text = exception.Message;
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }
    }
}
