//******************************************************************************************************
//  sttp.i - Gbtc
//
//  Copyright © 2019, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
//  file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  06/22/2019 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

%module(directors="1") Common
%include "stdint.i" 
%include "std_string.i"
%include "std_map.i"
%include "std_vector.i"
%include <boost_shared_ptr.i>
%include "exception.i"
%include "arrays_csharp.i"

%exception
{
  try
  {
      $action
  }
  catch (const std::exception& e)
  {
      SWIG_exception(SWIG_RuntimeError, e.what());
  }
}

%define SWIGEXCODE3 "\n        if ($imclassname.SWIGPendingException.Pending) throw $imclassname.SWIGPendingException.Retrieve();" %enddef

%{
// Include STTP library header files
#include "../cppapi/src/lib/CommonTypes.h"
#include "../cppapi/src/lib/Nullable.h"
#include "../cppapi/src/lib/data/DataSet.h"
#include "../cppapi/src/lib/transport/TransportTypes.h"
#include "../cppapi/src/lib/transport/SubscriberInstance.h"
#include "../cppapi/src/lib/transport/PublisherInstance.h"
%}

// Hijack module class modifiers to inject needed target classes into root namespace
%pragma(csharp) moduleclassmodifiers = %{
    // Measurement structure uses custom marshaling as an optimization

    // Fundamental data type representing a measurement in STTP
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public unsafe struct Measurement
    {
        // Measurement's globally unique identifier bytes.
        public fixed byte SignalID[16];

        // Instantaneous value of the measurement.
        public double Value;

        // The time, in ticks, that this measurement was taken.
        public long Timestamp;

        // Flags indicating the state of the measurement as reported by the device that took it.
        public MeasurementStateFlags Flags;

        public Measurement(System.Guid signalID, System.DateTime timestamp, double value = double.NaN, MeasurementStateFlags flags = MeasurementStateFlags.Normal)
        {
            Value = value;
            Timestamp = timestamp.Ticks;
            Flags = flags;

            this.SetSignalID(signalID);
        }

        public Measurement(System.Guid signalID, long timestamp, double value = double.NaN, MeasurementStateFlags flags = MeasurementStateFlags.Normal)
        {
            Value = value;
            Timestamp = timestamp;
            Flags = flags;

            this.SetSignalID(signalID);
        }
    }

    public static class MeasurementExtensions
    {
        public static unsafe System.Guid GetSignalID(this ref Measurement measurement)
        {
            fixed (byte* data = measurement.SignalID)
            {
                byte* swap = stackalloc byte[8];
                byte* copy = stackalloc byte[8];

                for (int i = 0; i < 8; i++)
                    copy[i] = data[i];
            
                // The following uint32 and two uint16 values are little-endian encoded in Microsoft implementations,
                // boost follows RFC encoding rules and encodes the bytes as big-endian. For proper Guid interpretation
                // by .NET applications the following bytes must be swapped before deserialization:
                swap[0] = copy[3];
                swap[1] = copy[2];
                swap[2] = copy[1];
                swap[3] = copy[0];

                swap[4] = copy[5];
                swap[5] = copy[4];

                swap[6] = copy[7];
                swap[7] = copy[6];

                return new System.Guid
                (
                    /* a */ *(uint*)swap,           // First 4 bytes of GUID
                    /* b */ *(ushort*)(swap + 4),   // Next 2 bytes of GUID
                    /* c */ *(ushort*)(swap + 6),   // Next 2 bytes of GUID
                    /* d */ data[8],                // Remaining bytes
                    /* e */ data[9],
                    /* f */ data[10],
                    /* g */ data[11],
                    /* h */ data[12],
                    /* i */ data[13],
                    /* j */ data[14],
                    /* k */ data[15]
                );
            }
        }

        public static unsafe void SetSignalID(this ref Measurement measurement, System.Guid value)
        {
            fixed (byte* data = measurement.SignalID)
            {
                byte* copy = stackalloc byte[8];
            
                fixed (byte* source = value.ToByteArray())
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (i < 8)
                            copy[i] = source[i];
                        else
                            data[i] = source[i];
                    }
                }

                // Convert Microsoft encoding to RFC
                data[0] = copy[3];
                data[1] = copy[2];
                data[3] = copy[0];
                data[2] = copy[1];

                data[4] = copy[5];
                data[5] = copy[4];

                data[6] = copy[7];
                data[7] = copy[6];
            }
        }

        public static System.DateTime GetDateTime(this ref Measurement measurement) => new System.DateTime(measurement.Timestamp);

        public static void SetDateTime(this ref Measurement measurement, System.DateTime value) => measurement.Timestamp = value.Ticks;
     }

    public class SubscriberInstance : SubscriberInstanceBase
    {
        public SubscriberInstance()
        {
            System.Reflection.Assembly assembly = typeof(SubscriberInstance).Assembly;
            System.Reflection.AssemblyName assemblyInfo = assembly.GetName();
            System.DateTime buildDate = System.IO.File.GetLastWriteTime(assembly.Location);

            GetAssemblyInfo(out string source, out string version, out string updatedOn);
            string wrapperAssemblyInfo = $", wrapping {source} version {version} updated on {updatedOn}";
            SetAssemblyInfo(assemblyInfo.Name, $"{assemblyInfo.Version.Major}.{assemblyInfo.Version.Minor}.{assemblyInfo.Version.Build}", $"{buildDate:yyyy-MM-dd HH:mm:ss}{wrapperAssemblyInfo}");
        }

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

        public new static readonly string SubscribeAllExpression = SubscriberInstanceBase.SubscribeAllExpression;
        public new static readonly string SubscribeAllNoStatsExpression = SubscriberInstanceBase.SubscribeAllNoStatsExpression;
        public new static readonly string FilterMetadataStatsExpression = SubscriberInstanceBase.FilterMetadataStatsExpression;
    }

public class%}

// Define SWIG types to wrap in target language
namespace sttp
{
    // Mark C++ library specific class pointer wrappers as internal
    %typemap(csclassmodifiers) decimal_t, decimal_t*, decimal_t&, decimal_t[], decimal_t (CLASS::*) "internal class"
    %typemap(csclassmodifiers) datetime_t, datetime_t*, datetime_t&, datetime_t[], datetime_t (CLASS::*) "internal class"
    %typemap(csclassmodifiers) Guid, Guid*, Guid&, Guid[], Guid (CLASS::*) "internal class"

    typedef float float32_t;
    typedef double float64_t;

    class decimal_t
    {
        decimal_t();
        decimal_t(const decimal_t&);
    };

    %rename(guid_t) Guid;
    class Guid
    {
        Guid();
        Guid(const Guid&);
    };

    class datetime_t
    {
        datetime_t();
        datetime_t(const datetime_t&);
    };

    namespace data
    {
        class DataSet;
        class DataTable;
        class DataRow;
        class DataColumn;
    }

    namespace transport
    {
        struct SimpleMeasurement;
        struct Measurement;
        struct MeasurementMetadata;
        struct PhasorMetadata;
        struct PhasorReference;
        struct DeviceMetadata;
        struct ConfigurationFrame;
        class SignalIndexCache;

        %rename(SubscriberInstanceBase) SubscriberInstance;
        class SubscriberInstance;
        class SubscriberConnection;
        class PublisherInstance;
    }
}

%shared_ptr(sttp::data::DataSet)
%shared_ptr(sttp::data::DataTable)
%shared_ptr(sttp::data::DataRow)
%shared_ptr(sttp::data::DataColumn)

%shared_ptr(sttp::transport::MeasurementMetadata)
%shared_ptr(sttp::transport::PhasorMetadata)
%shared_ptr(sttp::transport::PhasorReference)
%shared_ptr(sttp::transport::DeviceMetadata)
%shared_ptr(sttp::transport::ConfigurationFrame)
%shared_ptr(sttp::transport::SignalIndexCache)
%shared_ptr(sttp::transport::SubscriberInstance)
%shared_ptr(sttp::transport::SubscriberConnection)
%shared_ptr(sttp::transport::PublisherInstance)

%csmethodmodifiers GetGuidBytes "internal unsafe";
%apply uint8_t FIXED[] {uint8_t* data}
%inline
%{
    // Get byte array representing a globally unique identifier
    void GetGuidBytes(const sttp::Guid& value, uint8_t* data)
    {
        memcpy(data, value.data, 16);

        uint8_t copy[8];

        for (uint32_t i = 0; i < 8; i++)
            copy[i] = data[i];

        // The following uint32 and two uint16 values are little-endian encoded in Microsoft implementations,
        // boost follows RFC encoding rules and encodes the bytes as big-endian. For proper Guid interpretation
        // by .NET applications the following bytes must be swapped before deserialization:
        data[3] = copy[0];
        data[2] = copy[1];
        data[1] = copy[2];
        data[0] = copy[3];

        data[4] = copy[5];
        data[5] = copy[4];

        data[6] = copy[7];
        data[7] = copy[6];
    }

    std::string GetSubscriberConnectionIPAddress(const boost::shared_ptr<sttp::transport::SubscriberConnection>& connection)
    {
        return connection->GetIPAddress().to_string();
    }
%}

namespace sttp
{
    // Define nullable class (minimal definition)
    %typemap(csclassmodifiers) Nullable<T> "internal class"
    template <class T>
    class Nullable {
    public:
        Nullable();
        Nullable(const T& value);
        bool HasValue() const;
        const T& GetValueOrDefault() const;
    };

    %typemap(cstype) Nullable<T>, const Nullable<T> & "$typemap(cstype, T)?"
    %naturalvar Nullable<T>;

    %template(NullableString) Nullable<std::string>;
    %template(NullableBool) Nullable<bool>;
    %template(NullableDateTime) Nullable<datetime_t>;
    %template(NullableFloat32) Nullable<float32_t>;
    %template(NullableFloat64) Nullable<float64_t>;
    %template(NullableDecimal) Nullable<decimal_t>;
    %template(NullableGuid) Nullable<Guid>;
    %template(NullableInt8) Nullable<int8_t>;
    %template(NullableInt16) Nullable<int16_t>;
    %template(NullableInt32) Nullable<int32_t>;
    %template(NullableInt64) Nullable<int64_t>;
    %template(NullableUInt8) Nullable<uint8_t>;
    %template(NullableUInt16) Nullable<uint16_t>;
    %template(NullableUInt32) Nullable<uint32_t>;
    %template(NullableUInt64) Nullable<uint64_t>;

