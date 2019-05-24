using System;
using MetroFramework;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace ROCO
{
    class MyMessageBox
    {
        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message)
        { return Show(owner, message, "Notification", 211); }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, int height, int width = 0)
        { return Show(owner, message, "Notification", height, width); }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, String title)
        { return Show(owner, message, title, MessageBoxButtons.OK, 211); }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, String title, int height, int width = 0)
        { return Show(owner, message, title, MessageBoxButtons.OK, height, width); }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, String title, MessageBoxButtons buttons)
        { return Show(owner, message, title, buttons, MessageBoxIcon.None, 211); }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, String title, MessageBoxButtons buttons, int height, int width = 0)
        { return Show(owner, message, title, buttons, MessageBoxIcon.None, height, width); }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon)
        { return Show(owner, message, title, buttons, icon, MessageBoxDefaultButton.Button1, 211); }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, int height, int width = 0)
        { return Show(owner, message, title, buttons, icon, MessageBoxDefaultButton.Button1, height, width); }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultbutton"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
        {
            return Show(owner, message, title, buttons, icon, defaultbutton, 211);
        }

        /// <summary>
        /// Shows a metro-styles message notification into the specified owner window.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultbutton"></param>
        /// <param name="height" optional=211></param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton, int height, int width = 0)
        {
            DialogResult _result = DialogResult.None;
            
            if (owner != null)
            {
                Form _owner = (owner as Form == null) ? ((UserControl)owner).ParentForm : (Form)owner;
                
                switch (icon)
                {
                    case MessageBoxIcon.Error:
                        SystemSounds.Hand.Play(); break;
                    case MessageBoxIcon.Exclamation:
                        SystemSounds.Exclamation.Play(); break;
                    case MessageBoxIcon.Question:
                        SystemSounds.Beep.Play(); break;
                    default:
                        SystemSounds.Asterisk.Play(); break;
                }

                MetroMessageBoxControl _control = new MetroMessageBoxControl();
                _control.BackColor = _owner.BackColor;
                _control.Properties.Buttons = buttons;
                _control.Properties.DefaultButton = defaultbutton;
                _control.Properties.Icon = icon;
                _control.Properties.Message = message;
                _control.Properties.Title = title;
                _control.Padding = new Padding(0, 0, 0, 0);

                _control.FormBorderStyle = FormBorderStyle.FixedSingle;//Sizable
                _control.Font = new Font("Arial",12f);//

                _control.ControlBox = false;
                _control.ShowInTaskbar = false;
                _control.TopMost = true;
                _control.Size = new Size(width == 0 ? _owner.Size.Width : width, height);
                _control.Location = new Point(_owner.Location.X + (_owner.Width - _control.Width) / 2, _owner.Location.Y + (_owner.Height - _control.Height) / 2);
                _control.ArrangeApperance();
                int _overlaySizes = Convert.ToInt32(Math.Floor(_control.Size.Height * 0.28));

                _control.ShowDialog();
                _control.BringToFront();
                _control.SetDefaultButton();

                Action<MetroMessageBoxControl> _delegate = new Action<MetroMessageBoxControl>(ModalState);
                IAsyncResult _asyncresult = _delegate.BeginInvoke(_control, null, _delegate);
                bool _cancelled = false;

                try
                {
                    while (!_asyncresult.IsCompleted)
                    { Thread.Sleep(1); Application.DoEvents(); }
                }
                catch
                {
                    _cancelled = true;

                    if (!_asyncresult.IsCompleted)
                    {
                        try { _asyncresult = null; }
                        catch { }
                    }

                    _delegate = null;
                }

                if (!_cancelled)
                {
                    _result = _control.Result;
                    _control.Dispose(); _control = null;
                }

            }

            return _result;
        }

        private static void ModalState(MetroMessageBoxControl control)
        {
            while (control.Visible)
            { }
        }
    }
}
