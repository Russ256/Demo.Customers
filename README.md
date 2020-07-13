# Demo.Customers
This is a simple project to demostrate a modern scaleable web api design implemented in .Net Core using standard patterns and practices.

# Technologies used
1.  .Net Core 3.1
1.  Entitity Framework - For business logic domain model
1.  Dapper - For query requests
1.  DataRepositoryCore - For Entity Frmaework data repositories
1.  MediatR - For implementation of Mediator pattern
1.  AutoMapper - For mapping dto's across layers
1.  FluentValidation - For validation classes
1.  Moq - For unit testing
1.  Swashbuckle/Swagger - For generation of api documentation

# Techniques used
1.  Data Repository pattern for data access.
1.  Unit Of Work pattern for ensuring transaction integrity.
1.  Mediator pattern used to decouple the public interface from the application implementation.
1.  Dependency Injection/IOC patterns used for all application objects.
1.  Validator classes used for request validation.

# Design
Application is separated into three areas:
1.  Application - Conatains all the domain business logic
1.  Infrastructure - Peristence layer 
1.  Hosts - For hosting the Application layer, a .Net Core api website

# Application Layor
1.  App business logic is implemented in the application layor
1.  All calls to application logic are made through MediatR calls 
1.  CQRS architecture applied, Entity framework used for handling domain updates and Dapper for domain queries

# Host Api
1.  The application is hosted in a .Net Core api project.
1.  Calls to application logic are made through MediatR requests.
1.  AutoMapper is used to map repsonses from the application calls to publid Dto's exposed by the api.
1.  Swagger is implemented to create an api site

# Infrastructure 
1.  Sql database project 
1.  DataRepositoryCore used for implementing generic data repository parttern in Entity Framework
1.  Simple Unit Of Work implementation
