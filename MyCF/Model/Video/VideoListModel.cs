//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.Video
//类名称:       VideoListModel
//创建时间:     2015/8/14 星期五 15:19:09
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Async;
using MyCF.Base;
using MyCF.Config;
using MyCF.Encrypt;
using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyCF.Model.Video
{
    public class VideoListModel
    {
        //详细体
        public Msg msg { get; set; }
    }

    public class Msg
    {
        //总共数目
        public string total { get; set; }
        //总页数
        public string totalpage { get; set; }
        //视频列表
        private List<VideoModel> _result = new List<VideoModel>();
        public List<VideoModel> result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }
    }

    public class VideoModel : ModelPropertyBase
    {
        //视频解码id 
        public string sVID { get; set; }
        //视频标题
        public string sTitle { get; set; }
        //腾讯视频链接
        public string sUrl { get; set; }
        //CF视频链接
        public string detailurl { get; set; }

        private string _sIMG = string.Empty;
        //视频缩略图
        public string sIMG
        {
            get
            {
                return _sIMG;
            }
            set
            {
                _sIMG = value;

                if (!string.IsNullOrEmpty(_sIMG))
                {
                    GetConvertSIMGImage();
                }
            }
        }

        private AsyncProperty<object> _ConvertSIMGUrl;
        public AsyncProperty<object> ConvertSIMGUrl
        {
            get
            {
                //解决集合数据不是通过反序列化生成导致图片资源无法显示的Bug，比如通过ForEach循环Add的数据集合。并且一定要判空，不然会死循环。
                if (_ConvertSIMGUrl == null)
                {
                    if (!string.IsNullOrEmpty(_sIMG))
                    {
                        GetConvertSIMGImage();
                    }
                }

                return _ConvertSIMGUrl;
            }
            set
            {
                if (_ConvertSIMGUrl != value)
                {
                    _ConvertSIMGUrl = value;

                    OnPropertyChanged();
                }
            }
        }
        public void GetConvertSIMGImage()
        {
            ConvertSIMGUrl = new AsyncProperty<object>(async () =>
            {
                try
                {
                    var imageName = MD5Core.Instance.ComputeMD5(sIMG) + ".jpg";
                    var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(CacheConfig.Instance.ImageThumbnailCacheRelativePath, CreationCollisionOption.OpenIfExists);
                    var file = await folder.TryGetItemAsync(imageName);
                    if (file != null)
                    {
                        return folder.Path + "\\" + imageName;
                    }
                    else
                    {
                        WebDownFileHelper.Instance.SaveAsyncWithHttp(sIMG, imageName, folder);

                        return sIMG;
                    }
                }
                catch
                {
                    return sIMG;
                }
            }, null);
        }
        //腾讯视频上传者 -- 有可能没有
        public string sAuthor { get; set; }
        //视频时长
        public string iTime { get; set; }
        //总播放量
        public string iTotalPlay { get; set; }

        public string sExt3 { get; set; }

        private DateTime _sCreated = DateTime.Now;
        //视频创建时间
        public string sCreated
        {
            get
            {
                return _sCreated.ToString("yyyy-MM-dd");
            }
            set
            {
                try
                {
                    _sCreated = Convert.ToDateTime(value);
                }
                catch
                {
                    _sCreated = DateTime.Now;
                }
            }
        }

        public void Dispose()
        {
            this.ConvertSIMGUrl = null;
        }
    }
}
