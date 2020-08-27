namespace ProyectoCifrado3
{
    partial class Encrypto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Encrypto));
            this.boton_cifrar = new System.Windows.Forms.Button();
            this.campo_contrasena = new System.Windows.Forms.TextBox();
            this.etiqueta_contrasena = new System.Windows.Forms.Label();
            this.boton_descifrar = new System.Windows.Forms.Button();
            this.caja_archivos = new System.Windows.Forms.ListBox();
            this.boton_eliminar = new System.Windows.Forms.Button();
            this.etiqueta_nombre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // boton_cifrar
            // 
            this.boton_cifrar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_cifrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.boton_cifrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boton_cifrar.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.boton_cifrar.ForeColor = System.Drawing.Color.White;
            this.boton_cifrar.Location = new System.Drawing.Point(12, 366);
            this.boton_cifrar.Name = "boton_cifrar";
            this.boton_cifrar.Size = new System.Drawing.Size(140, 25);
            this.boton_cifrar.TabIndex = 1;
            this.boton_cifrar.Text = "Cifrar";
            this.boton_cifrar.UseVisualStyleBackColor = true;
            this.boton_cifrar.Click += new System.EventHandler(this.Boton_cifrar_Click);
            // 
            // campo_contrasena
            // 
            this.campo_contrasena.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.campo_contrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.campo_contrasena.Location = new System.Drawing.Point(85, 340);
            this.campo_contrasena.Name = "campo_contrasena";
            this.campo_contrasena.PasswordChar = '*';
            this.campo_contrasena.Size = new System.Drawing.Size(213, 20);
            this.campo_contrasena.TabIndex = 2;
            // 
            // etiqueta_contrasena
            // 
            this.etiqueta_contrasena.AutoSize = true;
            this.etiqueta_contrasena.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.etiqueta_contrasena.ForeColor = System.Drawing.Color.White;
            this.etiqueta_contrasena.Location = new System.Drawing.Point(12, 342);
            this.etiqueta_contrasena.Name = "etiqueta_contrasena";
            this.etiqueta_contrasena.Size = new System.Drawing.Size(67, 13);
            this.etiqueta_contrasena.TabIndex = 3;
            this.etiqueta_contrasena.Text = "Contraseña";
            // 
            // boton_descifrar
            // 
            this.boton_descifrar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_descifrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boton_descifrar.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.boton_descifrar.ForeColor = System.Drawing.Color.White;
            this.boton_descifrar.Location = new System.Drawing.Point(158, 366);
            this.boton_descifrar.Name = "boton_descifrar";
            this.boton_descifrar.Size = new System.Drawing.Size(140, 25);
            this.boton_descifrar.TabIndex = 4;
            this.boton_descifrar.Text = "Descifrar";
            this.boton_descifrar.UseVisualStyleBackColor = false;
            this.boton_descifrar.Click += new System.EventHandler(this.Boton_descifrar_Click);
            // 
            // caja_archivos
            // 
            this.caja_archivos.AllowDrop = true;
            this.caja_archivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.caja_archivos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.caja_archivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caja_archivos.ForeColor = System.Drawing.Color.White;
            this.caja_archivos.FormattingEnabled = true;
            this.caja_archivos.HorizontalScrollbar = true;
            this.caja_archivos.ItemHeight = 24;
            this.caja_archivos.Location = new System.Drawing.Point(12, 37);
            this.caja_archivos.Name = "caja_archivos";
            this.caja_archivos.Size = new System.Drawing.Size(286, 266);
            this.caja_archivos.TabIndex = 1;
            // 
            // boton_eliminar
            // 
            this.boton_eliminar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_eliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boton_eliminar.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boton_eliminar.ForeColor = System.Drawing.Color.White;
            this.boton_eliminar.Location = new System.Drawing.Point(12, 309);
            this.boton_eliminar.Name = "boton_eliminar";
            this.boton_eliminar.Size = new System.Drawing.Size(286, 25);
            this.boton_eliminar.TabIndex = 5;
            this.boton_eliminar.Text = "Limpiar lista";
            this.boton_eliminar.UseVisualStyleBackColor = false;
            this.boton_eliminar.Click += new System.EventHandler(this.Boton_eliminar_Click);
            // 
            // etiqueta_nombre
            // 
            this.etiqueta_nombre.AutoSize = true;
            this.etiqueta_nombre.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.etiqueta_nombre.ForeColor = System.Drawing.Color.White;
            this.etiqueta_nombre.Location = new System.Drawing.Point(8, 9);
            this.etiqueta_nombre.Name = "etiqueta_nombre";
            this.etiqueta_nombre.Size = new System.Drawing.Size(81, 21);
            this.etiqueta_nombre.TabIndex = 6;
            this.etiqueta_nombre.Text = "Encrypto";
            // 
            // Encrypto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 404);
            this.Controls.Add(this.etiqueta_nombre);
            this.Controls.Add(this.boton_eliminar);
            this.Controls.Add(this.caja_archivos);
            this.Controls.Add(this.boton_descifrar);
            this.Controls.Add(this.etiqueta_contrasena);
            this.Controls.Add(this.campo_contrasena);
            this.Controls.Add(this.boton_cifrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Encrypto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Encrypto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button boton_cifrar;
        private System.Windows.Forms.TextBox campo_contrasena;
        private System.Windows.Forms.Label etiqueta_contrasena;
        private System.Windows.Forms.Button boton_descifrar;
        private System.Windows.Forms.ListBox caja_archivos;
        private System.Windows.Forms.Button boton_eliminar;
        private System.Windows.Forms.Label etiqueta_nombre;
    }
}