    // The boost decimal type has a very complex internal representation, although slower,
    // it's safer convert the value to and from a string:

    // Parse decimal_t from string
    %csmethodmodifiers ParseDecimal "internal";
    decimal_t ParseDecimal(const std::string& value);

    %csmethodmodifiers ToString "internal";
    std::string ToString(const decimal_t& value);

    // Map decimal_t (boost::multiprecision::cpp_dec_float_100) to System.Decimal
    %typemap(cstype) decimal_t, const decimal_t& "decimal"
    %typemap(csin, 
        pre="    $csclassname temp$csinput = $module.ParseDecimal($csinput.ToString());"
    )
    decimal_t "$csclassname.getCPtr(temp$csinput)"
    %typemap(csin, 
        pre="    $csclassname temp$csinput = $module.ParseDecimal($csinput.ToString());"
    )
    const decimal_t& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype) decimal_t& "out decimal"
    %typemap(csin, 
        pre="    using ($csclassname temp$csinput = new $csclassname()) {", 
        post="      $csinput = System.Convert.ToDecimal($module.ToString(temp$csinput));",
        terminator="    }",
        cshin="out $csinput"
    )
    decimal_t& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype, out="decimal") decimal_t* "ref decimal"
    %typemap(csin,
        pre="    using ($csclassname temp$csinput = $module.ParseDecimal($csinput.ToString())) {",
        post="      $csinput = System.Convert.ToDecimal($module.ToString(temp$csinput));",
        terminator="    }",
        cshin="ref $csinput"
    )
    decimal_t* "$csclassname.getCPtr(temp$csinput)"

    %typemap(csvarin, excode=SWIGEXCODE3) decimal_t*, decimal_t&, decimal_t
    %{
      set {
        $csclassname temp$csinput = $module.ParseDecimal($csinput.ToString());
        $imcall;$excode
      }
    %}

    %typemap(csvarout, excode=SWIGEXCODE3) decimal_t*, decimal_t&, decimal_t
    %{
      get {
        global::System.IntPtr cPtr = $imcall;$excode
        using ($csclassname tempDate = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
          return System.Convert.ToDecimal($module.ToString(tempDate));
        }
      }
    %}

    %typemap(cstype) decimal_t, const decimal_t "decimal"
    %typemap(csout, excode=SWIGEXCODE2) decimal_t, const decimal_t, const decimal_t&
    {
      global::System.IntPtr cPtr = $imcall;$excode
      using ($csclassname tempDate = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
        return System.Convert.ToDecimal($module.ToString(tempDate));
      }
    }

    // Convert 16 contiguous bytes into a globally unique identifier
    %csmethodmodifiers ParseGuid "internal unsafe";
    %apply uint8_t FIXED[] {const uint8_t* data}
    extern Guid ParseGuid(const uint8_t* data, bool swapEndianness);

    // Map Guid (boost::uuids::uuid) to System.Guid
    %typemap(cstype) Guid, const Guid& "System.Guid"
    %typemap(csin,
        pre="    $csclassname temp$csinput = $module.ParseGuid($csinput.ToByteArray(), true);"
    )
    Guid "$csclassname.getCPtr(temp$csinput)"
    %typemap(csin,
        pre="    $csclassname temp$csinput = $module.ParseGuid($csinput.ToByteArray(), true);"
    )
    const Guid& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype) Guid& "out System.Guid"
    %typemap(csin,
        pre="    using ($csclassname temp$csinput = new $csclassname()) {", 
        post=
            "      byte[] $csinput_data = new byte[16];\n"
            "      $module.GetGuidBytes(temp$csinput, $csinput_data);\n"
            "      $csinput = new System.Guid($csinput_data);",
        terminator="    }",
        cshin="out $csinput"
    )
    Guid& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype, out="System.Guid") Guid* "ref System.Guid"
    %typemap(csin,
        pre="    using ($csclassname temp$csinput = $module.ParseGuid($csinput.ToByteArray(), true)) {",
        post=
            "      byte[] $csinput_data = new byte[16];\n"
            "      $module.GetGuidBytes(temp$csinput), $csinput_data);\n"
            "      $csinput = new System.Guid($csinput_data);",
        terminator="    }",
        cshin="ref $csinput"
    )
    Guid* "$csclassname.getCPtr(temp$csinput)"

    %typemap(csvarin, excode=SWIGEXCODE3) Guid*, Guid&, Guid
    %{
      set {
        $csclassname temp$csinput = $module.ParseGuid($csinput.ToByteArray(), true);
        $imcall;$excode
      }
    %}

    %typemap(csvarout, excode=SWIGEXCODE3) Guid*, Guid&, Guid
    %{
      get {
        global::System.IntPtr cPtr = $imcall;$excode
        using ($csclassname tempGuid = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
          byte[] data = new byte[16];
          $module.GetGuidBytes(tempGuid, data);
          return new System.Guid(data);
        }
      }
    %}

    %typemap(cstype) Guid, const Guid "System.Guid"
    %typemap(csout, excode=SWIGEXCODE2) Guid, const Guid, const Guid&
    {
      global::System.IntPtr cPtr = $imcall;$excode
      using ($csclassname tempGuid = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
        byte[] data = new byte[16];
        $module.GetGuidBytes(tempGuid, data);
        return new System.Guid(data);
      }
    }

    // Converts a timestamp, in Ticks, to datetime_t
    %csmethodmodifiers FromTicks "internal";
    datetime_t FromTicks(int64_t ticks);

    // Converts a datetime_t to Ticks
    %csmethodmodifiers ToTicks "internal";
    int64_t ToTicks(const datetime_t& time);

    // Map datetime_t (boost::posix_time::ptime) to System.DateTime
    %typemap(cstype) datetime_t, const datetime_t& "System.DateTime"
    %typemap(csin, 
        pre="    $csclassname temp$csinput = $module.FromTicks($csinput.Ticks);"
    )
    datetime_t "$csclassname.getCPtr(temp$csinput)"
    %typemap(csin, 
        pre="    $csclassname temp$csinput = $module.FromTicks($csinput.Ticks);"
    )
    const datetime_t& "$csclassname.getCPtr(temp$csinput)"
    %typemap(csdirectorin,
        pre = 
            "    $csclassname tempDate = new datetime_t($iminput, false);\n"
            "    System.DateTime temp$iminput  = new System.DateTime($module.ToTicks(tempDate));"
    )
    datetime_t "temp$iminput"

    %typemap(cstype) datetime_t& "out System.DateTime"
    %typemap(csin, 
        pre="    using ($csclassname temp$csinput = new $csclassname()) {", 
        post="      $csinput = new System.DateTime($module.ToTicks(temp$csinput));",
        terminator="    }",
        cshin="out $csinput"
    )
    datetime_t& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype, out="System.DateTime") datetime_t* "ref System.DateTime"
    %typemap(csin,
        pre="    using ($csclassname temp$csinput = $module.FromTicks($csinput)) {",
        post="      $csinput = new System.DateTime($module.ToTicks(temp$csinput));",
        terminator="    }",
        cshin="ref $csinput"
    )
    datetime_t* "$csclassname.getCPtr(temp$csinput)"

    %typemap(csvarin, excode=SWIGEXCODE3) datetime_t*, datetime_t&, datetime_t
    %{
      set {
        $csclassname temp$csinput = $module.FromTicks($csinput.Ticks);
        $imcall;$excode
      }
    %}

    %typemap(csvarout, excode=SWIGEXCODE3) datetime_t*, datetime_t&, datetime_t
    %{
      get {
        global::System.IntPtr cPtr = $imcall;$excode
        using ($csclassname tempDate = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
          return new System.DateTime($module.ToTicks(tempDate));
        }
      }
    %}

    %typemap(cstype) datetime_t, const datetime_t "System.DateTime"
    %typemap(csout, excode=SWIGEXCODE2) datetime_t, const datetime_t, const datetime_t&
    {
      global::System.IntPtr cPtr = $imcall;$excode
      using ($csclassname tempDate = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
        return new System.DateTime($module.ToTicks(tempDate));
      }
    }

    // Note: Do not change the spacing on the following Nullable typemaps as it affects spacing in generated code (see DataRow.cs):

    // Map sttp::Nullable<std::string> to string
    %typemap(cstype) const sttp::Nullable<std::string>& "string"
    %typemap(csin, 
        pre="    $csclassname temp$csinput = new $csclassname($csinput);"
    )
    const sttp::Nullable<std::string>& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype) sttp::Nullable<std::string> "string"
    %typemap(csout, excode=SWIGEXCODE) sttp::Nullable<std::string> {
    global::System.IntPtr cPtr = $imcall;$excode
    using ($csclassname tempString = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
      return tempString?.GetValueOrDefault();
    }
  }

    // Map sttp::Nullable<decimal_t> to decimal
    %typemap(cstype) const sttp::Nullable<decimal_t>& "decimal?"
    %typemap(csin, 
        pre="    $csclassname temp$csinput = $csinput.HasValue ? new $csclassname($module.ParseDecimal($csinput.Value.ToString())) : null;"
    )
    const sttp::Nullable<decimal_t>& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype) sttp::Nullable<decimal_t> "decimal?"
    %typemap(csout, excode=SWIGEXCODE) sttp::Nullable<decimal_t> {
    global::System.IntPtr cPtr = $imcall;$excode
    using ($csclassname tempDecimal = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
      if (tempDecimal?.HasValue() ?? false) return System.Convert.ToDecimal($module.ToString(tempDecimal.GetValueOrDefault()));
      return null;
    }
  }

    // Map sttp::Nullable<Guid> to System.Guid
    %typemap(cstype) const sttp::Nullable<Guid>& "System.Guid?"
    %typemap(csin, 
        pre="    $csclassname temp$csinput = $csinput.HasValue ? new $csclassname($module.ParseGuid($csinput.Value.ToByteArray(), true)) : null;"
    )
    const sttp::Nullable<Guid>& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype) sttp::Nullable<Guid> "System.Guid?"
    %typemap(csout, excode=SWIGEXCODE) sttp::Nullable<Guid> {
    global::System.IntPtr cPtr = $imcall;$excode
    using ($csclassname tempGuid = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
      if (!(tempGuid?.HasValue() ?? false)) return null;
      byte[] data = new byte[16];
      $module.GetGuidBytes(tempGuid.GetValueOrDefault(), data);
      return new System.Guid(data);
    }
  }

    // Map sttp::Nullable<datetime_t> to System.DateTime
    %typemap(cstype) const sttp::Nullable<datetime_t>& "System.DateTime?"
    %typemap(csin, 
        pre="    $csclassname temp$csinput = $csinput.HasValue ? new $csclassname($module.FromTicks($csinput.Value.Ticks)) : null;"
    )
    const sttp::Nullable<datetime_t>& "$csclassname.getCPtr(temp$csinput)"

    %typemap(cstype) sttp::Nullable<datetime_t> "System.DateTime?"
    %typemap(csout, excode=SWIGEXCODE) sttp::Nullable<datetime_t> {
    global::System.IntPtr cPtr = $imcall;$excode
    using ($csclassname tempDate = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {
      if (tempDate?.HasValue() ?? false) return new System.DateTime($module.ToTicks(tempDate.GetValueOrDefault()));
      return null;
    }
  }

    %define NULLABLE_CSOUT_EXPANSION "\n\
    global::System.IntPtr cPtr = $imcall;\n\
    if ($imclassname.SWIGPendingException.Pending) throw $imclassname.SWIGPendingException.Retrieve();\n\
    using ($csclassname tempValue = (cPtr == global::System.IntPtr.Zero) ? null : new $csclassname(cPtr, $owner)) {\n\
      if (tempValue?.HasValue() ?? false) return tempValue.GetValueOrDefault();\n\
      return null;\n\
    }\n"
    %enddef

    #define NULLABLE_TYPEDECL(stype, ttype)\
    %typemap(cstype) const sttp::Nullable<stype>& "ttype?"\
    %typemap(csin, pre="    $csclassname temp$csinput = $csinput.HasValue ? new $csclassname($csinput.Value) : null;")\
    const sttp::Nullable<stype>& "$csclassname.getCPtr(temp$csinput)"\
    %typemap(cstype) sttp::Nullable<stype> "ttype?" \
    %typemap(csout, excode=NULLABLE_CSOUT_EXPANSION) sttp::Nullable<stype> {$excode  }

    // Map Nullable templates for simple types
    NULLABLE_TYPEDECL(bool, bool);
    NULLABLE_TYPEDECL(float32_t, float);
    NULLABLE_TYPEDECL(float64_t, double);
    NULLABLE_TYPEDECL(int8_t, sbyte);
    NULLABLE_TYPEDECL(int16_t, short);
    NULLABLE_TYPEDECL(int32_t, int);
    NULLABLE_TYPEDECL(int64_t, long);
    NULLABLE_TYPEDECL(uint8_t, byte);
    NULLABLE_TYPEDECL(uint16_t, ushort);
    NULLABLE_TYPEDECL(uint32_t, uint);
    NULLABLE_TYPEDECL(uint64_t, ulong);
}

