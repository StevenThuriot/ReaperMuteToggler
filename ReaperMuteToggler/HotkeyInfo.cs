using System.Windows.Forms;

namespace ReaperMuteToggler
{
    static class HotkeyInfo
    {
        public static bool TryGetFromMessage(Message m, out (int key, int modifiers) output)
        {
            if (m.Msg == 0x0312)
            {
                var lpInt = (int)m.LParam;
                var Key = ((lpInt >> 16) & 0xFFFF);
                var Modifiers = (lpInt & 0xFFFF);

                output = (Key, Modifiers);
                return true;
            }
            else
            {
                output = default;
                return false;
            }
        }
    }
}