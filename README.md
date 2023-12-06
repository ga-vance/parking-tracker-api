# Parking Tracker API

Parking Tracker API is a .NET back-end project written in C#. It allows registered users to track visits to designated parking lots. There is a corresponding front-end project which will facilitate interaction with this API.

[Front-end Coming Soon](https://github.com/ga-vance/parking-tracker-api)

## Get Started

#### You will need:

-   .NET 8
-   Entity Framework Tools
-   PostgreSQL

#### Development

Run the following commands in the terminal to ensure that the project is set up correctly

```sh
dotnet restore
dotnet build
dotnet run
```

---

#### Setup Environment

This project assumes a .env file to store project secrets such as the database connection string, and a signing token for the generated JSON Web Token. An example .env file might look like

```
# Replace with your own database connection string
DB_CONNECTION_STRING = Host=localhost;Database=parking-tracker;Username=postgres;Password=postgres
# Must be minimum 31 characters long
SIGNING_TOKEN = <31 characters long token>
```

---

#### Setup Database

Code first migration is used to setup the database, and seed with initial data. Several example lots are created as well as a default admin user. **Ensure password is changed in live environments!**

```json
{
    "username": "admin",
    "password": "admin"
}
```

To create the database, ensure you have PostgreSQL installed and running. The migration commands assume that Entity Framework Core Tools are installed gloablly. Run '`dotnet tool install --global dotnet-ef`' to install EF Core tools globally, then run the following commands to setup the database

```sh
dotnet ef migrations add <Migration Name>
dotnet ef database update
```

---

#### API Documentation

The API can be accessed for development and local testing at **http://localhost:5149/swagger** or **https://localhost:7040/swagger** if launched with '`dotnet run --launch-profile https`'
![Alt Capture of swagger api documentation](/Design/ApiDocumentation.png)

---

## Usage

The API is designed to be used by only registered users. Authentication and Authorization are managed using JSON Web Tokens. The token is received after successfully logging in, and can be sent as a bearer token in the Authorization header.
For example:

```sh
curl -X 'GET' \
  'http://localhost:5149/api/Lot' \
  -H 'accept: text/plain' \
  -H 'Authorization: bearer <token>
```

All routes except for login (HTTP POST /api/auth) are protected. User accounts only have access to changing their passwords, and creating new visits. All other functionality is restricted to admin users.

### Admin Users

Admin users are able to create other users, modify user information, and modify lot information. Admins must create a new regular user before they are able to interact with the system.

### Users

Once registered, users are able to change their passwords, and log visits to parking lots. The user is able to search for visits based on licence plate number to see how many times a given vehicle has visited one of the designated parking lots. They are also able to provide a vehicles licence plate number, province, and a parking lot ID number to create a new logged visit for that vehicle.
