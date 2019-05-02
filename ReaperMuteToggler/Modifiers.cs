using System;

namespace ReaperMuteToggler
{
    [Flags]
    enum Modifiers
    {
        NoMod = 0x0000,

        Alt = 0x0001,
        Menu = 0x0001,
        LMenu = 0x0001,
        RMenu = 0x0001,

        Ctrl = 0x0002,
        Control = 0x0002,
        LControlKey = 0x0002,
        RControlKey = 0x0002,

        Shift = 0x0004,
        LShiftKey = 0x0004,
        RShiftKey = 0x0004,

        //Win = 0x0008
    }
}
