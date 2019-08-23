namespace Devices.Print
{
    partial class LabReportA4Data
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrRemark = new DevExpress.XtraReports.UI.XRLabel();
            this.xrResult = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlbRangeText = new DevExpress.XtraReports.UI.XRLabel();
            this.xrUnit = new DevExpress.XtraReports.UI.XRLabel();
            this.xrItemName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrHjcxm = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHjgz = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHdw = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHckzfw = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHbz = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHjg = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportDataSet1 = new Devices.Print.ReportDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRemark,
            this.xrResult,
            this.xrlbRangeText,
            this.xrUnit,
            this.xrItemName,
            this.xrPictureBox1});
            this.Detail.HeightF = 35.58334F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrRemark
            // 
            this.xrRemark.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TestingResults.Remark"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "TestingResults.IntRange")});
            this.xrRemark.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrRemark.LocationFloat = new DevExpress.Utils.PointFloat(475F, 6.583336F);
            this.xrRemark.Name = "xrRemark";
            this.xrRemark.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrRemark.SizeF = new System.Drawing.SizeF(93.45776F, 23F);
            this.xrRemark.StylePriority.UseFont = false;
            this.xrRemark.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Range_BeforePrint);
            // 
            // xrResult
            // 
            this.xrResult.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TestingResults.ResultValue"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "TestingResults.IntRange")});
            this.xrResult.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrResult.LocationFloat = new DevExpress.Utils.PointFloat(212.1667F, 6.583336F);
            this.xrResult.Name = "xrResult";
            this.xrResult.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrResult.SizeF = new System.Drawing.SizeF(76.83325F, 23F);
            this.xrResult.StylePriority.UseFont = false;
            this.xrResult.StylePriority.UseTextAlignment = false;
            this.xrResult.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrResult.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Range_BeforePrint);
            // 
            // xrlbRangeText
            // 
            this.xrlbRangeText.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TestingResults.RangeText"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "TestingResults.IntRange")});
            this.xrlbRangeText.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlbRangeText.LocationFloat = new DevExpress.Utils.PointFloat(353F, 6.583336F);
            this.xrlbRangeText.Name = "xrlbRangeText";
            this.xrlbRangeText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlbRangeText.SizeF = new System.Drawing.SizeF(122F, 23F);
            this.xrlbRangeText.StylePriority.UseFont = false;
            this.xrlbRangeText.StylePriority.UseTextAlignment = false;
            this.xrlbRangeText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrlbRangeText.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Range_BeforePrint);
            // 
            // xrUnit
            // 
            this.xrUnit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TestingResults.Unit"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "TestingResults.IntRange")});
            this.xrUnit.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrUnit.LocationFloat = new DevExpress.Utils.PointFloat(288.9999F, 6.583336F);
            this.xrUnit.Name = "xrUnit";
            this.xrUnit.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrUnit.SizeF = new System.Drawing.SizeF(64.00006F, 23F);
            this.xrUnit.StylePriority.UseFont = false;
            this.xrUnit.StylePriority.UseTextAlignment = false;
            this.xrUnit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrUnit.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Range_BeforePrint);
            // 
            // xrItemName
            // 
            this.xrItemName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TestingResults.ItemName"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "TestingResults.IntRange")});
            this.xrItemName.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrItemName.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6.583336F);
            this.xrItemName.Name = "xrItemName";
            this.xrItemName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrItemName.SizeF = new System.Drawing.SizeF(212.1666F, 23F);
            this.xrItemName.StylePriority.UseFont = false;
            this.xrItemName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Range_BeforePrint);
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "TestingResults.Image")});
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(568.4578F, 6.583336F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(182.5002F, 23F);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 2F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 1F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.ReportHeader.HeightF = 25F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable2
            // 
            this.xrTable2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(750.9583F, 25F);
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrHjcxm,
            this.xrHjgz,
            this.xrHdw,
            this.xrHckzfw,
            this.xrHbz,
            this.xrHjg});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 11.5D;
            // 
            // xrHjcxm
            // 
            this.xrHjcxm.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xrHjcxm.Name = "xrHjcxm";
            this.xrHjcxm.StylePriority.UseFont = false;
            this.xrHjcxm.Text = "检查项目";
            this.xrHjcxm.Weight = 0.97347637905755624D;
            // 
            // xrHjgz
            // 
            this.xrHjgz.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrHjgz.Name = "xrHjgz";
            this.xrHjgz.StylePriority.UseFont = false;
            this.xrHjgz.Text = "结果值";
            this.xrHjgz.Weight = 0.35253155438395056D;
            // 
            // xrHdw
            // 
            this.xrHdw.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrHdw.Name = "xrHdw";
            this.xrHdw.StylePriority.UseFont = false;
            this.xrHdw.Text = "单位";
            this.xrHdw.Weight = 0.293648823023762D;
            // 
            // xrHckzfw
            // 
            this.xrHckzfw.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrHckzfw.Name = "xrHckzfw";
            this.xrHckzfw.StylePriority.UseFont = false;
            this.xrHckzfw.Text = "参考值范围";
            this.xrHckzfw.Weight = 0.55976804165505734D;
            // 
            // xrHbz
            // 
            this.xrHbz.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrHbz.Name = "xrHbz";
            this.xrHbz.StylePriority.UseFont = false;
            this.xrHbz.Text = "备注";
            this.xrHbz.Weight = 0.428809613807728D;
            // 
            // xrHjg
            // 
            this.xrHjg.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrHjg.Name = "xrHjg";
            this.xrHjg.StylePriority.UseFont = false;
            this.xrHjg.Text = "结果";
            this.xrHjg.Weight = 0.83735957019221519D;
            // 
            // reportDataSet1
            // 
            this.reportDataSet1.DataSetName = "ReportDataSet";
            this.reportDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // LabReportA4Data
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.DataMember = "TestingResults";
            this.DataSource = this.reportDataSet1;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 2, 1);
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrHjcxm;
        private DevExpress.XtraReports.UI.XRTableCell xrHdw;
        private DevExpress.XtraReports.UI.XRTableCell xrHckzfw;
        private DevExpress.XtraReports.UI.XRTableCell xrHjgz;
        private DevExpress.XtraReports.UI.XRTableCell xrHbz;
        private DevExpress.XtraReports.UI.XRTableCell xrHjg;
        private ReportDataSet reportDataSet1;
        private DevExpress.XtraReports.UI.XRLabel xrRemark;
        private DevExpress.XtraReports.UI.XRLabel xrResult;
        private DevExpress.XtraReports.UI.XRLabel xrlbRangeText;
        private DevExpress.XtraReports.UI.XRLabel xrUnit;
        private DevExpress.XtraReports.UI.XRLabel xrItemName;
    }
}
