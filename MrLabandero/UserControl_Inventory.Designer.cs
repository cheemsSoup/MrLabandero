
namespace MrLabandero
{
    partial class UserControl_Inventory
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panelInventoryItems = new System.Windows.Forms.Panel();
            this.lblDetergentServings = new System.Windows.Forms.Label();
            this.lblFabconServings = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnAddDetergent = new System.Windows.Forms.Button();
            this.btnAddFabcon = new System.Windows.Forms.Button();
            this.nudDetergentAdd = new System.Windows.Forms.NumericUpDown();
            this.nudFabconAdd = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDetergentDefault = new System.Windows.Forms.Label();
            this.lblFabconDefault = new System.Windows.Forms.Label();
            this.lblDetergentStock = new System.Windows.Forms.Label();
            this.lblFabconStock = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.panelInventoryItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDetergentAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFabconAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.dgvInventory);
            this.panelHeader.Controls.Add(this.label2);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(808, 232);
            this.panelHeader.TabIndex = 0;
            // 
            // dgvInventory
            // 
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Location = new System.Drawing.Point(15, 40);
            this.dgvInventory.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.Size = new System.Drawing.Size(776, 183);
            this.dgvInventory.TabIndex = 4;
            this.dgvInventory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventory_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "INVENTORY";
            // 
            // panelInventoryItems
            // 
            this.panelInventoryItems.Controls.Add(this.lblDetergentServings);
            this.panelInventoryItems.Controls.Add(this.lblFabconServings);
            this.panelInventoryItems.Controls.Add(this.txtRemarks);
            this.panelInventoryItems.Controls.Add(this.label8);
            this.panelInventoryItems.Controls.Add(this.panel10);
            this.panelInventoryItems.Controls.Add(this.panel11);
            this.panelInventoryItems.Controls.Add(this.btnAddDetergent);
            this.panelInventoryItems.Controls.Add(this.btnAddFabcon);
            this.panelInventoryItems.Controls.Add(this.nudDetergentAdd);
            this.panelInventoryItems.Controls.Add(this.nudFabconAdd);
            this.panelInventoryItems.Controls.Add(this.label7);
            this.panelInventoryItems.Controls.Add(this.panel8);
            this.panelInventoryItems.Controls.Add(this.panel9);
            this.panelInventoryItems.Controls.Add(this.panel6);
            this.panelInventoryItems.Controls.Add(this.panel7);
            this.panelInventoryItems.Controls.Add(this.panel5);
            this.panelInventoryItems.Controls.Add(this.panel2);
            this.panelInventoryItems.Controls.Add(this.label6);
            this.panelInventoryItems.Controls.Add(this.lblDetergentDefault);
            this.panelInventoryItems.Controls.Add(this.lblFabconDefault);
            this.panelInventoryItems.Controls.Add(this.lblDetergentStock);
            this.panelInventoryItems.Controls.Add(this.lblFabconStock);
            this.panelInventoryItems.Controls.Add(this.panel3);
            this.panelInventoryItems.Controls.Add(this.panel4);
            this.panelInventoryItems.Controls.Add(this.label3);
            this.panelInventoryItems.Controls.Add(this.label1);
            this.panelInventoryItems.Controls.Add(this.panel1);
            this.panelInventoryItems.Location = new System.Drawing.Point(0, 225);
            this.panelInventoryItems.Name = "panelInventoryItems";
            this.panelInventoryItems.Size = new System.Drawing.Size(805, 291);
            this.panelInventoryItems.TabIndex = 1;
            // 
            // lblDetergentServings
            // 
            this.lblDetergentServings.AutoSize = true;
            this.lblDetergentServings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetergentServings.Location = new System.Drawing.Point(253, 113);
            this.lblDetergentServings.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblDetergentServings.Name = "lblDetergentServings";
            this.lblDetergentServings.Size = new System.Drawing.Size(121, 21);
            this.lblDetergentServings.TabIndex = 77;
            this.lblDetergentServings.Text = "Servings left: 60";
            // 
            // lblFabconServings
            // 
            this.lblFabconServings.AutoSize = true;
            this.lblFabconServings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFabconServings.Location = new System.Drawing.Point(11, 113);
            this.lblFabconServings.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblFabconServings.Name = "lblFabconServings";
            this.lblFabconServings.Size = new System.Drawing.Size(121, 21);
            this.lblFabconServings.TabIndex = 76;
            this.lblFabconServings.Text = "Servings left: 60";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(89, 255);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(279, 25);
            this.txtRemarks.TabIndex = 74;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 73;
            this.label8.Text = "Remarks:";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.Location = new System.Drawing.Point(15, 231);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel10.Size = new System.Drawing.Size(462, 11);
            this.panel10.TabIndex = 66;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Black;
            this.panel11.Location = new System.Drawing.Point(16, 231);
            this.panel11.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(461, 14);
            this.panel11.TabIndex = 67;
            // 
            // btnAddDetergent
            // 
            this.btnAddDetergent.BackColor = System.Drawing.Color.Maroon;
            this.btnAddDetergent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDetergent.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDetergent.ForeColor = System.Drawing.Color.White;
            this.btnAddDetergent.Location = new System.Drawing.Point(256, 193);
            this.btnAddDetergent.Name = "btnAddDetergent";
            this.btnAddDetergent.Size = new System.Drawing.Size(135, 37);
            this.btnAddDetergent.TabIndex = 72;
            this.btnAddDetergent.Text = "ADD DETERGENT";
            this.btnAddDetergent.UseVisualStyleBackColor = false;
            this.btnAddDetergent.Click += new System.EventHandler(this.btnAddDetergent_Click);
            // 
            // btnAddFabcon
            // 
            this.btnAddFabcon.BackColor = System.Drawing.Color.Maroon;
            this.btnAddFabcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFabcon.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFabcon.ForeColor = System.Drawing.Color.White;
            this.btnAddFabcon.Location = new System.Drawing.Point(15, 193);
            this.btnAddFabcon.Name = "btnAddFabcon";
            this.btnAddFabcon.Size = new System.Drawing.Size(135, 37);
            this.btnAddFabcon.TabIndex = 71;
            this.btnAddFabcon.Text = "ADD FABCON";
            this.btnAddFabcon.UseVisualStyleBackColor = false;
            this.btnAddFabcon.Click += new System.EventHandler(this.btnAddFabcon_Click);
            // 
            // nudDetergentAdd
            // 
            this.nudDetergentAdd.Enabled = false;
            this.nudDetergentAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDetergentAdd.Location = new System.Drawing.Point(257, 162);
            this.nudDetergentAdd.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudDetergentAdd.Name = "nudDetergentAdd";
            this.nudDetergentAdd.Size = new System.Drawing.Size(134, 25);
            this.nudDetergentAdd.TabIndex = 70;
            this.nudDetergentAdd.ValueChanged += new System.EventHandler(this.nudDetergentAdd_ValueChanged);
            // 
            // nudFabconAdd
            // 
            this.nudFabconAdd.Enabled = false;
            this.nudFabconAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudFabconAdd.Location = new System.Drawing.Point(15, 162);
            this.nudFabconAdd.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudFabconAdd.Name = "nudFabconAdd";
            this.nudFabconAdd.Size = new System.Drawing.Size(136, 25);
            this.nudFabconAdd.TabIndex = 69;
            this.nudFabconAdd.ValueChanged += new System.EventHandler(this.nudFabconAdd_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(253, 138);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 21);
            this.label7.TabIndex = 66;
            this.label7.Text = "Add Stock (ml):";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.Location = new System.Drawing.Point(257, 91);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel8.Size = new System.Drawing.Size(220, 11);
            this.panel8.TabIndex = 64;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Black;
            this.panel9.Location = new System.Drawing.Point(258, 91);
            this.panel9.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(219, 14);
            this.panel9.TabIndex = 65;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Location = new System.Drawing.Point(15, 91);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel6.Size = new System.Drawing.Size(220, 11);
            this.panel6.TabIndex = 61;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Black;
            this.panel7.Location = new System.Drawing.Point(16, 91);
            this.panel7.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(219, 14);
            this.panel7.TabIndex = 62;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel5.Size = new System.Drawing.Size(776, 11);
            this.panel5.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(15, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel2.Size = new System.Drawing.Size(776, 11);
            this.panel2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 138);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 21);
            this.label6.TabIndex = 60;
            this.label6.Text = "Add Stock (ml):";
            // 
            // lblDetergentDefault
            // 
            this.lblDetergentDefault.AutoSize = true;
            this.lblDetergentDefault.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetergentDefault.Location = new System.Drawing.Point(253, 71);
            this.lblDetergentDefault.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblDetergentDefault.Name = "lblDetergentDefault";
            this.lblDetergentDefault.Size = new System.Drawing.Size(182, 21);
            this.lblDetergentDefault.TabIndex = 59;
            this.lblDetergentDefault.Text = "Default: 60ml / per order";
            // 
            // lblFabconDefault
            // 
            this.lblFabconDefault.AutoSize = true;
            this.lblFabconDefault.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFabconDefault.Location = new System.Drawing.Point(11, 71);
            this.lblFabconDefault.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblFabconDefault.Name = "lblFabconDefault";
            this.lblFabconDefault.Size = new System.Drawing.Size(182, 21);
            this.lblFabconDefault.TabIndex = 58;
            this.lblFabconDefault.Text = "Default: 60ml / per order";
            // 
            // lblDetergentStock
            // 
            this.lblDetergentStock.AutoSize = true;
            this.lblDetergentStock.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetergentStock.Location = new System.Drawing.Point(253, 45);
            this.lblDetergentStock.Name = "lblDetergentStock";
            this.lblDetergentStock.Size = new System.Drawing.Size(115, 21);
            this.lblDetergentStock.TabIndex = 57;
            this.lblDetergentStock.Text = "Stock: 2,400 ml";
            // 
            // lblFabconStock
            // 
            this.lblFabconStock.AutoSize = true;
            this.lblFabconStock.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFabconStock.Location = new System.Drawing.Point(11, 45);
            this.lblFabconStock.Name = "lblFabconStock";
            this.lblFabconStock.Size = new System.Drawing.Size(115, 21);
            this.lblFabconStock.TabIndex = 56;
            this.lblFabconStock.Text = "Stock: 3,600 ml";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Location = new System.Drawing.Point(233, 20);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel3.Size = new System.Drawing.Size(10, 205);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(236, 20);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel4.Size = new System.Drawing.Size(10, 205);
            this.panel4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(252, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 25);
            this.label3.TabIndex = 55;
            this.label3.Text = "LIQUID DETERGENT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 25);
            this.label1.TabIndex = 54;
            this.label1.Text = "FABCON";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(15, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 14);
            this.panel1.TabIndex = 53;
            // 
            // UserControl_Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelInventoryItems);
            this.Controls.Add(this.panelHeader);
            this.Name = "UserControl_Inventory";
            this.Size = new System.Drawing.Size(808, 516);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.panelInventoryItems.ResumeLayout(false);
            this.panelInventoryItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDetergentAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFabconAdd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelInventoryItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFabconStock;
        private System.Windows.Forms.Label lblDetergentStock;
        private System.Windows.Forms.Label lblDetergentDefault;
        private System.Windows.Forms.Label lblFabconDefault;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudDetergentAdd;
        private System.Windows.Forms.NumericUpDown nudFabconAdd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnAddDetergent;
        private System.Windows.Forms.Button btnAddFabcon;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblFabconServings;
        private System.Windows.Forms.Label lblDetergentServings;
    }
}
