using MyCF.Base;
using MyCF.Model.Ency;
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
using MyCF.Extension;
using MyCF.Config;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCF.UIControl.UserControls.WeaponDetailUICtrl
{
    public sealed partial class WeaponDetailUIControl : UIControlBase
    {
        public WeaponDetailUIControl()
        {
            this.InitializeComponent();

            this.DataContext = this;

            this.grid.Width = Window.Current.Bounds.Width;
            this.grid.Height = Window.Current.Bounds.Height;

            if (AppEnvironment.IsPhone)
            {
                this.gridContent.Width = Window.Current.Bounds.Width - 5;
            }
        }

        public WeaponModel weaponModel { get; set; }

        private void grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            WeaponDetailBox.Instance.HideWeaponDetail();
        }

        protected override void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.grid.Width = Window.Current.Bounds.Width;
            this.grid.Height = Window.Current.Bounds.Height;
        }


        #region IsBusy 依赖属性
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(WeaponDetailUIControl),
            new PropertyMetadata(false, new PropertyChangedCallback(OnPropertyChanged)));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (WeaponDetailUIControl)d;
            //obj.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            obj.pro.IsActive = (bool)e.NewValue;
        }
        #endregion

        private async void grid_Loaded(object sender, RoutedEventArgs e)
        {
            await this.gridBeneath.AnimateAsync(new FadeInAnimation() { Duration = 0.2 });

            IsBusy = true;

            if (AppEnvironment.IsPhone)
            {
                this.grid.Tapped += grid_Tapped;
            }
            else
            {
                this.grid.Tapped -= grid_Tapped;
                this.appCloseBtn.AnimateAsync(new FadeInDownAnimation());
            }

            var backJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString("http://qt.qq.com/php_cgi/cf_goods/varcache_querycfwiki.php?code=" + weaponModel.code, null, 3);
            if (backJson != null)
            {
                var result = JsonConvertHelper.Instance.DeserializeObject<WeaponDetailModel>(backJson);
                if (result != null)
                {
                    this.tbWeaponName.Text = result.name;
                    this.tbWeaponType.Text = result.type2;
                    this.lvPrice.ItemsSource = result.reattr;

                    double power = 0;
                    double.TryParse(result.power, out power);
                    this.sliderWeaponPower.Value = power;

                    if (result.distance != "0")
                    {
                        this.tbWeaponAccuracy.Text = "手持移速 : ";
                        this.tbWeaponSpeed.Text = "距离 : ";
                        this.tbWeaponStability.Text = "范围 : ";

                        this.tbWeaponHandMoveSpeed.Visibility = Visibility.Collapsed;

                        this.gridThirdLine.Margin = new Thickness(0, -28, 0, 12);

                        double distance = 0;
                        double.TryParse(result.distance, out distance);
                        this.sliderWeaponSpeed.Value = distance;

                        double field = 0;
                        double.TryParse(result.field, out field);
                        this.sliderWeaponStability.Value = field;

                        this.sliderWeaponAccuracy.Value = 100 - distance;
                    }
                    else
                    {
                        this.tbWeaponAccuracy.Text = "准确度 : ";
                        this.tbWeaponSpeed.Text = "射速 : ";
                        this.tbWeaponStability.Text = "稳定性 : ";

                        this.tbWeaponHandMoveSpeed.Visibility = Visibility.Visible;

                        this.gridThirdLine.Margin = new Thickness(0, 12, 0, 12);

                        double accuracy = 0;
                        double.TryParse(result.accuracy, out accuracy);
                        this.sliderWeaponAccuracy.Value = accuracy;

                        double speed = 0;
                        double.TryParse(result.speed, out speed);
                        this.sliderWeaponSpeed.Value = speed;

                        double weight = 0;
                        double.TryParse(result.weight, out weight);
                        this.sliderWeaponStability.Value = accuracy - weight;

                        this.sliderWeaponHandMoveSpeed.Value = (accuracy + speed) * 0.7;
                    }

                    this.runOneCount.Text = result.magazine;
                    this.runAllCount.Text = result.bullet_num;

                    this.tbWeaponDetail.Text = result.goods_desc;
                }
            }

            await Task.Delay(200);

            IsBusy = false;

            StartAnimation();
        }

        public async void StartAnimation()
        {
            this.gridFirstLine.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.3 });
            this.gridSecondLine.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.3 });
            this.gridThirdLine.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.3 });
            this.gridFourthLine.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.3 });

            //await Task.Delay(400);

            await this.tbWeaponName.AnimateAsync(new FadeInDownAnimation() { Distance = 300, Duration = 0.2 });
            await this.tbWeaponType.AnimateAsync(new FadeInDownAnimation() { Distance = 300, Duration = 0.2 });

            this.lvPrice.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.2 });
            await this.tbWeaponPrice.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.2 });

            //this.lvPrice.UpdateLayout();
            //var control = VisualTreeHelperEx.FindVisualChildByName<WrapGrid>(this.lvPrice, "");
            //foreach (var ctrl in control.Children)
            //{
            //    await (ctrl as FrameworkElement).AnimateAsync(new FlipInYAnimation());
            //}


            this.tbWeaponPower.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.2 });
            this.sliderWeaponPower.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.2 });

            await Task.Delay(100);

            this.tbWeaponAccuracy.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.2 });
            this.sliderWeaponAccuracy.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.2 });

            await Task.Delay(100);

            this.tbWeaponSpeed.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.2 });
            this.sliderWeaponSpeed.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.2 });

            await Task.Delay(100);

            this.tbWeaponStability.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.2 });
            this.sliderWeaponStability.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.2 });

            await Task.Delay(100);
            
            this.tbWeaponHandMoveSpeed.AnimateAsync(new FadeInRightAnimation() { Distance = 600, Duration = 0.3 });
            this.sliderWeaponHandMoveSpeed.AnimateAsync(new FadeInLeftAnimation() { Distance = 600, Duration = 0.3 });

            await Task.Delay(100);

            this.spShotCount.AnimateAsync(new FlipInXAnimation());

            await tbWeaponDetail.AnimateAsync(new FadeInUpAnimation());
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            WeaponDetailBox.Instance.HideWeaponDetail();
        }
    }
}
