cmake_minimum_required(VERSION 3.22)
project(secdev VERSION 1.0.0 LANGUAGES C CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

# Compiler Warnings & Strict Security Flags
if(MSVC)
    add_compile_options(/W4 /permissive- /utf-8 /guard:cf)
    add_link_options(/GUARD:CF /DYNAMICBASE /NXCOMPAT /HIGHENTROPYVA)
else()
    add_compile_options(-Wall -Wextra -Wpedantic -fstack-protector-strong)
endif()

# Output directories
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR})
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR})
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR})

# Options
option(SECDEV_BUILD_TESTS "Build test suite" ON)
option(SECDEV_HEADLESS_TESTING "Enable headless CI test execution" OFF)

# Source files
set(SECDEV_SOURCES
    src/main.cpp
    src/core/Config.cpp
    src/engine/GraphicsEngine.cpp
    src/ai/InferenceEngine.cpp
)

set(SECDEV_HEADERS
    src/core/Config.hpp
    src/engine/GraphicsEngine.hpp
    src/ai/InferenceEngine.hpp
)

add_executable(secdev ${SECDEV_SOURCES} ${SECDEV_HEADERS})

target_include_directories(secdev PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/src
)

if(SECDEV_HEADLESS_TESTING)
    target_compile_definitions(secdev PRIVATE SECDEV_HEADLESS=1)
endif()

# Installation rules
install(TARGETS secdev DESTINATION bin)
install(DIRECTORY config DESTINATION .)
install(DIRECTORY scripts DESTINATION .)
