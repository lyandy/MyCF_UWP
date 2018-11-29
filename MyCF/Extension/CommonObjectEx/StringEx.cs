//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Extension.CommonObjectEx
//类名称:       StringEx
//创建时间:     2015/8/8 星期六 11:10:41
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Extension.CommonObjectEx
{
    public static class StringEx
    {
        public static bool IsNullOrBlankOrEmpry(this string s)
        {
            if (s.Equals("") || s.Equals(string.Empty) || s == null)
                return true;
            else return false;
        }
    }
}
