//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Model.News
//类名称:       NewsActivityModel
//创建时间:     2015/8/10 星期一 12:22:14
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Model.News
{
    public class NewsActivityModel
    {
        private List<ActivityModel> _news = new List<ActivityModel>();
        public List<ActivityModel> news
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
        public bool next_page { get; set; }
    }

    public class ActivityModel : NewsModelPropertyBase
    {
        private JoinInfo _backup3;
        /// <summary>
        /// 根据type，来决定backup3的反序列化类。这里已分开处理活动和非活动的情况
        /// </summary>
        public JoinInfo backup3
        {
            get
            {
                return _backup3;
            }
            set
            {
                _backup3 = value;
            }
        }

        private DateTime _act_begin_date = DateTime.Now;
        /// <summary>
        /// 开始时间
        /// </summary>
        public string act_begin_date
        {
            get
            {
                return _act_begin_date.ToString("yyyy-MM-dd");
            }
            set
            {
                try
                {
                    _act_begin_date = Convert.ToDateTime(value);
                }
                catch
                {
                    _act_begin_date = DateTime.Now;
                }
            }
        }

        private DateTime _act_end_date = DateTime.Now;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string act_end_date
        {
            get
            {
                return _act_end_date.ToString("yyyy-MM-dd");
            }
            set
            {
                try
                {
                    _act_end_date = Convert.ToDateTime(value);

                    MaxValue = (Convert.ToDateTime(act_end_date) - Convert.ToDateTime(act_begin_date)).Days;

                    CurrentValue = (DateTime.Now - Convert.ToDateTime(act_begin_date)).Days;

                    //如果已经过期则
                    if (CurrentValue > MaxValue)
                    {
                        IsExpired = true;
                    }
                    else
                    {
                        IsExpired = false;

                        if (CurrentValue == 0)
                        {
                            CurrentValue = 1;
                        }
                        else if (CurrentValue == MaxValue)
                        {
                            CurrentValue = MaxValue - 1;
                        }
                    }
                }
                catch
                {
                    _act_end_date = DateTime.Now;
                }
            }
        }

        //是否过期。这里根据是否过期来决定显示 “进行中”和“已结束”
        private bool _IsExpired = false;

        public bool IsExpired
        {
            get
            {
                return _IsExpired;
            }
            set
            {
                _IsExpired = value;
                if (_IsExpired)
                {
                    MaxValue = CurrentValue = 1;

                    StatusText = "已结束";
                    StatusColor = "#808080";
                }
                else
                {
                    StatusText = "进行中";
                    StatusColor = "#1ABD29";
                }
            }
        }


        private int _MaxValue = 1;
        public int MaxValue
        {
            get
            {
                return _MaxValue;
                
            }
            set
            {
                if (_MaxValue != value)
                {
                    _MaxValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _CurrentValue = 1;
        public int CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {
                if (_CurrentValue != value)
                {
                    _CurrentValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _JoinText = "";
        public string JoinText
        {
            get
            {
                if (backup3 != null)
                {
                    //手机参与
                    if (backup3.joinType == "1")
                    {
                        _JoinText = "手机参与";

                        //如果是手机
                        if (AppEnvironment.IsPhone)
                        {
                            JoinColor = "#FF5C57";
                        }
                        else
                        {
                            JoinColor = "#AFB8BC";
                        }
                    }
                    else
                    {
                        if (subscript.Contains("游戏"))
                        {
                            _JoinText = "登录游戏";
                        }
                        else
                        {
                            _JoinText = "电脑参与";
                        }

                        //如果是手机
                        if (AppEnvironment.IsPhone)
                        {
                            JoinColor = "#AFB8BC";
                        }
                        else
                        {
                            JoinColor = "#FF5C57";
                        }
                    }
                }

                return _JoinText;
            }
            set
            {
                _JoinText = value;
            }
        }

        private string _JoinColor = "#FF5C57";
        public string JoinColor
        {
            get
            {
                return _JoinColor;
            }
            set
            {
                if (_JoinColor != value)
                {
                    _JoinColor = value;
                    OnPropertyChanged();
                }
            }
        }

        //当前"进行中"和已结束的状态颜色设置
        private string _StatusColor = "#808080";
        public string StatusColor
        {
            get
            {
                return _StatusColor;
            }
            set
            {
                if (_StatusColor != value)
                {
                    _StatusColor = value;
                    OnPropertyChanged();
                }
            }
        }

        //当前“进行中”和“已结束”的文字
        private string _StatusText = "已结束";
        public string StatusText
        {
            get
            {
                return _StatusText;
            }
            set
            {
                if (_StatusText != value)
                {
                    _StatusText = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// "手机"  手机参与   "游戏"  登录游戏  其他的都是电脑参与
        /// </summary>
        public string subscript { get; set; }
    }

    public class JoinInfo
    {
        /// <summary>
        /// 活动加入类型。"1" 手机加入 "2""0" 电脑加入 
        /// </summary>
        public string joinType { get; set; }
    }
}
