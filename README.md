# TaskTrackr
Project management application using Angular for user-friendly task tracking and C# .NET MVC with SQL Server for backend data management on AWS, enabling efficient project assignment, progress tracking, and team collaboration.

Process:
<ul>
  <li>ASP.NET MVC
    <ul>
      <li>Create Models(User, Project, ProjectTask, Milestone) with data annotations.</li>
      <li>Create DTOs to transfer only the required data between the client and server. Also to avoid exposing the navigation properties that cause cycles.</li>
      <li>Add Required NuGet Packages
        <ul>
          <li><code>Microsoft.EntityFrameworkCore</code></li>
          <li><code>Microsoft.EntityFrameworkCore.SqlServer</code></li>
          <li><code>Microsoft.EntityFrameworkCore.Tools</code></li>
        </ul>
      </li>
      <li>Create DBContext to interact with the database. This serves as a bridge between the application and the database using Entity Framework Core.</li>
      <li>Create Repositories with async methods to implement data access logic using Entity Framework and interact with the database.</li>
      <li>Create Controllers for basic CRUD (Create, Read, Update, Delete) operations.</li>
      <li>Configure Database Connection. Set up the connections tring to AWS SQL Server. IN PROGRESS</li>
    </ul>
  </li>
</ul>