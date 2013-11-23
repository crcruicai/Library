using System.Drawing;
using System.Windows.Forms;

namespace CRC.Controls
{
    public class ImageComboBox : ComboBox 
    {
        private ImageList _ImageList = new ImageList(); 

		public ImageComboBox()
		{
			this.DrawMode = DrawMode.OwnerDrawFixed;	
		}

		/// <summary>
        /// ImageList Property
		/// </summary>
		public ImageList ImageList 
		{
			get 
			{
				return _ImageList; 
			}
			set 
			{
				_ImageList = value; 
			}
		}

		/// <summary>
		/// Override OnDrawItem, To Be able To Draw Images, Text, And Font Formatting
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			e.DrawBackground(); //Draw Background Of Item
			e.DrawFocusRectangle(); //Draw Its rectangle

			if (e.Index < 0) //Do We Have A Valid List ?

				//Just Draw Indented Text
				e.Graphics.DrawString(this.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + _ImageList.ImageSize.Width, e.Bounds.Top);

			else //We Have A List
			{
				
				if (this.Items[e.Index].GetType() == typeof(ImageItem))  //Is It A ImageCombo Item ?
				{															

					ImageItem ICCurrItem = (ImageItem)this.Items[e.Index]; //Get Current Item

					//Obtain Current Item's ForeColour
                    Color ICCurrForeColour = (ICCurrItem.ForeColour != Color.FromKnownColor(KnownColor.Transparent)) ? ICCurrItem.ForeColour : e.ForeColor;

					//Obtain Current Item's Font
                    Font ICCurrFont = ICCurrItem.HighLight ? new Font(e.Font, FontStyle.Bold) : e.Font;

					if (ICCurrItem.ImageIndex != -1) //If In Actual List ( Which Needs Images )
					{
						//Draw Image
						this.ImageList.Draw(e.Graphics, e.Bounds.Left, e.Bounds.Top, ICCurrItem.ImageIndex);

						//Then, Draw Text In Specified Bounds
                        e.Graphics.DrawString(ICCurrItem.Text, ICCurrFont, new SolidBrush(ICCurrForeColour), e.Bounds.Left + _ImageList.ImageSize.Width, e.Bounds.Top);
					}
					else //No Image Needed, Index = -1

						//Just Draw The Indented Text
						e.Graphics.DrawString(ICCurrItem.Text, ICCurrFont, new SolidBrush(ICCurrForeColour), e.Bounds.Left + _ImageList.ImageSize.Width, e.Bounds.Top);

				}
				else //Not An ImageCombo Box Item
				
					//Just Draw The Text
					e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + _ImageList.ImageSize.Width, e.Bounds.Top);
				
			}

			base.OnDrawItem (e);
		}

       public class ImageItem : object
        {

            private string _Text = null;	//ImageCombo Item Text

            private int _ImageIndex = -1; //Image Combo Item Image Index

            private bool _HighLight = false; //Highlighted ?

            private Color _ForeColor = Color.FromKnownColor(KnownColor.Transparent); //ImageCombo Item ForeColour

            public ImageItem()
            {
            }

            /// <summary>
            /// Text & Image Index Only
            /// </summary>
            /// <param name="ICIItemText"></param>
            /// <param name="ICImageIndex"></param>
            public ImageItem(string itemText, int imageIndex) //First Overload
            {
                _Text = itemText; //Text
                _ImageIndex = imageIndex; //Image Index
            }

            /// <summary>
            /// Text, Image Index, Highlight, ForeColour
            /// </summary>
            /// <param name="itemText"></param>
            /// <param name="imageIndex"></param>
            /// <param name="highLight"></param>
            /// <param name="foreColor"></param>
            public ImageItem(string itemText, int imageIndex, bool highLight, Color foreColor) //Second Overload
            {
                _Text = itemText; //Text
                _ImageIndex = imageIndex; //Image Index
                _HighLight = highLight; //Highlighted ?
                _ForeColor = foreColor; //ForeColour
            }

            /// <summary>
            /// ImageCombo Item Text
            /// </summary>
            public string Text
            {
                get
                {
                    return _Text; 
                }
                set
                {
                    _Text = value;
                }
            }

            /// <summary>
            /// Image Index
            /// </summary>
            public int ImageIndex
            {
                get
                {
                    return _ImageIndex; 
                }
                set
                {
                    _ImageIndex = value;
                }
            }

            /// <summary>
            /// Highlighted ?
            /// </summary>
            public bool HighLight
            {
                get
                {
                    return _HighLight; 
                }
                set
                {
                    _HighLight = value; 
                }
            }

            /// <summary>
            /// ForeColour
            /// </summary>
            public Color ForeColour
            {
                get
                {
                    return _ForeColor; 
                }
                set
                {
                    _ForeColor = value;
                }
            }

            /// <summary>
            /// Override ToString To Return Item Text
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return _Text;
            }

        }
		
    }
}
