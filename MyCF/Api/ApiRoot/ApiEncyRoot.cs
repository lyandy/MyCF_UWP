//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Api.ApiRoot
//类名称:       ApiEncyRoot
//创建时间:     2015/8/17 星期一 11:55:41
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Api.ApiRoot
{
    public class ApiEncyRoot :ClassBase<ApiEncyRoot>
    {
        public ApiEncyRoot() : base() { }

        public string EncyUrl
        {
            get
            {
                return "http://qt.qq.com/php_cgi/cf_goods/varcache_querycfwikilist.php?name=wuqi";
            }
        }
    }
}
