# Verify "SWIG_LIB " is defined
if [[ -z "$SWIG_LIB" ]]; then
    echo 'The "SWIG_LIB" environmental variable not found, abroting compile.'
    exit
fi

echo ' Compiling inpendendent debug version of "libsttp.a"...'
mkdir -p bin/Debug
pushd bin/Debug
cmake ../../../cppapi/src -DCMAKE_BUILD_TYPE=Debug -DCMAKE_POSITION_INDEPENDENT_CODE=ON -DCMAKE_CXX_FLAGS="-Wno-unknown-pragmas"
make -j6
popd

echo ' Compiling debug version of "sttp.net.lib.so"...'
mkdir -p obj/Debug
gcc -g -D SWIG -c -fPIC sttp.net.lib.cpp -o obj/Debug/sttp.net.lib.o
gcc -g -shared obj/Debug/sttp.net.lib.o bin/Debug/Libraries/libsttp.a -o bin/Debug/sttp.net.lib.so

echo ' Compiling inpendendent release version of "libsttp.a"...'
mkdir -p bin/Release
pushd bin/Release
cmake ../../../cppapi/src -DCMAKE_POSITION_INDEPENDENT_CODE=ON -DCMAKE_CXX_FLAGS="-Wno-unknown-pragmas"
make -j6
popd

echo ' Compiling release version of "sttp.net.lib.so"...'
mkdir -p obj/Release
gcc -D SWIG -c -fPIC sttp.net.lib.cpp -o obj/Release/sttp.net.lib.o
gcc -shared obj/Release/sttp.net.lib.o bin/Release/Libraries/libsttp.a -o bin/Release/sttp.net.lib.so

# Copy resulting compiled "sttp.net.lib.so" files to target folders:
cp bin/Debug/sttp.net.lib.so ../../../build/output/x64/Debug/lib
cp bin/Release/sttp.net.lib.so ../../../build/output/x64/Release/lib