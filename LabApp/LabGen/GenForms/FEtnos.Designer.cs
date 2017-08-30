namespace GenForms
{
  partial class FEtnos
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FEtnos));
      this.dataList1 = new Ctrls.DataList();
      this.EtnoName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.EtnoNameEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.EtnoNameEn});
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
      this.dataList1.EnableHeadersVisualStyles = false;
      this.dataList1.KeyNames = "EtnoId";
      this.dataList1.Location = new System.Drawing.Point(0, 25);
      this.dataList1.Name = "dataList1";
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
      this.dataList1.Size = new System.Drawing.Size(387, 215);
      this.dataList1.TabIndex = 3;
      // 
      // EtnoName
      // 
      this.EtnoName.DataPropertyName = "Name";
      this.EtnoName.HeaderText = "Название этноса";
      this.EtnoName.Name = "EtnoName";
      this.EtnoName.Width = 150;
      // 
      // EtnoNameEn
      // 
      this.EtnoNameEn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.EtnoNameEn.DataPropertyName = "NameEn";
      this.EtnoNameEn.HeaderText = "Английское название";
      this.EtnoNameEn.Name = "EtnoNameEn";
      // 
      // FEtnos
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(387, 262);
      this.Controls.Add(this.dataList1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FEtnos";
      this.Text = "Этносы";
      this.Controls.SetChildIndex(this.dataList1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.dataList1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Ctrls.DataList dataList1;
    private System.Windows.Forms.DataGridViewTextBoxColumn EtnoName;
    private System.Windows.Forms.DataGridViewTextBoxColumn EtnoNameEn;
  }
}