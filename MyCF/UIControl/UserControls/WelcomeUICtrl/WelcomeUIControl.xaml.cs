using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Brain.Animate;
using MyCF.Helper;
using System.Threading.Tasks;
using MyCF.Base;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.UserControls.WelcomeUICtrl
{
    public sealed partial class WelcomeUIControl : UIControlBase
    {
        public WelcomeUIControl()
        {
            this.InitializeComponent();

            this.grid.Width = Window.Current.Bounds.Width;
            this.grid.Height = Window.Current.Bounds.Height;
        }

        protected override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.OnRootFrameSizeChanged(sender, e);

            this.grid.Width = Window.Current.Bounds.Width;
            this.grid.Height = Window.Current.Bounds.Height;
        }

        private async void grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.pathMark.RotateAsync(0.5, 360);
            this.pathMark.AnimateAsync(new BounceInAnimation() { Duration = 0.5 });
            await this.pathMark.AnimateAsync(new FadeInAnimation() { Duration = 0.5 });

            this.tbBam.Text = "掌上穿越火线";
            await tbBam.AnimateText(RandomAnimationHelper.Instance.GetAnimation(), 0.1);

            sb_wel.Completed += Sb_wel_Completed; ;
            await Task.Delay(200);
            sb_wel.Begin();
        }

        private void Sb_wel_Completed(object sender, object e)
        {
            sb_wel.Completed -= Sb_wel_Completed;
            var popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
                popup.Child = null;
                popup = null;
            }
        }
    }
}
