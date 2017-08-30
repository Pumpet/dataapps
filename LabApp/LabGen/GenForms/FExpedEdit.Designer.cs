namespace GenForms
{
  partial class FExpedEdit
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FExpedEdit));
      this.panel = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.tInfo = new System.Windows.Forms.TextBox();
      this.tHead = new System.Windows.Forms.TextBox();
      this.tRegion = new System.Windows.Forms.TextBox();
      this.tName = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.dtDateEnd = new Ctrls.DateTimeBox(this.components);
      this.dtDateStart = new Ctrls.DateTimeBox(this.components);
      this.panel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dtDateEnd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dtDateStart)).BeginInit();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 5;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 262F));
      this.panel.Controls.Add(this.label1, 1, 0);
      this.panel.Controls.Add(this.label2, 1, 2);
      this.panel.Controls.Add(this.label3, 1, 3);
      this.panel.Controls.Add(this.label4, 1, 4);
      this.panel.Controls.Add(this.label5, 1, 5);
      this.panel.Controls.Add(this.tInfo, 2, 5);
      this.panel.Controls.Add(this.tHead, 2, 2);
      this.panel.Controls.Add(this.tRegion, 2, 1);
      this.panel.Controls.Add(this.tName, 2, 0);
      this.panel.Controls.Add(this.label6, 1, 1);
      this.panel.Controls.Add(this.dtDateEnd, 2, 4);
      this.panel.Controls.Add(this.dtDateStart, 2, 3);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 25);
      this.panel.Margin = new System.Windows.Forms.Padding(0);
      this.panel.Name = "panel";
      this.panel.RowCount = 8;
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.Size = new System.Drawing.Size(512, 215);
      this.panel.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(13, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(105, 25);
      this.label1.TabIndex = 0;
      this.label1.Text = "Название";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(13, 50);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(105, 25);
      this.label2.TabIndex = 1;
      this.label2.Text = "Руководитель";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label3.Location = new System.Drawing.Point(13, 75);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(105, 25);
      this.label3.TabIndex = 2;
      this.label3.Text = "Дата начала";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label4.Location = new System.Drawing.Point(13, 100);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(105, 25);
      this.label4.TabIndex = 3;
      this.label4.Text = "Дата окончания";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label5.Location = new System.Drawing.Point(13, 125);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(105, 25);
      this.label5.TabIndex = 4;
      this.label5.Text = "Описание";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // tInfo
      // 
      this.panel.SetColumnSpan(this.tInfo, 2);
      this.tInfo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tInfo.Location = new System.Drawing.Point(124, 128);
      this.tInfo.Multiline = true;
      this.tInfo.Name = "tInfo";
      this.panel.SetRowSpan(this.tInfo, 2);
      this.tInfo.Size = new System.Drawing.Size(243, 44);
      this.tInfo.TabIndex = 5;
      // 
      // tHead
      // 
      this.tHead.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tHead.Location = new System.Drawing.Point(124, 53);
      this.tHead.Name = "tHead";
      this.tHead.Size = new System.Drawing.Size(143, 22);
      this.tHead.TabIndex = 2;
      // 
      // tRegion
      // 
      this.tRegion.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tRegion.Location = new System.Drawing.Point(124, 28);
      this.tRegion.Name = "tRegion";
      this.tRegion.Size = new System.Drawing.Size(143, 22);
      this.tRegion.TabIndex = 1;
      // 
      // tName
      // 
      this.tName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tName.Location = new System.Drawing.Point(124, 3);
      this.tName.Name = "tName";
      this.tName.Size = new System.Drawing.Size(143, 22);
      this.tName.TabIndex = 0;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(13, 25);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(105, 25);
      this.label6.TabIndex = 7;
      this.label6.Text = "Регион";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // dtDateEnd
      // 
      this.dtDateEnd.ForeColor = System.Drawing.SystemColors.WindowText;
      this.dtDateEnd.Location = new System.Drawing.Point(124, 103);
      this.dtDateEnd.Name = "dtDateEnd";
      this.dtDateEnd.Size = new System.Drawing.Size(100, 22);
      this.dtDateEnd.TabIndex = 4;
      this.dtDateEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // dtDateStart
      // 
      this.dtDateStart.ForeColor = System.Drawing.SystemColors.WindowText;
      this.dtDateStart.Location = new System.Drawing.Point(124, 78);
      this.dtDateStart.Name = "dtDateStart";
      this.dtDateStart.Size = new System.Drawing.Size(100, 22);
      this.dtDateStart.TabIndex = 3;
      this.dtDateStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // FExpedEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(512, 262);
      this.Controls.Add(this.panel);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyNames = "ExpedId";
      this.Name = "FExpedEdit";
      this.Text = "Экспедиция";
      this.Controls.SetChildIndex(this.panel, 0);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dtDateEnd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dtDateStart)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox tInfo;
    private System.Windows.Forms.TextBox tHead;
    private System.Windows.Forms.TextBox tRegion;
    private System.Windows.Forms.TextBox tName;
    private System.Windows.Forms.Label label6;
    private Ctrls.DateTimeBox dtDateStart;
    private Ctrls.DateTimeBox dtDateEnd;

  }
}