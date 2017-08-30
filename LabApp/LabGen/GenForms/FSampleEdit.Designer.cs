namespace GenForms
{
  partial class FSampleEdit
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSampleEdit));
      this.panel = new System.Windows.Forms.TableLayoutPanel();
      this.tSampleCode = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.sbAnket = new Ctrls.SelectBox(this.components);
      this.sbSampleType = new Ctrls.SelectBox(this.components);
      this.label10 = new System.Windows.Forms.Label();
      this.tAnketGPID = new System.Windows.Forms.TextBox();
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 5;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 183F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 236F));
      this.panel.Controls.Add(this.tSampleCode, 2, 2);
      this.panel.Controls.Add(this.label6, 1, 2);
      this.panel.Controls.Add(this.label9, 1, 1);
      this.panel.Controls.Add(this.sbAnket, 2, 1);
      this.panel.Controls.Add(this.sbSampleType, 2, 0);
      this.panel.Controls.Add(this.label10, 1, 0);
      this.panel.Controls.Add(this.tAnketGPID, 3, 1);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 25);
      this.panel.Margin = new System.Windows.Forms.Padding(0);
      this.panel.Name = "panel";
      this.panel.RowCount = 4;
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.Size = new System.Drawing.Size(425, 118);
      this.panel.TabIndex = 7;
      // 
      // tSampleCode
      // 
      this.tSampleCode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tSampleCode.Location = new System.Drawing.Point(82, 53);
      this.tSampleCode.Name = "tSampleCode";
      this.tSampleCode.Size = new System.Drawing.Size(177, 22);
      this.tSampleCode.TabIndex = 3;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(13, 50);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(63, 25);
      this.label6.TabIndex = 7;
      this.label6.Text = "Номер";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label9.Location = new System.Drawing.Point(13, 25);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(63, 25);
      this.label9.TabIndex = 14;
      this.label9.Text = "Анкета";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // sbAnket
      // 
      this.sbAnket.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbAnket.DoExecSelect = null;
      this.sbAnket.DoGetParent = null;
      this.sbAnket.ExtSource = null;
      this.sbAnket.ExtSourceFields = null;
      this.sbAnket.ExtSourceParent = null;
      this.sbAnket.FormattingEnabled = true;
      this.sbAnket.IntegralHeight = false;
      this.sbAnket.KeyNames = null;
      this.sbAnket.Location = new System.Drawing.Point(82, 28);
      this.sbAnket.Name = "sbAnket";
      this.sbAnket.SelectFormName = "FPopuls";
      this.sbAnket.Size = new System.Drawing.Size(177, 21);
      this.sbAnket.TabIndex = 1;
      this.sbAnket.ThisSource = null;
      // 
      // sbSampleType
      // 
      this.sbSampleType.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbSampleType.DoExecSelect = null;
      this.sbSampleType.DoGetParent = null;
      this.sbSampleType.ExtSource = null;
      this.sbSampleType.ExtSourceFields = null;
      this.sbSampleType.ExtSourceParent = null;
      this.sbSampleType.FormattingEnabled = true;
      this.sbSampleType.IntegralHeight = false;
      this.sbSampleType.KeyNames = null;
      this.sbSampleType.Location = new System.Drawing.Point(82, 3);
      this.sbSampleType.Name = "sbSampleType";
      this.sbSampleType.SelectFormName = "FExpeds";
      this.sbSampleType.Size = new System.Drawing.Size(177, 21);
      this.sbSampleType.TabIndex = 0;
      this.sbSampleType.ThisSource = null;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label10.Location = new System.Drawing.Point(13, 0);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(63, 25);
      this.label10.TabIndex = 17;
      this.label10.Text = "Тип";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // tAnketGPID
      // 
      this.tAnketGPID.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tAnketGPID.Location = new System.Drawing.Point(265, 28);
      this.tAnketGPID.Name = "tAnketGPID";
      this.tAnketGPID.ReadOnly = true;
      this.tAnketGPID.Size = new System.Drawing.Size(128, 22);
      this.tAnketGPID.TabIndex = 2;
      // 
      // FSampleEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(425, 165);
      this.Controls.Add(this.panel);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.DefaultControlName = "sbSampleType";
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyNames = "SampleId";
      this.Name = "FSampleEdit";
      this.Text = "Образец";
      this.Controls.SetChildIndex(this.panel, 0);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.TextBox tSampleCode;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label9;
    private Ctrls.SelectBox sbAnket;
    private Ctrls.SelectBox sbSampleType;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox tAnketGPID;
  }
}