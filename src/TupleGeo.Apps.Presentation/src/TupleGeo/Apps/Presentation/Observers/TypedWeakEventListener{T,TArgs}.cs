
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
  internal class TypedWeakEventListener<T, TArgs> : WeakEventListenerBase<T, TArgs>
    where T : class
    where TArgs : EventArgs {
        
    private readonly Action<T, EventHandler<TArgs>> _unregister;

    #region Protected Methods

    protected override void StopListening(T source) => _unregister(source, HandleEvent);

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="register"></param>
    /// <param name="unregister"></param>
    /// <param name="handler"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public TypedWeakEventListener(T source, Action<T, EventHandler<TArgs>> register, Action<T, EventHandler<TArgs>> unregister, Action<T, TArgs> handler)
      : base(source, handler) {
      
      if (register == null) {
        throw new ArgumentNullException(nameof(register));
      }
      
      _unregister = unregister ?? throw new ArgumentNullException(nameof(unregister));
      
      register(source, HandleEvent);
      
    }

    #endregion

  }

}
