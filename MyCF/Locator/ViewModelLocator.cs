//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Locator
//类名称:       ViewModelLocator
//创建时间:     2015/8/8 星期六 11:25:01
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MyCF.Base;
using MyCF.Common;
using MyCF.ViewModelExecuteCommand;
using MyCF.ViewModelExecuteCommand.News;
using MyCF.ViewModelExecuteCommand.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Locator
{
    public class ViewModelLocator : ClassBase<ViewModelLocator>
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator() : base()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            //SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<NewsViewModelExecuteCommand>(false);
            SimpleIoc.Default.Register<VideoListViewModelExecuteCommand>(false);
            SimpleIoc.Default.Register<VideoCategoryViewModelExecuteCommand>(false);
        }

        public NewsViewModelExecuteCommand ExeCommandNewsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewsViewModelExecuteCommand>();
            }
        }

        public VideoListViewModelExecuteCommand ExeCommandVideoListVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VideoListViewModelExecuteCommand>();
            }
        }

        public VideoCategoryViewModelExecuteCommand ExeCommandVideoCategoryVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VideoCategoryViewModelExecuteCommand>();
            }
        }

        //平板模式页面导航事件委托
        public delegate void NavigateBackEventHandler(ref bool handled);
        public NavigateBackEventHandler NavigateBackHandler;
        public void NavigateBack(ref bool handled)
        {
            NavigateBackEventHandler handler = NavigateBackHandler;
            if (handler != null)
            {
                handler(ref handled);
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
