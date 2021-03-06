﻿//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Helper
//类名称:       DeviceHelper
//创建时间:     2015/8/8 星期六 11:15:37
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using MyCF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace MyCF.Helper
{
    public class DeviceHelper : ClassBase<DeviceHelper>
    {
        public DeviceHelper() : base() { }

        /// <summary>
        /// 获取设备唯一Id
        /// </summary>
        /// <returns></returns>
        public string GetDeviceUniqueId()
        {
            var token = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null);
            IBuffer buffer = token.Id;
            using (var dataReader = DataReader.FromBuffer(buffer))
            {
                var bytes = new byte[buffer.Length];
                dataReader.ReadBytes(bytes);
                return BitConverter.ToString(bytes);
            }
        }
    }
}
