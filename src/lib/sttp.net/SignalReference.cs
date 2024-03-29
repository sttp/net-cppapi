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

public class SignalReference : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SignalReference(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SignalReference obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~SignalReference() {
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
          CommonPINVOKE.delete_SignalReference(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public System.Guid SignalID {
      set {
        guid_t tempvalue = Common.ParseGuid(value.ToByteArray(), true);
        CommonPINVOKE.SignalReference_SignalID_set(swigCPtr, guid_t.getCPtr(tempvalue));
        if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      }
    
      get {
        global::System.IntPtr cPtr = CommonPINVOKE.SignalReference_SignalID_get(swigCPtr);
        if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
        using (guid_t tempGuid = (cPtr == global::System.IntPtr.Zero) ? null : new guid_t(cPtr, false)) {
          byte[] data = new byte[16];
          Common.GetGuidBytes(tempGuid, data);
          return new System.Guid(data);
        }
      }
    
  }

  public string Acronym {
    set {
      CommonPINVOKE.SignalReference_Acronym_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = CommonPINVOKE.SignalReference_Acronym_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ushort Index {
    set {
      CommonPINVOKE.SignalReference_Index_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      ushort ret = CommonPINVOKE.SignalReference_Index_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SignalKind Kind {
    set {
      CommonPINVOKE.SignalReference_Kind_set(swigCPtr, (int)value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      SignalKind ret = (SignalKind)CommonPINVOKE.SignalReference_Kind_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SignalReference() : this(CommonPINVOKE.new_SignalReference__SWIG_0(), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

  public SignalReference(string signal) : this(CommonPINVOKE.new_SignalReference__SWIG_1(signal), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
