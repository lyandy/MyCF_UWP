//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Converter
//类名称:       DoubleToAppBarButtonLabel
//创建时间:     2015/8/16 星期日 11:13:23
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MyCF.Converter
{
    public class BoolToAppBarButtonLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
            {
                return (bool)value ? "已静音" : "声音";
            }
            else
            {
                return "已静音";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return "已静音";
        }
    }
}
