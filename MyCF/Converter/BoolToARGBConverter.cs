//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Converter
//类名称:       BoolToARGBConverter
//创建时间:     2015/8/12 星期三 14:58:25
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
    public class BoolToARGBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && (bool)value)
            {
                return "#AFB8BC";
            }
            else
            {
                return "#FF5C57";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return "#FF5C57";
        }
    }
}
