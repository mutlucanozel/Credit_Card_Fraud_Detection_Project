using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Credit_Card_Fraud_Detection_Project
{

    public partial class LoginScreen : Form
    {
        private SqlConnection conn;
      
        public LoginScreen()
        {
            InitializeComponent();
        }
        void connect()
        {
            conn = new SqlConnection("server=.;Initial Catalog=proje;Integrated Security=SSPI");
            conn.Open();


        }
        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterScreen FORM2gecis = new RegisterScreen();
            FORM2gecis.ShowDialog();
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {  if (chck_selleraccount.Checked)
            {
                string user = txtbox_username.Text;
                string pass = txtbox_password.Text;
                if (IsLogin2(user, pass))
                {
                    MessageBox.Show($" WELCOME {user}  ", "Login Successful ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formgec2();
                }
                else
                {
                    MessageBox.Show($"{user} does not exist or password is incorrect !", "LOGIN FAILED ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    

                }

            }

            else
            {
                string user = txtbox_username.Text;
                string pass = txtbox_password.Text;
                if (IsLogin(user, pass))
                {
                    MessageBox.Show($" WELCOME {user}  ", "Login Successful ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formgec();
                }
                else
                {
                    MessageBox.Show($"{user} does not exist or password is incorrect !", "LOGIN FAILED ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    

                }




            }

        }

        private bool OpenConnection()
        {

            try
            {
                
                return true;
            }


            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("No Connection");
                        break;
                    case 1045:
                        MessageBox.Show("Server username or  password is incorrect!");
                        break;

                }
                return false;


            }

        }

         public bool IsLogin(string user, string pass)
            {
            string query = $"SELECT * FROM Customers WHERE Username='{user}' AND Password='{pass}';";
            
            try
            {
                if (OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query,conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if (reader.Read())
                {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }

        
        }
        public bool IsLogin2(string user, string pass)
        {
            string query = $"SELECT * FROM Accounts WHERE Username='{user}' AND Password='{pass}';";

            try
            {
                if (OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }


        }
        public void formgec()
        {
            this.Hide();
            UpdateScreen FORM3gecis = new UpdateScreen();
            FORM3gecis.textBox1.Text = txtbox_username.Text;
            FORM3gecis.ShowDialog();
            this.Close();
        }
        public void formgec2()
        {
            this.Hide();
            ProductPage FORM4gecis = new ProductPage();
            FORM4gecis.ShowDialog();
            this.Close();
        }

        private void CheckbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckbxShowPas.Checked)
            {
                txtbox_password.UseSystemPasswordChar = true;

            }
            else
            {
                txtbox_password.UseSystemPasswordChar = false;


            }
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            txtbox_password.PasswordChar ='*';
            connect();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtbox_password.Clear();
            txtbox_username.Clear();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string user = txtbox_username.Text;
            
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            txtbox_username.BackColor = System.Drawing.Color.Thistle;
            
            txtbox_password.BackColor = System.Drawing.Color.White;
            

        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            txtbox_password.BackColor = System.Drawing.Color.Thistle;

            txtbox_username.BackColor = System.Drawing.Color.White;

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            btn_logın.ForeColor = System.Drawing.Color.DeepPink;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            btn_logın.ForeColor = System.Drawing.Color.Thistle;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            btn_clear.BackColor= System.Drawing.Color.Thistle;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            btn_clear.BackColor = System.Drawing.Color.White;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void CheckbxShowPas_MouseEnter(object sender, EventArgs e)
        {

            CheckbxShowPas.ForeColor = Color.DeepPink;
        }

        private void CheckbxShowPas_MouseLeave(object sender, EventArgs e)
        {
            CheckbxShowPas.ForeColor = Color.DarkGray;
        }

        private void txtbox_password_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
    }

