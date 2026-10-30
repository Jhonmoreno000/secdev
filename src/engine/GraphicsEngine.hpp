#pragma once

#include <string>
#include <cstdint>
#include <vector>

namespace secdev::engine {

struct TelemetryStats {
    float vramUsageGB{0.0f};
    float vramTotalGB{4.0f};
    float tokensPerSec{0.0f};
    float gpuTemperatureC{0.0f};
    uint32_t activeFps{120};
    bool offlineSecured{true};
};

class GraphicsEngine {
public:
    GraphicsEngine();
    ~GraphicsEngine();

    bool Initialize(uint32_t width, uint32_t height, const std::string& title);
    void RunMainLoop();
    void Shutdown();

    void UpdateTelemetry(const TelemetryStats& stats);
    void AppendCodeStream(const std::string& chunk);
    void AppendAgentToken(const std::string& token);

private:
    void RenderHeader();
    void RenderCodeWorkspace();
    void RenderAIAgentPane();
    void RenderTelemetryHUD();

    bool isRunning_{false};
    uint32_t width_{1600};
    uint32_t height_{960};
    std::string title_;

    TelemetryStats telemetry_;
    std::string currentCodeBuffer_;
    std::string currentAgentResponse_;
};

} // namespace secdev::engine
