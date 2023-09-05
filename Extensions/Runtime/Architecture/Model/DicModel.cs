using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Plugins.Framework.Extensions.Runtime.Architecture.Model
{
    /// <summary>
    /// 带有字典数据结构的模型
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class DicModel<T1, T2> : AbstractModel
    {
        [LabelText("数据字典")] [ShowInInspector]
        protected readonly SafeDic<T1, T2> Dic = new SafeDic<T1, T2>(typeof(T2).Name);

        public IEnumerable<T2> AllValue => Dic.Dictionary.Values;

        public virtual void Add(T1 key, T2 value)
        {
            Dic.Add(key, value);
        }

        public virtual void Set(T1 key, T2 value)
        {
            Dic[key] = value;
        }

        public virtual T2 Get(T1 key)
        {
            return Dic[key];
        }

        public virtual bool ContainsKey(T1 key)
        {
            return Dic.ContainsKey(key);
        }

        public virtual void Remove(T1 key)
        {
            Dic.Remove(key);
        }

        public virtual void Clear()
        {
            Dic.Clear();
        }
    }
}