# TaskTrackr
Project management application using Angular for user-friendly task tracking and C# .NET MVC with SQL Server for backend data management on AWS, enabling efficient project assignment, progress tracking, and team collaboration.

Process:
<ul>
  <li>SQL Server on AWS
    <ul>
      <li>Go to <a href="https://aws.amazon.com/">AWS</a> and create an account.</li>
      <li>Once logged in, go to the AWS Management Console and search for "RDS". </li>
      <li>Create and launch Database</li>
      <li>Connect to SQL Server Database - In Connectivity & security use the Endpoint (hostname) and ensure Port is set to 1433 (default for SQL Server).</li>
      <li>Allow Access from application - RDS Security Groups and add a rule to allow inbound traffic on port 1433 from IP.</li>
      <li>Test connection in SQL Server Management Studio
        <ul>
          <li>Open SQL Server Management Studio(SSMS)
            <ul>
              <li>Server Name: Your RDS Endpoint,1433 (e.g., yourdbinstance.abcd1234.region.rds.amazonaws.com,1433).</li>
              <li>Authentication: SQL Server Authentication.</li>
              <li>Username: The master username you created.</li>
              <li>Password: The password you set.</li>
            </ul>
          </li>
          <li>Error with RDS SQL Server instance is using SSL encryption with a certificate that client machine does not trust by default. Configure client to trust the certificate.
            <ul>
              <li>Download the AWS RDS Region Bundle <a href="https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/UsingWithRDS.SSL.html">link</a></li>
              <li>Install Certificate</li>
              <li>Retest connection</li>
            </ul>
            <li>Installed latest version of microsoft sql server management studio.</li>
          </li>
        </ul>
      </li>
      <li>Get connection string and add to appsettings.json</li>
    </ul>
  </li>
  <li>ASP.NET MVC
    <ul>
      <li>Create Models(User, Project, ProjectTask) with data annotations.</li>
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
      <li>Configure Database Connection. Set up the connections string to AWS SQL Server.</li>
      <li>Create migrations and apply to the database to create tables
        <ul>
          <li>Add Migration: ~<code>dotnet ef migrations add InitialCreate</code></li>
          <li>Apply Migration: ~<code>dotnet ef database update</code></li>
          <li></li>
        </ul>
      </li>
      <li>Seed database wtih dummy data
        <ul>
          <li>Create SeedData.cs.</li>
          <li>Modify Program.cs to call the <code>SeedData.Initialize</code> method during application startup.</li>
          <li>Add migrations and apply to the database to create tables
            <ul>
              <li>Add Migration: ~<code>dotnet ef migrations add SeedDataMigration</code></li>
              <li>Apply Migration: ~<code>dotnet ef database update</code></li>
              <li></li>
            </ul>
          </li>
        </ul>
      </li>
      <li>Test all methods on Swagger.</li>
    </ul>
  </li>
  <li>Frontend(Angular)
    <ul>
      <li>Create models(User, Project, ProjectTask).</li>
      <li>Create services to handle communication with the database.</li>
      <li>Install Bootstap: ~<code>npm install bootstrap</code></li>
      <li>Create components for proejct-list and user-list.</li>
    </ul>
  </li>
  <li>Create environments for development and production, specifically for connection string.</li>
</ul>