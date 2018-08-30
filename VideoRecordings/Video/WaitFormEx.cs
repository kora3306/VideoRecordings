using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;

namespace SeemmoData.Controls
{
    public partial class WaitFormEx : WaitForm
    {
        private ManualResetEventSlim _manualResetEventSlim = new ManualResetEventSlim(false);

        public WaitFormEx()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }

        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum WaitFormCommand
        {
        }

        public static void Run(Action action, string caption = null, string description = null)
        {
            var form = new WaitFormEx();
            if (caption != null)
                form.SetCaption(caption);
            if (description != null)
                form.SetDescription(description);
            Task.Run(() =>
            {
                try
                {
                    action?.Invoke();
                }
                finally 
                {
                    form._manualResetEventSlim.Wait();
                    form.DialogResult = DialogResult.OK;
                }
            });
            form.ShowDialog();
            form.Dispose();
        }

        private void WaitForm1_Load(object sender, EventArgs e)
        {
            _manualResetEventSlim.Set();
        }

        private static object _lock = new object();
        private static int _opentimes = 0;

        private static Dictionary<Form, FormWaitFormContext> _formcontexts =
            new Dictionary<Form, FormWaitFormContext>();

        public static bool TryShowWaitForm(Form parentform, int pandingtime = 200, bool lockparent = false)
        {
            if (parentform == null)
                return false;
            lock (_lock)
            {
                if (_formcontexts.ContainsKey(parentform))
                {
                    _formcontexts[parentform]._showtimes++;
                }
                else
                {
                    _formcontexts[parentform] = new FormWaitFormContext(
                        new SplashScreenManager(typeof(WaitFormEx),
                            SplashFormStartPosition.Default, Point.Empty,
                            new SplashFormProperties(parentform, true, true, pandingtime),
                            lockparent ? ParentFormState.Locked : ParentFormState.Unlocked));
                }

                if (_formcontexts[parentform]._showtimes > 1)
                    return false;
                _formcontexts[parentform]._manager.ShowWaitForm();
                return true;
            }
        }

        public static bool TryCloseWaitForm(Form parentform)
        {
            lock (_lock)
            {
                if (!_formcontexts.ContainsKey(parentform))
                    return false;
                _formcontexts[parentform]._showtimes--;
                if (_formcontexts[parentform]._showtimes != 0)
                    return false;
                _formcontexts[parentform]._manager.CloseWaitForm();
                _formcontexts[parentform]._manualResetEventSlim.Set();
                _formcontexts[parentform].Dispose();
                _formcontexts.Remove(parentform);
                return true;
            }
        }

        public static bool IsWaitFormShown(Form form)
        {
            lock (_lock)
            {
                return _formcontexts.ContainsKey(form);
            }
        }

        public static async Task TryShowAndCloseForm(Form parentform, Task task, int pandingtime = 200, bool lockparent = true)
        {
            TryShowWaitForm(parentform, pandingtime, lockparent);
            try
            {
                await task;
            }
            finally
            {
                TryCloseWaitForm(parentform);
            }
        }

        public static void WaitForWaitFormClose(Form form)
        {
            ManualResetEventSlim slim;
            lock (_lock)
            {
                if (!_formcontexts.ContainsKey(form))
                    return;
                slim=_formcontexts[form]._manualResetEventSlim;
            }

            slim.Wait();
        }
    }

    class FormWaitFormContext:IDisposable
    {
        internal int _showtimes = 1;
        internal SplashScreenManager _manager;
        internal ManualResetEventSlim _manualResetEventSlim = new ManualResetEventSlim(false);
        
        public FormWaitFormContext(SplashScreenManager manager)
        {
            _manager = manager;
        }

        public void Dispose()
        {
            _manager?.Dispose();
            _manualResetEventSlim?.Dispose();
        }
    }
}