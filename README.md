# Report 

## Statistics computed inside the controller
Principle: Single Responsibility  
Location: `Controllers/ItemController.cs`
Why: The controller calculated `TotalCount` and `AverageValue`
Fix: Extracted into `GradeStatisticsService`

## Controller depending on the repository
Principle: Dependency Inversion  
Location: `Controllers/ItemController.cs`
Why: The controller bypassed the service layer and called data access directly.  
Fix: `GradeController` depends on `IGradeService` for business logic.

## `ItemRepository` implements only read interface 

Principle: Interface Segregation
Location: `Repositories/ItemRepository.cs`
Why: The class held `_items` and `_nextId` but only implemented `IItemReader`. It was set up to support writes but exposed no contract for them.
Fix: Implement a write interface (`IGradeWriter`) alongside the read one.

## `virtual` methods on `ItemRepository` with no actual subclass

Principle: Open/Closed
Location: `Repositories/ItemRepository.cs`
Why: Marking methods `virtual` without a concrete extension scenario pre-emptively opens the class for modification in an uncontrolled way.
Fix: Remove `virtual` unless a subclass with a defined override reason exists.

## `Console.WriteLine` instead of injected logger
Principle: Single Responsibility + Dependency Inversion  
Location: `Controllers/ItemController.cs`
Why: Hardcodes a concrete I/O mechanism inside the controller instead of depending on an abstraction.  
Fix: `GradeController` injects `ILogger<GradeController>` and uses `_logger.LogInformation()`.