using System;
using System.Windows.Forms;

namespace ReaperMuteToggler
{
    class HotkeyInfo
    {
        public Keys Key { get; }
        public Modifiers Modifiers { get; }

        private HotkeyInfo(IntPtr lParam)
        {
            var lpInt = (int)lParam;
            Key = (Keys)((lpInt >> 16) & 0xFFFF);
            Modifiers = (Modifiers)(lpInt & 0xFFFF);
        }

        public static HotkeyInfo GetFromMessage(Message m)
        {
            return !IsHotkeyMessage(m) ? null : new HotkeyInfo(m.LParam);
        }

        const int WM_HOTKEY_MSG_ID = 0x0312;
        public static bool IsHotkeyMessage(Message m)
        {
            return m.Msg == WM_HOTKEY_MSG_ID;
        }
    }
}