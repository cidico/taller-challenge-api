# Taller Challenge - API Challenge

This document outlines the enhancements made to the `GetUser` endpoint in the `UserController` to improve security, 
particularly focusing on input sanitization and other security measures.

## How to run

Just open the terminal and run the follow commands in the project folder:

`git clone https://github.com/cidico/taller-challenge-api.git`

`cd taller-challenge-api`

`dotnet restore`

`dotnet build`

`dotnet run --project TallerAPI/TallerAPI.csproj  --environment=Development` - Use the flags so you can access http://localhost:5082/swagger/index.html

## Overview

The endpoint `/User/{username}` was updated to ensure better handling and sanitization of user input, preventing 
common security vulnerabilities like SQL Injection, Cross-Site Scripting (XSS). Below are the detailed improvements:

- **What was done**: 
  - A check was added to ensure the `username` parameter is not null or empty and is within a reasonable length 
  (1 to 50 characters) to prevent potential buffer overflow or resource exhaustion attacks.
  - Added a RegEx to only allow letters, numbers and underscore.
  - Added output encoding to avoid XSS attacks.
  - Query parameterized to avoid SQL Injection attacks.

## A few extra details

I took some liberty of adding a few extra details to help you review my changes.

Here is a small list:

- Added a new endpoing `/users` that allows you to see all the users registred.
- Added a seeding data step during the initialization so you could have some data to test it out.
- Using InMemory database. It's recreated at every run with new data.
- Used a lib just to mock fake data to the endpoints.


