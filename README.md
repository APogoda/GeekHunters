Geek Hunters!
=============

Welcome to the GRS application (ASP NET CORE 2.0 + Angular)

**GRS system functional requirements:**

*   register a new candidate:
    *   first name / last name
    *   select technologies candidate has experience in from the predefined list
*   view all candidates
*   filter candidates by technology

**GRS system non-functional requirements (extended):**

*   Edit an existing candidate:
    *   first name / last name
    *   re-select technologies candidate has experience in from the predefined list
*   Delete an existing candidate

*   View all skills
*   Add new skill
*   Delete an existing skill

**[REST API description](https://web.postman.co/collections/144825-253de76e-aff9-45de-a5b6-8158627e466d?workspace=b712cd2b-ee40-4828-816c-94d5302d9cfb#introduction)**

**The application built with:**

*   [ASP.NET Core](https://get.asp.net/) and [C#](https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx) for cross-platform server-side code
*   [Angular](https://angular.io/) and [TypeScript](http://www.typescriptlang.org/) for client-side code
*   [Angular CLI](https://cli.angular.io//) for generate components
*   [Webpack](https://webpack.github.io/) for building and bundling client-side resources
*   [Bootstrap](http://getbootstrap.com/) for layout and styling
*   [NUnit](http://nunit.org/) for unit testing
*   Packed in [Docker](https://www.docker.com/) image for platform independency and run as expected
*   Continuous integration with [Travis CI](https://travis-ci.com/) _(build,test,docker packaging,docker image pushing)_

**Development DB context - Local Sql lite**

**Production DB context - Remote SQL Server**

**Working example - [GeekHunters](http://45.76.114.242/)**

**Run on a new machine with simply one command:**

**_docker run -d -p 80:80 --name=geekhunters apogoda/geekhunters_**