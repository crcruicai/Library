/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 10:25:30
 * 描述说明：系统版本代理类.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC.Util
{
    /// <summary>
    /// OS版本代理类.
    /// </summary>
    public class OSVersionUtil
    {
        /// <summary>
        /// 检查OS版本是否为Windows XP系统
        /// </summary>
        /// <returns></returns>
        public static bool IsWinXP()
        {

            OperatingSystem OS = Environment.OSVersion;
            return (OS.Platform == PlatformID.Win32NT) &&
                ((OS.Version.Major > 5) || ((OS.Version.Major == 5) && (OS.Version.Minor == 1)));
        }

        /// <summary>
        /// 检查OS版本是否为Windows Vista系统.
        /// </summary>
        /// <returns></returns>
        public static bool IsWinVista()
        {

            OperatingSystem OS = Environment.OSVersion;
            return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);

        }
    }
}
