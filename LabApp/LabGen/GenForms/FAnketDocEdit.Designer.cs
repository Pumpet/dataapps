namespace GenForms
{
  partial class FAnketDocEdit
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
      this.panel = new System.Windows.Forms.TableLayoutPanel();
      this.tInfo = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.sbDocType = new Ctrls.SelectBox(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this.tLink = new System.Windows.Forms.TextBox();
      this.bLink = new System.Windows.Forms.Button();
      this.fileDialog = new System.Windows.Forms.OpenFileDialog();
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 6;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 227F));
      this.panel.Controls.Add(this.tInfo, 2, 1);
      this.panel.Controls.Add(this.label6, 1, 1);
      this.panel.Controls.Add(this.label9, 1, 0);
      this.panel.Controls.Add(this.sbDocType, 2, 0);
      this.panel.Controls.Add(this.label1, 1, 2);
      this.panel.Controls.Add(this.tLink, 2, 2);
      this.panel.Controls.Add(this.bLink, 4, 2);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 25);
      this.panel.Margin = new System.Windows.Forms.Padding(0);
      this.panel.Name = "panel";
      this.panel.RowCount = 4;
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.Size = new System.Drawing.Size(418, 137);
      this.panel.TabIndex = 8;
      // 
      // tInfo
      // 
      this.panel.SetColumnSpan(this.tInfo, 3);
      this.tInfo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tInfo.Location = new System.Drawing.Point(124, 28);
      this.tInfo.Name = "tInfo";
      this.tInfo.Size = new System.Drawing.Size(259, 22);
      this.tInfo.TabIndex = 1;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(13, 25);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(105, 25);
      this.label6.TabIndex = 7;
      this.label6.Text = "Информация";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label9.Location = new System.Drawing.Point(13, 0);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(105, 25);
      this.label9.TabIndex = 14;
      this.label9.Text = "Тип документа";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // sbDocType
      // 
      this.sbDocType.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbDocType.DoExecSelect = null;
      this.sbDocType.DoGetParent = null;
      this.sbDocType.ExtSource = null;
      this.sbDocType.ExtSourceFields = null;
      this.sbDocType.ExtSourceParent = null;
      this.sbDocType.FormattingEnabled = true;
      this.sbDocType.IntegralHeight = false;
      this.sbDocType.KeyNames = null;
      this.sbDocType.Location = new System.Drawing.Point(124, 3);
      this.sbDocType.Name = "sbDocType";
      this.sbDocType.SelectFormName = "FPopuls";
      this.sbDocType.Size = new System.Drawing.Size(114, 21);
      this.sbDocType.TabIndex = 0;
      this.sbDocType.ThisSource = null;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(13, 50);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(105, 25);
      this.label1.TabIndex = 15;
      this.label1.Text = "Ссылка на файл";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // tLink
      // 
      this.panel.SetColumnSpan(this.tLink, 2);
      this.tLink.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tLink.Location = new System.Drawing.Point(124, 53);
      this.tLink.Name = "tLink";
      this.tLink.Size = new System.Drawing.Size(236, 22);
      this.tLink.TabIndex = 2;
      // 
      // bLink
      // 
      this.bLink.FlatAppearance.BorderSize = 0;
      this.bLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bLink.Image = global::GenForms.Properties.Resources.fileopen;
      this.bLink.Location = new System.Drawing.Point(366, 53);
      this.bLink.Name = "bLink";
      this.bLink.Size = new System.Drawing.Size(17, 19);
      this.bLink.TabIndex = 17;
      this.bLink.UseVisualStyleBackColor = true;
      this.bLink.Click += new System.EventHandler(this.bLink_Click);
      // 
      // FAnketDocEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(418, 184);
      this.Controls.Add(this.panel);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.KeyNames = "AnketDocId";
      this.Name = "FAnketDocEdit";
      this.ShowIcon = false;
      this.Text = "Документ";
      this.Controls.SetChildIndex(this.panel, 0);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.TextBox tInfo;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label9;
    private Ctrls.SelectBox sbDocType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tLink;
    private System.Windows.Forms.Button bLink;
    private System.Windows.Forms.OpenFileDialog fileDialog;
  }
}