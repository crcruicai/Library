namespace CrazyCombos
{
    partial class frmCrazy
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrazy));
            this.label1 = new System.Windows.Forms.Label();
            this.ilCrazy = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboCenterCrazy = new System.Windows.Forms.ComboBox();
            this.btnRightText = new System.Windows.Forms.Button();
            this.btnLeftAlignScroll = new System.Windows.Forms.Button();
            this.btnLeftAlignButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboAlignAllCrazy = new System.Windows.Forms.ComboAlignSettings();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imgCboCrazy = new CRC.Controls.ImageComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.fontComboBox1 = new CRC.Controls.FontComboBox();
            this.lineComboBox1 = new CRC.Controls.LineComboBox();
            this.colorComboBox1 = new CRC.ColorComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Image ComboBox";
            // 
            // ilCrazy
            // 
            this.ilCrazy.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilCrazy.ImageStream")));
            this.ilCrazy.TransparentColor = System.Drawing.Color.Transparent;
            this.ilCrazy.Images.SetKeyName(0, "Blue hills.jpg");
            this.ilCrazy.Images.SetKeyName(1, "Sunset.jpg");
            this.ilCrazy.Images.SetKeyName(2, "Water lilies.jpg");
            this.ilCrazy.Images.SetKeyName(3, "Winter.jpg");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "System Colour ComboBox";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Center Align Normal ComboBox Text";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Custom Alignment ComboBox";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCenterCrazy
            // 
            this.cboCenterCrazy.FormattingEnabled = true;
            this.cboCenterCrazy.Location = new System.Drawing.Point(6, 32);
            this.cboCenterCrazy.Name = "cboCenterCrazy";
            this.cboCenterCrazy.Size = new System.Drawing.Size(203, 20);
            this.cboCenterCrazy.TabIndex = 8;
            this.cboCenterCrazy.DropDown += new System.EventHandler(this.cboCenterCrazy_DropDown);
            // 
            // btnRightText
            // 
            this.btnRightText.Location = new System.Drawing.Point(6, 100);
            this.btnRightText.Name = "btnRightText";
            this.btnRightText.Size = new System.Drawing.Size(203, 21);
            this.btnRightText.TabIndex = 10;
            this.btnRightText.Text = "Right Align Text Custom ComboBox";
            this.btnRightText.UseVisualStyleBackColor = true;
            this.btnRightText.Click += new System.EventHandler(this.btnRightText_Click);
            // 
            // btnLeftAlignScroll
            // 
            this.btnLeftAlignScroll.Location = new System.Drawing.Point(6, 126);
            this.btnLeftAlignScroll.Name = "btnLeftAlignScroll";
            this.btnLeftAlignScroll.Size = new System.Drawing.Size(203, 21);
            this.btnLeftAlignScroll.TabIndex = 11;
            this.btnLeftAlignScroll.Text = "Left Align ScrollBar Custom ComboBox";
            this.btnLeftAlignScroll.UseVisualStyleBackColor = true;
            this.btnLeftAlignScroll.Click += new System.EventHandler(this.btnLeftAlignScroll_Click);
            // 
            // btnLeftAlignButton
            // 
            this.btnLeftAlignButton.Location = new System.Drawing.Point(6, 153);
            this.btnLeftAlignButton.Name = "btnLeftAlignButton";
            this.btnLeftAlignButton.Size = new System.Drawing.Size(203, 21);
            this.btnLeftAlignButton.TabIndex = 12;
            this.btnLeftAlignButton.Text = "Left Align Button Custom ComboBox";
            this.btnLeftAlignButton.UseVisualStyleBackColor = true;
            this.btnLeftAlignButton.Click += new System.EventHandler(this.btnLeftAlignButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Line ComboBox";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "Fonts ComboBox";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboCenterCrazy);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboAlignAllCrazy);
            this.groupBox1.Controls.Add(this.btnRightText);
            this.groupBox1.Controls.Add(this.btnLeftAlignScroll);
            this.groupBox1.Controls.Add(this.btnLeftAlignButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 189);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ComboBox Alignment Options";
            // 
            // cboAlignAllCrazy
            // 
            this.cboAlignAllCrazy.CASDropButtonAlignment = System.Windows.Forms.ComboAlignSettings.CASAlignment.CASRight;
            this.cboAlignAllCrazy.CASDropListAlignment = System.Windows.Forms.ComboAlignSettings.CASAlignment.CASLeft;
            this.cboAlignAllCrazy.CASScrollAlignment = System.Windows.Forms.ComboAlignSettings.CASAlignment.CASRight;
            this.cboAlignAllCrazy.FormattingEnabled = true;
            this.cboAlignAllCrazy.Location = new System.Drawing.Point(6, 75);
            this.cboAlignAllCrazy.Name = "cboAlignAllCrazy";
            this.cboAlignAllCrazy.Size = new System.Drawing.Size(203, 20);
            this.cboAlignAllCrazy.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.colorComboBox1);
            this.groupBox2.Controls.Add(this.lineComboBox1);
            this.groupBox2.Controls.Add(this.fontComboBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.imgCboCrazy);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(249, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 189);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom ComboBox Item Options";
            // 
            // imgCboCrazy
            // 
            this.imgCboCrazy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.imgCboCrazy.FormattingEnabled = true;
            this.imgCboCrazy.ImageList = this.ilCrazy;
            this.imgCboCrazy.Location = new System.Drawing.Point(6, 153);
            this.imgCboCrazy.Name = "imgCboCrazy";
            this.imgCboCrazy.Size = new System.Drawing.Size(195, 22);
            this.imgCboCrazy.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 204);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(453, 21);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // fontComboBox1
            // 
            this.fontComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.fontComboBox1.FormattingEnabled = true;
            this.fontComboBox1.Items.AddRange(new object[] {
            new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Aharoni", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Algerian", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Andalus", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Angsana New", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("AngsanaUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Aparajita", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Arabic Typesetting", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bauhaus 93", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bell MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Berlin Sans FB Demi", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bernard MT Condensed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Blackadder ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bodoni MT Black", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bodoni MT Condensed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bodoni MT Poster Compressed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bookshelf Symbol 7", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Bradley Hand ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Broadway", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Browallia New", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("BrowalliaUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Brush Script MT", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
            new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Calisto MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Cambria Math", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Castellar", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Centaur", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Century Schoolbook", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Chiller", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Colonna MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Copperplate Gothic Bold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Copperplate Gothic Light", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Cordia New", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("CordiaUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Curlz MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("DaunPenh", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("David", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("DFKai-SB", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("DilleniaUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("DokChampa", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Edwardian Script ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Engravers MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Eras Demi ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Eras Light ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Eras Medium ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Estrangelo Edessa", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("EucrosiaUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Euphemia", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Felix Titling", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Footlight MT Light", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Forte", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Franklin Gothic Demi", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Franklin Gothic Demi Cond", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Franklin Gothic Heavy", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Franklin Gothic Medium Cond", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("FrankRuehl", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("FreesiaUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Freestyle Script", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("French Script MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gabriola", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gautami", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gigi", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gill Sans MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gill Sans MT Condensed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gill Sans MT Ext Condensed Bold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gill Sans Ultra Bold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gisha", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Gloucester MT Extra Condensed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Goudy Old Style", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Goudy Stout", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Haettenschweiler", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Harlow Solid Italic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
            new System.Drawing.Font("Harrington", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("High Tower Text", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Imprint MT Shadow", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Informal Roman", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("IrisUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Iskoola Pota", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("JasmineUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Jokerman", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Juice ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Kalinga", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Kartika", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Khmer UI", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("KodchiangUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Kokila", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Kristen ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Kunstler Script", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lao UI", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Latha", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Leelawadee", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Levenim MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("LilyUPC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lucida Handwriting", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Magneto", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Mangal", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Matura MT Script Capitals", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Meiryo", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Microsoft Himalaya", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Microsoft New Tai Lue", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Microsoft PhagsPa", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Microsoft Uighur", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MingLiU", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MingLiU-ExtB", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MingLiU_HKSCS", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MingLiU_HKSCS-ExtB", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Miriam", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Mistral", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Monotype Corsiva", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
            new System.Drawing.Font("MoolBoran", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MS Mincho", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MS Outlook", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MS PMincho", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MS Reference Specialty", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MT Extra", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Narkisim", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Niagara Engraved", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Niagara Solid", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Nyala", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("OCR A Extended", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Onyx", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Palace Script MT", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
            new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Papyrus", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Parchment", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Perpetua", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Perpetua Titling MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Plantagenet Cherokee", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Playbill", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("PMingLiU-ExtB", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Poor Richard", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Raavi", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Rage Italic", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Ravie", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Rockwell Condensed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Rockwell Extra Bold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Rod", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Sakkal Majalla", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Script MT Bold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Segoe Script", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Shonar Bangla", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Shruti", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Simplified Arabic", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Simplified Arabic Fixed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("SimSun-ExtB", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Snap ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Sylfaen", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Tempus Sans ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Traditional Arabic", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Tunga", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Tw Cen MT Condensed", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Tw Cen MT Condensed Extra Bold", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Utsaah", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Vani", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Vijaya", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Viner Hand ITC", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Vivaldi", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
            new System.Drawing.Font("Vladimir Script", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Vrinda", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Wide Latin", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("仿宋", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文仿宋", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文宋体", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文彩云", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文新魏", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文楷体", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文琥珀", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文行楷", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("华文隶书", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("方正姚体", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("方正舒体", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Bold),
            new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold)});
            this.fontComboBox1.Location = new System.Drawing.Point(6, 70);
            this.fontComboBox1.Name = "fontComboBox1";
            this.fontComboBox1.Size = new System.Drawing.Size(195, 22);
            this.fontComboBox1.TabIndex = 16;
            // 
            // lineComboBox1
            // 
            this.lineComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lineComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lineComboBox1.FormattingEnabled = true;
            this.lineComboBox1.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom"});
            this.lineComboBox1.Location = new System.Drawing.Point(9, 111);
            this.lineComboBox1.Name = "lineComboBox1";
            this.lineComboBox1.Size = new System.Drawing.Size(192, 22);
            this.lineComboBox1.TabIndex = 17;
            // 
            // colorComboBox1
            // 
            this.colorComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorComboBox1.FormattingEnabled = true;
            this.colorComboBox1.Items.AddRange(new object[] {
            System.Drawing.SystemColors.ActiveBorder,
            System.Drawing.SystemColors.ActiveCaption,
            System.Drawing.SystemColors.ActiveCaptionText,
            System.Drawing.SystemColors.AppWorkspace,
            System.Drawing.SystemColors.Control,
            System.Drawing.SystemColors.ControlDark,
            System.Drawing.SystemColors.ControlDarkDark,
            System.Drawing.SystemColors.ControlLight,
            System.Drawing.SystemColors.ControlLightLight,
            System.Drawing.SystemColors.ControlText,
            System.Drawing.SystemColors.Desktop,
            System.Drawing.SystemColors.GrayText,
            System.Drawing.SystemColors.Highlight,
            System.Drawing.SystemColors.HighlightText,
            System.Drawing.SystemColors.HotTrack,
            System.Drawing.SystemColors.InactiveBorder,
            System.Drawing.SystemColors.InactiveCaption,
            System.Drawing.SystemColors.InactiveCaptionText,
            System.Drawing.SystemColors.Info,
            System.Drawing.SystemColors.InfoText,
            System.Drawing.SystemColors.Menu,
            System.Drawing.SystemColors.MenuText,
            System.Drawing.SystemColors.ScrollBar,
            System.Drawing.SystemColors.Window,
            System.Drawing.SystemColors.WindowFrame,
            System.Drawing.SystemColors.WindowText,
            System.Drawing.Color.Transparent,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.AntiqueWhite,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.Aquamarine,
            System.Drawing.Color.Azure,
            System.Drawing.Color.Beige,
            System.Drawing.Color.Bisque,
            System.Drawing.Color.Black,
            System.Drawing.Color.BlanchedAlmond,
            System.Drawing.Color.Blue,
            System.Drawing.Color.BlueViolet,
            System.Drawing.Color.Brown,
            System.Drawing.Color.BurlyWood,
            System.Drawing.Color.CadetBlue,
            System.Drawing.Color.Chartreuse,
            System.Drawing.Color.Chocolate,
            System.Drawing.Color.Coral,
            System.Drawing.Color.CornflowerBlue,
            System.Drawing.Color.Cornsilk,
            System.Drawing.Color.Crimson,
            System.Drawing.Color.Cyan,
            System.Drawing.Color.DarkBlue,
            System.Drawing.Color.DarkCyan,
            System.Drawing.Color.DarkGoldenrod,
            System.Drawing.Color.DarkGray,
            System.Drawing.Color.DarkGreen,
            System.Drawing.Color.DarkKhaki,
            System.Drawing.Color.DarkMagenta,
            System.Drawing.Color.DarkOliveGreen,
            System.Drawing.Color.DarkOrange,
            System.Drawing.Color.DarkOrchid,
            System.Drawing.Color.DarkRed,
            System.Drawing.Color.DarkSalmon,
            System.Drawing.Color.DarkSeaGreen,
            System.Drawing.Color.DarkSlateBlue,
            System.Drawing.Color.DarkSlateGray,
            System.Drawing.Color.DarkTurquoise,
            System.Drawing.Color.DarkViolet,
            System.Drawing.Color.DeepPink,
            System.Drawing.Color.DeepSkyBlue,
            System.Drawing.Color.DimGray,
            System.Drawing.Color.DodgerBlue,
            System.Drawing.Color.Firebrick,
            System.Drawing.Color.FloralWhite,
            System.Drawing.Color.ForestGreen,
            System.Drawing.Color.Fuchsia,
            System.Drawing.Color.Gainsboro,
            System.Drawing.Color.GhostWhite,
            System.Drawing.Color.Gold,
            System.Drawing.Color.Goldenrod,
            System.Drawing.Color.Gray,
            System.Drawing.Color.Green,
            System.Drawing.Color.GreenYellow,
            System.Drawing.Color.Honeydew,
            System.Drawing.Color.HotPink,
            System.Drawing.Color.IndianRed,
            System.Drawing.Color.Indigo,
            System.Drawing.Color.Ivory,
            System.Drawing.Color.Khaki,
            System.Drawing.Color.Lavender,
            System.Drawing.Color.LavenderBlush,
            System.Drawing.Color.LawnGreen,
            System.Drawing.Color.LemonChiffon,
            System.Drawing.Color.LightBlue,
            System.Drawing.Color.LightCoral,
            System.Drawing.Color.LightCyan,
            System.Drawing.Color.LightGoldenrodYellow,
            System.Drawing.Color.LightGray,
            System.Drawing.Color.LightGreen,
            System.Drawing.Color.LightPink,
            System.Drawing.Color.LightSalmon,
            System.Drawing.Color.LightSeaGreen,
            System.Drawing.Color.LightSkyBlue,
            System.Drawing.Color.LightSlateGray,
            System.Drawing.Color.LightSteelBlue,
            System.Drawing.Color.LightYellow,
            System.Drawing.Color.Lime,
            System.Drawing.Color.LimeGreen,
            System.Drawing.Color.Linen,
            System.Drawing.Color.Magenta,
            System.Drawing.Color.Maroon,
            System.Drawing.Color.MediumAquamarine,
            System.Drawing.Color.MediumBlue,
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.MediumPurple,
            System.Drawing.Color.MediumSeaGreen,
            System.Drawing.Color.MediumSlateBlue,
            System.Drawing.Color.MediumSpringGreen,
            System.Drawing.Color.MediumTurquoise,
            System.Drawing.Color.MediumVioletRed,
            System.Drawing.Color.MidnightBlue,
            System.Drawing.Color.MintCream,
            System.Drawing.Color.MistyRose,
            System.Drawing.Color.Moccasin,
            System.Drawing.Color.NavajoWhite,
            System.Drawing.Color.Navy,
            System.Drawing.Color.OldLace,
            System.Drawing.Color.Olive,
            System.Drawing.Color.OliveDrab,
            System.Drawing.Color.Orange,
            System.Drawing.Color.OrangeRed,
            System.Drawing.Color.Orchid,
            System.Drawing.Color.PaleGoldenrod,
            System.Drawing.Color.PaleGreen,
            System.Drawing.Color.PaleTurquoise,
            System.Drawing.Color.PaleVioletRed,
            System.Drawing.Color.PapayaWhip,
            System.Drawing.Color.PeachPuff,
            System.Drawing.Color.Peru,
            System.Drawing.Color.Pink,
            System.Drawing.Color.Plum,
            System.Drawing.Color.PowderBlue,
            System.Drawing.Color.Purple,
            System.Drawing.Color.Red,
            System.Drawing.Color.RosyBrown,
            System.Drawing.Color.RoyalBlue,
            System.Drawing.Color.SaddleBrown,
            System.Drawing.Color.Salmon,
            System.Drawing.Color.SandyBrown,
            System.Drawing.Color.SeaGreen,
            System.Drawing.Color.SeaShell,
            System.Drawing.Color.Sienna,
            System.Drawing.Color.Silver,
            System.Drawing.Color.SkyBlue,
            System.Drawing.Color.SlateBlue,
            System.Drawing.Color.SlateGray,
            System.Drawing.Color.Snow,
            System.Drawing.Color.SpringGreen,
            System.Drawing.Color.SteelBlue,
            System.Drawing.Color.Tan,
            System.Drawing.Color.Teal,
            System.Drawing.Color.Thistle,
            System.Drawing.Color.Tomato,
            System.Drawing.Color.Turquoise,
            System.Drawing.Color.Violet,
            System.Drawing.Color.Wheat,
            System.Drawing.Color.White,
            System.Drawing.Color.WhiteSmoke,
            System.Drawing.Color.Yellow,
            System.Drawing.Color.YellowGreen,
            System.Drawing.SystemColors.ButtonFace,
            System.Drawing.SystemColors.ButtonHighlight,
            System.Drawing.SystemColors.ButtonShadow,
            System.Drawing.SystemColors.GradientActiveCaption,
            System.Drawing.SystemColors.GradientInactiveCaption,
            System.Drawing.SystemColors.MenuBar,
            System.Drawing.SystemColors.MenuHighlight});
            this.colorComboBox1.Location = new System.Drawing.Point(9, 30);
            this.colorComboBox1.Name = "colorComboBox1";
            this.colorComboBox1.Size = new System.Drawing.Size(192, 22);
            this.colorComboBox1.TabIndex = 18;
            // 
            // frmCrazy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 236);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCrazy";
            this.Text = "C# Crazy Combos";
            this.Load += new System.EventHandler(this.frmCrazy_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.ImageComboBox imgCboCrazy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList ilCrazy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboCenterCrazy;
        private System.Windows.Forms.ComboAlignSettings cboAlignAllCrazy;
        private System.Windows.Forms.Button btnRightText;
        private System.Windows.Forms.Button btnLeftAlignScroll;
        private System.Windows.Forms.Button btnLeftAlignButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExit;
        private CRC.ColorComboBox colorComboBox1;
        private CRC.Controls.LineComboBox lineComboBox1;
        private CRC.Controls.FontComboBox fontComboBox1;
    }
}

