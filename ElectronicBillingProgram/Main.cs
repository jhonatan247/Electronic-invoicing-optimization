using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using DevComponents.DotNetBar.Metro;
using GeneralBusiness;

namespace ElectronicBillingProgram
{
    public partial class Main : MetroForm
    {
        private int currentIdInsert;
        private int currentIdXML;
        private int currentIdPDF;
        private List<RequestModel> requests;
        private Queue<RequestModel> requestsValidator1;
        private Queue<RequestModel> requestsValidator2;
        private Queue<RequestModel> requestsValidator3;
        private Queue<RequestModel> requestsStep2;
        private Queue<RequestModel> requestsStep3;
        private Queue<RequestModel> requestsStep4;
        private Queue<RequestModel> requestsStep5;

        

        public Main()
        {
            InitializeComponent();
            currentIdInsert = 1;
            currentIdXML = 0;
            currentIdPDF = 0;
            requests = new List<RequestModel>();
            requestsValidator1 = new Queue<RequestModel>();
            requestsValidator2 = new Queue<RequestModel>();
            requestsValidator3 = new Queue<RequestModel>();
            requestsStep2 = new Queue<RequestModel>();
            requestsStep3 = new Queue<RequestModel>();
            requestsStep4 = new Queue<RequestModel>();
            requestsStep5 = new Queue<RequestModel>();
            ThreadStart Validator1Delegate = new ThreadStart(validator1);
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
                if (request.Type == RequestType.PDF)
                    dataGridViewPDF.Rows.Add(request.RequestIndex.ToString(), request.Value, request.Status);
                else
                    dataGridViewXML.Rows.Add(request.RequestIndex.ToString(), request.Value, request.Status);

                if(requestsValidator1.Count<= requestsValidator2.Count && requestsValidator1.Count <= requestsValidator3.Count)
                    requestsValidator1.Enqueue(request);
                else if (requestsValidator2.Count <= requestsValidator1.Count && requestsValidator2.Count <= requestsValidator3.Count)
                    requestsValidator2.Enqueue(request);
                else
                    requestsValidator3.Enqueue(request);
            }
        }

        private void validator1() {
            Queue<RequestModel> requests = requestsValidator1;
            RequestModel current;
            while (true) {
                if (requests.Count > 0) {
                    current = requests.Peek();
                    current.Invoice.State = InvoiceState.confirmed;
                }
            }
        }
        private void validator2(Queue<RequestModel> requests)
        {
            RequestModel current;
            while (true)
            {
                if (requests.Count > 0)
                {
                    current = requests.Peek();
                    current.Invoice.State = InvoiceState.confirmed;
                }
            }
        }
        private void validator3(Queue<RequestModel> requests)
        {
            RequestModel current;
            while (true)
            {
                if (requests.Count > 0)
                {
                    current = requests.Peek();
                    current.Invoice.State = InvoiceState.confirmed;
                }
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
                    newRequests.Add(new RequestModel(RequestType.XML, currentIdInsert++, currentIdXML++, xml));
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
                    newRequests.Add(new RequestModel(RequestType.PDF, currentIdInsert++, currentIdPDF++, path));
                }
                addRequests(newRequests);
            }
        }

        private void request_StatusChanged(object sender) {
            RequestModel request = (RequestModel)sender;
            if (request.Type == RequestType.PDF) {
                dataGridViewPDF[2, request.TypeIndex].Value = request.Status;
                dataGridViewPDF.UpdateCellValue(2, request.TypeIndex);
            }
        }

    }
}
