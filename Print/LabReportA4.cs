using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Collections.Generic;
using System.IO;
using Devices;

namespace Devices.Print
{
    public partial class LabReportA4 : XtraReport
    {

        private Devices.Result result;

        private string OrgName { get; set; }

        public string PrintClassName => throw new NotImplementedException();

        //LabResultVO entity;

        public LabReportA4(Devices.Result _result)
        {
            InitializeComponent();
            result = _result;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        }

        private void LabReportA4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            //HqLabResultLogic logic = new HqLabResultLogic();
            //HqLabResult entity = logic.GetLabResult(orgId, regid, reportId);
            if (result != null)
            {
                Devices.Command cmd = result.CMD;
                //labNo.Text = orgId.ToString() + reportId.ToString();

                labNo.Text = cmd.Id;
                txtOrgName.Text = OrgName;
                txtCustomerName.Text = cmd.Customer;//.cus_name;
                txtGender.Text = cmd.Gender == Devices.GenderType.Female ? "雄" : "雌";
                txtKindOf.Text = cmd.KindOf.ToString();// labReport.pet_kindof_txt;
                txtPetRecordNum.Text = cmd.PetId;// "";// labReport.record_number;
                txtPetName.Text = cmd.PetName;
                txtPhone.Text = cmd.CustomerId;
                txtVarity.Text = "";// labReport.pet_variety_txt;
                txtWeight.Text = string.Format("{0:F2}", cmd.Weight);

                txtPhysician.Text = cmd.Doctor;// labReport.exec_physician_name;
                txtPrescriptionDate.Text = result.Date.ToString();// Convert.ToDateTime(labReport.reporting_time).ToString("yyyy-MM-dd HH:mm");

                int age = 0;
                if (int.TryParse(cmd.Age, out age))
                {
                    if (age / 12 > 0)
                        txtAge.Text = (age / 12) + "年";
                    txtAge.Text += (age % 12) + "月";
                }

                lblReportName.Text = cmd.Name;
                xrTextRemark.Visible = false;


                //List<HqImgs> imgs = entity.imgs;
                //if (imgs != null && imgs.Count > 0)
                //{
                //    xrSubreportImg.ReportSource = new LabReportVirusA4Img(imgs);
                //}

                //List<HqLabReportDetails> results = entity.labvirus_details;

                  xrSubreportData.ReportSource = new LabReportA4Data(result);
            }
            //TODO:系统1019开关设置
            //if (Warmsoft.ClientUtilities.Utilities.GetPrivilige(1019))
            if (true)
            {
                //Org orgInfo = ((AppResource)HIHSApplication.Resource).OrgInfo;
                //SysClientManagementLogic logic = new SysClientManagementLogic();
                //SysClientInfo clientInfo = logic.GetSysClientInfo(orgInfo.systemId ?? 0, orgInfo.brandId);

                //try
                //{
                //    xrPictureBox3.Image = ImageUtil.DownloadImage(clientInfo.configs.reportLogo);
                //}
                //catch (Exception ex)
                //{

                //}
            }
        }
    }
}
