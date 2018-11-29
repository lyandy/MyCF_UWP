//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Const
//类名称:       AppNetworkMessageConst
//创建时间:     2015/8/12 星期三 13:30:12
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Const
{
    public class AppNetworkMessageConst
    {
        public const string COLLECTION_ITEM_IS_ZERO = "没有获取到数据，请重试。";

        public const string NETWOTK_IS_ERROR = "数据获取发生错误，请重试。";

        public const string NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_ERROR = "无网络连接且本地缓存读取出错，请连接网络后重试。";

        public const string NETWORK_IS_OFFLINE_LOCAL_CACHE_IS_NULL = "无网络连接且本地无缓存，请连接网络后重试。";

        public const string NETWORK_IS_OFFLINEL = "无网络连接，请连接网络后重试。";

        public const string WEB_IS_ERROR = "网页加载错误，请重试。";
    }
}
