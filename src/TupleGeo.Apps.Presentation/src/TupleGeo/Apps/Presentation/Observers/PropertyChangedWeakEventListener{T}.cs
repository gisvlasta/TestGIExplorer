
//
// Author: David Milligan
// Source: https://github.com/davidmilligan/WeakEventListener
//

#region Imported Namespaces

using System.ComponentModel;

#endregion

namespace TupleGeo.Apps.Presentation.Observers {
  
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  internal class PropertyChangedWeakEventListener<T> : WeakEventListenerBase<T, PropertyChangedEventArgs>
    where T : class,
    INotifyPropertyChanged {

    #region Protected Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    protected override void StopListening(T source) => source.PropertyChanged -= HandleEvent;

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="handler"></param>
    public PropertyChangedWeakEventListener(T source, Action<T, PropertyChangedEventArgs> handler)
      : base(source, handler) {

      source.PropertyChanged += HandleEvent;

    }

    #endregion

  }
  
}
