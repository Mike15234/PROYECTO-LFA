namespace ProyectoLenguajes1137818
{
    partial class Proyecto
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
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.data = new System.Windows.Forms.DataGridView();
            this.Linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dataF = new System.Windows.Forms.DataGridView();
            this.Fi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.La = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataF)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1128, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cargar Archivo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 60);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(598, 370);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1128, 213);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(166, 42);
            this.button3.TabIndex = 4;
            this.button3.Text = "Analizar Archivo";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1128, 281);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(166, 42);
            this.button2.TabIndex = 6;
            this.button2.Text = "Limpiar Cuadro";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // data
            // 
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Linea,
            this.Cadena});
            this.data.Location = new System.Drawing.Point(659, 60);
            this.data.Name = "data";
            this.data.RowTemplate.Height = 28;
            this.data.Size = new System.Drawing.Size(444, 150);
            this.data.TabIndex = 7;
            // 
            // Linea
            // 
            this.Linea.HeaderText = "Linea";
            this.Linea.Name = "Linea";
            // 
            // Cadena
            // 
            this.Cadena.HeaderText = "Cadena";
            this.Cadena.Name = "Cadena";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 29);
            this.label1.TabIndex = 8;
            // 
            // dataF
            // 
            this.dataF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fi,
            this.La,
            this.Fo});
            this.dataF.Location = new System.Drawing.Point(659, 280);
            this.dataF.Name = "dataF";
            this.dataF.RowTemplate.Height = 28;
            this.dataF.Size = new System.Drawing.Size(444, 150);
            this.dataF.TabIndex = 9;
            // 
            // Fi
            // 
            this.Fi.HeaderText = "First";
            this.Fi.Name = "Fi";
            // 
            // La
            // 
            this.La.HeaderText = "Last";
            this.La.Name = "La";
            // 
            // Fo
            // 
            this.Fo.HeaderText = "Follow";
            this.Fo.Name = "Fo";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1128, 341);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(166, 35);
            this.button4.TabIndex = 10;
            this.button4.Text = "Mostrar DFA";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1128, 395);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(166, 35);
            this.button5.TabIndex = 11;
            this.button5.Text = "Cerrar Todo";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Proyecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1306, 446);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dataF);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.data);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "Proyecto";
            this.Text = "Proyecto Lenguajes";
            this.Load += new System.EventHandler(this.Proyecto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cadena;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fi;
        private System.Windows.Forms.DataGridViewTextBoxColumn La;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fo;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

