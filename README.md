# Student Registration System

This project is a .NET Windows Forms application designed for managing student, course, and academic personnel records. It interfaces with an MSSQL database and includes a main form that allows navigation between different management forms.

## Features

- **Student Management**: Add, edit, and delete student records.
- **Course Management**: Manage courses including creation, modification, and deletion of course records.
- **Academic Personnel Management**: Handle academic staff details, allowing for additions, edits, and removals.
- **Database Integration**: Uses MSSQL to store and manage all data with relational integrity.

## Prerequisites

- .NET Framework 4.7.2 or later.
- Microsoft SQL Server 2019 or later.

## Installation

Follow these steps to get your development environment set up:

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Restore any NuGet packages if needed.
4. Update the connection string in the application configuration file (`App.config` or `Web.config`) to match your MSSQL server settings.
5. Build the solution to ensure all dependencies are properly set up.

## Database Setup

1. Create a new database in SQL Server.
2. Run the provided SQL script (`database_script.sql`) to create the required tables and relationships.
3. Ensure the connection string in your project matches the database settings.

## Running the Application

- Run the application from Visual Studio by pressing `F5` or `Ctrl+F5` to start without debugging.
- Use the main form to navigate to the various management forms.

## Contributing

Contributions are welcome. Please fork the repository and submit pull requests to the master branch. Alternatively, if you find any issues, please report them using the GitHub issues tracker.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any queries, please open an issue in the GitHub repository, or contact me directly through my GitHub profile.

