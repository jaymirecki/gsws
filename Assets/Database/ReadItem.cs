﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSWS.Assets.Database
{
    public abstract class ReadItem<T>
        where T : Item
    {
        private T _original;
        public T Original
        {
            protected get { return _original; }
            set { 
                if (_original == null)
                    {
                        _original = value;
                    }
            }
        }
        private Lock _lock;
        internal Lock Lock
        {
            set
            {
                if (_lock == null)
                {
                    _lock = value;
                }
            }
        }
        public ReadItem()
        {

        }
        public ReadItem(T original)
        {
            Original = original;
        }

    }
}
