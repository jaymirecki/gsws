﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSWS.Assets.Database
{
    public class LocalCollection<T, ReadT>: Collection<T, ReadT>
        where T: Item
        where ReadT: ReadItem<T>, new()
    {
        private Dictionary<string, T> Items;
        public LocalCollection(Database db) : base(db) 
        {
            Items = new Dictionary<string, T>();
        }

        public override bool TryAdd(T item)
        {
            if (!Items.ContainsKey(item.ID))
            {
                Items.Add(item.ID, item);
                return true;
            }
            return false;
        }
        public override bool TryGetReadExclusive(string id, out ReadT item)
        {
            item = null;
            if (Items.ContainsKey(id))
            {
                Lock objectLock;
                if (TryGetReadExclusiveLock(id, out objectLock))
                {
                    item = new ReadT();
                    item.Original = Items[id];
                    item.Lock = objectLock;
                    return true;
                }
            }
            return false;
        }
        public override bool TryGetReadShared(string id, out ReadT item)
        {
            item = null;
            if (Items.ContainsKey(id))
            {
                Lock objectLock;
                if (TryGetReadSharedLock(id, out objectLock))
                {
                    item = new ReadT();
                    item.Original = Items[id];
                    item.Lock = objectLock;
                    return true;
                }
            }
            return false;
        }
        public override bool TryGetEditExclusive(string id, out EditItem<T> item)
        {
            item = null;
            if (Items.ContainsKey(id))
            {
                Lock objectLock;
                if (TryGetEditExclusiveLock(id, out objectLock))
                {
                    item = new EditItem<T>(Items[id]);
                    item.Lock = objectLock;
                    return true;
                }
            }
            return false;
        }
        public override bool TryGetEditShared(string id, out EditItem<T> item)
        {
            item = null;
            if (Items.ContainsKey(id))
            {
                Lock objectLock;
                if (TryGetEditSharedLock(id, out objectLock))
                {
                    item = new EditItem<T>(Items[id]);
                    item.Lock = objectLock;
                    return true;
                }
            }
            return false;
        }
        public override bool TryGetClone(string id, out T item)
        {
            item = null;
            if (Items.TryGetValue(id, out item))
            {
                item = (T)item.Clone();
                return true;
            }
            return false;
        }
    }
}