using SrpLab.Appointments;
using SrpLab.Billing;
using SrpLab.Checkout;
using SrpLab.Enrollment;
using SrpLab.Grades;
using SrpLab.Kitchen;
using SrpLab.Loan;
using SrpLab.Support;
using SrpLab.Ward;
using SrpLab.Warehouse;

Console.WriteLine("SrpLab - Refactored Classes");
Console.WriteLine("===========================");

// 1. Ward
var acuityScorer = new AcuityScorer();
var wardCensus = new WardCensus();
var wardPager = new WardPager();
var handoffFormatter = new HandoffNoteFormatter();

int bed1Acuity = acuityScorer.Score(heartRate: 130, spo2: 89);
var bed1 = wardCensus.AssignBed(1, "p-88", bed1Acuity);
wardPager.Evaluate(bed1.BedNumber, bed1.AcuityScore);
Console.WriteLine(handoffFormatter.Format(1, wardCensus.GetAssignment(1)));
Console.WriteLine(string.Join(" | ", wardPager.DrainLog()));

// 2. Checkout
var basket = new ShoppingBasket();
basket.AddLine("SKU-1", 40m, 2);
basket.EnableGiftWrap();
var discountCalc = new CouponDiscountCalculator();
var totalCalc = new CheckoutTotalCalculator();
var paymentAuth = new PaymentAuthorizer();

decimal discount = discountCalc.CalculateDiscount("SAVE10", basket.SubTotal());
decimal grandTotal = totalCalc.CalculateGrandTotal(basket, discount);
Console.WriteLine($"basket total={grandTotal} auth={paymentAuth.Authorize(grandTotal, "4242", basket.Items.Count)}");

// 3. Support
var classifier = new TicketPriorityClassifier();
var slaPolicy = new SlaPolicy();
var replyComposer = new PublicReplyComposer();

string ticketSubject = "cannot login";
string ticketBody = "prod is down for me";
var ticket = new SupportTicket(
    "T-1",
    ticketSubject,
    ticketBody,
    DateTimeOffset.UtcNow,
    classifier.Classify(ticketSubject, ticketBody));
Console.WriteLine(replyComposer.Draft(ticket, "Nora", slaPolicy.GetDeadline(ticket.OpenedAt, ticket.Priority)));

// 4. Loan
var loanApp = new LoanApplication(60_000m, 640, 4, hasCollateral: false);
var riskEval = new LoanRiskEvaluator();
var docChecklist = new LoanDocumentChecklist();
var letterFormatter = new LoanDecisionLetterFormatter();

decimal riskScore = riskEval.CalculateRiskScore(loanApp);
bool isEligible = riskEval.IsEligible(loanApp);
var docs = docChecklist.GetRequiredDocuments(loanApp, isEligible);
Console.WriteLine(letterFormatter.Format("Omar", loanApp, riskScore, isEligible, docs));

// 5. Enrollment
var roster = new CourseRoster("SEF-101", capacity: 1, tuition: 3000m);
var welcomeFormatter = new WelcomePacketFormatter();
Console.WriteLine(roster.Register("a@mail.com"));
Console.WriteLine(roster.Register("b@mail.com"));
Console.WriteLine(welcomeFormatter.FormatMarkdown(roster, "b@mail.com", "Bea"));

// 6. Kitchen
var order = new KitchenOrder();
order.AddItem("Pasta", new[] { "wheat", "milk" }, 12);
var allergenScanner = new AllergenScanner();
var prepEstimator = new PrepTimeEstimator();
var ticketFormatter = new ThermalTicketFormatter();

var allergens = allergenScanner.DetectAllergens(order);
int eta = prepEstimator.EstimateReadyMinutes(order, openStations: 2, allergens.Count > 0);
Console.WriteLine(ticketFormatter.Format(42, order, allergens, eta));

// 7. Billing
var sub = new Subscription("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));
sub.RegisterFailedPayment();
var prorationCalc = new ProrationCalculator();
var invoiceGen = new InvoiceNumberGenerator();
var dunningComposer = new DunningEmailComposer();

decimal amountDue = prorationCalc.Calculate(sub, sub.PeriodStart);
string invoiceNo = invoiceGen.Next(sub.PeriodStart);
Console.WriteLine(dunningComposer.Compose(sub, "Sara", new DateOnly(2026, 9, 20), amountDue, invoiceNo));

// 8. Warehouse
var pickRequest = new PickRequest();
pickRequest.AddNeed("BOLT", "A", 3, 10, 7);
pickRequest.AddNeed("NUT", "B", 1, 5, 5);
var allocator = new StockAllocator();
var routePlanner = new PickRoutePlanner();
var scriptFormatter = new PickerScriptFormatter();

var allocations = allocator.Allocate(pickRequest);
var steps = routePlanner.PlanWalkingOrder(pickRequest);
Console.WriteLine(scriptFormatter.Format(steps, allocations));

// 9. Grades
var scoreBook = new ScoreBook();
scoreBook.Record("s1", 92);
scoreBook.Record("s1", 88);
var gradingPolicy = new GradingPolicy();
var transcriptFormatter = new TranscriptFormatter();

decimal avg = scoreBook.Average("s1");
Console.WriteLine(transcriptFormatter.FormatPlain("s1", "Ali", avg, gradingPolicy.GetLetterGrade(avg), gradingPolicy.MeetsHonorRoll(avg)));

// 10. Appointments
var hoursPolicy = new ClinicHoursPolicy(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
var scheduler = new AppointmentScheduler(hoursPolicy);
var smsFormatter = new SmsReminderFormatter();

var slot = scheduler.FindNextSlot(DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48);
if (slot is null) throw new InvalidOperationException("no slot");
scheduler.TryBook(slot.Value);
Console.WriteLine(smsFormatter.Format(slot.Value, "0100"));

Console.WriteLine("Done.");
