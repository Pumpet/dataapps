namespace Ctrls
{
  partial class FormSelectCols
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
      this.bOk = new System.Windows.Forms.Button();
      this.chlbCols = new System.Windows.Forms.CheckedListBox();
      this.SuspendLayout();
      // 
      // bOk
      // 
      this.bOk.Location = new System.Drawing.Point(193, 170);
      this.bOk.Name = "bOk";
      this.bOk.Size = new System.Drawing.Size(81, 23);
      this.bOk.TabIndex = 1;
      this.bOk.Text = "Установить";
      this.bOk.UseVisualStyleBackColor = true;
      this.bOk.Click += new System.EventHandler(this.bOk_Click);
      // 
      // chlbCols
      // 
      this.chlbCols.CheckOnClick = true;
      this.chlbCols.FormattingEnabled = true;
      this.chlbCols.Location = new System.Drawing.Point(10, 10);
      this.chlbCols.Name = "chlbCols";
      this.chlbCols.ScrollAlwaysVisible = true;
      this.chlbCols.Size = new System.Drawing.Size(264, 154);
      this.chlbCols.TabIndex = 0;
      this.chlbCols.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chlbCols_ItemCheck);
      // 
      // FormSelectCols
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 199);
      this.Controls.Add(this.chlbCols);
      this.Controls.Add(this.bOk);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.KeyPreview = true;
      this.Name = "FormSelectCols";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Выбор столбцов";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSelectCols_KeyDown);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button bOk;
    private System.Windows.Forms.CheckedListBox chlbCols;
  }
}