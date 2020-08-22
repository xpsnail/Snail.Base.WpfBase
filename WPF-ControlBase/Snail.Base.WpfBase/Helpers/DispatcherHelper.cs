using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Snail.Base.WpfBase.Helpers
{
    public static class DispatcherHelper
    {
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void UpdateUI()
        {
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback((agr) =>
                {
                    ((DispatcherFrame)agr).Continue = false;
                    return null;
                }), frame);
            try
            {
                Dispatcher.PushFrame(frame);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
