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


    // Measurement structure uses custom marshaling as an optimization

    // Fundamental data type representing a measurement in STTP
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public unsafe struct Measurement
    {
        // Identification number used in human-readable measurement key.
        public ulong ID;

        // Measurement's globally unique identifier bytes.
        public fixed byte SignalID[16];

        // Instantaneous value of the measurement.
        public double Value;

        // Additive value modifier.
        public double Adder;

        // Multiplicative value modifier.
        public double Multiplier;

        // The time, in ticks, that this measurement was taken.
        public long Timestamp;

        // Flags indicating the state of the measurement as reported by the device that took it.
        public MeasurementStateFlags Flags;
    }

    public static class MeasurementExtensions
    {
        public static unsafe System.Guid GetSignalID(this Measurement measurement)
        {
            byte* data = measurement.SignalID;

            return new System.Guid
            (
                /* a */ *(uint*)data[0],    // First 4 bytes of GUID
                /* b */ *(ushort*)data[4],  // Next 2 bytes of GUID
                /* c */ *(ushort*)data[6],  // Next 2 bytes of GUID
                /* d */ data[8],            // Remaining bytes
                /* e */ data[9],
                /* f */ data[10],
                /* g */ data[11],
                /* h */ data[12],
                /* i */ data[13],
                /* j */ data[14],
                /* k */ data[15]
            );
        }

        public static unsafe void SetSignalID(this Measurement measurement, System.Guid value)
        {
            byte* data = measurement.SignalID;
            byte[] bytes = value.ToByteArray();

            for (int i = 0; i < 16; i++)
                data[i] = bytes[i];
        }

        public static System.DateTime GetTimestamp(this Measurement measurement) => new System.DateTime(measurement.Timestamp);

        public static void SetTimestamp(this Measurement measurement, System.DateTime value) => measurement.Timestamp = value.Ticks;

        public static double AdjustedValue(this Measurement measurement) => measurement.Value * measurement.Multiplier + measurement.Adder;
     }

    public class SubscriberInstance : SubscriberInstanceBase
    {
        internal override unsafe void ReceivedNewMeasurements(SimpleMeasurement simpleMeasurementArray, int length)
        {
            Measurement* measurements = (Measurement*)SimpleMeasurement.getCPtr(simpleMeasurementArray).Handle.ToPointer();

            if (measurements == null)
                return;

            ReceivedNewMeasurements(measurements, length);
        }

        public virtual unsafe void ReceivedNewMeasurements(Measurement* measurements, int length)
        {
        }
    }

public class Common {
  internal unsafe static void GetGuidBytes(guid_t value, byte[] data) {
    fixed ( byte* swig_ptrTo_data = data ) {
    {
      CommonPINVOKE.GetGuidBytes(guid_t.getCPtr(value), (global::System.IntPtr)swig_ptrTo_data);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    }
    }
  }

  public static string GetSubscriberConnectionIPAddress(SubscriberConnection connection) {
    string ret = CommonPINVOKE.GetSubscriberConnectionIPAddress(SubscriberConnection.getCPtr(connection));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  internal static decimal_t ParseDecimal(string value) {
    decimal_t ret = new decimal_t(CommonPINVOKE.ParseDecimal(value), true);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  internal static string ToString(decimal_t value) {
    string ret = CommonPINVOKE.ToString(decimal_t.getCPtr(value));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  internal unsafe static guid_t ParseGuid(byte[] data, bool swapEndianness) {
    fixed ( byte* swig_ptrTo_data = data ) {
    {
      guid_t ret = new guid_t(CommonPINVOKE.ParseGuid((global::System.IntPtr)swig_ptrTo_data, swapEndianness), true);
      if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    }
    }
  }

  internal static datetime_t FromTicks(long ticks) {
    datetime_t ret = new datetime_t(CommonPINVOKE.FromTicks(ticks), true);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  internal static long ToTicks(datetime_t time) {
    long ret = CommonPINVOKE.ToTicks(datetime_t.getCPtr(time));
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string GetSignalTypeAcronym(SignalKind kind, char phasorType) {
    string ret = CommonPINVOKE.GetSignalTypeAcronym__SWIG_0((int)kind, phasorType);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string GetSignalTypeAcronym(SignalKind kind) {
    string ret = CommonPINVOKE.GetSignalTypeAcronym__SWIG_1((int)kind);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string GetEngineeringUnits(string signalType) {
    string ret = CommonPINVOKE.GetEngineeringUnits(signalType);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string GetProtocolType(string protocolName) {
    string ret = CommonPINVOKE.GetProtocolType(protocolName);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static SignalKind ParseSignalKind(string acronym) {
    SignalKind ret = (SignalKind)CommonPINVOKE.ParseSignalKind(acronym);
    if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
