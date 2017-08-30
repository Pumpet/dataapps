namespace GenForms
{
  partial class FMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
      this.tools = new System.Windows.Forms.ToolStrip();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.bExpeds = new System.Windows.Forms.ToolStripMenuItem();
      this.bPopuls = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
      this.bStores = new System.Windows.Forms.ToolStripMenuItem();
      this.bBlocks = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
      this.bLists = new System.Windows.Forms.ToolStripMenuItem();
      this.bPlaces = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
      this.bRepSamples = new System.Windows.Forms.ToolStripMenuItem();
      this.tools.SuspendLayout();
      this.SuspendLayout();
      // 
      // tools
      // 
      this.tools.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tools.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton4,
            this.toolStripButton3,
            this.toolStripDropDownButton3,
            this.toolStripDropDownButton2});
      this.tools.Location = new System.Drawing.Point(0, 0);
      this.tools.Name = "tools";
      this.tools.Size = new System.Drawing.Size(826, 38);
      this.tools.TabIndex = 1;
      this.tools.TabStop = true;
      this.tools.Text = "toolStrip1";
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bExpeds,
            this.bPopuls});
      this.toolStripDropDownButton1.Image = global::GenForms.Properties.Resources.research;
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(131, 35);
      this.toolStripDropDownButton1.Text = "Исследования";
      // 
      // bExpeds
      // 
      this.bExpeds.Image = global::GenForms.Properties.Resources.expedition;
      this.bExpeds.Name = "bExpeds";
      this.bExpeds.Size = new System.Drawing.Size(140, 22);
      this.bExpeds.Text = "Экспедиции";
      this.bExpeds.Click += new System.EventHandler(this.bExpeds_Click);
      // 
      // bPopuls
      // 
      this.bPopuls.Image = global::GenForms.Properties.Resources.etnos;
      this.bPopuls.Name = "bPopuls";
      this.bPopuls.Size = new System.Drawing.Size(140, 22);
      this.bPopuls.Text = "Популяции";
      this.bPopuls.Click += new System.EventHandler(this.bPopuls_Click);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = global::GenForms.Properties.Resources.ankets;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(84, 35);
      this.toolStripButton1.Text = "Анкеты";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.Image = global::GenForms.Properties.Resources.lab;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(93, 35);
      this.toolStripButton2.Text = "Образцы";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.Image = global::GenForms.Properties.Resources.process;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(105, 35);
      this.toolStripButton4.Text = "Результаты";
      this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bStores,
            this.bBlocks});
      this.toolStripButton3.Image = global::GenForms.Properties.Resources.fridge2;
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(105, 35);
      this.toolStripButton3.Text = "Хранение";
      // 
      // bStores
      // 
      this.bStores.Image = global::GenForms.Properties.Resources.fridge1;
      this.bStores.Name = "bStores";
      this.bStores.Size = new System.Drawing.Size(162, 22);
      this.bStores.Text = "Места хранения";
      this.bStores.Click += new System.EventHandler(this.bStores_Click);
      // 
      // bBlocks
      // 
      this.bBlocks.Image = ((System.Drawing.Image)(resources.GetObject("bBlocks.Image")));
      this.bBlocks.Name = "bBlocks";
      this.bBlocks.Size = new System.Drawing.Size(162, 22);
      this.bBlocks.Text = "Штативы";
      this.bBlocks.Click += new System.EventHandler(this.bBlocks_Click);
      // 
      // toolStripDropDownButton3
      // 
      this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bLists,
            this.bPlaces});
      this.toolStripDropDownButton3.Image = global::GenForms.Properties.Resources.other;
      this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
      this.toolStripDropDownButton3.Size = new System.Drawing.Size(94, 35);
      this.toolStripDropDownButton3.Text = "Прочее";
      // 
      // bLists
      // 
      this.bLists.Name = "bLists";
      this.bLists.Size = new System.Drawing.Size(186, 22);
      this.bLists.Text = "Списки";
      this.bLists.Click += new System.EventHandler(this.bLists_Click);
      // 
      // bPlaces
      // 
      this.bPlaces.Name = "bPlaces";
      this.bPlaces.Size = new System.Drawing.Size(186, 22);
      this.bPlaces.Text = "Населенные пункты";
      this.bPlaces.Click += new System.EventHandler(this.bPlaces_Click);
      // 
      // toolStripDropDownButton2
      // 
      this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bRepSamples});
      this.toolStripDropDownButton2.Image = global::GenForms.Properties.Resources.xlrep;
      this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
      this.toolStripDropDownButton2.Size = new System.Drawing.Size(93, 35);
      this.toolStripDropDownButton2.Text = "Отчеты";
      // 
      // bRepSamples
      // 
      this.bRepSamples.Name = "bRepSamples";
      this.bRepSamples.Size = new System.Drawing.Size(152, 22);
      this.bRepSamples.Text = "Образцы";
      this.bRepSamples.Click += new System.EventHandler(this.bRepSamples_Click);
      // 
      // FMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(826, 38);
      this.Controls.Add(this.tools);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximumSize = new System.Drawing.Size(10000, 76);
      this.MinimumSize = new System.Drawing.Size(770, 76);
      this.Name = "FMain";
      this.Text = "Лаборатория популяционной генетики";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Shown += new System.EventHandler(this.FMain_Shown);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FMain_KeyDown);
      this.tools.ResumeLayout(false);
      this.tools.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip tools;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem bExpeds;
    private System.Windows.Forms.ToolStripMenuItem bPopuls;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripDropDownButton toolStripButton3;
    private System.Windows.Forms.ToolStripMenuItem bStores;
    private System.Windows.Forms.ToolStripMenuItem bBlocks;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
    private System.Windows.Forms.ToolStripMenuItem bLists;
    private System.Windows.Forms.ToolStripMenuItem bPlaces;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
    private System.Windows.Forms.ToolStripMenuItem bRepSamples;
  }
}