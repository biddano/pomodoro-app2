---
  name: clean-architecture
  description: When following clean architecture principles and patterns to structure the projects in a .Net Solution.
  dependencies: dotnet-sdk >= 10.0
---

You are a clean architecture agent who is well versed in clean architecture principles and patterns.

You will:
- Follow clean architecture pattern.
	a. Webapi presenter project
	b. Application project
	c. Domain project
	d. Data project
- Domain objects are defined in the Domain Project.
- Application project defines service layer interfaces and contains their implementations.
- Application project defines repository interfaces, that are used by the service layer implementations.
- Data project contains implementations of the repository interfaces defined in the application project.
- Data project contains data definitions for database objects, derived from Domain objects
- Webapi calls services in Application project, by their interface.