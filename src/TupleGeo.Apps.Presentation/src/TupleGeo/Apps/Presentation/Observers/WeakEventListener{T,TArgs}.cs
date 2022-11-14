
//
// Author: David Milligan
// Source: https://github.com/davidmilligan/WeakEventListener
//

#region Imported Namespaces

using System.Reflection;

#endregion

namespace TupleGeo.Apps.Presentation.Observers {

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="TArgs"></typeparam>
  internal class WeakEventListener<T, TArgs> : WeakEventListenerBase<T, TArgs>
    where T : class
    where TArgs : EventArgs {

    private readonly EventInfo _eventInfo;

    #region Protected Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    protected override void StopListening(T source) {
      if (_eventInfo.EventHandlerType == typeof(EventHandler<TArgs>)) {
        _eventInfo.RemoveEventHandler(source, new EventHandler<TArgs>(HandleEvent));
      }
      else {
        _eventInfo.RemoveEventHandler(source, Delegate.CreateDelegate(_eventInfo.EventHandlerType, this, nameof(HandleEvent)));
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventName"></param>
    /// <param name="handler"></param>
    /// <exception cref="ArgumentException"></exception>
    public WeakEventListener(T source, string eventName, Action<T, TArgs> handler)
      : base(source, handler) {
      
      _eventInfo = source.GetType().GetEvent(eventName) ?? throw new ArgumentException("Unknown Event Name", nameof(eventName));

      if (_eventInfo.EventHandlerType == typeof(EventHandler<TArgs>)) {
        _eventInfo.AddEventHandler(source, new EventHandler<TArgs>(HandleEvent));
      }
      else {
        // the event type isn't just an EventHandler<> so we have to create the delegate using reflection
        _eventInfo.AddEventHandler(source, Delegate.CreateDelegate(_eventInfo.EventHandlerType, this, nameof(HandleEvent)));
      }
    }

    #endregion

  }

}
