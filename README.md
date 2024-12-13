# TaskTrackr
Task management application using ASP.NET Core, Entity Framework Core, Angular, Bootstrap, and SQL Server on AWS RDS, enabling efficient project assignment, progress tracking, and team collaboration.

Process:
<ul>
  <li>SQL Server Management Studio
    <ul>
      <li>Connect to Server
        <ul>
          <li>Server Type: Database Engine</li>
          <li>Server Name: localhost</li>
          <li>Authentication: Windows Authentication</li>
          <li>Connect</li>
        </ul>
      </li>
      <li>Create a new Database
        <ul>
          <li>In the <strong>Object Explorer</strong>, right-click on Databases and select <strong>New Database</strong></li>
          <li>Database Name: TaskTrackrDb</li>
        </ul>
      </li>
      <li>Configure a User
        <ul>
          <li>Expand the <strong>Security</strong> node.</li>
          <li>Right-click <strong>Logins</strong> and select <strong>New Login</strong></li>
          <li>In the <strong>Login - New</strong> dialog:
            <ul>
              <li>Login Name: Enter a username</li>
              <li>Authentication: Choose SQL Server Authentication and set a password.</li>
            </ul>
          </li>
          <li>In the left panel, go to <strong>User Mapping</strong>, check your database (TaskTrackrDb), and assign the db_owner role.</li>
          <li></li>
        </ul>
      </li>
      <li>Configure the Connection String in appsettings.json
        <ul>
          <li><code>"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TaskTrackrDb;Trusted_Connection=True;"</code></li>
        </ul>
      </li>
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

Archived Process:
<ul>
  <li>SQL Server on AWS RDS
    <ul>
      <li>CANCELED AWS DUE TO COSTS</li>
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
      <li>CANCELED AWS DUE TO COSTS</li>
    </ul>
  </li>
</ul>