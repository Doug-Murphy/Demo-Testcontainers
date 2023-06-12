# Introduction
This repo is to showcase how one can use Testcontainers. When looking at a new technology, you of course want to know what it is and why you should use it. So let's explore that below.

## What is Testcontainers?

Testcontainers is a NuGet package that is used for automatically spinning up and tearing down specific containers for the purposes of automated testing.
In this solution, you will see how xUnit can leverage this setup to automatically spin up a resource, run the tests, and then tear it down afterwards.

## Why use Testcontainers?

Cleanup after tests can be hard. So can ensuring you have a proper test bed for your tests to execute against.
You may need to make sure you use a specific account, or make sure you have a specific set of data before a test run.
These things can be really hard to do without Testcontainers. It also becomes problematic from a parallelization standpoint.
Imagine different test classes have a specific prerequisite data set that may be incompatible with each other.
You can automatically spin up two different containers for these two different test classes and each one will be completely isolated from the other.  
Since it makes a new containerized test resource for you, you also don't need to worry about any cleanup after your tests have executed!
Lastly, it also potentially gives you a more realistic test scenario for your code since you won't be running against a mocked instance.
It will be running a real, containerized instance of your dependent resource.

# Projects

## TestcontainersDemo.WebApi
An ASP.NET Core WebApi running in .NET 7. This web API will contain a few endpoints to do a simple data access to be our system under test (SUT).

## TestcontainersDemo.Infrastructure.Postgres
This project houses the DB interaction with our Postgres database. It contains the migrations for EF Core, DB model, repository, and EF Core DB context.

## TestcontainersDemo.Tests.Integration
This project will house our integration tests to our containerized resources using the `Testcontainers` NuGet package.

# Running The Project

## Docker Compose
A Docker Compose file is included in this repo and should make things very easy to get up and running.
First make sure that you have [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed, or install Docker and Docker Compose however you'd like.

After Docker and Docker Compose are installed, ensure they are working correctly be executing the following commands:

For Docker: `docker --version`

For Docker Compose: `docker compose version`

After you have verified that Docker and Docker Compose have been installed correctly, type `docker compose up -d` to start the required services.

## Database Setup
There is nothing that you need to do for your database setup. This project uses EF Core Migrations so your required DB objects will be created for you, if needed, when the API starts.