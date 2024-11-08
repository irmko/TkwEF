using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TkwEF.Core.Extentions;

namespace TkwEF.UI
{

    public class UiException : Exception
    {
        private readonly bool _sendMail = true;
        public UiException()
        {
        }
        public UiException(string message) : base(message)
        {
        }
        public UiException(string message, bool sendMail) : this(message)
        {
            _sendMail = sendMail;
        }
        public UiException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public UiException(string message, Exception innerException, bool sendMail) : this(message, innerException)
        {
            _sendMail = sendMail;
        }
        protected UiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        protected UiException(SerializationInfo info, StreamingContext context, bool sendMail) : base(info, context)
        {
            _sendMail = sendMail;
        }

        public void ShowMessage(Form form)
        {
            _sendMailToRazrabAsync();
            if (form?.IsDisposed ?? false)
                form = null;
            MessageBox.Show(form, this.Message, "Ошибка программы", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private Task _sendMailToRazrabAsync()
        {
            if (_sendMail)
                return new Helper.TkwMail().SendMailFromRobotToRazrabAsync("Ошибка TKW", this);
            else
                return new Task(null);
        }
    }
}
