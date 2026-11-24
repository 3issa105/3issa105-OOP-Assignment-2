# Task 1.1 - Identifying SRP Responsibilities

This document breaks down the 10 classes in `SrpLab`, listing the separate responsibilities found in each class and why keeping them in a single class creates maintenance issues.

---

## 1. WardBoard

**Responsibilities found:**
1. Storing and updating bed-to-patient assignments and acuity scores (`AssignBed`).
2. Calculating clinical acuity score from vital signs (`ScoreAcuity`).
3. Deciding when to trigger a pager alert and buffering/draining pager alerts (`AssignBed` side effect + `DrainPagerLog`).
4. Formatting the nurse handoff summary string (`BuildHandoffNote`).
5. Exporting the ward census as a CSV string (`ExportCensusCsv`).

**Why keeping them together is a problem:**
- Calling `AssignBed` silently pushes alerts into `_pagerLog`, mixing state updates with alerting rules.
- If the clinical team changes the heart rate/SpO2 scoring formula, or nursing changes the handoff note layout, or reporting changes the CSV columns, they all have to edit the exact same file.

**Refactored types:** `BedAssignment`, `WardCensus`, `AcuityScorer`, `WardPager`, `HandoffNoteFormatter`, `WardCensusCsvExporter`.

---

## 2. CheckoutBasket

**Responsibilities found:**
1. Managing cart line items and gift-wrap selection (`AddLine`, `EnableGiftWrap`, `SubTotal`).
2. Parsing marketing coupon strings and computing discount rules (`DiscountAmount`).
3. Calculating the final grand total including packaging fees (`GrandTotal`).
4. Formatting customer-facing gift message cards (`GiftMessageCard`).
5. Building a payment authorization token stub (`AuthorizePaymentStub`).

**Why keeping them together is a problem:**
- Marketing promotions (`SAVE10`, `WELCOME10`, `FREESHIP`) change frequently and are coupled to cart item storage and payment gateway logic.
- Updating gift card copy or changing the gift-wrap fee forces changes to the same class that builds payment payloads.

**Refactored types:** `BasketItem`, `ShoppingBasket`, `CouponDiscountCalculator`, `CheckoutTotalCalculator`, `GiftCardFormatter`, `PaymentAuthorizer`.

---

## 3. SupportTicket

**Responsibilities found:**
1. Holding ticket state (`Id`, `Subject`, `Body`, `OpenedAt`, `Priority`) and appending customer messages (`AppendCustomerMessage`).
2. Classifying ticket priority using keyword matching (`RecalculatePriorityFromText`).
3. Computing SLA deadlines and checking breaches (`SlaDeadline`, `IsBreached`).
4. Drafting public customer reply text (`DraftPublicReply`).
5. Formatting internal escalation strings (`InternalEscalationBlurb`).

**Why keeping them together is a problem:**
- The constructor and `AppendCustomerMessage` silently run `RecalculatePriorityFromText`, hiding keyword classification inside simple data operations.
- Support playbooks (keywords), operations policy (SLA hours), and customer messaging templates all belong to different teams, yet all force edits to `SupportTicket`.

**Refactored types:** `SupportTicket`, `TicketPriorityClassifier`, `SlaPolicy`, `PublicReplyComposer`, `EscalationNoticeFormatter`.

---

## 4. LoanDesk

**Responsibilities found:**
1. Holding loan application inputs (`RequestedAmount`, `CreditScore`, `EmploymentMonths`, `HasCollateral`).
2. Calculating risk scores and determining loan eligibility (`RiskScore`, `IsEligible`).
3. Determining required compliance documents (`RequiredDocuments`).
4. Generating applicant decision letters (`DecisionLetter`).
5. Exporting underwriting data as CSV rows (`UnderwriterCsvRow`).

**Why keeping them together is a problem:**
- Risk formulas change with the credit committee, document lists change with regulations, letter wording changes with legal/communications, and CSV columns change with analytics.
- Changing any one of those four areas touches the same class and risks breaking unrelated logic.

**Refactored types:** `LoanApplication`, `LoanRiskEvaluator`, `LoanDocumentChecklist`, `LoanDecisionLetterFormatter`, `UnderwriterCsvExporter`.

---

## 5. CourseEnrollmentDesk

**Responsibilities found:**
1. Managing course seats, waitlist tracking, and seat promotion (`Register`, `WaitlistPosition`, `PromoteFromWaitlist`).
2. Generating student welcome packet Markdown (`WelcomePacketMarkdown`).
3. Formatting tuition and VAT invoice lines (`TuitionInvoiceLine`).

