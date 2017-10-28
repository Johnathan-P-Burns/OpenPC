namespace OpenPC_Database_Management
{
    partial class DatabaseManagement
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
            this.SQLTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // SQLTreeView
            // 
            this.SQLTreeView.Location = new System.Drawing.Point(12, 12);
            this.SQLTreeView.Name = "SQLTreeView";
            this.SQLTreeView.Size = new System.Drawing.Size(449, 472);
            this.SQLTreeView.TabIndex = 0;
            this.SQLTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SQLTreeView_AfterSelect);
            this.SQLTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SQLTreeView_NodeMouseClick);
            // 
            // DatabaseManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 496);
            this.Controls.Add(this.SQLTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DatabaseManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DatabaseManagement";
            this.Load += new System.EventHandler(this.DatabaseManagement_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView SQLTreeView;
    }
}

