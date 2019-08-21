using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace DevicesTest
{
    public partial class FormResult : Form
    {
        public FormResult()
        {
            InitializeComponent();
            
        }

        private void FormResult_Load(object sender, EventArgs e)
        {
            HttpResult<List<string>> httpResult = WebLogic.GetHttpResult<List<string>>(new HttpItem { Url = "http://140.143.203.114:80/api/result/all", Method = HttpMethod.Get });
            if (httpResult.Success)
            {
                if (httpResult.Result != null)
                {
                    List<Result> results = httpResult.Result.Select<string, Result>(o=> JsonConvert.DeserializeObject<Result>(o)).ToList();
                    this.dataGridView1.DataSource = results;
                }
            }
        }
    }
}
