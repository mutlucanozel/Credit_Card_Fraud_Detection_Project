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
    public partial class ProductPage : Form
    {

        SqlConnection baglanti;
        SqlCommand komut1;
        SqlDataAdapter da;
        public ProductPage()
        {

            StripeConfiguration.ApiKey = "sk_test_51LrKnIHO7XYPxtMQSdkXp4g5Y7TrJLFLwlBWzBbebdXlfOR6x8fegkmsZSSBiBxgDXHR1168vpvK53KVr6AFeRmz00zMks1hWy";
            baglanti = new SqlConnection("server=.;Initial Catalog=proje;Integrated Security=SSPI");
            InitializeComponent();
        }
        void getproduct()
        {
            baglanti.Open();
            da = new SqlDataAdapter("SELECT *FROM Products", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();


        }

        private void Product_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projeDataSet.Products' table. You can move, or remove it, as needed.
            //this.productsTableAdapter.Fill(this.projeDataSet.Products);
            getproduct();

        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            StripeConfiguration.ApiKey = "sk_test_51LrKnIHO7XYPxtMQSdkXp4g5Y7TrJLFLwlBWzBbebdXlfOR6x8fegkmsZSSBiBxgDXHR1168vpvK53KVr6AFeRmz00zMks1hWy";

            try
            {
                var options = new ProductCreateOptions
                {
                    Name = name_txt.Text,
                };
                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";

                var service = new ProductService();
                Product prod = service.Create(options, requestOptions);
                Prodıd.Text = prod.Id;

                var options2 = new PriceCreateOptions
                {
                    UnitAmount = long.Parse(Price_txt.Text) * 100,
                    Currency = "usd",
                    Recurring = new PriceRecurringOptions
                    {
                        //Interval = "month",
                    },
                    Product = Prodıd.Text,
                };
                var service2 = new PriceService();
                Price priceq = service2.Create(options2, requestOptions);
                price.Text = priceq.Id;


                string sorgu = "INSERT INTO Products(Name,Description,ProdId,PriceId,Price) values('" + name_txt.Text + "','" + describ_txt.Text + "','" + Prodıd.Text + "','" + price.Text + "','" + Price_txt.Text + "')";
                baglanti.Open();
                komut1 = new SqlCommand(sorgu, baglanti);
                komut1.ExecuteNonQuery();
                MessageBox.Show("The product has been successfully created.", "Successful ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglanti.Close();
                getproduct();


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

        private void lblbacktologın_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginScreen FORMgecis = new LoginScreen();
            FORMgecis.ShowDialog();
            this.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            name_upd_txt.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            descrb_upd_txt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            price_updt_txt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            prodıdupt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            priceıdupd.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu2 = "DELETE FROM Products WHERE ProdId=@prodı";
                komut1 = new SqlCommand(sorgu2, baglanti);
                komut1.Parameters.AddWithValue("@prodı", prodıdupt.Text);
                baglanti.Open();
                komut1.ExecuteNonQuery();
                baglanti.Close();
                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";
               
                var service = new ProductService();
                service.Delete(prodıdupt.Text, null, requestOptions);
                getproduct();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu3 = "UPDATE Products SET Name=@namee,Description=@desc,Price=@pricee WHERE ProdId=@prodıdı";
                komut1 = new SqlCommand(sorgu3, baglanti);
                komut1.Parameters.AddWithValue("@prodıdı", prodıdupt.Text);
                komut1.Parameters.AddWithValue("@namee", name_upd_txt.Text);
                komut1.Parameters.AddWithValue("@desc", descrb_upd_txt.Text);
                komut1.Parameters.AddWithValue("@pricee", price_updt_txt.Text);
                baglanti.Open();
                komut1.ExecuteNonQuery();
                baglanti.Close();
                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = "acct_1LsSSrQjKkdK0c1D";
                var options = new ProductUpdateOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        { "order_id", "6735" },
                    },
                    Name = name_upd_txt.Text,
                    Description=descrb_upd_txt.Text,
                    DefaultPrice= priceıdupd.Text,
                };
                var service = new ProductService();
                service.Update(prodıdupt.Text, options,requestOptions);
                var options2 = new PriceCreateOptions
                {
                    UnitAmount = long.Parse(price_updt_txt.Text) * 100,
                    Currency = "usd",
                    Recurring = new PriceRecurringOptions
                    {
                        //Interval = "month",
                    },
                    Product = prodıdupt.Text,
                };
                var service2 = new PriceService();
                Price priceq = service2.Create(options2, requestOptions);

                getproduct();
                MessageBox.Show("The product has been successfully Updated.", "Successful ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
}
