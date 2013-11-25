using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CRC.Controls
{
	/// <summary>
	/// Summary description for ListViewPager.
	/// </summary>
	[Designer(typeof(ListViewPagerDesigner))]
	public class ListViewPager : System.Windows.Forms.UserControl
	{
		#region Designer
		private System.Windows.Forms.Button _firstButton;
		private System.Windows.Forms.Button _preButton;
		private System.Windows.Forms.TextBox _goTextBox;
		private System.Windows.Forms.Button _goButton;
		private System.Windows.Forms.Button _nextButton;
		private System.Windows.Forms.Button _lastButton;
		private System.Windows.Forms.Label _currentPageLabel;
		private System.Windows.Forms.Label _totalLabel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ListViewPager()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ListViewPager));
			this._firstButton = new System.Windows.Forms.Button();
			this._preButton = new System.Windows.Forms.Button();
			this._goTextBox = new System.Windows.Forms.TextBox();
			this._goButton = new System.Windows.Forms.Button();
			this._nextButton = new System.Windows.Forms.Button();
			this._lastButton = new System.Windows.Forms.Button();
			this._currentPageLabel = new System.Windows.Forms.Label();
			this._totalLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _firstButton
			// 
			this._firstButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this._firstButton.Image = ((System.Drawing.Image)(resources.GetObject("_firstButton.Image")));
			this._firstButton.Location = new System.Drawing.Point(0, 23);
			this._firstButton.Name = "_firstButton";
			this._firstButton.Size = new System.Drawing.Size(24, 23);
			this._firstButton.TabIndex = 0;
			this._firstButton.Click += new System.EventHandler(this._firstButton_Click);
			this._firstButton.MouseEnter += new System.EventHandler(this._firstButton_MouseEnter);
			this._firstButton.MouseLeave += new System.EventHandler(this._firstButton_MouseLeave);
			// 
			// _preButton
			// 
			this._preButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this._preButton.Image = ((System.Drawing.Image)(resources.GetObject("_preButton.Image")));
			this._preButton.Location = new System.Drawing.Point(24, 23);
			this._preButton.Name = "_preButton";
			this._preButton.Size = new System.Drawing.Size(24, 23);
			this._preButton.TabIndex = 1;
			this._preButton.Click += new System.EventHandler(this._preButton_Click);
			this._preButton.MouseEnter += new System.EventHandler(this._preButton_MouseEnter);
			this._preButton.MouseLeave += new System.EventHandler(this._preButton_MouseLeave);
			// 
			// _goTextBox
			// 
			this._goTextBox.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this._goTextBox.Location = new System.Drawing.Point(96, 23);
			this._goTextBox.Name = "_goTextBox";
			this._goTextBox.Size = new System.Drawing.Size(48, 23);
			this._goTextBox.TabIndex = 2;
			this._goTextBox.Text = "";
			this._goTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._goTextBox_KeyPress);
			this._goTextBox.TextChanged += new System.EventHandler(this._goTextBox_TextChanged);
			// 
			// _goButton
			// 
			this._goButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this._goButton.Image = ((System.Drawing.Image)(resources.GetObject("_goButton.Image")));
			this._goButton.Location = new System.Drawing.Point(144, 23);
			this._goButton.Name = "_goButton";
			this._goButton.Size = new System.Drawing.Size(24, 23);
			this._goButton.TabIndex = 3;
			this._goButton.Click += new System.EventHandler(this._goButton_Click);
			this._goButton.MouseEnter += new System.EventHandler(this._goButton_MouseEnter);
			this._goButton.MouseLeave += new System.EventHandler(this._goButton_MouseLeave);
			// 
			// _nextButton
			// 
			this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this._nextButton.Image = ((System.Drawing.Image)(resources.GetObject("_nextButton.Image")));
			this._nextButton.Location = new System.Drawing.Point(48, 23);
			this._nextButton.Name = "_nextButton";
			this._nextButton.Size = new System.Drawing.Size(24, 23);
			this._nextButton.TabIndex = 4;
			this._nextButton.Click += new System.EventHandler(this._nextButton_Click);
			this._nextButton.MouseEnter += new System.EventHandler(this._nextButton_MouseEnter);
			this._nextButton.MouseLeave += new System.EventHandler(this._nextButton_MouseLeave);
			// 
			// _lastButton
			// 
			this._lastButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this._lastButton.Image = ((System.Drawing.Image)(resources.GetObject("_lastButton.Image")));
			this._lastButton.Location = new System.Drawing.Point(72, 23);
			this._lastButton.Name = "_lastButton";
			this._lastButton.Size = new System.Drawing.Size(24, 23);
			this._lastButton.TabIndex = 5;
			this._lastButton.Click += new System.EventHandler(this._lastButton_Click);
			this._lastButton.MouseEnter += new System.EventHandler(this._lastButton_MouseEnter);
			this._lastButton.MouseLeave += new System.EventHandler(this._lastButton_MouseLeave);
			// 
			// _currentPageLabel
			// 
			this._currentPageLabel.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this._currentPageLabel.Location = new System.Drawing.Point(0, 0);
			this._currentPageLabel.Name = "_currentPageLabel";
			this._currentPageLabel.Size = new System.Drawing.Size(86, 23);
			this._currentPageLabel.TabIndex = 6;
			this._currentPageLabel.Text = "Current:1";
			this._currentPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _totalLabel
			// 
			this._totalLabel.Location = new System.Drawing.Point(86, 0);
			this._totalLabel.Name = "_totalLabel";
			this._totalLabel.Size = new System.Drawing.Size(82, 23);
			this._totalLabel.TabIndex = 7;
			this._totalLabel.Text = "Total:1";
			this._totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ListViewPager
			// 
			this.Controls.Add(this._totalLabel);
			this.Controls.Add(this._currentPageLabel);
			this.Controls.Add(this._lastButton);
			this.Controls.Add(this._nextButton);
			this.Controls.Add(this._goButton);
			this.Controls.Add(this._goTextBox);
			this.Controls.Add(this._preButton);
			this.Controls.Add(this._firstButton);
			this.Name = "ListViewPager";
			this.Size = new System.Drawing.Size(168, 46);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		#region 事件控制

		private void _firstButton_Click(object sender, System.EventArgs e)
		{
			if(FirstButton_Click != null)
			{
				if(CurrentPageNum != 1)
				{
                    FirstButton_Click(sender, new PagerEventArgs(CurrentPageNum, 1));
					Go2Page(1);
				}
			}
		}

		private void _preButton_Click(object sender, System.EventArgs e)
		{
			if(PreButton_Click != null)
			{
				if(CurrentPageNum > 1)
				{
                    PreButton_Click(sender, new PagerEventArgs(CurrentPageNum, CurrentPageNum-1));
					Go2Page(CurrentPageNum - 1);
				}
			}
		}

		private void _nextButton_Click(object sender, System.EventArgs e)
		{
			if(NextButton_Click != null)
			{
				if(CurrentPageNum < MaxPageNum)
				{
                    NextButton_Click(sender, new PagerEventArgs(CurrentPageNum, CurrentPageNum + 1));
					Go2Page(CurrentPageNum + 1);
				}
			}
		}

		private void _lastButton_Click(object sender, System.EventArgs e)
		{
			if(LastButton_Click != null)
			{
				if(CurrentPageNum != MaxPageNum)
				{
                    LastButton_Click(sender, new PagerEventArgs(CurrentPageNum,MaxPageNum));
					Go2Page(MaxPageNum);
				}
			}
		}

		private void _goButton_Click(object sender, System.EventArgs e)
		{
			if(GoButton_Click != null)
			{
                if (!VerifyInput())
                {
                    //MessageBox.Show("Input string is invalid.\r\n(Event)GoButton_Click(object,EventArgs).");
                    return;
                }
			    if(CurrentPageNum != Convert.ToInt32(_goTextBox.Text))
				{
					GoButton_Click(sender,new PagerEventArgs(CurrentPageNum,Convert.ToInt32(_goTextBox.Text)));
					Go2Page(Convert.ToInt32(_goTextBox.Text));
				}
			}
		}

		private void _goTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == 13)
			{
				if(GoButton_Click != null)
				{
                    if (!VerifyInput())
                    {
                        //MessageBox.Show("Input string is invalid.\r\n(Event)GoButton_Click(object,EventArgs).");
                        return;
                    }
					if(CurrentPageNum != Convert.ToInt32(_goTextBox.Text))
					{
						GoButton_Click(sender,new PagerEventArgs(CurrentPageNum,Convert.ToInt32(_goTextBox.Text)));
						Go2Page(Convert.ToInt32(_goTextBox.Text));
					}
				}
			}
		}

		#endregion

		#region Varibles
		private int _prePageNum = 1 ;
		private int _currentPageNum = 1;
		private int _maxPageNum =1;
	    private int _goPageNum=-1;
		private Color _hoverColor = Color.AliceBlue;
		private Color _backColor = SystemColors.Control ;

	    private string _currentText = "Current";
	    private string _totalText = "Total";
		#endregion

		#region 事件定义
        public delegate void PreClickHandle(object sender, PagerEventArgs e);
        public delegate void FirstClickHandle(object sender, PagerEventArgs e);
        public delegate void NextClickHandle(object sender, PagerEventArgs e);
        public delegate void LastClickHandle(object sender, PagerEventArgs e);
		public delegate void GoToHandle(object sender,PagerEventArgs e);
		/// <summary>
		/// 上一页按钮事件
		/// </summary>
		[Category("Pager"),Description("上一页按钮事件")]
		public event PreClickHandle PreButton_Click ;
		/// <summary>
		/// 第一页按钮事件
		/// </summary>
		[Category("Pager"),Description("第一页按钮事件")]
		public event FirstClickHandle FirstButton_Click ;
		/// <summary>
		/// 下一页按钮事件
		/// </summary>
		[Category("Pager"),Description("下一页按钮事件")]
		public event NextClickHandle NextButton_Click ;
		/// <summary>
		/// 最后一页按钮事件
		/// </summary>
		[Category("Pager"),Description("最后一页按钮事件")]
		public event LastClickHandle LastButton_Click ;
		/// <summary>
		/// 跳到指定页按钮事件
		/// </summary>
		[Category("Pager"),Description("跳到指定页按钮事件")]
		public event GoToHandle GoButton_Click ;
		#endregion

		#region 属性
		[Category("Pager"),Description("Current Page Number."),DefaultValue(1)]
		public int CurrentPageNum
		{
			get{return _currentPageNum ;}
		}
        [Category("Pager"), Description("Max Page Number."), DefaultValue(1)]
		public int MaxPageNum
		{
			get{return _maxPageNum ;}
			set
			{
				_maxPageNum = value;
				_totalLabel.Text = _totalText + ":"+(value==0?"1":value.ToString());
                if (value < _currentPageNum)
                {
                    _currentPageNum = 1;
                    _currentPageLabel.Text = _currentText + ":1";
                }
			}
		}
        [Category("Pager"), Description("Go Page Number."), DefaultValue(-1)]
	    public int GoPageNum
	    {
            get
            {
                //if (VerifyInput())
                //    return Convert.ToInt32(_goTextBox.Text);
                //else
                //    return -1;
				return _goPageNum ;
            }
            set
            {
                _goTextBox.Text = value.ToString();
				_goPageNum = value ;
            }
	    }
		[Category("Pager"), Description("Button's back color when mouse hover on it.")]
		public Color ButtonHoverColor
		{
			get{return _hoverColor ;}
			set{_hoverColor = value;}
		}
		[Category("Pager"), Description("Button's back color when mouse hover on it.")]
		public Color ButtonBackColor
		{
			get{return _backColor ;}
			set
			{
				_backColor = value ;
				_firstButton.BackColor = value ;
				_preButton.BackColor = value ;
				_nextButton.BackColor = value;
				_lastButton.BackColor = value ;
				_goButton.BackColor = value;
			}
		}
        [Category("Pager"), DefaultValue("Current"),Description("'Current' text,for multi-lingual.")]
	    public string CurrentText
	    {
            get
            {
                return _currentText;
            }
            set
            {
                _currentText = value;
                string lbl = _currentPageLabel.Text;
                _currentPageLabel.Text = value+":"+lbl.Substring(lbl.IndexOf(":")+1);
            }
	    }
        [Category("Pager"), DefaultValue("Total"), Description("'Total' text,for multi-lingual.")]
	    public string TotalText
	    {
            get { return _totalText; }
            set
            {
                _totalText = value;
                string lbl = _totalLabel.Text;
                _totalLabel.Text = value + ":" + lbl.Substring(lbl.IndexOf(":")+1);
            }
	    }
		#endregion

		#region 内部方法
		private void Go2Page(int page)
		{
			if(page < 1 || page > MaxPageNum)
				throw new Exception("Out of the page number.\r\n(Method)Go2Page(int).") ;
			_prePageNum = _currentPageNum ;
			_currentPageNum = page ;
			_currentPageLabel.Text = _currentText + ":"+page.ToString(); 
		}

		private bool VerifyInput()
		{
			try
			{
				int p = Convert.ToInt32(_goTextBox.Text); 
				if(p < 1 || p > MaxPageNum)
					return false ;
				return true ;
			}
			catch{return false ;}
		}

		private void HoverIt(Button btn)
		{
			btn.BackColor = _hoverColor ;
		}

		private void LeaveIt(Button btn)
		{
			btn.BackColor = _backColor ;
		}
		#endregion

		#region 公共方法
		[Description("Use it when operate failed in the event.")]
		public void RollBackPage()
		{
			if(_prePageNum >= 1 && _prePageNum <= MaxPageNum)
				Go2Page(_prePageNum);
		}
        public void SetCurrentPage(int page)
        {
            if(page >= 1 && page <= MaxPageNum)
            {
                Go2Page(page);
            }
        }
        public void PerformClickFirstButton()
        {
            //_firstButton_Click(_firstButton, new EventArgs());
            _firstButton.PerformClick();
        }
		/// <summary>
		/// 获取ListView所能显示的最大记录数（无滚动条）
		/// </summary>
		/// <param name="lv"></param>
		/// <returns></returns>
		public int GetListViewPageRecordNum(ref ListView lv)
		{
			int _itemHeight = 0;
			if(lv.Items.Count > 0)
				_itemHeight = RectangleToClient(lv.Items[0].Bounds).Height;
			else
			{
				lv.Items.Add(new ListViewItem(new string[]{"0","0"}))  ;
				_itemHeight = RectangleToClient(lv.Items[0].Bounds).Height;
				lv.Items.Clear() ;
			}
//			RECT itemRect = new RECT();
//			SendMessage(lv.Handle, (int)HeaderControlMessages.HDM_GETITEMRECT, 0, ref itemRect);
			int columnHeaderHeight = 0;//itemRect.bottom - itemRect.top;
//			if(columnHeaderHeight == 0)
			columnHeaderHeight = _itemHeight*2;//用两倍数据行的高度作为列名高度
			//计算listview最多能够容纳的记录条数（不显示滚动条的情况下）
			int rnum = (lv.RectangleToClient(new Rectangle(lv.Location.X, lv.Location.Y, lv.Width, lv.Height)).Height - columnHeaderHeight) / _itemHeight ;
			return rnum;
		}

