#region Copyright (c) 2002-2006 EConTech JSC., All Rights Reserved
/* ---------------------------------------------------------------------*
*                           EConTech JSC.,                              *
*              Copyright (c) 2002-2006 All Rights reserved              *
*                                                                       *
*                                                                       *
* This file and its contents are protected by Vietnam and               *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF ECONTECH JSC.,     *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME, OR  *
* SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY ECONTECH JSC. PRODUCT.   *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF ECONTECH JSC.,     *
* THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO             *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/
#endregion Copyright (c) 2002-2006 EConTech JSC., All Rights Reserved

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CRC.Controls
{
	/// <summary>
	/// Summary description for ColorHelper.
	/// </summary>
	internal class ColorHelper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="red"></param>
		/// <param name="green"></param>
		/// <param name="blue"></param>
		/// <returns></returns>
		public static Color CreateColorFromRGB(int red, int green, int blue)
		{
            // make sure color value in 【0-255】 range
            int r = red > 255 ? 255 : red < 0 ? 0 : red;
            int g = green > 255 ? 255 : green < 0 ? 0 : green;       
            int b = blue > 255 ? 255 : blue < 0 ? 0 : blue;   
     
			return Color.FromArgb(r, g, b);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="blendColor"></param>
		/// <param name="baseColor"></param>
		/// <param name="opacity"></param>
		/// <returns></returns>
		public static Color OpacityMix(Color blendColor, Color baseColor, int opacity)
		{
            
            int r = (int)(((blendColor.R * ((float)opacity / 100)) + (baseColor.R * (1 - ((float)opacity / 100)))));
            int g = (int)(((blendColor.G * ((float)opacity / 100)) + (baseColor.G * (1 - ((float)opacity / 100)))));
            int b = (int)(((blendColor.B * ((float)opacity / 100)) + (baseColor.B * (1 - ((float)opacity / 100)))));
			return CreateColorFromRGB(r, g, b);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="baseColor"></param>
		/// <param name="blendColor"></param>
		/// <param name="opacity"></param>
		/// <returns></returns>
		public static Color SoftLightMix(Color baseColor, Color blendColor, int opacity)
		{		
            int r = SoftLightMath(baseColor.R, blendColor.R);
            int g = SoftLightMath(baseColor.G, blendColor.G);
            int b = SoftLightMath(baseColor.B, blendColor.B);
			return OpacityMix(CreateColorFromRGB(r, g, b), baseColor, opacity);
		}

		/// <summary>
		/// 计算叠加颜色.
		/// </summary>
		/// <param name="baseColor">基色</param>
		/// <param name="blendColor">混合颜色.</param>
		/// <param name="opacity">透明度</param>
		/// <returns></returns>
		public static Color OverlayMix(Color baseColor, Color blendColor, int opacity)
		{      
            int r = OverlayMath(baseColor.R, blendColor.R);
            int g = OverlayMath(baseColor.G, blendColor.G);
            int b = OverlayMath(baseColor.B, blendColor.B);
			return OpacityMix(CreateColorFromRGB(r, g, b), baseColor, opacity);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="ibase"></param>
		/// <param name="blend"></param>
		/// <returns></returns>
		private static int SoftLightMath(int ibase, int blend)
		{
            float dbase = (float)ibase / 255;
			float dblend= (float)blend / 255;
			if (dblend < 0.5) 
			{
				return (int)(((2 * dbase * dblend) + (Math.Pow(dbase, 2)) * (1 - (2 * dblend))) * 255);
			} 
			else 
			{
				return (int)(((Math.Sqrt(dbase) * (2 * dblend - 1)) + ((2 * dbase) * (1 - dblend))) * 255);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ibase"></param>
		/// <param name="blend"></param>
		/// <returns></returns>
		public static int OverlayMath(int ibase, int blend)
		{
            double dbase = (double)ibase / 255;
			double dblend= (double)blend / 255;
			if (dbase < 0.5) 
			{
				return (int)((2 * dbase * dblend) * 255);
			} 
			else 
			{
				return (int)((1 - (2 * (1 - dbase) * (1 - dblend))) * 255);
			}
		}

	}
}