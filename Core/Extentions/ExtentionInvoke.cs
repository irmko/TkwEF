using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TkwEF.Core.Extentions
{
    #region ControlExtentions

    /// <summary>
    /// Расширения облегчающие работу с элементами управления в многопоточной среде.
    /// </summary>
    public static class ControlExtentionsCore
    {
        //20171213

        /// <summary>
        /// Вызов делегата через control.Invoke, если это необходимо.
        /// </summary>
        /// <param name="control">Элемент управления</param>
        /// <param name="doit">Делегат с некоторым действием</param>
        //20171213
        //public static void InvokeIfNeeded(this Control control, MethodInvoker doit)
        //{
        //    try
        //    {
        //        //20170905
        //        //if (control == null || control.IsDisposed)//20170330 || !control.IsHandleCreated
        //        //if (control == null || control.IsDisposed || (control is Form && !control.IsHandleCreated))
        //        //    return;
        //        if (!control.IsHandleCreated && !control.IsDisposed) return; // Взял из Microsoft
        //        if (control.InvokeRequired)
        //            control.Invoke(doit);
        //        else
        //            doit();
        //    }
        //    //catch (BLL.BIZ.RdbsBllException ex) { _sendMail(control.ToString(), doit.ToString(), ex.GetAllInnerMessage()); }
        //    //catch (DAL.DataAccess.RdbsSqlException ex) { _sendMail(control.ToString(), doit.ToString(), ex.GetAllInnerMessage()); }
        //    catch (ObjectDisposedException) { throw; }
        //    catch (InvalidOperationException) { throw; }
        //    catch (Exception) { throw; }
        //}

        /// <summary>
        /// Вызов делегата через control.Invoke, если это необходимо.
        /// </summary>
        /// <param name="control">Элемент управления</param>
        /// <param name="doit">Делегат с некоторым действием</param>
        public static void InvokeIfNeededCore(this Control control, Action doit)
        {
            try
            {
                //================ 20180709 ===========
                if (control == null || (!control.InvokeRequired && !(control.Disposing || control.IsDisposed)))
                    doit();
                else
                {
                    if ((control is Form && ((Form)control).MdiParent == null && !control.IsHandleCreated)
                        || (control.Disposing || control.IsDisposed))
                        return;
                    else
                        control.Invoke(doit);
                }
                //if (control == null 
                //    || (control is Form && ((Form)control).MdiParent == null && !control.IsHandleCreated)
                //    || (control.Disposing || control.IsDisposed)) return;
                //if (control.InvokeRequired)
                //    control.Invoke(doit);
                //else
                //    doit();
                //======================================
            }
            catch (ObjectDisposedException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (Exception) { throw; }
        }
        /// <summary>
        /// Вызов делегата через control.Invoke, если это необходимо.
        /// </summary>
        /// <typeparam name="T">Тип параметра делегата</typeparam>
        /// <param name="control">Элемент управления</param>
        /// <param name="doit">Делегат с некоторым действием</param>
        /// <param name="arg">Аргумент делагата с действием</param>
        public static void InvokeIfNeededCore<T>(this Control control, Action<T> doit, T arg)
        {
            try
            {                
                //================ 20180709 ===========
                if (control == null || (!control.InvokeRequired && !(control.Disposing || control.IsDisposed)))
                    doit(arg);
                else
                {
                    if ((control is Form && ((Form)control).MdiParent == null && !control.IsHandleCreated)
                        || (control.Disposing || control.IsDisposed))
                        return;
                    else
                        control.Invoke(doit, arg);
                }
                //if ((control is Form && ((Form)control).MdiParent == null && !control.IsHandleCreated)
                //    || (control.Disposing || control.IsDisposed)) return;
                //if (control.InvokeRequired)
                //    control.Invoke(doit, arg);
                //else
                //    doit(arg);
                //======================================
            }
            catch (ObjectDisposedException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (Exception) { throw; }
        }
        //С помощью этого вспомогательного класса, реализация метода, взаимодействующего с элементами управления из других потоков, будет следующей:
        //private void AsyncHandler(object data)
        //{
        //    tickCount++;
        //    this.InvokeIfNeeded(
        //        () => textBox1.Text = tickCount.ToString()
        //            );
        //}

        #region Другой вариант

        public static void InvokeExCore<T>(this T @this, Action<T> action)
          where T : Control
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                if (!@this.IsHandleCreated)
                    return;
                if (@this.IsDisposed)
                    throw new ObjectDisposedException("@this is disposed.");

                action(@this);
            }
        }

        public static IAsyncResult BeginInvokeExCore<T>(this T @this, Action<T> action)
          where T : Control
        {
            return @this.BeginInvoke((Action)delegate { @this.InvokeExCore(action); });
        }

        public static void EndInvokeExCore<T>(this T @this, IAsyncResult result)
          where T : Control
        {
            @this.EndInvoke(result);
        }

        #endregion
    }

    #endregion
}
