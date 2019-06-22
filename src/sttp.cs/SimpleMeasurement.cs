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

internal class SimpleMeasurement : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SimpleMeasurement(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SimpleMeasurement obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~SimpleMeasurement() {
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
          CommonPINVOKE.delete_SimpleMeasurement(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ulong ID {
    set {
      CommonPINVOKE.SimpleMeasurement_ID_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      ulong ret = CommonPINVOKE.SimpleMeasurement_ID_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public System.Guid SignalID {
      set {
        guid_t tempvalue = Common.ParseGuid(value.ToByteArray(), true);
        CommonPINVOKE.SimpleMeasurement_SignalID_set(swigCPtr, guid_t.getCPtr(tempvalue));
        if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      }
    
      get {
        global::System.IntPtr cPtr = CommonPINVOKE.SimpleMeasurement_SignalID_get(swigCPtr);
        if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
        using (guid_t tempGuid = (cPtr == global::System.IntPtr.Zero) ? null : new guid_t(cPtr, false)) {
          byte[] data = new byte[16];
          Common.GetGuidBytes(tempGuid, data);
          return new System.Guid(data);
        }
      }
    
  }

  public double Value {
    set {
      CommonPINVOKE.SimpleMeasurement_Value_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = CommonPINVOKE.SimpleMeasurement_Value_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double Adder {
    set {
      CommonPINVOKE.SimpleMeasurement_Adder_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = CommonPINVOKE.SimpleMeasurement_Adder_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double Multiplier {
    set {
      CommonPINVOKE.SimpleMeasurement_Multiplier_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = CommonPINVOKE.SimpleMeasurement_Multiplier_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public long Timestamp {
    set {
      CommonPINVOKE.SimpleMeasurement_Timestamp_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      long ret = CommonPINVOKE.SimpleMeasurement_Timestamp_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public MeasurementStateFlags Flags {
    set {
      CommonPINVOKE.SimpleMeasurement_Flags_set(swigCPtr, (int)value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      MeasurementStateFlags ret = (MeasurementStateFlags)CommonPINVOKE.SimpleMeasurement_Flags_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SimpleMeasurement() : this(CommonPINVOKE.new_SimpleMeasurement(), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
