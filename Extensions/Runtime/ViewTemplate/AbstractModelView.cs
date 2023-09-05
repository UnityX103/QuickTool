using System;
using System.Collections.Generic;
using System.Linq;
using FrameworkExtensions.OdinStateExtensions;
using Sirenix.OdinInspector;

namespace Plugins.Framework.Extensions.Runtime.ViewTemplate
    {
        /// <summary>
        /// 可以在mono中观测model
        /// </summary>
        public abstract class AbstractModelView : AbstractViewTemplate
        {
            [TypeFilter("GetModelType")] 
            [LabelText("需要观测的模型")]
            public IModel model;

            public IEnumerable<Type> GetModelType() => OdinStaticExtansion.GetTypes<IModel>(GetType().Assembly);

            protected override void OnInit()
            {
                base.OnInit();
                if (model == null) return;

                var type = model.GetType().GetInterfaces().First(e =>
                    e.GetInterfaces().Contains(typeof(IModel)));
                model = this.GetModelFromType(type);

                if (model == null)
                {
                    model = this.GetModelFromType(model.GetType());
                }
            }
        }
    }