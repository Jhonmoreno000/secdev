# Security Policy

## Responsible Disclosure

Security is a foundational pillar of `secdev`. If you discover a vulnerability or security issue, please notify the maintainers responsibly before public disclosure.

### Reporting Process

1. **Email:** Send details to `security@local.secdev` or open a confidential Security Advisory on GitHub.
2. **Details to include:**
   - Detailed description of the vulnerability.
   - Proof of Concept (PoC) or reproducible steps.
   - Impact assessment (e.g., buffer overflow in custom shader parser, sandbox escape, prompt injection in local agent).
3. **Response SLA:** Maintainers acknowledge receipt within 48 hours and provide a remediation timeline.

---

## Security Guarantees of `secdev`

1. **Strict Air-Gap & Zero Telemetry:**
   `secdev` is designed for red-teamers, reverse engineers, and developers working on sensitive code. It NEVER sends telemetry, code snippets, or model prompts over the internet.
2. **Isolated Execution Sandbox:**
   Code generated or audited by the local model runs in a restricted subprocess/sandbox with configurable permission barriers.
