﻿using EveScanner.Interfaces.EDE;
using EveScanner.IoC.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveScanner.SQLiteStorage
{
    public class InventoryRepackaged : IInventoryRepackaged
    {
        private long groupId;
        private double volume;

        public InventoryRepackaged()
        {
        }

        [IgnoreMember]
        public int GroupId
        {
            get
            {
                return (int)this.groupId;
            }
        }

        [IgnoreMember]
        public double Volume
        {
            get
            {
                return this.volume;
            }
        }
    }
}
