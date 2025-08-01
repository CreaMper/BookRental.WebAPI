# Disclaimer
This project is not finished and should be only used as a reference during any technical interview.
Several mechanisms were not implemented on purpose such as logging, authorization etc.

# Book Rental API

![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)

## Features

Specified in the `zadanie.pdf` file

## Quick Start

### Prerequisites
- .NET 8 SDK
- SQL Server (or Docker for containerized DB)

### Installation
 - Create a database for the Development and/or production
    - Create the following environmental variables and insert the connection string
        - BR_ConnectionString_DEV
        - BR_ConnectionString_PROD
    - Migrate the data structure to the new database using .NET CLI `dotnet ef database update`
 - Build the solution via VisualStudio or use .NET 8 SDK `dotnet run --launch-profile "Development"`