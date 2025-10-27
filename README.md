# Gate Assignment Kata — Problem Definition

A small **library/service** that, given a **Flight** and a set of **Gates**, returns an **Assignment** (or none) according to the rules below.  
Include unit tests demonstrating each requirement with at least one passing and one failing example.

---

## Domain Model (suggested)

**Flight**
- `id`
- `timeWindow` (start/end) — represented by an `Interval` type `[start, end)`
- `aircraftType`: `NARROW` / `WIDE`
- `originType`: `DOMESTIC` / `INTERNATIONAL`
- `requiresJetway`: `bool`

**Gate**
- `id`
- `supportsWidebody`: `bool`
- `isDomestic`: `bool`
- `hasJetway`: `bool`
- `availabilityWindows`: `List<Interval>` (each `[start, end)`)

**Assignment**
- `flightId`
- `gateId`
- `timeWindow` — identical to the flight’s time window

> You may extend the model, but keep each rule testable in isolation.  
> Time is represented as a half-open interval `[start, end)` — start inclusive, end exclusive.  
> Adjacent windows such as `[01–02)` and `[02–03)` are non-overlapping.

---

## Core Specifications (Hard Rules)

### 1️⃣ Time Window

**Rule:**  
The selected gate must have at least one availability window that fully covers the flight’s time window.  
Intervals use **[start, end)** semantics — start inclusive, end exclusive.

- ✅ Example (pass): Gate A1 available 00–24; Flight 01–02  
- ❌ Example (fail): Gate A1 available 01–01:30; Flight 01–02

### 2️⃣ Aircraft Size

**Rule:**  
WIDE aircraft require `supportsWidebody = true`.  
NARROW aircraft may use any gate.

- ✅ Pass: WIDE → gate with `supportsWidebody = true`  
- ❌ Fail: WIDE → narrow-only gate

### 3️⃣ Zone / Border Control

**Rule (simplified):**  
DOMESTIC flights must use domestic gates.  
INTERNATIONAL flights must use non-domestic gates (`isDomestic = false`).

- ✅ Pass: DOMESTIC → domestic gate  
- ❌ Fail: INTERNATIONAL → domestic gate

### 4️⃣ Jetway Requirement

**Rule:**  
If `requiresJetway = true`, the gate must have `hasJetway = true`.  
If `requiresJetway = false`, either gate type is acceptable (having a jetway is allowed even if not required).

- ✅ Pass: `requiresJetway = true` → gate with jetway  
- ❌ Fail: `requiresJetway = true` → gate without jetway

**All hard rules must be satisfied together:**

```
Time Window AND Size AND Zone AND Jetway
```

---

## Composition Examples (OR / NOT)

Use these examples to demonstrate incremental rule evolution and logical operator semantics.

### OR Example — Precleared International Flights

**Policy:**  
International flights that have completed preclearance (e.g., U.S. CBP Preclearance) may use domestic gates.

**Spec:**  
```
Time Window AND Size AND Jetway AND (Zone OR flight.isPrecleared)
```

- ✅ Pass: INTERNATIONAL **precleared** flight → domestic gate  
- ❌ Fail: INTERNATIONAL **not precleared** flight → domestic gate

This simulates airports that can treat some international arrivals/departures as domestic due to preclearance agreements.

### NOT Example — Exclude a Subset of Gates

**Policy:**  
Temporarily block gates whose `id` starts with `"B"` (e.g., under maintenance).

**Spec:**  
```
Time Window AND Size AND Zone AND Jetway AND NOT(GateIdStartsWithB)
```

- ✅ Pass: A1 passes; B1 filtered out → choose A1  
- ❌ Fail: Only gates B1/B2 available → no assignment returned

If no gates satisfy the rule, the service should return **no assignment**  
(e.g., `null`, `Optional.empty()`, or equivalent).

Include unit tests for both examples so participants can see concrete OR and NOT compositions.

---

## Strategy (Tie-Breaking) — Optional but Recommended

When multiple gates satisfy the hard specification, the system must apply a deterministic tie-breaking strategy:

- **GreedyFirstFit (default):** choose the first matching gate in deterministic order (e.g., by `id`).
- **MostAvailable (stretch):** choose the gate with the largest total remaining availability (sum of its availability windows).  
  Remaining availability is based solely on the gate’s current `availabilityWindows`, not past assignments.  
  If two gates tie, fall back to deterministic order (e.g., by `id`).
- **ClosestTerminal (stretch):** choose based on a numeric proximity metric.

**Acceptance:**  
Swapping strategies must not require changes to rule implementations.
