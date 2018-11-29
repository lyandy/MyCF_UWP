using MyCF.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.UserControls.RetryUICtrl
{
    public sealed partial class RetryUIControl : UIControlBase
    {
        public RetryUIControl()
        {
            this.InitializeComponent();

            this.Loaded -= RetryUIControl_Loaded;
            this.Loaded += RetryUIControl_Loaded;

            this.Width = Window.Current.Bounds.Width;
            this.Height = Window.Current.Bounds.Height;

        }

        private void RetryUIControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbFailure.Text = msg;
        }

        public string msg { get; set; }

        public Type fromType { get; set; }

        public string method { get; set; }

        public object[] parameters { get; set; }

        protected override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = Window.Current.Bounds.Width;
            this.Height = Window.Current.Bounds.Height;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object o = Activator.CreateInstance(fromType);

                object obj2 = fromType.GetMethod(method).Invoke(o, parameters);

                Popup pop = this.Parent as Popup;
                if (pop != null)
                {
                    pop.IsOpen = false;
                    pop.Child = null;
                    pop = null;
                }
            }
            catch(Exception ex)
            {
                string s = ex.Message;
            }
        }
    }
}
