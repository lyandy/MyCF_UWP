//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Selector
//类名称:       NewsActivityDataTemplateSelector
//创建时间:     2015/8/12 星期三 19:23:47
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyCF.Selector
{
    public class NewsActivityDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NewsActivityDataTemplate { get; set; }
        public DataTemplate NewsActivityMobileDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (AppEnvironment.IsPhone)
            {
                return NewsActivityMobileDataTemplate;
            }
            else
            {
                return NewsActivityDataTemplate;
            }
        }
    }
}
