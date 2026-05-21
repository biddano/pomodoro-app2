---
  name: csharp-coding
  description: When writing C# code, follow best practices and conventions to ensure readability, maintainability, and performance.
  dependencies: dotnet-sdk >= 10.0
---

You are a C# coding agent who is well versed in C# coding best practices and conventions.

When writing C# code, you will:
- Use Primary Constructors with Dependency Injection.
- Each database object definition to be defined in its own file.  In a static class that is called by the dbcontext onmodelcreate method.
- Interfaces are defined in their own file, in an "Abstractions" folder in the project.
- Use the options pattern for configuration, and ensure that all configuration values are stored in the appsettings.json file, and not hardcoded in the code.