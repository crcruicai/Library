﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    /// <summary>
    ///     美化的ComboBox.
    /// </summary>
    [ToolboxBitmap(typeof(ComboBox))]
    public class ComboBoxEx : ComboBox
    {
        #region Fields

        private Color _ArrowColor = Color.FromArgb(19, 88, 128);
        private bool _BPainting;
        private Color _BaseColor = Color.FromArgb(51, 161, 224);
        private Color _BorderColor = Color.FromArgb(51, 161, 224);
        private ControlState _ButtonState;
        private IntPtr _EditHandle;

        #endregion

        #region Constructors

        public ComboBoxEx()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #endregion

        #region Properties

        [DefaultValue(typeof(Color), "51, 161, 224")]
        public Color BaseColor
        {
            get { return _BaseColor; }
            set
            {
                if(_BaseColor != value)
                {
                    _BaseColor = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "51, 161, 224")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                if(_BorderColor != value)
                {
                    _BorderColor = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "19, 88, 128")]
        public Color ArrowColor
        {
            get { return _ArrowColor; }
            set
            {
                if(_ArrowColor != value)
                {
                    _ArrowColor = value;
                    base.Invalidate();
                }
            }
        }

        internal ControlState ButtonState
        {
            get { return _ButtonState; }
            set
            {
                if(_ButtonState != value)
                {
                    _ButtonState = value;
                    Invalidate(ButtonRect);
                }
            }
        }

        internal Rectangle ButtonRect
        {
            get { return GetDropDownButtonRect(); }
        }

        internal bool ButtonPressed
        {
            get { return IsHandleCreated && GetComboBoxButtonPressed(); }
        }

        internal IntPtr EditHandle
        {
            get { return _EditHandle; }
        }

        internal Rectangle EditRect
        {
            get
            {
                if(DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    var rect = new Rectangle(3, 3, Width - ButtonRect.Width - 6, Height - 6);
                    if(RightToLeft == RightToLeft.Yes)
                    {
                        rect.X += ButtonRect.Right;
                    }
                    return rect;
                }
                if(IsHandleCreated && EditHandle != IntPtr.Zero)
                {
                    var rcClient = new NativeMethods.RECT();
                    NativeMethods.GetWindowRect(EditHandle, ref rcClient);
                    return RectangleToClient(rcClient.Rect);
                }
                return Rectangle.Empty;
            }
        }

        #endregion

        #region Override Methods

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            NativeMethods.ComboBoxInfo cbi = GetComboBoxInfo();
            _EditHandle = cbi.hwndEdit;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point point = e.Location;
            if(ButtonRect.Contains(point))
            {
                ButtonState = ControlState.Hover;
            }
            else
            {
                ButtonState = ControlState.Normal;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            Point point = PointToClient(Cursor.Position);
            if(ButtonRect.Contains(point))
            {
                ButtonState = ControlState.Hover;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            ButtonState = ControlState.Normal;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ButtonState = ControlState.Normal;
        }

        protected override void WndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case (int) NativeMethods.WindowsMessage.WM_PAINT:
                    WmPaint(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region Windows Message Methods

        private void WmPaint(ref Message m)
        {
            if(base.DropDownStyle == ComboBoxStyle.Simple)
            {
                base.WndProc(ref m);
                return;
            }

            if(base.DropDownStyle == ComboBoxStyle.DropDown)
            {
                if(!_BPainting)
                {
                    var ps = new NativeMethods.PAINTSTRUCT();

                    _BPainting = true;
                    NativeMethods.BeginPaint(m.HWnd, ref ps);

                    RenderComboBox(ref m);

                    NativeMethods.EndPaint(m.HWnd, ref ps);
                    _BPainting = false;
                    m.Result = NativeMethods.TRUE;
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            else
            {
                base.WndProc(ref m);
                RenderComboBox(ref m);
            }
        }

        #endregion

        #region Render Methods

        private void RenderComboBox(ref Message m)
        {
            var rect = new Rectangle(Point.Empty, Size);
            Rectangle buttonRect = ButtonRect;
            ControlState state = ButtonPressed ? ControlState.Pressed : ButtonState;
            using(Graphics g = Graphics.FromHwnd(m.HWnd))
            {
                RenderComboBoxBackground(g, rect, buttonRect);
                RenderConboBoxDropDownButton(g, ButtonRect, state);
                RenderConboBoxBorder(g, rect);
            }
        }

        private void RenderConboBoxBorder(Graphics g, Rectangle rect)
        {
            Color borderColor = base.Enabled ? _BorderColor : SystemColors.ControlDarkDark;
            using(var pen = new Pen(borderColor))
            {
                rect.Width--;
                rect.Height--;
                g.DrawRectangle(pen, rect);
            }
        }

        private void RenderComboBoxBackground(Graphics g, Rectangle rect, Rectangle buttonRect)
        {
            Color backColor = base.Enabled ? base.BackColor : SystemColors.Control;
            using(var brush = new SolidBrush(backColor))
            {
                buttonRect.Inflate(-1, -1);
                rect.Inflate(-1, -1);
                using(var region = new Region(rect))
                {
                    region.Exclude(buttonRect);
                    region.Exclude(EditRect);
                    g.FillRegion(brush, region);
                }
            }
        }

        private void RenderConboBoxDropDownButton(Graphics g, Rectangle buttonRect, ControlState state)
        {
            Color baseColor;
            Color backColor = Color.FromArgb(160, 250, 250, 250);
            Color borderColor = base.Enabled ? _BorderColor : SystemColors.ControlDarkDark;
            Color arrowColor = base.Enabled ? _ArrowColor : SystemColors.ControlDarkDark;
            Rectangle rect = buttonRect;

            if(base.Enabled)
            {
                switch(state)
                {
                    case ControlState.Hover:
                        baseColor = RenderHelper.GetColor(_BaseColor, 0, -33, -22, -13);
                        break;
                    case ControlState.Pressed:
                        baseColor = RenderHelper.GetColor(_BaseColor, 0, -65, -47, -25);
                        break;
                    default:
                        baseColor = _BaseColor;
                        break;
                }
            }
            else
            {
                baseColor = SystemColors.ControlDark;
            }

            rect.Inflate(-1, -1);

            RenderScrollBarArrowInternal(g, rect, baseColor, borderColor, backColor, arrowColor, RoundStyle.None,
                true, false, ArrowDirection.Down, LinearGradientMode.Vertical);
        }

        internal void RenderScrollBarArrowInternal(Graphics g, Rectangle rect, Color baseColor, 
            Color borderColor, Color innerBorderColor, Color arrowColor, RoundStyle roundStyle, 
            bool drawBorder, bool drawGlass, ArrowDirection arrowDirection, LinearGradientMode mode)
        {
            RenderHelper.RenderBackgroundInternal(g, rect, baseColor, borderColor, innerBorderColor,
                roundStyle, 0, .45F, drawBorder, drawGlass, mode);

            using(var brush = new SolidBrush(arrowColor))
            {
                RenderArrowInternal(g, rect, arrowDirection, brush);
            }
        }

        internal void RenderArrowInternal(Graphics g, Rectangle dropDownRect, ArrowDirection direction, Brush brush)
        {
            var point = new Point(dropDownRect.Left + (dropDownRect.Width/2), dropDownRect.Top + (dropDownRect.Height/2));
            Point[] points = null;
            switch(direction)
            {
                case ArrowDirection.Left:
                    points = new[] 
                    {
                        new Point(point.X + 2, point.Y - 3), 
                        new Point(point.X + 2, point.Y + 3), 
                        new Point(point.X - 1, point.Y)
                    };
                    break;

                case ArrowDirection.Up:
                    points = new[] 
                    {
                        new Point(point.X - 3, point.Y + 2), 
                        new Point(point.X + 3, point.Y + 2),
                        new Point(point.X, point.Y - 2)
                    };
                    break;

                case ArrowDirection.Right:
                    points = new[]
                    {
                        new Point(point.X - 2, point.Y - 3),
                        new Point(point.X - 2, point.Y + 3),
                        new Point(point.X + 1, point.Y)
                    };
                    break;

                default:
                    points = new[] 
                    {
                        new Point(point.X - 2, point.Y - 1), 
                        new Point(point.X + 3, point.Y - 1),
                        new Point(point.X, point.Y + 2)
                    };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        #endregion

        #region Help Methods

        private NativeMethods.ComboBoxInfo GetComboBoxInfo()
        {
            var cbi = new NativeMethods.ComboBoxInfo();
            cbi.cbSize = Marshal.SizeOf(cbi);
            NativeMethods.GetComboBoxInfo(base.Handle, ref cbi);
            return cbi;
        }

        private bool GetComboBoxButtonPressed()
        {
            NativeMethods.ComboBoxInfo cbi = GetComboBoxInfo();
            return cbi.stateButton == NativeMethods.ComboBoxButtonState.STATE_SYSTEM_PRESSED;
        }

        private Rectangle GetDropDownButtonRect()
        {
            NativeMethods.ComboBoxInfo cbi = GetComboBoxInfo();

            return cbi.rcButton.Rect;
        }

        #endregion
    }
}