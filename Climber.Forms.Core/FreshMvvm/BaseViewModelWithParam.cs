using System;
using FreshMvvm;

namespace Climber.Forms.Core
{
    public abstract class BaseViewModelWithParam<T> : FreshBasePageModel where T : class
    {
        #region Properties

        public abstract string Title { get; }

        #endregion

        #region LifeCycle

        public override void Init(object initData)
        {
            if (initData is T param)
                Init(param);
            else
                throw new ArgumentException($"Parameter is not of type {typeof(T)}");
        }

        #endregion

        #region Public

        public abstract void Init(T parameter);

        #endregion
    }
}
