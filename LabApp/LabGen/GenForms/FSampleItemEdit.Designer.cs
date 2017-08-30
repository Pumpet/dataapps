namespace GenForms
{
  partial class FSampleItemEdit
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSampleItemEdit));
      this.panel = new System.Windows.Forms.TableLayoutPanel();
      this.tComment = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tStoreName = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.sbBlockItem = new Ctrls.SelectBox(this.components);
      this.sbSampleItemType = new Ctrls.SelectBox(this.components);
      this.label10 = new System.Windows.Forms.Label();
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 5;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 236F));
      this.panel.Controls.Add(this.tComment, 2, 4);
      this.panel.Controls.Add(this.label2, 1, 4);
      this.panel.Controls.Add(this.tStoreName, 2, 2);
      this.panel.Controls.Add(this.label9, 1, 1);
      this.panel.Controls.Add(this.sbBlockItem, 2, 1);
      this.panel.Controls.Add(this.sbSampleItemType, 2, 0);
      this.panel.Controls.Add(this.label10, 1, 0);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 25);
      this.panel.Margin = new System.Windows.Forms.Padding(0);
      this.panel.Name = "panel";
      this.panel.RowCount = 6;
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.Size = new System.Drawing.Size(512, 199);
      this.panel.TabIndex = 8;
      // 
      // tComment
      // 
      this.panel.SetColumnSpan(this.tComment, 2);
      this.tComment.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tComment.Location = new System.Drawing.Point(115, 103);
      this.tComment.Name = "tComment";
      this.tComment.Size = new System.Drawing.Size(278, 22);
      this.tComment.TabIndex = 20;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(13, 100);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(96, 25);
      this.label2.TabIndex = 19;
      this.label2.Text = "Примечание";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // tStoreName
      // 
      this.panel.SetColumnSpan(this.tStoreName, 2);
      this.tStoreName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tStoreName.Location = new System.Drawing.Point(115, 53);
      this.tStoreName.Multiline = true;
      this.tStoreName.Name = "tStoreName";
      this.tStoreName.ReadOnly = true;
      this.panel.SetRowSpan(this.tStoreName, 2);
      this.tStoreName.Size = new System.Drawing.Size(278, 44);
      this.tStoreName.TabIndex = 3;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label9.Location = new System.Drawing.Point(13, 25);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(96, 25);
      this.label9.TabIndex = 14;
      this.label9.Text = "Хранение";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // sbBlockItem
      // 
      this.panel.SetColumnSpan(this.sbBlockItem, 2);
      this.sbBlockItem.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbBlockItem.DoExecSelect = null;
      this.sbBlockItem.DoGetParent = null;
      this.sbBlockItem.ExtSource = null;
      this.sbBlockItem.ExtSourceFields = null;
      this.sbBlockItem.ExtSourceParent = null;
      this.sbBlockItem.FormattingEnabled = true;
      this.sbBlockItem.IntegralHeight = false;
      this.sbBlockItem.KeyNames = null;
      this.sbBlockItem.Location = new System.Drawing.Point(115, 28);
      this.sbBlockItem.Name = "sbBlockItem";
      this.sbBlockItem.SelectFormName = "FBlocks";
      this.sbBlockItem.Size = new System.Drawing.Size(278, 21);
      this.sbBlockItem.TabIndex = 1;
      this.sbBlockItem.ThisSource = null;
      // 
      // sbSampleItemType
      // 
      this.sbSampleItemType.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbSampleItemType.DoExecSelect = null;
      this.sbSampleItemType.DoGetParent = null;
      this.sbSampleItemType.ExtSource = null;
      this.sbSampleItemType.ExtSourceFields = null;
      this.sbSampleItemType.ExtSourceParent = null;
      this.sbSampleItemType.FormattingEnabled = true;
      this.sbSampleItemType.IntegralHeight = false;
      this.sbSampleItemType.KeyNames = null;
      this.sbSampleItemType.Location = new System.Drawing.Point(115, 3);
      this.sbSampleItemType.Name = "sbSampleItemType";
      this.sbSampleItemType.SelectFormName = null;
      this.sbSampleItemType.Size = new System.Drawing.Size(144, 21);
      this.sbSampleItemType.TabIndex = 0;
      this.sbSampleItemType.ThisSource = null;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label10.Location = new System.Drawing.Point(13, 0);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(96, 25);
      this.label10.TabIndex = 17;
      this.label10.Text = "Пробирка";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // FSampleItemEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(512, 246);
      this.Controls.Add(this.panel);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.DefaultControlName = "sbSampleItemType";
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyNames = "SampleItemId";
      this.Name = "FSampleItemEdit";
      this.Text = "Пробирка";
      this.Controls.SetChildIndex(this.panel, 0);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.TextBox tStoreName;
    private System.Windows.Forms.Label label9;
    private Ctrls.SelectBox sbBlockItem;
    private Ctrls.SelectBox sbSampleItemType;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox tComment;
    private System.Windows.Forms.Label label2;
  }
}