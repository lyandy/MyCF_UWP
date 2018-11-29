//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Base
//类名称:       UIControlBase
//创建时间:     2015/8/10 星期一 15:29:42
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyCF.Base
{
    public class UIControlBase : UserControl
    {
        NavigationRootPage rootPage = null;
        Frame rootFrame = null;
        public UIControlBase()
        {
            this.Loaded -= UIControlBase_Loaded;
            this.Unloaded -= UIControlBase_UnLoaded;
            this.Loaded += UIControlBase_Loaded;
            this.Unloaded += UIControlBase_UnLoaded;
        }

        private void UIControlBase_UnLoaded(object sender, RoutedEventArgs e)
        {
            if (rootPage != null)
            {
                if (rootFrame != null)
                {
                   rootFrame.SizeChanged -= RootFrame_SizeChanged;
                }
            }
        }

        private void UIControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            rootPage = Window.Current.Content as NavigationRootPage;
            if (rootPage != null)
            {
                rootFrame = (Frame)rootPage.FindName("rootFrame");

                if (rootFrame != null)
                {
                    //rootFrame.SizeChanged -= RootFrame_SizeChanged;
                    rootFrame.SizeChanged += RootFrame_SizeChanged;
                }
            }
        }

        //public string str = "ss";

        private void RootFrame_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OnRootFrameSizeChanged(sender, e);
            //Test();
            //Test1();
        }

        protected virtual void Test()
        {
            Debug.WriteLine("父类执行");
        }

        public void Test1()
        {
            Debug.WriteLine("父类执行Test1");
        }

        protected virtual void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Debug.Write(str);
        }

        ~ UIControlBase()
        {
            rootPage = null;
            rootFrame = null;
        }
    }
}
