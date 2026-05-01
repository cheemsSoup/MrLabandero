
namespace MrLabandero
{
    partial class frmMrLabandero
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMrLabandero));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.panelBlueLeft = new System.Windows.Forms.Panel();
            this.panelWhiteTop2 = new System.Windows.Forms.Panel();
            this.panelWhiteTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPOS = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnSalesReport = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.panelBlueLeft.SuspendLayout();
            this.panelWhiteTop2.SuspendLayout();
            this.panelWhiteTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(200, 45);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(604, 416);
            this.mainPanel.TabIndex = 1;
            // 
            // panelBlueLeft
            // 
            this.panelBlueLeft.BackColor = System.Drawing.Color.Navy;
            this.panelBlueLeft.Controls.Add(this.btnSettings);
            this.panelBlueLeft.Controls.Add(this.btnSalesReport);
            this.panelBlueLeft.Controls.Add(this.btnInventory);
            this.panelBlueLeft.Controls.Add(this.btnPOS);
            this.panelBlueLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBlueLeft.Location = new System.Drawing.Point(0, 0);
            this.panelBlueLeft.Name = "panelBlueLeft";
            this.panelBlueLeft.Size = new System.Drawing.Size(200, 461);
            this.panelBlueLeft.TabIndex = 0;
            // 
            // panelWhiteTop2
            // 
            this.panelWhiteTop2.BackColor = System.Drawing.Color.Black;
            this.panelWhiteTop2.Controls.Add(this.panelWhiteTop);
            this.panelWhiteTop2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWhiteTop2.Location = new System.Drawing.Point(200, 0);
            this.panelWhiteTop2.Name = "panelWhiteTop2";
            this.panelWhiteTop2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.panelWhiteTop2.Size = new System.Drawing.Size(604, 45);
            this.panelWhiteTop2.TabIndex = 1;
            // 
            // panelWhiteTop
            // 
            this.panelWhiteTop.BackColor = System.Drawing.Color.White;
            this.panelWhiteTop.Controls.Add(this.label1);
            this.panelWhiteTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWhiteTop.Location = new System.Drawing.Point(0, 0);
            this.panelWhiteTop.Name = "panelWhiteTop";
            this.panelWhiteTop.Size = new System.Drawing.Size(604, 44);
            this.panelWhiteTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mr. Labandero POS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPOS
            // 
            this.btnPOS.BackColor = System.Drawing.Color.Transparent;
            this.btnPOS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPOS.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPOS.FlatAppearance.BorderSize = 0;
            this.btnPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPOS.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPOS.ForeColor = System.Drawing.Color.White;
            this.btnPOS.Location = new System.Drawing.Point(0, 0);
            this.btnPOS.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.btnPOS.Name = "btnPOS";
            this.btnPOS.Padding = new System.Windows.Forms.Padding(2);
            this.btnPOS.Size = new System.Drawing.Size(200, 44);
            this.btnPOS.TabIndex = 1;
            this.btnPOS.Text = "POS";
            this.btnPOS.UseVisualStyleBackColor = false;
            this.btnPOS.Click += new System.EventHandler(this.btnPOS_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.Transparent;
            this.btnInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Location = new System.Drawing.Point(0, 44);
            this.btnInventory.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Padding = new System.Windows.Forms.Padding(2);
            this.btnInventory.Size = new System.Drawing.Size(200, 45);
            this.btnInventory.TabIndex = 2;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.UseVisualStyleBackColor = false;
            // 
            // btnSalesReport
            // 
            this.btnSalesReport.BackColor = System.Drawing.Color.Transparent;
            this.btnSalesReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalesReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSalesReport.FlatAppearance.BorderSize = 0;
            this.btnSalesReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalesReport.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalesReport.ForeColor = System.Drawing.Color.White;
            this.btnSalesReport.Location = new System.Drawing.Point(0, 89);
            this.btnSalesReport.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.btnSalesReport.Name = "btnSalesReport";
            this.btnSalesReport.Padding = new System.Windows.Forms.Padding(2);
            this.btnSalesReport.Size = new System.Drawing.Size(200, 45);
            this.btnSalesReport.TabIndex = 3;
            this.btnSalesReport.Text = "Sales Report";
            this.btnSalesReport.UseVisualStyleBackColor = false;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(0, 134);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(2);
            this.btnSettings.Size = new System.Drawing.Size(200, 45);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            // 
            // frmMrLabandero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(804, 461);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panelWhiteTop2);
            this.Controls.Add(this.panelBlueLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMrLabandero";
            this.Text = "Mr. Labandero POS & Inventory System";
            this.panelBlueLeft.ResumeLayout(false);
            this.panelWhiteTop2.ResumeLayout(false);
            this.panelWhiteTop.ResumeLayout(false);
            this.panelWhiteTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panelWhiteTop2;
        private System.Windows.Forms.Panel panelBlueLeft;
        private System.Windows.Forms.Panel panelWhiteTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnSalesReport;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnPOS;
    }
}

