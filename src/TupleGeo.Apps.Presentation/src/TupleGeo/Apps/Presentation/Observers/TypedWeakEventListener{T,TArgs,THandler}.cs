
//
// Author: David Milligan
// Source: https://github.com/davidmilligan/WeakEventListener
//

#region Imported Namespaces

#endregion

namespace TupleGeo.Apps.Presentation.Observers {

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="TArgs"></typeparam>
  /// <typeparam name="THandler"></typeparam>
  internal class TypedWeakEventListener<T, TArgs, THandler> : WeakEventListenerBase<T, TArgs>
    where T : class
    where TArgs : EventArgs
    where THandler : Delegate {
    
    private readonly Action<T, THandler> _unregister;

    #region Protected Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    protected override void StopListening(T source) {
      _unregister(source, (THandler)Delegate.CreateDelegate(typeof(THandler), this, nameof(HandleEvent)));
    }

    #endregion

    #region Public Methods

    public TypedWeakEventListener(T source, Action<T, THandler> register, Action<T, THandler> unregister, Action<T, TArgs> handler)
      : base(source, handler) {
      
      if (register == null) {
        throw new ArgumentNullException(nameof(register));
      }
      
      _unregister = unregister ?? throw new ArgumentNullException(nameof(unregister));
      
      register(source, (THandler)Delegate.CreateDelegate(typeof(THandler), this, nameof(HandleEvent)));
      
    }

    #endregion
    
  }

}
