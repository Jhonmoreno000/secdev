---
name: ui-ux-design-systems
category: Frontend & UI Architecture
source: public-catalog/design-systems-wcag22
tokens_cost: 145
description: Architectural UI/UX design rules, component tokens, responsive breakpoints, and WCAG 2.2 accessibility standards.
---
# Skill: UI/UX & Design Systems Architecture

## 1. Visual Hierarchy & Token System
- **Color Architecture:** Use semantic surface tokens (`bg-root`, `bg-surface`, `bg-elevated`, `border-subtle`) with desaturated, low-fatigue contrast ratios (>= 4.5:1 for body text, >= 3:1 for UI components per WCAG 2.2 AA).
- **Typography Scale:** Modular scale (`11px` metadata, `12.5px` body/code, `14px` section headers, `18px` titles). Pair geometric sans-serif (`Inter`/`Segoe UI Variable`) with tabular monospace (`JetBrains Mono`/`Consolas`) for numerical/code alignment.
- **Spacing & Grid:** Strict 4px/8px spatial grid (`4, 8, 12, 16, 24, 32px`). Avoid arbitrary margins.

## 2. Interaction & Motion Engineering
- **Micro-Transitions:** 150ms–220ms duration using `cubic-bezier(0.22, 1, 0.36, 1)` (Ease-Out Cubic). Never block user input during transitions.
- **States:** Every interactive element must define `default`, `hover`, `active/pressed`, `focus-visible`, and `disabled` states.
- **Responsive Breakpoints:**
  - `Compact (< 920px)`: Single-column or vertical stack, collapsible drawers.
  - `Medium (920px–1260px)`: Dual-pane split, hide secondary minimaps/columns.
  - `Wide (> 1260px)`: Multi-pane workstation with full telemetry and auxiliary inspectors.
