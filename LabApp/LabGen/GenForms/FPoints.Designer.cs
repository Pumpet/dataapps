namespace GenForms
{
  partial class FPoints
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPoints));
      this.dataList2 = new Ctrls.DataList();
      this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dataList2)).BeginInit();
      this.SuspendLayout();
      // 
      // dataList2
      // 
      this.dataList2.AllowUserToAddRows = false;
      this.dataList2.AllowUserToDeleteRows = false;
      this.dataList2.AllowUserToOrderColumns = true;
      this.dataList2.AutoGenerateColumns = false;
      this.dataList2.BackgroundColor = System.Drawing.SystemColors.Control;
      this.dataList2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dataList2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      this.dataList2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataList2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataList2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9,
            this.Column6,
            this.Column7,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16});
      this.dataList2.DefaultSort = "PointName";
      this.dataList2.DoCheck = null;
      this.dataList2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataList2.DoClone = null;
      this.dataList2.DoDelete = null;
      this.dataList2.DoExecCommand = null;
      this.dataList2.DoExecEdit = null;
      this.dataList2.DoGetList = null;
      this.dataList2.DoSave = null;
      this.dataList2.DoSetDefaults = null;
      this.dataList2.EnableHeadersVisualStyles = false;
      this.dataList2.KeyNames = "PointId";
      this.dataList2.Location = new System.Drawing.Point(0, 25);
      this.dataList2.Name = "dataList2";
      this.dataList2.ReadOnly = true;
      this.dataList2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataList2.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dataList2.RowHeadersWidth = 23;
      this.dataList2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dataList2.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      this.dataList2.Size = new System.Drawing.Size(512, 215);
      this.dataList2.TabIndex = 3;
      // 
      // Column8
      // 
      this.Column8.DataPropertyName = "PointName";
      this.Column8.HeaderText = "Исследование";
      this.Column8.Name = "Column8";
      this.Column8.ReadOnly = true;
      // 
      // Column9
      // 
      this.Column9.DataPropertyName = "Period";
      this.Column9.HeaderText = "Период";
      this.Column9.Name = "Column9";
      this.Column9.ReadOnly = true;
      // 
      // Column6
      // 
      this.Column6.DataPropertyName = "EtnoName";
      this.Column6.HeaderText = "Этнос";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      // 
      // Column7
      // 
      this.Column7.DataPropertyName = "PopulName";
      this.Column7.HeaderText = "Популяция";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      // 
      // Column10
      // 
      this.Column10.DataPropertyName = "Region";
      this.Column10.HeaderText = "Регион";
      this.Column10.Name = "Column10";
      this.Column10.ReadOnly = true;
      this.Column10.Width = 200;
      // 
      // Column11
      // 
      this.Column11.DataPropertyName = "RegionEn";
      this.Column11.HeaderText = "Регион (англ)";
      this.Column11.Name = "Column11";
      this.Column11.ReadOnly = true;
      this.Column11.Width = 200;
      // 
      // Column12
      // 
      this.Column12.DataPropertyName = "LocName";
      this.Column12.HeaderText = "Место на карте";
      this.Column12.Name = "Column12";
      this.Column12.ReadOnly = true;
      this.Column12.Width = 200;
      // 
      // Column13
      // 
      this.Column13.DataPropertyName = "LocX";
      this.Column13.HeaderText = "Широта";
      this.Column13.Name = "Column13";
      this.Column13.ReadOnly = true;
      // 
      // Column14
      // 
      this.Column14.DataPropertyName = "LocY";
      this.Column14.HeaderText = "Долгота";
      this.Column14.Name = "Column14";
      this.Column14.ReadOnly = true;
      // 
      // Column15
      // 
      this.Column15.DataPropertyName = "Head";
      this.Column15.HeaderText = "Руководитель";
      this.Column15.Name = "Column15";
      this.Column15.ReadOnly = true;
      // 
      // Column16
      // 
      this.Column16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Column16.DataPropertyName = "Comment";
      this.Column16.FillWeight = 200F;
      this.Column16.HeaderText = "Примечание";
      this.Column16.MinimumWidth = 50;
      this.Column16.Name = "Column16";
      this.Column16.ReadOnly = true;
      // 
      // FPoints
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(512, 262);
      this.Controls.Add(this.dataList2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FPoints";
      this.Text = "Исследования";
      this.Controls.SetChildIndex(this.dataList2, 0);
      ((System.ComponentModel.ISupportInitialize)(this.dataList2)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Ctrls.DataList dataList2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
  }
}