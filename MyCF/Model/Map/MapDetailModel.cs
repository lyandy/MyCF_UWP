//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.Map
//类名称:       MapDetailModel
//创建时间:     2015/8/18 星期二 14:51:32
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Async;
using MyCF.Base;
using MyCF.Config;
using MyCF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyCF.Model.Map
{
    public class MapDetailModel
    {
        private List<Datum> _data = new List<Datum>();
        public List<Datum> data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }

    public class Datum : ModelPropertyBase
    {
        //点位名称
        public string p_name { get; set; }
        //点位类型 
        private string _type = "卡点";
        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;

                MapMarkWith = _type.Length * 11;

                switch (_type)
                {
                    case "卡点":
                        MapMarkColor = "#E12929";
                        break;
                    case "阴人点":
                        MapMarkColor = "#CC3BED";
                        break;
                    case "双卡点":
                        MapMarkColor = "#8F1313";
                        break;
                    case "下包点":
                        MapMarkColor = "#FF9B0B";
                        break;
                    case "投掷点":
                        MapMarkColor = "#FD7A2B";
                        break;
                    case "藏匿点":
                        MapMarkColor = "#DD6060";
                        break;
                    case "反卡点":
                        MapMarkColor = "#4DA789";
                        break;
                    case "穿点":
                        MapMarkColor = "#FFBF43";
                        break;
                    case "防守点":
                        MapMarkColor = "#3F97E9";
                        break;
                    case "进攻点":
                        MapMarkColor = "#FC663F";
                        break;
                }
            }
        }

        private string _MapMarkColor = "#1E2929";

        public string MapMarkColor
        {
            get
            {
                return _MapMarkColor;
            }
            set
            {
                if (_MapMarkColor != value)
                {
                    _MapMarkColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _MapMarkWith = 22;
        public double MapMarkWith
        {
            get
            {
                return _MapMarkWith;
            }
            set
            {
                if (_MapMarkWith != value)
                {
                    _MapMarkWith = value;
                    OnPropertyChanged();
                }
            }
        }
        //阵营
        private string _camp = "保卫者";
        public string camp
        {
            get
            {
                return _camp;
            }
            set
            {
                if (_camp != value)
                {
                    _camp = value;
                    OnPropertyChanged();
                }
            }
        }
        //模式
        private string _mode = "通用";
        public string mode
        {
            get
            {
                return _mode;
            }
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    OnPropertyChanged();
                }
            }
        }

        //所用武器
        private string _weapons = "狙击枪";
        public string weapons
        {
            get
            {
                return _weapons;
            }
            set
            {
                _weapons = value;

                if (camp.Length > mode.Length)
                {
                    if (mode.Length == 2)
                    {
                        mode = mode.Substring(0, 1) + "   " + mode.Substring(1, 1);
                    }
                }
                else if (camp.Length < mode.Length)
                {
                    
                    if (camp.Length == 2)
                    {
                        camp = camp.Substring(0, 1) + "   " + camp.Substring(1, 1);
                    }
                }
            }
        }
        //点位来源
        public string p_author { get; set; }
        //点位描述
        public string p_desc { get; set; }

        #region 第一张图片

        private string _first_Image_url = string.Empty;
        public string first_Image_url
        {
            get
            {
                return _first_Image_url;
            }
            set
            {
                _first_Image_url = value;
                if (!string.IsNullOrEmpty(_first_Image_url))
                {
                    GetFirstImageUrl();
                }
            }
        }

        private AsyncProperty<object> _FirstImageUrl;
        public AsyncProperty<object> FirstImageUrl
        {
            get
            {
                //解决集合数据不是通过反序列化生成导致图片资源无法显示的Bug，比如通过ForEach循环Add的数据集合。并且一定要判空，不然会死循环。
                if (_FirstImageUrl == null)
                {
                    if (!string.IsNullOrEmpty(_first_Image_url))
                    {
                        GetFirstImageUrl();
                    }
                }

                return _FirstImageUrl;
            }
            set
            {
                if (_FirstImageUrl != value)
                {
                    _FirstImageUrl = value;

                    OnPropertyChanged();
                }
            }
        }
        public void GetFirstImageUrl()
        {
            FirstImageUrl = new AsyncProperty<object>(async () =>
            {
                try
                {
                    var imageName = first_Image_url.Split('/').Last();
                    var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(CacheConfig.Instance.ImageThumbnailCacheRelativePath, CreationCollisionOption.OpenIfExists);
                    var file = await folder.TryGetItemAsync(imageName);
                    if (file != null)
                    {
                        return folder.Path + "\\" + imageName;
                    }
                    else
                    {
                        WebDownFileHelper.Instance.SaveAsyncWithHttp(first_Image_url, imageName, folder);

                        return first_Image_url;
                    }
                }
                catch
                {
                    return first_Image_url;
                }
            }, null);
        }
        #endregion

        private List<pics> _purls = new List<pics>();
        public List<pics> purls
        {
            get
            {
                return _purls;
            }
            set
            {
                _purls = value;
            }
        }

        private List<string> _urls = new List<string>();
        public List<string> urls
        {
            get
            {
                return _urls;
            }
            set
            {
                _urls = value;

                if (_urls.Count > 0)
                {
                    _urls.ForEach(o => purls.Add(new pics() { p_url = o }));

                    first_Image_url = _urls[0];

                    picCount = _urls.Count;
                }
            }
        }

        private int _picCount = 1;

        public int picCount
        {
            get
            {
                return _picCount;
            }
            set
            {
                if (_picCount != value)
                {
                    _picCount = value;
                    OnPropertyChanged();
                }
            }
        }


        public void Dispose()
        {
            FirstImageUrl = null;
        }
    }

    public class pics : ModelPropertyBase
    {
        private string _p_url = string.Empty;
        public string p_url
        {
            get
            {
                return _p_url;
            }
            set
            {
                _p_url = value;

                if (!string.IsNullOrEmpty(_p_url))
                {
                    GetImageUrl();
                }
            }
        }

        private AsyncProperty<object> _ImageUrl;
        public AsyncProperty<object> ImageUrl
        {
            get
            {
                //解决集合数据不是通过反序列化生成导致图片资源无法显示的Bug，比如通过ForEach循环Add的数据集合。并且一定要判空，不然会死循环。
                if (_ImageUrl == null)
                {
                    if (!string.IsNullOrEmpty(_p_url))
                    {
                        GetImageUrl();
                    }
                }

                return _ImageUrl;
            }
            set
            {
                if (_ImageUrl != value)
                {
                    _ImageUrl = value;

                    OnPropertyChanged();
                }
            }
        }
        public void GetImageUrl()
        {
            ImageUrl = new AsyncProperty<object>(async () =>
            {
                try
                {
                    var imageName = p_url.Split('/').Last();
                    var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(CacheConfig.Instance.ImageThumbnailCacheRelativePath, CreationCollisionOption.OpenIfExists);
                    var file = await folder.TryGetItemAsync(imageName);
                    if (file != null)
                    {
                        return folder.Path + "\\" + imageName;
                    }
                    else
                    {
                        WebDownFileHelper.Instance.SaveAsyncWithHttp(p_url, imageName, folder);

                        return p_url;
                    }
                }
                catch
                {
                    return p_url;
                }
            }, null);
        }

        public void Dispose()
        {
            ImageUrl = null;
        }
    }
}