//		[DllImport("User32.dll", CharSet = CharSet.Auto)]
//		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT r);
		#endregion

		#region 其他控制
        private void _goTextBox_TextChanged(object sender, EventArgs e)
        {
            if(_goTextBox.Text.Trim () != "")
            {
                try
                {
                    int i = Convert.ToInt32(_goTextBox.Text);
                    _goPageNum = i;
                }
                catch
                {
                    _goPageNum = -1;
                }
            }
        }

		private void _firstButton_MouseEnter(object sender, System.EventArgs e)
		{
			HoverIt(_firstButton) ;
		}

		private void _firstButton_MouseLeave(object sender, System.EventArgs e)
		{
			LeaveIt(_firstButton) ;
		}

		private void _preButton_MouseEnter(object sender, System.EventArgs e)
		{
			HoverIt(_preButton) ;
		}

		private void _preButton_MouseLeave(object sender, System.EventArgs e)
		{
			LeaveIt(_preButton) ;
		}

		private void _nextButton_MouseEnter(object sender, System.EventArgs e)
		{
			HoverIt(_nextButton) ;
		}

		private void _nextButton_MouseLeave(object sender, System.EventArgs e)
		{
			LeaveIt(_nextButton) ;
		}

		private void _lastButton_MouseEnter(object sender, System.EventArgs e)
		{
			HoverIt(_lastButton) ;
		}

		private void _lastButton_MouseLeave(object sender, System.EventArgs e)
		{
			LeaveIt(_lastButton) ;
		}

		private void _goButton_MouseEnter(object sender, System.EventArgs e)
		{
			HoverIt(_goButton) ;
		}

		private void _goButton_MouseLeave(object sender, System.EventArgs e)
		{
			LeaveIt(_goButton) ;
		}
		#endregion
	}
	public class PagerEventArgs:EventArgs
	{
		public PagerEventArgs(int op,int np)
		{
			OldPage = op;
			NewPage = np;
		}

		public int OldPage=0;
		public int NewPage=0;
	}
    /// <summary>
    /// ListViewPagerDesigner Class
    /// </summary>
    public class ListViewPagerDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        public ListViewPagerDesigner()
        {
        }

        public override SelectionRules SelectionRules
        {
            get
            {
                SelectionRules rules = SelectionRules.Visible | SelectionRules.Moveable;
                return rules;
            }
        }
    }
//	[StructLayout(LayoutKind.Sequential)]
//	public struct RECT
//	{
//		public int left;
//		public int top;
//		public int right;
//		public int bottom;
//
//		public static implicit operator Rectangle(RECT rect)
//		{
//			return new Rectangle(rect.left, rect.top,
//				rect.right - rect.left, rect.bottom - rect.top);
//		}
//	}
//	public enum HeaderControlMessages : int
//	{
//		HDM_FIRST = 0x1200,
//		HDM_GETITEMRECT = (HDM_FIRST + 7),
//		HDM_HITTEST = (HDM_FIRST + 6),
//		HDM_SETIMAGELIST = (HDM_FIRST + 8),
//		HDM_GETITEMW = (HDM_FIRST + 11),
//		HDM_ORDERTOINDEX = (HDM_FIRST + 15)
//	}

}
