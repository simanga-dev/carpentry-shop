## Carpentry Shop
A web application that manages Carpentry Orders and tracks if they're complete.
It also notifies the person who placed the Order upon completion

### Motivation of the Project
The web application solves a practical problem that is faced by employees where
I work. Instead of showing off how much I know about Rest API, Graph QL or any
of the shiny technology I went for a project that show my problem-solving
skills. A similar project is being used where I work.

### Project Setup
The Web Application is built with .Net Core 6 and SQLite. You need to have the
.Net core framework installed on your local machine to run the application
locally. You can check if you have dotnet by running the following command in
your terminal one at a time.

```
dotnet --version

dotnet --list-runtimes

````
if the output contains the number 6 then you are good to go.
Unzip the folder and navigate to the root of the project
Run the following command to start the server

```
dotnet build

dotnet run
```
Your default browser will be open running the application on
localhost.

### Credential For Admin

Email:      admin@localhost.com\
Password:   P@$$w0r1d

### Emails
The email functionality has been commented out because it needs credentials to
work and I did not want to include mine for security reasons but if you are
familiar with C# you can put your SMT configuring on the `appsettings.json` and
comment it out

