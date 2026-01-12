# Neumorphic Conference Pass — Design Specification

> **Purpose**: Framework-agnostic specification for recreating a monochromatic neumorphic conference badge UI. Suitable for implementation in any platform: Web (HTML/CSS, React, Vue), Mobile (SwiftUI, Jetpack Compose, Flutter, .NET MAUI), or Desktop.

---

## TUI Layout Reference

```
                    40px
                  ┌──────┐
                  │  ◯   │ ← Lanyard Hole (centered, overlaps card by 20px)
                  └──┬───┘
       ┌─────────────┴─────────────┐
       │                           │
       │   ┌─────────────────┐     │  ← Event Badge (pill, inset)
       │   │  ●  EVENT NAME  │     │     Contents: dot + mono text
       │   └─────────────────┘     │
       │                           │
       │   ┌──────┐                │
       │   │      │  FULL NAME     │  ← Avatar Section
       │   │  MG  │  Role • Title  │     Square avatar + text stack
       │   │      │                │
       │   └──────┘                │
       │                           │
       │   ════════════════════    │  ← Divider (inset line, 2px)
       │                           │
       │   ┌────┐  LABEL           │
       │   │ ⌖  │  Value Text      │  ← Detail Row 1 (icon + content)
       │   └────┘                  │
       │                           │
       │   ┌────┐  LABEL           │
       │   │ ⌗  │  Value Text      │  ← Detail Row 2
       │   └────┘                  │
       │                           │
       │   ┌────┐  LABEL           │
       │   │ ◈  │  Value Text      │  ← Detail Row 3
       │   └────┘                  │
       │                           │
       │   ┌────────┐              │
       │   │ ▓▓▓▓▓▓ │  Scan text   │  ← QR Section
       │   │ ▓▓▓▓▓▓ │  Pass ID     │     Inset square + info text
       │   │ ▓▓▓▓▓▓ │              │
       │   └────────┘              │
       │                           │
       │   ┌───────────────────┐   │
       │   │   ✓  ACCESS LEVEL │   │  ← Access Badge (raised, interactive)
       │   └───────────────────┘   │
       │                           │
       └───────────────────────────┘
       
       ◄──────── 340px ────────────►
```

### Component Hierarchy Tree

```
PassContainer
├── LanyardHole
└── PassCard
    ├── EventBadge
    │   ├── StatusDot
    │   └── EventText
    ├── AvatarSection
    │   ├── Avatar
    │   │   └── Initials
    │   └── AttendeeInfo
    │       ├── Name
    │       └── Role
    ├── Divider
    ├── DetailsGrid
    │   └── DetailItem (×3)
    │       ├── IconContainer
    │       │   └── Icon
    │       └── DetailContent
    │           ├── Label
    │           └── Value
    ├── QRSection
    │   ├── QRCode
    │   │   └── QRPattern
    │   └── QRInfo
    │       ├── ScanText
    │       └── PassID
    └── AccessBadge
        ├── BadgeIcon
        └── BadgeText
```

---

## 1. Design System Tokens

### 1.1 Color Palette (Monochromatic Gray)

| Token              | Hex       | RGB              | Usage                          |
|--------------------|-----------|------------------|--------------------------------|
| `bg`               | `#E0E5EC` | `224, 229, 236`  | Base background, all surfaces  |
| `shadow-dark`      | `#A3B1C6` | `163, 177, 198`  | Dark shadow (bottom-right)     |
| `shadow-light`     | `#FFFFFF` | `255, 255, 255`  | Light shadow (top-left)        |
| `text-primary`     | `#2D3748` | `45, 55, 72`     | Headings, main content         |
| `text-secondary`   | `#5A6578` | `90, 101, 120`   | Subtitles, supporting text     |
| `text-muted`       | `#8A95A5` | `138, 149, 165`  | Labels, hints                  |
| `accent`           | `#4A5568` | `74, 85, 104`    | Status indicators, emphasis    |

### 1.2 Neumorphic Shadow System

**Principle**: All elements share the same base color (`bg`). Depth is created entirely through shadows.

