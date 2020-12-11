using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenameMe.DataSource;
using Corel.Interop.VGCore;
//using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace RenameMe
{
    public class RenameWarpercs : System.Windows.Controls.UserControl
    {
        public static Application corelApp;
        public static Point P { get; private set; }
        public RenameWarpercs(object app)
        {
            try
            {
                RenameWarpercs.corelApp = app as Application;
                RenameWarpercs.corelApp.OnApplicationEvent += CorelApp_OnApplicationEvent;
                
                var dsf = new DataSourceFactory();
                dsf.AddDataSource("RenameDataSource", typeof(RenameDataSource));
                dsf.Register();
            }
            catch
            {
                global::System.Windows.MessageBox.Show("VGCore Erro");
            }

        }
        private void CorelApp_OnApplicationEvent(string EventName, ref object[] Parameters)
        {
            if (EventName == "FrameworkManagerToolbarListChanged")
            {
                Point p = new Point(0, 0);
                GetCursorPos(out p);
                Debug.WriteLine(p.X);
                if (RenameWarpercs.corelApp.ActiveShape == null)
                    return;
                Rect rect = RenameWarpercs.corelApp.ActiveShape.BoundingBox;
                double xDoc = 0;
                double yDoc = 0;
                RenameWarpercs.corelApp.ActiveWindow.ScreenToDocument(p.X, p.Y, out xDoc, out yDoc);
                if (rect.IsPointInside(xDoc, yDoc))
                    P = p;
                //int x = 0;
                //int y = 0;
                //int w = 0;
                //int h = 0;
                //// RenameWarpercs.corelApp.FrameWork.Automation.GetItemScreenRect("1056b8d8-9185-46ee-af8e-77c7ba383a4e", "1056b8d8-9185-46ee-af8e-77c7ba383a4e", out x, out y, out w, out h);
                //RenameWarpercs.corelApp.FrameWork.Automation.GetActiveMenuItemScreenRect(1, out x, out y, out w, out h);
                //Debug.WriteLine(x);


            }
        }
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point pVal);
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X, Y;
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
