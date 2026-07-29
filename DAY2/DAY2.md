# DAY 2 — Capgemini Refresher Coding Practice

**Topic:** Static Methods, Parameters, Overloading, Default/Named Arguments, ref/out/in/params, Local Functions, Recursion, TryParse

---

## Medium Questions

| ID  | Project Folder          | Scenario                                      | Status  |
|-----|-------------------------|-----------------------------------------------|---------|
| M1  | M1_FinancialCalculator  | Compound Interest Calculator                  | Done    |
| M2  | M2_LibraryISBN          | Library ISBN Order Processor                  | Done    |
| M3  | M3_LogParser            | Log File Line Parser                          | Done    |
| M4  | M4_GeometryLibrary      | Geometry Area Calculator                      | Done    |
| M5  | M5_MathOperations       | Add and Multiply with params                  | Done    |

---

## Hard Questions

| ID  | Project Folder            | Scenario                                      | Status  |
|-----|---------------------------|-----------------------------------------------|---------|
| H1  | H1_ConfigurationLoader    | Config Loader with TryLoad Fallback           | Done    |
| H2  | H2_TreeFlattener          | Flatten a Tree using Recursion + Local Func   | Done    |
| H3  | H3_LogFormatter           | Log Message Formatter with params + TryParse  | Done    |
| H4  | H4_FactorialRecursion     | Factorial, Fibonacci, Sum of Digits           | Done    |
| H5  | H5_StudentGradeSystem     | Student Grade with Overloading + out + params | Done    |

---

## Key Concepts Covered

- **Static methods and static classes**
- **Method overloading** — same name, different parameters
- **Default parameter values** — caller can omit them
- **Named arguments** — pass by name for readability
- **ref** — caller's variable is modified inside the method
- **out** — method sends a value back through a parameter
- **in** — read-only reference, prevents copying large structs
- **params** — accept variable number of arguments
- **Local functions** — helper functions defined inside a method
- **Recursion** — method calls itself with a base case to stop
- **TryParse pattern** — returns bool, result in out, no exceptions

---

## Sample Outputs

### M1 — Compound Interest
```
CalculateCompoundInterest(10000, 0.05, 10)           -> $16,288.95  (annual)
CalculateCompoundInterest(principal:10000, rate:0.05, time:10, compoundingFrequency:12) -> $16,470.09  (monthly)
```

### M2 — ISBN Processor
```
"978-3-16-148410-0"  -> Valid   -> 9783161484100
"invalid-isbn"       -> Invalid -> skipped
"978-1-4028-9462-6"  -> Valid   -> 9781402894626
```

### M3 — Log Parser
```
"2023-10-27 14:30:00 ERROR: Disk full"
  Timestamp : 2023-10-27 14:30:00
  LogLevel  : Error
  Counter   : 1
```

### M4 — Geometry
```
CalculateArea(5)                     -> Circle    78.54  (default 2 dp)
CalculateArea(4, 6)                  -> Rectangle 24
CalculateArea(3, 7, isTriangle:true) -> Triangle  10.5
CalculateArea(radius:5, decimals:4)  -> Circle    78.5398
```

### M5 — Math Operations
```
Add(5, 10)       = 15
Add(1,2,3,4,5)   = 15
Multiply(2, 3)   = 6
Multiply(2,3,4,5)= 120
```

### H1 — Config Loader
```
Trying EnvironmentVariables... no data found. Skipping.
Trying JsonFile ('app.json')... file not found. Skipping.
Trying Defaults... loaded successfully.
Source: Defaults
db_host = localhost
db_port = 5432
```

### H2 — Tree Flattener
```
Flattened: A, A1, A2, B, B1, B1a, B1b, C
```

### H3 — Log Formatter
```
User JohnDoe logged in from 192.168.1.1 at 2026-07-31 10:00:00
Order #42 placed by Alice for 3 items
```

### H4 — Recursion
```
5! = 120
F(7) = 13
SumOfDigits(123) = 6  (recursive calls: 3)
```

### H5 — Grade System
```
Average: 85.00  Grade: B  Result: Pass
Average: 40.00  Grade: F  Result: Fail
```
