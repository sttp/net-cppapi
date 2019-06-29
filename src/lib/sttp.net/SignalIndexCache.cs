//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.0
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace sttp {

public class SignalIndexCache : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal SignalIndexCache(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SignalIndexCache obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~SignalIndexCache() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnBase) {
          swigCMemOwnBase = false;
          CommonPINVOKE.delete_SignalIndexCache(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public bool Contains(int signalIndex) {
    bool ret = CommonPINVOKE.SignalIndexCache_Contains(swigCPtr, signalIndex);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public System.Guid GetSignalID(int signalIndex) {
      global::System.IntPtr cPtr = CommonPINVOKE.SignalIndexCache_GetSignalID(swigCPtr, signalIndex);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      using (guid_t tempGuid = (cPtr == global::System.IntPtr.Zero) ? null : new guid_t(cPtr, false)) {
        byte[] data = new byte[16];
        Common.GetGuidBytes(tempGuid, data);
        return new System.Guid(data);
      }
    }

  public string GetSource(int signalIndex) {
    string ret = CommonPINVOKE.SignalIndexCache_GetSource(swigCPtr, signalIndex);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ulong GetID(int signalIndex) {
    ulong ret = CommonPINVOKE.SignalIndexCache_GetID(swigCPtr, signalIndex);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetSignalIndex(System.Guid signalID) {
    guid_t tempsignalID = Common.ParseGuid(signalID.ToByteArray(), true);
    {
      int ret = CommonPINVOKE.SignalIndexCache_GetSignalIndex(swigCPtr, guid_t.getCPtr(tempsignalID));
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    }
  }

  public uint Count() {
    uint ret = CommonPINVOKE.SignalIndexCache_Count(swigCPtr);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool GetSignalIDs(GuidCollection signalIDs) {
    bool ret = CommonPINVOKE.SignalIndexCache_GetSignalIDs(swigCPtr, GuidCollection.getCPtr(signalIDs));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

        public System.Guid[] GetSignalIDs()
        {
            using (GuidCollection guids = new GuidCollection())
            {
                GetSignalIDs(guids);
                return System.Linq.Enumerable.ToArray(guids);
            }
        }
    
  public SignalIndexCache() : this(CommonPINVOKE.new_SignalIndexCache(), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
