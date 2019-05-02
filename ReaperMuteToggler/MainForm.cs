using ReaperMuteToggler.Properties;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaperMuteToggler
{
    public partial class MainForm : Form
    {
        NotifyIcon _trayIcon;
        HttpClient _httpClient;

        HotKeyListener _listener;
        Keys _key;
        Modifiers _modifier;

        readonly Uri _muteToggle;
        readonly string _endPoint;

        public MainForm()
        {
            InitializeComponent();

            _muteToggle = new Uri(ConfigurationManager.AppSettings["MuteToggle"] ?? throw new ArgumentNullException("MuteToggle"));

            if (!Enum.TryParse<Keys>(ConfigurationManager.AppSettings["Key"], out _key))
                _key = Keys.F14;

            if (!Enum.TryParse<Modifiers>(ConfigurationManager.AppSettings["Modifier"], out _modifier))
                _modifier = Modifiers.NoMod;

            _endPoint = _muteToggle.AbsolutePath;
            var basePath = _muteToggle.GetLeftPart(UriPartial.Authority);
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(basePath)
            };
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            PrintCurrentKeys();

            _trayIcon = new NotifyIcon()
            {
                Icon = Resources.keyboard_off,
                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem("Show/Hide", ToggleShow),
                    new MenuItem("Exit", Exit),
                }),
                Visible = true
            };

            _trayIcon.DoubleClick += ToggleShow;

            void Listen(object sender, EventArgs activationE)
            {
                Activated -= Listen;
                ToggleShow(null, null);
            }

            Activated += Listen;
        }

        void PrintCurrentKeys()
        {
            var text = _key.ToString();
            if (_modifier != Modifiers.NoMod)
                text = _modifier + " - " + text;

            shortcutTextbox.Text = text;
        }

        void ToggleShow(object sender, EventArgs e)
        {
            _listener?.Dispose();

            if (ShowInTaskbar)
            {
                Hide();
                ShowInTaskbar = false;
            }
            else
            {
                Show();
                ShowInTaskbar = true;
            }

            _listener = new HotKeyListener(_modifier, _key, this);
        }

        void Exit(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            _httpClient.Dispose();

            _listener?.Dispose();

            Application.Exit();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            ToggleShow(null, null);
        }

        protected override void WndProc(ref Message m)
        {
            var hotkeyInfo = HotkeyInfo.GetFromMessage(m);
            if (hotkeyInfo != null) HotkeyProc(hotkeyInfo);
            base.WndProc(ref m);
        }

        async void HotkeyProc(HotkeyInfo hotkeyInfo)
        {
            if (hotkeyInfo.Key != _key || _modifier != hotkeyInfo.Modifiers)
                return;

            var sw = Stopwatch.StartNew();

            _trayIcon.Icon = Resources.keyboard_on;

            using (var msg = await _httpClient.GetAsync(_endPoint))
            {
                _trayIcon.Icon = Resources.keyboard_off;
                Debug.WriteLine($"{(int)msg.StatusCode} {msg.StatusCode} - {sw.ElapsedMilliseconds}ms");
            }
        }

        void ShortcutTextbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode < Keys.A || e.KeyCode > Keys.Z) && (e.KeyCode < Keys.F1 || e.KeyCode > Keys.F24))
                return;

            _key = e.KeyCode;
            if (!Enum.TryParse(e.Modifiers.ToString(), out _modifier))
                _modifier = Modifiers.NoMod;

            PrintCurrentKeys();
            SetButtonColor();
        }

        void SetButtonColor()
        {
            if (_listener.Key == (int)_key && _listener.Modifier == (int)_modifier)
            {
                SetButton.BackColor = System.Drawing.SystemColors.Control;
                SetButton.ForeColor = System.Drawing.SystemColors.ControlText;
            }
            else
            {
                SetButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
                SetButton.ForeColor = System.Drawing.SystemColors.Info;
            }
        }

        void SetButton_Click(object sender, EventArgs e)
        {
            _listener.Dispose();
            _listener = new HotKeyListener(_modifier, _key, this);

            SetButtonColor();
        }
    }
}
