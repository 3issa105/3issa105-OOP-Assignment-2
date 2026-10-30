# OOP Design, Encapsulation & The Builder Pattern
**Simulation Academy • .NET Diploma • Cycle 1**

- **Student Name:** Ahmed Eissa
- **GitHub:** [@3issa105](https://github.com/3issa105)
- **Track:** Backend .NET
- **Assignment:** OOP Assignment 2 (Branch: `assignment/1-6`)

---

## Repository Structure

```text
(repository root)
├── README.md
├── Part1_ProceduralToOOP/
│   ├── Critique.md
│   └── src/
│       ├── Part1_ProceduralToOOP.csproj
│       ├── Customer.cs
│       ├── Product.cs
│       ├── OrderLine.cs
│       ├── Order.cs
│       ├── OrderSystem.cs
│       └── Program.cs
├── Part2_HotelReservationSystem/
│   └── src/
│       ├── Part2_HotelReservationSystem.csproj
│       ├── Guest.cs
│       ├── Room.cs
│       ├── Reservation.cs
│       ├── Enums.cs
│       └── Program.cs
├── Part3_BuilderPattern/
│   ├── Answers.md
│   └── src/
│       ├── Part3_BuilderPattern.csproj
│       ├── Model/
│       │   ├── Address.cs
│       │   ├── OrderDetails.cs
│       │   └── Invoice.cs
│       ├── Builders/
│       │   ├── AddressBuilder.cs
│       │   ├── OrderBuilder.cs
│       │   ├── SimpleInvoiceBuilder.cs
│       │   └── ComposedInvoiceBuilder.cs
│       └── Program.cs
└── Part4_LeetCode/
    └── 1679_MaxNumberOfKSumPairs/
        ├── Solution.cs
        └── accepted_screenshot.png
```

---

## Summary of Parts

1. **Part 1 — Procedural C++ to Object-Oriented C#**:
   - `Critique.md`: In-depth analysis of the procedural code design flaws (global state, parallel arrays, lack of encapsulation, hard limits).
   - `src/`: Complete refactored C# OOP solution preserving all original functionality without any global state.

2. **Part 2 — Hotel Reservation System**:
   - Strict encapsulation modeling `Guest`, `Room`, and `Reservation`.
   - Invariant enforcement: read-only collections (`IReadOnlyList<Reservation>`), protected state transitions (`Pending` -> `Confirmed` -> `CheckedIn` -> `CheckedOut`), validation on pricing and date ranges, and overbooking prevention.

3. **Part 3 — Builder Pattern & Constructor Complexity**:
   - `Answers.md`: Analysis of long-parameter constructors, primitive obsession, and the architectural advantages of composed builders.
   - `src/`: Implementation of both a monolithic Fluent Builder and modular, reusable Composed Builders (`AddressBuilder`, `OrderBuilder`).

4. **Part 4 — LeetCode (1679. Max Number of K-Sum Pairs)**:
   - Solution using the **Two Pointers** technique with $O(N \log N)$ time and $O(1)$ extra space.
