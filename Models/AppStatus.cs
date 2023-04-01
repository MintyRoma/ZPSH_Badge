using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPSH_Badge.Models
{
    enum GlobalMenu
    {
        UsersView,
        TemplateSetting,
        DataLoad,
        Export
    }
    internal static class AppStatus
    {
        private static GlobalMenu status;

        public static GlobalMenu Status
        {
            get
            {
                return status;
            }
            set
            {
                StatusChangedEvent?.Invoke();
                status = value;
            }
        }

        internal delegate void StatusHandler();
        public static event StatusHandler StatusChangedEvent;
    }
}
