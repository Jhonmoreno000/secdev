#pragma once

#include <string>
#include <functional>
#include <memory>
#include <vector>

namespace secdev::ai {

using TokenCallback = std::function<void(const std::string& token)>;

class InferenceEngine {
public:
    InferenceEngine();
    ~InferenceEngine();

    bool LoadModel(const std::string& modelPath, uint32_t gpuLayers);
    void StreamInference(const std::string& prompt, TokenCallback onToken);
    void SetSystemPrompt(const std::string& systemPrompt);

    [[nodiscard]] bool IsLoaded() const { return isLoaded_; }
    [[nodiscard]] const std::string& CurrentModel() const { return currentModelPath_; }

private:
    bool isLoaded_{false};
    std::string currentModelPath_;
    std::string systemPrompt_;
};

} // namespace secdev::ai
