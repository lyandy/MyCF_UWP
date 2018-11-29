//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Api.ApiRoot
//类名称:       ApiNewsRoot
//创建时间:     2015/8/10 星期一 10:16:44
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
    public class ApiNewsRoot : ClassBase<ApiNewsRoot>
    {
        public ApiNewsRoot() : base() { }

        private string newsBaseUrl = "http://qt.qq.com/php_cgi/cf_news/php/varcache_getnews.php";

        /// <summary>
        /// 资讯网络请求链接
        /// </summary>
        public string FirstNewsUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.NEWS_FIRSTNEWS_PAGE_INDEX_CONST, 1);
                return newsBaseUrl + "?id=" + (int)NewsEnum.FIRSTNEWS + "&page=" + pageIndex;
            }
        }

        /// <summary>
        /// 赛事网络请求链接
        /// </summary>
        public string ChampionUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.NEWS_CHAMPION_PAGE_INDEX_CONST, 1);
                return newsBaseUrl + "?id=" + (int)NewsEnum.CHAMPION + "&page=" + pageIndex;
            }
        }

        /// <summary>
        /// 活动网络请求链接
        /// </summary>
        public string ActivityUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.NEWS_ACTIVITY_PAGE_INDEX_CONST, 1);
                return newsBaseUrl + "?id=" + (int)NewsEnum.ACTIVITY + "&page=" + pageIndex;
            }
        }

        /// <summary>
        /// 动漫网络请求链接
        /// </summary>
        public string CartoonUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.NEWS_CARTOON_PAGE_INDEX_CONST, 1);
                return newsBaseUrl + "?id=" + (int)NewsEnum.CARTOON + "&page=" + pageIndex;
            }
        }

        /// <summary>
        /// 资攻略网络请求链接
        /// </summary>
        public string StrategyUrl
        {
            get
            {
                var pageIndex = DicStore.GetValueOrDefault<int>(AppPageIndexConst.NEWS_STRATEGY_PAGE_INDEX_CONST, 1);
                return newsBaseUrl + "?id=" + (int)NewsEnum.STRATEGY + "&page=" + pageIndex;
            }
        }

        public enum NewsEnum
        {
            FIRSTNEWS = 8,
            CHAMPION = 23,
            ACTIVITY = 15,
            CARTOON = 21,
            STRATEGY = 3
        }
    }
}