namespace std
{
    %naturalvar string;

    class string;

    // string &
    %typemap(ctype) std::string & "char**"
    %typemap(imtype) std::string & "out string"
    %typemap(cstype) std::string & "out string"

    //C++
    %typemap(in, canthrow=1) std::string &
    %{  //typemap in
        std::string temp$1_name;
        $1 = &temp$1_name; 
    %}

    //C++
    %typemap(argout) std::string & 
    %{ 
        //Typemap argout in c++ file.
        //This will convert c++ string to c# string
        *$input = SWIG_csharp_string_callback($1->c_str());
    %}

    %typemap(argout) const std::string & 
    %{ 
        //argout typemap for const std::string&
    %}

    %typemap(csin) std::string & "out $csinput"

    %typemap(throws, canthrow=1) string &
    %{ SWIG_CSharpSetPendingException(SWIG_CSharpApplicationException, $1.c_str());
       return $null; %}

    %template(ByteBuffer) vector<unsigned char>;
    %template(StringCollection) vector<string>;
    %template(DataTableCollection) vector<boost::shared_ptr<sttp::data::DataTable>>;
    %template(MeasurementMetadataCollection) vector<boost::shared_ptr<sttp::transport::MeasurementMetadata>>;
    %template(PhasorMetadataCollection) vector<boost::shared_ptr<sttp::transport::PhasorMetadata>>;
    %template(PhasorReferenceCollection) vector<boost::shared_ptr<sttp::transport::PhasorReference>>;
    %template(DeviceMetadataCollection) vector<boost::shared_ptr<sttp::transport::DeviceMetadata>>;
    %template(SubscriberConnectionCollection) vector<boost::shared_ptr<sttp::transport::SubscriberConnection>>;

    %template(DeviceMetadataMap) map<string, boost::shared_ptr<sttp::transport::DeviceMetadata>>;
    %template(MeasurementMetadataMap) map<sttp::Guid, boost::shared_ptr<sttp::transport::MeasurementMetadata>>;
}

namespace sttp {
namespace data
{
    enum class DataType
    {
        String,
        Boolean,
        DateTime,
        Single,
        Double,
        Decimal,
        Guid,
        Int8,
        Int16,
        Int32,
        Int64,
        UInt8,
        UInt16,
        UInt32,
        UInt64
    };

    typedef boost::shared_ptr<DataSet> DataSetPtr;
    typedef boost::shared_ptr<DataTable> DataTablePtr;
    typedef boost::shared_ptr<DataRow> DataRowPtr;
    typedef boost::shared_ptr<DataColumn> DataColumnPtr;

    %rename(_TableCount) DataSet::TableCount;
    %rename(_TableNames) DataSet::TableNames;
    %rename(_Tables) DataSet::Tables;

    class DataSet
    {
    public:
         DataSet();
        ~DataSet();

        const DataTablePtr& Table(const std::string& tableName) const;

        DataTablePtr CreateTable(const std::string& name);

        %csmethodmodifiers TableCount "private";
        int32_t TableCount() const;

        %csmethodmodifiers TableNames "private";
        std::vector<std::string> TableNames() const;

        %csmethodmodifiers Tables "private";
        std::vector<DataTablePtr> Tables() const;

        bool AddOrUpdateTable(DataTablePtr table);

        bool RemoveTable(const std::string& tableName);

        void ReadXml(const std::string& fileName);
        void ReadXml(const std::vector<uint8_t>& buffer);
        
        %csmethodmodifiers ReadXml "public unsafe";
        %apply uint8_t FIXED[] {uint8_t* buffer}
        void ReadXml(const uint8_t* buffer, uint32_t length);

        void WriteXml(const std::string& fileName, const std::string& dataSetName = "DataSet") const;
        void WriteXml(std::vector<uint8_t>& buffer, const std::string& dataSetName = "DataSet") const;

        static DataSetPtr FromXml(const std::string& fileName);
        static DataSetPtr FromXml(const std::vector<uint8_t>& buffer);

        %csmethodmodifiers FromXml "public unsafe";
        %apply uint8_t FIXED[] {const uint8_t* buffer}
        static DataSetPtr FromXml(const uint8_t* buffer, uint32_t length);

        static const std::string XmlSchemaNamespace;
        static const std::string ExtXmlSchemaDataNamespace;
    };

    %extend DataSet {
    %proxycode
    %{
        public int TableCount => _TableCount();

        public StringCollection TableNames => _TableNames();

        public DataTableCollection Tables => _Tables();

        public DataTable this[string tableName] => Table(tableName);
    %}}

    %rename(_Parent) DataTable::Parent;
    %rename(_Name) DataTable::Name;
    %rename(_ColumnCount) DataTable::ColumnCount;
    %rename(_RowCount) DataTable::RowCount;

    class DataTable
    {
    public:
        DataTable(DataSetPtr parent, std::string name);
        ~DataTable();

        %csmethodmodifiers Parent "private";
        const DataSetPtr& Parent() const;

        %csmethodmodifiers Name "private";
        const std::string& Name() const;

        void AddColumn(DataColumnPtr column);

        const DataColumnPtr& Column(const std::string& columnName) const;

        const DataColumnPtr& Column(int32_t index) const;

        DataColumnPtr CreateColumn(const std::string& name, DataType type, std::string expression = "");

        DataColumnPtr CloneColumn(const DataColumnPtr& source);

        %csmethodmodifiers ColumnCount "private";
        int32_t ColumnCount() const;

        const DataRowPtr& Row(int32_t index);

        void AddRow(DataRowPtr row);

        DataRowPtr CreateRow();

        DataRowPtr CloneRow(const DataRowPtr& source);

        %csmethodmodifiers RowCount "private";
        int32_t RowCount() const;
    };

    %extend DataTable {
    %proxycode
    %{
        public DataSet Parent => _Parent();

        public string Name => _Name();

        public int ColumntCount => _ColumnCount();

        public int RowCount => _RowCount();

        public DataColumn this[int index] => Column(index);

        public DataColumn this[string columnName] => Column(columnName);
    %}}
 
    %rename(_Parent) DataRow::Parent;

    class DataRow
    {
    public:
        DataRow(DataTablePtr parent);
        ~DataRow();

        %csmethodmodifiers Parent "private";
        const DataTablePtr& Parent() const;

        bool IsNull(int32_t columnIndex);
        bool IsNull(const std::string& columnName);
        void SetNullValue(int32_t columnIndex);
        void SetNullValue(const std::string& columnName);

        sttp::Nullable<std::string> ValueAsString(int32_t columnIndex);
        sttp::Nullable<std::string> ValueAsString(const std::string& columnName);
        void SetStringValue(int32_t columnIndex, const sttp::Nullable<std::string>& value);
        void SetStringValue(const std::string& columnName, const sttp::Nullable<std::string>& value);

        sttp::Nullable<bool> ValueAsBoolean(int32_t columnIndex);
        sttp::Nullable<bool> ValueAsBoolean(const std::string& columnName);
        void SetBooleanValue(int32_t columnIndex, const sttp::Nullable<bool>& value);
        void SetBooleanValue(const std::string& columnName, const sttp::Nullable<bool>& value);

        sttp::Nullable<sttp::datetime_t> ValueAsDateTime(int32_t columnIndex);
        sttp::Nullable<sttp::datetime_t> ValueAsDateTime(const std::string& columnName);
        void SetDateTimeValue(int32_t columnIndex, const sttp::Nullable<sttp::datetime_t>& value);
        void SetDateTimeValue(const std::string& columnName, const sttp::Nullable<sttp::datetime_t>& value);

