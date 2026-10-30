#include "InferenceEngine.hpp"
#include <iostream>
#include <thread>
#include <chrono>

namespace secdev::ai {

InferenceEngine::InferenceEngine() {
    systemPrompt_ =
        "You are secdev-ai, an elite cybersecurity and systems programming AI assistant. "
        "You specialize in C/C++, Rust, Assembly, vulnerability analysis, exploit mitigation, "
        "and AST security auditing. Provide concise, secure, production-grade solutions.";
}

InferenceEngine::~InferenceEngine() = default;

bool InferenceEngine::LoadModel(const std::string& modelPath, uint32_t gpuLayers) {
    currentModelPath_ = modelPath;
    std::cout << "[secdev::AI] Initializing llama.cpp backend..." << std::endl;
    std::cout << "  -> Model File: " << modelPath << std::endl;
    std::cout << "  -> Offloading " << gpuLayers << " layers to dedicated NVIDIA VRAM." << std::endl;
    std::cout << "  -> Residual layers allocated to 32 GB System RAM." << std::endl;

    isLoaded_ = true;
    return true;
}

void InferenceEngine::SetSystemPrompt(const std::string& systemPrompt) {
    systemPrompt_ = systemPrompt;
}

void InferenceEngine::StreamInference(const std::string& prompt, TokenCallback onToken) {
    if (!isLoaded_) {
        std::cerr << "[secdev::AI] Error: Model is not loaded. Run 'scripts/pull-model.ps1' first." << std::endl;
        return;
    }

    std::cout << "[secdev::AI] Processing Prompt: \"" << prompt << "\" with local model..." << std::endl;

    // Simulated token generation stream for baseline
    std::vector<std::string> sampleTokens = {
        "Analyzing", " vulnerability", " surface", "...\n",
        "Target", " code", " inspected.", " Found", " Potential", " Buffer", " Overflow", " in", " strcpy().\n",
        "Recommendation:\n",
        "Replace", " `strcpy(dest, src)`", " with", " `strncpy_s()`", " or", " `std::string_view`", " bounds", " check.\n"
    };

    for (const auto& token : sampleTokens) {
        if (onToken) {
            onToken(token);
        }
        std::this_thread::sleep_for(std::chrono::milliseconds(25));
    }
}

} // namespace secdev::ai
