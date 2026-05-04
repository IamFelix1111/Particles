namespace Particles
{
    partial class PropertyForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.devTabPage = new System.Windows.Forms.TabPage();
            this.openDevButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.devTabPage.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.devTabPage);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(9, 9);
            this.tabControl.Margin = new System.Windows.Forms.Padding(9, 9, 9, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(560, 664);
            this.tabControl.TabIndex = 0;
            // 
            // devTabPage
            // 
            this.devTabPage.Controls.Add(this.openDevButton);
            this.devTabPage.Location = new System.Drawing.Point(4, 25);
            this.devTabPage.Name = "devTabPage";
            this.devTabPage.Padding = new System.Windows.Forms.Padding(12);
            this.devTabPage.Size = new System.Drawing.Size(552, 635);
            this.devTabPage.TabIndex = 0;
            this.devTabPage.Text = global::Particles.Properties.Resources.Dev;
            this.devTabPage.UseVisualStyleBackColor = true;
            // 
            // openDevButton
            // 
            this.openDevButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.openDevButton.Location = new System.Drawing.Point(12, 12);
            this.openDevButton.Margin = new System.Windows.Forms.Padding(20);
            this.openDevButton.Name = "openDevButton";
            this.openDevButton.Size = new System.Drawing.Size(528, 192);
            this.openDevButton.TabIndex = 0;
            this.openDevButton.Text = global::Particles.Properties.Resources.Open_Dev;
            this.openDevButton.UseVisualStyleBackColor = true;
            this.openDevButton.Click += new System.EventHandler(this.openDevButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(552, 635);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel.Controls.Add(this.tabControl);
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(578, 730);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.applyButton);
            this.flowLayoutPanel1.Controls.Add(this.cancelButton);
            this.flowLayoutPanel1.Controls.Add(this.acceptButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 676);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(572, 51);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // applyButton
            // 
            this.applyButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.applyButton.Font = new System.Drawing.Font("宋体", 9F);
            this.applyButton.Location = new System.Drawing.Point(444, 8);
            this.applyButton.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(120, 35);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = global::Particles.Properties.Resources.Apply;
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("宋体", 9F);
            this.cancelButton.Location = new System.Drawing.Point(316, 8);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(120, 35);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = global::Particles.Properties.Resources.Cancel;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Font = new System.Drawing.Font("宋体", 9F);
            this.acceptButton.Location = new System.Drawing.Point(188, 8);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(120, 35);
            this.acceptButton.TabIndex = 3;
            this.acceptButton.Text = global::Particles.Properties.Resources.Accept;
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // PropertyForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(578, 731);
            this.Controls.Add(this.flowLayoutPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertyForm";
            this.ShowInTaskbar = false;
            this.Text = "属性";
            this.Activated += new System.EventHandler(this.PropertyForm_Activated);
            this.Load += new System.EventHandler(this.PropertyForm_Load);
            this.tabControl.ResumeLayout(false);
            this.devTabPage.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TabControl tabControl;
        internal System.Windows.Forms.TabPage devTabPage;
        internal System.Windows.Forms.Button openDevButton;
        internal System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        internal System.Windows.Forms.Button applyButton;
        internal System.Windows.Forms.Button cancelButton;
        internal System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
