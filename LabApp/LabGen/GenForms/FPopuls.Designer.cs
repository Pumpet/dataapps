namespace GenForms
{
  partial class FPopuls
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPopuls));
      this.dataList1 = new Ctrls.DataList();
      this.EtnoName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.PopulName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.PopulNameEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.PopulCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dataList1)).BeginInit();
      this.SuspendLayout();
      // 
      // dataList1
      // 
      this.dataList1.AllowUserToAddRows = false;
      this.dataList1.AllowUserToDeleteRows = false;
      this.dataList1.AllowUserToOrderColumns = true;
      this.dataList1.AutoGenerateColumns = false;
      this.dataList1.BackgroundColor = System.Drawing.SystemColors.Control;
      this.dataList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dataList1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      this.dataList1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataList1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataList1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EtnoName,
            this.PopulName,
            this.PopulNameEn,
            this.PopulCode,
            this.Comment});
      this.dataList1.DefaultSort = "Name asc";
      this.dataList1.DoCheck = null;
      this.dataList1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataList1.DoClone = null;
      this.dataList1.DoDelete = null;
      this.dataList1.DoExecCommand = null;
      this.dataList1.DoExecEdit = null;
      this.dataList1.DoGetList = null;
      this.dataList1.DoSave = null;
      this.dataList1.DoSetDefaults = null;
      this.dataList1.EditFormName = "FPopulEdit";
      this.dataList1.EnableHeadersVisualStyles = false;
      this.dataList1.KeyNames = "PopulId";
      this.dataList1.Location = new System.Drawing.Point(0, 25);
      this.dataList1.Name = "dataList1";
      this.dataList1.ReadOnly = true;
      this.dataList1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataList1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dataList1.RowHeadersWidth = 23;
      this.dataList1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dataList1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      this.dataList1.Size = new System.Drawing.Size(512, 215);
      this.dataList1.TabIndex = 3;
      this.dataList1.OnSetMenu += new System.Action<object>(this.dataList1_OnSetMenu);
      this.dataList1.OnExecCommand += new System.Func<string, object, object, object, object[], object>(this.dataList1_OnExecCommand);
      // 
      // EtnoName
      // 
      this.EtnoName.DataPropertyName = "EtnoName";
      this.EtnoName.HeaderText = "Этнос";
      this.EtnoName.Name = "EtnoName";
      this.EtnoName.ReadOnly = true;
      // 
      // PopulName
      // 
      this.PopulName.DataPropertyName = "Name";
      this.PopulName.HeaderText = "Название";
      this.PopulName.Name = "PopulName";
      this.PopulName.ReadOnly = true;
      this.PopulName.Width = 89;
      // 
      // PopulNameEn
      // 
      this.PopulNameEn.DataPropertyName = "NameEn";
      this.PopulNameEn.HeaderText = "Название (англ)";
      this.PopulNameEn.Name = "PopulNameEn";
      this.PopulNameEn.ReadOnly = true;
      // 
      // PopulCode
      // 
      this.PopulCode.DataPropertyName = "Codes";
      this.PopulCode.HeaderText = "Код";
      this.PopulCode.Name = "PopulCode";
      this.PopulCode.ReadOnly = true;
      // 
      // Comment
      // 
      this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Comment.DataPropertyName = "Comment";
      this.Comment.HeaderText = "Примечание";
      this.Comment.Name = "Comment";
      this.Comment.ReadOnly = true;
      // 
      // FPopuls
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(512, 262);
      this.Controls.Add(this.dataList1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FPopuls";
      this.Text = "Популяции";
      this.Controls.SetChildIndex(this.dataList1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.dataList1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Ctrls.DataList dataList1;
    private System.Windows.Forms.DataGridViewTextBoxColumn EtnoName;
    private System.Windows.Forms.DataGridViewTextBoxColumn PopulName;
    private System.Windows.Forms.DataGridViewTextBoxColumn PopulNameEn;
    private System.Windows.Forms.DataGridViewTextBoxColumn PopulCode;
    private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
  }
}