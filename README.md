# Engineering-Associated

As a company with over 1000 employees in 20+ locations, Associated Engineering (AE) looks to improve the current intranet employee directory, which is essential for connecting employees across locations for project assistance and collaboration. The current employee directory application’s interface is outdated, not intuitive and missing some valuable functionality. 

To modernize the directory interface and improve the functionality of their current system, our team, Engineering Associated (EA), have outlined several features our system aims to achieve:

* __Improved search interface and functionality__: The new search interface will be easy and intuitive to use. It will allow users to search for filters and will include additional search vectors, such as combinations of experience, skills, disciplines, and location. The search results interface will be redesigned to allow easy navigation and view.

* __Dynamically generated organization chart__: An Organization Chart will be automatically generated and updated to display three levels of employee relationships based on the employee selected. 

* __Improved employee card display__: The employee card will have a contemporary, compact design that displays the essential information of an employee and allows users to easily expand and find the information they need.

* __Administrator role to add contractor information__: The administrator will be able to log in and add, modify, or delete contractor information as needed.

We also highlight several technical objectives and the assumptions within each:

* __Capacity__: The application needs to be able to support 1,100 employees, with several hundred simultaneous users. We are also assuming these will be the maximum capacity of users our system will handle, and thus will only guarantee performance scalability to these numbers.

* __Performance__: Should load data quickly by maximizing use of front-end implementation. Users should be able to efficiently query the entire database for candidates that match the filters provided. 

* __Availability__: Application should also achieve 99.9% uptime. Our SPOF will be our server and database, as we are only using one instance of each.  

* __Security__: Role-authentication will be implemented. Accounts can only perform authorized actions based on their account type.
