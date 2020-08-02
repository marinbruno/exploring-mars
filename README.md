<h1 align="center">
    ExploringMars Console Application
</h1>

<p align="center">
    <img src="mars.jpg">
</p>

<h5 align="center">
    .NET Core application that helps users land their probes at Mars' plateaus.
</h5>

[![](https://img.shields.io/badge/.NET%20Core-v3.1-blueviolet)](https://dotnet.microsoft.com/download)

# Table of Contents

- [Table of Contents](#table-of-contents)
  - [Our Goal](#our-goal)
  - [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Contact](#contact)
  - [References](#references)
  
## Our Goal
Provide a simple but accurate tool to calculate probe's landing spot.

## Getting Started
ExploringMars is a .NET Core v3.1 console application and it was built following methodologies, principles and design patterns, such as DDD, MVC, SOLID, Dependency Injection, in order to make the software more understable, flexible, mantainable and scalable.

The following libraries are used at this application:

- [Microsoft DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/)
- [FluentValidation](https://fluentvalidation.net/)
- [xUnit](https://xunit.net/)
- [Moq](https://www.nuget.org/packages/Moq/)
- [FluentAssertions](https://fluentassertions.com/)

## Installation
1. Install .NET Core v3.1+

2. Clone the repo and `cd` into it

3. Restore solution's packages

## Usage
Before we run the application, there a few things you should know.

- The Mars plateau may be as big as you want. Just input two integers separate by a blankspace and you are good to go. Note that we do not accept negative numbers, ok?
- Every probe has a starting position and a direction - we call it starting setup. Any direction is accepted, as long as it capitalized, and the probe's starting position must be within the plateau limits defined by you.
- The probe's possible instructions are R (rotate right), L (rotate left) and M (move). The input must be capitalized and with no blankspaces between them.
- Your probe is not allowed to move outside the plateau. Thus, if you try to move them out, it will stay at the same position until a new valid movement takes place.

Now, to run the application, it is simple.

Through the command line, move inside the repo directory and enter the following command: `dotnet run --project src/ExploringMars.Application/ExploringMars.Application.csproj`.

## Contact

- Bruno Marin (marinbruno92@gmail.com)