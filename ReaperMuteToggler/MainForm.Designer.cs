namespace ReaperMuteToggler
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.shortcutTextbox = new System.Windows.Forms.TextBox();
            this.SetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shortcutTextbox
            // 
            this.shortcutTextbox.Location = new System.Drawing.Point(12, 12);
            this.shortcutTextbox.Name = "shortcutTextbox";
            this.shortcutTextbox.ReadOnly = true;
            this.shortcutTextbox.Size = new System.Drawing.Size(232, 20);
            this.shortcutTextbox.TabIndex = 0;
            this.shortcutTextbox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ShortcutTextbox_PreviewKeyDown);
            // 
            // SetButton
            // 
            this.SetButton.Location = new System.Drawing.Point(12, 38);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(232, 23);
            this.SetButton.TabIndex = 1;
            this.SetButton.Text = "Set Shortcut";
            this.SetButton.UseVisualStyleBackColor = false;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 72);
            this.Controls.Add(this.SetButton);
            this.Controls.Add(this.shortcutTextbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Reaper Toggle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox shortcutTextbox;
        private System.Windows.Forms.Button SetButton;
    }
}