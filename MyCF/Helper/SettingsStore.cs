//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Helper
//类名称:       SettingsStore
//创建时间:     2015/8/8 星期六 11:17:22
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyCF.Helper
{
    public class SettingsStore
    {
        public static bool AddOrUpdateValue<T>(string key, T value)
        {
            try
            {
                ApplicationDataContainer myContainer = ApplicationData.Current.LocalSettings;
                myContainer.Values[key] = value;

                return true;
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                return false;
            }
        }

        public static T GetValueOrDefault<T>(string key, T defaultValue)
        {
            ApplicationDataContainer myContainer = ApplicationData.Current.LocalSettings;
            if (myContainer.Values.ContainsKey(key))
            {
                return (T)myContainer.Values[key];
            }
            else
            {
                return defaultValue;
            }
        }

        public static bool RemoveKey(string key)
        {
            ApplicationDataContainer myContainer = ApplicationData.Current.LocalSettings;
            if (myContainer.Values.ContainsKey(key))
            {
                return myContainer.Values.Remove(key);
            }
            else
            {
                return true;
            }
        }

        public static bool Clear()
        {
            ApplicationDataContainer myContainer = ApplicationData.Current.LocalSettings;
            try
            {
                myContainer.Values.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
