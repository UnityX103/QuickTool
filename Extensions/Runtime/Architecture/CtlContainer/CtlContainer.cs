using System.Collections.Generic;
using FrameworkExtensions.Architecture;
using UnityEngine;

namespace FrameworkExtensions.Model
{
    public class CtlContainer 
    {
        private SafeDic<string, ICtl> _ctls = new SafeDic<string, ICtl>();


        public void RegisterCtl(ICtl ctl)
        {
            _ctls.Add(ctl.name, ctl);
        }

        public void UnRegisterCtl(ICtl ctl)
        {
            _ctls.Remove(ctl.name);
        }

        public T1 GetCtl<T1>(string name) where T1 : ICtl
        {
            return (T1)_ctls[name];
        }
    }
}