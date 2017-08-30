namespace Common
{
  partial class FormErrMess
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
      this.panel = new System.Windows.Forms.TableLayoutPanel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.bExec = new System.Windows.Forms.Button();
      this.bOK = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.tbErr = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.tbErrFull = new System.Windows.Forms.TextBox();
      this.panel.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 2;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.panel.Controls.Add(this.panel2, 0, 1);
      this.panel.Controls.Add(this.panel1, 0, 0);
      this.panel.Controls.Add(this.tbErr, 1, 0);
      this.panel.Controls.Add(this.panel3, 0, 2);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 0);
      this.panel.Margin = new System.Windows.Forms.Padding(0);
      this.panel.Name = "panel";
      this.panel.RowCount = 3;
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
      this.panel.Size = new System.Drawing.Size(404, 391);
      this.panel.TabIndex = 0;
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.SystemColors.Control;
      this.panel.SetColumnSpan(this.panel2, 2);
      this.panel2.Controls.Add(this.bExec);
      this.panel2.Controls.Add(this.bOK);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 100);
      this.panel2.Margin = new System.Windows.Forms.Padding(0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(404, 38);
      this.panel2.TabIndex = 3;
      // 
      // bExec
      // 
      this.bExec.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bExec.FlatAppearance.BorderSize = 0;
      this.bExec.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
      this.bExec.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
      this.bExec.Image = global::Common.Properties.Resources.up;
      this.bExec.Location = new System.Drawing.Point(7, 6);
      this.bExec.Name = "bExec";
      this.bExec.Size = new System.Drawing.Size(24, 27);
      this.bExec.TabIndex = 5;
      this.bExec.UseVisualStyleBackColor = true;
      this.bExec.Click += new System.EventHandler(this.bExec_Click);
      // 
      // bOK
      // 
      this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bOK.Location = new System.Drawing.Point(310, 6);
      this.bOK.Name = "bOK";
      this.bOK.Size = new System.Drawing.Size(87, 27);
      this.bOK.TabIndex = 0;
      this.bOK.Text = "OK";
      this.bOK.Click += new System.EventHandler(this.bOK_Click);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Window;
      this.panel1.Controls.Add(this.pictureBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Margin = new System.Windows.Forms.Padding(0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(55, 100);
      this.panel1.TabIndex = 0;
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Image = global::Common.Properties.Resources.err;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(55, 100);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      // 
      // tbErr
      // 
      this.tbErr.AutoSize = true;
      this.tbErr.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbErr.Location = new System.Drawing.Point(62, 7);
      this.tbErr.Margin = new System.Windows.Forms.Padding(7);
      this.tbErr.Name = "tbErr";
      this.tbErr.Size = new System.Drawing.Size(335, 86);
      this.tbErr.TabIndex = 4;
      this.tbErr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.SystemColors.Control;
      this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel.SetColumnSpan(this.panel3, 2);
      this.panel3.Controls.Add(this.tbErrFull);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 138);
      this.panel3.Margin = new System.Windows.Forms.Padding(0);
      this.panel3.Name = "panel3";
      this.panel3.Padding = new System.Windows.Forms.Padding(7);
      this.panel3.Size = new System.Drawing.Size(404, 253);
      this.panel3.TabIndex = 5;
      // 
      // tbErrFull
      // 
      this.tbErrFull.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tbErrFull.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbErrFull.Location = new System.Drawing.Point(7, 7);
      this.tbErrFull.Margin = new System.Windows.Forms.Padding(0);
      this.tbErrFull.Multiline = true;
      this.tbErrFull.Name = "tbErrFull";
      this.tbErrFull.ReadOnly = true;
      this.tbErrFull.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.tbErrFull.Size = new System.Drawing.Size(386, 235);
      this.tbErrFull.TabIndex = 0;
      this.tbErrFull.WordWrap = false;
      // 
      // FormErrMess
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.ClientSize = new System.Drawing.Size(404, 391);
      this.Controls.Add(this.panel);
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.KeyPreview = true;
      this.Name = "FormErrMess";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ошибка";
      this.Load += new System.EventHandler(this.FormErrMess_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormErrMess_KeyDown);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button bOK;
    private System.Windows.Forms.Label tbErr;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox tbErrFull;
    private System.Windows.Forms.Button bExec;
  }
}