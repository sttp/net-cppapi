#!/usr/bin/env bash
set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
repo_root="$(cd -- "$script_dir/../../.." && pwd)"
build_root="${STTP_BUILD_ROOT:-$script_dir/bin/linux}"
jobs="${STTP_BUILD_JOBS:-6}"

# Uses the checked-in SWIG-generated C++ source; SWIG is needed only to regenerate it.
for config in Debug Release; do
    cmake -S "$script_dir/.." -B "$build_root/$config" -DCMAKE_BUILD_TYPE="$config" "$@"
    cmake --build "$build_root/$config" --target sttp.net.lib --parallel "$jobs"
    destination="$repo_root/build/output/x64/$config/lib"
    mkdir -p "$destination"
    cp "$build_root/$config/Libraries/sttp.net.lib.so" "$destination/sttp.net.lib.so"
done
