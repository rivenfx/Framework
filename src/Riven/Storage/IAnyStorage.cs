using JetBrains.Annotations;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Linq;

namespace Riven.Storage
{
    public interface IAnyStorage<TKey, TValue>
    {
        void AddOrUpdate(TKey key, TValue val);


        TValue Get(TKey key);


        IReadOnlyList<TValue> GetAll();

        void Remove(TKey key);


        void Clear();
    }

    public interface IAnyStorage<TValue> : IAnyStorage<string, TValue>
    {

    }

    public abstract class AnyStorageBase<TKey, TValue> : IAnyStorage<TKey, TValue>
    {
        protected readonly ConcurrentDictionary<TKey, TValue> _dataMap;

        public AnyStorageBase()
        {
            _dataMap = new ConcurrentDictionary<TKey, TValue>();
        }

        public void AddOrUpdate([NotNull] TKey key, [NotNull] TValue val)
        {
            Check.NotNull(key, nameof(key));
            Check.NotNull(val, nameof(val));

            _dataMap.AddOrUpdate(key, val, (k, v) =>
            {
                return val;
            });
        }

        public void Clear()
        {
            _dataMap.Clear();
        }

        public TValue Get(TKey key)
        {
            Check.NotNull(key, nameof(key));

            if (_dataMap.TryGetValue(key, out TValue value))
            {
                return value;
            }

            return default(TValue);
        }

        public IReadOnlyList<TValue> GetAll()
        {
            return _dataMap.Values.ToList().AsReadOnly();
        }

        public void Remove(TKey key)
        {
            _dataMap.TryRemove(key, out TValue value);
        }
    }

    public abstract class AnyStorageBase<TValue> : AnyStorageBase<string, TValue>, IAnyStorage<TValue>
    {
        public AnyStorageBase()
            : base()
        {

        }
    }
}
