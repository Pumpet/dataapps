namespace GenForms
{
  partial class FLists
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.sbTypes = new Ctrls.SelectBox(this.components);
      this.dataList1 = new Ctrls.DataList();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataList1)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.sbTypes);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 25);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(281, 32);
      this.panel1.TabIndex = 3;
      // 
      // sbTypes
      // 
      this.sbTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.sbTypes.DoExecSelect = null;
      this.sbTypes.DoGetParent = null;
      this.sbTypes.ExtSource = null;
      this.sbTypes.ExtSourceFields = null;
      this.sbTypes.ExtSourceParent = null;
      this.sbTypes.FormattingEnabled = true;
      this.sbTypes.IntegralHeight = false;
      this.sbTypes.KeyNames = null;
      this.sbTypes.Location = new System.Drawing.Point(12, 5);
      this.sbTypes.Name = "sbTypes";
      this.sbTypes.SelectFormName = null;
      this.sbTypes.Size = new System.Drawing.Size(257, 21);
      this.sbTypes.TabIndex = 0;
      this.sbTypes.ThisSource = null;
      // 
      // dataList1
      // 
      this.dataList1.AllowUserToAddRows = false;
      this.dataList1.AllowUserToDeleteRows = false;
      this.dataList1.AllowUserToOrderColumns = true;
      this.dataList1.AutoGenerateColumns = false;
      this.dataList1.BackgroundColor = System.Drawing.SystemColors.Control;
      this.dataList1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dataList1.CanExcelExport = false;
      this.dataList1.CanFilter = false;
      this.dataList1.CanSearch = false;
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
            this.Column1,
            this.Code});
      this.dataList1.DefaultSort = "Item";
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
      this.dataList1.KeyNames = "ListId";
      this.dataList1.Location = new System.Drawing.Point(0, 57);
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
      this.dataList1.Size = new System.Drawing.Size(281, 283);
      this.dataList1.TabIndex = 0;
      this.dataList1.OnSetFilter += new System.Func<object>(this.dataList1_OnSetFilter);
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "Item";
      this.Column1.HeaderText = "Наименование";
      this.Column1.Name = "Column1";
      this.Column1.Width = 150;
      // 
      // Code
      // 
      this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Code.DataPropertyName = "Code";
      this.Code.HeaderText = "Системный код";
      this.Code.Name = "Code";
      // 
      // FLists
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(281, 362);
      this.Controls.Add(this.dataList1);
      this.Controls.Add(this.panel1);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.DefaultControlName = "dataList1";
      this.Name = "FLists";
      this.ShowIcon = false;
      this.Text = "Списки";
      this.OnControlChanged += new System.Action<System.Windows.Forms.Control, bool>(this.FLists_OnControlChanged);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.dataList1, 0);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataList1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private Ctrls.SelectBox sbTypes;
    private Ctrls.DataList dataList1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Code;
  }
}