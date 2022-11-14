
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
  internal abstract class WeakEventListenerBase<T, TArgs> : IWeakEventListener
    where T : class
    where TArgs : EventArgs {
    
    #region Public Properties
    
    /// <summary>
    /// 
    /// </summary>
    public bool IsAlive => _handler.TryGetTarget(out var _) && _source.TryGetTarget(out var __);

    private readonly WeakReference<T> _source;
    
    /// <summary>
    /// 
    /// </summary>
    public object Source {
      get {
        if (_source.TryGetTarget(out var source)) {
          return source;
        }
        return null;
      }
    }

    private readonly WeakReference<Action<T, TArgs>> _handler;
    
    /// <summary>
    /// 
    /// </summary>
    public Delegate Handler {
      get {
        if (_handler.TryGetTarget(out var handler)) {
          return handler;
        }
        return null;
      }
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="handler"></param>
    /// <exception cref="ArgumentNullException"></exception>
    protected WeakEventListenerBase(T source, Action<T, TArgs> handler) {
      _source = new WeakReference<T>(source ?? throw new ArgumentNullException(nameof(source)));
      _handler = new WeakReference<Action<T, TArgs>>(handler ?? throw new ArgumentNullException(nameof(handler)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void HandleEvent(object sender, TArgs e) {
      if (_handler.TryGetTarget(out var handler)) {
        handler(sender as T, e);
      }
      else {
        StopListening();
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    protected abstract void StopListening(T source);

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    public void StopListening() {
      if (_source.TryGetTarget(out var source)) {
        StopListening(source);
      }
    }

    #endregion
    
  }
  
}
