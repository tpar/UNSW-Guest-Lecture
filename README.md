# UNSW-Guest-Lecture

I delivered a Guest Lecure for the University of New South Wales (UNSW) on the 6th July 2020. The slides of the same are available <a href="https://github.com/tpar/UNSW-Guest-Lecture/blob/master/UNSW%20Guest%20Lecture.pdf">here</a>
This repository contains the code I presented, with the Broken Access Control vulnerability. 

# Pre-Requisites to run

1. <a href="https://auth0.com">Auth0</a> login
2. SQL Server Instance - You can use <a href="https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15">LocalDB</a>, <a href = "https://azure.microsoft.com/en-us/services/sql-database/campaign/">SQL Server for Azure<a> or <a href = "https://aws.amazon.com/sql/">SQL Server on AWS</a>

# Steps to Run
1. Get the `clientId`, `clientSecret` and `Domain` for Auth0 and set these in the <a href = "https://github.com/tpar/UNSW-Guest-Lecture/blob/master/Application/appsettings.json">appsettings.json</a>
2. Get the connection string for the database and update <a href = "https://github.com/tpar/UNSW-Guest-Lecture/blob/master/Application/appsettings.json">appsettings.json</a>
3. Install .Net Core 3.1
4. You can use the scripts in `db/` folder to create and seed the database with users

