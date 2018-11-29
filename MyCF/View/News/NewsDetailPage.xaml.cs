using GalaSoft.MvvmLight.Ioc;
using MyCF.Common;
using MyCF.Const;
using MyCF.Helper;
using MyCF.Locator;
using MyCF.Model.News;
using MyCF.UIControl.UserControls.RetryUICtrl;
using MyCF.ViewModel.News;
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
using MyCF.Config;
using Windows.Web.Http;
using System.Diagnostics;
using System.Text;
using Windows.Storage;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MyCF.View.News
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewsDetailPage : Page
    {
        private NavigationHelper navigationHelper;

        private NewsViewModel newsViewModel;

        public NewsDetailPage()
        {
            this.InitializeComponent();

            if (newsViewModel == null)
            {
                if (!SimpleIoc.Default.IsRegistered<NewsViewModel>())
                {
                    SimpleIoc.Default.Register<NewsViewModel>();
                }

                newsViewModel = SimpleIoc.Default.GetInstance<NewsViewModel>();
            }

            this.DataContext = newsViewModel;

            this.navigationHelper = new NavigationHelper(this);

            //this.wv.ManipulationMode = ManipulationModes.TranslateY | ManipulationModes.TranslateInertia;
            //this.wv.ManipulationStarted += Wv_ManipulationStarted;
            //this.wv.ManipulationDelta += Wv_ManipulationDelta;

            this.Loaded -= NewsDetailPage_Loaded;
            this.Loaded += NewsDetailPage_Loaded;
        }

        //Point start;
        //private void Wv_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        //{
        //    if (e.IsInertial)
        //    {
        //        int threshold = 250;
        //        if (start.X - e.Position.X > threshold) //swipe left
        //        {
        //            e.Complete();

        //            bool ignored = false;
        //            ViewModelLocator.Instance.NavigateBack(ref ignored);
        //        }
        //    }
        //}

        //private void Wv_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        //{
        //    start = e.Position;
        //}

        //为反射再次调用放假做准备
        public void ReLoadUrlSource()
        {
            NewsDetailPage_Loaded(null, null);
        }

        public void NewsDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            newsViewModel.IsBusy = true;

            var model = DicStore.GetValueOrDefault<NewsModelPropertyBase>(AppCommonConst.CURRENT_NEWS_MODEL, null);
            if (model != null)
            {
                var url = model.url;
                if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    this.wv.NavigationCompleted -= Wv_NavigationCompleted;
                    this.wv.NavigationCompleted += Wv_NavigationCompleted;
                    this.wv.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));

                    //uoa替换没有成功，改用js方法处理了
                    //string ua = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_4 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12H143 cfapp/1.8.2.117";
                    //Uri targetUri = new Uri(url);
                    //HttpRequestMessage hrm = new HttpRequestMessage(HttpMethod.Get, new Uri(url, UriKind.RelativeOrAbsolute));
                    //hrm.Headers.Add("User-Agent", ua);
                    //wv.NavigateWithHttpRequestMessage(hrm);
                }
                else
                {
                    newsViewModel.IsBusy = false;
                }
                
            }
            else
            {
                newsViewModel.IsBusy = false;
            }
        }

        private async void Wv_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            //故意放在这里先移除广告在显示动画显示wv
            RemoveBottomAD(wv);

            //InsertJS();

            newsViewModel.IsBusy = true;

            newsViewModel.IsBusy = false;

            if (args.IsSuccess)
            {
                //只有加载成功才可以记录已经看过，以便再次加载的时候能够及时更改颜色
                var model = DicStore.GetValueOrDefault<NewsModelPropertyBase>(AppCommonConst.CURRENT_NEWS_MODEL, null);
                if (model != null)
                {
                    model.NewsTitleForeground = AppCommonConst.NEWS_IS_ALREADY_READ_FOREGROUND;
                    SettingsStore.AddOrUpdateValue<bool>(model.id, true);
                }

                wv.Visibility = Visibility.Visible;

                if (AppEnvironment.IsPhone)
                {
                    await AnimationGrid.AnimateAsync(new FadeInDownAnimation() { Distance = 400, Duration = 0.25 });
                }
                else
                {
                    await AnimationGrid.AnimateAsync(new FadeInLeftAnimation() { Duration = 0.13, Distance = 600 });
                }
            }
            else
            {
                wv.Visibility = Visibility.Collapsed;

                if (AppEnvironment.IsInternetAccess)
                {
                    RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWORK_IS_OFFLINEL, typeof(MyCF.View.News.NewsDetailPage), "ReLoadUrlSource", null);
                }
                else
                {

                    RetryBox.Instance.ShowRetry(AppNetworkMessageConst.NETWOTK_IS_ERROR, typeof(MyCF.View.News.NewsDetailPage), "ReLoadUrlSource", null);
                }
            } 
        }

        #region js注入实验。可以注入成功，就是写的有问题。三种网页互操作方式。1、js注入。2、直接执行js代码。3、替换页面原有js。
        public async void InsertJS()
        {
            try
            {
                //1、js注入。
                StringBuilder bldr = new StringBuilder();
                bldr.Append("var script = document.createElement('script');");
                var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///html/js/Gesture.js"));
                string jsStr = await FileIO.ReadTextAsync(file);
                bldr.Append("script.text = '" + jsStr.Replace("\r\n", "") + "';");
                bldr.Append("var headNode = document.getElementsByTagName('head'); ");
                bldr.Append("if (headNode[0] != null){headNode[0].appendChild(script);};");
                await wv.InvokeScriptAsync("eval", new string[] { bldr.ToString() });

                var retrieveHtml = "document.documentElement.outerHTML;";
                var html = await wv.InvokeScriptAsync("eval", new[] { retrieveHtml });
                Debug.WriteLine(html);
                //await wv.InvokeScriptAsync("eval", new string[] { "function onLoad(u){window.external.notify('56465456')}" });
                var ss = await wv.InvokeScriptAsync("onLoad", new string[] { "cececece" });
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
        }
        #endregion

        //移除底部广告
        public void RemoveBottomAD(WebView _WebView)
        {
            try
            {
                //2、直接执行js代码
                var replaceTarget = "var as = document.getElementsByTagName('div');" +
                                                  "for(i = 0;i < as.length;i++){" +
                                                  " if (as[i].className  == 'qt_bottom_bar'){ as[i].style.visibility = 'hidden';break;}}";

                _WebView.InvokeScriptAsync("eval", new[] { replaceTarget });
            }
            catch (Exception ex)
            {
                string s = "";
            }

            //3、替换页面原有js。
            //await wv.InvokeScriptAsync("eval", new string[] { "function ShowReplay(parentid,newsid){window.external.notify(parentid + ' ')}" });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);

            newsViewModel.IsBusy = false;

            this.Loaded -= NewsDetailPage_Loaded;
            this.wv.NavigationCompleted -= Wv_NavigationCompleted;
        }

        private void wv_ScriptNotify(object sender, NotifyEventArgs e)
        {
            //非https链接现在可以激发ScriptNotify事件了
        }
    }
}
