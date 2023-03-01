using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using Stripe;

namespace Credit_Card_Fraud_Detection_Project
{
    public partial class UpdateScreen : Form
    {
        SqlConnection baglanti;
        SqlCommand komut1;
        SqlCommand komut2;

        public UpdateScreen()
        {

            StripeConfiguration.ApiKey = "sk_test_51LrKnIHO7XYPxtMQSdkXp4g5Y7TrJLFLwlBWzBbebdXlfOR6x8fegkmsZSSBiBxgDXHR1168vpvK53KVr6AFeRmz00zMks1hWy";
            baglanti = new SqlConnection("server=.;Initial Catalog=proje;Integrated Security=SSPI");
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            this.Close();
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
        private void UpdateScreen_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            label8.Text = "";
            SqlCommand command = new SqlCommand("select Name,Surname,CustomerId from Customers where Username = '" + textBox1.Text + "'", baglanti);
            SqlDataReader srd = command.ExecuteReader();
            while (srd.Read())
            {
                labelad.Text = srd.GetValue(0).ToString() + " " + srd.GetValue(1).ToString();
                ad.Text = srd.GetValue(0).ToString();
                surname.Text = srd.GetValue(1).ToString();
                cusıd.Text = srd.GetValue(2).ToString();
                

            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand command2 = new SqlCommand("select CardNo,Cvv,ExpMonth,ExpYear,CardId from Cards where CusId = '" + cusıd.Text + "'", baglanti);
            SqlDataReader srd2 = command2.ExecuteReader();
            while (srd2.Read())
            {
                
                ccno.Text = srd2.GetValue(0).ToString();
                cvv.Text = srd2.GetValue(1).ToString();
                month.Text = srd2.GetValue(2).ToString();
                year.Text = srd2.GetValue(3).ToString();
                cardıd.Text = srd2.GetValue(4).ToString();


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
            webView1.Url = "https://dashboard.stripe.com/test/connect/accounts/acct_1LsSSrQjKkdK0c1D/customers/" + cusıd.Text;
        }
        private void label9_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginScreen FORM4gecis = new LoginScreen();
            FORM4gecis.ShowDialog();
            this.Close();
        }

        private void cvv_MouseClick(object sender, MouseEventArgs e)
        {
            ad.BackColor = System.Drawing.Color.White;
            ccno.BackColor = System.Drawing.Color.White;
            cvv.BackColor = System.Drawing.Color.Thistle;
            month.BackColor = System.Drawing.Color.White;
            year.BackColor = System.Drawing.Color.White;
        }

        private void Paynow_MouseEnter(object sender, EventArgs e)
        {
            btn_updatee.ForeColor = System.Drawing.Color.DeepPink;
        }


        private void Paynow_MouseLeave(object sender, EventArgs e)
        {
            btn_updatee.ForeColor = System.Drawing.Color.White;
        }
        private void backto()
        {
            this.Hide();
            LoginScreen FORM4gecis = new LoginScreen();
            FORM4gecis.ShowDialog();
            this.Close();

        }
        private void lblbacktologın_Click(object sender, EventArgs e)
        {
            backto();
        }

        private void labelad_Click(object sender, EventArgs e)
        {

        }

        private void ad_TextChanged(object sender, EventArgs e)
        {
            labelad.Text = ad.Text + "  " + surname.Text;
        }

        private void surname_TextChanged(object sender, EventArgs e)
        {
            labelad.Text = ad.Text + "  " + surname.Text;
        }

        private void ccno_KeyUp(object sender, KeyEventArgs e)
        {
            labelkartno.Text = ccno.Text;
        }

        private void ccno_TextChanged(object sender, EventArgs e)
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

        private void cvv_TextChanged(object sender, EventArgs e)
        {
            label7.Text = cvv.Text;
        }

        private void month_TextChanged(object sender, EventArgs e)
        {
            labelskt.Text = month.Text + "/" + year.Text;
        }

        private void year_TextChanged(object sender, EventArgs e)
        {
            labelskt.Text = month.Text + "/" + year.Text;
        }

        private void Btn_updatee_Click(object sender, EventArgs e)
        {

            try
            {
                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";
                var options = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Name = ad.Text,
                        Number = ccno.Text,
                        ExpMonth = month.Text,
                        ExpYear = year.Text,
                        Cvc = cvv.Text,
                    },
                };

                var service2 = new TokenService();
                Token stripetoken = service2.Create(options);
                tokenıd.Text = stripetoken.Id;

                var options3 = new CardCreateOptions
                {
                    Source = tokenıd.Text,
                };

                var service1 = new CardService();
                service1.Delete(
                cusıd.Text,
                cardıd.Text,null, requestOptions);
                
                var service3 = new CardService();
                Card stripecard = service3.Create(cusıd.Text, options3,requestOptions);
                cardıd.Text = stripecard.Id;


                string sorgu2 = " Update Cards set CardNo=@number,Cvv=@cvv,ExpMonth=@expmonth,ExpYear=@expyear,CardId=@Cardıd,TokenId=@Tokenıd where CusId=@cusıd";
                komut2 = new SqlCommand(sorgu2, baglanti);

                komut2.Parameters.AddWithValue("@Tokenıd", tokenıd.Text);
                komut2.Parameters.AddWithValue("@Cardıd", cardıd.Text);
                komut2.Parameters.AddWithValue("@cusıd", cusıd.Text);
                komut2.Parameters.AddWithValue("@number", ccno.Text);
                komut2.Parameters.AddWithValue("@cvv", cvv.Text);
                komut2.Parameters.AddWithValue("@expmonth", month.Text);
                komut2.Parameters.AddWithValue("@expyear", year.Text);
                baglanti.Open();
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("The update was successful.", "Successful ", MessageBoxButtons.OK, MessageBoxIcon.Information);


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


        private void btn_pay_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = long.Parse(amounttxtbx.Text) * 100,
                    Currency = "usd",
                    Customer = cusıd.Text,
                    PaymentMethod = cardıd.Text
,
                };
                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";

