# TimeFlow
A modern time management and time tracking application built with C#, .NET and SQLite.

TimeFlow started as a personal learning project to deepen my knowledge of C#, software architecture, databases, and modern .NET development practices. At the same time, it solves a real problem: after several useful features in my favorite time management app became part of their paid plans, I decided to build my own solution that I can fully customize and use on a daily basis.

The application allows users to manage projects, track time, and generate reports. Development follows an incremental approach, starting with a console application to focus on C# fundamentals, object-oriented programming, CRUD operations and database integration. Once the core functionality is stable, the project will evolve into a full ASP.NET Core web application.

![.NET](https://img.shields.io/badge/.NET-10-purple)
![License](https://img.shields.io/badge/license-MIT-blue)
![Status](https://img.shields.io/badge/status-Active-green)

## Table of Contents

- [Features](#features)
- [Screenshots](#screenshots)
- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [Roadmap](#roadmap)
- [Learning Objectives](#learning-objectives)
- [Future Improvements](#future-improvements)
- [Contributing](#contributing)
- [License](#license)

## Features

### Project Management

- Create projects
- Edit project information
- Delete projects
- View all projects

### Time Tracking

- Log time entries
- Edit existing entries
- Delete entries
- Link entries to projects

### Reporting

Generate reports for

- Daily overview
- Weekly overview
- Monthly overview
- Yearly overview

### Data Persistence

- SQLite database
- Entity Framework Core
- LINQ queries

## Screenshots

### Main Menu

![Main Menu](screenshots/main-menu.png)

### Project Overview

![Projects](screenshots/projects.png)

### Reports

![Reports](screenshots/report.png)

## Techologies

Language
- C#

Framework
- .NET 10

ORM
- Entity Framework Core

Database
- SQLite

Architecture
- Layered Architecture
- Services
- Models
- Menus

Version Control
- Git
- GitHub

## Installation

### Prerequisites

- .NET SDK 10
- Git

### Clone repository

```bash
git clone https://github.com/username/TimeFlow.git
```

### Navigate

```bash
cd TimeFlow
```

### Restore packages

```bash
dotnet restore
```

### Run

```bash
dotnet run
```

## Usage

After launching the application, users can:

1. Create a project.
2. Manage existing projects.
3. Log time entries.
4. Edit or delete time entries.
5. Generate daily, weekly, monthly or yearly reports.
6. Exit the application safely.

## Roadmap

### v1.0 Console Foundation

- [x] Project CRUD
- [x] Time Entry CRUD
- [x] Reporting
- [x] SQLite Integration
- [x] Input Validation
- [ ] Business Rule Validation
- [ ] Unit Tests

### v1.1

- Timer functionality
- CSV import/export
- Custom date range export
- Report by project

### v2.0 ASP.NET Core

- MVC architecture
- SQL Server
- Authentication
- Dashboard
- Charts
- Responsive UI

### v3.0

- REST API
- Mobile App
- Docker
- Cloud Deployment

## Learning Objectives

This project is used to practice

- Object-Oriented Programming
- Clean Code
- Repository Design
- Entity Framework Core
- LINQ
- Database Design
- Git & GitHub Workflow
- Software Architecture

## Future improvements

- JSON Import
- Statistics
- Categories
- Tags
- Billable Hours
- Dark Mode
- Docker
- Logging

## Contributing

Contributions are welcome.

Please create an issue before submitting major changes.

## License

MIT License
