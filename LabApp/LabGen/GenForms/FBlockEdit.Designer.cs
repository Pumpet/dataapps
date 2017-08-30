namespace GenForms
{
  partial class FBlockEdit
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBlockEdit));
      this.panel = new System.Windows.Forms.TableLayoutPanel();
      this.tBlockCode = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.sbStore = new Ctrls.SelectBox(this.components);
      this.cbIsSetItems = new System.Windows.Forms.CheckBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.tY = new Ctrls.NumberBox(this.components);
      this.tX = new Ctrls.NumberBox(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this.panel.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tX)).BeginInit();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 5;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 294F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
      this.panel.Controls.Add(this.tBlockCode, 2, 0);
      this.panel.Controls.Add(this.label6, 1, 0);
      this.panel.Controls.Add(this.label9, 1, 1);
      this.panel.Controls.Add(this.sbStore, 2, 1);
      this.panel.Controls.Add(this.cbIsSetItems, 2, 2);
      this.panel.Controls.Add(this.panel1, 2, 3);
      this.panel.Controls.Add(this.label1, 1, 3);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 25);
      this.panel.Margin = new System.Windows.Forms.Padding(0);
      this.panel.Name = "panel";
      this.panel.RowCount = 6;
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.Size = new System.Drawing.Size(570, 147);
      this.panel.TabIndex = 7;
      // 
      // tBlockCode
      // 
      this.tBlockCode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tBlockCode.Location = new System.Drawing.Point(103, 3);
      this.tBlockCode.Name = "tBlockCode";
      this.tBlockCode.Size = new System.Drawing.Size(88, 22);
      this.tBlockCode.TabIndex = 3;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(13, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(84, 25);
      this.label6.TabIndex = 7;
      this.label6.Text = "Номер";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label9.Location = new System.Drawing.Point(13, 25);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(84, 25);
      this.label9.TabIndex = 14;
      this.label9.Text = "Находится в";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // sbStore
      // 
      this.panel.SetColumnSpan(this.sbStore, 2);
      this.sbStore.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbStore.DoExecSelect = null;
      this.sbStore.DoGetParent = null;
      this.sbStore.ExtSource = null;
      this.sbStore.ExtSourceFields = null;
      this.sbStore.ExtSourceParent = null;
      this.sbStore.FormattingEnabled = true;
      this.sbStore.IntegralHeight = false;
      this.sbStore.KeyNames = null;
      this.sbStore.Location = new System.Drawing.Point(103, 28);
      this.sbStore.Name = "sbStore";
      this.sbStore.SelectFormName = "FPopuls";
      this.sbStore.Size = new System.Drawing.Size(382, 21);
      this.sbStore.TabIndex = 2;
      this.sbStore.ThisSource = null;
      // 
      // cbIsSetItems
      // 
      this.cbIsSetItems.AutoSize = true;
      this.panel.SetColumnSpan(this.cbIsSetItems, 2);
      this.cbIsSetItems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cbIsSetItems.Location = new System.Drawing.Point(103, 53);
      this.cbIsSetItems.Name = "cbIsSetItems";
      this.cbIsSetItems.Size = new System.Drawing.Size(382, 18);
      this.cbIsSetItems.TabIndex = 15;
      this.cbIsSetItems.Text = "Сформировать ячейки";
      this.cbIsSetItems.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.tY);
      this.panel1.Controls.Add(this.tX);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(103, 77);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(88, 24);
      this.panel1.TabIndex = 18;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(35, 3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(20, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "на";
      // 
      // tY
      // 
      this.tY.Location = new System.Drawing.Point(58, 0);
      this.tY.Name = "tY";
      this.tY.Size = new System.Drawing.Size(32, 22);
      this.tY.TabIndex = 1;
      this.tY.Text = "0";
      this.tY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // tX
      // 
      this.tX.Location = new System.Drawing.Point(0, 0);
      this.tX.Name = "tX";
      this.tX.Size = new System.Drawing.Size(32, 22);
      this.tX.TabIndex = 0;
      this.tX.Text = "0";
      this.tX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(13, 74);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(82, 25);
      this.label1.TabIndex = 16;
      this.label1.Text = "Размерность";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // FBlockEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(570, 194);
      this.Controls.Add(this.panel);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.DefaultControlName = "tBlockCode";
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyNames = "BlockId";
      this.Name = "FBlockEdit";
      this.Text = "Штатив";
      this.OnControlChanged += new System.Action<System.Windows.Forms.Control, bool>(this.FBlockEdit_OnControlChanged);
      this.Controls.SetChildIndex(this.panel, 0);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tX)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.TextBox tBlockCode;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label9;
    private Ctrls.SelectBox sbStore;
    private System.Windows.Forms.CheckBox cbIsSetItems;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;
    private Ctrls.NumberBox tY;
    private Ctrls.NumberBox tX;
  }
}