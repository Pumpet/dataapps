namespace GenForms
{
  partial class FAnketAttrEdit
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
      this.tValue = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.sbType = new Ctrls.SelectBox(this.components);
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 5;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 271F));
      this.panel.Controls.Add(this.tValue, 2, 1);
      this.panel.Controls.Add(this.label6, 1, 1);
      this.panel.Controls.Add(this.label9, 1, 0);
      this.panel.Controls.Add(this.sbType, 2, 0);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 25);
      this.panel.Margin = new System.Windows.Forms.Padding(0);
      this.panel.Name = "panel";
      this.panel.RowCount = 3;
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.Size = new System.Drawing.Size(387, 68);
      this.panel.TabIndex = 7;
      // 
      // tValue
      // 
      this.panel.SetColumnSpan(this.tValue, 2);
      this.tValue.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tValue.Location = new System.Drawing.Point(124, 28);
      this.tValue.Name = "tValue";
      this.tValue.Size = new System.Drawing.Size(234, 22);
      this.tValue.TabIndex = 1;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(13, 25);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(105, 25);
      this.label6.TabIndex = 7;
      this.label6.Text = "Значение";
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
      this.label9.Text = "Характеристика";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // sbType
      // 
      this.sbType.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbType.DoExecSelect = null;
      this.sbType.DoGetParent = null;
      this.sbType.ExtSource = null;
      this.sbType.ExtSourceFields = null;
      this.sbType.ExtSourceParent = null;
      this.sbType.FormattingEnabled = true;
      this.sbType.IntegralHeight = false;
      this.sbType.KeyNames = null;
      this.sbType.Location = new System.Drawing.Point(124, 3);
      this.sbType.Name = "sbType";
      this.sbType.SelectFormName = "FPopuls";
      this.sbType.Size = new System.Drawing.Size(114, 21);
      this.sbType.TabIndex = 0;
      this.sbType.ThisSource = null;
      // 
      // FAnketAttrEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(387, 115);
      this.Controls.Add(this.panel);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.KeyNames = "AttrId";
      this.Name = "FAnketAttrEdit";
      this.ShowIcon = false;
      this.Text = "Характеристика";
      this.Controls.SetChildIndex(this.panel, 0);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.TextBox tValue;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label9;
    private Ctrls.SelectBox sbType;
  }
}