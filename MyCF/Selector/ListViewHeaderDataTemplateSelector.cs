//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Selector
//类名称:       ListViewHeaderDataTemplateSelector
//创建时间:     2015/9/11 星期五 18:22:09
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Config;
using MyCF.ViewModel.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyCF.Selector
{
    public abstract class MyDataTemplateSelector : ContentControl
    {
        //根据newContent的属性，返回所需的DataTemplate
        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            //根据newContent的属性，选择对应的DataTemplate
            ContentTemplate = SelectTemplate(newContent, this);
        }
    }
    public class ListViewHeaderDataTemplateSelector : MyDataTemplateSelector
    {
        public DataTemplate HeaderNormalDataTemplate { get; set; }
        public DataTemplate HeaderNullDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //var vm = item as NewsViewModel;
            if (AppEnvironment.IsPhone)
            {
                return HeaderNormalDataTemplate;
            }
            else
            {
                return HeaderNullDataTemplate;
            }
        }
    }
}
