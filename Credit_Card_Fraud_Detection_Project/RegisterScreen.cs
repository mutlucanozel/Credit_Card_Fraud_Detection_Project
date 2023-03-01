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
using Stripe;



namespace Credit_Card_Fraud_Detection_Project
{       
    public partial class RegisterScreen : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
       

        public RegisterScreen()
        {
            StripeConfiguration.ApiKey = "sk_test_51LrKnIHO7XYPxtMQSdkXp4g5Y7TrJLFLwlBWzBbebdXlfOR6x8fegkmsZSSBiBxgDXHR1168vpvK53KVr6AFeRmz00zMks1hWy";
            InitializeComponent();
        }
        void verilerigetir()
        {
            baglanti = new SqlConnection("server=.;Initial Catalog=proje;Integrated Security=SSPI");
            baglanti.Open();
            
         }
      
        private void clearall()
        {   
            name.Clear();
            surname.Clear();  
            email.Clear();
            usrname.Clear();
            pass.Clear();
            
        }

   

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginScreen FORM1gecis = new LoginScreen();
            FORM1gecis.ShowDialog();
            this.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            pass.UseSystemPasswordChar = false;
            verilerigetir();
        }
        private void addcreditscreen()
        {
            this.Hide();
            PaymentScreen paymentgecis = new PaymentScreen();
            paymentgecis.cusıd.Text = textbox_cusıd.Text;
            paymentgecis.labelad.Text = label8.Text;
            paymentgecis.ShowDialog();
            this.Close();
        }

     

        private void button2_Click(object sender, EventArgs e)
        {

            addcreditscreen();

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            name.BackColor = System.Drawing.Color.Thistle;
            button2.BackColor = System.Drawing.Color.White;
            email.BackColor = System.Drawing.Color.White;
            pass.BackColor= System.Drawing.Color.White;
            usrname.BackColor= System.Drawing.Color.White;
            surname.BackColor=System.Drawing.Color.White;


        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            name.BackColor = System.Drawing.Color.White;
            button2.BackColor = System.Drawing.Color.White;
            email.BackColor = System.Drawing.Color.Thistle;
            pass.BackColor = System.Drawing.Color.White;
            usrname.BackColor = System.Drawing.Color.White;
            surname.BackColor = System.Drawing.Color.White;

        }


        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            button2.BackColor = System.Drawing.Color.White;
           
            name.BackColor = System.Drawing.Color.White;
            email.BackColor = System.Drawing.Color.White;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            
            button1.ForeColor = System.Drawing.Color.DeepPink;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = System.Drawing.Color.White;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {

            button2.BackColor = System.Drawing.Color.Thistle;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = System.Drawing.Color.White;
        }

       
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckbxregShowPas_CheckedChanged(object sender, EventArgs e)
        {

            if (CheckbxregShowPas.Checked)
            {
                pass.UseSystemPasswordChar = true; }
            else
            {
                pass.UseSystemPasswordChar = false;
            }
        }

        private void surname_MouseClick(object sender, MouseEventArgs e)
        {
            name.BackColor = System.Drawing.Color.White;
            button2.BackColor = System.Drawing.Color.White;
            email.BackColor = System.Drawing.Color.White;
            pass.BackColor = System.Drawing.Color.White;
            usrname.BackColor = System.Drawing.Color.White;
            surname.BackColor = System.Drawing.Color.Thistle;
        }

        private void usrname_MouseClick(object sender, MouseEventArgs e)
        {
            name.BackColor = System.Drawing.Color.White;
            button2.BackColor = System.Drawing.Color.White;
            email.BackColor = System.Drawing.Color.White;
            pass.BackColor = System.Drawing.Color.White;
            usrname.BackColor = System.Drawing.Color.Thistle;
            surname.BackColor = System.Drawing.Color.White;
        }

        private void pass_MouseClick(object sender, MouseEventArgs e)
        {
            name.BackColor = System.Drawing.Color.White;
            button2.BackColor = System.Drawing.Color.White;
            email.BackColor = System.Drawing.Color.White;
            pass.BackColor = System.Drawing.Color.Thistle;
            usrname.BackColor = System.Drawing.Color.White;
            surname.BackColor = System.Drawing.Color.White;
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            label8.Text = name.Text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (name.Text == string.Empty || email.Text == string.Empty || surname.Text == string.Empty || usrname.Text == string.Empty || pass.Text == string.Empty)
            {
                MessageBox.Show("Please fill in all the blanks", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var options = new CustomerCreateOptions

                    {   
                        Name = name.Text,
                        Email = email.Text,
                    };
                    var requestOptions = new RequestOptions();
                    requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";

                    var service = new CustomerService();
                    var customer = service.Create(options,requestOptions);
                    textbox_cusıd.Text = customer.Id.ToString();

                    //Sql database customercreate
                    string sorgu = "INSERT INTO Customers(Name,Surname,email,Username,Password,CustomerId) values('" + name.Text + "','" + surname.Text + "','" + email.Text + "','" + usrname.Text + "','" + pass.Text + "','" + textbox_cusıd.Text + "')";
                    komut = new SqlCommand(sorgu, baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                   
                    MessageBox.Show(" Customer registration successful.", "Successful ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    verilerigetir();
                    clearall();
                    addcreditscreen();
                }

                catch (StripeException ex)
                {
                    switch (ex.StripeError.Type)
                    {
                        case "card error":
                            MessageBox.Show("A payment error occured:{error.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "invalid_request_error":
                            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        default:
                            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }

                }
                //stripe customercreate//

            }

        }
    }
    }
       
      