using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using GeneralBusiness;

namespace ElectronicBillingProgram
{
    public partial class Main : MetroForm
    {
        private int CurrentId;
        private List<RequestModel> requests;

        public Main()
        {
            InitializeComponent();
            CurrentId = 1;
            requests = new List<RequestModel>();
            //textBoxX1.Text =(new Guid()).ToString();
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
        private void addRequests(List<RequestModel> newRequests) {
            foreach (RequestModel request in newRequests) {
                if (request.type == RequestType.PDF)
                    dataGridViewPDF.Rows.Add(request.id.ToString(), request.value, request.status);
                else
                    dataGridViewXML.Rows.Add(request.id.ToString(), request.value, request.status);
            }
        }
        private void btnAddXML_Click(object sender, EventArgs e)
        {
            String[] paths = getFiles("XML");
            if (paths != null)
            {
                List<RequestModel> newRequests = new List<RequestModel>();
                String xml = "";
                foreach (String path in paths)
                {
                    FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                    xml = new StreamReader(file).ReadToEnd();
                    newRequests.Add(new RequestModel(RequestType.XML, CurrentId++, xml));
                }
                addRequests(newRequests);
            }
        }

        private void btnAddPDF_Click(object sender, EventArgs e)
        {
            String[] paths = getFiles("PDF");
            if (paths != null)
            {
                List<RequestModel> newRequests = new List<RequestModel>();
                foreach (String path in paths)
                {
                    newRequests.Add(new RequestModel(RequestType.PDF, CurrentId++, path));
                }
                addRequests(newRequests);
            }
        }
    }
}
