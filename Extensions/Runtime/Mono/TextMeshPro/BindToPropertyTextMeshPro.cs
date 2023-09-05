using System;
using App.View;
using TMPro;
using UnityEngine;

namespace Plugins.Framework.Extensions.Runtime.ViewTemplate
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class BindToPropertyTextMeshPro : MonoBehaviour ,ITriggerWhenDestroy
    {
        private IBindableProperty _targetProperty;

        public IBindableProperty TargetProperty
        {
            set
            {
                if (_targetProperty == null)
                {
                    _text = GetComponent<TextMeshProUGUI>();
                }
                if (_targetProperty != null && _unRegister != null)
                {
                    _unRegister.UnRegister();
                }

                _targetProperty = value;
                _unRegister = _targetProperty.RegisterOnValueChanged(OnTargetPropertyChange, out var data);
                OnTargetPropertyChange(data);
            }
        }

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        private TextMeshProUGUI _text;

        private IUnRegister _unRegister;


        private void OnTargetPropertyChange(object obj)
        {
            _text.text = obj.ToString();
        }

        public  void WhenDestroy()
        {
            _unRegister?.UnRegister();
        }
    }
}