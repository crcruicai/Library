/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/10 22:46:16
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CRC.Util
{
    /// <summary>
    /// 该类实现获取系统中文件的关联图标
    /// </summary>
    public class SystemIcon
    {
        #region 函数说明

        /*C#获取文件(磁盘驱动器)的关联图标（使用API SHGetFileInfo）收藏
        这是一个C#调用系统API SHGetFileInfo 的一个演示例子，也是给一位网友的答复，先看效果图：        
         * SHGetFileInfo 这个API， 可以获取指定对象的非常详细的相关信息，
         * 具体的内容，大家可以MSDN上关于此API的说明。    
         * 这个获取关联图标，可以获取磁盘分区的图标，可以获取某个特定类型的文件的图标，
         * 也可以获取某个指定文件的图标，下面给出实现的全部代码：

        */

        /*
         SHGetFileInfo函数
function SHGetFileInfo(pszPath: PAnsiChar; dwFileAttributes: DWORD;
  var psfi: TSHFileInfo; cbFileInfo, uFlags: UINT): DWORD; stdcall;

pszPath 参数:指定的文件名。
 当uFlags的取值中不包含 SHGFI_PIDL时,可直接指定;
 当uFlags的取值中包含 SHGFI_PIDL时pszPath要通过计算获得,不能直接指定;

dwFileAttributes参数:文件属性。
 仅当uFlags的取值中包含SHGFI_USEFILEATTRIBUTES时有效,一般不用此参数;

psfi 参数:返回获得的文件信息,是一个记录类型,有以下字段:
  _SHFILEINFOA = record
    hIcon: HICON;                      { out: icon }  //文件的图标句柄
    iIcon: Integer;                    { out: icon index }     //图标的系统索引号
    dwAttributes: DWORD;               { out: SFGAO_ flags }    //文件的属性值
    szDisplayName: array [0..MAX_PATH-1] of  AnsiChar; { out: display name (or path) }  //文件的显示名
    szTypeName: array [0..79] of AnsiChar;             { out: type name }      //文件的类型名
  end;

cbFileInfo 参数:psfi的比特值;

uFlags 参数:指明需要返回的文件信息标识符,常用的有以下常数:
    SHGFI_ICON;           //获得图标
    SHGFI_DISPLAYNAME;    //获得显示名
    SHGFI_TYPENAME;       //获得类型名
    SHGFI_ATTRIBUTES;     //获得属性
    SHGFI_LARGEICON;      //获得大图标
    SHGFI_SMALLICON;      //获得小图标
    SHGFI_PIDL;           // pszPath是一个标识符
函数SHGetFileInfo()的返回值也随uFlags的取值变化而有所不同。

可见通过调用SHGetFileInfo()可以由psfi参数得到文件的图标句柄。但要注意在uFlags参数中不使用SHGFI_PIDL时,SHGetFileInfo()不能获得“我的电脑”等虚似文件夹的信息。
应该注意的是，在调用SHGetFileInfo()之前，必须使用 CoInitialize 或者OleInitialize 初始化COM,否则表面上能够使用，但是会造成不安全或者丧失部分功能。例如，一个常见的例子：如果不初始化COM,那么调用该函数就无法得到.htm/.mht/.xml文件的图标。
以下是两个例子：

1.获得系统图标列表：
//取得系统图标列表
uses
 ShellAPI
var
  ImageListHandle : THandle;
  FileInfo: TSHFileInfo;
//小图标 
ImageListHandle := SHGetFileInfo('C:\',
                           0,
                           FileInfo,
                           SizeOf(FileInfo),
                           SHGFI_SYSICONINDEX or SHGFI_SMALLICON);
//把图标列表同一个名叫ListView1的ListView控件的小图标关联。                           
SendMessage(ListView1.Handle, LVM_SETIMAGELIST, LVSIL_SMALL, ImageListHandle);  
//大图标    
ImageListHandle := SHGetFileInfo('C:\',
                           0,
                           FileInfo,
                           SizeOf(FileInfo),
                           SHGFI_SYSICONINDEX or SHGFI_LARGEICON);
//把图标列表同一个名叫ListView1的ListView控件的大图标关联。                           
SendMessage(ListView1.Handle, LVM_SETIMAGELIST, LVSIL_NORMAL, ImageListHandle); 

2.获得一个文件的显示名和图标
var
  sfi: TSHFileInfo;
 IconIndex : Integer;
//取图标的索引号等信息
SHGetFileInfo(PAnsiChar(FileName),
                0,
                sfi,
                sizeof(TSHFileInfo),
                ShellAPI.SHGFI_DISPLAYNAME or ShellAPI.SHGFI_TYPENAME or ShellAPI.SHGFI_LARGEICON or ShellAPI.SHGFI_ICON);
//显示名和图标在系统图标列表中的编号就分别在sfi.szDisplayName和sfi.iIcon中 

*/
        #endregion

        #region API
        /// <summary>
        /// 保存文件信息的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pszPath"></param>
        /// <param name="dwFileAttributes"></param>
        /// <param name="psfi"></param>
        /// <param name="cbFileInfo"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("Shell32.dll", EntryPoint = "SHGetFileInfo", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [DllImport("User32.dll", EntryPoint = "DestroyIcon")]
        static extern int DestroyIcon(IntPtr hIcon);

        #region API 参数的常量定义
        /// <summary>
        /// 图标
        /// </summary>
        const uint SHGFI_ICON = 0x100;
        /// <summary>
        /// 大图标
        /// </summary>
        const uint SHGFI_LARGEICON = 0x0; //大图标 32×32
        /// <summary>
        /// 小图标
        /// </summary>
        const uint SHGFI_SMALLICON = 0x1; //小图标 16×16

        const uint SHGFI_USEFILEATTRIBUTES = 0x10;


        #endregion
        #endregion

        #region 函数

        /// <summary>
        /// 获取文件类型的关联图标
        /// </summary>
        /// <param name="fileName">文件类型的扩展名或文件的绝对路径</param>
        /// <param name="isLargeIcon">是否返回大图标</param>
        /// <returns>获取到的图标</returns>
        public static Icon GetIcon(string fileName, bool isLargeIcon)
        {
            SHFILEINFO shfi = new SHFILEINFO();
            IntPtr hI;

            if (isLargeIcon)
                hI = SHGetFileInfo(fileName, 0, ref shfi, (uint)Marshal.SizeOf(shfi), SHGFI_ICON | SHGFI_USEFILEATTRIBUTES | SHGFI_LARGEICON);
            else
                hI = SHGetFileInfo(fileName, 0, ref shfi, (uint)Marshal.SizeOf(shfi), SHGFI_ICON | SHGFI_USEFILEATTRIBUTES | SHGFI_SMALLICON);


            Icon icon = Icon.FromHandle(shfi.hIcon).Clone() as Icon;

            DestroyIcon(shfi.hIcon); //释放资源
            return icon;
        }
        /// <summary>
        /// 获取文件图标 
        /// </summary>
        /// <param name="p_Path">文件全路径</param>
        /// <returns>图标</returns>
        public static Icon GetFileIcon(string p_Path)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(p_Path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI_ICON | SHGFI_LARGEICON | SHGFI_USEFILEATTRIBUTES));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        /// <summary>
        /// 获取文件夹图标  
        /// </summary>
        /// <returns>图标</returns>
        public static Icon GetDirectoryIcon()
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI_ICON | SHGFI_LARGEICON));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        /// <summary>
        /// 获取文件类型的关联图标
        /// </summary>
        /// <param name="filename">文件类型的扩展名或文件的绝对路径</param>
        /// <param name="isLargeIcon">是否返回大图标</param>
        /// <returns>获取到的图标</returns>
        public static Bitmap GetBitmap(string filename, bool isLargeIcon)
        {
            return GetIcon(filename, isLargeIcon).ToBitmap();
        }

        /// <summary>
        /// 获取文件类型的关联图标
        /// </summary>
        /// <param name="filename">文件类型的扩展名或文件的绝对路径</param>
        /// <param name="isLargeIcon">是否返回大图标</param>
        /// <returns>获取到的图标</returns>
        public static Bitmap[] GetBitmaps(string[] filename, bool isLargeIcon)
        {
            Bitmap[] bit = new Bitmap[filename.Length];
            int i = 0;
            foreach (string str in filename)
            {
                bit[i] = GetBitmap(str, isLargeIcon);
                i++;
            }
            return bit;
        }

        /// <summary>
        /// 使用Alpha效果处理，使用图片更出色
        /// </summary>
        /// <param name="bmSource"></param>
        /// <returns></returns>
        public static Bitmap FixAlphaBitmap(Bitmap bmSource)
        {
            System.Drawing.Imaging.BitmapData bmData;
            Rectangle bmBounds = new Rectangle(0, 0, bmSource.Width, bmSource.Height);
            bmData = bmSource.LockBits(bmBounds, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmSource.PixelFormat);
            Bitmap dstBitmap = new Bitmap(bmData.Width, bmData.Height, bmData.Stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, bmData.Scan0);
            bmSource.UnlockBits(bmData);
            return dstBitmap;
        }

        #endregion

    }
}
