using System.ComponentModel.DataAnnotations;

namespace SampleSite.Models;

/**
    These model classes are used with Entity Framework Core (EF Core) to work with
    a database. EF Core is an object-relational mapping (ORM) framework that
    simplifies the data access code that you have to write.

    The model classes created are known as POCO classes, from Plain Old CLR
    Objects. POCO classes don't have any dependency on EF Core. They only define
    the properties of the data to be stored in the database.

    Now: Use the scaffolding tool to produce Create, Read, Update, and Delete
    (CRUD) pages for the movie model.
    In Solution Explorer, right-click the Controllers folder and select Add > New Scaffolded Item.

    Scaffolding adds the following packages:

    Microsoft.EntityFrameworkCore.SqlServer
    Microsoft.EntityFrameworkCore.Tools
    Microsoft.VisualStudio.Web.CodeGeneration.Design
    Scaffolding creates the following:

    A movies controller: Controllers/MoviesController.cs
    Razor view files for Create, Delete, Details, Edit, and Index pages: Views/Movies/*.cshtml
    A database context class: Data/MvcMovieContext.cs
    Scaffolding updates the following:

    Inserts required package references in the MvcMovie.csproj project file.
    Registers the database context in the Program.cs file.
    Adds a database connection string to the appsettings.json file.
    The automatic creation of these files and file updates is known as scaffolding.

    Afterwards:
    In Package Manager Console:
    Add-Migration InitialCreate
    Update-Database

    Add-Migration InitialCreate: Generates a
    Migrations/{timestamp}_InitialCreate.cs migration file. The InitialCreate
    argument is the migration name. Any name can be used, but by convention, a name
    is selected that describes the migration. Because this is the first migration,
    the generated class contains code to create the database schema. The database
    schema is based on the model specified in the MvcMovieContext class.

    Update-Database: Updates the database to the latest migration, which the
    previous command created. This command runs the Up method in the
    Migrations/{time-stamp}_InitialCreate.cs file, which creates the database.
 */

public class Product
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Genre { get; set; }
    public decimal Price { get; set; }
}
