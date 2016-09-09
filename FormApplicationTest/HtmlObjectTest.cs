using HtmlObjects.BusinessOperations.POCO;
using HtmlObjects.DataOperations.DataReader;
using HtmlObjects.DataOperations.DbOperations.Entities;
using HtmlObjects.ServiceOperations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApplicationTest;

namespace FormApplicationTest
{
    public partial class HtmlObjectTest : Form
    {
  
        FirmService serv;
        List<Firm> FirmList;

        
        public HtmlObjectTest()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.dataGridView.DefaultCellStyle.Font = new Font("Tahoma", 12); dataGridView.AutoResizeColumns();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DataGridViewRow row = this.dataGridView.RowTemplate;
            row.DefaultCellStyle.BackColor = Color.Bisque;
            row.Height = 35;
           

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
            Thread thread = new Thread(new ThreadStart(work));
            thread.Start();
        }

        public void work()
        {
           
            string url = textBoxURL.Text;
            string name = textBoxTAG.Text;

            HtmlObjects.DataOperations.DataReader.HtmlPageBuilder builder = new HtmlObjects.DataOperations.DataReader.HtmlPageFromWebBuilder();

            progressBar1.Value = 15; //progress

            serv = new FirmService(builder, url, name);

            progressBar1.Value = 35; //progress

            List<Firm> firms = serv.getFirmList();

            progressBar1.Value = 80; //progress

            int firmCount = 0;
            List<string> firmList = new List<string>();

            if (firms != null)
                foreach (var item in firms)
                {
                    if (!String.IsNullOrEmpty(item.firmPhone))
                    {
                        firmCount++;
                        richTextBox1.AppendText("\nFirm Name : " + item.firmName);
                        richTextBox1.AppendText("\nFirm Phone : " + item.firmPhone);
                        richTextBox1.AppendText("\nFirm Fax : " + item.firmFax);
                        richTextBox1.AppendText("\nFirm Mail : " + item.firmMail);
                        richTextBox1.AppendText("\nFirm Web Address : " + item.firmWebSite);
                        richTextBox1.AppendText("\n");
                    }

                }

            progressBar1.Value = 90; //progress



            richTextBox1.AppendText(String.Format("{0} Firms Found", firmCount));

            progressBar1.Value = 100; //progress


            LoadView(firms);
            FirmList = firms;

        }

        public void workAll()
        {
            Hashtable table = new Hashtable();
            table["http://gosb.com/?sid=112"] = "strong";
            table["http://geposb.com.tr/tr/firmalar.php"] = "a";
            table["http://ggosb.com.tr/firma-rehberi"] = "a";
            table["http://www.gaosb.org/firmakat.php?id=77"] = "h3";
            table["http://www.gaosb.org/firmakat.php?id=74"] = "h3";
            table["http://www.ibosb.com/TR/firm/"] = "a";
            table["http://geposb.com.tr/tr/firmalar.php"] = "a";

            FirmService firmService = new FirmService();

            List<Firm> firmList = firmService.getFirmList(table); // default web builder

            FirmList = firmList;

            serv = firmService;

           
        }

        public void Compare()
        {
            FirmService service = new FirmService();
            //service.GetUnmatchedFirmList();

        }

        public void LoadView(List<Firm> firmList)
        {
            try
            {
                if (dataGridView.DataSource != null)
                {
                    dataGridView.DataSource = null;
                }

                var list = firmList;
                var bindingList = new BindingList<Firm>(list);
                var source = new BindingSource(bindingList, null);
                dataGridView.DataSource = source;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }



            this.Refresh();
        }
               
        private void HtmlObjectTest_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
            dataGridView.Refresh();
        }

        private void buttonSaveExcel_Click(object sender, EventArgs e)
        {

            if (FirmList != null)
            {
                ExcelDialogForm excel = new ExcelDialogForm(FirmList, serv);
                excel.ShowDialog();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thrad = new Thread(Compare);
            thrad.Start();
           
        }


        private void buttonAllSearch_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(workAll);
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = textBoxURL.Text;
            string tag = textBoxTAG.Text;

            HtmlObjects.BusinessOperations.XmlOperations.WebAddressConfig config = new HtmlObjects.BusinessOperations.XmlOperations.WebAddressConfig();
            config.add(url, tag);
            config.commit();
        }

        private void button3_Click( object sender, EventArgs e )
        {

            Thread thread = new Thread(comparisonService);
            thread.Start();
        }
        void comparisonService()
        {


            Stopwatch voc = new Stopwatch();
            voc.Start();
                        CompareService service = new CompareService();
                        service.Start();

            //ComparisonService service = new ComparisonService();

            //List<UnmatchedFirm> list = service.GetUnmatched_FirmList();

            ////DataTable table = service.GetTable();
            ////dataGridView.DataSource = table;

            ////richTextBox1.AppendText(String.Format("Eşleşmeyen Firma Sayısı {0} \n", list.Count));

            //foreach (var item in list)
            //{
            //    richTextBox1.AppendText(item.Unvan + "\n");
            //}

            //OsbDBService writer = new OsbDBService();

            //foreach (var item in list)
            //{
            //    writer.SaveData(item);
            //}

            //List<MatchedFirm> matchedFirms = service.GetMatched_FirmList();

            //foreach (var item in matchedFirms)
            //{
            //    writer.SaveData(item);
            //}
            voc.Stop();
            Console.WriteLine("SN {0}", voc.Elapsed.TotalSeconds.ToString());


        }

        private void button4_Click(object sender, EventArgs e)
        {
            HtmlPageBuilder builder = new HtmlPageFromFileBuilder();
            FirmService service = new FirmService(builder, @"C:\Users\buraks\Documents\Ege İhracatçı Birlikleri.html", "tr");
        }

    }
}
