
namespace MrLabandero
{
    partial class UserControlReceipt
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
            this.panelReceiptContent = new System.Windows.Forms.Panel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.panelServiceButtons = new System.Windows.Forms.Panel();
            this.panelServiceButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelReceiptContent
            // 
            this.panelReceiptContent.AutoScroll = true;
            this.panelReceiptContent.AutoSize = true;
            this.panelReceiptContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReceiptContent.Location = new System.Drawing.Point(0, 0);
            this.panelReceiptContent.Name = "panelReceiptContent";
            this.panelReceiptContent.Size = new System.Drawing.Size(808, 456);
            this.panelReceiptContent.TabIndex = 0;
            this.panelReceiptContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelReceiptContent_Paint);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Maroon;
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(254, 7);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(120, 34);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "RETURN";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click_1);
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.BackColor = System.Drawing.Color.Maroon;
            this.btnNewOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewOrder.ForeColor = System.Drawing.Color.White;
            this.btnNewOrder.Location = new System.Drawing.Point(400, 7);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(120, 34);
            this.btnNewOrder.TabIndex = 2;
            this.btnNewOrder.Text = "NEW ORDER";
            this.btnNewOrder.UseVisualStyleBackColor = false;
            this.btnNewOrder.Click += new System.EventHandler(this.btnNewOrder_Click_1);
            // 
            // panelServiceButtons
            // 
            this.panelServiceButtons.Controls.Add(this.btnReturn);
            this.panelServiceButtons.Controls.Add(this.btnNewOrder);
            this.panelServiceButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelServiceButtons.Location = new System.Drawing.Point(0, 456);
            this.panelServiceButtons.Name = "panelServiceButtons";
            this.panelServiceButtons.Size = new System.Drawing.Size(808, 60);
            this.panelServiceButtons.TabIndex = 1;
            // 
            // UserControlReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelReceiptContent);
            this.Controls.Add(this.panelServiceButtons);
            this.Name = "UserControlReceipt";
            this.Size = new System.Drawing.Size(808, 516);
            this.panelServiceButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelReceiptContent;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Panel panelServiceButtons;
    }
}
