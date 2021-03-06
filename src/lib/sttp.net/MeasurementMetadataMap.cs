//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace sttp {

public class MeasurementMetadataMap : global::System.IDisposable 
    , global::System.Collections.Generic.IDictionary<System.Guid, MeasurementMetadata>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal MeasurementMetadataMap(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MeasurementMetadataMap obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~MeasurementMetadataMap() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          CommonPINVOKE.delete_MeasurementMetadataMap(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }


  public MeasurementMetadata this[System.Guid key] {
    get {
      return getitem(key);
    }

    set {
      setitem(key, value);
    }
  }

  public bool TryGetValue(System.Guid key, out MeasurementMetadata value) {
    if (this.ContainsKey(key)) {
      value = this[key];
      return true;
    }
    value = default(MeasurementMetadata);
    return false;
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public global::System.Collections.Generic.ICollection<System.Guid> Keys {
    get {
      global::System.Collections.Generic.ICollection<System.Guid> keys = new global::System.Collections.Generic.List<System.Guid>();
      int size = this.Count;
      if (size > 0) {
        global::System.IntPtr iter = create_iterator_begin();
        for (int i = 0; i < size; i++) {
          keys.Add(get_next_key(iter));
        }
        destroy_iterator(iter);
      }
      return keys;
    }
  }

  public global::System.Collections.Generic.ICollection<MeasurementMetadata> Values {
    get {
      global::System.Collections.Generic.ICollection<MeasurementMetadata> vals = new global::System.Collections.Generic.List<MeasurementMetadata>();
      foreach (global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata> pair in this) {
        vals.Add(pair.Value);
      }
      return vals;
    }
  }

  public void Add(global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata> item) {
    Add(item.Key, item.Value);
  }

  public bool Remove(global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata> item) {
    if (Contains(item)) {
      return Remove(item.Key);
    } else {
      return false;
    }
  }

  public bool Contains(global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata> item) {
    if (this[item.Key] == item.Value) {
      return true;
    } else {
      return false;
    }
  }

  public void CopyTo(global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata>[] array) {
    CopyTo(array, 0);
  }

  public void CopyTo(global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata>[] array, int arrayIndex) {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (arrayIndex+this.Count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");

    global::System.Collections.Generic.IList<System.Guid> keyList = new global::System.Collections.Generic.List<System.Guid>(this.Keys);
    for (int i = 0; i < keyList.Count; i++) {
      System.Guid currentKey = keyList[i];
      array.SetValue(new global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata>(currentKey, this[currentKey]), arrayIndex+i);
    }
  }

  global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata>> global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata>>.GetEnumerator() {
    return new MeasurementMetadataMapEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new MeasurementMetadataMapEnumerator(this);
  }

  public MeasurementMetadataMapEnumerator GetEnumerator() {
    return new MeasurementMetadataMapEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class MeasurementMetadataMapEnumerator : global::System.Collections.IEnumerator,
      global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata>>
  {
    private MeasurementMetadataMap collectionRef;
    private global::System.Collections.Generic.IList<System.Guid> keyCollection;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public MeasurementMetadataMapEnumerator(MeasurementMetadataMap collection) {
      collectionRef = collection;
      keyCollection = new global::System.Collections.Generic.List<System.Guid>(collection.Keys);
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata> Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata>)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object global::System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        System.Guid currentKey = keyCollection[currentIndex];
        currentObject = new global::System.Collections.Generic.KeyValuePair<System.Guid, MeasurementMetadata>(currentKey, collectionRef[currentKey]);
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new global::System.InvalidOperationException("Collection modified.");
      }
    }

    public void Dispose() {
      currentIndex = -1;
      currentObject = null;
    }
  }


  public MeasurementMetadataMap() : this(CommonPINVOKE.new_MeasurementMetadataMap__SWIG_0(), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public MeasurementMetadataMap(MeasurementMetadataMap other) : this(CommonPINVOKE.new_MeasurementMetadataMap__SWIG_1(MeasurementMetadataMap.getCPtr(other)), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = CommonPINVOKE.MeasurementMetadataMap_size(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool empty() {
    bool ret = CommonPINVOKE.MeasurementMetadataMap_empty(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Clear() {
    CommonPINVOKE.MeasurementMetadataMap_Clear(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  private MeasurementMetadata getitem(System.Guid key) {
    guid_t tempkey = Common.ParseGuid(key.ToByteArray(), true);
    {
      global::System.IntPtr cPtr = CommonPINVOKE.MeasurementMetadataMap_getitem(swigCPtr, guid_t.getCPtr(tempkey));
      MeasurementMetadata ret = (cPtr == global::System.IntPtr.Zero) ? null : new MeasurementMetadata(cPtr, true);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    }
  }

  private void setitem(System.Guid key, MeasurementMetadata x) {
    guid_t tempkey = Common.ParseGuid(key.ToByteArray(), true);
    {
      CommonPINVOKE.MeasurementMetadataMap_setitem(swigCPtr, guid_t.getCPtr(tempkey), MeasurementMetadata.getCPtr(x));
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    }
  }

  public bool ContainsKey(System.Guid key) {
    guid_t tempkey = Common.ParseGuid(key.ToByteArray(), true);
    {
      bool ret = CommonPINVOKE.MeasurementMetadataMap_ContainsKey(swigCPtr, guid_t.getCPtr(tempkey));
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    }
  }

  public void Add(System.Guid key, MeasurementMetadata value) {
    guid_t tempkey = Common.ParseGuid(key.ToByteArray(), true);
    {
      CommonPINVOKE.MeasurementMetadataMap_Add(swigCPtr, guid_t.getCPtr(tempkey), MeasurementMetadata.getCPtr(value));
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    }
  }

  public bool Remove(System.Guid key) {
    guid_t tempkey = Common.ParseGuid(key.ToByteArray(), true);
    {
      bool ret = CommonPINVOKE.MeasurementMetadataMap_Remove(swigCPtr, guid_t.getCPtr(tempkey));
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    }
  }

  private global::System.IntPtr create_iterator_begin() {
    global::System.IntPtr ret = CommonPINVOKE.MeasurementMetadataMap_create_iterator_begin(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private System.Guid get_next_key(global::System.IntPtr swigiterator) {
      global::System.IntPtr cPtr = CommonPINVOKE.MeasurementMetadataMap_get_next_key(swigCPtr, swigiterator);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      using (guid_t tempGuid = (cPtr == global::System.IntPtr.Zero) ? null : new guid_t(cPtr, false)) {
        byte[] data = new byte[16];
        Common.GetGuidBytes(tempGuid, data);
        return new System.Guid(data);
      }
    }

  private void destroy_iterator(global::System.IntPtr swigiterator) {
    CommonPINVOKE.MeasurementMetadataMap_destroy_iterator(swigCPtr, swigiterator);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
