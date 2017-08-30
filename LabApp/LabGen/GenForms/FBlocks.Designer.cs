namespace GenForms
{
  partial class FBlocks
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBlocks));
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.sbFridge = new Ctrls.SelectBox(this.components);
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.listBlocks = new Ctrls.DataList();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.listBlockItems = new Ctrls.DataList();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.SampleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.VialType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.listBlocks)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.listBlockItems)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.sbFridge);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 25);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(645, 32);
      this.panel1.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Холодильник";
      // 
      // sbFridge
      // 
      this.sbFridge.DoExecSelect = null;
      this.sbFridge.DoGetParent = null;
      this.sbFridge.ExtSource = null;
      this.sbFridge.ExtSourceFields = null;
      this.sbFridge.ExtSourceParent = null;
      this.sbFridge.FormattingEnabled = true;
      this.sbFridge.IntegralHeight = false;
      this.sbFridge.KeyNames = null;
      this.sbFridge.Location = new System.Drawing.Point(88, 5);
      this.sbFridge.Name = "sbFridge";
      this.sbFridge.SelectFormName = "FPopuls";
      this.sbFridge.Size = new System.Drawing.Size(131, 21);
      this.sbFridge.TabIndex = 2;
      this.sbFridge.ThisSource = null;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 57);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.listBlocks);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.listBlockItems);
      this.splitContainer1.Size = new System.Drawing.Size(645, 355);
      this.splitContainer1.SplitterDistance = 212;
      this.splitContainer1.TabIndex = 4;
      // 
      // listBlocks
      // 
      this.listBlocks.AllowUserToAddRows = false;
      this.listBlocks.AllowUserToDeleteRows = false;
      this.listBlocks.AllowUserToOrderColumns = true;
      this.listBlocks.AutoGenerateColumns = false;
      this.listBlocks.BackgroundColor = System.Drawing.SystemColors.Control;
      this.listBlocks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.listBlocks.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      this.listBlocks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.listBlocks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.listBlocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column3});
      this.listBlocks.DefaultSort = "BlockCode";
      this.listBlocks.DoCheck = null;
      this.listBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBlocks.DoClone = null;
      this.listBlocks.DoDelete = null;
      this.listBlocks.DoExecCommand = null;
      this.listBlocks.DoExecEdit = null;
      this.listBlocks.DoGetList = null;
      this.listBlocks.DoSave = null;
      this.listBlocks.DoSetDefaults = null;
      this.listBlocks.EditFormName = "FBlockEdit";
      this.listBlocks.EnableHeadersVisualStyles = false;
      this.listBlocks.KeyNames = "BlockId";
      this.listBlocks.Location = new System.Drawing.Point(0, 0);
      this.listBlocks.Name = "listBlocks";
      this.listBlocks.ReadOnly = true;
      this.listBlocks.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.listBlocks.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.listBlocks.RowHeadersWidth = 23;
      this.listBlocks.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.listBlocks.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      this.listBlocks.Size = new System.Drawing.Size(645, 212);
      this.listBlocks.TabIndex = 0;
      this.listBlocks.OnSetFilter += new System.Func<object>(this.listBlocks_OnSetFilter);
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "BlockCode";
      this.Column2.HeaderText = "Номер";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // Column4
      // 
      this.Column4.DataPropertyName = "Fridge";
      this.Column4.HeaderText = "Холодильник";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      // 
      // Column5
      // 
      this.Column5.DataPropertyName = "FridgeModule";
      this.Column5.HeaderText = "Отсек";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      // 
      // Column6
      // 
      this.Column6.DataPropertyName = "FridgeShelf";
      this.Column6.HeaderText = "Полка";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      // 
      // Column7
      // 
      this.Column7.DataPropertyName = "Container";
      this.Column7.HeaderText = "Контейнер";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      // 
      // Column3
      // 
      this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.Column3.DataPropertyName = "Lab";
      this.Column3.HeaderText = "Лаборатория";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Width = 103;
      // 
      // listBlockItems
      // 
      this.listBlockItems.AllowUserToAddRows = false;
      this.listBlockItems.AllowUserToDeleteRows = false;
      this.listBlockItems.AllowUserToOrderColumns = true;
      this.listBlockItems.AutoGenerateColumns = false;
      this.listBlockItems.BackgroundColor = System.Drawing.SystemColors.Control;
      this.listBlockItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.listBlockItems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      this.listBlockItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.listBlockItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.listBlockItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column8,
            this.SampleType,
            this.VialType});
      this.listBlockItems.DataControllerName = "BlockItems";
      this.listBlockItems.DefaultSort = "BlockItemCode";
      this.listBlockItems.DoCheck = null;
      this.listBlockItems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBlockItems.DoClone = null;
      this.listBlockItems.DoDelete = null;
      this.listBlockItems.DoExecCommand = null;
      this.listBlockItems.DoExecEdit = null;
      this.listBlockItems.DoGetList = null;
      this.listBlockItems.DoSave = null;
      this.listBlockItems.DoSetDefaults = null;
      this.listBlockItems.EnableHeadersVisualStyles = false;
      this.listBlockItems.FilterKeyNames = "BlockId = BlockId";
      this.listBlockItems.KeyNames = "BlockItemId";
      this.listBlockItems.Location = new System.Drawing.Point(0, 0);
      this.listBlockItems.MainList = false;
      this.listBlockItems.Name = "listBlockItems";
      this.listBlockItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.listBlockItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.listBlockItems.RowHeadersWidth = 23;
      this.listBlockItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.listBlockItems.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      this.listBlockItems.Size = new System.Drawing.Size(645, 139);
      this.listBlockItems.TabIndex = 0;
      this.listBlockItems.OnSetMenu += new System.Action<object>(this.listBlockItems_OnSetMenu);
      this.listBlockItems.OnExecCommand += new System.Func<string, object, object, object, object[], object>(this.listBlockItems_OnExecCommand);
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "BlockItemCode";
      this.Column1.HeaderText = "Номер ячейки";
      this.Column1.Name = "Column1";
      // 
      // Column8
      // 
      this.Column8.DataPropertyName = "SampleCode";
      this.Column8.HeaderText = "Номер образца";
      this.Column8.Name = "Column8";
      this.Column8.ReadOnly = true;
      this.Column8.Width = 115;
      // 
      // SampleType
      // 
      this.SampleType.DataPropertyName = "SampleType";
      this.SampleType.HeaderText = "Тип";
      this.SampleType.Name = "SampleType";
      this.SampleType.ReadOnly = true;
      // 
      // VialType
      // 
      this.VialType.DataPropertyName = "VialType";
      this.VialType.HeaderText = "Пробирка";
      this.VialType.Name = "VialType";
      this.VialType.ReadOnly = true;
      // 
      // FBlocks
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(645, 434);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.panel1);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.DefaultControlName = "listBlocks";
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FBlocks";
      this.Text = "Штативы";
      this.OnControlChanged += new System.Action<System.Windows.Forms.Control, bool>(this.FBlocks_OnControlChanged);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.splitContainer1, 0);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.listBlocks)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.listBlockItems)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private Ctrls.DataList listBlocks;
    private System.Windows.Forms.Label label1;
    private Ctrls.SelectBox sbFridge;
    private Ctrls.DataList listBlockItems;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    private System.Windows.Forms.DataGridViewTextBoxColumn SampleType;
    private System.Windows.Forms.DataGridViewTextBoxColumn VialType;
  }
}