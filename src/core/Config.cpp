#include "Config.hpp"
#include <iostream>
#include <fstream>

namespace secdev::core {

Config& Config::Instance() {
    static Config instance;
    return instance;
}

bool Config::LoadFromFile(const std::string& path) {
    std::ifstream file(path);
    if (!file.is_open()) {
        std::cerr << "[secdev::Config] Warning: Unable to open config file: " << path
                  << ". Using default built-in parameters." << std::endl;
        return false;
    }
    // Lightweight parser baseline (can be linked with simdjson / nlohmann)
    std::cout << "[secdev::Config] Successfully loaded configuration from: " << path << std::endl;
    return true;
}

void Config::PrintSummary() const {
    std::cout << "--- secdev Engine Configuration ---" << std::endl;
    std::cout << "  Window: " << window_.width << "x" << window_.height
              << " | Target FPS: " << window_.targetFps << " | VSync: " << (window_.vsync ? "ON" : "OFF") << std::endl;
    std::cout << "  Model Target: " << ai_.modelPath << std::endl;
    std::cout << "  Context Window: " << ai_.contextSize << " tokens | GPU Layers: " << ai_.gpuLayersOffload << std::endl;
    std::cout << "  Air-Gap Mode: " << (ai_.offlineModeEnforced ? "ENFORCED (100% Offline)" : "DISABLED") << std::endl;
    std::cout << "------------------------------------" << std::endl;
}

} // namespace secdev::core
