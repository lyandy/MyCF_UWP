//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Api.ApiRoot
//类名称:       ApiMapRoot
//创建时间:     2015/8/18 星期二 11:25:42
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using MyCF.Const;
using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Api.ApiRoot
{
    public class ApiMapRoot : ClassBase<ApiMapRoot>
    {
        public ApiMapRoot() : base() { }

        public string MapTeamUrl
        {
            get
            {
                return "http://qt.qq.com/php_cgi/cf_map/varcache_querycfmaplist.php?type=%E5%9B%A2%E9%98%9F";
            }
        }

        public string MapPersonUrl
        {
            get
            {
                return "http://qt.qq.com/php_cgi/cf_map/varcache_querycfmaplist.php?type=%E4%B8%AA%E7%AB%9E";
            }
        }

        public string MapBlastUrl
        {
            get
            {
                return "http://qt.qq.com/php_cgi/cf_map/varcache_querycfmaplist.php?type=%E7%88%86%E7%A0%B4";
            }
        }

        public string MapZombieUrl
        {
            get
            {
                return "http://qt.qq.com/php_cgi/cf_map/varcache_querycfmaplist.php?type=%E7%94%9F%E5%8C%96";
            }
        }

        public string MapDetailUrl
        {
            get
            {
                var id = DicStore.GetValueOrDefault<int>(AppCommonConst.CURRENT_MAP_ID, 1);
                return "http://qt.qq.com/php_cgi/cf_map/varcache_querycfmappoint.php?id=" + id;
            }
        }
    }
}
