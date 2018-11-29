//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.News
//类名称:       NewsCommonModel
//创建时间:     2015/8/10 星期一 11:53:21
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Model.News
{
    public class NewsCommonModel
    {
        private List<NewsModel> _news = new List<NewsModel>();
        public List<NewsModel> news
        {
            get
            {
                return _news;
            }
            set
            {
                _news = value;
            }
        }

        private List<NewsModel> _ads = new List<NewsModel>();
        public List<NewsModel> ads
        {
            get
            {
                return _ads;
            }
            set
            {
                _ads = value;
            }
        }
        public bool next_page { get; set; }
    }

    public class NewsModel : NewsModelPropertyBase
    {
        private string _type = "6";
        /// <summary>
        /// 新闻类型 type = 6 专题  type = 1 普通 type = 3 视频 根据vid获取到真实的视频地址播放 type = 2 活动
        /// </summary>
        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                if (_type == ((int)NewsTypeEnum.VideoType).ToString())
                {
                    IsVideo = true;
                }
            }
        }
        
        /// <summary>
        /// 根据type，来决定backup3的反序列化类。这里已分开处理活动和非活动的情况
        /// </summary>
        public VideoInfo backup3 { get; set; }

        private bool _IsVideo = false;

        public bool IsVideo
        {
            get
            {
                return _IsVideo;
            }
            set
            {
                if (_IsVideo != value)
                {
                    _IsVideo = value;
                    OnPropertyChanged();
                }
            }
        }
    }

    public class VideoInfo
    {
        public string topicId { get; set; }
        public string videoType { get; set; }
        /// <summary>
        /// 视频vid，重点根据这个来获取到真实的视频地址
        /// </summary>
        public string vid { get; set; }
        //此链接可能是爱拍的视频链接，但到目前为止还没遇到。VideoListModel.cs经常遇到爱拍的sExt3链接就是mp4格式的爱拍视频
        public string videoUrl { get; set; }
    }

    public enum  NewsTypeEnum
    {
        VideoType = 3,
        NewsType = 1,
        TopicType = 6,
        ActivityType = 2
    }
}
