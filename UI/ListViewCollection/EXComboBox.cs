using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace CRC.Controls
{

    public class EXComboBox : ComboBox
    {

        private Brush _HighLightBrush; //color of highlighted items

        public EXComboBox()
        {
            _HighLightBrush = SystemBrushes.Highlight;
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DrawItem += new DrawItemEventHandler(PaintItem);
        }

        public Brush HighLightBrush
        {
            get { return _HighLightBrush; }
            set { _HighLightBrush = value; }
        }

        private void PaintItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) return;
            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) != 0)
            {
                e.Graphics.FillRectangle(_HighLightBrush, e.Bounds);
            }
            Item item = (Item)this.Items[e.Index];
            Rectangle bounds = e.Bounds;
            int x = bounds.X + 2;
            if (item.GetType() == typeof(ImageItem))
            {
                ImageItem imgitem = (ImageItem)item;
                if (imgitem.Image != null)
                {
                    Image img = imgitem.Image;
                    int y = bounds.Y + ((int)(bounds.Height / 2)) - ((int)(img.Height / 2)) + 1;
                    e.Graphics.DrawImage(img, x, y, img.Width, img.Height);
                    x += img.Width + 2;
                }
            }
            else if (item.GetType() == typeof(MultipleImagesItem))
            {
                MultipleImagesItem imgitem = (MultipleImagesItem)item;
                if (imgitem.Images != null)
                {
                    for (int i = 0; i < imgitem.Images.Count; i++)
                    {
                        Image img = (Image)imgitem.Images[i];
                        int y = bounds.Y + ((int)(bounds.Height / 2)) - ((int)(img.Height / 2)) + 1;
                        e.Graphics.DrawImage(img, x, y, img.Width, img.Height);
                        x += img.Width + 2;
                    }
                }
            }
            int fonty = bounds.Y + ((int)(bounds.Height / 2)) - ((int)(e.Font.Height / 2));
            e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), x, fonty);
            e.DrawFocusRectangle();
        }

        public class Item
        {

            private string _Text = "";
            private string _Value = "";

            public Item()
            {

            }

            public Item(string text)
            {
                _Text = text;
            }

            public string Text
            {
                get { return _Text; }
                set { _Text = value; }
            }

            public string Value
            {
                get { return _Value; }
                set { _Value = value; }
            }

            public override string ToString()
            {
                return _Text;
            }

        }

        public class ImageItem : Item
        {

            private Image _Image;

            public ImageItem()
            {

            }

            public ImageItem(string text)
            {
                this.Text = text;
            }

            public ImageItem(Image image)
            {
                _Image = image;
            }

            public ImageItem(string text, Image image)
            {
                this.Text = text;
                _Image = image;
            }

            public ImageItem(Image image, string value)
            {
                _Image = image;
                this.Value = value;
            }

            public ImageItem(string text, Image image, string value)
            {
                this.Text = text;
                _Image = image;
                this.Value = value;
            }

            public Image Image
            {
                get { return _Image; }
                set { _Image = value; }
            }

        }

        public class MultipleImagesItem : Item
        {

            private ArrayList _ImagesList;

            public MultipleImagesItem()
            {

            }

            public MultipleImagesItem(string text)
            {
                this.Text = text;
            }

            public MultipleImagesItem(ArrayList images)
            {
                _ImagesList = images;
            }

            public MultipleImagesItem(string text, ArrayList images)
            {
                this.Text = text;
                _ImagesList = images;
            }

            public MultipleImagesItem(ArrayList images, string value)
            {
                _ImagesList = images;
                this.Value = value;
            }

            public MultipleImagesItem(string text, ArrayList images, string value)
            {
                this.Text = text;
                _ImagesList = images;
                this.Value = value;
            }

            public ArrayList Images
            {
                get { return _ImagesList; }
                set { _ImagesList = value; }
            }

        }

    }

}