# The Silton Foundation - Grant Applications (WORK IN PROGRESS)
This product was commissioned by The Silton Foundation in April 2019 to replace its manual application process with an online, workflow-managed process. TSF-Applications is an ASP.NET Core (MVC + Razor Pages) application built with the AWSSDK-Core and AWS.SimpleEmailService libraries to support deployment and services on AWS. It was deployed using AWS - Elastic Beanstalk to orchestrate the needed AWS services (such as Elastic Container Service (ECS), Relational Database Service (RDS), and service / security groups). 

Deployed on AWS at: http://siltonfoundationapp-env.tv6vfag29m.us-west-2.elasticbeanstalk.com/

## Getting Started, Build, and Test
To build and run this page locally (using Visual Studio and SQL Server):
1. Clone the repo locally and compile it. There are no additional external dependencies or data sources to load/access.
2. Build an AWS Elastic Beanstalk app using .NET 4.0 runtime and an RDS instance using SQL Server (t2.micro).
3. This application builds DB connection strings using env vars from appsettings.json. Add these to your appsettings.json, and add them to your AWS app's 'Environment Properties":  
    {  
      "RDS_HOSTNAME": "copy/paste your AWS.RDS instance Endpoint here",  
      "RDS_USERNAME": "enter the username you selected for your AWS.RDS instance here"  
      "RDS_PASSWORD": "enter the password you selected for your AWS.RDS instance here",  
      "RDS_DBNAME_USER": "SiltonUser",  
      "RDS_DBNAME_SCHOL": "SiltonScholarship",  
    }
  
4. Build the scholarships database using existing migration (Update-Database -Context ScholarshipDbContext).
5. Build the users database using existing migration (Update-Database -Context AppUserDbContext).
6. Deploy the app to AWS using the AWSSDK, the online console, or the CLI (per your preference).
Alternately, replace the connection string builders with local connection strings (with same DB names), run via your local/live server.  
7. Register a new user using the email address 'admin@thesiltonfoundation.org'. This will be a perpetual admin user, which initiates your ability to manage user roles in your database. 


## Architecture
This application is built on ASP.NET Core using Model-View-Controller (MVC) and Razor Pages (MV-VM) architecture. It relies on 2 SQL Server databases - one to manage grant applications, and a separate one to manage user accounts (data security requires separation of these concerns).  Both databases are built using Entity Framework Core.  
  
Notification emails are automatically generated on registration and application submittal using AWS Simple Email Service.  Future releases are intended to leverage auto-emailer service for a variety of defined data state changes ()

### User Database
The User data model contains only basic properties (name and birthdate); however, the Identity API (Microsoft.AspNetCore.Identity) adds properties required in order to support security requirements. A View Model allows the application to capture data to populate the other Identity-initiated properties (ie - password) without having those appear in the User class. User 'claims' are limited to FullName and Email for greeting purposes only.  
  
The app leverages the .NET Identity library for authentication and to enforce role-based authorization. Site policy grants anonymous access to Login and Registration only (served by MVC structure). Role-specific dashboards (served by individual Razor Pages to separate concerns) with access restrictions are the backbone of the workflow management strategy.  Defined roles include:  
  - General: This role is applied to every authenticated user. It carries the most restrictive access privilege of any authenticated user.
  - Applicant: This role is applied to any user who launches a grant application. It carries access privilege for the 'Applicant Dashboard' only. A user can not be an 'Applicant' if (s)he is an 'Evaluator'.
  - Evalutor: This role is applied to a user only if assigned by an 'Admin'. It carries access privilege for the 'Evaluator Dashboard' only.
  - Admin: This role is applied to a user only if assigned by an 'Admin'. A perpetual (protected) 'Admin' account is built into the database to ensure that an accessible 'Admin' account is always available in case of errors in role assignments. This is because every 'Admin' user has the ability to modify (not delete) user accounts and change role assignments (for 'Admin' and 'Evaluator' only).

3rd-party authentication is not a current or planned feature of this application.

### Scholarship Database
The 'SFApp' data model contains properties to envelope the application - it identifies the applicant, the application year, and which grant the applicant is seeking, and it carries application status and award decision details.  

The 'Question' data model carries the question prompt and specifies the expected response type (so that the proper input type is rendered). It also carries flags to turn the question and its scoring to 'Active' (both flags managed in Admin Dashboard, planned future release) so that this year's applications are only associated with the question in years when it's active, and Evaluator scoring is only available for appropriate questions.  

The 'AppResponse' data model is a join between an SFApp and a Question; that is, it connects a Question object to an SFApp object, and it holds the applicant's answer as payload. On SFApp instantiation, an AppResponse object is created for each 'Question' object in the database that is flagged as 'Active' at that time.  Further, it 'collects' Evaluation objects (scores from each Evaluator for that Question on that SFApp).

The 'Evaluation' data model supports a planned future release. Each applicant answer is evaluated/scored (if the associated Question object is flagged as 'Score'), and the Evaluation object carries that score for that Evaluator, along with the Evaluator's email (for identification).


Dependency injection is used to isolate the data storage from its point of use (ie - the CRUD logic in the page routes). The Identity API includes interfaces and services for the user - those interfaces have been injected into pages where user data is needed to change the displayed content (such as displaying a login link if no user is logged in). Additionally, the app contains an interface package for scholarship data to avoid direct coupling between page route logic and the SiltonScholar database. Interfaces and associated services are built for each class described above.


![Application Screenshots](layouts/Slide1.JPG)  
![Application Screenshots](layouts/Slide2.JPG)  
![Application Screenshots](layouts/Slide3.JPG)  
![Application Screenshots](layouts/Slide4.JPG)  
![Application Screenshots](layouts/Slide5.JPG)  
![Application Screenshots](layouts/Slide6.JPG)  
![Application Screenshots](layouts/Slide7.JPG)  
![Application Screenshots](layouts/Slide8.JPG)  
![Application Screenshots](layouts/Slide9.JPG)  
![Application Screenshots](layouts/Slide10.JPG)  
![Application Screenshots](layouts/Slide11.JPG)  
![Application Screenshots](layouts/Slide12.JPG)  
![Application Screenshots](layouts/Slide13.JPG)  
![Application Screenshots](layouts/Slide14.JPG)  
![Application Screenshots](layouts/Slide15.JPG)  
![Application Screenshots](layouts/Slide16.JPG)  
