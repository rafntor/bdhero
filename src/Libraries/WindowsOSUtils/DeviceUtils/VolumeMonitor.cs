﻿// Copyright 2012-2014 Andrew C. Dvorak
//
// This file is part of BDHero.
//
// BDHero is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BDHero is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with BDHero.  If not, see <http://www.gnu.org/licenses/>.

using System;
using WindowsOSUtils.WMI;
using WindowsOSUtils.WMI.Classes;
using WindowsOSUtils.WMI.Events;

namespace WindowsOSUtils.DeviceUtils
{
    public class VolumeMonitor
    {
        // public event DeviceOperationHandler DeviceInserted;
        // public event DeviceOperationHandler DeviceRemoved;

        // public event DeviceOperationHandler DiskInserted;
        // public event DeviceOperationHandler DiskRemoved;

        public VolumeMonitor()
        {
            var eventWatcher1 = new WMIEventWatcher<VolumeChangeEvent>();
            var eventWatcher2 = new WMIEventWatcher<DeviceChangeEvent>();
            var instanceWatcher1 = new WMIInstanceWatcher<DiskDrive>();
            var instanceWatcher2 = new WMIInstanceWatcher<LogicalDisk>();

            eventWatcher1.EventOccurred += delegate(VolumeChangeEvent instance)
                {
                    Console.WriteLine(instance);
                };

            eventWatcher2.EventOccurred += delegate(DeviceChangeEvent instance)
                {
                    Console.WriteLine(instance);
                };

            instanceWatcher1.InstanceCreated += delegate(DiskDrive instance)
                {
                    Console.WriteLine(instance);
                };
            instanceWatcher1.InstanceDeleted += delegate(DiskDrive instance)
                {
                    Console.WriteLine(instance);
                };

            instanceWatcher2.InstanceCreated += delegate(LogicalDisk instance)
                {
                    Console.WriteLine(instance);
                };
            instanceWatcher2.InstanceDeleted += delegate(LogicalDisk instance)
                {
                    Console.WriteLine(instance);
                };

            eventWatcher1.Start();
            eventWatcher2.Start();
            instanceWatcher1.Start();
            instanceWatcher2.Start();
        }
    }

    public delegate void DeviceOperationHandler(string driveLetter);
}
