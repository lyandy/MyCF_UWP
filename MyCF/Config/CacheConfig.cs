//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Config
//类名称:       CacheConfig
//创建时间:     2015/8/11 星期二 12:29:05
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Config
{
    public class CacheConfig : ClassBase<CacheConfig>
    {
        /// <summary>
        /// 情报站数据列表文件缓存相对路径
        /// </summary>
        public string NewsListFileCacheRelativePath
        {
            get
            {
                return "Cache\\News\\";
            }
        }

        /// <summary>
        /// 视屏库数据列表文件缓存相对路径
        /// </summary>
        public string VideoListFileCacheRelativePath
        {
            get
            {
                return "Cache\\Videos\\";
            }
        }

        /// <summary>
        /// 图片缩略图文件缓存相对路径
        /// </summary>
        public string ImageThumbnailCacheRelativePath
        {
            get
            {
                return "Cache\\Images\\";
            }
        }

        /// <summary>
        /// 火线百科数据列表文件缓存相对路径
        /// </summary>
        public string EncyListFileCacheRelativePath
        {
            get
            {
                return "Cache\\Ency\\";
            }
        }

        /// <summary>
        /// 地图点位数据列表文件缓存相对路径
        /// </summary>
        public string MapListFileCacheRelativePath
        {
            get
            {
                return "Cache\\Map\\";
            }
        }
    }
}
