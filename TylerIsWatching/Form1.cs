using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TylerIsWatching
{
    public partial class Form1 : Form
    {
        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        #region dll imports
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();
        #endregion

        public Form1()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();

            BackColor = Color.DarkBlue;
            TransparencyKey = Color.DarkBlue;

            img.Top = 0;
            img.Left = 0;

            img.Width = img.Image.Width;
            img.Height = img.Image.Height;

            Width = img.Width;
            Height = img.Height;

#if DEBUG
            AllocConsole();
#endif
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var bounds = new Rect();
            GetWindowRect(GetForegroundWindow(), ref bounds);

            Top = bounds.Top - Height;
            Left = bounds.Right - Width - 50;

#if DEBUG
            Console.WriteLine($"T: {bounds.Top}, B: {bounds.Bottom}, L: {bounds.Left}, R: {bounds.Right}");
#endif

            TopMost = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // e.Graphics.FillRectangle(Brushes.Transparent, e.ClipRectangle);
        }
    }
}
