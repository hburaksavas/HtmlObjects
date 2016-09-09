using HtmlObjects.BusinessOperations.POCO;
using HtmlObjects.ServiceOperations;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplicationTest
{
    public partial class ExcelDialogForm : Form
    {
        List<Firm> firmList;
        FirmService service;
        public ExcelDialogForm(List<Firm> firmList,FirmService service)
        {
            InitializeComponent();
            this.firmList = firmList;
            this.service = service;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string fileName = textBox1.Text;
            if (String.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("İsim Alanı Boş Bırakılmamalıdır");
            }
            else
            {
                service.SaveAsExcelFile(firmList, fileName);
                this.Close();
            }
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
