//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.UIControl.UserControls.PullToRefreshUICtrl
//类名称:       PullToRefreshScrollViewer
//创建时间:     2015/9/11 星期五 14:23:01
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace MyCF.UIControl.UserControls.PullToRefreshUICtrl
{
    public class PullToRefreshScrollViewer : ListView
    {
        private const string ScrollViewerControl = "ScrollViewer";
        private const string ContainerGrid = "ContainerGrid";
        private const string ArrowGrid = "ArrowGrid";
        private const string ImgHeadShot = "ImgHeadShot";
        private const string ImgSolider = "ImgSolider";
        private const string AnimationCanvas = "AnimationCanvas";
        private const string TextGrid = "TextGrid";
        private const string PullToRefreshIndicator = "PullToRefreshIndicator";
        private const string VisualStateNormal = "Normal";
        private const string VisualStateReadyToRefresh = "ReadyToRefresh";

        private DispatcherTimer compressionTimer;
        public ScrollViewer scrollViewer;
        private DispatcherTimer timer;
        private Grid containerGrid;
        private Grid arrowGrid;
        private Image imgHeadShot;
        private Image imgSolider;
        private Canvas animationCanvas;
        private Border pullToRefreshIndicator;
        private bool isCompressionTimerRunning;
        private bool isReadyToRefresh;
        private bool isCompressedEnough;

        public event EventHandler RefreshContent;

        public static readonly DependencyProperty PullTextProperty = DependencyProperty.Register("PullText", typeof(string), typeof(PullToRefreshScrollViewer), new PropertyMetadata("下拉刷新..."));
        public static readonly DependencyProperty RefreshTextProperty = DependencyProperty.Register("RefreshText", typeof(string), typeof(PullToRefreshScrollViewer), new PropertyMetadata("松开开始刷新..."));
        public static readonly DependencyProperty RefreshTimeProperty = DependencyProperty.Register("RefreshTime", typeof(string), typeof(PullToRefreshScrollViewer), new PropertyMetadata("最后更新：" + SettingsStore.GetValueOrDefault<string>(AppCommonConst.LAST_UPDATE_TIME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))));
        public static readonly DependencyProperty RefreshHeaderHeightProperty = DependencyProperty.Register("RefreshHeaderHeight", typeof(double), typeof(PullToRefreshScrollViewer), new PropertyMetadata(40D));
        public static readonly DependencyProperty RefreshCommandProperty = DependencyProperty.Register("RefreshCommand", typeof(ICommand), typeof(PullToRefreshScrollViewer), new PropertyMetadata(null));
        public static readonly DependencyProperty ArrowColorProperty = DependencyProperty.Register("ArrowColor", typeof(Brush), typeof(PullToRefreshScrollViewer), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(200,128,128,128))));
        public static readonly DependencyProperty LeftOffsetProperty = DependencyProperty.Register("LeftOffset", typeof(double), typeof(PullToRefreshScrollViewer), new PropertyMetadata(0));

        //下拉到多少偏移量状态改变
        private double offsetTreshhold = 45;

        public PullToRefreshScrollViewer()
        {
            this.DefaultStyleKey = typeof(PullToRefreshScrollViewer);
            Loaded += PullToRefreshScrollViewer_Loaded;
            Unloaded += PullToRefreshScrollViewer_Unloaded;
        }

        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        public double RefreshHeaderHeight
        {
            get { return (double)GetValue(RefreshHeaderHeightProperty); }
            set { SetValue(RefreshHeaderHeightProperty, value); }
        }

        public string RefreshText
        {
            get { return (string)GetValue(RefreshTextProperty); }
            set { SetValue(RefreshTextProperty, value); }
        }

        public string RefreshTime
        {
            get { return (string)GetValue(RefreshTimeProperty); }
            set { SetValue(RefreshTimeProperty, value); }
        }

        public string PullText
        {
            get { return (string)GetValue(PullTextProperty); }
            set { SetValue(PullTextProperty, value); }
        }

        public double LeftOffset
        {
            get { return (double)GetValue(LeftOffsetProperty); }
            set { SetValue(LeftOffsetProperty, value); }
        }

        public Brush ArrowColor
        {
            get { return (Brush)GetValue(ArrowColorProperty); }
            set { SetValue(ArrowColorProperty, value); }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            scrollViewer = (ScrollViewer)GetTemplateChild(ScrollViewerControl);
            scrollViewer.ViewChanging += ScrollViewer_ViewChanging;
            scrollViewer.Margin = new Thickness(0, 0, 0, -RefreshHeaderHeight);
            var transform = new CompositeTransform();
            transform.TranslateY = -RefreshHeaderHeight;
            scrollViewer.RenderTransform = transform;

            imgHeadShot = (Image)GetTemplateChild(ImgHeadShot);
            imgSolider = (Image)GetTemplateChild(ImgSolider);
            animationCanvas = (Canvas)(GetTemplateChild(AnimationCanvas));


            containerGrid = (Grid)GetTemplateChild(ContainerGrid);

            arrowGrid = (Grid)GetTemplateChild(ArrowGrid);
            //textGrid.Loaded += (ss, ee) =>
            //{
            //    //Rect rect= textGrid.TransformToVisual(pullToRefreshIndicator).TransformBounds(new Rect(0.0, 0.0, pullToRefreshIndicator.ActualWidth, pullToRefreshIndicator.ActualHeight));

            //    //Debug.WriteLine(rect);
                
            //};

            pullToRefreshIndicator = (Border)GetTemplateChild(PullToRefreshIndicator);
            pullToRefreshIndicator.Loaded += (ss, ee) =>
            {
                arrowGrid.Margin = new Thickness(pullToRefreshIndicator.ActualWidth / 2 - 110, 0, 0, 0);
                animationCanvas.Margin = new Thickness(pullToRefreshIndicator.ActualWidth / 2 - 182, 0, 0, 0);
            };
            SizeChanged += OnSizeChanged;
        }

        MediaElement me = null;
        IRandomAccessStream audioStream = null;
        private async void PullToRefreshScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppEnvironment.IsPhone)
            {
                offsetTreshhold = 45;
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;

            compressionTimer = new DispatcherTimer();
            compressionTimer.Interval = TimeSpan.FromSeconds(0.001);
            compressionTimer.Tick += CompressionTimer_Tick;

            timer.Start();

            #region 音效准备
            StorageFile audioFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Audio/Refresh/Headshot_GR.wav"));

            if (audioStream != null)
            {
                audioStream.Dispose();
            }
            audioStream = await audioFile.OpenAsync(Windows.Storage.FileAccessMode.Read);

            if (me != null)
            {
                me.Stop();
                me.Source = null;
            }
            if (me == null)
            {
                me = new MediaElement();
            }
            me.AutoPlay = false;
            me.AudioCategory = AudioCategory.Media;
            await Window.Current.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                me.SetSource(audioStream, audioFile.ContentType);
            });
            #endregion
        }

        private void PullToRefreshScrollViewer_Unloaded(object sender, RoutedEventArgs e)
        {
            if (audioStream != null)
            {
                audioStream.Dispose();
            }

            if (me != null)
            {
                me.Stop();
                me.Source = null;
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Clip = new RectangleGeometry()
            {
                Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height)
            };
        }

        private void ScrollViewer_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            
            if (e.NextView.VerticalOffset == 0)
            {
                timer.Start();
            }
            else
            {
                if (timer != null)
                {
                    timer.Stop();
                }

                if (compressionTimer != null)
                {
                    compressionTimer.Stop();
                }

                isCompressionTimerRunning = false;
                isCompressedEnough = false;
                isReadyToRefresh = false;

                VisualStateManager.GoToState(this, VisualStateNormal, true);
            }
        }

        private async void CompressionTimer_Tick(object sender, object e)
        {
            if (isCompressedEnough)
            {
                VisualStateManager.GoToState(this, VisualStateReadyToRefresh, true);
                isReadyToRefresh = true;
            }
            else
            {
                //这个地方时为了制造一个时间差，从下拉可刷新的位置到compressionOffset < 20 这个20的位置，时间上肯定会在0.1秒完成。阻止imgHeadShot.Opacity变为0，以便能正确相应刷新。太牵强了，唉。后期优化
                await Task.Delay(100);
                //VisualStateManager改变控件可视化速度太慢，不如直接拿到控件快
                imgHeadShot.Opacity = 0;
                imgSolider.Opacity = 1;
                animationCanvas.Opacity = 1;

                VisualStateManager.GoToState(this, VisualStateNormal, true);

                isCompressedEnough = false;
                compressionTimer.Stop();
                //哈哈哈哈哈哈哈哈，我是不是太聪明了，我日你的嘴微软！！ScrollViewer的回弹不会超过0.3秒，所以我延迟0.3秒就解决了问题。哈哈哈哈哈。这里和await Task.Delay(100);处的说法一直
                await Task.Delay(300);
                isReadyToRefresh = false;
            }
        }



        private void PlayAudio()
        {

        }

        private async void Timer_Tick(object sender, object e)
        {
            if (containerGrid != null)
            {
                Rect elementBounds = pullToRefreshIndicator.TransformToVisual(containerGrid).TransformBounds(new Rect(0.0, 0.0, pullToRefreshIndicator.Height, RefreshHeaderHeight));
                var compressionOffset = elementBounds.Bottom;
                //Debug.WriteLine(compressionOffset);
                // Debug.WriteLine(isReadyToRefresh);

                if (compressionOffset >= offsetTreshhold + 10)
                {
                    #region 播放音效
                    if (imgHeadShot.Opacity == 0)
                    {
                        if (me != null)
                        {
                            await Window.Current.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                //me.Play();
                                //SystemMediaTransportControls.GetForCurrentView().PlaybackStatus = MediaPlaybackStatus.Playing;
                            });
                        }
                    }
                    #endregion

                    //VisualStateManager改变控件可视化速度太慢，不如直接拿到控件快。立刻改变控件的可视化状态
                    imgSolider.Opacity = 0;
                    animationCanvas.Opacity = 0;
                    imgHeadShot.Opacity = 1;

                    VisualStateManager.GoToState(this, VisualStateReadyToRefresh, true);

                    if (isCompressionTimerRunning == false)
                    {
                        isCompressionTimerRunning = true;
                        compressionTimer.Start();
                    }

                    isCompressedEnough = true;
                }
                else if (compressionOffset < 20 && isReadyToRefresh == true && imgHeadShot.Opacity != 0)
                {
                    isReadyToRefresh = false;
                    if (timer != null)
                    {
                        timer.Stop();
                        timer.Start();
                    }
                    InvokeRefresh();
                }
                else
                {
                    LeftOffset = compressionOffset * 3 - 100;

                    isCompressedEnough = false;
                    isCompressionTimerRunning = false;
                }
            }
        }

        private void InvokeRefresh()
        {
            isReadyToRefresh = false;
            VisualStateManager.GoToState(this, VisualStateNormal, true);

            if (RefreshContent != null)
            {
                RefreshContent(this, EventArgs.Empty);
            }

            var paramter = GetCommandParameter(this);

            if (RefreshCommand != null && RefreshCommand.CanExecute(paramter))
            {
                RefreshCommand.Execute(paramter);
            }

            SettingsStore.AddOrUpdateValue<string>(AppCommonConst.LAST_UPDATE_TIME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            RefreshTime = "最后更新：" + SettingsStore.GetValueOrDefault<string>(AppCommonConst.LAST_UPDATE_TIME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        #region RefeshCommandParameter
        public static readonly DependencyProperty RefeshCommandParameterProperty =
           DependencyProperty.RegisterAttached("RefreshCommandParameter", typeof(object), typeof(PullToRefreshScrollViewer), null);

        public object RefreshCommandParameter
        {
            get { return GetCommandParameter(this); }
            set { SetCommandParameter(this, value); }
        }

        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(RefeshCommandParameterProperty);
        }

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(RefeshCommandParameterProperty, value);
        }

        #endregion
    }
}
