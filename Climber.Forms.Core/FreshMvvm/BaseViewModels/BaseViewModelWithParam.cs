using System;
using FreshMvvm;

namespace Climber.Forms.Core
{
    public abstract class BaseViewModel<T> : FreshBasePageModel where T : class
    {
        #region Properties

        public abstract string Title { get; }

        #endregion

        #region LifeCycle

        public override void Init(object initData)
        {
            if (initData is T param)
                Prepare(param);
            else if (initData != null)
                throw new ArgumentException($"Parameter is not of type {typeof(T)}");

            Init();
        }

        public virtual void Init()
        {
        }

        public abstract void Prepare(T parameter);

        #endregion
    }
}