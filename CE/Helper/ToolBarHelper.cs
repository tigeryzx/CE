using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CE.Helper
{
    /// <summary>
    /// 工具栏帮助类
    /// </summary>
    public class ToolBarHelper
    {
        [DllImport("user32.dll")]
        public static extern
            Int32 GetWindowLong(IntPtr hwnd, Int32 index);
        [DllImport("user32.dll")]
        public static extern
            Int32 SetWindowLong(IntPtr hwnd, Int32 index, Int32 newValue);
        public const int GWL_EXSTYLE = (-20);
        public static void AddWindowExStyle(IntPtr hwnd, Int32 val)
        {
            int oldValue = GetWindowLong(hwnd, GWL_EXSTYLE);
            if (oldValue == 0)
            {
                throw new System.ComponentModel.Win32Exception();
            }
            if (0 == SetWindowLong(hwnd, GWL_EXSTYLE, oldValue | val))
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }
        public static int WS_EX_TOOLWINDOW = 0x00000080;

        public static void SetFormToolWindowStyle(System.Windows.Forms.Form form)
        {
            AddWindowExStyle(form.Handle, WS_EX_TOOLWINDOW);
        }
    }
}