#### Raised Element (Extruded)
```
shadow: 
  offset-x: [distance]
  offset-y: [distance]
  blur: [distance × 2]
  color: shadow-dark
  
  offset-x: -[distance]
  offset-y: -[distance]
  blur: [distance × 2]
  color: shadow-light
```

#### Inset Element (Pressed)
```
shadow (inset): 
  offset-x: [distance]
  offset-y: [distance]
  blur: [distance × 2]
  color: shadow-dark
  
  offset-x: -[distance]
  offset-y: -[distance]
  blur: [distance × 2]
  color: shadow-light
```

#### Shadow Scale

| Size    | Distance | Blur | Use Case                    |
|---------|----------|------|-----------------------------|
| `xs`    | 1px      | 2px  | Subtle lines, dividers      |
| `sm`    | 3px      | 6px  | Small badges, pills         |
| `md`    | 6px      | 12px | Icons, buttons, QR area     |
| `lg`    | 8px      | 16px | Avatar                      |
| `xl`    | 20px     | 40px | Main card                   |

### 1.3 Typography

| Role            | Family              | Weight | Size     | Letter Spacing | Line Height |
|-----------------|---------------------|--------|----------|----------------|-------------|
| `display`       | Outfit              | 800    | 24px     | -0.02em        | 1.1         |
| `body`          | Outfit              | 400    | 14px     | 0              | 1.4         |
| `body-semibold` | Outfit              | 600    | 15px     | 0              | 1.4         |
| `mono-label`    | JetBrains Mono      | 500    | 10.5px   | 0.1em          | 1.2         |
| `mono-small`    | JetBrains Mono      | 400    | 12px     | 0.05em         | 1.2         |
| `initials`      | Outfit              | 800    | 28px     | -0.02em        | 1           |

**Font Fallbacks**:  
- Outfit → "SF Pro Display", "Segoe UI", system-ui, sans-serif  
- JetBrains Mono → "SF Mono", "Cascadia Code", "Consolas", monospace

### 1.4 Spacing Scale

| Token  | Value | Usage                              |
|--------|-------|------------------------------------|
| `xs`   | 2px   | Inline gaps, micro spacing         |
| `sm`   | 8px   | Icon padding, tight gaps           |
| `md`   | 16px  | Standard gaps, section spacing     |
| `lg`   | 20px  | Component gaps                     |
| `xl`   | 24px  | Large section margins              |
| `2xl`  | 40px  | Card padding                       |

### 1.5 Border Radius Scale

| Token    | Value | Usage                     |
|----------|-------|---------------------------|
| `xs`     | 1px   | QR cells                  |
| `sm`     | 12px  | Icon containers, buttons  |
| `md`     | 16px  | QR container              |
| `lg`     | 20px  | Avatar                    |
| `xl`     | 24px  | Main card                 |
| `pill`   | 50px  | Event badge               |
| `circle` | 50%   | Lanyard hole, status dot  |

---

## 2. Component Specifications

### 2.1 Pass Container (Root)

```yaml
element: PassContainer
layout: flex
direction: column
alignment: center (both axes)
min-height: 100vh
padding: 32px
background: bg
```

### 2.2 Lanyard Hole

```yaml
element: LanyardHole
shape: circle
dimensions: 40px × 40px
position: centered horizontally, overlaps card top by 20px (negative margin)
z-index: elevated above card
background: bg
shadow: inset, size sm (3px/6px)

inner-circle:
  shape: circle
  dimensions: 20px × 20px
  position: centered
  background: bg
  shadow: raised, size sm (4px/8px)
```

### 2.3 Pass Card (Main Container)

```yaml
element: PassCard
dimensions: 340px width, height auto
padding: 40px
border-radius: xl (24px)
background: bg
shadow: raised, size xl (20px/40px)
overflow: hidden

pseudo-overlay:
  type: gradient texture
  content: 
    - radial-gradient at 20% 80%, rgba(255,255,255,0.1), transparent 50%
    - radial-gradient at 80% 20%, rgba(0,0,0,0.03), transparent 50%
  pointer-events: none
```

### 2.4 Event Badge

