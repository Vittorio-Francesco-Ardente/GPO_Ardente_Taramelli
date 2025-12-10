namespace Applicativo_Ardente
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Tavolo_Picture = new PictureBox();
            Titolo_Menu = new Label();
            label1 = new Label();
            Lista_Pizze = new ListBox();
            Ordini_Lista = new ListBox();
            Cancella_Button = new Button();
            Servi_Button = new Button();
            Aggiungi_Button = new Button();
            Panel_Ordini = new Panel();
            Button_Ordini = new Button();
            Panel_Sparecchia = new Panel();
            Sparecchia_Button = new Button();
            Gnam = new Label();
            Button_Scontrino = new Button();
            ((System.ComponentModel.ISupportInitialize)Tavolo_Picture).BeginInit();
            Panel_Ordini.SuspendLayout();
            Panel_Sparecchia.SuspendLayout();
            SuspendLayout();
            // 
            // Tavolo_Picture
            // 
            Tavolo_Picture.Image = (Image)resources.GetObject("Tavolo_Picture.Image");
            Tavolo_Picture.Location = new Point(21, 12);
            Tavolo_Picture.Name = "Tavolo_Picture";
            Tavolo_Picture.Size = new Size(1047, 621);
            Tavolo_Picture.TabIndex = 0;
            Tavolo_Picture.TabStop = false;
            // 
            // Titolo_Menu
            // 
            Titolo_Menu.AutoSize = true;
            Titolo_Menu.BackColor = Color.White;
            Titolo_Menu.BorderStyle = BorderStyle.FixedSingle;
            Titolo_Menu.Font = new Font("Segoe UI", 19.8F, FontStyle.Bold, GraphicsUnit.Point);
            Titolo_Menu.ForeColor = Color.Blue;
            Titolo_Menu.Location = new Point(3, 14);
            Titolo_Menu.Name = "Titolo_Menu";
            Titolo_Menu.Size = new Size(229, 47);
            Titolo_Menu.TabIndex = 1;
            Titolo_Menu.Text = "MENU' PIZZE";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Segoe UI", 19.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(3, 376);
            label1.Name = "label1";
            label1.Size = new Size(147, 47);
            label1.TabIndex = 2;
            label1.Text = "ORDINE";
            // 
            // Lista_Pizze
            // 
            Lista_Pizze.FormattingEnabled = true;
            Lista_Pizze.ItemHeight = 20;
            Lista_Pizze.Items.AddRange(new object[] { "Americana", "Caprese", "Capricciosa", "Margherita", "Marinara", "Napoletana", "Prosciutto e funghi", "Quattro formaggi", "Salamino", "Salmone", "Tonno e cipolle", "Vegetariana", "Wurstel e cipolle" });
            Lista_Pizze.Location = new Point(3, 74);
            Lista_Pizze.Name = "Lista_Pizze";
            Lista_Pizze.Size = new Size(236, 264);
            Lista_Pizze.TabIndex = 3;
            // 
            // Ordini_Lista
            // 
            Ordini_Lista.FormattingEnabled = true;
            Ordini_Lista.ItemHeight = 20;
            Ordini_Lista.Location = new Point(3, 426);
            Ordini_Lista.Name = "Ordini_Lista";
            Ordini_Lista.Size = new Size(236, 124);
            Ordini_Lista.TabIndex = 4;
            // 
            // Cancella_Button
            // 
            Cancella_Button.Location = new Point(3, 556);
            Cancella_Button.Name = "Cancella_Button";
            Cancella_Button.Size = new Size(236, 29);
            Cancella_Button.TabIndex = 5;
            Cancella_Button.Text = "Cancella Ordine!";
            Cancella_Button.UseVisualStyleBackColor = true;
            Cancella_Button.Click += Cancella_Button_Click;
            // 
            // Servi_Button
            // 
            Servi_Button.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Servi_Button.Location = new Point(3, 591);
            Servi_Button.Name = "Servi_Button";
            Servi_Button.Size = new Size(236, 29);
            Servi_Button.TabIndex = 6;
            Servi_Button.Text = "Servi il tavolo!";
            Servi_Button.UseVisualStyleBackColor = true;
            Servi_Button.Click += Servi_Button_Click;
            // 
            // Aggiungi_Button
            // 
            Aggiungi_Button.Location = new Point(3, 344);
            Aggiungi_Button.Name = "Aggiungi_Button";
            Aggiungi_Button.Size = new Size(236, 29);
            Aggiungi_Button.TabIndex = 7;
            Aggiungi_Button.Text = "Aggiungi";
            Aggiungi_Button.UseVisualStyleBackColor = true;
            Aggiungi_Button.Click += Aggiungi_Button_Click;
            // 
            // Panel_Ordini
            // 
            Panel_Ordini.BackColor = Color.White;
            Panel_Ordini.BorderStyle = BorderStyle.FixedSingle;
            Panel_Ordini.Controls.Add(Button_Scontrino);
            Panel_Ordini.Controls.Add(Button_Ordini);
            Panel_Ordini.Controls.Add(Servi_Button);
            Panel_Ordini.Controls.Add(Titolo_Menu);
            Panel_Ordini.Controls.Add(Lista_Pizze);
            Panel_Ordini.Controls.Add(Aggiungi_Button);
            Panel_Ordini.Controls.Add(Cancella_Button);
            Panel_Ordini.Controls.Add(Ordini_Lista);
            Panel_Ordini.Controls.Add(label1);
            Panel_Ordini.Location = new Point(1335, 12);
            Panel_Ordini.Name = "Panel_Ordini";
            Panel_Ordini.Size = new Size(246, 693);
            Panel_Ordini.TabIndex = 8;
            // 
            // Button_Ordini
            // 
            Button_Ordini.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Button_Ordini.Location = new Point(3, 626);
            Button_Ordini.Name = "Button_Ordini";
            Button_Ordini.Size = new Size(236, 29);
            Button_Ordini.TabIndex = 8;
            Button_Ordini.Text = "Salva ordini!";
            Button_Ordini.UseVisualStyleBackColor = true;
            Button_Ordini.Click += SalvaStorico_Click;
            // 
            // Panel_Sparecchia
            // 
            Panel_Sparecchia.BackColor = Color.White;
            Panel_Sparecchia.Controls.Add(Sparecchia_Button);
            Panel_Sparecchia.Controls.Add(Gnam);
            Panel_Sparecchia.Location = new Point(1335, 711);
            Panel_Sparecchia.Name = "Panel_Sparecchia";
            Panel_Sparecchia.Size = new Size(246, 125);
            Panel_Sparecchia.TabIndex = 9;
            // 
            // Sparecchia_Button
            // 
            Sparecchia_Button.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            Sparecchia_Button.ForeColor = Color.FromArgb(0, 192, 0);
            Sparecchia_Button.Location = new Point(38, 63);
            Sparecchia_Button.Name = "Sparecchia_Button";
            Sparecchia_Button.Size = new Size(171, 43);
            Sparecchia_Button.TabIndex = 10;
            Sparecchia_Button.Text = "Sparecchia!";
            Sparecchia_Button.UseVisualStyleBackColor = true;
            Sparecchia_Button.Click += Sparecchia_Button_Click;
            // 
            // Gnam
            // 
            Gnam.AutoSize = true;
            Gnam.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Gnam.ForeColor = Color.Red;
            Gnam.Location = new Point(47, 21);
            Gnam.Name = "Gnam";
            Gnam.Size = new Size(151, 28);
            Gnam.TabIndex = 10;
            Gnam.Text = "GNAM GNAM!";
            // 
            // Button_Scontrino
            // 
            Button_Scontrino.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Button_Scontrino.Location = new Point(3, 659);
            Button_Scontrino.Name = "Button_Scontrino";
            Button_Scontrino.Size = new Size(236, 29);
            Button_Scontrino.TabIndex = 9;
            Button_Scontrino.Text = "Mostra scontrino!";
            Button_Scontrino.UseVisualStyleBackColor = true;
            Button_Scontrino.Click += MostraScontrino_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1590, 822);
            Controls.Add(Panel_Sparecchia);
            Controls.Add(Panel_Ordini);
            Controls.Add(Tavolo_Picture);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Verifica_Pizzeria";
            ((System.ComponentModel.ISupportInitialize)Tavolo_Picture).EndInit();
            Panel_Ordini.ResumeLayout(false);
            Panel_Ordini.PerformLayout();
            Panel_Sparecchia.ResumeLayout(false);
            Panel_Sparecchia.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private PictureBox Tavolo_Picture;
        private Label Titolo_Menu;
        private Label label1;
        private ListBox Lista_Pizze;
        private ListBox Ordini_Lista;
        private Button Cancella_Button;
        private Button Servi_Button;
        private Button Aggiungi_Button;
        private Panel Panel_Ordini;
        private Panel Panel_Sparecchia;
        private Button Sparecchia_Button;
        private Label Gnam;
        private Button Button_Ordini;
        private Button Button_Scontrino;
    }
}