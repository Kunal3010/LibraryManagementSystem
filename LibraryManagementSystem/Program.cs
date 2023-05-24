using System.Data.SqlClient;
using Spectre.Console;
using Unity;

namespace LibraryManagementSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            LibraryManagement library = new LibraryManagement();
            AnsiConsole.Write(new FigletText("Library Management System").Centered().Color(Color.LightSteelBlue3));
            bool Login = false;
            AnsiConsole.MarkupLine("[bold blue underline]Login[/]");
            while (!Login)
            {
                string username = AnsiConsole.Ask<string>("[yellow]Enter Username: [/]");
                string password = AnsiConsole.Ask<string>("[yellow]Enter Password: [/]");
                Login = library.loginUser(username,password);
            }

            Console.WriteLine();
            while (Login)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[green]Select an option[/]")
                    .AddChoices(new[]
                    {
                                "Add Book Details", "Edit Book Details", "Delete Book Details",
                                "Add Student Details", "Edit Student Details", "Delete Student Details",
                                "Issue Book to Student","Return Book from Student","Search Books by Author or Publication Name",
                                "Search Student by Roll No","Show Students with Issued Books","Exit"
                    }));

                switch (choice)
                {
                    case "Add Book Details":
                        string title = AnsiConsole.Ask<string>("[yellow]Enter Title:[/]");
                        string author = AnsiConsole.Ask<string>("[yellow]Enter Author:[/]");
                        string publication = AnsiConsole.Ask<string>("[yellow]Enter Publication:[/]");
                        Book book = new Book
                        {
                            Title = title,
                            Author = author,
                            Publication = publication,
                            IsIssued = false
                        };
                        library.AddBookDetails(book);
                        break;

                    case "Edit Book Details":
                        int bookid = AnsiConsole.Ask<int>("[yellow]Enter Book Id to Edit:[/]");
                        string newtitle = AnsiConsole.Ask<string>("[yellow]Enter Title:[/]");
                        string newauthor = AnsiConsole.Ask<string>("[yellow]Enter Author:[/]");
                        string newpublication = AnsiConsole.Ask<string>("[yellow]Enter Publication:[/]");
                        Book updatedbook = new Book
                        {
                            Id = bookid,
                            Title = newtitle,
                            Author = newauthor,
                            Publication = newpublication,
                            IsIssued = false
                        };
                        library.EditBookDetails(updatedbook);
                        break;

                    case "Delete Book Details":
                        int bookIdToDelete = AnsiConsole.Ask<int>("[yellow]Enter Book Id to Delete:[/]");
                        library.DeleteBookDetails(bookIdToDelete);
                        break;

                    case "Add Student Details":
                        string name = AnsiConsole.Ask<string>("[yellow]Enter Student Name:[/]");
                        long mobile = AnsiConsole.Ask<long>("[yellow]Enter Mobile No: [/]");
                        Student student = new Student
                        {
                            Mobile = mobile,
                            Name = name,
                            Issued = false
                        };
                        library.AddStudentDetails(student);
                        break;

                    case "Edit Student Details":
                        int rollnumtoedit = AnsiConsole.Ask<int>("[yellow]Enter Student Roll Number:[/]");
                        string newName = AnsiConsole.Ask<string>("[yellow]Enter New Name:[/]");
                        Student updatedStudent = new Student
                        {
                            RollNo = rollnumtoedit,
                            Name = newName
                        };
                        library.EditStudentDetails(updatedStudent);
                        break;

                    case "Delete Student Details":
                        int rollNoToDelete = AnsiConsole.Ask<int>("[yellow]Enter Student Roll Number you want to Delete:[/]");
                        library.DeleteStudentDetails(rollNoToDelete);
                        break;

                    case "Issue Book to Student":
                        int bookIdToIssue = AnsiConsole.Ask<int>("[yellow]Enter Book Id You want to Issue:[/]");
                        int studentRollNo = AnsiConsole.Ask<int>("[yellow]Enter Your Roll Number:[/]");
                        library.IssueBookToStudent(bookIdToIssue, studentRollNo);
                        break;

                    case "Return Book from Student":
                        int bookIdToReturn = AnsiConsole.Ask<int>("[yellow]Enter Book Id You want to Return:[/]");
                        int studentRollNum = AnsiConsole.Ask<int>("[yellow]Enter Your Roll Number:[/]");
                        library.ReturnBookFromStudent(bookIdToReturn, studentRollNum);
                        break;

                    case "Search Books by Author or Publication Name":
                        string searchText = AnsiConsole.Ask<string>("[yellow]Enter Author/Publisher Name:[/]");
                        library.SearchBooksByAuthorOrPublication(searchText);
                        break;

                    case "Search Student by Roll No":
                        int rollNoToSearch = AnsiConsole.Ask<int>("[yellow]Enter Student Roll Number to search:[/]");
                        library.SearchStudentByRollNo(rollNoToSearch);
                        break;

                    case "Show Students with Issued Books":
                        library.GetIssuedBook();
                        break;

                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}