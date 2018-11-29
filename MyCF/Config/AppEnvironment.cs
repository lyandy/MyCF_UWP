//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Config
//类名称:       AppEnvironment
//创建时间:     2015/8/8 星期六 14:16:49
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Networking.Connectivity;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace MyCF.Config
{
    public class AppEnvironment
    {
        //判断是不是手机
        public static bool IsPhone
        {
            get
            {
                return ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            }
        }        

        //判断手机是不是竖屏
        public static bool IsPortrait
        {
            get
            {
                return  ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Portrait;
            }
        }

        //记录竖屏屏幕宽度
        public static double ScreenPortraitWith;

        //设定桌面模式下的窗口最小大小
        public static Size DesktopSize
        {
            get
            {
                //其实这个地方设置了等于没设 ，因为只要Height高度设置不为0的数，它的最小高度都会变为666
                return new Size(800, 800);
            }
        }

        /// <summary>
        /// 是否具有网络连接
        /// </summary>
        public static bool IsInternetAccess
        {
            get
            {
                ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                if (internetConnectionProfile != null)
                {
                    if (internetConnectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
