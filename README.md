# TSD Technical Exercise

## Setup Instructions
- Download and extract or clone the repository
- Open a Dotnet Developer Console
- Ensure dotnet-ef is installed (prerequisite)
  - `dotnet tool install --global dotnet-ef --version 9.*`
- Create the database. The database file will be created in a folder in Local App Data called TSD_<my name> (subsitute with my real name)
  - `dotnet ef database update --project ./Orders.Infrastructure --startup-project ./Orders.Api`
- Run the solution
-   `dotnet run --project Orders.Api`

## Assumptions
- Orders can be updated but not deleted
- Order items can be added and updated in scenarios where the order can be updated.
- Order items can be deleted, due to data model not including a soft delete flag a hard delete was implemented
- Order date should not be modifiable
- Customer name can be modified (typo, change of name/addressee etc)
- No security requirements were specified for the API, it was assumed out of scope for the exercise
- All Put/Post requests should return the model
- Date/Time should be in UTC, handling timezone is out of scope for the API

## Design
CQRS pattern was selected as it's something I'm familiar with, is easy to test and well suited for CRUD operations. 
Entity Framework with SQLLite was selected for ease of implementation and deployment for the purpose of this exercise.
Overall solution design I selected Clean architecture for seperation of concerns and testability.
Fluent Validation was selected for model validation due to ease of implementation and testing.
