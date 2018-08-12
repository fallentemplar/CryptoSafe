namespace ProyectoCifrado2
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.campo_cifrar = new System.Windows.Forms.TextBox();
            this.boton_cifrar = new System.Windows.Forms.Button();
            this.boton_descifrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.campo_descifrar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.campo_contrasena = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.test_texto = new System.Windows.Forms.Label();
            this.test_pass = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Texto sin cifrar";
            // 
            // campo_cifrar
            // 
            this.campo_cifrar.Location = new System.Drawing.Point(6, 32);
            this.campo_cifrar.Multiline = true;
            this.campo_cifrar.Name = "campo_cifrar";
            this.campo_cifrar.Size = new System.Drawing.Size(158, 98);
            this.campo_cifrar.TabIndex = 1;
            // 
            // boton_cifrar
            // 
            this.boton_cifrar.Location = new System.Drawing.Point(6, 136);
            this.boton_cifrar.Name = "boton_cifrar";
            this.boton_cifrar.Size = new System.Drawing.Size(158, 23);
            this.boton_cifrar.TabIndex = 2;
            this.boton_cifrar.Text = "Cifrar";
            this.boton_cifrar.UseVisualStyleBackColor = true;
            this.boton_cifrar.Click += new System.EventHandler(this.boton_cifrar_Click);
            // 
            // boton_descifrar
            // 
            this.boton_descifrar.Location = new System.Drawing.Point(6, 136);
            this.boton_descifrar.Name = "boton_descifrar";
            this.boton_descifrar.Size = new System.Drawing.Size(158, 23);
            this.boton_descifrar.TabIndex = 3;
            this.boton_descifrar.Text = "Descifrar";
            this.boton_descifrar.UseVisualStyleBackColor = true;
            this.boton_descifrar.Click += new System.EventHandler(this.boton_descifrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.campo_cifrar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.boton_cifrar);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 175);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cifrado";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.campo_descifrar);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.boton_descifrar);
            this.groupBox2.Location = new System.Drawing.Point(198, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 175);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Descifrado";
            // 
            // campo_descifrar
            // 
            this.campo_descifrar.Location = new System.Drawing.Point(0, 32);
            this.campo_descifrar.Multiline = true;
            this.campo_descifrar.Name = "campo_descifrar";
            this.campo_descifrar.Size = new System.Drawing.Size(164, 98);
            this.campo_descifrar.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Texto cifrado";
            // 
            // campo_contrasena
            // 
            this.campo_contrasena.Location = new System.Drawing.Point(198, 194);
            this.campo_contrasena.Name = "campo_contrasena";
            this.campo_contrasena.PasswordChar = '*';
            this.campo_contrasena.Size = new System.Drawing.Size(100, 20);
            this.campo_contrasena.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Contraseña";
            // 
            // test_texto
            // 
            this.test_texto.AutoSize = true;
            this.test_texto.Location = new System.Drawing.Point(19, 220);
            this.test_texto.Name = "test_texto";
            this.test_texto.Size = new System.Drawing.Size(35, 13);
            this.test_texto.TabIndex = 8;
            this.test_texto.Text = "label4";
            // 
            // test_pass
            // 
            this.test_pass.AutoSize = true;
            this.test_pass.Location = new System.Drawing.Point(19, 246);
            this.test_pass.Name = "test_pass";
            this.test_pass.Size = new System.Drawing.Size(35, 13);
            this.test_pass.TabIndex = 9;
            this.test_pass.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 328);
            this.Controls.Add(this.test_pass);
            this.Controls.Add(this.test_texto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.campo_contrasena);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox campo_cifrar;
        private System.Windows.Forms.Button boton_cifrar;
        private System.Windows.Forms.Button boton_descifrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox campo_descifrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox campo_contrasena;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label test_texto;
        private System.Windows.Forms.Label test_pass;
    }
}

