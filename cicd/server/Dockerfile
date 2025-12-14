FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution + csproj files (with correct directory levels)
COPY ExpenseTracker/ExpenseTracker.sln ./ExpenseTracker/
COPY ExpenseTracker/ExpenseTracker.API/ExpenseTracker.API.csproj ./ExpenseTracker/ExpenseTracker.API/
COPY ExpenseTracker/ExpenseTracker.Application/ExpenseTracker.Application.csproj ./ExpenseTracker/ExpenseTracker.Application/
COPY ExpenseTracker/ExpenseTracker.Domain/ExpenseTracker.Domain.csproj ./ExpenseTracker/ExpenseTracker.Domain/
COPY ExpenseTracker/ExpenseTracker.Infrastructure/ExpenseTracker.Infrastructure.csproj ./ExpenseTracker/ExpenseTracker.Infrastructure/
COPY ExpenseTracker/ExpenseTracker.Tests/ExpenseTracker.Tests.csproj ./ExpenseTracker/ExpenseTracker.Tests/

COPY ExpenseTracker ./ExpenseTracker/

# Restore from inside the solution folder
WORKDIR /src/ExpenseTracker
RUN dotnet restore

WORKDIR /src/ExpenseTracker/ExpenseTracker.API
# Publish API project
RUN dotnet publish -c Release -o /app/publish

# -------- RUNTIME IMAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "ExpenseTracker.API.dll"]
