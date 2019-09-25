namespace ProyectoCifrado3
{
    partial class Procesando
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
            this.barraProgreso = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // barraProgreso
            // 
            this.barraProgreso.Location = new System.Drawing.Point(12, 12);
            this.barraProgreso.Name = "barraProgreso";
            this.barraProgreso.Size = new System.Drawing.Size(349, 23);
            this.barraProgreso.Step = 1;
            this.barraProgreso.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.barraProgreso.TabIndex = 0;
            // 
            // Procesando
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 48);
            this.Controls.Add(this.barraProgreso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Procesando";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Procesando...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar barraProgreso;
    }
}