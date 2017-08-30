namespace GenForms
{
  partial class FAnketRelEdit
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
      this.panel = new System.Windows.Forms.TableLayoutPanel();
      this.label2 = new System.Windows.Forms.Label();
      this.tOrigin = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.sbPopul = new Ctrls.SelectBox(this.components);
      this.sbRelType = new Ctrls.SelectBox(this.components);
      this.label10 = new System.Windows.Forms.Label();
      this.sbBirthPlace = new Ctrls.SelectBox(this.components);
      this.sbLingua = new Ctrls.SelectBox(this.components);
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.ColumnCount = 5;
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 271F));
      this.panel.Controls.Add(this.label2, 1, 4);
      this.panel.Controls.Add(this.tOrigin, 2, 2);
      this.panel.Controls.Add(this.label6, 1, 2);
      this.panel.Controls.Add(this.label7, 1, 3);
      this.panel.Controls.Add(this.label9, 1, 1);
      this.panel.Controls.Add(this.sbPopul, 2, 1);
      this.panel.Controls.Add(this.sbRelType, 2, 0);
      this.panel.Controls.Add(this.label10, 1, 0);
      this.panel.Controls.Add(this.sbBirthPlace, 2, 3);
      this.panel.Controls.Add(this.sbLingua, 2, 4);
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
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.panel.Size = new System.Drawing.Size(387, 149);
      this.panel.TabIndex = 6;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(13, 100);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(105, 25);
      this.label2.TabIndex = 1;
      this.label2.Text = "Язык";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // tOrigin
      // 
      this.panel.SetColumnSpan(this.tOrigin, 2);
      this.tOrigin.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tOrigin.Location = new System.Drawing.Point(124, 53);
      this.tOrigin.Name = "tOrigin";
      this.tOrigin.Size = new System.Drawing.Size(234, 22);
      this.tOrigin.TabIndex = 3;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(13, 50);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(105, 25);
      this.label6.TabIndex = 7;
      this.label6.Text = "Происхожение";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label7.Location = new System.Drawing.Point(13, 75);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(105, 25);
      this.label7.TabIndex = 8;
      this.label7.Text = "Место рождения";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label9.Location = new System.Drawing.Point(13, 25);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(105, 25);
      this.label9.TabIndex = 14;
      this.label9.Text = "Популяция";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // sbPopul
      // 
      this.sbPopul.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbPopul.DoExecSelect = null;
      this.sbPopul.DoGetParent = null;
      this.sbPopul.ExtSource = null;
      this.sbPopul.ExtSourceFields = null;
      this.sbPopul.ExtSourceParent = null;
      this.sbPopul.FormattingEnabled = true;
      this.sbPopul.IntegralHeight = false;
      this.sbPopul.KeyNames = null;
      this.sbPopul.Location = new System.Drawing.Point(124, 28);
      this.sbPopul.Name = "sbPopul";
      this.sbPopul.SelectFormName = "FPopuls";
      this.sbPopul.Size = new System.Drawing.Size(114, 21);
      this.sbPopul.TabIndex = 2;
      this.sbPopul.ThisSource = null;
      // 
      // sbRelType
      // 
      this.sbRelType.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbRelType.DoExecSelect = null;
      this.sbRelType.DoGetParent = null;
      this.sbRelType.ExtSource = null;
      this.sbRelType.ExtSourceFields = null;
      this.sbRelType.ExtSourceParent = null;
      this.sbRelType.FormattingEnabled = true;
      this.sbRelType.IntegralHeight = false;
      this.sbRelType.KeyNames = null;
      this.sbRelType.Location = new System.Drawing.Point(124, 3);
      this.sbRelType.Name = "sbRelType";
      this.sbRelType.SelectFormName = "FExpeds";
      this.sbRelType.Size = new System.Drawing.Size(114, 21);
      this.sbRelType.TabIndex = 1;
      this.sbRelType.ThisSource = null;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label10.Location = new System.Drawing.Point(13, 0);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(105, 25);
      this.label10.TabIndex = 17;
      this.label10.Text = "Родство";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // sbBirthPlace
      // 
      this.panel.SetColumnSpan(this.sbBirthPlace, 2);
      this.sbBirthPlace.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbBirthPlace.DoExecSelect = null;
      this.sbBirthPlace.DoGetParent = null;
      this.sbBirthPlace.ExtSource = null;
      this.sbBirthPlace.ExtSourceFields = null;
      this.sbBirthPlace.ExtSourceParent = null;
      this.sbBirthPlace.FormattingEnabled = true;
      this.sbBirthPlace.IntegralHeight = false;
      this.sbBirthPlace.KeyNames = null;
      this.sbBirthPlace.Location = new System.Drawing.Point(124, 78);
      this.sbBirthPlace.Name = "sbBirthPlace";
      this.sbBirthPlace.SelectFormName = null;
      this.sbBirthPlace.Size = new System.Drawing.Size(234, 21);
      this.sbBirthPlace.TabIndex = 18;
      this.sbBirthPlace.ThisSource = null;
      // 
      // sbLingua
      // 
      this.sbLingua.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sbLingua.DoExecSelect = null;
      this.sbLingua.DoGetParent = null;
      this.sbLingua.ExtSource = null;
      this.sbLingua.ExtSourceFields = null;
      this.sbLingua.ExtSourceParent = null;
      this.sbLingua.FormattingEnabled = true;
      this.sbLingua.IntegralHeight = false;
      this.sbLingua.KeyNames = null;
      this.sbLingua.Location = new System.Drawing.Point(124, 103);
      this.sbLingua.Name = "sbLingua";
      this.sbLingua.SelectFormName = null;
      this.sbLingua.Size = new System.Drawing.Size(114, 21);
      this.sbLingua.TabIndex = 19;
      this.sbLingua.ThisSource = null;
      // 
      // FAnketRelEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(387, 196);
      this.Controls.Add(this.panel);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.KeyNames = "AnketRelId";
      this.Name = "FAnketRelEdit";
      this.ShowIcon = false;
      this.Text = "Родственник";
      this.Controls.SetChildIndex(this.panel, 0);
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel panel;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tOrigin;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label9;
    private Ctrls.SelectBox sbPopul;
    private Ctrls.SelectBox sbRelType;
    private System.Windows.Forms.Label label10;
    private Ctrls.SelectBox sbBirthPlace;
    private Ctrls.SelectBox sbLingua;
  }
}