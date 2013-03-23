namespace FRADataStructs.DataStructs.Controls
{
    partial class FormLSArray
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
            this.controlDrawable1 = new FRADataStructs.DataStructs.Controls.ControlDrawable();
            this.SuspendLayout();
            // 
            // controlDrawable1
            // 
            this.controlDrawable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlDrawable1.Location = new System.Drawing.Point(0, 0);
            this.controlDrawable1.Name = "controlDrawable1";
            this.controlDrawable1.Size = new System.Drawing.Size(502, 329);
            this.controlDrawable1.TabIndex = 0;
            this.controlDrawable1.Clicked += new FRADataStructs.DataStructs.Controls.DrawableControlClickHandler(this.controlDrawable1_Clicked);
            // 
            // FormLSArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(502, 329);
            this.Controls.Add(this.controlDrawable1);
            this.Name = "FormLSArray";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormLSArray";
            this.Load += new System.EventHandler(this.FormLSArray_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ControlDrawable controlDrawable1;
    }
}