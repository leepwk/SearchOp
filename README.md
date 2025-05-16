# SearchOp
Search Engine Result solution

This application comprises a Web API middleware, an optional but highly recommended database, and a frontend built with Angular utilizing Material Design components.

Regarding the web scraping technique, it is important to note that Google’s terms and conditions strongly discourage and prohibit scraping. However, for the purposes of this development exercise (and explicitly not intended for production environments), scraping is being implemented. It is worth mentioning that other search engines may impose similar restrictions. Although attempts were made to scrape results from Google, the necessity of rendering JavaScript on Google’s end required the use of a headless browser (Microsoft Playwright). On the initial execution of the solution, Playwright will be installed, causing a delay of approximately one minute.

When selecting the option to use Playwright for Google search scraping, a new browser instance will be launched, and passing the CAPTCHA challenge is typically required before results can be scraped.

The recommended approach is to utilise official Google APIs or SearchAPI provider service to avoid potential violations of terms and conditions. For Bing, playwright is used in the headless state

The database is designed to persist data; however, in the event of database unavailability, the system has resilience built in. While there may be slower response times due to failure in communicating with the database, the application will continue to function.

Results for a single day are stored in the database, they are overwritten with the most recent results for that day if run multiple times

The best results are using the Bing search with Playwright

The Web API can be run and tested using swagger documentation. A page with testable endpoints is available

## Main Features

- Headless browser search scraping
- JavaScript-rendering using Playwright (Chromium) - Bing by default (using headless) and Google if selected
- Other search engine types will attempt to use a generic request and html document scraper
- Database implementation for data/history persistence - without a database, only the current search results will be rendered
- Scatter chart for data points display

## If there was more time....

- Separation of Web API solution into separate projects for Host, Service and Repository layers, each with their own models - currently in one project for convenience
- Better server side validation of data input using FluentValidation, more useful error messaging
- Speech to text implementation on the UI
- Load search engine terms and conditions to check whether scraping is allowed or not
- Additional SearchAPI scraper implementation for Google
- Entity Framework for robust datastore processing
- Further Unit Tests

## Tech Stack

- Web API developed in Visual Studio 2022 .NET 8.0, C#
- Frontend developed in Visual Studio Code - Angular CLI 17.0.10, node 18.17.0 (https://nodejs.org/en/download/), npm 10.3.0

## Getting Started

### 1. Clone the Repository

`git clone https://github.com/leepwk/SearchOp.git`

### 2. Build and Run the solutions

`cd SearchOp/api` - for the Web API
Visual Studio 2022 is recommended IDE

After restore of all packages (access to nuget.org is a must) and successful Build - on the first run of the solution (IIS Express tested), the Playwright minimal browser will be installed so there will be an initial delay for this and run automatically in Program.cs

Running by default as localhost:44343

```Console.WriteLine("Installing Playwright browsers...");```

```var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });```

```if (exitCode != 0)```

```{```

```    Console.WriteLine("Browser installation failed.");```

```    Environment.Exit(exitCode);```

```}```

```Console.WriteLine("Browser installation completed.");```

The ConnectionString configuration is found in **appsettings.Development.json** - replace with the details of a local database

```"ConnectionString": "Server=localhost\\SQLEXPRESS;Database=YourDatabaseName;Trusted_Connection=True"```

There is a folder, **DBSCripts**, in the solution containing the database creation and data load (for some historical test data) sql script to run

---
`cd SearchOp/static-web-app` - for the angular frontend

Install version 18 of Node via the download url (https://nodejs.org/en/download/) - includes automatic install of `npm` node package manager which is used to install/restore packages in the angular project

Optional installation of `nvm` to manage versions of node (https://github.com/coreybutler/nvm-windows/releases) to download a setup executable

`npm install -g @angular/cli@17.0.10` - install specific version of angular

`npm install` - install all project packages

`ng serve` - to run solution, default port is 4200 ie. running as localhost:4200

In ngx-config.json, the Web API endpoint url is set to the default localhost and port, update as necessary

```"searchApiWebUrl":  "https://localhost:44343"```

