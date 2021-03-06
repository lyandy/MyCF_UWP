﻿//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Base
//类名称:       ClassBase
//创建时间:     2015/8/8 星期六 10:52:48
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Base
{
    public abstract class ClassBase<T> where T : new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (null == instance)
                    instance = new T();

                return instance;
            }
        }

        protected ClassBase()
        {
        }
    }
}
