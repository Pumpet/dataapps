namespace GenForms
{
  partial class FRepSamples
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.tRUSID = new System.Windows.Forms.TextBox();
      this.tSampleCode = new System.Windows.Forms.TextBox();
      this.tGPID = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.sbPopul = new Ctrls.SelectBox(this.components);
      this.sbExped = new Ctrls.SelectBox(this.components);
      this.label10 = new System.Windows.Forms.Label();
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 4;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.panel.Controls.Add(this.label1, 1, 2);
      this.panel.Controls.Add(this.label2, 1, 3);
      this.panel.Controls.Add(this.label4, 1, 4);
      this.panel.Controls.Add(this.tRUSID, 2, 3);
      this.panel.Controls.Add(this.tSampleCode, 1, 2);
      this.panel.Controls.Add(this.tGPID, 2, 4);
      this.panel.Controls.Add(this.label9, 1, 1);
      this.panel.Controls.Add(this.sbPopul, 2, 1);
      this.panel.Controls.Add(this.sbExped, 2, 0);
      this.panel.Controls.Add(this.label10, 1, 0);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 25);
      this.panel.Margin = new System.Windows.Forms.Padding(0);
      this.panel.Name = "panel";
      this.panel.RowCount = 7;
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.Size = new System.Drawing.Size(320, 179);
      this.panel.TabIndex = 6;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(13, 50);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(105, 25);
      this.label1.TabIndex = 0;
      this.label1.Text = "Номер образца";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(13, 75);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(105, 25);
      this.label2.TabIndex = 1;
      this.label2.Text = "Анкета № РУС";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label4.Location = new System.Drawing.Point(13, 100);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(105, 25);
      this.label4.TabIndex = 3;
      this.label4.Text = "Анкета GPID";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // tRUSID
      // 
      this.tRUSID.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tRUSID.Location = new System.Drawing.Point(124, 78);
      this.tRUSID.Name = "tRUSID";
      this.tRUSID.Size = new System.Drawing.Size(114, 22);
      this.tRUSID.TabIndex = 3;
      // 
      // tSampleCode
      // 
      this.tSampleCode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tSampleCode.Location = new System.Drawing.Point(124, 53);
      this.tSampleCode.Name = "tSampleCode";
      this.tSampleCode.Size = new System.Drawing.Size(114, 22);
      this.tSampleCode.TabIndex = 2;
      // 
      // tGPID
      // 
      this.tGPID.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tGPID.Location = new System.Drawing.Point(124, 103);
      this.tGPID.Name = "tGPID";
      this.tGPID.Size = new System.Drawing.Size(114, 22);
      this.tGPID.TabIndex = 4;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label9.Location = new System.Drawing.Point(13, 25);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(105, 25);
      this.label9.TabIndex = 14;
      this.label9.Text = "Популяция";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // sbPopul
      // 
      this.sbPopul.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbPopul.DoExecSelect = null;
      this.sbPopul.DoGetParent = null;
      this.sbPopul.ExtSource = null;
      this.sbPopul.ExtSourceFields = null;
      this.sbPopul.ExtSourceParent = null;
      this.sbPopul.FormattingEnabled = true;
      this.sbPopul.IntegralHeight = false;
      this.sbPopul.KeyNames = null;
      this.sbPopul.Location = new System.Drawing.Point(124, 28);
      this.sbPopul.Name = "sbPopul";
      this.sbPopul.Nullable = true;
      this.sbPopul.SelectFormName = "FPopuls";
      this.sbPopul.Size = new System.Drawing.Size(114, 21);
      this.sbPopul.TabIndex = 1;
      this.sbPopul.ThisSource = null;
      // 
      // sbExped
      // 
      this.sbExped.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbExped.DoExecSelect = null;
      this.sbExped.DoGetParent = null;
      this.sbExped.ExtSource = null;
      this.sbExped.ExtSourceFields = null;
      this.sbExped.ExtSourceParent = null;
      this.sbExped.FormattingEnabled = true;
      this.sbExped.IntegralHeight = false;
      this.sbExped.KeyNames = null;
      this.sbExped.Location = new System.Drawing.Point(124, 3);
      this.sbExped.Name = "sbExped";
      this.sbExped.Nullable = true;
      this.sbExped.SelectFormName = "FExpeds";
      this.sbExped.Size = new System.Drawing.Size(114, 21);
      this.sbExped.TabIndex = 0;
      this.sbExped.ThisSource = null;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label10.Location = new System.Drawing.Point(13, 0);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(105, 25);
      this.label10.TabIndex = 17;
      this.label10.Text = "Экспедиция";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // FRepSamples
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(320, 226);
      this.Controls.Add(this.panel);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.DefaultControlName = "sbExped";
      this.Name = "FRepSamples";
      this.ShowIcon = false;
      this.Text = "FRepSamples";
      this.BeforeSave += new System.Action(this.FRepSamples_BeforeSave);
      this.Controls.SetChildIndex(this.panel, 0);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox tRUSID;
    private System.Windows.Forms.TextBox tSampleCode;
    private System.Windows.Forms.TextBox tGPID;
    private System.Windows.Forms.Label label9;
    private Ctrls.SelectBox sbPopul;
    private Ctrls.SelectBox sbExped;
    private System.Windows.Forms.Label label10;
  }
}