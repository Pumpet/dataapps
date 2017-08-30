namespace Forms
{
    partial class FormBase
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
      this.status = new System.Windows.Forms.StatusStrip();
      this.lbInfo = new System.Windows.Forms.ToolStripStatusLabel();
      this.tools = new System.Windows.Forms.ToolStrip();
      this.menus = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.status.SuspendLayout();
      this.SuspendLayout();
      // 
      // status
      // 
      this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbInfo});
      this.status.Location = new System.Drawing.Point(0, 240);
      this.status.Name = "status";
      this.status.Size = new System.Drawing.Size(512, 22);
      this.status.TabIndex = 0;
      this.status.Text = "statusStrip1";
      // 
      // lbInfo
      // 
      this.lbInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.lbInfo.Name = "lbInfo";
      this.lbInfo.Size = new System.Drawing.Size(497, 17);
      this.lbInfo.Spring = true;
      this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tools
      // 
      this.tools.Location = new System.Drawing.Point(0, 0);
      this.tools.Name = "tools";
      this.tools.Size = new System.Drawing.Size(512, 25);
      this.tools.TabIndex = 1;
      this.tools.Text = "toolStrip1";
      // 
      // menus
      // 
      this.menus.Name = "menus";
      this.menus.Size = new System.Drawing.Size(61, 4);
      // 
      // FormBase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(512, 262);
      this.Controls.Add(this.tools);
      this.Controls.Add(this.status);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.KeyPreview = true;
      this.Name = "FormBase";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Shown += new System.EventHandler(this.FormBase_Shown);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBase_KeyDown);
      this.status.ResumeLayout(false);
      this.status.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.StatusStrip status;
        public System.Windows.Forms.ToolStripStatusLabel lbInfo;
        public System.Windows.Forms.ToolStrip tools;
        public System.Windows.Forms.ContextMenuStrip menus;
    }
}