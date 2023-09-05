using MoreMountains.Tools;
using UnityEngine;

namespace Plugins.Framework.Extensions.Runtime.Feel
{
    [RequireComponent(typeof(MMProgressBar))]
    public class BindToPropertyMMProgressBar : MonoBehaviour, ITriggerWhenDestroy
    {
        public MMProgressBar _progressBar;

        private BindableProperty<float> _maxProperty;
        private BindableProperty<int> _maxPropertyInt;
        private IUnRegister _unRegister;
        private IUnRegister _unMaxRegister;

        private BindableProperty<float> _targetProperty;
        private BindableProperty<int> _targetPropertyInt;

        private BindableProperty<float> TargetProperty
        {
            set
            {
                if (_targetProperty == null)
                {
                    _progressBar = GetComponent<MMProgressBar>();
                }

                if (_targetProperty != null && _unRegister != null)
                {
                    _unRegister.UnRegister();
                    _unMaxRegister.UnRegister();
                }

                _targetProperty = value;
                _unRegister = _targetProperty.RegisterOnValueChanged(OnTargetPropertyChange, out var data);
                _unMaxRegister = _maxProperty.RegisterOnValueChanged(OnTargetPropertyChange);
                OnTargetPropertyChange(data);
            }
        }

        private BindableProperty<int> TargetPropertyInt
        {
            set
            {
                if (_targetPropertyInt == null)
                {
                    _progressBar = GetComponent<MMProgressBar>();
                }

                if (_targetPropertyInt != null && _unRegister != null)
                {
                    _unRegister.UnRegister();
                }

                _targetPropertyInt = value;
                _unRegister = _targetPropertyInt.RegisterOnValueChanged(OnTargetPropertyChange, out var data);
                OnTargetPropertyChange(data);
            }
        }


        private void OnTargetPropertyChange(float obj)
        {
            _progressBar.UpdateBar01(obj / _maxProperty.Value);
        }


        private void OnTargetPropertyChange(int obj)
        {
            _progressBar.UpdateBar( obj,0, _maxPropertyInt.Value);
        }


        public void Init(BindableProperty<float> targetProperty, BindableProperty<float> max)
        {
            _maxProperty = max;

            TargetProperty = targetProperty;
        }

        public void Init(BindableProperty<int> targetProperty, BindableProperty<int> max)
        {
            _maxPropertyInt = max;

            TargetPropertyInt = targetProperty;
        }

        public void WhenDestroy()
        {
            _unRegister?.UnRegister();
        }
    }
}