
//
// Author: David Milligan
// Source: https://github.com/davidmilligan/WeakEventListener
//

#region Imported Namespaces

using System.Collections.Specialized;

#endregion

namespace TupleGeo.Apps.Presentation.Observers {
  
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  internal class CollectionChangedWeakEventListener<T> : WeakEventListenerBase<T, NotifyCollectionChangedEventArgs>
    where T : class,
    INotifyCollectionChanged {

    #region Protected Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    protected override void StopListening(T source) => source.CollectionChanged -= HandleEvent;

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="handler"></param>
    public CollectionChangedWeakEventListener(T source, Action<T, NotifyCollectionChangedEventArgs> handler)
      : base(source, handler) {

      source.CollectionChanged += HandleEvent;

    }

    #endregion

  }

}
