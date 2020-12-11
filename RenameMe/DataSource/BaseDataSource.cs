using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Corel.Interop.VGCore;

namespace RenameMe.DataSource
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class BaseDataSource : INotifyPropertyChanged
    {
        protected DataSourceProxy m_AppProxy;

        //protected PropertyChangedEventHandler PropertyChangedEvent;

        public BaseDataSource(DataSourceProxy proxy)
        {
            this.m_AppProxy = proxy;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    add
        //    {
        //        this.PropertyChangedEvent = (PropertyChangedEventHandler)Delegate.Combine(this.PropertyChangedEvent, value);
        //    }
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    remove
        //    {
        //        this.PropertyChangedEvent = (PropertyChangedEventHandler)Delegate.Remove(this.PropertyChangedEvent, value);
        //    }
        //}
        
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            try
            {
                this.m_AppProxy.UpdateListeners(propertyName);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            catch { }
        }
    }
}
