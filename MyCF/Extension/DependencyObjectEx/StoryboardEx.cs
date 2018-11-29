//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Extension.DependencyObjectEx
//类名称:       StoryboardEx
//创建时间:     2015/8/8 星期六 12:00:41
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace MyCF.Extension.DependencyObjectEx
{
    public static class StoryboardEx
    {
        /// <summary>
        /// Begins a storyboard and waits for it to complete.
        /// </summary>
        public static async Task BeginAsync(this Storyboard storyboard)
        {
            await EventAsync.FromEvent<object>(
                eh => storyboard.Completed += eh,
                eh => storyboard.Completed -= eh,
                storyboard.Begin);
        }
    }
}
