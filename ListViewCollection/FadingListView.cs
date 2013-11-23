using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;

namespace CRC.Controls
{
	public class FadingListView : System.Windows.Forms.ListView
	{
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Timer timerFading; 
        private Color changeColor = Color.Green;
        private Color deleteColor = Color.Blue;
        private Color addColor = Color.Red;
        private int fadingTime = 10;

        private class TagWrapper
        {
            // Nested class which will hold the list item's tag and information necessary for the fading.

            public object innerObject;
            public ColorTransform colorTransform;
            public bool deleted = false;
            
            public TagWrapper(object tag, Color start, Color goal, int fadingTime)
            {
                this.innerObject = tag;
                this.colorTransform = new ColorTransform(start, goal, fadingTime);
            }
        }

        public FadingListView()
        {
            // This call is required by the Windows.Forms Form Designer.

            InitializeComponent();

            // Create a timer which will tick once per second.

            timerFading = new System.Windows.Forms.Timer();
            timerFading.Enabled = true;
            timerFading.Interval = 1000;
            timerFading.Tick += new System.EventHandler(this.timerFading_Tick);

            SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);

            // This implementation only supports single select.

            MultiSelect = false;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if( components != null )
                    components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
        }
        #endregion

        private void timerFading_Tick(object sender, System.EventArgs e)
        {
            // Loop through all list view items and transform their text color.

            for (int i = Items.Count - 1; i >= 0; i--)
            {
                ListViewItem listViewItem = Items[i];

                if (!((TagWrapper)listViewItem.Tag).colorTransform.Transform())
                {
                    if (((TagWrapper)listViewItem.Tag).deleted)
                    {
                        // The list view item have status deleted and its foretext color is fully faded
                        // since Transform() returned false so we should delete it from the list view.

                        Items.RemoveAt(i);
                    }

                    // Transform() returned false so fading of color is done and no updates are necessary.
                    // Let's continue with next list view item.

                    continue;
                }

                // Update each list view items text color since its color has faded one step.

                listViewItem.ForeColor = ((TagWrapper)listViewItem.Tag).colorTransform.Color;
            }
        }

        private void ListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SelectedIndices.Count == 1 && ((TagWrapper)SelectedItems[0].Tag).deleted)
            {
                // Deleted list view items are displayed while they fade away, but it should not
                // be possible to select them for a user. 
                // Remember, our list view only allows single select.

                SelectedItems[0].Selected = false;
            }
        }

        public ListViewItem AddItem(ListViewItem listViewItem)
        {
            // The user have added a new list view item. It should fade from the AddColor back to its original
            // text color, ForeColor.

            listViewItem.Tag = new TagWrapper(listViewItem.Tag, AddColor, ForeColor, FadingTime);
            listViewItem.ForeColor = AddColor;

            return Items.Add(listViewItem);
        }
        
        public void ChangeItem(ListViewItem listViewItem)
        {
            // The user have changed an existing list view item. It should fade from the ChangeColor back to its original
            // text color, ForeColor.

            ((TagWrapper)listViewItem.Tag).colorTransform = new ColorTransform(ChangeColor, ForeColor, FadingTime);
            listViewItem.ForeColor = ChangeColor;
        }

        public void DeleteItem(ListViewItem listViewItem)
        {
            // The user have deleted an existing list view item. It should fade from the DeleteColor to the list view's
            // back color (BackCOlor) so it becomes invisible. We also need to deselect the item, since it should not
            // be possible for a user to select a deleted item.

            listViewItem.ForeColor = DeleteColor;
            ((TagWrapper)listViewItem.Tag).deleted = true;
            ((TagWrapper)listViewItem.Tag).colorTransform = new ColorTransform(DeleteColor, BackColor, FadingTime);
            listViewItem.Selected = false;
        }

        public static object GetTag(ListViewItem listViewItem)
        {
            // Use GetTag() to get a list view item's associated user data object (tag) instead of 
            // using the ListViewItem.Tag property directly.

            // Important. Only call GetTag() on list view items that been added using FadingListView.AddItem(),
            // otherwise the list view item's tag will not be of type TagWrapper and the following cast will fail.

            return ((TagWrapper)listViewItem.Tag).innerObject;
        }

        public static void SetTag(ListViewItem listViewItem, object tag)
        {
            // Use SetTag() to set a list view item's associated user data object (tag) instead of 
            // using the ListViewItem.Tag property directly.

            // Important. Only call SetTag() on list view items that been added using FadingListView.AddItem(),
            // otherwise the list view item's tag will not be of type TagWrapper and the following cast will fail.

            ((TagWrapper)listViewItem.Tag).innerObject = tag;
        }

        // Design time properties.

        [
            Category("Fading"),
            Description("Deleted list items will initially be displayed in this color.")
        ]
        public Color DeleteColor
        {
            get
            {
                return deleteColor;
            }
            set
            {
                deleteColor = value;
            }
        }

        [
            Category("Fading"),
            Description("Changed list items will initially be displayed in this color.")
        ]
        public Color ChangeColor
        {
            get
            {
                return changeColor;
            }
            set
            {
                changeColor = value;
            }
        }

        [
            Category("Fading"),
            Description("Added list items will initially be displayed in this color.")
        ]
        public Color AddColor
        {
            get
            {
                return addColor;
            }
            set
            {
                addColor = value;
            }
        }

        [
        Category("Fading"),
        Description("Time it will take from that a list item is modified until it is no longer displayed with special color.")
        ]
        public int FadingTime
        {
            get
            {
                return fadingTime;
            }
            set
            {
                fadingTime = value;
            }
        }
	}
}
