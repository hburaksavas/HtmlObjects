using HtmlObjects.BusinessOperations.POCO;
using HtmlObjects.DataOperations.DbOperations.Entities;
using HtmlObjects.ServiceOperations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;


namespace FormApplicationTest
{
    public partial class MainForm : Form
    {
        Thread mainThread = null;
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            panel1.Controls.Clear();

            AddOsbForm osbForm = new AddOsbForm();
            osbForm.TopLevel = false;
            panel1.Controls.Add(osbForm);
            osbForm.Show();
            osbForm.Dock = DockStyle.Top;
            osbForm.BringToFront();

        }

        private void btnLoadOsbUrls_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            ShowDataOsbWebAdresForm osbForm = new ShowDataOsbWebAdresForm();

            osbForm.TopLevel = false;
            panel1.Controls.Add(osbForm);
            osbForm.Show();
            osbForm.Dock = DockStyle.Top;
            osbForm.BringToFront();

        }

        private void buttonCustomSearch_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            CustomSearchForm osbForm = new CustomSearchForm();
            osbForm.TopLevel = false;
            panel1.Controls.Add(osbForm);
            osbForm.Show();
            osbForm.Dock = DockStyle.Top;
            osbForm.BringToFront();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            mainThread = new Thread(new ThreadStart(Search));
            mainThread.Start();
        }
        private void Search()
        {
            ComparisonService service = new ComparisonService();

            List<UnmatchedFirm> list = service.GetUnmatched_FirmList();


            foreach (var item in list)
            {
                //richTextBox1.AppendText(item.Unvan + "\n");
            }

            OsbDBService writer = new OsbDBService();

            foreach (var item in list)
            {
                writer.SaveData(item);
            }

            List<MatchedFirm> matchedFirms = service.GetMatched_FirmList();

            foreach (var item in matchedFirms)
            {
                writer.SaveData(item);
            }

            //richTextBox1.AppendText("Eşleşmeyen Firmalar Veritabanına yazıldı");
        }
    }
}
