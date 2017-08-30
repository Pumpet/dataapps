namespace Ctrls
{
  partial class FormSearch
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
      this.chCs = new System.Windows.Forms.CheckBox();
      this.chEq = new System.Windows.Forms.CheckBox();
      this.tbStr = new System.Windows.Forms.TextBox();
      this.bExec = new System.Windows.Forms.Button();
      this.ttip = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // chCs
      // 
      this.chCs.AutoSize = true;
      this.chCs.Location = new System.Drawing.Point(150, 38);
      this.chCs.Name = "chCs";
      this.chCs.Size = new System.Drawing.Size(121, 17);
      this.chCs.TabIndex = 2;
      this.chCs.Text = "учитывать регистр";
      this.chCs.UseVisualStyleBackColor = true;
      // 
      // chEq
      // 
      this.chEq.AutoSize = true;
      this.chEq.Location = new System.Drawing.Point(12, 38);
      this.chEq.Name = "chEq";
      this.chEq.Size = new System.Drawing.Size(132, 17);
      this.chEq.TabIndex = 1;
      this.chEq.Text = "точное соответствие";
      this.chEq.UseVisualStyleBackColor = true;
      // 
      // tbStr
      // 
      this.tbStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbStr.Location = new System.Drawing.Point(12, 12);
      this.tbStr.Name = "tbStr";
      this.tbStr.Size = new System.Drawing.Size(259, 20);
      this.tbStr.TabIndex = 0;
      // 
      // bExec
      // 
      this.bExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bExec.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bExec.FlatAppearance.BorderSize = 0;
      this.bExec.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
      this.bExec.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
      this.bExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bExec.Image = global::Ctrls.Properties.Resources.play;
      this.bExec.Location = new System.Drawing.Point(277, 8);
      this.bExec.Name = "bExec";
      this.bExec.Size = new System.Drawing.Size(19, 27);
      this.bExec.TabIndex = 6;
      this.bExec.UseVisualStyleBackColor = true;
      this.bExec.Click += new System.EventHandler(this.bExec_Click);
      // 
      // FormSearch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 61);
      this.Controls.Add(this.chCs);
      this.Controls.Add(this.bExec);
      this.Controls.Add(this.chEq);
      this.Controls.Add(this.tbStr);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(800, 95);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(320, 95);
      this.Name = "FormSearch";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Найти";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSearch_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button bExec;
    private System.Windows.Forms.CheckBox chCs;
    private System.Windows.Forms.CheckBox chEq;
    private System.Windows.Forms.TextBox tbStr;
    private System.Windows.Forms.ToolTip ttip;
  }
}