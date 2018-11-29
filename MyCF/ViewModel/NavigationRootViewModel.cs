//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.ViewModel
//类名称:       MainViewModel
//创建时间:     2015/8/8 星期六 11:38:08
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Helper;
using MyCF.Model;
using MyCF.View;
using MyCF.View.About;
using MyCF.ViewModelAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MyCF.ViewModel
{
    public class NavigationRootViewModel : NavigationRootViewModelAttribute
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public NavigationRootViewModel()
        {

        }

        public async Task InitCategoryPngs()
        {
            NewsPNG = await CommonHelper.Instance.LoadImageSource("ms-appx:///Assets/Category/news.png");
            VideoPNG = await CommonHelper.Instance.LoadImageSource("ms-appx:///Assets/Category/video.png");
            SetPNG = await CommonHelper.Instance.LoadImageSource("ms-appx:///Assets/Category/set.png");
            MapPNG = await CommonHelper.Instance.LoadImageSource("ms-appx:///Assets/Category/map.png");
            AboutPNG = await CommonHelper.Instance.LoadImageSource("ms-appx:///Assets/Category/about.png");

            await InitEncyWeaponBg();
        }

        public async Task InitEncyWeaponBg()
        {
            //加载火线百科-武器的背景到内存，加快运行图像显示速度
            App.EncyWeaponBg = await CommonHelper.Instance.LoadImageSource("ms-appx:///Assets/weaponBg.png");
        }

        public void GetCFCommonCollection()
        {
            CFCommonCollection.Clear();
            // 添加项列表
            CFCommonCollection.Add(new NavigationRootModel { IconBitmap = NewsPNG, Title = "情报站", ClassType = typeof(NewsPage) });
            CFCommonCollection.Add(new NavigationRootModel { IconBitmap = VideoPNG, Title = "视频库", ClassType = typeof(VideoPage) });
            CFCommonCollection.Add(new NavigationRootModel { IconBitmap = SetPNG, Title = "火线百科", ClassType = typeof(EncyPage) });
            CFCommonCollection.Add(new NavigationRootModel { IconBitmap = MapPNG, Title = "地图点位", ClassType = typeof(MapPage) });
        }

        public void GetCFBottomCollection()
        {
            CFBottomCollection.Clear();
            // 添加项列表
            CFBottomCollection.Add(new NavigationRootModel { IconBitmap = AboutPNG, Title = "关于", ClassType = typeof(AboutPage) });
        }

        #region 清理ViewModel
        public override void Cleanup()
        {
            CleanApp();
            base.Cleanup();
        }

        public void CleanApp()
        {
            IsBusy = false;
        }
        #endregion
    }
}
