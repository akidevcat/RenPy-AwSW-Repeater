namespace AwSW_Repeater
{
    partial class AwSWR
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
            this.label1 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.LabelDrag = new System.Windows.Forms.Label();
            this.ClearFilesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(42)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "AwSW-Repeater";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StatusBar_MouseDown);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(42)))));
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(42)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(225, 0);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(25, 25);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // LabelDrag
            // 
            this.LabelDrag.AllowDrop = true;
            this.LabelDrag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelDrag.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDrag.ForeColor = System.Drawing.Color.White;
            this.LabelDrag.Location = new System.Drawing.Point(0, 25);
            this.LabelDrag.Name = "LabelDrag";
            this.LabelDrag.Size = new System.Drawing.Size(250, 225);
            this.LabelDrag.TabIndex = 2;
            this.LabelDrag.Text = "Drag\'n\'drop files or\r\nfolder here";
            this.LabelDrag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelDrag.Click += new System.EventHandler(this.LabelDrag_Click);
            this.LabelDrag.DragDrop += new System.Windows.Forms.DragEventHandler(this.LabelDrag_DragDrop);
            this.LabelDrag.DragEnter += new System.Windows.Forms.DragEventHandler(this.LabelDrag_DragEnter);
            this.LabelDrag.DragLeave += new System.EventHandler(this.LabelDrag_DragLeave);
            // 
            // ClearFilesButton
            // 
            this.ClearFilesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(42)))));
            this.ClearFilesButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(42)))));
            this.ClearFilesButton.FlatAppearance.BorderSize = 0;
            this.ClearFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearFilesButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearFilesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.ClearFilesButton.Location = new System.Drawing.Point(200, 0);
            this.ClearFilesButton.Margin = new System.Windows.Forms.Padding(0);
            this.ClearFilesButton.Name = "ClearFilesButton";
            this.ClearFilesButton.Size = new System.Drawing.Size(25, 25);
            this.ClearFilesButton.TabIndex = 3;
            this.ClearFilesButton.Text = "C";
            this.ClearFilesButton.UseVisualStyleBackColor = false;
            this.ClearFilesButton.Click += new System.EventHandler(this.ClearFilesButton_Click);
            // 
            // AwSWR
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.ClientSize = new System.Drawing.Size(250, 250);
            this.Controls.Add(this.ClearFilesButton);
            this.Controls.Add(this.LabelDrag);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AwSWR";
            this.Text = "AwSWR";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label LabelDrag;
        private System.Windows.Forms.Button ClearFilesButton;
    }
}

