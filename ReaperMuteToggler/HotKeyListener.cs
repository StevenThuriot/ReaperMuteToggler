using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ReaperMuteToggler
{
    class HotKeyListener : IDisposable
    {
        public int Modifier { get; }
        public int Key { get; }
        public int Id { get; }

        readonly IWin32Window _window;
        bool registered;

        public HotKeyListener(Modifiers modifier, Keys key, IWin32Window window)
        {
            Modifier = (int) modifier;
            Key = (int) key;

            _window = window;

            Id = Modifier ^ Key ^ _window.Handle.ToInt32();
            Register();
        }

        public void Register()
        {
            if (!NativeMethods.RegisterHotKey(_window.Handle, Id, Modifier, Key))
            {
                int error = Marshal.GetLastWin32Error();
                if (error != 0 && error != 1419)
                {
                    var inner = new Win32Exception(error);
                    throw new Exception("Hotkey failed to register.", inner);
                }
            }

            registered = true;
        }

        public void Unregister()
        {
            if (registered)
            {
                registered = false;

                if (!NativeMethods.UnregisterHotKey(_window.Handle, Id))
                {
                    int error = Marshal.GetLastWin32Error();
                    if (error != 0 && error != 1419)
                    {
                        var inner = new Win32Exception(error);
                        throw new Exception("Hotkey failed to unregister.", inner);
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~HotKeyListener()
        {
            Dispose(false);
        }

        void Dispose(bool disposing)
        {
            Unregister();
        }
    }
}
