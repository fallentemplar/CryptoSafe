namespace ProyectoCifrado3
{
    partial class CriptoZorro
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
            this.boton_cifrar = new System.Windows.Forms.Button();
            this.campo_contrasena = new System.Windows.Forms.TextBox();
            this.etiqueta_contrasena = new System.Windows.Forms.Label();
            this.boton_descifrar = new System.Windows.Forms.Button();
            this.caja_archivos = new System.Windows.Forms.ListBox();
            this.boton_eliminar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualDeUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // boton_cifrar
            // 
            this.boton_cifrar.Location = new System.Drawing.Point(217, 54);
            this.boton_cifrar.Name = "boton_cifrar";
            this.boton_cifrar.Size = new System.Drawing.Size(131, 23);
            this.boton_cifrar.TabIndex = 1;
            this.boton_cifrar.Text = "Cifrar";
            this.boton_cifrar.UseVisualStyleBackColor = true;
            this.boton_cifrar.Click += new System.EventHandler(this.Boton_cifrar_Click);
            // 
            // campo_contrasena
            // 
            this.campo_contrasena.Location = new System.Drawing.Point(217, 28);
            this.campo_contrasena.Name = "campo_contrasena";
            this.campo_contrasena.PasswordChar = '*';
            this.campo_contrasena.Size = new System.Drawing.Size(131, 20);
            this.campo_contrasena.TabIndex = 2;
            // 
            // etiqueta_contrasena
            // 
            this.etiqueta_contrasena.AutoSize = true;
            this.etiqueta_contrasena.Location = new System.Drawing.Point(217, 12);
            this.etiqueta_contrasena.Name = "etiqueta_contrasena";
            this.etiqueta_contrasena.Size = new System.Drawing.Size(61, 13);
            this.etiqueta_contrasena.TabIndex = 3;
            this.etiqueta_contrasena.Text = "Contraseña";
            // 
            // boton_descifrar
            // 
            this.boton_descifrar.Location = new System.Drawing.Point(217, 83);
            this.boton_descifrar.Name = "boton_descifrar";
            this.boton_descifrar.Size = new System.Drawing.Size(131, 23);
            this.boton_descifrar.TabIndex = 4;
            this.boton_descifrar.Text = "Descifrar";
            this.boton_descifrar.UseVisualStyleBackColor = true;
            this.boton_descifrar.Click += new System.EventHandler(this.Boton_descifrar_Click);
            // 
            // caja_archivos
            // 
            this.caja_archivos.AllowDrop = true;
            this.caja_archivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.caja_archivos.FormattingEnabled = true;
            this.caja_archivos.HorizontalScrollbar = true;
            this.caja_archivos.Location = new System.Drawing.Point(0, 41);
            this.caja_archivos.Name = "caja_archivos";
            this.caja_archivos.ScrollAlwaysVisible = true;
            this.caja_archivos.Size = new System.Drawing.Size(200, 238);
            this.caja_archivos.TabIndex = 1;
            // 
            // boton_eliminar
            // 
            this.boton_eliminar.Location = new System.Drawing.Point(0, 285);
            this.boton_eliminar.Name = "boton_eliminar";
            this.boton_eliminar.Size = new System.Drawing.Size(199, 23);
            this.boton_eliminar.TabIndex = 5;
            this.boton_eliminar.Text = "Limpiar lista";
            this.boton_eliminar.UseVisualStyleBackColor = true;
            this.boton_eliminar.Click += new System.EventHandler(this.Boton_eliminar_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.herramientasToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(358, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem1});
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualDeUsuarioToolStripMenuItem,
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // manualDeUsuarioToolStripMenuItem
            // 
            this.manualDeUsuarioToolStripMenuItem.Name = "manualDeUsuarioToolStripMenuItem";
            this.manualDeUsuarioToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.manualDeUsuarioToolStripMenuItem.Text = "Manual de usuario";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem1
            // 
            this.abrirToolStripMenuItem1.Name = "abrirToolStripMenuItem1";
            this.abrirToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.abrirToolStripMenuItem1.Text = "Abrir";
            this.abrirToolStripMenuItem1.Click += new System.EventHandler(this.abrirToolStripMenuItem1_Click);
            // 
            // CriptoZorro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 339);
            this.Controls.Add(this.boton_eliminar);
            this.Controls.Add(this.caja_archivos);
            this.Controls.Add(this.boton_descifrar);
            this.Controls.Add(this.etiqueta_contrasena);
            this.Controls.Add(this.campo_contrasena);
            this.Controls.Add(this.boton_cifrar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CriptoZorro";
            this.Text = "CryptoZorro";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualDeUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem1;
    }
}

