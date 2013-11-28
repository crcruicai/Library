using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CRC.Froms
{
    /// <summary>
    /// 
    /// </summary>
    public enum KeyState { Up = 0, Down = 1 }
    /// <summary>
    /// 
    /// </summary>
    public enum KeyValue { Untoggled = 0, Toggled = 1 }

    /// <summary>
    /// 
    /// </summary>
    internal static class KeyStateCheck
    {
        //GetKeyState Win32 API declaration
        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        private static short _KeyDown = Convert.ToInt16("1000000000000000", 2); //High-Order bit set (KeyDown)
        private static short _KeyUp = Convert.ToInt16("0000000000000000", 2); //High-Order bit not set (Key Up)
        private static short _KeyToggled = Convert.ToInt16("0000000000000001", 2); //Low-Order bit set (Key Toggled)
        private static short _KeyUnToggled = Convert.ToInt16("0000000000000000", 2); // Low-Order bit not set (Key Untoggled)

        // Get if Key is Up or Down
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualKey"></param>
        /// <returns></returns>
        public static KeyState GetKeyState(Keys virtualKey)
        {
            short keyState = GetKeyState((int)virtualKey);
            KeyState state;

            // Bitwise AND to get wether the key is Down
            if ((keyState & _KeyDown) == _KeyDown)
                state = KeyState.Down;
            else
                state = KeyState.Up;

            return state;
        }

        // Get if key is toggled or untgled (useful to detect if capslock or nunlock is on)
        public static KeyValue GetToggled(Keys virtualKey)
        {
            short keyState = GetKeyState((int)virtualKey);
            KeyValue value;

            // Bitwise AND to get wether the key is toggled
            if ((keyState & _KeyToggled) == _KeyToggled)
                value = KeyValue.Toggled;
            else
                value = KeyValue.Untoggled;

            return value;
        }
    }
}
