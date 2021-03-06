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

public class DeviceMetadata : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal DeviceMetadata(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DeviceMetadata obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~DeviceMetadata() {
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
          CommonPINVOKE.delete_DeviceMetadata(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public string Acronym {
    set {
      CommonPINVOKE.DeviceMetadata_Acronym_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = CommonPINVOKE.DeviceMetadata_Acronym_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string Name {
    set {
      CommonPINVOKE.DeviceMetadata_Name_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = CommonPINVOKE.DeviceMetadata_Name_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public System.Guid UniqueID {
      set {
        guid_t tempvalue = Common.ParseGuid(value.ToByteArray(), true);
        CommonPINVOKE.DeviceMetadata_UniqueID_set(swigCPtr, guid_t.getCPtr(tempvalue));
        if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      }
    
      get {
        global::System.IntPtr cPtr = CommonPINVOKE.DeviceMetadata_UniqueID_get(swigCPtr);
        if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
        using (guid_t tempGuid = (cPtr == global::System.IntPtr.Zero) ? null : new guid_t(cPtr, false)) {
          byte[] data = new byte[16];
          Common.GetGuidBytes(tempGuid, data);
          return new System.Guid(data);
        }
      }
    
  }

  public ushort AccessID {
    set {
      CommonPINVOKE.DeviceMetadata_AccessID_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      ushort ret = CommonPINVOKE.DeviceMetadata_AccessID_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string ParentAcronym {
    set {
      CommonPINVOKE.DeviceMetadata_ParentAcronym_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = CommonPINVOKE.DeviceMetadata_ParentAcronym_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string ProtocolName {
    set {
      CommonPINVOKE.DeviceMetadata_ProtocolName_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = CommonPINVOKE.DeviceMetadata_ProtocolName_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ushort FramesPerSecond {
    set {
      CommonPINVOKE.DeviceMetadata_FramesPerSecond_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      ushort ret = CommonPINVOKE.DeviceMetadata_FramesPerSecond_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string CompanyAcronym {
    set {
      CommonPINVOKE.DeviceMetadata_CompanyAcronym_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = CommonPINVOKE.DeviceMetadata_CompanyAcronym_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string VendorAcronym {
    set {
      CommonPINVOKE.DeviceMetadata_VendorAcronym_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = CommonPINVOKE.DeviceMetadata_VendorAcronym_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string VendorDeviceName {
    set {
      CommonPINVOKE.DeviceMetadata_VendorDeviceName_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = CommonPINVOKE.DeviceMetadata_VendorDeviceName_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double Longitude {
    set {
      CommonPINVOKE.DeviceMetadata_Longitude_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = CommonPINVOKE.DeviceMetadata_Longitude_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double Latitude {
    set {
      CommonPINVOKE.DeviceMetadata_Latitude_set(swigCPtr, value);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = CommonPINVOKE.DeviceMetadata_Latitude_get(swigCPtr);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public System.DateTime UpdatedOn {
      set {
        datetime_t tempvalue = Common.FromTicks(value.Ticks);
        CommonPINVOKE.DeviceMetadata_UpdatedOn_set(swigCPtr, datetime_t.getCPtr(tempvalue));
        if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      }
    
      get {
        global::System.IntPtr cPtr = CommonPINVOKE.DeviceMetadata_UpdatedOn_get(swigCPtr);
        if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
        using (datetime_t tempDate = (cPtr == global::System.IntPtr.Zero) ? null : new datetime_t(cPtr, false)) {
          return new System.DateTime(Common.ToTicks(tempDate));
        }
      }
    
  }

  public MeasurementMetadataCollection Measurements {
    set {
      CommonPINVOKE.DeviceMetadata_Measurements_set(swigCPtr, MeasurementMetadataCollection.getCPtr(value));
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = CommonPINVOKE.DeviceMetadata_Measurements_get(swigCPtr);
      MeasurementMetadataCollection ret = (cPtr == global::System.IntPtr.Zero) ? null : new MeasurementMetadataCollection(cPtr, false);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public PhasorReferenceCollection Phasors {
    set {
      CommonPINVOKE.DeviceMetadata_Phasors_set(swigCPtr, PhasorReferenceCollection.getCPtr(value));
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = CommonPINVOKE.DeviceMetadata_Phasors_get(swigCPtr);
      PhasorReferenceCollection ret = (cPtr == global::System.IntPtr.Zero) ? null : new PhasorReferenceCollection(cPtr, false);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public DeviceMetadata() : this(CommonPINVOKE.new_DeviceMetadata(), true) {
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
