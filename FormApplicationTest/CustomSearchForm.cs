using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApplicationTest
{
    public partial class CustomSearchForm : Form
    {
        Thread thread = null;
        public CustomSearchForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(load));
            thread.Start();
        }
        private void load()
        {

            string tag = txtTag.Text;
            string url = txtURL.Text;
            HtmlObjects.DataOperations.DataReader.HtmlPageBuilder builder = new HtmlObjects.DataOperations.DataReader.HtmlPageFromWebBuilder();
            HtmlObjects.ServiceOperations.FirmService service = new HtmlObjects.ServiceOperations.FirmService(builder, url, tag);
            List<Firm> list = service.getFirmList();
            loadView(list);
        }

        private void loadView(List<Firm> firmList)
        {
            try
            {
                if (dataGridView1.DataSource != null)
                {
                    dataGridView1.DataSource = null;
                }

                var list = firmList;
                var bindingList = new BindingList<Firm>(list);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }



            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(thread != null)
            {
                if (thread.IsAlive)
                {
                    try
                    {
                        thread.Suspend();
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            this.Close();
        }
    }
}
