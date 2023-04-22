using System;
using System.Collections.Generic;
using FrameworkExtensions.Architecture;

namespace FrameworkExtensions.Model
{
    public static class Ctl_IOCContainer
    {
        private static Dictionary<Type, CtlContainer> CtlToContainerRelationshipTable =
            new Dictionary<Type, CtlContainer>();


        public static CtlContainer GetICtlContainer<T>() where T : ICtl
        {
            return CtlToContainerRelationshipTable[typeof(T)];
        }
        public static CtlContainer GetICtlContainer(Type type) 
        {
            return CtlToContainerRelationshipTable[type];
        }
        public static void AutoCreateContainer(Type type) 
        {
            if (!CtlToContainerRelationshipTable.ContainsKey(type))
            {
                CtlToContainerRelationshipTable.Add(type,new CtlContainer());
            }
        }
        public static T GetCtl<T>(string name) where T : ICtl
        {
            return GetICtlContainer<T>().GetCtl<T>(name);
        }

        public static void RegisterCtl(ICtl ctl)
        {
            Type type = ctl.GetType();
            AutoCreateContainer(type);
            GetICtlContainer(type).RegisterCtl(ctl);
        }
        
        public static void RegisterCtl <T>( T ctl) where  T : ICtl
        {
            AutoCreateContainer(typeof(T));
            GetICtlContainer<T>().RegisterCtl(ctl);
        }
        
        
        public static void UnRegisterCtl <T>( T ctl) where  T : ICtl
        {
            
            GetICtlContainer<T>().UnRegisterCtl(ctl);
        }
    }
}