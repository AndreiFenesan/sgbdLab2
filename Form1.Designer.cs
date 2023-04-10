using System.Configuration;
using System.Windows.Forms;

namespace laborator1
{
    partial class Form1
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
            this.gridAddress = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.clientGridView = new System.Windows.Forms.DataGridView();
            this.addBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.modifyBtn = new System.Windows.Forms.Button();
            this.inputPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridAddress
            // 
            this.gridAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAddress.Location = new System.Drawing.Point(12, 29);
            this.gridAddress.MultiSelect = false;
            this.gridAddress.Name = "gridAddress";
            this.gridAddress.ReadOnly = true;
            this.gridAddress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAddress.Size = new System.Drawing.Size(348, 150);
            this.gridAddress.TabIndex = 0;
            this.gridAddress.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAddress_CellClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "afisare";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // clientGridView
            // 
            this.clientGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientGridView.Location = new System.Drawing.Point(452, 29);
            this.clientGridView.MultiSelect = false;
            this.clientGridView.Name = "clientGridView";
            this.clientGridView.ReadOnly = true;
            this.clientGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientGridView.Size = new System.Drawing.Size(402, 124);
            this.clientGridView.TabIndex = 2;
            this.clientGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clientGridView_CellClick);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(918, 197);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 13;
            this.addBtn.Text = "add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(918, 247);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 14;
            this.deleteBtn.Text = "delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // modifyBtn
            // 
            this.modifyBtn.Location = new System.Drawing.Point(918, 300);
            this.modifyBtn.Name = "modifyBtn";
            this.modifyBtn.Size = new System.Drawing.Size(75, 23);
            this.modifyBtn.TabIndex = 15;
            this.modifyBtn.Text = "modify";
            this.modifyBtn.UseVisualStyleBackColor = true;
            this.modifyBtn.Click += new System.EventHandler(this.modifyBtn_Click);
            // 
            // inputPanel
            // 
            this.inputPanel.Location = new System.Drawing.Point(508, 159);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(346, 582);
            this.inputPanel.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 753);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.modifyBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.clientGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridAddress);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientGridView)).EndInit();
            initialiseTextBoxes();
            this.ResumeLayout(false);
        }

        private void initialiseTextBoxes()
        {
            string[] textBoxesNames = ConfigurationManager.AppSettings["childColumns"].Split(',');
            TableLayoutPanel verticalLayout = new TableLayoutPanel();
            verticalLayout.Size = this.inputPanel.Size;
            for (int i = 0; i < textBoxesNames.Length; i++)
            {
                Label label = new Label();
                label.Text = textBoxesNames[i];
                TextBox textBox = new TextBox();
                textBox.Name = textBoxesNames[i];
                if (i == 0 || i == textBoxesNames.Length - 1)
                {
                    textBox.ReadOnly = true;
                }
                verticalLayout.Controls.Add(label);
                verticalLayout.Controls.Add(textBox);
            }
            
            this.inputPanel.Controls.Add(verticalLayout);
        }

        private System.Windows.Forms.Panel inputPanel;

        private System.Windows.Forms.Button modifyBtn;

        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button deleteBtn;

        private System.Windows.Forms.DataGridView clientGridView;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.DataGridView gridAddress;

        #endregion
    }
}