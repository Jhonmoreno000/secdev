#pragma once

#include <string>
#include <cstdint>

namespace secdev::core {

struct WindowConfig {
    uint32_t width{1600};
    uint32_t height{960};
    bool vsync{true};
    uint32_t targetFps{120};
    bool borderless{true};
};

struct ThemeConfig {
    std::string background{"#0d1117"};
    std::string surface{"#161b22"};
    std::string accentPrimary{"#00ff9d"};
    std::string accentSecondary{"#00e5ff"};
    std::string warning{"#d29922"};
    std::string danger{"#f85149"};
};

struct AIConfig {
    std::string modelPath{"models/qwen2.5-coder-7b-instruct-q4_k_m.gguf"};
    uint32_t contextSize{32768};
    uint32_t gpuLayersOffload{28};
    uint32_t threads{8};
    float temperature{0.2f};
    bool offlineModeEnforced{true};
};

class Config {
public:
    static Config& Instance();

    bool LoadFromFile(const std::string& path);
    void PrintSummary() const;

    [[nodiscard]] const WindowConfig& Window() const { return window_; }
    [[nodiscard]] const ThemeConfig& Theme() const { return theme_; }
    [[nodiscard]] const AIConfig& AI() const { return ai_; }

    void SetModelPath(const std::string& path) { ai_.modelPath = path; }

private:
    Config() = default;

    WindowConfig window_;
    ThemeConfig theme_;
    AIConfig ai_;
};

} // namespace secdev::core
