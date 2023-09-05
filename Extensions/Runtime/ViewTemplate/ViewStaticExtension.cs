using System;

namespace App.View
{
    public static class ViewStaticExtension
    {
        public static IUnRegister RegisterAutoDestroyBindable<T>(this AbstractViewTemplate self,BindableProperty<T> bindableProperty,Action<T> onValueChanged)
        {
           var unRegister = bindableProperty.RegisterOnValueChanged(onValueChanged);
           onValueChanged.Invoke(bindableProperty.Value);
           self.TempEventHelper.AddUnRegister(unRegister);

           return unRegister;
        }
        
        
        
        public static void RegisterListAdd<T>(this AbstractViewTemplate self,BindableList<T> list,Action<T> action)
        {
            var unRegister =  list.RegisterAddAction(action);
            self.TempEventHelper.AddUnRegister(unRegister);
        }
        
        public static void RegisterListRemove<T>(this AbstractViewTemplate self,BindableList<T> list,Action<T> action)
        {
            var unRegister =  list.RegisterRemoveAction(action);
            self.TempEventHelper.AddUnRegister(unRegister);
        }
        
            
        
        public static void RegisterAutoDestroyEvent<T>(this AbstractViewTemplate self,Action<T> action)
        {
            self.TempEventHelper.RegisterTempEvent<T>(action.Invoke);
        }
    }
}