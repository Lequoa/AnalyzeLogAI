# 📊 AI-Powered Log Analyzer (C# + Ollama)

## 🚀 Overview
The **AI-Powered Log Analyzer** is a lightweight C# application that leverages a locally hosted Large Language Model (LLM) via Ollama to analyze system logs and provide intelligent insights.

The application helps developers and support teams quickly understand errors, identify root causes, and receive actionable recommendations—reducing debugging time and improving system reliability.

---

## 🧠 Features
- 🔍 Log analysis using AI
- ⚠️ Automatic issue detection
- 📊 Severity classification (Low / Medium / High)
- 🛠️ Suggested fixes and recommendations
- 🏠 Runs fully locally (no external API required)
- ⚡ Customizable AI behavior using ModelFile

---

## 🏗️ Architecture

```
C# Console App (.NET)
       ↓
HTTP Client
       ↓
Ollama Local API (http://localhost:11434)
       ↓
LLM Model (e.g. llama3)
```

---

## ⚙️ Prerequisites

- .NET 8 or .NET 9 SDK
- Ollama installed and running locally
- A pulled model (e.g. llama3)

---

## 🧪 Getting Started

### 1. Run Ollama

```bash
ollama run llama3
```

This will:
- Download the model (if not already available)
- Start the local API server

---

### 2. Run the Application

```bash
dotnet run
```

---

### 3. Provide a Log Input

Example:

```
System.NullReferenceException: Object reference not set to an instance of an object
   at PaymentService.Process()
```

---

### 4. View AI Analysis Output

Example:

```
Issue: NullReferenceException
Severity: High
Cause: Object not initialized
Fix: Add null checks or validate dependency injection
```

---

## 🧾 API Request Example

The application sends a request to Ollama:

```json
{
  "model": "llama3",
  "prompt": "Analyze the following log and explain the issue, severity, and possible fix:\n{log}",
  "stream": false
}
```

---

## ⚡ Model Optimization (ModelFile)

One of the key learnings in this project is that **model behavior can be significantly improved using a ModelFile**.

### Benefits:
- More consistent responses
- Structured output (e.g. JSON)
- Better domain-specific analysis
- Reduced ambiguity in results

### Example ModelFile Customization:

```
SYSTEM:
You are a senior software engineer analyzing logs.

RULES:
- Always classify severity (Low, Medium, High)
- Provide clear root cause
- Suggest actionable fixes
- Keep responses concise and structured
```

---

## 📈 Future Enhancements

- 🔹 Convert to Web API (.NET)
- 🔹 Add structured JSON responses
- 🔹 Store historical logs for pattern detection
- 🔹 Integrate with logging frameworks (Serilog, NLog)
- 🔹 Add retry and resilience handling
- 🔹 Build dashboard for visualization
- 🔹 Integrate with messaging systems (e.g. queues)

---

## 🧠 Lessons Learned

- Local AI is practical and powerful for developer tooling
- Prompt design significantly affects output quality
- ModelFile customization enables domain-specific intelligence
- Latency can be managed through:
  - Prompt optimization
  - Model selection
  - Response structuring

---

## ⚠️ Known Limitations

- ⏱️ Ollama response time can be slower compared to cloud APIs
- 🧠 Model accuracy depends on prompt quality
- 📦 Large models may require significant system resources

---

## 🤝 Contribution

Feel free to fork the repository and improve:
- Prompt engineering
- Model tuning
- Performance optimizations
- UI/UX enhancements

---

## 📌 Conclusion

This project demonstrates how AI can be practically integrated into everyday development workflows using local tooling. It serves as a foundation for building more advanced AI-assisted engineering solutions.
