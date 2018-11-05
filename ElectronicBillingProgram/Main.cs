using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using DevComponents.DotNetBar.Metro;
using GeneralBusiness;
using Server;
using Data.CRUD;
using Data.CRUD;

namespace ElectronicBillingProgram
{
    public partial class Main : MetroForm
    {
        RequestServer server;
        public Main()
        {
            InitializeComponent();
            server = new RequestServer();
            Timer.Start();
        }
        private String[] getFiles(String type) {
            OpenFileDialog OpenMedia = new OpenFileDialog();
            OpenMedia.InitialDirectory = Environment.SpecialFolder.CommonMusic.ToString();
            OpenMedia.Title = "Select file";
            OpenMedia.Multiselect = true;
            OpenMedia.Filter = type.ToUpper() + " File (*." + type.ToUpper() + ")|*." + type.ToUpper();
            if (OpenMedia.ShowDialog() == DialogResult.OK)
            {
                return OpenMedia.FileNames;
            }
            return null;
        }

        private void btnAddXML_Click(object sender, EventArgs e)
        {
            String[] paths = getFiles("XML");
            if (paths != null)
            {
                List<InvoiceModel> newRequests = new List<InvoiceModel>();
                String xml = "";
                foreach (String path in paths)
                {
                    FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                    xml = new StreamReader(file).ReadToEnd();
                    newRequests.Add(new InvoiceModel(RequestType.XML, xml));
                }
                server.addRequests(newRequests);
            }
        }

        private void btnAddPDF_Click(object sender, EventArgs e)
        {
            String[] paths = getFiles("PDF");
            if (paths != null)
            {
                List<InvoiceModel> newRequests = new List<InvoiceModel>();
                foreach (String path in paths)
                {
                    newRequests.Add(new InvoiceModel(RequestType.PDF, path));
                }
                server.addRequests(newRequests);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dataGridViewStep1.DataSource = Request.SelectInterface();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Timer.Enabled = !Timer.Enabled;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Request.ClearData();
            try
            {
                Application.Restart();
            }
            catch { }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            server.CompleteProcess();
        }
    }
}
