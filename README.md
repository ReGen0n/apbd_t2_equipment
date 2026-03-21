
# University Equipment Rental

## Project description
This is a C# console application for managing a university equipment rental service.  
It supports adding users, adding equipment, renting and returning equipment, checking availability, viewing overdue rentals, and generating a summary report.

## How to run
1. Open the solution in JetBrains Rider or Visual Studio.
2. Build the project.
3. Run the program from `Program.cs`.

## Project structure
- `Domain` contains the core domain classes:
    - `Equipment`, `Laptop`, `Projector`, `Camera`
    - `User`, `Student`, `Employee`
    - `Rental`
- `Services` contains `RentalService`, which handles business logic.
- `Program.cs` contains only the demonstration scenario.

## Design decisions
The project separates the domain model from the execution logic.  
`RentalService` contains rental rules and reporting logic, so business logic is not placed in `Program.cs`.

This improves cohesion because:
- domain classes store data related to one concept,
- `RentalService` handles rental operations,
- `Program.cs` only starts and demonstrates the application.

This reduces coupling because:
- domain objects do not control the whole application flow,
- the service works with domain objects through a clear interface.

Inheritance is used where it matches the domain:
- `Laptop`, `Camera`, and `Projector` inherit from `Equipment`,
- `Student` and `Employee` inherit from `User`.

Rental limits are easy to modify because they are defined in user types.
Penalty logic is centralized in `RentalService`.