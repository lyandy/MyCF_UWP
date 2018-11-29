//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Helper
//类名称:       QVideoHelper
//创建时间:     2015/8/15 星期六 19:00:09
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace MyCF.Helper
{
    public class QVideoHelper : ClassBase<QVideoHelper>
    {
        public QVideoHelper() : base() { }

        public async Task<VideoDecodeResult> DecodeVid(string vid)
        {
            var backJson = await WebDataHelper.Instance.GetFromUrlWithAuthReturnString("http://vv.video.qq.com/geturl?vid=" + vid + "&otype=json&platform=3&ran=0%2E9652906153351068", null, 3);
            if (backJson != null)
            {
                var result = JsonConvertHelper.Instance.DeserializeObject<VideoDecodeResult>(backJson.Replace("QZOutputJson=", "").Replace("};", ""));

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class VideoDecodeResult
    {
        public Vd vd { get; set; }
    }

    public class Vd
    {
        private List<Vi> _vi = new List<Vi>();
        public List<Vi> vi
        {
            get
            {
                return _vi;
            }
            set
            {
                _vi = value;
            }
        }
    }

    public class Vi
    {
        //可以作为文件名
        public string lnk { get; set; }
        //视频链接
        public string url { get; set; }
        //可选视频链接模型
        public Urlbk urlbk { get; set; }
    }

    public class Urlbk
    {
        private List<Ui> _ui = new List<Ui>();
        public List<Ui> ui
        {
            get
            {
                return _ui;
            }
            set
            {
                _ui = value;
            }
        }
    }

    public class Ui
    {
        //视频链接
        public string url { get; set; }
    }

}
