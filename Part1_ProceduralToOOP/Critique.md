# Design Critique: Procedural C++ Order System

This document reviews the architectural and design flaws in the procedural C++ implementation (`order_system.cpp`). The program functions as a monolithic script with free functions manipulating global data structures.

---

## 1. Global Mutable State and Uncontrolled Access

### The Issue
Every core piece of data in the system—customer records, product catalog, orders, and order lines—lives in globally scoped variables (`customerIds`, `productStock`, `orderIsPaid`, etc.). Any function in the codebase can read and mutate any of these arrays at any point in execution.

### Why It Is a Problem & Risks
* **No Single Source of Truth for Invariants:** When state is global, there is no boundary enforcing validity. For instance, `productStock[i]` can be decremented or set to a negative number by any function without going through validation logic.
* **Hidden Dependencies:** Functions don't declare their inputs and outputs explicitly through parameters and return values; they implicitly read and write global variables. This creates invisible coupling across unrelated routines, making the program extremely fragile to change.
* **Concurrency and Scalability Blockers:** Global mutable state makes it impossible to run operations concurrently or instantiate multiple stores/tenants within the same process.

---

## 2. The Parallel Arrays Anti-Pattern

### The Issue
Instead of grouping related attributes into structured types (`struct` or `class`), entities are fragmented across separate, flat arrays indexed by a shared integer:
* Customer: `customerIds[]`, `customerNames[]`, `customerEmails[]`, `customerCities[]`, `customerIsVip[]`
* Order Lines: a 2D matrix of raw integers `lineProductIndexes[][]` and `lineQuantities[][]`

### Why It Is a Problem & Risks
* **High Risk of Desynchronization:** An entity only exists implicitly through matching index positions. If an insertion, deletion, or sorting operation shifts an element in one array but fails in another, the entire dataset becomes silently corrupted (e.g., Customer #2 ends up with Customer #3's email address).
* **Code Duplication & Maintenance Burden:** Adding a single property (like `shippingAddress`) requires declaring a new global array, updating `MAX_CUSTOMERS`, modifying every function signature that touches customers, and updating all search and print loops.

---

## 3. Arbitrary Fixed Capacities

### The Issue
The program relies on compile-time fixed constants:
* `MAX_CUSTOMERS = 50`
* `MAX_PRODUCTS = 50`
* `MAX_ORDERS = 100`
* `MAX_LINES_PER_ORDER = 20`

### Why It Is a Problem & Risks
* **Inflexible Resource Allocation:** Fixed arrays waste memory if the system only processes a few items, while immediately failing with runtime errors (`ERROR: customer list is full`) if the system exceeds 50 customers or 20 order lines.
* **Poor Real-World Fit:** Business systems require dynamic collections (`List<T>`, `std::vector`) that allocate memory on demand.

---

## 4. Lack of Domain Encapsulation and Anemic Logic

### The Issue
Business logic is scattered across free procedural functions rather than encapsulated within the entities that own the data:
* Stock deduction happens directly inside `addLineToOrder`: `productStock[productIndex] -= quantity;`
* VIP discounts are hardcoded inside `calculateOrderTotal`: `if (customerIsVip[customerIndex]) total *= 0.90;`
* Order state verification is split between `addLineToOrder` and `markOrderPaid`.

### Why It Is a Problem & Risks
* **Violations of Domain Invariants:** A product should be solely responsible for managing its inventory (verifying stock availability and decrementing). In the procedural code, an order function directly mutates product stock. If an order line fails or gets removed later, there is no stock rollback mechanism, resulting in permanent inventory leaks.
* **Rigid Business Rules:** If VIP discount rules change (e.g., tiered discounts or minimum spend thresholds), changes must be made inside an order calculation function rather than within customer or pricing policy abstractions.

---

## 5. Weak Identifiers and Index-Based Coupling

### The Issue
Orders store `orderCustomerIndexes[orderCount] = customerIndex;` instead of maintaining durable domain references or foreign keys.

### Why It Is a Problem & Risks
* The system conflates an entity's in-memory array index with its identity. If the customer list is ever sorted or filtered, existing orders would immediately point to the wrong customers.
* Lookups require linear scans (`findCustomerIndexById`) through flat arrays repeatedly.

---

## Summary

The procedural version functions strictly as a proof-of-concept script. In the C# redesign, these problems are resolved by:
1. Encapsulating domain concepts into coherent classes (`Customer`, `Product`, `Order`, `OrderLine`).
2. Eliminating all global variables and managing lifecycle through an `OrderSystem` class.
3. Using dynamic collections (`List<T>`) instead of fixed-size arrays.
4. Making entities responsible for protecting their internal state (e.g., `Product.DeductStock`, `Order.AddLine`, `Order.MarkPaid`).
