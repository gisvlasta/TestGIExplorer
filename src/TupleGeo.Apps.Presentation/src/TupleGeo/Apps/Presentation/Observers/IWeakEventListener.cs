
// Author: David Milligan
// Source: https://github.com/davidmilligan/WeakEventListener

#region Imported Namespaces

#endregion

namespace TupleGeo.Apps.Presentation.Observers {

  /// <summary>
  /// 
  /// </summary>
  internal interface IWeakEventListener {

    /// <summary>
    /// 
    /// </summary>
    bool IsAlive {
      get;
    }

    /// <summary>
    /// 
    /// </summary>
    object Source {
      get;
    }

    /// <summary>
    /// 
    /// </summary>
    Delegate Handler {
      get;
    }
    
    /// <summary>
    /// 
    /// </summary>
    void StopListening();
    
  }

}
