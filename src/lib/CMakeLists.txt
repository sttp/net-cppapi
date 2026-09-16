#******************************************************************************************************
#  CMakeLists.txt - Gbtc
#
#  Copyright © 2019, Grid Protection Alliance.  All Rights Reserved.
#
#  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
#  the NOTICE file distributed with this work for additional information regarding copyright ownership.
#  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
#  file except in compliance with the License. You may obtain a copy of the License at:
#
#      http://opensource.org/licenses/MIT
#
#  Unless agreed to in writing, the subject software distributed under the License is distributed on an
#  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
#  License for the specific language governing permissions and limitations.
#
#  Code Modification History:
#  ----------------------------------------------------------------------------------------------------
#  11/24/2019 - J. Ritchie Carroll
#       Generated original version of source code.
#
#******************************************************************************************************

cmake_minimum_required (VERSION 3.13)
project (sttp.net.lib VERSION 1.1.3 DESCRIPTION ".NET STTP C++ API Wrapper Library" LANGUAGES CXX)

# Keep direct single-configuration builds aligned with the package output layout.
if (NOT CMAKE_CONFIGURATION_TYPES AND NOT CMAKE_BUILD_TYPE)
    set (CMAKE_BUILD_TYPE Release CACHE STRING "Build configuration" FORCE)
endif ()

set (CMAKE_POSITION_INDEPENDENT_CODE ON)

# Embed Boost in the NuGet native library. Its static archives must be built with -fPIC.
set (Boost_USE_STATIC_LIBS ON)
find_package (Boost REQUIRED COMPONENTS thread date_time iostreams chrono)
if ("${Boost_MAJOR_VERSION}.${Boost_MINOR_VERSION}" VERSION_LESS "1.69")
    find_package (Boost REQUIRED COMPONENTS system)
endif ()

# Reuse the native API's source list, compiler settings, and transitive dependencies.
add_subdirectory (cppapi/src cppapi EXCLUDE_FROM_ALL)
target_compile_definitions (sttp PRIVATE SWIG)

# Replace the native target's bare Boost linker names with imported static targets.
# Preserve the native target's other dependencies.
get_target_property (STTP_NATIVE_LIBRARIES sttp LINK_LIBRARIES)
set (STTP_WRAPPER_LIBRARIES)
foreach (dependency IN LISTS STTP_NATIVE_LIBRARIES)
    if (dependency MATCHES "^boost_(.+)$")
        list (APPEND STTP_WRAPPER_LIBRARIES "Boost::${CMAKE_MATCH_1}")
    else ()
        list (APPEND STTP_WRAPPER_LIBRARIES "${dependency}")
    endif ()
endforeach ()
set_target_properties (sttp PROPERTIES
    LINK_LIBRARIES "${STTP_WRAPPER_LIBRARIES}"
    INTERFACE_LINK_LIBRARIES "${STTP_WRAPPER_LIBRARIES}"
)
add_library (sttp.net.lib SHARED sttp.net.lib/sttp.net.lib.cpp)
target_compile_features (sttp.net.lib PRIVATE cxx_std_20)
target_compile_definitions (sttp.net.lib PRIVATE SWIG BOOST_CONFIG_SUPPRESS_OUTDATED_MESSAGE ANTLR4CPP_STATIC)
target_include_directories (sttp.net.lib PRIVATE ${Boost_INCLUDE_DIRS})
target_link_libraries (sttp.net.lib PRIVATE sttp)

# Match the name used by the managed loader and NuGet native asset paths.
set_target_properties (sttp.net.lib PROPERTIES
    PREFIX ""
    LIBRARY_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/Libraries"
)

# Fail at link time if any native dependency is missing.
if (UNIX AND NOT APPLE)
    target_link_options (sttp.net.lib PRIVATE "-Wl,--no-undefined")
endif ()

# Stage Linux x64 binaries where sttp.net.csproj collects the NuGet native assets.
if (CMAKE_SYSTEM_NAME STREQUAL "Linux" AND CMAKE_SYSTEM_PROCESSOR MATCHES "^(x86_64|amd64|AMD64)$")
    get_filename_component (STTP_REPOSITORY_ROOT "${CMAKE_CURRENT_SOURCE_DIR}/../.." ABSOLUTE)
    set (STTP_PACKAGE_NATIVE_DIRECTORY "${STTP_REPOSITORY_ROOT}/build/output/x64/$<CONFIG>/lib")
    add_custom_command (TARGET sttp.net.lib POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E make_directory "${STTP_PACKAGE_NATIVE_DIRECTORY}"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different
            "$<TARGET_FILE:sttp.net.lib>" "${STTP_PACKAGE_NATIVE_DIRECTORY}/$<TARGET_FILE_NAME:sttp.net.lib>"
        COMMENT "Copying Linux native library to the NuGet input directory"
        VERBATIM
    )
endif ()