```yaml
element: EventBadge
layout: inline-flex
alignment: center
gap: 8px
padding: 8px horizontal, 16px vertical
border-radius: pill (50px)
background: bg
shadow: inset, size sm (3px/6px)
margin-bottom: 24px
animation: pulse (see animations)

children:
  - StatusDot:
      shape: circle
      dimensions: 8px × 8px
      background: accent
      animation: blink (see animations)
  
  - EventText:
      typography: mono-label
      color: text-secondary
      transform: uppercase
```

### 2.5 Avatar Section

```yaml
element: AvatarSection
layout: flex
direction: row
alignment: center
gap: 20px
margin-bottom: 32px

children:
  - Avatar:
      dimensions: 80px × 80px
      border-radius: lg (20px)
      background: bg
      shadow: raised, size lg (8px/16px)
      layout: flex
      alignment: center (both)
      
      children:
        - Initials:
            typography: initials
            color: text-primary
  
  - AttendeeInfo:
      layout: flex
      direction: column
      
      children:
        - Name:
            typography: display
            color: text-primary
            margin-bottom: 4px
        
        - Role:
            typography: body
            color: text-secondary
```

### 2.6 Divider

```yaml
element: Divider
dimensions: 100% width × 2px height
border-radius: 1px
background: bg
shadow: inset, size xs (1px/2px)
margin: 24px vertical
```

### 2.7 Details Grid

```yaml
element: DetailsGrid
layout: flex or grid
direction: column
gap: 16px
```

### 2.8 Detail Item (×3)

```yaml
element: DetailItem
layout: flex
direction: row
alignment: center
gap: 16px

children:
  - IconContainer:
      dimensions: 44px × 44px
      border-radius: sm (12px)
      background: bg
      shadow: raised, size md (6px/12px)
      layout: flex
      alignment: center (both)
      transition: shadow 300ms ease
      
      hover-state:
        shadow: inset, size sm (4px/8px)
      
      children:
        - Icon:
            dimensions: 20px × 20px
            stroke: text-secondary
            stroke-width: 2
            fill: none
  
  - DetailContent:
      layout: flex
      direction: column
      flex: 1
      
      children:
        - Label:
            typography: mono-label (6.5px adjusted)
            color: text-muted
            transform: uppercase
            margin-bottom: 2px
        
        - Value:
            typography: body-semibold
            color: text-primary
```

**Icons (stroke-based, 24×24 viewBox)**:

| Row     | Icon Description                                           |
|---------|------------------------------------------------------------|
| Venue   | Map pin: circle at (12,10) r=3, path to (12,23) with curve |
| Date    | Calendar: rect, two vertical lines at x=8,16, horizontal at y=10 |
| Session | Layers: three stacked chevron paths                        |

### 2.9 QR Section

```yaml
element: QRSection
layout: flex
direction: row
alignment: center
gap: 20px
margin-top: 32px

children:
  - QRCode:
      dimensions: 90px × 90px
      border-radius: md (16px)
      background: bg
      shadow: inset, size md (6px/12px)
      layout: flex
      alignment: center (both)
      
      children:
        - QRPattern:
            dimensions: 60px × 60px
            layout: grid
            columns: 7
            gap: 2px
            
            cells:
              dimensions: ~6.5px × ~6.5px each
              border-radius: xs (1px)
              filled: text-primary
              empty: transparent
              
              pattern (7×7 binary, 1=filled, 0=empty):
                [1,1,1,0,1,1,1]
                [1,0,1,1,1,0,1]
                [1,0,1,0,1,0,1]
                [0,0,0,1,0,0,0]
                [1,0,1,0,1,0,1]
                [1,0,1,1,1,0,1]
                [1,1,1,0,1,1,1]
  
  - QRInfo:
      layout: flex
      direction: column
      flex: 1
      
      children:
        - ScanText:
            typography: body (12.8px)
            color: text-secondary
            margin-bottom: 4px
        
        - PassID:
            typography: mono-small
            color: text-muted
```

### 2.10 Access Badge

```yaml
element: AccessBadge
layout: flex
direction: row
alignment: center
justify: center
gap: 10px
padding: 12px vertical, 20px horizontal
border-radius: sm (12px)
background: bg
shadow: raised, size md (6px/12px)
margin-top: 24px
cursor: pointer
transition: shadow 300ms ease

hover-state:
  shadow: inset, size sm (4px/8px)

children:
  - BadgeIcon:
      dimensions: 18px × 18px
      stroke: currentColor (inherits text-secondary)
      stroke-width: 2
      fill: none
      content: shield with checkmark
  
  - BadgeText:
      typography: mono-label (12px)
      color: text-secondary
      transform: uppercase
      letter-spacing: 0.15em
```

