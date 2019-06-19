// The macro arguments for all these macros are the name of the exported class
// and the C++ type T of Nullable<T> to generate the typemaps for.

// Common part of the macros below, shouldn't be used directly.
%define DEFINE_NULLABLE_HELPER(NullableT, T)

// The Nullable<> specializations themselves are only going to be used
// inside our own code, the user will deal with either T? or T, depending on
// whether T is a value or a reference type, so make them private to our own
// assembly.
%typemap(csclassmodifiers) Nullable< T > "internal class"

// Do this to use reference typemaps instead of the pointer ones used by
// default for the member variables of this type.
//
// Notice that this must be done before %template below, SWIG must know about
// all features attached to the type before dealing with it.
%naturalvar Nullable< T >;

// Even although we're not going to really use them, we must still name the
// exported template instantiation, otherwise SWIG would give it an
// auto-generated name starting with SWIGTYPE which would be even uglier.
%template(NullableT) Nullable< T >;

%enddef

// This macro should be used for simple types that can be represented as
// Nullable<T> in C#.
//
// It exists in 2 versions: normal one for native C# code and special dumbed
// down version used for COM clients such as VBA.
#ifndef USE_OBJECT_FOR_SIMPLE_NULLABLE_VALUES

%define DEFINE_NULLABLE_SIMPLE(NullableT, T)

DEFINE_NULLABLE_HELPER(NullableT, T)

// Define the type we want to use in C#.
%typemap(cstype) Nullable< T >, const Nullable< T > & "$typemap(cstype, T)?"

// This typemap is used to convert C# NullableT to the handler passed to the
// intermediate native wrapper function. Notice that it assumes that the NullableT
// variable is obtained from the C# variable by prefixing it with "nullable", this
// is important for the csvarin typemap below which uses "nullablevalue" because the
// property value is accessible as "value" in C#.
%typemap(csin,
         pre="    NullableT nullable$csinput = $csinput.HasValue ? new NullableT($csinput.Value) : new NullableT();"
         ) const Nullable< T >& "$csclassname.getCPtr(nullable$csinput)"

// This is used for functions returning optional values.
%typemap(csout, excode=SWIGEXCODE) Nullable< T >, const Nullable< T >& {
    NullableT nullablevalue = new NullableT($imcall, $owner);$excode
    if (nullablevalue.HasValue())
      return nullablevalue.GetValueOrDefault();
    else
      return null;
  }

// This code is used for the optional-valued properties in C#.
%typemap(csvarin, excode=SWIGEXCODE2) const Nullable< T >& %{
    set {
      NullableT nullablevalue = value.HasValue ? new NullableT(value.Value) : new NullableT();
      $imcall;$excode
    }%}
%typemap(csvarout, excode=SWIGEXCODE2) const Nullable< T >& %{
    get {
      NullableT nullablevalue = new NullableT($imcall, $owner);$excode
      if (nullablevalue.HasValue())
          return nullablevalue.GetValueOrDefault();
      else
          return null;
    }%}

%enddef

#else // COM version of the macro

%define DEFINE_NULLABLE_SIMPLE(NullableT, T)

DEFINE_NULLABLE_HELPER(NullableT, T)

%typemap(cstype) Nullable< T >, const Nullable< T > & "object"

// Note: the test for the input value being empty (in the sense of VT_EMPTY) is
// done using ToString() because checking "is $typemap(cstype, T)" doesn't work
// for enum types and there doesn't seem to be any other way to generically
// check if the input argument contains a value or not.
%typemap(csin,
         pre="    NullableT nullable$csinput = $csinput.ToString().Length > 0 ? new NullableT(($typemap(cstype, T))$csinput) : new NullableT();"
        ) const Nullable< T >& "$csclassname.getCPtr(nullable$csinput)"

%typemap(csout, excode=SWIGEXCODE) Nullable< T >, const Nullable< T >& {
    NullableT nullablevalue = new NullableT($imcall, $owner);$excode
    if (nullablevalue.HasValue())
      return nullablevalue.GetValueOrDefault();
    else
      return null;
  }

// No need to define csvar{in,out} as we don't use properties for
// optional-valued values anyhow because VBA doesn't handle them neither.

%enddef

#endif // normal/COM versions of DEFINE_NULLABLE_SIMPLE

%define DEFINE_NULLABLE_CLASS_HELPER(NullableT, T)

DEFINE_NULLABLE_HELPER(NullableT, T)

%typemap(cstype) const Nullable< T > & "$typemap(cstype, T)"

%typemap(csin,
         pre="    NullableT nullable$csinput = $csinput != null ? new NullableT($csinput): new NullableT();"
         ) const Nullable< T >& "$csclassname.getCPtr(nullable$csinput)"

%typemap(csvarin, excode=SWIGEXCODE2) const Nullable< T >& %{
    set {
      NullableT nullablevalue = value != null ? new NullableT(value) : new NullableT();
      $imcall;$excode
    }%}
%typemap(csvarout, excode=SWIGEXCODE2) const Nullable< T >& %{
    get {
      NullableT nullablevalue = new NullableT($imcall, $owner);$excode
      return nullablevalue.HasValue() ? nullablevalue.GetValueOrDefault() : null;
    }%}

%enddef

// This macro should be used for optional classes which are represented by
// either a valid object or null in C#.
//
// Its arguments are the scope in which the class is defined (either a
// namespace or, possibly, a class name if this is a nested class) and the
// unqualified name of the class, the name of the exported optional type is
// constructed by prepending "Optional" to the second argument.
%define DEFINE_NULLABLE_CLASS(scope, classname)

DEFINE_NULLABLE_CLASS_HELPER(Optional ## classname, scope::classname)

%enddef
