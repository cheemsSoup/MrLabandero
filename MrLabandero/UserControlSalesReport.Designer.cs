
namespace MrLabandero
{
    partial class UserControlSalesReport
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelSearchID = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtTransactionID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelYearlyMonthly = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            this.flowPanelSelection = new System.Windows.Forms.FlowLayoutPanel();
            this.rbDaily = new System.Windows.Forms.RadioButton();
            this.rbWeekly = new System.Windows.Forms.RadioButton();
            this.rbMonthly = new System.Windows.Forms.RadioButton();
            this.rbSearchID = new System.Windows.Forms.RadioButton();
            this.panelDaily = new System.Windows.Forms.Panel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgvSalesReport = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnDetailed = new System.Windows.Forms.Button();
            this.lblTotalSales = new System.Windows.Forms.Label();
            this.panelExactTransaction = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.panelSearchID.SuspendLayout();
            this.panelYearlyMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            this.flowPanelSelection.SuspendLayout();
            this.panelDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "SALES REPORT";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.panelSearchID);
            this.panelHeader.Controls.Add(this.label6);
            this.panelHeader.Controls.Add(this.label2);
            this.panelHeader.Controls.Add(this.panelYearlyMonthly);
            this.panelHeader.Controls.Add(this.flowPanelSelection);
            this.panelHeader.Controls.Add(this.panelDaily);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(808, 152);
            this.panelHeader.TabIndex = 4;
            // 
            // panelSearchID
            // 
            this.panelSearchID.Controls.Add(this.btnSearch);
            this.panelSearchID.Controls.Add(this.txtTransactionID);
            this.panelSearchID.Controls.Add(this.label7);
            this.panelSearchID.Controls.Add(this.txtCustomerID);
            this.panelSearchID.Controls.Add(this.label3);
            this.panelSearchID.Location = new System.Drawing.Point(15, 77);
            this.panelSearchID.Name = "panelSearchID";
            this.panelSearchID.Size = new System.Drawing.Size(577, 71);
            this.panelSearchID.TabIndex = 48;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Maroon;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(0, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(189, 37);
            this.btnSearch.TabIndex = 46;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTransactionID
            // 
            this.txtTransactionID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionID.Location = new System.Drawing.Point(366, 2);
            this.txtTransactionID.Name = "txtTransactionID";
            this.txtTransactionID.Size = new System.Drawing.Size(100, 25);
            this.txtTransactionID.TabIndex = 21;
            this.txtTransactionID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTransactionID_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(228, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 25);
            this.label7.TabIndex = 20;
            this.label7.Text = "Transaction ID:";
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerID.Location = new System.Drawing.Point(117, 3);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Size = new System.Drawing.Size(100, 25);
            this.txtCustomerID.TabIndex = 19;
            this.txtCustomerID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerID_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-5, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Customer ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 43);
            this.label6.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "View By:";
            // 
            // panelYearlyMonthly
            // 
            this.panelYearlyMonthly.Controls.Add(this.label5);
            this.panelYearlyMonthly.Controls.Add(this.nudYear);
            this.panelYearlyMonthly.Location = new System.Drawing.Point(15, 75);
            this.panelYearlyMonthly.Name = "panelYearlyMonthly";
            this.panelYearlyMonthly.Size = new System.Drawing.Size(189, 35);
            this.panelYearlyMonthly.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 4);
            this.label5.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "Year:";
            // 
            // nudYear
            // 
            this.nudYear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudYear.Location = new System.Drawing.Point(63, 3);
            this.nudYear.Maximum = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.nudYear.Minimum = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(120, 29);
            this.nudYear.TabIndex = 22;
            this.nudYear.Value = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            // 
            // flowPanelSelection
            // 
            this.flowPanelSelection.Controls.Add(this.rbDaily);
            this.flowPanelSelection.Controls.Add(this.rbWeekly);
            this.flowPanelSelection.Controls.Add(this.rbMonthly);
            this.flowPanelSelection.Controls.Add(this.rbSearchID);
            this.flowPanelSelection.Location = new System.Drawing.Point(92, 42);
            this.flowPanelSelection.Name = "flowPanelSelection";
            this.flowPanelSelection.Size = new System.Drawing.Size(313, 29);
            this.flowPanelSelection.TabIndex = 16;
            // 
            // rbDaily
            // 
            this.rbDaily.AutoSize = true;
            this.rbDaily.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDaily.Location = new System.Drawing.Point(5, 3);
            this.rbDaily.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rbDaily.Name = "rbDaily";
            this.rbDaily.Size = new System.Drawing.Size(63, 25);
            this.rbDaily.TabIndex = 17;
            this.rbDaily.TabStop = true;
            this.rbDaily.Text = "Daily";
            this.rbDaily.UseVisualStyleBackColor = true;
            this.rbDaily.CheckedChanged += new System.EventHandler(this.rbDaily_CheckedChanged);
            // 
            // rbWeekly
            // 
            this.rbWeekly.AutoSize = true;
            this.rbWeekly.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbWeekly.Location = new System.Drawing.Point(78, 3);
            this.rbWeekly.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rbWeekly.Name = "rbWeekly";
            this.rbWeekly.Size = new System.Drawing.Size(78, 25);
            this.rbWeekly.TabIndex = 18;
            this.rbWeekly.TabStop = true;
            this.rbWeekly.Text = "Weekly";
            this.rbWeekly.UseVisualStyleBackColor = true;
            this.rbWeekly.CheckedChanged += new System.EventHandler(this.rbWeekly_CheckedChanged);
            // 
            // rbMonthly
            // 
            this.rbMonthly.AutoSize = true;
            this.rbMonthly.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMonthly.Location = new System.Drawing.Point(166, 3);
            this.rbMonthly.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rbMonthly.Name = "rbMonthly";
            this.rbMonthly.Size = new System.Drawing.Size(86, 25);
            this.rbMonthly.TabIndex = 19;
            this.rbMonthly.TabStop = true;
            this.rbMonthly.Text = "Monthly";
            this.rbMonthly.UseVisualStyleBackColor = true;
            this.rbMonthly.CheckedChanged += new System.EventHandler(this.rbMonthly_CheckedChanged);
            // 
            // rbSearchID
            // 
            this.rbSearchID.AutoSize = true;
            this.rbSearchID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSearchID.Location = new System.Drawing.Point(262, 3);
            this.rbSearchID.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rbSearchID.Name = "rbSearchID";
            this.rbSearchID.Size = new System.Drawing.Size(43, 25);
            this.rbSearchID.TabIndex = 20;
            this.rbSearchID.TabStop = true;
            this.rbSearchID.Text = "ID";
            this.rbSearchID.UseVisualStyleBackColor = true;
            this.rbSearchID.CheckedChanged += new System.EventHandler(this.rbSearchID_CheckedChanged);
            // 
            // panelDaily
            // 
            this.panelDaily.Controls.Add(this.dtpTo);
            this.panelDaily.Controls.Add(this.label4);
            this.panelDaily.Controls.Add(this.dtpFrom);
            this.panelDaily.Controls.Add(this.label1);
            this.panelDaily.Location = new System.Drawing.Point(15, 77);
            this.panelDaily.Name = "panelDaily";
            this.panelDaily.Size = new System.Drawing.Size(577, 30);
            this.panelDaily.TabIndex = 46;
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Location = new System.Drawing.Point(339, 3);
            this.dtpTo.MaxDate = new System.DateTime(2030, 6, 1, 0, 0, 0, 0);
            this.dtpTo.MinDate = new System.DateTime(2026, 1, 1, 0, 0, 0, 0);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(230, 25);
            this.dtpTo.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 25);
            this.label4.TabIndex = 17;
            this.label4.Text = "From:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Location = new System.Drawing.Point(63, 3);
            this.dtpFrom.MaxDate = new System.DateTime(2030, 6, 1, 0, 0, 0, 0);
            this.dtpFrom.MinDate = new System.DateTime(2026, 1, 1, 0, 0, 0, 0);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(226, 25);
            this.dtpFrom.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(302, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "To:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.Maroon;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(15, 111);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(189, 37);
            this.btnGenerate.TabIndex = 45;
            this.btnGenerate.Text = "GENERATE";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dgvSalesReport
            // 
            this.dgvSalesReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesReport.Location = new System.Drawing.Point(25, 158);
            this.dgvSalesReport.Margin = new System.Windows.Forms.Padding(25, 3, 25, 3);
            this.dgvSalesReport.Name = "dgvSalesReport";
            this.dgvSalesReport.Size = new System.Drawing.Size(758, 280);
            this.dgvSalesReport.TabIndex = 5;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnSummary);
            this.panelBottom.Controls.Add(this.btnDetailed);
            this.panelBottom.Controls.Add(this.lblTotalSales);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 444);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(808, 72);
            this.panelBottom.TabIndex = 6;
            // 
            // btnSummary
            // 
            this.btnSummary.BackColor = System.Drawing.Color.Maroon;
            this.btnSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSummary.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSummary.ForeColor = System.Drawing.Color.White;
            this.btnSummary.Location = new System.Drawing.Point(531, 20);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(121, 37);
            this.btnSummary.TabIndex = 49;
            this.btnSummary.Text = "SUMMARY";
            this.btnSummary.UseVisualStyleBackColor = false;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnDetailed
            // 
            this.btnDetailed.BackColor = System.Drawing.Color.Maroon;
            this.btnDetailed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetailed.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetailed.ForeColor = System.Drawing.Color.White;
            this.btnDetailed.Location = new System.Drawing.Point(662, 20);
            this.btnDetailed.Name = "btnDetailed";
            this.btnDetailed.Size = new System.Drawing.Size(121, 37);
            this.btnDetailed.TabIndex = 48;
            this.btnDetailed.Text = "DETAILED";
            this.btnDetailed.UseVisualStyleBackColor = false;
            this.btnDetailed.Click += new System.EventHandler(this.btnDetailed_Click);
            // 
            // lblTotalSales
            // 
            this.lblTotalSales.AutoSize = true;
            this.lblTotalSales.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSales.Location = new System.Drawing.Point(11, 27);
            this.lblTotalSales.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.lblTotalSales.Name = "lblTotalSales";
            this.lblTotalSales.Size = new System.Drawing.Size(150, 21);
            this.lblTotalSales.TabIndex = 23;
            this.lblTotalSales.Text = "Total Sales: 0.00 Php";
            // 
            // panelExactTransaction
            // 
            this.panelExactTransaction.Location = new System.Drawing.Point(25, 158);
            this.panelExactTransaction.Name = "panelExactTransaction";
            this.panelExactTransaction.Size = new System.Drawing.Size(758, 280);
            this.panelExactTransaction.TabIndex = 7;
            this.panelExactTransaction.Paint += new System.Windows.Forms.PaintEventHandler(this.panelExactTransaction_Paint);
            // 
            // UserControlSalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.panelExactTransaction);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.dgvSalesReport);
            this.Controls.Add(this.panelHeader);
            this.Name = "UserControlSalesReport";
            this.Size = new System.Drawing.Size(808, 516);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSearchID.ResumeLayout(false);
            this.panelSearchID.PerformLayout();
            this.panelYearlyMonthly.ResumeLayout(false);
            this.panelYearlyMonthly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            this.flowPanelSelection.ResumeLayout(false);
            this.flowPanelSelection.PerformLayout();
            this.panelDaily.ResumeLayout(false);
            this.panelDaily.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowPanelSelection;
        private System.Windows.Forms.RadioButton rbDaily;
        private System.Windows.Forms.RadioButton rbWeekly;
        private System.Windows.Forms.RadioButton rbMonthly;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.NumericUpDown nudYear;
        private System.Windows.Forms.Panel panelDaily;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dgvSalesReport;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelYearlyMonthly;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnDetailed;
        private System.Windows.Forms.Panel panelSearchID;
        private System.Windows.Forms.TextBox txtTransactionID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbSearchID;
        private System.Windows.Forms.Panel panelExactTransaction;
        private System.Windows.Forms.Button btnSearch;
    }
}