---

## 3. Animation Specifications

### 3.1 Entry Animations

#### Float In (Container)
```yaml
name: floatIn
duration: 800ms
easing: cubic-bezier(0.34, 1.56, 0.64, 1)  # Spring overshoot
initial:
  opacity: 0
  transform: translateY(30px)
final:
  opacity: 1
  transform: translateY(0)
```

#### Slide In (Detail Items, QR, Access Badge)
```yaml
name: slideIn
duration: 500ms
easing: ease-out
fill-mode: both
initial:
  opacity: 0
  transform: translateX(-10px)
final:
  opacity: 1
  transform: translateX(0)

stagger-delays:
  DetailItem[1]: 200ms
  DetailItem[2]: 300ms
  DetailItem[3]: 400ms
  QRSection: 500ms
  AccessBadge: 600ms
```

### 3.2 Ambient Animations

#### Pulse (Event Badge)
```yaml
name: pulse
duration: 3000ms
easing: ease-in-out
iteration: infinite

keyframes:
  0%, 100%: shadow: inset 3px/6px
  50%: shadow: inset 2px/4px
```

#### Blink (Status Dot)
```yaml
name: blink
duration: 2000ms
easing: ease-in-out
iteration: infinite

keyframes:
  0%, 100%: opacity: 1
  50%: opacity: 0.4
```

### 3.3 Interactive Transitions

```yaml
hover-transition:
  property: box-shadow
  duration: 300ms
  easing: ease
```

---

## 4. Content Placeholders

| Field       | Example Value                     | Constraints           |
|-------------|-----------------------------------|-----------------------|
| Event Name  | "CODEMASH 2025"                   | Uppercase, ≤20 chars  |
| Full Name   | "Matt Goldman"                    | ≤24 chars             |
| Role        | "Speaker • Developer Relations"   | ≤35 chars             |
| Initials    | "MG"                              | 2 characters          |
| Venue       | "Kalahari Resort, Sandusky"       | ≤30 chars             |
| Date        | "January 14–17, 2025"             | Date range format     |
| Session     | "Cross-Platform Development"      | ≤30 chars             |
| Scan Text   | "Scan for schedule & materials"   | ≤35 chars             |
| Pass ID     | "ID: CM25-SPK-0847"               | Format: XX00-XXX-0000 |
| Access      | "ALL ACCESS PASS"                 | Uppercase, ≤20 chars  |

---

## 5. Responsive Considerations

| Viewport       | Adaptation                                          |
|----------------|-----------------------------------------------------|
| < 380px        | Card width: 100%, padding: 24px                     |
| 380px – 768px  | Card width: 340px (fixed), centered                 |
| > 768px        | No change (badge is inherently mobile-sized)        |

---

## 6. Accessibility Requirements

1. **Color Contrast**: All text meets WCAG AA (4.5:1 minimum)
2. **Touch Targets**: Interactive elements ≥44px
3. **Focus States**: Add visible focus ring on interactive elements (2px solid accent, 2px offset)
4. **Reduced Motion**: Respect `prefers-reduced-motion` — disable animations
5. **Screen Reader**: Provide semantic structure and aria-labels for icons

---

## 7. Implementation Checklist

- [ ] Set up color tokens/variables
- [ ] Configure typography with font imports
- [ ] Implement shadow utility (raised/inset × size variants)
- [ ] Build container with gradient overlay
- [ ] Create lanyard hole with nested circles
- [ ] Build event badge with dot animation
- [ ] Implement avatar section layout
- [ ] Add inset divider
- [ ] Create detail item component (reusable)
- [ ] Implement QR code with grid pattern
- [ ] Build access badge with hover state
- [ ] Add entry animations with stagger
- [ ] Add ambient animations (pulse, blink)
- [ ] Test hover transitions
- [ ] Verify responsive behavior
- [ ] Test accessibility

---

*Specification Version: 1.0*  
*Generated: 2025*
