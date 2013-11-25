


namespace CRC.Controls
{
    #region  Enum
    /// <summary>
    /// 箭头的方向.
    /// </summary>
    public enum ThumbArrowDirection
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4,
        /// <summary>
        /// 左右?
        /// </summary>
        LeftRight = 5,
        /// <summary>
        /// 上下?
        /// </summary>
        UpDown = 6
    }


    /// <summary>
    /// 分隔栏的状态.
    /// </summary>
    internal enum SpliterPanelState
    {
        /// <summary>
        /// 收缩
        /// </summary>
        Collapsed = 0,
        /// <summary>
        /// 展开
        /// </summary>
        Expanded = 1,
    }

    /// <summary>
    /// 点击SplitContainer控件收缩按钮时隐藏的Panel。
    /// </summary>
    public enum CollapsePanel
    {
        None = 0,
        Panel1 = 1,
        Panel2 = 2,
    }


    #endregion
}