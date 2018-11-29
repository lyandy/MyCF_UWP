//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.Video
//类名称:       VideoCategoryModel
//创建时间:     2015/8/9 星期日 17:55:01
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Model.Video
{
    public class VideoCategoryModel
    {
        /// <summary>
        /// 视频类别名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 视频类别背景色
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 跳转页面类型
        /// </summary>
        public Type ClassType { get; set; }
    }
}
