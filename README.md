# ASP .NET Core Web API - JobPortal Project

  
## 
IDE: Visual Studio 2022  
Database: Microsoft SQL Express 2019  
ASP Version: .NET Core 6  
  

## Database Info
Database credentials  
User ID: job_portal_db_user  
Password: admin@1234  
Database sql script as well as .bak backup file both provided in the /Database directory. You can use either one of the two.  

  
## API Authentication Info
API Authentication has been implemented.
Make a post request to the below URL (port number may vary)
> https://localhost:7202/api/v1/Authenticate/Login
```json
{
  "username": "admin@jobportal.com",
  "password": "admin@1234"
}
```
Copy the token and add it to the Bearer Token Authorization header to make request.  
All the endpoint have been protected except the below endpoint.  
> /api/v1/jobs/list