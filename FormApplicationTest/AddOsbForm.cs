using System;
using System.Windows.Forms;
using HtmlObjects.BusinessOperations.XmlOperations;

namespace FormApplicationTest
{
    public partial class AddOsbForm : Form
    {
        public AddOsbForm()
        {
            InitializeComponent();
        }

        private void btnKAPAT_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string url = txtURL.Text;
                string tag = txtTAG.Text;
                if(!String.IsNullOrEmpty(url) && !String.IsNullOrEmpty(tag))
                {
                    WebAddressConfig config = new WebAddressConfig();
                    config.add(url,tag);
                }
                else
                {
                    MessageBox.Show("Url ve html tag değerleri boş olamaz");
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
       
        }

        private void btnIPTAL_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
