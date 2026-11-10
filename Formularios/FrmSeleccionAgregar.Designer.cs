namespace Tarea_ProgramacionII_SistemaGestionProductos.Formularios
{
    partial class FrmSeleccionAgregar
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
            this.lblPregunta = new System.Windows.Forms.Label();
            this.btnCatalogo = new System.Windows.Forms.Button();
            this.btnSeleccionados = new System.Windows.Forms.Button();
            this.btnCanselar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPregunta
            // 
            this.lblPregunta.AutoSize = true;
            this.lblPregunta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPregunta.Location = new System.Drawing.Point(67, 29);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(320, 22);
            this.lblPregunta.TabIndex = 0;
            this.lblPregunta.Text = "Dónde desea agregar el producto?";
            // 
            // btnCatalogo
            // 
            this.btnCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCatalogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCatalogo.Location = new System.Drawing.Point(127, 76);
            this.btnCatalogo.Name = "btnCatalogo";
            this.btnCatalogo.Size = new System.Drawing.Size(196, 49);
            this.btnCatalogo.TabIndex = 1;
            this.btnCatalogo.Text = "Agregar al Catálogo";
            this.btnCatalogo.UseVisualStyleBackColor = false;
            this.btnCatalogo.Click += new System.EventHandler(this.btnCatalogo_Click);
            // 
            // btnSeleccionados
            // 
            this.btnSeleccionados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSeleccionados.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionados.Location = new System.Drawing.Point(127, 143);
            this.btnSeleccionados.Name = "btnSeleccionados";
            this.btnSeleccionados.Size = new System.Drawing.Size(196, 53);
            this.btnSeleccionados.TabIndex = 2;
            this.btnSeleccionados.Text = "Agregar a Productos Seleccionados";
            this.btnSeleccionados.UseVisualStyleBackColor = false;
            this.btnSeleccionados.Click += new System.EventHandler(this.btnSeleccionados_Click);
            // 
            // btnCanselar
            // 
            this.btnCanselar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCanselar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanselar.Location = new System.Drawing.Point(127, 220);
            this.btnCanselar.Name = "btnCanselar";
            this.btnCanselar.Size = new System.Drawing.Size(196, 45);
            this.btnCanselar.TabIndex = 3;
            this.btnCanselar.Text = "Canselar";
            this.btnCanselar.UseVisualStyleBackColor = false;
            this.btnCanselar.Click += new System.EventHandler(this.btnCanselar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(370, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 4;
            // 
            // FrmSeleccionAgregar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 317);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCanselar);
            this.Controls.Add(this.btnSeleccionados);
            this.Controls.Add(this.btnCatalogo);
            this.Controls.Add(this.lblPregunta);
            this.Name = "FrmSeleccionAgregar";
            this.Text = "FrmSeleccionAgregar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.Button btnCatalogo;
        private System.Windows.Forms.Button btnSeleccionados;
        private System.Windows.Forms.Button btnCanselar;
        private System.Windows.Forms.Label label1;
    }
}