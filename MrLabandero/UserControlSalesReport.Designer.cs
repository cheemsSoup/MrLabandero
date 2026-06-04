
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
            this.label6 = new System.Windows.Forms.Label();
            this.flowPanelSelection = new System.Windows.Forms.FlowLayoutPanel();
            this.rbDaily = new System.Windows.Forms.RadioButton();
            this.rbWeekly = new System.Windows.Forms.RadioButton();
            this.rbMonthly = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panelDaily = new System.Windows.Forms.Panel();
            this.dgvSalesReport = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelYearlyMonthly = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.flowPanelSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            this.panelDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).BeginInit();
            this.panelYearlyMonthly.SuspendLayout();
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
            this.panelHeader.Controls.Add(this.label6);
            this.panelHeader.Controls.Add(this.label2);
            this.panelHeader.Controls.Add(this.btnGenerate);
            this.panelHeader.Controls.Add(this.panelYearlyMonthly);
            this.panelHeader.Controls.Add(this.flowPanelSelection);
            this.panelHeader.Controls.Add(this.panelDaily);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(808, 170);
            this.panelHeader.TabIndex = 4;
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
            // flowPanelSelection
            // 
            this.flowPanelSelection.Controls.Add(this.rbDaily);
            this.flowPanelSelection.Controls.Add(this.rbWeekly);
            this.flowPanelSelection.Controls.Add(this.rbMonthly);
            this.flowPanelSelection.Location = new System.Drawing.Point(92, 42);
            this.flowPanelSelection.Name = "flowPanelSelection";
            this.flowPanelSelection.Size = new System.Drawing.Size(262, 29);
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
            this.dtpFrom.MaxDate = new System.DateTime(2028, 6, 1, 0, 0, 0, 0);
            this.dtpFrom.MinDate = new System.DateTime(2026, 6, 1, 0, 0, 0, 0);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(226, 25);
            this.dtpFrom.TabIndex = 18;
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Location = new System.Drawing.Point(339, 3);
            this.dtpTo.MaxDate = new System.DateTime(2028, 6, 1, 0, 0, 0, 0);
            this.dtpTo.MinDate = new System.DateTime(2026, 6, 1, 0, 0, 0, 0);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(230, 25);
            this.dtpTo.TabIndex = 19;
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
            // nudYear
            // 
            this.nudYear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudYear.Location = new System.Drawing.Point(63, 3);
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(120, 29);
            this.nudYear.TabIndex = 22;
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
            // dgvSalesReport
            // 
            this.dgvSalesReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesReport.Location = new System.Drawing.Point(25, 176);
            this.dgvSalesReport.Margin = new System.Windows.Forms.Padding(25, 3, 25, 3);
            this.dgvSalesReport.Name = "dgvSalesReport";
            this.dgvSalesReport.Size = new System.Drawing.Size(758, 262);
            this.dgvSalesReport.TabIndex = 5;
            // 
            // panelBottom
            // 
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 444);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(808, 72);
            this.panelBottom.TabIndex = 6;
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
            // UserControlSalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.dgvSalesReport);
            this.Controls.Add(this.panelHeader);
            this.Name = "UserControlSalesReport";
            this.Size = new System.Drawing.Size(808, 516);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.flowPanelSelection.ResumeLayout(false);
            this.flowPanelSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            this.panelDaily.ResumeLayout(false);
            this.panelDaily.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).EndInit();
            this.panelYearlyMonthly.ResumeLayout(false);
            this.panelYearlyMonthly.PerformLayout();
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
    }
}
