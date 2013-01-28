namespace Yuugosu
{
    partial class MapSelectForm
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
            this.playButton = new System.Windows.Forms.Button();
            this.mapListBox = new System.Windows.Forms.ListBox();
            this.mapDiffBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(264, 284);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(86, 28);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // mapListBox
            // 
            this.mapListBox.FormattingEnabled = true;
            this.mapListBox.Location = new System.Drawing.Point(18, 21);
            this.mapListBox.Name = "mapListBox";
            this.mapListBox.Size = new System.Drawing.Size(332, 121);
            this.mapListBox.TabIndex = 1;
            this.mapListBox.SelectedIndexChanged += new System.EventHandler(this.mapListBox_SelectedIndexChanged);
            // 
            // mapDiffBox
            // 
            this.mapDiffBox.FormattingEnabled = true;
            this.mapDiffBox.Location = new System.Drawing.Point(20, 157);
            this.mapDiffBox.Name = "mapDiffBox";
            this.mapDiffBox.Size = new System.Drawing.Size(330, 121);
            this.mapDiffBox.TabIndex = 2;
            this.mapDiffBox.SelectedIndexChanged += new System.EventHandler(this.mapDiffBox_SelectedIndexChanged);
            // 
            // MapSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 324);
            this.Controls.Add(this.mapDiffBox);
            this.Controls.Add(this.mapListBox);
            this.Controls.Add(this.playButton);
            this.Name = "MapSelectForm";
            this.Text = "MapSelectForm";
            this.Load += new System.EventHandler(this.MapSelectForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.ListBox mapListBox;
        private System.Windows.Forms.ListBox mapDiffBox;
    }
}