using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TkwEF.Helper
{
    public static class FormAsyncEx
    {
        public static async Task<DialogResult> ShowDialogAsync(this Form @this, Form parent = null, CancellationToken token = default(CancellationToken))
        {
            await Task.Yield();
            if (@this.IsDisposed)
                return DialogResult.OK;
            using (token.Register(() => @this.Close(), useSynchronizationContext: true))
            {
                return @this.ShowDialog(parent);
            }
        }
    }
    public static class ControlEx
    {
        /// <summary>
        /// Ожидание нажатия кнопки
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static Task WhenClicked(this Button button)
        {
            var tcs = new TaskCompletionSource<object>();
            EventHandler onClick = null;
            onClick = (sender, e) =>
            {
                button.Click -= onClick;
                tcs.TrySetResult(null);
            };
            button.Click += onClick;
            return tcs.Task;
        }
    }
}
