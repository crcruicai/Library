using System;
using System.Windows.Forms;

namespace CRC.Controls
{
    internal sealed class NoneExcludedImageIndexConverter : ImageIndexConverter
    {
        protected override bool IncludeNoneAsStandardValue
        {
            get
            {
                return false;
            }
        }
    }
}