**Why keeping them together is a problem:**
- Marketing changes to the welcome message (links, wording) and finance changes to tax rates (14% VAT) or CSV structure live inside the same class that handles enrollment capacity and waitlist order.

**Refactored types:** `CourseRoster`, `WelcomePacketFormatter`, `TuitionInvoiceFormatter`.

---

## 6. KitchenTicket

**Responsibilities found:**
1. Storing order items with ingredients and prep times (`AddItem`).
2. Scanning ingredient names against allergen categories (`DetectAllergens`).
3. Estimating kitchen ready time based on stations and allergen delays (`EstimatedReadyMinutes`).
4. Choosing the expo routing lane (`ExpoLaneHint`).
5. Rendering thermal printer layout (`RenderThermalTicket`).

**Why keeping them together is a problem:**
- `RenderThermalTicket` and `ExpoLaneHint` hardcode `2` open stations and depend directly on printer width and separator styling.
- Updating allergen regulations, kitchen station capacity logic, or thermal printer hardware formatting all require modifying `KitchenTicket`.

**Refactored types:** `KitchenOrderItem`, `KitchenOrder`, `AllergenScanner`, `PrepTimeEstimator`, `ExpoLaneRouter`, `ThermalTicketFormatter`.

---

## 7. SubscriptionBilling

**Responsibilities found:**
1. Storing subscription period details and tracking failed payments (`RegisterFailedPayment`).
2. Computing prorated billing amounts (`Prorate`).
3. Minting sequential invoice numbers using shared mutable static state (`NextInvoiceNumber`).
4. Composing dunning reminder emails (`DunningEmail`).
5. Formatting accounting ledger journal lines (`LedgerJournalLine`).

**Why keeping them together is a problem:**
- Both `DunningEmail` and `LedgerJournalLine` call `NextInvoiceNumber()`, which silently increments the global `_invoiceSeq` counter as a hidden side effect just from formatting text.
- Billing calendar math, invoice numbering rules, collections tone, and accounting export format are four separate concerns.

**Refactored types:** `Subscription`, `ProrationCalculator`, `InvoiceNumberGenerator`, `DunningEmailComposer`, `LedgerJournalFormatter`.

---

## 8. WarehousePickList

**Responsibilities found:**
1. Recording needed SKUs, bin locations, and stock on hand (`AddNeed`).
2. Allocating available stock against needed quantities (`Allocate`).
3. Sorting items into warehouse walking order (`WalkingOrder`).
4. Formatting handheld picker instructions with shortage warnings (`PickerScript`).
5. Generating WMS XML batch payloads (`WmsXmlBatch`).

**Why keeping them together is a problem:**
- Stock allocation rules, aisle/bin walking heuristics, handheld UI text, and XML integration schemas change for completely different reasons (inventory policy vs. warehouse layout vs. handheld UX vs. external WMS contract).

**Refactored types:** `PickRequestLine`, `PickAllocation`, `PickStep`, `PickRequest`, `StockAllocator`, `PickRoutePlanner`, `PickerScriptFormatter`, `WmsXmlExporter`.

---

## 9. GradeBook

**Responsibilities found:**
1. Recording student scores and calculating numeric averages (`Record`, `Average`).
2. Applying academic letter-grade bands and honor roll rules (`Letter`, `MeetsHonorRoll`).
3. Formatting plain-text student transcripts (`TranscriptPlain`).
4. Exporting all student grades to CSV (`ExportCsv`).

**Why keeping them together is a problem:**
- If faculty changes the letter grade cutoffs or honor roll threshold, or the registrar changes transcript layout or CSV column headers, all changes hit the same class that stores raw scores.

**Refactored types:** `ScoreBook`, `GradingPolicy`, `TranscriptFormatter`, `GradeCsvExporter`.

---

## 10. AppointmentDesk

**Responsibilities found:**
1. Enforcing clinic working days and opening hours (`IsWithinBusinessHours`).
2. Searching for open slots and booking them (`FindNextSlot`, `TryBook`).
3. Generating iCalendar (`.ics`) event text (`ToIcs`).
4. Formatting SMS reminder messages (`SmsReminder`).

**Why keeping them together is a problem:**
- Clinic schedule rules (holidays/working hours), booking state, ICS calendar serialization, and SMS messaging copy have no reason to live in one type. Changing SMS text or adding a field to the ICS output should not risk touching booking logic.

**Refactored types:** `ClinicHoursPolicy`, `AppointmentScheduler`, `IcsEventFormatter`, `SmsReminderFormatter`.
