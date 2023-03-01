using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Stripe;


namespace Credit_Card_Fraud_Detection_Project
{
    public partial class PaymentScreen : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        

        public PaymentScreen()
        {
            StripeConfiguration.ApiKey = "sk_test_51LrKnIHO7XYPxtMQSdkXp4g5Y7TrJLFLwlBWzBbebdXlfOR6x8fegkmsZSSBiBxgDXHR1168vpvK53KVr6AFeRmz00zMks1hWy";
            baglanti = new SqlConnection("server=.;Initial Catalog=proje;Integrated Security=SSPI");
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            labelad.Text = ad.Text +"  "+ surname.Text;

        }

        private void ccno_KeyUp(object sender, KeyEventArgs e)
        {
            labelkartno.Text = ccno.Text;
        }

        private void month_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelskt.Text = month.Text + "/" + year.Text;

        }

        private void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelskt.Text = month.Text + "/" + year.Text;
        }

       

       
     
        private void cvv_KeyUp(object sender, KeyEventArgs e)
        {
            label7.Text = cvv.Text;
        }

        private void paybtn_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void ad_MouseClick(object sender, MouseEventArgs e)
        {
            
            ad.BackColor = System.Drawing.Color.Thistle;
            surname.BackColor = System.Drawing.Color.White;
            ccno.BackColor = System.Drawing.Color.White;
            cvv.BackColor = System.Drawing.Color.White;
            month.BackColor = System.Drawing.Color.White;
            year.BackColor = System.Drawing.Color.White;
            
        }

        private void ccno_MouseClick(object sender, MouseEventArgs e)
        {
            ad.BackColor = System.Drawing.Color.White;
            surname.BackColor = System.Drawing.Color.White;
            ccno.BackColor = System.Drawing.Color.Thistle;
            cvv.BackColor = System.Drawing.Color.White;
            month.BackColor = System.Drawing.Color.White;
            year.BackColor = System.Drawing.Color.White;
        }

        private void month_MouseClick(object sender, MouseEventArgs e)
        {
            ad.BackColor = System.Drawing.Color.White;
            surname.BackColor = System.Drawing.Color.White;
            ccno.BackColor = System.Drawing.Color.White;
            cvv.BackColor = System.Drawing.Color.White;
            month.BackColor = System.Drawing.Color.Thistle;
            year.BackColor = System.Drawing.Color.White;
        }

        private void year_MouseClick(object sender, MouseEventArgs e)
        {
            ad.BackColor = System.Drawing.Color.White;
            surname.BackColor = System.Drawing.Color.White;
            ccno.BackColor = System.Drawing.Color.White;
            cvv.BackColor = System.Drawing.Color.White;
            month.BackColor = System.Drawing.Color.White;
            year.BackColor = System.Drawing.Color.Thistle;
        }

      
        private void labelkartno_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            labelad.Text = ad.Text;
        }

        private void maskedTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            labelcvv.Text = cvv.Text;
        }

       

        

        private void PaymentScreen_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("select Name,Surname from Customers where CustomerId = '" + cusıd.Text + "'", baglanti);
            SqlDataReader srd = command.ExecuteReader();
            while (srd.Read())
            {
                labelad.Text = srd.GetValue(0).ToString()+ srd.GetValue(1).ToString();
                ad.Text = srd.GetValue(0).ToString();
                surname.Text = srd.GetValue(1).ToString();

            }
            baglanti.Close();
           
            int ay;
            int yıl;
            for (ay = 1; ay < 13; ay++)
            {
                month.Items.Add(ay);
            }
            for (yıl = 23; yıl < 33; yıl++)
            {
                year.Items.Add(yıl);
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ad_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

      

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ad.MaxLength = 20;
        }

      
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void loginscreen()
        {
            this.Hide();
            LoginScreen FORM4gecis = new LoginScreen();
            FORM4gecis.ShowDialog();
            this.Close();
        }
        private void label9_Click(object sender, EventArgs e)
        {
            loginscreen();
        }

        private void cvv_MouseClick(object sender, MouseEventArgs e)
        {
            ad.BackColor = System.Drawing.Color.White;
            surname.BackColor = System.Drawing.Color.White;
            ccno.BackColor = System.Drawing.Color.White;
            cvv.BackColor = System.Drawing.Color.Thistle;
            month.BackColor = System.Drawing.Color.White;
            year.BackColor = System.Drawing.Color.White;
        }

        private void Paynow_MouseEnter(object sender, EventArgs e)
        {
            Paynow.ForeColor = System.Drawing.Color.DeepPink;
        }

        private void Paynow_MouseLeave(object sender, EventArgs e)
        {
            Paynow.ForeColor = System.Drawing.Color.White;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Paynow_Click(object sender, EventArgs e)
        {
            if (ad.Text == string.Empty || surname.Text == string.Empty || cvv.Text == string.Empty || ccno.Text == string.Empty || year.Text == string.Empty || month.Text == string.Empty)
            {
                MessageBox.Show("Please fill in all the blanks", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    var options = new TokenCreateOptions
                    {
                        Card = new TokenCardOptions
                        {
                            Name = labelad.Text,
                            Number = ccno.Text,
                            ExpMonth = month.Text,
                            ExpYear = year.Text,
                            Cvc = cvv.Text,
                        },
                    };
                    var service = new TokenService();
                    Token stripetoken = service.Create(options);
                    tokenıd.Text = stripetoken.Id;
                    service.Create(options);

                    var options2 = new CardCreateOptions
                    {
                        Source = tokenıd.Text,
                    };
                    var requestOptions = new RequestOptions();
                    requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";
                    var service2 = new CardService();
                    Card stripecard = service2.Create(cusıd.Text, options2,requestOptions);
                    cardıd.Text = stripecard.Id;

                    string sorgu = "INSERT INTO Cards(Cusıd,CardNo,Cvv,ExpMonth,ExpYear,CardId,TokenId) values('" + cusıd.Text + "','" + ccno.Text + "','" + cvv.Text + "','" + month.Text + "','" + year.Text + "','"+ cardıd.Text + "','" + tokenıd.Text + "')";
                    komut = new SqlCommand(sorgu, baglanti);

                    // güncelleme 
                    //string sorgu = " Update Customers set Number=@number,cvv=@cvv,expMonth=@expmonth,expYear=@expyear,TokenId=@TokenId,CardId=@CardId where CustomerId=@cusıd";
                    //komut = new SqlCommand(sorgu, baglanti);
                    //komut.Parameters.AddWithValue("@cusıd", cusıd.Text);
                    //komut.Parameters.AddWithValue("@CardId", cardıd.Text);
                    //komut.Parameters.AddWithValue("TokenId", tokenıd.Text);
                    //komut.Parameters.AddWithValue("@number", ccno.Text);
                    //komut.Parameters.AddWithValue("@cvv", cvv.Text);
                    //komut.Parameters.AddWithValue("@expmonth", month.Text);
                    //komut.Parameters.AddWithValue("@expyear", year.Text);

                    
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Credit card successfully added.", "Successful ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearall();
                    loginscreen();
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

            }
        }

        private void clearall()
        {
            ad.Clear();
            surname.Clear();
            ccno.Clear();
            month.Text = string.Empty;
            year.Text = string.Empty;
            cvv.Clear();
        }        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void surname_TextChanged(object sender, EventArgs e)
        {
            labelad.Text = ad.Text +"  "+ surname.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void ccno_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void surname_MouseClick(object sender, MouseEventArgs e)
        {
            ad.BackColor = System.Drawing.Color.White;
            surname.BackColor = System.Drawing.Color.Thistle;
            ccno.BackColor = System.Drawing.Color.White;
            cvv.BackColor = System.Drawing.Color.White;
            month.BackColor = System.Drawing.Color.White;
            year.BackColor = System.Drawing.Color.White;
        }
    }
   }
