This is a project I am currently working on in my free time (unfortunately, I don't have much of it). 

CloStyle is a web application built with ASP.NET Core MVC, designed to manage clothing brands and their associated products. The project is structured using the CQRS (Command Query Responsibility Segregation) pattern.


Technologies and Tools

Backend: ASP.NET Core MVC, C#, Entity Framework Core, SQL Server
Architecture: CQRS with MediatR, Repository Pattern, Dependency Injection
Frontend: Razor Pages, Bootstrap 5, JavaScript
Utilities: AutoMapper for object-object mapping


Architecture Overview

The project follows the principles of Clean Architecture, segregating the application into distinct layers:
Domain Layer: Contains core business logic and domain entities.
Application Layer: Houses application-specific logic, including CQRS handlers and service interfaces.
Presentation Layer: Manages UI concerns using Razor Pages and MVC controllers.


Communication between layers is facilitated through the MediatR library, promoting a decoupled and maintainable codebase.