        sttp::Nullable<sttp::float32_t> ValueAsSingle(int32_t columnIndex);
        sttp::Nullable<sttp::float32_t> ValueAsSingle(const std::string& columnName);
        void SetSingleValue(int32_t columnIndex, const sttp::Nullable<sttp::float32_t>& value);
        void SetSingleValue(const std::string& columnName, const sttp::Nullable<sttp::float32_t>& value);

        sttp::Nullable<sttp::float64_t> ValueAsDouble(int32_t columnIndex);
        sttp::Nullable<sttp::float64_t> ValueAsDouble(const std::string& columnName);
        void SetDoubleValue(int32_t columnIndex, const sttp::Nullable<sttp::float64_t>& value);
        void SetDoubleValue(const std::string& columnName, const sttp::Nullable<sttp::float64_t>& value);

        sttp::Nullable<sttp::decimal_t> ValueAsDecimal(int32_t columnIndex);
        sttp::Nullable<sttp::decimal_t> ValueAsDecimal(const std::string& columnName);
        void SetDecimalValue(int32_t columnIndex, const sttp::Nullable<sttp::decimal_t>& value);
        void SetDecimalValue(const std::string& columnName, const sttp::Nullable<sttp::decimal_t>& value);

        sttp::Nullable<sttp::Guid> ValueAsGuid(int32_t columnIndex);
        sttp::Nullable<sttp::Guid> ValueAsGuid(const std::string& columnName);
        void SetGuidValue(int32_t columnIndex, const sttp::Nullable<sttp::Guid>& value);
        void SetGuidValue(const std::string& columnName, const sttp::Nullable<sttp::Guid>& value);

        sttp::Nullable<int8_t> ValueAsInt8(int32_t columnIndex);
        sttp::Nullable<int8_t> ValueAsInt8(const std::string& columnName);
        void SetInt8Value(int32_t columnIndex, const sttp::Nullable<int8_t>& value);
        void SetInt8Value(const std::string& columnName, const sttp::Nullable<int8_t>& value);

        sttp::Nullable<int16_t> ValueAsInt16(int32_t columnIndex);
        sttp::Nullable<int16_t> ValueAsInt16(const std::string& columnName);
        void SetInt16Value(int32_t columnIndex, const sttp::Nullable<int16_t>& value);
        void SetInt16Value(const std::string& columnName, const sttp::Nullable<int16_t>& value);

        sttp::Nullable<int32_t> ValueAsInt32(int32_t columnIndex);
        sttp::Nullable<int32_t> ValueAsInt32(const std::string& columnName);
        void SetInt32Value(int32_t columnIndex, const sttp::Nullable<int32_t>& value);
        void SetInt32Value(const std::string& columnName, const sttp::Nullable<int32_t>& value);

        sttp::Nullable<int64_t> ValueAsInt64(int32_t columnIndex);
        sttp::Nullable<int64_t> ValueAsInt64(const std::string& columnName);
        void SetInt64Value(int32_t columnIndex, const sttp::Nullable<int64_t>& value);
        void SetInt64Value(const std::string& columnName, const sttp::Nullable<int64_t>& value);

        sttp::Nullable<uint8_t> ValueAsUInt8(int32_t columnIndex);
        sttp::Nullable<uint8_t> ValueAsUInt8(const std::string& columnName);
        void SetUInt8Value(int32_t columnIndex, const sttp::Nullable<uint8_t>& value);
        void SetUInt8Value(const std::string& columnName, const sttp::Nullable<uint8_t>& value);

        sttp::Nullable<uint16_t> ValueAsUInt16(int32_t columnIndex);
        sttp::Nullable<uint16_t> ValueAsUInt16(const std::string& columnName);
        void SetUInt16Value(int32_t columnIndex, const sttp::Nullable<uint16_t>& value);
        void SetUInt16Value(const std::string& columnName, const sttp::Nullable<uint16_t>& value);

        sttp::Nullable<uint32_t> ValueAsUInt32(int32_t columnIndex);
        sttp::Nullable<uint32_t> ValueAsUInt32(const std::string& columnName);
        void SetUInt32Value(int32_t columnIndex, const sttp::Nullable<uint32_t>& value);
        void SetUInt32Value(const std::string& columnName, const sttp::Nullable<uint32_t>& value);

        sttp::Nullable<uint64_t> ValueAsUInt64(int32_t columnIndex);
        sttp::Nullable<uint64_t> ValueAsUInt64(const std::string& columnName);
        void SetUInt64Value(int32_t columnIndex, const sttp::Nullable<uint64_t>& value);
        void SetUInt64Value(const std::string& columnName, const sttp::Nullable<uint64_t>& value);
    };

