namespace Ctrls
{
  partial class FormFilter
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilter));
      this.pStr = new System.Windows.Forms.Panel();
      this.chInList = new System.Windows.Forms.CheckBox();
      this.chCs = new System.Windows.Forms.CheckBox();
      this.chEq = new System.Windows.Forms.CheckBox();
      this.tbStr = new System.Windows.Forms.TextBox();
      this.chEmpty = new System.Windows.Forms.CheckBox();
      this.pCommon = new System.Windows.Forms.Panel();
      this.mess = new System.Windows.Forms.Label();
      this.bExec = new System.Windows.Forms.Button();
      this.pDate = new System.Windows.Forms.Panel();
      this.tbDateToS = new Ctrls.DateTimeBox(this.components);
      this.tbDateS = new Ctrls.DateTimeBox(this.components);
      this.chTill = new System.Windows.Forms.CheckBox();
      this.pNum = new System.Windows.Forms.Panel();
      this.tbNumTill = new Ctrls.NumberBox(this.components);
      this.cbSignTill = new System.Windows.Forms.ComboBox();
      this.chTillNum = new System.Windows.Forms.CheckBox();
      this.tbNumber = new Ctrls.NumberBox(this.components);
      this.cbSign = new System.Windows.Forms.ComboBox();
      this.ttip = new System.Windows.Forms.ToolTip(this.components);
      this.pStr.SuspendLayout();
      this.pCommon.SuspendLayout();
      this.pDate.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbDateToS)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbDateS)).BeginInit();
      this.pNum.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbNumTill)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbNumber)).BeginInit();
      this.SuspendLayout();
      // 
      // pStr
      // 
      this.pStr.Controls.Add(this.chInList);
      this.pStr.Controls.Add(this.chCs);
      this.pStr.Controls.Add(this.chEq);
      this.pStr.Controls.Add(this.tbStr);
      this.pStr.Location = new System.Drawing.Point(0, 31);
      this.pStr.Margin = new System.Windows.Forms.Padding(0);
      this.pStr.Name = "pStr";
      this.pStr.Size = new System.Drawing.Size(274, 49);
      this.pStr.TabIndex = 0;
      // 
      // chInList
      // 
      this.chInList.AutoSize = true;
      this.chInList.Location = new System.Drawing.Point(160, 2);
      this.chInList.Name = "chInList";
      this.chInList.Size = new System.Drawing.Size(62, 17);
      this.chInList.TabIndex = 3;
      this.chInList.Text = "список";
      this.chInList.UseVisualStyleBackColor = true;
      this.chInList.CheckedChanged += new System.EventHandler(this.chInList_CheckedChanged);
      // 
      // chCs
      // 
      this.chCs.AutoSize = true;
      this.chCs.Location = new System.Drawing.Point(62, 2);
      this.chCs.Name = "chCs";
      this.chCs.Size = new System.Drawing.Size(96, 17);
      this.chCs.TabIndex = 2;
      this.chCs.Text = "учет регистра";
      this.chCs.UseVisualStyleBackColor = true;
      // 
      // chEq
      // 
      this.chEq.AutoSize = true;
      this.chEq.Location = new System.Drawing.Point(6, 2);
      this.chEq.Name = "chEq";
      this.chEq.Size = new System.Drawing.Size(54, 17);
      this.chEq.TabIndex = 1;
      this.chEq.Text = "точно";
      this.chEq.UseVisualStyleBackColor = true;
      // 
      // tbStr
      // 
      this.tbStr.Location = new System.Drawing.Point(6, 22);
      this.tbStr.Name = "tbStr";
      this.tbStr.Size = new System.Drawing.Size(261, 20);
      this.tbStr.TabIndex = 0;
      this.tbStr.WordWrap = false;
      // 
      // chEmpty
      // 
      this.chEmpty.AutoSize = true;
      this.chEmpty.Location = new System.Drawing.Point(6, 5);
      this.chEmpty.Name = "chEmpty";
      this.chEmpty.Size = new System.Drawing.Size(54, 17);
      this.chEmpty.TabIndex = 0;
      this.chEmpty.Text = "пусто";
      this.chEmpty.UseVisualStyleBackColor = true;
      this.chEmpty.CheckedChanged += new System.EventHandler(this.chEmpty_CheckedChanged);
      // 
      // pCommon
      // 
      this.pCommon.Controls.Add(this.mess);
      this.pCommon.Controls.Add(this.bExec);
      this.pCommon.Controls.Add(this.chEmpty);
      this.pCommon.Location = new System.Drawing.Point(0, 0);
      this.pCommon.Margin = new System.Windows.Forms.Padding(0);
      this.pCommon.Name = "pCommon";
      this.pCommon.Size = new System.Drawing.Size(274, 27);
      this.pCommon.TabIndex = 3;
      // 
      // mess
      // 
      this.mess.Dock = System.Windows.Forms.DockStyle.Right;
      this.mess.ForeColor = System.Drawing.Color.DarkRed;
      this.mess.Location = new System.Drawing.Point(92, 0);
      this.mess.Name = "mess";
      this.mess.Size = new System.Drawing.Size(158, 27);
      this.mess.TabIndex = 5;
      this.mess.Text = "  ";
      this.mess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // bExec
      // 
      this.bExec.Dock = System.Windows.Forms.DockStyle.Right;
      this.bExec.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.bExec.FlatAppearance.BorderSize = 0;
      this.bExec.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
      this.bExec.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
      this.bExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bExec.Image = global::Ctrls.Properties.Resources.play;
      this.bExec.Location = new System.Drawing.Point(250, 0);
      this.bExec.Name = "bExec";
      this.bExec.Size = new System.Drawing.Size(24, 27);
      this.bExec.TabIndex = 4;
      this.bExec.UseVisualStyleBackColor = true;
      this.bExec.Click += new System.EventHandler(this.b_Click);
      // 
      // pDate
      // 
      this.pDate.Controls.Add(this.tbDateToS);
      this.pDate.Controls.Add(this.tbDateS);
      this.pDate.Controls.Add(this.chTill);
      this.pDate.Location = new System.Drawing.Point(0, 89);
      this.pDate.Margin = new System.Windows.Forms.Padding(0);
      this.pDate.Name = "pDate";
      this.pDate.Size = new System.Drawing.Size(238, 31);
      this.pDate.TabIndex = 1;
      // 
      // tbDateToS
      // 
      this.tbDateToS.Enabled = false;
      this.tbDateToS.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tbDateToS.Location = new System.Drawing.Point(141, 3);
      this.tbDateToS.Name = "tbDateToS";
      this.tbDateToS.Size = new System.Drawing.Size(92, 20);
      this.tbDateToS.SqlType = Ctrls.DateTimeSqlType.None;
      this.tbDateToS.TabIndex = 5;
      this.tbDateToS.Text = "01.01.0001";
      this.tbDateToS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // tbDateS
      // 
      this.tbDateS.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tbDateS.Location = new System.Drawing.Point(3, 3);
      this.tbDateS.Name = "tbDateS";
      this.tbDateS.Size = new System.Drawing.Size(92, 20);
      this.tbDateS.SqlType = Ctrls.DateTimeSqlType.None;
      this.tbDateS.TabIndex = 4;
      this.tbDateS.Text = "01.01.0001";
      this.tbDateS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // chTill
      // 
      this.chTill.AutoSize = true;
      this.chTill.Location = new System.Drawing.Point(103, 5);
      this.chTill.Name = "chTill";
      this.chTill.Size = new System.Drawing.Size(38, 17);
      this.chTill.TabIndex = 1;
      this.chTill.Text = "по";
      this.chTill.UseVisualStyleBackColor = true;
      this.chTill.CheckedChanged += new System.EventHandler(this.chTill_CheckedChanged);
      // 
      // pNum
      // 
      this.pNum.Controls.Add(this.tbNumTill);
      this.pNum.Controls.Add(this.cbSignTill);
      this.pNum.Controls.Add(this.chTillNum);
      this.pNum.Controls.Add(this.tbNumber);
      this.pNum.Controls.Add(this.cbSign);
      this.pNum.Location = new System.Drawing.Point(0, 127);
      this.pNum.Margin = new System.Windows.Forms.Padding(0);
      this.pNum.Name = "pNum";
      this.pNum.Size = new System.Drawing.Size(241, 53);
      this.pNum.TabIndex = 2;
      // 
      // tbNumTill
      // 
      this.tbNumTill.Location = new System.Drawing.Point(50, 26);
      this.tbNumTill.Name = "tbNumTill";
      this.tbNumTill.Size = new System.Drawing.Size(141, 20);
      this.tbNumTill.TabIndex = 3;
      this.tbNumTill.Text = "0";
      this.tbNumTill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // cbSignTill
      // 
      this.cbSignTill.FormattingEnabled = true;
      this.cbSignTill.Items.AddRange(new object[] {
            "<",
            "<="});
      this.cbSignTill.Location = new System.Drawing.Point(197, 26);
      this.cbSignTill.Name = "cbSignTill";
      this.cbSignTill.Size = new System.Drawing.Size(38, 21);
      this.cbSignTill.TabIndex = 4;
      this.cbSignTill.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSignTill_KeyDown);
      // 
      // chTillNum
      // 
      this.chTillNum.AutoSize = true;
      this.chTillNum.Location = new System.Drawing.Point(6, 29);
      this.chTillNum.Name = "chTillNum";
      this.chTillNum.Size = new System.Drawing.Size(38, 17);
      this.chTillNum.TabIndex = 2;
      this.chTillNum.Text = "до";
      this.chTillNum.UseVisualStyleBackColor = true;
      this.chTillNum.CheckedChanged += new System.EventHandler(this.chTillNum_CheckedChanged);
      // 
      // tbNumber
      // 
      this.tbNumber.Location = new System.Drawing.Point(50, 2);
      this.tbNumber.Name = "tbNumber";
      this.tbNumber.Size = new System.Drawing.Size(141, 20);
      this.tbNumber.TabIndex = 1;
      this.tbNumber.Text = "0";
      this.tbNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // cbSign
      // 
      this.cbSign.FormattingEnabled = true;
      this.cbSign.Items.AddRange(new object[] {
            "=",
            ">",
            "<",
            "<>",
            ">=",
            "<="});
      this.cbSign.Location = new System.Drawing.Point(6, 1);
      this.cbSign.Name = "cbSign";
      this.cbSign.Size = new System.Drawing.Size(38, 21);
      this.cbSign.TabIndex = 0;
      this.cbSign.SelectionChangeCommitted += new System.EventHandler(this.cbSign_SelectionChangeCommitted);
      this.cbSign.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSign_KeyDown);
      // 
      // FormFilter
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(319, 188);
      this.Controls.Add(this.pNum);
      this.Controls.Add(this.pDate);
      this.Controls.Add(this.pCommon);
      this.Controls.Add(this.pStr);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormFilter";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Фильтр:";
      this.Shown += new System.EventHandler(this.FormFilter_Shown);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormFilter_KeyDown);
      this.pStr.ResumeLayout(false);
      this.pStr.PerformLayout();
      this.pCommon.ResumeLayout(false);
      this.pCommon.PerformLayout();
      this.pDate.ResumeLayout(false);
      this.pDate.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbDateToS)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbDateS)).EndInit();
      this.pNum.ResumeLayout(false);
      this.pNum.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbNumTill)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbNumber)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pStr;
    private System.Windows.Forms.TextBox tbStr;
    private System.Windows.Forms.CheckBox chEmpty;
    private System.Windows.Forms.Panel pCommon;
    private System.Windows.Forms.CheckBox chCs;
    private System.Windows.Forms.CheckBox chEq;
    private System.Windows.Forms.Panel pDate;
    private System.Windows.Forms.Panel pNum;
    private System.Windows.Forms.ComboBox cbSign;
    private System.Windows.Forms.CheckBox chTill;
    private System.Windows.Forms.Button bExec;
    private System.Windows.Forms.Label mess;
    private DateTimeBox tbDateToS;
    private DateTimeBox tbDateS;
    private NumberBox tbNumber;
    private System.Windows.Forms.ToolTip ttip;
    private NumberBox tbNumTill;
    private System.Windows.Forms.ComboBox cbSignTill;
    private System.Windows.Forms.CheckBox chTillNum;
    private System.Windows.Forms.CheckBox chInList;
  }
}