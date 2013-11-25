using System;
using System.Drawing;
using System.Windows.Forms;
using CRC.Controls;

namespace CrazyCombos
{
    public partial class frmCrazy : Form //Testing Form
    {
        private SolidBrush FontForeColour; //Font's Colour

        public frmCrazy()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Upfront Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCrazy_Load(object sender, EventArgs e)
        {
            imgCboCrazy.Items.Add(new ImageComboBox.ImageItem("No Icon", -1)); //Insert Text Only In ImageCombo
            imgCboCrazy.Items.Add(new ImageComboBox.ImageItem("Blue Hills", 0, true, Color.Green)); //First Real Item In ImageCombo
            imgCboCrazy.Items.Add(new ImageComboBox.ImageItem("Sunset", 1, false, Color.Blue)); //Second ImageCombo Item
            imgCboCrazy.Items.Add(new ImageComboBox.ImageItem("Water Lillies", 2, false, Color.Gray)); //Third ImageCombo Item
            imgCboCrazy.Items.Add(new ImageComboBox.ImageItem("Winter", 3, false, Color.Red)); //Fourth ImageCombo Item

           

            //Alignment Combo
            string[] AllAlignArr = new string[10]; //Array For Combo Items

            int intAllLoop; //Loop Counter
            for (intAllLoop = 0; intAllLoop <= 9; intAllLoop++) //Initiate Loop
            {
                AllAlignArr[intAllLoop] = "Item " + intAllLoop; //Fill Each Array Element With Appropriate Text

                cboAlignAllCrazy.Items.Add(AllAlignArr[intAllLoop]); //Display Items In Alignment Combo

                //Set Alignment Combo's Text To Left ( Default )
                cboAlignAllCrazy.CASDropListAlignment = ComboAlignSettings.CASAlignment.CASLeft;
            }

          
 
        }




        /// <summary>
        /// Center The Text Again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCenterCrazy_DropDown(object sender, EventArgs e)
        {
            cboCenterCrazy.Items.Clear(); //Clear ComboBox
            string[] stringArr = new string[10]; //New Items

            int intLoop; //Loop Counter

            for (intLoop = 0; intLoop <= 9; intLoop++) //Start Loop
            {
                stringArr[intLoop] = "Item " + intLoop; //Add Items To Array

                //Center Align Items Again
                cboCenterCrazy.Items.Add(stringArr[intLoop].PadLeft(((cboCenterCrazy.DropDownWidth / 3) - (stringArr[intLoop].Length)) / 2));

            }

        }

    

        /// <summary>
        /// Button To Right Align Alignment Combo's Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRightText_Click(object sender, EventArgs e)
        {

            if (cboAlignAllCrazy.CASDropListAlignment == ComboAlignSettings.CASAlignment.CASLeft) //If Left
            {
                cboAlignAllCrazy.CASDropListAlignment = ComboAlignSettings.CASAlignment.CASRight; //Set To Right
            }
 
        }

        /// <summary>
        /// Button To Left Align Alignment Combo's ScrollBar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeftAlignScroll_Click(object sender, EventArgs e)
        {
            if (cboAlignAllCrazy.CASScrollAlignment == ComboAlignSettings.CASAlignment.CASRight) //If Right Aligned
            {
                cboAlignAllCrazy.CASScrollAlignment = ComboAlignSettings.CASAlignment.CASLeft; //Set To Left
            }
        }

        /// <summary>
        /// Button To Left Align Alignment Combo's Drop Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeftAlignButton_Click(object sender, EventArgs e)
        {
            if (cboAlignAllCrazy.CASDropButtonAlignment == ComboAlignSettings.CASAlignment.CASRight) //If Right
            {
                cboAlignAllCrazy.CASDropButtonAlignment = ComboAlignSettings.CASAlignment.CASLeft; //Set To Left
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); //Exit
        }

    }
}