# TimeFlow
A modern time management and time tracking application built with C# and the .NET ecosystem.

TimeFlow started as a personal learning project to deepen my knowledge of C#, software architecture, databases, and modern .NET development practices. At the same time, it solves a real problem: after several useful features in my favorite time management app became part of their paid plans, I decided to build my own solution that I can fully customize and use on a daily basis.

The project follows an incremental approach. Development starts with a console application to focus on C# fundamentals, object-oriented programming, CRUD operations, and database integration. Once the core functionality is stable, the application will evolve into a full ASP.NET Core web application.

## Current Development Phase

**Phase 1: Console Application**

The current goal is to build and validate the core business logic:

* Create, read, update, and delete projects
* Create, read, update, and delete time entries
* Start and stop timers
* Generate daily, weekly, monthly and yearly overviews
* Persist data using a database
* Build a clean and maintainable architecture

After the foundation is complete, the project will move to the web.

**Phase 2: Web Application**

Planned improvements:

* User authentication
* Responsive web interface
* Dashboard and analytics
* Multiple projects and categories
* CSV import and export
* Dark mode

## Tech Stack

### Current Stack (Console Application)

* C#
* .NET
* Console Application
* Entity Framework Core
* SQLite
* LINQ
* xUnit (planned)

### Future Stack (Web Application)

* ASP.NET Core
* Razor Pages
* Entity Framework Core
* SQL Server or PostgreSQL
* ASP.NET Core Identity
* Bootstrap
* xUnit

## Project Structure

```text
TimeFlow
├── TimeFlow.Console
├── TimeFlow.Web          (planned)
├── TimeFlow.Tests        (planned)
└── TimeFlow.sln
```

## Getting Started

Clone the repository:

```bash
git clone https://github.com/yourusername/time-flow.git
```

Run the console application:

```bash
cd TimeFlow.Console
dotnet run
```

## Roadmap

### Version 1.0 - Console Foundation

* [x] Project CRUD
* [x] Time entry CRUD
* [ ] Timer functionality
* [ ] Weekly, monthly and yearly reports
* [x] SQLite integration
* [ ] Unit tests

### Version 2.0 - Web Application

* [ ] ASP.NET Core migration
* [ ] User authentication
* [ ] Dashboard
* [ ] Responsive UI
* [ ] CSV import/export
* [ ] Billable hours

### Version 3.0

* [ ] Notifications
* [ ] Mobile support

## Screenshots

Coming soon.

## License

MIT License
