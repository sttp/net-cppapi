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

public class SubscriberConnectionCollection : global::System.IDisposable, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerable<SubscriberConnection>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SubscriberConnectionCollection(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SubscriberConnectionCollection obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~SubscriberConnectionCollection() {
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
          CommonPINVOKE.delete_SubscriberConnectionCollection(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SubscriberConnectionCollection(global::System.Collections.IEnumerable c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (SubscriberConnection element in c) {
      this.Add(element);
    }
  }

  public SubscriberConnectionCollection(global::System.Collections.Generic.IEnumerable<SubscriberConnection> c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (SubscriberConnection element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public SubscriberConnection this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < size())
        throw new global::System.ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsSynchronized {
    get {
      return false;
    }
  }

  public void CopyTo(SubscriberConnection[] array)
  {
    CopyTo(0, array, 0, this.Count);
  }

  public void CopyTo(SubscriberConnection[] array, int arrayIndex)
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

  public void CopyTo(int index, SubscriberConnection[] array, int arrayIndex, int count)
  {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (index < 0)
      throw new global::System.ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new global::System.ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

  public SubscriberConnection[] ToArray() {
    SubscriberConnection[] array = new SubscriberConnection[this.Count];
    this.CopyTo(array);
    return array;
  }

  global::System.Collections.Generic.IEnumerator<SubscriberConnection> global::System.Collections.Generic.IEnumerable<SubscriberConnection>.GetEnumerator() {
    return new SubscriberConnectionCollectionEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new SubscriberConnectionCollectionEnumerator(this);
  }

  public SubscriberConnectionCollectionEnumerator GetEnumerator() {
    return new SubscriberConnectionCollectionEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class SubscriberConnectionCollectionEnumerator : global::System.Collections.IEnumerator
    , global::System.Collections.Generic.IEnumerator<SubscriberConnection>
  {
    private SubscriberConnectionCollection collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public SubscriberConnectionCollectionEnumerator(SubscriberConnectionCollection collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public SubscriberConnection Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (SubscriberConnection)currentObject;
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
        currentObject = collectionRef[currentIndex];
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

  public void Clear() {
    CommonPINVOKE.SubscriberConnectionCollection_Clear(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Add(SubscriberConnection x) {
    CommonPINVOKE.SubscriberConnectionCollection_Add(swigCPtr, SubscriberConnection.getCPtr(x));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = CommonPINVOKE.SubscriberConnectionCollection_size(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private uint capacity() {
    uint ret = CommonPINVOKE.SubscriberConnectionCollection_capacity(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void reserve(uint n) {
    CommonPINVOKE.SubscriberConnectionCollection_reserve(swigCPtr, n);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public SubscriberConnectionCollection() : this(CommonPINVOKE.new_SubscriberConnectionCollection__SWIG_0(), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public SubscriberConnectionCollection(SubscriberConnectionCollection other) : this(CommonPINVOKE.new_SubscriberConnectionCollection__SWIG_1(SubscriberConnectionCollection.getCPtr(other)), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public SubscriberConnectionCollection(int capacity) : this(CommonPINVOKE.new_SubscriberConnectionCollection__SWIG_2(capacity), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  private SubscriberConnection getitemcopy(int index) {
    global::System.IntPtr cPtr = CommonPINVOKE.SubscriberConnectionCollection_getitemcopy(swigCPtr, index);
    SubscriberConnection ret = (cPtr == global::System.IntPtr.Zero) ? null : new SubscriberConnection(cPtr, true);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private SubscriberConnection getitem(int index) {
    global::System.IntPtr cPtr = CommonPINVOKE.SubscriberConnectionCollection_getitem(swigCPtr, index);
    SubscriberConnection ret = (cPtr == global::System.IntPtr.Zero) ? null : new SubscriberConnection(cPtr, true);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, SubscriberConnection val) {
    CommonPINVOKE.SubscriberConnectionCollection_setitem(swigCPtr, index, SubscriberConnection.getCPtr(val));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(SubscriberConnectionCollection values) {
    CommonPINVOKE.SubscriberConnectionCollection_AddRange(swigCPtr, SubscriberConnectionCollection.getCPtr(values));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public SubscriberConnectionCollection GetRange(int index, int count) {
    global::System.IntPtr cPtr = CommonPINVOKE.SubscriberConnectionCollection_GetRange(swigCPtr, index, count);
    SubscriberConnectionCollection ret = (cPtr == global::System.IntPtr.Zero) ? null : new SubscriberConnectionCollection(cPtr, true);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, SubscriberConnection x) {
    CommonPINVOKE.SubscriberConnectionCollection_Insert(swigCPtr, index, SubscriberConnection.getCPtr(x));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, SubscriberConnectionCollection values) {
    CommonPINVOKE.SubscriberConnectionCollection_InsertRange(swigCPtr, index, SubscriberConnectionCollection.getCPtr(values));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    CommonPINVOKE.SubscriberConnectionCollection_RemoveAt(swigCPtr, index);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    CommonPINVOKE.SubscriberConnectionCollection_RemoveRange(swigCPtr, index, count);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public static SubscriberConnectionCollection Repeat(SubscriberConnection value, int count) {
    global::System.IntPtr cPtr = CommonPINVOKE.SubscriberConnectionCollection_Repeat(SubscriberConnection.getCPtr(value), count);
    SubscriberConnectionCollection ret = (cPtr == global::System.IntPtr.Zero) ? null : new SubscriberConnectionCollection(cPtr, true);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    CommonPINVOKE.SubscriberConnectionCollection_Reverse__SWIG_0(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Reverse(int index, int count) {
    CommonPINVOKE.SubscriberConnectionCollection_Reverse__SWIG_1(swigCPtr, index, count);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, SubscriberConnectionCollection values) {
    CommonPINVOKE.SubscriberConnectionCollection_SetRange(swigCPtr, index, SubscriberConnectionCollection.getCPtr(values));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
