# Demo.Customers
This is a demo api site demostrating the use of modern design architecture and techniques.

# Technologies used:
1.  .Net Core 5.0 Preview 6
1.  Entitity Framework - For business logic domain model
1.  Dapper - For database queries
1.  DataRepositoryCore - For Entity Frmaework Data Repositories
1.  MediatR - For implementation of Mediator pattern
1.  AutoMapper - For mapping dto's across layers
1.  FluentValidation - For validation classes
1.  Moq - For unit testing

# Design
Appliaction is separated into three areas:
1.  Application - Conatains all the domain business logic
1.  Infrastructure - Peristence layer 
1.  Hosts - For hosting the Application layer, a .Net Core api website