    %extend DataRow {
    %proxycode
    %{
        public DataTable Parent => _Parent();

        public object this[int columnIndex]
        {
            get
            {
                switch (Parent[columnIndex].Type)
                {
                    case DataType.String:
                        return ValueAsString(columnIndex);
                    case DataType.Boolean:
                        return ValueAsBoolean(columnIndex);
                    case DataType.DateTime:
                        return ValueAsDateTime(columnIndex);
                    case DataType.Single:
                        return ValueAsSingle(columnIndex);
                    case DataType.Double:
                        return ValueAsDouble(columnIndex);
                    case DataType.Decimal:
                        return ValueAsDecimal(columnIndex);
                    case DataType.Guid:
                        return ValueAsGuid(columnIndex);
                    case DataType.Int8:
                        return ValueAsInt8(columnIndex);
                    case DataType.Int16:
                        return ValueAsInt16(columnIndex);
                    case DataType.Int32:
                        return ValueAsInt32(columnIndex);
                    case DataType.Int64:
                        return ValueAsInt64(columnIndex);
                    case DataType.UInt8:
                        return ValueAsUInt8(columnIndex);
                    case DataType.UInt16:
                        return ValueAsUInt16(columnIndex);
                    case DataType.UInt32:
                        return ValueAsUInt32(columnIndex);
                    case DataType.UInt64:
                        return ValueAsUInt64(columnIndex);
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (Parent[columnIndex].Type)
                {
                    case DataType.String:
                        SetStringValue(columnIndex, value.ToString());
                        break;
                    case DataType.Boolean:
                        SetBooleanValue(columnIndex, CastType<bool>(value));
                        break;
                    case DataType.DateTime:
                        SetDateTimeValue(columnIndex, CastType<System.DateTime>(value));
                        break;
                    case DataType.Single:
                        SetSingleValue(columnIndex, CastType<float>(value));
                        break;
                    case DataType.Double:
                        SetDoubleValue(columnIndex, CastType<double>(value));
                        break;
                    case DataType.Decimal:
                        SetDecimalValue(columnIndex, CastType<decimal>(value));
                        break;
                    case DataType.Guid:
                        SetGuidValue(columnIndex, CastType<System.Guid>(value));
                        break;
                    case DataType.Int8:
                        SetInt8Value(columnIndex, CastType<sbyte>(value));
                        break;
                    case DataType.Int16:
                        SetInt16Value(columnIndex, CastType<short>(value));
                        break;
                    case DataType.Int32:
                        SetInt32Value(columnIndex, CastType<int>(value));
                        break;
                    case DataType.Int64:
                        SetInt64Value(columnIndex, CastType<long>(value));
                        break;
                    case DataType.UInt8:
                        SetUInt8Value(columnIndex, CastType<byte>(value));
                        break;
                    case DataType.UInt16:
                        SetUInt16Value(columnIndex, CastType<ushort>(value));
                        break;
                    case DataType.UInt32:
                        SetUInt32Value(columnIndex, CastType<uint>(value));
                        break;
                    case DataType.UInt64:
                        SetUInt64Value(columnIndex, CastType<ulong>(value));
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
        }

        public object this[string columnName]
        {
            get
            {
                switch (Parent[columnName].Type)
                {
                    case DataType.String:
                        return ValueAsString(columnName);
                    case DataType.Boolean:
                        return ValueAsBoolean(columnName);
                    case DataType.DateTime:
                        return ValueAsDateTime(columnName);
                    case DataType.Single:
                        return ValueAsSingle(columnName);
                    case DataType.Double:
                        return ValueAsDouble(columnName);
                    case DataType.Decimal:
                        return ValueAsDecimal(columnName);
                    case DataType.Guid:
                        return ValueAsGuid(columnName);
                    case DataType.Int8:
                        return ValueAsInt8(columnName);
                    case DataType.Int16:
                        return ValueAsInt16(columnName);
                    case DataType.Int32:
                        return ValueAsInt32(columnName);
                    case DataType.Int64:
                        return ValueAsInt64(columnName);
                    case DataType.UInt8:
                        return ValueAsUInt8(columnName);
                    case DataType.UInt16:
                        return ValueAsUInt16(columnName);
                    case DataType.UInt32:
                        return ValueAsUInt32(columnName);
                    case DataType.UInt64:
                        return ValueAsUInt64(columnName);
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (Parent[columnName].Type)
                {
                    case DataType.String:
                        SetStringValue(columnName, value.ToString());
                        break;
                    case DataType.Boolean:
                        SetBooleanValue(columnName, CastType<bool>(value));
                        break;
                    case DataType.DateTime:
                        SetDateTimeValue(columnName, CastType<System.DateTime>(value));
                        break;
                    case DataType.Single:
                        SetSingleValue(columnName, CastType<float>(value));
                        break;
                    case DataType.Double:
                        SetDoubleValue(columnName, CastType<double>(value));
                        break;
                    case DataType.Decimal:
                        SetDecimalValue(columnName, CastType<decimal>(value));
                        break;
                    case DataType.Guid:
                        SetGuidValue(columnName, CastType<System.Guid>(value));
                        break;
                    case DataType.Int8:
                        SetInt8Value(columnName, CastType<sbyte>(value));
                        break;
                    case DataType.Int16:
                        SetInt16Value(columnName, CastType<short>(value));
                        break;
                    case DataType.Int32:
                        SetInt32Value(columnName, CastType<int>(value));
                        break;
                    case DataType.Int64:
                        SetInt64Value(columnName, CastType<long>(value));
                        break;
                    case DataType.UInt8:
                        SetUInt8Value(columnName, CastType<byte>(value));
                        break;
                    case DataType.UInt16:
                        SetUInt16Value(columnName, CastType<ushort>(value));
                        break;
                    case DataType.UInt32:
                        SetUInt32Value(columnName, CastType<uint>(value));
                        break;
                    case DataType.UInt64:
                        SetUInt64Value(columnName, CastType<ulong>(value));
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
        }

        private static T? CastType<T>(object value) where T : struct
        {
            switch (value)
            {
                case null:
                    return null;
                case T result:
                    return result;
                default:
                    return (T)System.Convert.ChangeType(value, typeof(T));
            }
        }
    %}}

    %rename(_Parent) DataColumn::Parent;
    %rename(_Name) DataColumn::Name;
    %rename(_Type) DataColumn::Type;
    %rename(_Expression) DataColumn::Expression;
    %rename(_Computed) DataColumn::Computed;
    %rename(_Index) DataColumn::Index;

    class DataColumn
    {
    public:
        DataColumn(DataTablePtr parent, std::string name, DataType type, std::string expression = "");
        ~DataColumn();

        %csmethodmodifiers Parent "private";
        const DataTablePtr& Parent() const;

        %csmethodmodifiers Name "private";
        const std::string& Name() const;

        %csmethodmodifiers Type "private";
        DataType Type() const;

        %csmethodmodifiers Expression "private";
        const std::string& Expression() const;

        %csmethodmodifiers Computed "private";
        bool Computed() const;

        %csmethodmodifiers Index "private";
        int32_t Index() const;
    };

    %extend DataColumn {
    %proxycode
    %{
        public DataTable Parent => _Parent();

        public string Name => _Name();

        public DataType Type => _Type();

        public string Expression => _Expression();

        public bool Computed => _Computed();

        public int Index => _Index();
    %}}
}

namespace transport
{
    // Measurement state flags.
    %typemap(csbase) MeasurementStateFlags "uint"
    enum class MeasurementStateFlags : unsigned int
    {
        // Defines normal state.
        Normal = 0x0,
        // Defines bad data state.
        BadData = 0x1,
        // Defines suspect data state.
        SuspectData = 0x2,
        // Defines over range error, i.e., unreasonable high value.
        OverRangeError = 0x4,
        // Defines under range error, i.e., unreasonable low value.
        UnderRangeError = 0x8,
        // Defines alarm for high value.
        AlarmHigh = 0x10,
        // Defines alarm for low value.
        AlarmLow = 0x20,
        // Defines warning for high value.
        WarningHigh = 0x40,
        // Defines warning for low value.
        WarningLow = 0x80,
        // Defines alarm for flat-lined value, i.e., latched value test alarm.
        FlatlineAlarm = 0x100,
        // Defines comparison alarm, i.e., outside threshold of comparison with a real-time value.
        ComparisonAlarm = 0x200,
        // Defines rate-of-change alarm.
        ROCAlarm = 0x400,
        // Defines bad value received.
        ReceivedAsBad = 0x800,
        // Defines calculated value state.
        CalculatedValue = 0x1000,
        // Defines calculation error with the value.
        CalculationError = 0x2000,
        // Defines calculation warning with the value.
        CalculationWarning = 0x4000,
        // Defines reserved quality flag.
        ReservedQualityFlag = 0x8000,
        // Defines bad time state.
        BadTime = 0x10000,
        // Defines suspect time state.
        SuspectTime = 0x20000,
        // Defines late time alarm.
        LateTimeAlarm = 0x40000,
        // Defines future time alarm.
        FutureTimeAlarm = 0x80000,
        // Defines up-sampled state.
        UpSampled = 0x100000,
        // Defines down-sampled state.
        DownSampled = 0x200000,
        // Defines discarded value state.
        DiscardedValue = 0x400000,
        // Defines reserved time flag.
        ReservedTimeFlag = 0x800000,
        // Defines user defined flag 1.
        UserDefinedFlag1 = 0x1000000,
        // Defines user defined flag 2.
        UserDefinedFlag2 = 0x2000000,
        // Defines user defined flag 3.
        UserDefinedFlag3 = 0x4000000,
        // Defines user defined flag 4.
        UserDefinedFlag4 = 0x8000000,
        // Defines user defined flag 5.
        UserDefinedFlag5 = 0x10000000,
        // Defines system error state.
        SystemError = 0x20000000,
        // Defines system warning state.
        SystemWarning = 0x40000000,
        // Defines measurement error flag.
        MeasurementError = 0x80000000
    };

    // Fundamental POD type representing a measurement in STTP
    %typemap(csclassmodifiers) SimpleMeasurement "internal class"
    struct SimpleMeasurement
    {
        // Measurement's globally unique identifier.
        sttp::Guid SignalID;

        // Instantaneous value of the measurement.
        float64_t Value;

        // The time, in ticks, that this measurement was taken.
        int64_t Timestamp;

        // Flags indicating the state of the measurement as reported by the device that took it.
        MeasurementStateFlags Flags;
    };

    %typemap(csbase) SignalKind "ushort"
    enum SignalKind : int16_t
    {
        Angle,          // Phase angle
        Magnitude,      // Phase magnitude
        Frequency,      // Line frequency
        DfDt,           // Frequency delta over time (dF/dt)
        Status,         // Status flags
        Digital,        // Digital value
        Analog,         // Analog value
        Calculation,    // Calculated value
        Statistic,      // Statistical value
        Alarm,          // Alarm value
        Quality,        // Quality flags
        Unknown         // Undetermined signal type
    };

    // Map signal kind to signal type acronym
    extern std::string GetSignalTypeAcronym(SignalKind kind, char phasorType = 'I');

    // Map signal type acronym to engineering units
    extern std::string GetEngineeringUnits(const std::string& signalType);

    // Map protocol name to type
    extern std::string GetProtocolType(const std::string& protocolName);

    // Helper function to parse signal kind
    extern SignalKind ParseSignalKind(const std::string& acronym);

    struct SignalReference
    {
        Guid SignalID;          // Unique UUID of this individual measurement (key to MeasurementMetadata.SignalID)
        std::string Acronym;    // Associated (parent) device for measurement (key to DeviceMetadata.Acronym / MeasurementMetadata.DeviceAcronym)
        uint16_t Index;         // For phasors, digitals and analogs - this is the ordered index, uses 1-based indexing
        SignalKind Kind;        // Signal classification (e.g., phase angle, but not specific type of voltage or current)

        SignalReference();
        SignalReference(const std::string& signal);
    };

    struct MeasurementMetadata
    {
        std::string DeviceAcronym;  // Associated (parent) device for measurement (key to DeviceMetadata.Acronym)
        std::string ID;             // Measurement key string, format: "Source:ID" (if useful)
        Guid SignalID;              // Unique UUID of this individual measurement (lookup key!)
        std::string PointTag;       // Well formatted tag name for historians, e.g., OSI-PI, etc.
        SignalReference Reference;  // Parsed signal reference structure
        uint16_t PhasorSourceIndex; // Measurement phasor index, if measurement represents a "Phasor"
        std::string Description;    // Detailed measurement description (free-form)
        datetime_t UpdatedOn;       // Time of last meta-data update
    };

    typedef boost::shared_ptr<MeasurementMetadata> MeasurementMetadataPtr;
    %typemap(cstype) MeasurementMetadataPtr "MeasurementMetadata"

    struct PhasorMetadata
    {
        std::string DeviceAcronym;  // Associated (parent) device for phasor (key to DeviceMetadata.Acronym)
        std::string Label;          // Channel name for "phasor" (covers two measurements)
        std::string Type;           // Phasor type, i.e., "V" for voltage or "I" for current
        std::string Phase;          // Phasor phase, one of, "+", "-", "0", "A", "B" or "C"
        uint16_t SourceIndex;       // Phasor ordered index, uses 1-based indexing (key to MeasurementMetadata.PhasorSourceIndex)
        datetime_t UpdatedOn;       // Time of last meta-data update
    };

    typedef boost::shared_ptr<PhasorMetadata> PhasorMetadataPtr;

    struct PhasorReference
    {
        PhasorMetadataPtr Phasor;           // Phasor metadata, includes phasor type, i.e., voltage or current
        MeasurementMetadataPtr Angle;       // Angle measurement metadata for phasor
        MeasurementMetadataPtr Magnitude;   // Magnitude measurement metadata for phasor
    };

    typedef boost::shared_ptr<PhasorReference> PhasorReferencePtr;

    struct DeviceMetadata
    {
        std::string Acronym;            // Alpha-numeric device acronym, e.g., PMU/station name (all-caps - no spaces)
        std::string Name;               // User-defined device name / description (free-form)
        Guid UniqueID;                  // Device unique UUID (used for C37.118 v3 config frame)
        uint16_t AccessID;              // ID code used for device connection / reference
        std::string ParentAcronym;      // Original PDC name (if useful / not assigned for directly connected devices)
        std::string ProtocolName;       // Original protocol name (if useful)
        uint16_t FramesPerSecond;       // Device reporting rate, e.g., 30 fps
        std::string CompanyAcronym;     // Original device company name (if useful)
        std::string VendorAcronym;      // Original device vendor name (if useful / provided)
        std::string VendorDeviceName;   // Original vendor device name, e.g., PMU brand (if useful / provided)
        float64_t Longitude;            // Device longitude (if reported)
        float64_t Latitude;             // Device latitude (if reported)
        datetime_t UpdatedOn;           // Time of last meta-data update

        // Associated measurement and phasor meta-data
        std::vector<MeasurementMetadataPtr> Measurements;   // DataPublisher does not need to assign
        std::vector<PhasorReferencePtr> Phasors;            // DataPublisher does not need to assign
    };

    typedef boost::shared_ptr<DeviceMetadata> DeviceMetadataPtr;
    %typemap(cstype) DeviceMetadataPtr& "out DeviceMetadata"
    %typemap(csin,
        pre = "    $csinput = new DeviceMetadata();",
        cshin = "out $csinput"
    )
    DeviceMetadataPtr& "DeviceMetadata.getCPtr($csinput)"

    // Defines the configuration frame "structure" for a device data frame
    struct ConfigurationFrame
    {
        std::string DeviceAcronym;
        MeasurementMetadataPtr QualityFlags; // This measurement may be null, see below **
        MeasurementMetadataPtr StatusFlags;
        MeasurementMetadataPtr Frequency;
        MeasurementMetadataPtr DfDt;
        std::vector<PhasorReferencePtr> Phasors;
        std::vector<MeasurementMetadataPtr> Analogs;
        std::vector<MeasurementMetadataPtr> Digitals;

        // Associated measurements
        //std::unordered_set<Guid> Measurements; // No SWIG wrapper for unordered_set yet...
    };

    typedef boost::shared_ptr<ConfigurationFrame> ConfigurationFramePtr;
    %typemap(cstype) ConfigurationFramePtr& "out ConfigurationFrame"
    %typemap(csin,
        pre = "    $csinput = new ConfigurationFrame();",
        cshin = "out $csinput"
    )
    ConfigurationFramePtr& "ConfigurationFrame.getCPtr($csinput)"

    %typemap(cstype) const ConfigurationFramePtr& "ConfigurationFrame"
    %typemap(csin)
    const ConfigurationFramePtr& "ConfigurationFrame.getCPtr($csinput)"

    // This type map is declared after PhasorReference and ConfigurationFrame to prevent properties being generated with an "out"
    %typemap(cstype) MeasurementMetadataPtr& "out MeasurementMetadata"
    %typemap(csin,
        pre = "    $csinput = new MeasurementMetadata();",
        cshin = "out $csinput"
    )
    MeasurementMetadataPtr& "MeasurementMetadata.getCPtr($csinput)"

    // Info structure used to configure subscriptions.
    struct SubscriptionInfo
    {
        std::string FilterExpression;

        // Down-sampling properties
        bool Throttled;
        float64_t PublishInterval;

        // UDP channel properties
        bool UdpDataChannel;
        uint16_t DataChannelLocalPort;

        // Compact measurement properties
        bool IncludeTime;
        float64_t LagTime;
        float64_t LeadTime;
        bool UseLocalClockAsRealTime;
        bool UseMillisecondResolution;
        bool RequestNaNValueFilter;

        // Temporal playback properties
        std::string StartTime;
        std::string StopTime;
        std::string ConstraintParameters;
        int32_t ProcessingInterval;

        std::string ExtraConnectionStringParameters;

        SubscriptionInfo();
    };

    // Helper class to provide retry and auto-reconnect functionality to the subscriber.
    class SubscriberConnector
    {
    public:
        %csmethodmodifiers SetHostname "private";
        void SetHostname(const std::string& hostname);

        %csmethodmodifiers SetPort "private";
        void SetPort(uint16_t port);

        %csmethodmodifiers SetMaxRetries "private";
        void SetMaxRetries(int32_t maxRetries);

        %csmethodmodifiers SetRetryInterval "private";
        void SetRetryInterval(int32_t retryInterval);

        %csmethodmodifiers SetMaxRetryInterval "private";
        void SetMaxRetryInterval(int32_t maxRetryInterval);

        %csmethodmodifiers SetAutoReconnect "private";
        void SetAutoReconnect(bool autoReconnect);

        %csmethodmodifiers GetHostname "private";
        std::string GetHostname() const;

        %csmethodmodifiers GetPort "private";
        uint16_t GetPort() const;

        %csmethodmodifiers GetMaxRetries "private";
        int32_t GetMaxRetries() const;

        %csmethodmodifiers GetRetryInterval "private";
        int32_t GetRetryInterval() const;

        %csmethodmodifiers GetMaxRetryInterval "private";
        int32_t GetMaxRetryInterval() const;

        %csmethodmodifiers GetAutoReconnect "private";
        bool GetAutoReconnect() const;

        %csmethodmodifiers GetConnectionRefused "private";
        bool GetConnectionRefused() const;
    };    

    // Proxy getters and setters as actual .NET properties
    %extend SubscriberConnector {
    %proxycode
    %{
        // Gets or sets the hostname of the publisher to connect to.
        public string HostName
        {
          get => GetHostname();
          set => SetHostname(value);
        }

        // Gets or sets the port that the publisher is listening on.
        public ushort Port
        {
          get => GetPort();
          set => SetPort(value);
        }

        // Gets or sets the maximum number of retries during a connection sequence.
        public int MaxRetries
        {
          get => GetMaxRetries();
          set => SetMaxRetries(value);
        }

        // Gets or sets the initial interval of idle time (in milliseconds) between connection attempts.
        public int RetryInterval
        {
          get => GetRetryInterval();
          set => SetRetryInterval(value);
        }

        // Gets or sets maximum retry interval - connection retry attempts use exponential back-off algorithm up to this defined maximum.
        public int MaxRetryInterval
        {
          get => GetMaxRetryInterval();
          set => SetMaxRetryInterval(value);
        }

        // Gets or sets flag that determines whether the subscriber should automatically attempt to reconnect when the connection is terminated.
        public bool AutoReconnect
        {
          get => GetAutoReconnect();
          set => SetAutoReconnect(value);
        }

        // Gets flag that determines if last connection attempt was refused.
        public bool ConnectionRefused => GetConnectionRefused();
    %}}

    %feature("director") SubscriberInstance;
    class SubscriberInstance
    {
    public:
        %csmethodmodifiers SetupSubscriberConnector "protected virtual";
        virtual void SetupSubscriberConnector(SubscriberConnector& connector);

        %csmethodmodifiers CreateSubscriptionInfo "protected virtual";
        virtual SubscriptionInfo CreateSubscriptionInfo();

        %csmethodmodifiers StatusMessage "protected virtual";
        virtual void StatusMessage(const std::string& message); // Defaults output to cout

        %csmethodmodifiers ErrorMessage "protected virtual";
        virtual void ErrorMessage(const std::string& message);  // Defaults output to cerr

        %csmethodmodifiers DataStartTime "protected virtual";
        virtual void DataStartTime(datetime_t startTime);

        %csmethodmodifiers ReceivedMetadata "protected virtual";
        virtual void ReceivedMetadata(const std::vector<uint8_t>& payload);

        %csmethodmodifiers ParsedMetadata "protected virtual";
        virtual void ParsedMetadata();

        %csmethodmodifiers ReceivedNewMeasurements "internal virtual";
        virtual void ReceivedNewMeasurements(const SimpleMeasurement* measurements, int32_t length);

        %csmethodmodifiers ConfigurationChanged "protected virtual";
        virtual void ConfigurationChanged();

        %csmethodmodifiers HistoricalReadComplete "protected virtual";
        virtual void HistoricalReadComplete();

        %csmethodmodifiers ConnectionEstablished "protected virtual";
        virtual void ConnectionEstablished();

        %csmethodmodifiers ConnectionTerminated "protected virtual";
        virtual void ConnectionTerminated();

        // Version info functions
        %csmethodmodifiers ConnectionTerminated "protected";
        void GetAssemblyInfo(std::string& source, std::string& version, std::string& updatedOn) const;

        %csmethodmodifiers ConnectionTerminated "protected";
        void SetAssemblyInfo(const std::string& source, const std::string& version, const std::string& updatedOn);

        %csmethodmodifiers SubscriberInstance "protected";
        SubscriberInstance();
        virtual ~SubscriberInstance();

        // Constants
        static constexpr const char* SubscribeAllExpression = "FILTER ActiveMeasurements WHERE ID IS NOT NULL";
        static constexpr const char* SubscribeAllNoStatsExpression = "FILTER ActiveMeasurements WHERE SignalType <> 'STAT'";
        static constexpr const char* FilterMetadataStatsExpression = "FILTER MeasurementDetail WHERE SignalAcronym <> 'STAT'";

        // Subscription functions

        // Initialize a connection with host name, port. To enable UDP for data channel,
        // optionally specify a UDP receive port. This function must be called before
        // calling the Connect method.
        void Initialize(const std::string& hostname, uint16_t port, uint16_t udpPort = 0);

        const Guid& GetSubscriberID() const;

        // Gets or sets flag that determines if auto-reconnect is enabled
        %csmethodmodifiers GetAutoReconnect "private";
        bool GetAutoReconnect() const;

        %csmethodmodifiers SetAutoReconnect "private";
        void SetAutoReconnect(bool autoReconnect);

        // Gets or sets flag that determines if metadata should be automatically
        // parsed. When true, metadata will be requested upon connection before
        // subscription; otherwise, metadata will not be manually requested and
        // subscribe will happen upon connection.
        %csmethodmodifiers GetAutoParseMetadata "private";
        bool GetAutoParseMetadata() const;

        %csmethodmodifiers SetAutoParseMetadata "private";
        void SetAutoParseMetadata(bool autoParseMetadata);

        // Gets or sets maximum connection retries
        %csmethodmodifiers GetMaxRetries "private";
        int16_t GetMaxRetries() const;

        %csmethodmodifiers SetMaxRetries "private";
        void SetMaxRetries(int16_t maxRetries);

        // Gets or sets delay between connection retries
        %csmethodmodifiers GetRetryInterval "private";
        int16_t GetRetryInterval() const;

        %csmethodmodifiers SetRetryInterval "private";
        void SetRetryInterval(int16_t retryInterval);

        // The following are example filter expression formats:
        //
        // - Signal ID list -
        // subscriber.SetFilterExpression("7aaf0a8f-3a4f-4c43-ab43-ed9d1e64a255;"
        //                                "93673c68-d59d-4926-b7e9-e7678f9f66b4;"
        //                                "65ac9cf6-ae33-4ece-91b6-bb79343855d5;"
        //                                "3647f729-d0ed-4f79-85ad-dae2149cd432;"
        //                                "069c5e29-f78a-46f6-9dff-c92cb4f69371;"
        //                                "25355a7b-2a9d-4ef2-99ba-4dd791461379");
        //
        // - Measurement key list pattern -
        // subscriber.SetFilterExpression("PPA:1;PPA:2;PPA:3;PPA:4;PPA:5;PPA:6;PPA:7;PPA:8;PPA:9;PPA:10;PPA:11;PPA:12;PPA:13;PPA:14");
        //
        // - Filter pattern -
        // subscriber.SetFilterExpression("FILTER ActiveMeasurements WHERE ID LIKE 'PPA:*'");
        // subscriber.SetFilterExpression("FILTER ActiveMeasurements WHERE Device = 'SHELBY' AND SignalType = 'FREQ'");

        // Define a filter expression to control which points to receive. The filter expression
        // defaults to all non-static points available. When specified before the Connect function,
        // this filter expression will be used for the initial connection. Updating the filter
        // expression while a subscription is active will cause a resubscribe with new expression.
        %csmethodmodifiers GetFilterExpression "private";
        const std::string& GetFilterExpression() const;

        %csmethodmodifiers SetFilterExpression "private";
        void SetFilterExpression(const std::string& filterExpression);

        // Define any metadata filters to be applied to incoming metadata. Each separate filter should
        // be separated by semi-colons.
        %csmethodmodifiers GetMetadataFilters "private";
        const std::string& GetMetadataFilters() const;

        %csmethodmodifiers SetMetadataFilters "private";
        void SetMetadataFilters(const std::string& metadataFilters);

        // Starts the connection cycle to an STTP publisher. Upon connection, meta-data will be requested,
        // when received, a subscription will be established
        void Connect();
        void ConnectAsync();

        // Disconnects from the STTP publisher
        void Disconnect() const;

        // Historical subscription functions

        // Defines the desired time-range of data from the STTP publisher, if the publisher supports
        // historical queries. If specified, this function must be called before Connect.
        void EstablishHistoricalRead(const std::string& startTime, const std::string& stopTime);

        // Dynamically controls replay speed - can be updated while historical data is being received
        void SetHistoricalReplayInterval(int32_t replayInterval) const;

        // Gets or sets value that determines whether
        // payload data is compressed using TSSC.
        %csmethodmodifiers IsPayloadDataCompressed "private";
        bool IsPayloadDataCompressed() const;

        %csmethodmodifiers SetPayloadDataCompressed "private";
        void SetPayloadDataCompressed(bool compressed) const;

        // Gets or sets value that determines whether the
        // metadata transfer is compressed using GZip.
        %csmethodmodifiers IsMetadataCompressed "private";
        bool IsMetadataCompressed() const;

        %csmethodmodifiers SetMetadataCompressed "private";
        void SetMetadataCompressed(bool compressed) const;

        // Gets or sets value that determines whether the
        // signal index cache is compressed using GZip.
        %csmethodmodifiers IsSignalIndexCacheCompressed "private";
        bool IsSignalIndexCacheCompressed() const;

        %csmethodmodifiers SetSignalIndexCacheCompressed "private";
        void SetSignalIndexCacheCompressed(bool compressed) const;

        // Statistical functions
        uint64_t GetTotalCommandChannelBytesReceived() const;
        uint64_t GetTotalDataChannelBytesReceived() const;
        uint64_t GetTotalMeasurementsReceived() const;
        bool IsConnected() const;
        bool IsSubscribed() const;

        // Safely get list of device acronyms (accessed from metadata after successful auto-parse),
        // vector will be cleared then appended to, returns true if any devices were added
        bool TryGetDeviceAcronyms(std::vector<std::string>& deviceAcronyms);

        // Safely get parsed device metadata (accessed after successful auto-parse),
        // vector will be cleared then appended to with copy of all parsed data
        void GetParsedDeviceMetadata(std::map<std::string, DeviceMetadataPtr>& devices);

        // Safely get parsed measurements (accessed after successful auto-parse),
        // vector will be cleared then appended to with copy of all parsed data
        void GetParsedMeasurementMetadata(std::map<Guid, MeasurementMetadataPtr>& measurements);

        // Metadata record lookup functions (post-parse)
        bool TryGetDeviceMetadata(const std::string& deviceAcronym, DeviceMetadataPtr& deviceMetadata);
        bool TryGetMeasurementMetdata(const Guid& signalID, MeasurementMetadataPtr& measurementMetadata);
        bool TryGetConfigurationFrame(const std::string& deviceAcronym, ConfigurationFramePtr& configurationFrame);
        bool TryFindTargetConfigurationFrame(const Guid& signalID, ConfigurationFramePtr& targetFrame);

        // Configuration frame limits the required search range for measurement metadata,
        // searching the frame members for a matching signal ID should normally be much
        // faster than executing a lookup in the full measurement map cache.
        static bool TryGetMeasurementMetdataFromConfigurationFrame(const Guid& signalID, const ConfigurationFramePtr& sourceFrame, MeasurementMetadataPtr& measurementMetadata);
    };

    // Proxy getters and setters as actual .NET properties
    %extend SubscriberInstance {
    %proxycode
    %{
        public void EstablishHistoricalRead(System.DateTime startTime, System.DateTime stopTime) => EstablishHistoricalRead($"{startTime:yyyy-MM-dd HH:mm:ss.fff}", $"{stopTime:yyyy-MM-dd HH:mm:ss.fff}");

        // Gets or sets flag that determines if auto-reconnect is enabled.
        public bool AutoReconnect
        {
          get => GetAutoReconnect();
          set => SetAutoReconnect(value);
        }

        // Gets or sets flag that determines if metadata should be automatically
        // parsed. When true, metadata will be requested upon connection before
        // subscription; otherwise, metadata will not be manually requested and
        // subscribe will happen upon connection.
        public bool AutoParseMetadata
        {
          get => GetAutoParseMetadata();
          set => SetAutoParseMetadata(value);
        }

        // Gets or sets maximum connection retries.
        public short MaxRetries
        {
          get => GetMaxRetries();
          set => SetMaxRetries(value);
        }

        // Gets or sets the initial interval of idle time (in milliseconds) between connection attempts.
        public short RetryInterval
        {
          get => GetRetryInterval();
          set => SetRetryInterval(value);
        }

        // Gets or sets a filter expression to control which points to receive. The filter expression
        // defaults to all non-static points available. When specified before the Connect function,
        // this filter expression will be used for the initial connection. Updating the filter
        // expression while a subscription is active will cause a resubscribe with new expression.
        public string FilterExpression
        {
          get => GetFilterExpression();
          set => SetFilterExpression(value);
        }

        // Gets or sets any metadata filters to be applied to incoming metadata. Each separate filter
        // should be separated by semi-colons.
        public string MetadataFilters
        {
          get => GetMetadataFilters();
          set => SetMetadataFilters(value);
        }

        // Gets or sets value that determines whether payload data is compressed using TSSC.
        public bool PayloadDataCompressed
        {
          get => IsPayloadDataCompressed();
          set => SetPayloadDataCompressed(value);
        }

        // Gets or sets value that determines whether the metadata transfer is compressed using GZip.
        public bool MetadataCompressed
        {
          get => IsMetadataCompressed();
          set => SetMetadataCompressed(value);
        }

        // Gets or sets flag that determines whether the signal index cache is compressed using GZip.
        public bool SignalIndexCacheCompressed
        {
          get => IsSignalIndexCacheCompressed();
          set => SetSignalIndexCacheCompressed(value);
        }
    %}}

    typedef boost::shared_ptr<SubscriberInstance> SubscriberInstancePtr;

    // Security modes used by the DataPublisher to secure data sent over the command channel.
    enum class SecurityMode
    {
        // No security.
        None,
        // Transport Layer Security.
        TLS
    };

    // Maps 16-bit runtime IDs to 128-bit globally unique IDs.
    // Additionally provides reverse lookup and an extra mapping
    // to human-readable measurement keys.
    class SignalIndexCache
    {
    public:
        // Determines whether an element with the given runtime ID exists in the signal index cache.
        bool Contains(int32_t signalIndex) const;

        // Gets the globally unique signal ID associated with the given 16-bit runtime ID.
        sttp::Guid GetSignalID(int32_t signalIndex) const;

        // Gets the first half of the human-readable measurement
        // key associated with the given 16-bit runtime ID.
        const std::string& GetSource(int32_t signalIndex) const;

        // Gets the second half of the human-readable measurement
        // key associated with the given 16-bit runtime ID.
        uint64_t GetID(int32_t signalIndex) const;

        // Gets the 16-bit runtime ID associated with the given globally unique signal ID.
        int32_t GetSignalIndex(const sttp::Guid& signalID) const;

        // Gets the mapped signal count
        uint32_t Count() const;
    };

    typedef boost::shared_ptr<SignalIndexCache> SignalIndexCachePtr;

    // Represents a subscriber connection to a data publisher
    %nodefaultctor SubscriberConnection;
    class SubscriberConnection
    {
    public:
        ~SubscriberConnection();

        // Gets subscriber UUID used when subscriber is known and pre-established
        const sttp::Guid& GetSubscriberID() const;

        // Gets a UUID representing a unique run-time identifier for the current subscriber connection,
        // this can be used to disambiguate when the same subscriber makes multiple connections
        const sttp::Guid& GetInstanceID() const;

        // Gets subscriber connection identification, e.g., remote IP/port, for display and logging references
        const std::string& GetConnectionID() const;

        // Gets subscriber communications port
        const std::string& GetHostName() const;

        // Gets established subscriber operational modes
        uint32_t GetOperationalModes() const;

        // Gets established subscriber string encoding
        uint32_t GetEncoding() const;

        // Gets flags that determines if this subscription is temporal based
        bool GetIsTemporalSubscription() const;

        // Gets the start time temporal processing constraint
        const sttp::datetime_t& GetStartTimeConstraint() const;

        // Gets the stop time temporal processing constraint
        const sttp::datetime_t& GetStopTimeConstraint() const;

        // Gets the desired processing interval, in milliseconds, for temporal history playback.
        // With the exception of the values of -1 and 0, this value specifies the desired processing interval for data, i.e.,
        // basically a delay, or timer interval, over which to process data. A value of -1 means to use the default processing
        // interval while a value of 0 means to process data as fast as possible.
        int32_t GetProcessingInterval() const;

        // Gets flag that determines if payload compression should be enabled in data packets
        bool GetUsingPayloadCompression() const;

        // Gets flag that determines if the compact measurement format should be used in data packets
        bool GetUsingCompactMeasurementFormat() const;

        // Gets flag that determines if time should be included in data packets when the compact measurement format used
        bool GetIncludeTime() const;

        // Gets flag that determines if local clock should be used for current time instead of latest reasonable timestamp
        // when using compact format with rotation of base time offsets
        bool GetUseLocalClockAsRealTime() const;

        // Gets the allowed past time deviation tolerance, in seconds (can be sub-second). This value is used to determine
        // reasonability of timestamps as compared to the local clock when using compact format and base time offsets.
        float64_t GetLagTime() const;

        // Gets the allowed future time deviation tolerance, in seconds (can be sub-second). This value is used to determine
        // reasonability of timestamps as compared to the local clock when using compact format and base time offsets.
        float64_t GetLeadTime() const;
        
        // Gets value used to control throttling speed for real-time subscriptions when tracking latest measurements.
        float64_t GetPublishInterval() const;

        // Gets flag that determines if time should be restricted to millisecond resolution in data packets when the
        // compact measurement format used; otherwise, full resolution time will be used
        bool GetUseMillisecondResolution() const;

        // Gets flag that determines if latest measurements should tracked for subscription throttling. When property is true,
        // subscription data speed will be reduced by the lag-time property for real-time subscriptions and the processing interval
        // property for temporal subscriptions.
        bool GetTrackLatestMeasurements() const;

        // Gets flag that determines if NaN values should be excluded from data packets
        bool GetIsNaNFiltered() const;

        // Gets flag that determines if connection is currently subscribed
        bool GetIsSubscribed() const;

        // Gets subscription details about subscriber
        const std::string& GetSubscriptionInfo() const;

        // Gets signal index cache for subscriber representing run-time mappings for subscribed points
        const SignalIndexCachePtr& GetSignalIndexCache() const;

        // Statistical functions
        uint64_t GetTotalCommandChannelBytesSent() const;
        uint64_t GetTotalDataChannelBytesSent() const;
        uint64_t GetTotalMeasurementsSent() const;

        bool CipherKeysDefined() const;
        std::vector<uint8_t> Keys(int32_t cipherIndex);
        std::vector<uint8_t> IVs(int32_t cipherIndex);

        void Start(bool connectionAccepted = true);
        void Stop(bool shutdownSocket = true);

        void CancelTemporalSubscription();

        bool SendResponse(uint8_t responseCode, uint8_t commandCode);
        bool SendResponse(uint8_t responseCode, uint8_t commandCode, const std::string& message);
        bool SendResponse(uint8_t responseCode, uint8_t commandCode, const std::vector<uint8_t>& data);
    };

    %extend SubscriberConnection {
    %proxycode
    %{
        // Gets subscriber remote IP address
        public string GetIPAddress() => Common.GetSubscriberConnectionIPAddress(this);
    %}}

    typedef boost::shared_ptr<SubscriberConnection> SubscriberConnectionPtr;

    %feature("director") PublisherInstance;
    class PublisherInstance
    {
    public:
        %csmethodmodifiers StatusMessage "protected virtual";
        virtual void StatusMessage(const std::string& message);	// Defaults output to cout

        %csmethodmodifiers ErrorMessage "protected virtual";
        virtual void ErrorMessage(const std::string& message);	// Defaults output to cerr

        %csmethodmodifiers ClientConnected "protected virtual";
        virtual void ClientConnected(const SubscriberConnectionPtr& connection);

        %csmethodmodifiers ClientDisconnected "protected virtual";
        virtual void ClientDisconnected(const SubscriberConnectionPtr& connection);

        %csmethodmodifiers ProcessingIntervalChangeRequested "protected virtual";
        virtual void ProcessingIntervalChangeRequested(const SubscriberConnectionPtr& connection);

        %csmethodmodifiers TemporalSubscriptionRequested "protected virtual";
        virtual void TemporalSubscriptionRequested(const SubscriberConnectionPtr& connection);

        %csmethodmodifiers TemporalSubscriptionCanceled "protected virtual";
        virtual void TemporalSubscriptionCanceled(const SubscriberConnectionPtr& connection);

        %csmethodmodifiers HandleUserCommand "protected virtual";
        virtual void HandleUserCommand(const SubscriberConnectionPtr& connection, uint32_t command, const std::vector<uint8_t>& buffer);

        %csmethodmodifiers PublisherInstance "protected";
        PublisherInstance();
        virtual ~PublisherInstance();

        // Define metadata from existing metadata tables
        void DefineMetadata(const std::vector<DeviceMetadataPtr>& deviceMetadata, const std::vector<MeasurementMetadataPtr>& measurementMetadata, const std::vector<PhasorMetadataPtr>& phasorMetadata, int32_t versionNumber = 0) const;

        // Define metadata from existing dataset
        void DefineMetadata(const sttp::data::DataSetPtr& metadata) const;

        // Gets primary metadata. This dataset contains all the normalized metadata tables that define
        // the available detail about the data points that can be subscribed to by clients.
        const sttp::data::DataSetPtr& GetMetadata() const;

        // Gets filtering metadata. This dataset, derived from primary metadata, contains a flattened
        // table used to subscribe to a filtered set of points with an expression, e.g.:
        // FILTER ActiveMeasurements WHERE SignalType LIKE '%PHA'
        const sttp::data::DataSetPtr& GetFilteringMetadata() const;

        // Filters primary MeasurementDetail metadata returning values as measurement metadata records
        std::vector<MeasurementMetadataPtr> FilterMetadata(const std::string& filterExpression) const;

        // Starts or restarts publisher using specified connection info
        virtual bool Start(uint16_t port, bool ipV6 = false);                       // Bind to default NIC
        virtual bool Start(const std::string& networkInterfaceIP, uint16_t port);   // Bind to specified NIC IP, format determines IP version
        
        // Shuts down publisher
        virtual void Stop();

        // Determines if publisher has been started
        bool IsStarted() const;

        %csmethodmodifiers PublishMeasurements "private";
        void PublishMeasurements(const SimpleMeasurement* measurements, int32_t count) const;

        // Node ID defines a unique identification for the publisher
        // instance that gets included in published metadata so that clients
        // can easily distinguish the source of the measurements
        %csmethodmodifiers GetNodeID "private";
        const sttp::Guid& GetNodeID() const;

        %csmethodmodifiers SetNodeID "private";
        void SetNodeID(const sttp::Guid& nodeID) const;

        %csmethodmodifiers GetSecurityMode "private";
        SecurityMode GetSecurityMode() const;

        %csmethodmodifiers SetSecurityMode "private";
        void SetSecurityMode(SecurityMode securityMode) const;

        %csmethodmodifiers GetMaximumAllowedConnections "private";
        int32_t GetMaximumAllowedConnections() const;

        %csmethodmodifiers SetMaximumAllowedConnections "private";
        void SetMaximumAllowedConnections(int32_t value) const;

        %csmethodmodifiers IsMetadataRefreshAllowed "private";
        bool IsMetadataRefreshAllowed() const;

        %csmethodmodifiers SetMetadataRefreshAllowed "private";
        void SetMetadataRefreshAllowed(bool allowed) const;

        %csmethodmodifiers IsNaNValueFilterAllowed "private";
        bool IsNaNValueFilterAllowed() const;

        %csmethodmodifiers SetNaNValueFilterAllowed "private";
        void SetNaNValueFilterAllowed(bool allowed) const;

        %csmethodmodifiers IsNaNValueFilterForced "private";
        bool IsNaNValueFilterForced() const;

        %csmethodmodifiers SetNaNValueFilterForced "private";
        void SetNaNValueFilterForced(bool forced) const;

        %csmethodmodifiers GetSupportsTemporalSubscriptions "private";
        bool GetSupportsTemporalSubscriptions() const;

        %csmethodmodifiers SetSupportsTemporalSubscriptions "private";
        void SetSupportsTemporalSubscriptions(bool value) const;

        %csmethodmodifiers GetCipherKeyRotationPeriod "private";
        uint32_t GetCipherKeyRotationPeriod() const;

        %csmethodmodifiers SetCipherKeyRotationPeriod "private";
        void SetCipherKeyRotationPeriod(uint32_t period) const;

        uint16_t GetPort() const;
        bool IsIPv6() const;

        // Statistical functions
        uint64_t GetTotalCommandChannelBytesSent() const;
        uint64_t GetTotalDataChannelBytesSent() const;
        uint64_t GetTotalMeasurementsSent() const;

        // Safely get list of subscriber connections. Vector will be cleared then appended to,
        // returns true if any connections were added
        bool TryGetSubscriberConnections(std::vector<SubscriberConnectionPtr>& subscriberConnections) const;

        void DisconnectSubscriber(const SubscriberConnectionPtr& connection) const;
        void DisconnectSubscriber(const sttp::Guid& instanceID) const;
    };

    // Proxy getters and setters as actual .NET properties
    %extend PublisherInstance {
    %proxycode
    %{
        // Gets or sets node ID that defines a unique identification for the publisher
        // instance that gets included in published metadata so that clients can easily
        // distinguish the source of the measurements
        public System.Guid NodeID
        {
            get => GetNodeID();
            set => SetNodeID(value);
        }

        public SecurityMode SecurityMode
        {
            get => GetSecurityMode();
            set => SetSecurityMode(value);
        }

        public int MaximumAllowedConnections
        {
            get => GetMaximumAllowedConnections();
            set => SetMaximumAllowedConnections(value);
        }

        public bool MetadataRefreshAllowed
        {
            get => IsMetadataRefreshAllowed();
            set => SetMetadataRefreshAllowed(value);
        }

        public bool NaNValueFilterAllowed
        {
            get => IsNaNValueFilterAllowed();
            set => SetNaNValueFilterAllowed(value);
        }

        public bool NaNValueFilterForced
        {
            get => IsNaNValueFilterForced();
            set => SetNaNValueFilterForced(value);
        }

        public bool SupportsTemporalSubscriptions
        {
            get => GetSupportsTemporalSubscriptions();
            set => SetSupportsTemporalSubscriptions(value);
        }

        public uint CipherKeyRotationPeriod
        {
            get => GetCipherKeyRotationPeriod();
            set => SetCipherKeyRotationPeriod(value);
        }

        [System.Runtime.InteropServices.DllImport("sttp.net.lib.dll", EntryPoint="CSharp_sttp_PublisherInstance_PublishMeasurements")]
        private static extern unsafe void InvokePublishMeasurements(System.Runtime.InteropServices.HandleRef publisherInstancePtr, Measurement* measurements, int length);

        public unsafe void PublishMeasurements(Measurement[] measurements)
        {
            fixed (Measurement* measurementsPtr = measurements)
                InvokePublishMeasurements(swigCPtr, measurementsPtr, measurements.Length);

            if (CommonPINVOKE.SWIGPendingException.Pending) throw CommonPINVOKE.SWIGPendingException.Retrieve();
        }

        public void PublishMeasurements(System.Collections.Generic.IEnumerable<Measurement> measurements) => PublishMeasurements(System.Linq.Enumerable.ToArray(measurements));
    %}}

    typedef boost::shared_ptr<PublisherInstance> PublisherInstancePtr;
}}