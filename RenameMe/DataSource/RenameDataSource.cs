using System;
using System.Collections.Generic;
using Corel.Interop.VGCore;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Interop;
using System.Diagnostics;

namespace RenameMe.DataSource
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class RenameDataSource : BaseDataSource
    {
        public static IntPtr corelHandle;
        
        public RenameDataSource(DataSourceProxy proxy) : base(proxy)
        {
            
        }

     

        public void CallRenameBox()
        {
            DockerUI ui = new DockerUI();
            ui.Closed += Ui_Closed;
            IntPtr ownerWindowHandler = GetFocus();
            corelHandle = ownerWindowHandler;
            WindowInteropHelper helper = new WindowInteropHelper(ui);
            helper.Owner = ownerWindowHandler;
            ui.Show();

        }

        private void Ui_Closed(object sender, EventArgs e)
        {
            if ((sender as DockerUI).Renamed)
            {
                string name = (sender as DockerUI).ShapeName;
                RenameShape(name);
            }
            SetFocus(corelHandle);
        }

        private void RenameShape(string name)
        {
            RenameWarpercs.corelApp.ActiveShape.Name = name;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetFocus();
        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(IntPtr intPtr);
        
      
    }

}
