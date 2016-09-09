using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApplicationTest
{
    public partial class ShowDataOsbWebAdresForm : Form
    {
        public ShowDataOsbWebAdresForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowDataOsbWebAdresForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12); dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
       
            DataGridViewRow row = this.dataGridView1.RowTemplate;
            row.DefaultCellStyle.BackColor = Color.Bisque;
            row.Height = 32;
            loadData();
        }
        private void loadData()
        {

            HtmlObjects.DataOperations.DataReader.XmlReader xmlRead = new HtmlObjects.DataOperations.DataReader.XmlReader();

            List<OsbWebAddress> list = xmlRead.ReadWebAddressXml("OsbWebAddress");

            var bindingList = new BindingList<OsbWebAddress>(list);

            var source = new BindingSource(bindingList, null);

            dataGridView1.DataSource = source;   
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