                var service = new PaymentIntentService();
                var paymentıntent = service.Create(options,requestOptions);
                paymentıntent_ıd.Text = paymentıntent.Id;



                label8.ForeColor = Color.Purple;
                label8.Text = "The payment has been created.";
                Task.Delay(4000);

                var options2 = new PaymentIntentConfirmOptions
                {
                    PaymentMethod = cardıd.Text,
                    ReceiptEmail = emailtxtbox.Text,
                };
               

                var service2 = new PaymentIntentService();
                service.Confirm(paymentıntent_ıd.Text, options2,requestOptions);
                label8.Text = "The payment has been confirmed.";
                label8.ForeColor = Color.Green;

            }
            
            catch (StripeException ex)
            {
                switch (ex.StripeError.Type)
                {
                    case "card error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    case "invalid_request_error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    
                    default:
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                }

            }

        }

        private void btn_charge_Click(object sender, EventArgs e)
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
                var service2 = new TokenService();
                Token stripetoken = service2.Create(options);
                tokenıd_charge.Text = stripetoken.Id;

                var options3 = new ChargeCreateOptions
                {
                    Amount = long.Parse(amounttxtbx.Text) * 100,
                    Currency = "usd",
                    Source = tokenıd_charge.Text,


                };
                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";

                var service3 = new ChargeService();
                Charge charget = service3.Create(options3, requestOptions);
                chargetxt.Text = charget.Id;
                label8.Text = "The charge has been confirmed.";
                label8.ForeColor = Color.Green;

                

            }



            catch (StripeException ex)
            {
                switch (ex.StripeError.Type)
                {
                    case "card error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    case "invalid_request_error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    default:
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                }

            }
        }

        private void delete_cus_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu = "DELETE FROM Customers WHERE customerId=@cusıd";
                komut1 = new SqlCommand(sorgu, baglanti);
                komut1.Parameters.AddWithValue("@cusıd", cusıd.Text);
                string sorgu2 = "DELETE FROM Cards WHERE CusId=@cusıd";
                komut2 = new SqlCommand(sorgu2, baglanti);
                komut2.Parameters.AddWithValue("@cusıd", cusıd.Text);
                baglanti.Open();
                komut2.ExecuteNonQuery();
                komut1.ExecuteNonQuery();
                baglanti.Close();

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";

                var service = new CustomerService();
                service.Delete(cusıd.Text,null,requestOptions);
                MessageBox.Show("The deletion was successful.", "Successful ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearall();
                backto();
            }
            catch (StripeException ex)
            {
                switch (ex.StripeError.Type)
                {
                    case "card error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    case "invalid_request_error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    default:
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                }

            }

        }

        private void Transfer_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new TransferCreateOptions
                {
                    Amount = long.Parse(amounttxtbx.Text) * 100,
                    Currency = "usd",
                    Destination = "acct_1LsSSrQjKkdK0c1D",
                    //SourceTransaction=chargetxt.Text,
                    TransferGroup = "ORDER_95",
                    
                };
                label8.Text = "The Transfer has been confirmed.";
                label8.ForeColor = Color.Green;
                

                var service = new TransferService();
                var transfer = service.Create(options);
                trıdtxtbx.Text = transfer.Id.ToString();
                lastvalue.Text = amounttxtbx.Text;
                TransferReversal.Visible = true;
            }
            catch (StripeException ex)
            {
                switch (ex.StripeError.Type)
                {
                    case "card error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    case "invalid_request_error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    default:
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                }
            }

        }
        private void Withdraw_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new TransferReversalCreateOptions
                {
                    Amount = long.Parse(amounttxtbx.Text) * 100,
                };
                var service = new TransferReversalService();
                service.Create(trıdtxtbx.Text,options);
                label8.Text = "TransferReversal is successful";
                
                label8.ForeColor = Color.Green;
                TransferReversal.Visible = false;
            }


            catch (StripeException ex)
            {
                switch (ex.StripeError.Type)
                {
                    case "card error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    case "invalid_request_error":
                        label8.Text = "This transfer only has $" + lastvalue.Text + ".00 remaining to reverse. We cannot reverse $" + amounttxtbx.Text + ".00";
                        label8.ForeColor = Color.Red;
                        break;
                    case "transfers_not_allowed":
                        label8.Text = "Transfer not allowed";
                        label8.ForeColor = Color.Red;
                        break;
                    default:
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                }
            }
        }

        private void Payout_Click(object sender, EventArgs e)
        {
            try
            {
                

                var options = new PayoutCreateOptions
                {
                    Amount = long.Parse(amounttxtbx.Text) * 100,
                    Currency = "usd",
                    Destination=cardıd.Text,
                };
                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";

                var service = new PayoutService();
                Payout payoutt =service.Create(options,requestOptions);
                payout_txt.Text = payoutt.Id;
            }
            catch (StripeException ex)
            {
                switch (ex.StripeError.Type)
                {
                    case "card error":
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                    case "invalid_request_error":
                        label8.Text = "This transfer only has $" + lastvalue.Text + ".00 remaining to reverse. We cannot reverse $" + amounttxtbx.Text + ".00";
                        label8.ForeColor = Color.Red;
                        break;
                    case "transfers_not_allowed":
                        label8.Text = "Transfer not allowed";
                        label8.ForeColor = Color.Red;
                        break;
                    default:
                        label8.ForeColor = Color.Purple;
                        label8.Text = ex.Message;
                        break;
                }
            }
            }
    }    
    }

