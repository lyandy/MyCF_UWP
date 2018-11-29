//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model
//类名称:       NavigationRootModel
//创建时间:     2015/8/8 星期六 12:41:32
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MyCF.Model
{
    public class NavigationRootModel
    {
        /// <summary>
        /// 类别前面的图标
        /// </summary>
        public BitmapImage IconBitmap { get; set; }

        /// <summary>
        /// 类别名称/页面名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 跳转页面类型
        /// </summary>
        public Type ClassType { get; set; }
    }
}
