//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.News
//类名称:       NewsModelBase
//创建时间:     2015/8/10 星期一 12:22:34
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Async;
using MyCF.Base;
using MyCF.Config;
using MyCF.Const;
using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyCF.Model.News
{
    public class NewsModelPropertyBase : ModelPropertyBase
    {
        private string _id = "0";
        /// <summary>
        /// 新闻唯一id
        /// </summary>
        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;

                if (!SettingsStore.GetValueOrDefault<bool>(_id, false))
                {
                    NewsTitleForeground = AppCommonConst.NEWS_IS_NOT_READ_FOREGROUND;
                }
                else
                {
                    NewsTitleForeground = AppCommonConst.NEWS_IS_ALREADY_READ_FOREGROUND;
                }
            }
        }

        private string _NewsTitleForeground = "#252323";
        public string NewsTitleForeground
        {
            get
            {
                return _NewsTitleForeground;
            }
            set
            {
                if (_NewsTitleForeground != value)
                {
                    _NewsTitleForeground = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 新闻简述
        /// </summary>
        public string summary { get; set; }

        private string _image_url = string.Empty;
        /// <summary>
        /// 新闻缩略图。如果是视频还要自家添加播放的图片
        /// </summary>
        public string image_url
        {
            get
            {
                return _image_url;
            }
            set
            {
                _image_url = value;
                if (!string.IsNullOrEmpty(_image_url))
                {
                    GetFoucesImage();
                }
            }
        }

        private AsyncProperty<object> _ConvertImageUrl;
        public AsyncProperty<object> ConvertImageUrl
        {
            get
            {
                //解决集合数据不是通过反序列化生成导致图片资源无法显示的Bug，比如通过ForEach循环Add的数据集合。并且一定要判空，不然会死循环。
                if (_ConvertImageUrl == null)
                {
                    if (!string.IsNullOrEmpty(_image_url))
                    {
                        GetFoucesImage();
                    }
                }

                return _ConvertImageUrl;
            }
            set
            {
                if (_ConvertImageUrl != value)
                {
                    _ConvertImageUrl = value;

                    OnPropertyChanged();
                }
            }
        }
        public void GetFoucesImage()
        {
            ConvertImageUrl = new AsyncProperty<object>(async () =>
            {
                try
                {
                    var imageName = image_url.Split('/').Last();
                    var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(CacheConfig.Instance.ImageThumbnailCacheRelativePath, CreationCollisionOption.OpenIfExists);
                    var file = await folder.TryGetItemAsync(imageName);
                    if (file != null)
                    {
                        return folder.Path + "\\" + imageName;
                    }
                    else
                    {
                        WebDownFileHelper.Instance.SaveAsyncWithHttp(image_url, imageName, folder);

                        return image_url;
                    }
                }
                catch
                {
                    return image_url;
                }
            }, null);
        }



        /// <summary>
        /// 是否置顶
        /// </summary>
        public int is_top { get; set; }

        private string _publication_date = string.Empty;
        /// <summary>
        /// 发布时间
        /// </summary>
        public string publication_date
        {
            get
            {
                return _publication_date;
                //try
                //{
                //    DateTime dt = DateTime.Parse(_publication_date);

                //    TimeSpan span = System.TimeZoneInfo.Local.BaseUtcOffset.Hours == 13 ? (DateTime.Now - System.TimeZoneInfo.Local.BaseUtcOffset).AddHours(7) - dt : (DateTime.Now - System.TimeZoneInfo.Local.BaseUtcOffset).AddHours(8) - dt;

                //    if (span.TotalDays > 60)
                //    {
                //        return dt.ToString("yyyy-MM-dd");
                //    }
                //    else
                //        if (span.TotalDays > 30)
                //    {
                //        return "1个月前";
                //    }
                //    else if (span.TotalDays > 14)
                //    {
                //        return "2周前";
                //    }
                //    else
                //                if (span.TotalDays > 7)
                //    {
                //        return "1周前";
                //    }
                //    else
                //                    if (span.TotalDays > 1)
                //    {
                //        return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
                //    }
                //    else
                //                        if (span.TotalHours > 1)
                //    {
                //        return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                //    }
                //    else
                //                            if (span.TotalMinutes > 1)
                //    {
                //        return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                //    }
                //    else
                //                                if (span.TotalSeconds >= 1)
                //    {
                //        return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                //    }
                //    else
                //    {
                //        return string.Format("{0}秒前", 1);
                //    }
                //}
                //catch
                //{
                //    return "时间转换错误";
                //}
            }
            set
            {
                _publication_date = value;
            }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public string url { get; set; }
    }
}
