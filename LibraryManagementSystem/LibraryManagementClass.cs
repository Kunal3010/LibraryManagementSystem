using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace LibraryManagementSystem
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publication { get; set; }
        public bool IsIssued { get; set; }
    }

    public class Student
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public long Mobile { get; set; }
        public bool Issued { get; set; }
    }

    public class LibraryManagement : ILibraryManagementClass
    {
        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection con = new SqlConnection("Server=US-513K9S3; database=LibraryMS; Integrated Security=true");
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[bold red]An error occurred while connecting to the database: {ex.Message}[/]");
                throw;
            }
        }

        public bool loginUser(string User, string pass)
        {
            bool passed = false;
            SqlConnection con = GetConnection();
            

            string query = $"SELECT * FROM login WHERE Username = @Username AND Pass = @Password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", User);
            cmd.Parameters.AddWithValue("@Password", pass);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                passed = true;
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Invalid Credentials!! Type Again [/]");
                passed = false;
            }
            con.Close();
            return passed;
        }

        public int AddBookDetails(Book book)
        {
            SqlConnection con = GetConnection();
            string query = "INSERT INTO Books (Title, Author, Publication, IsIssued) VALUES (@Title, @Author, @Publication, @IsIssued); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, con);

            command.Parameters.AddWithValue("@Title", book.Title);
            command.Parameters.AddWithValue("@Author", book.Author);
            command.Parameters.AddWithValue("@Publication", book.Publication);
            command.Parameters.AddWithValue("@IsIssued", book.IsIssued);

            int newBookId = Convert.ToInt32(command.ExecuteScalar());
            AnsiConsole.MarkupLine($"[Bold Green]Book added with ID: {newBookId} [/]");
            con.Close();
            return newBookId;
        }

        public int EditBookDetails(Book book)
        {
            SqlConnection con = GetConnection();
            string query = "UPDATE Books SET Title = @Title, Author = @Author, Publication = @Publication, IsIssued = @IsIssued WHERE Id = @Id;";
            SqlCommand command = new SqlCommand(query, con);

            command.Parameters.AddWithValue("@Id", book.Id);
            command.Parameters.AddWithValue("@Title", book.Title);
            command.Parameters.AddWithValue("@Author", book.Author);
            command.Parameters.AddWithValue("@Publication", book.Publication);
            command.Parameters.AddWithValue("@IsIssued", book.IsIssued);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                AnsiConsole.MarkupLine($"[Bold Green]Book With ID: {book.Id} Updated Successfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[Bold Red]Book with ID {book.Id} not found.[/]");
            }
            con.Close();
            return rowsAffected;
        }

        public int DeleteBookDetails(int bookId)
        {
            SqlConnection con = GetConnection();
            string query = "DELETE FROM Books WHERE Id = @Id;";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@Id", bookId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                AnsiConsole.MarkupLine($"[Bold Green]Book With ID: {bookId} Deleted Successfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[Bold Red]Book With ID: {bookId} Not Found!!! [/]");
            }
            con.Close();
            return rowsAffected;
        }

        public int AddStudentDetails(Student student)
        {
            SqlConnection con = GetConnection();
            string query = "INSERT INTO Students (Name, Issued, MobileNo) VALUES (@Name, @Issued, @Mobile);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Issued", student.Issued);
            command.Parameters.AddWithValue("@Mobile", student.Mobile);
            int newrollnum = Convert.ToInt32(command.ExecuteScalar());

            AnsiConsole.MarkupLine($"[Bold Green]Student Added with Roll Number:[/] {newrollnum} [Bold Green] Successfully [/]");
            con.Close();
            return newrollnum;
        }

        public int EditStudentDetails(Student student)
        {
            SqlConnection con = GetConnection();
            string query = "UPDATE Students SET Name = @Name, MobileNo = @Mobile WHERE RollNo = @RollNo;";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@RollNo", student.RollNo);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Mobile", student.Mobile);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                AnsiConsole.MarkupLine($"[Bold Green]Student With Roll Number: {student.RollNo} Updated Successfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[Bold Red]Student With Roll Number: {student.RollNo} Not Found!!! [/]");
            }
            con.Close();
            return rowsAffected;
        }

        public int DeleteStudentDetails(int rollNo)
        {
            SqlConnection con = GetConnection();
            string query = "DELETE FROM Students WHERE RollNo = @RollNo;";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@RollNo", rollNo);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                AnsiConsole.MarkupLine($"[Bold Green]Student With Roll Number: {rollNo} Deleted Successfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[Bold Red]Student With Roll Number: {rollNo} Not Found!!! [/]");
            }
            con.Close();
            return rowsAffected;
        }

        public int IssueBookToStudent(int bookId, int rollNo)
        {
            SqlConnection con = GetConnection();

            // Check Book existence
            string check = "SELECT COUNT(*) FROM Books WHERE Id = @Id;";
            int bookCount;
            using (SqlCommand com2 = new SqlCommand(check, con))
            {
                com2.Parameters.AddWithValue("@Id", bookId);
                bookCount = Convert.ToInt32(com2.ExecuteScalar());
                if (bookCount == 0)
                {
                    AnsiConsole.MarkupLine($"[red]Book with ID {bookId} does not exist.[/]");
                    return bookCount;
                }
            }

            //check book availability
            bool isIssued;
            string check1 = "SELECT IsIssued FROM Books WHERE Id = @Id;";
            using (SqlCommand cmd = new SqlCommand(check1, con))
            {
                cmd.Parameters.AddWithValue("@Id", bookId);
                isIssued = Convert.ToBoolean(cmd.ExecuteScalar());
                if (isIssued)
                {
                    AnsiConsole.MarkupLine("[red]Book is already issued to a student.[/]");
                    return 0;
                }
            }

            // Check student existence
            string check2 = "SELECT COUNT(*) FROM Students WHERE RollNo = @RollNo;";
            int studentCount;
            using (SqlCommand com2 = new SqlCommand(check2, con))
            {
                com2.Parameters.AddWithValue("@RollNo", rollNo);
                studentCount = Convert.ToInt32(com2.ExecuteScalar());
                if (studentCount == 0)
                {
                    AnsiConsole.MarkupLine($"[red]Student with Roll No {rollNo} does not exist.[/]");
                    return studentCount;
                }
            }

            //check is student is already issued a book availability
            bool SIssued;
            string check3 = "SELECT Issued FROM Students WHERE RollNo = @rollnum;";
            using (SqlCommand cmd3 = new SqlCommand(check3, con))
            {
                cmd3.Parameters.AddWithValue("@rollnum", rollNo);
                SIssued = Convert.ToBoolean(cmd3.ExecuteScalar());
                if (SIssued)
                {
                    AnsiConsole.MarkupLine("[red]Student Has already issued a book[/]");
                    return 0;
                }
            }

            // Issue book
            string issueQuery = "INSERT INTO IssueBook (Roll_No, Book_ID, Issue_Date) VALUES (@StudentId, @BookId, @IssueDate)";
            using (SqlCommand issueCommand = new SqlCommand(issueQuery, con))
            {
                issueCommand.Parameters.AddWithValue("@StudentId", rollNo);
                issueCommand.Parameters.AddWithValue("@BookId", bookId);
                issueCommand.Parameters.AddWithValue("@IssueDate", DateTime.Today);

                issueCommand.ExecuteNonQuery();
            }
            string issueBookQuery = "UPDATE Books SET IsIssued = 1 WHERE Id = @Id; UPDATE Students SET Issued = 1 WHERE RollNo = @rollnum;";
            using (SqlCommand command = new SqlCommand(issueBookQuery, con))
            {
                command.Parameters.AddWithValue("@Id", bookId);
                command.Parameters.AddWithValue("@rollnum", rollNo);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    AnsiConsole.MarkupLine($"[green]Book with ID {bookId} issued to student with Roll No {rollNo}.[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]Book with ID {bookId} not found.[/]");
                }
                return rowsAffected;
            }
            con.Close();
        }

        public int ReturnBookFromStudent(int bookId, int rollNo)
        {
            using (SqlConnection con = GetConnection())
            {
                // Check if book is issued
                string checkBookIssuedQuery = "SELECT IsIssued FROM Books WHERE Id = @Id;";
                bool isIssued;
                using (SqlCommand command = new SqlCommand(checkBookIssuedQuery, con))
                {
                    command.Parameters.AddWithValue("@Id", bookId);
                    isIssued = Convert.ToBoolean(command.ExecuteScalar());
                }
                if (!isIssued)
                {
                    AnsiConsole.MarkupLine("[red]Book is not issued to any student.[/]");
                    return 0;
                }

                // Check student existence
                string checkStudentExistenceQuery = "SELECT COUNT(*) FROM Students WHERE RollNo = @RollNo;";
                int studentCount;
                using (SqlCommand command = new SqlCommand(checkStudentExistenceQuery, con))
                {
                    command.Parameters.AddWithValue("@RollNo", rollNo);
                    studentCount = Convert.ToInt32(command.ExecuteScalar());
                }

                if (studentCount == 0)
                {
                    AnsiConsole.MarkupLine($"[red]Student with Roll No {rollNo} does not exist.[/]");
                    return studentCount;
                }

                // Return book
                string returnQuery = "UPDATE IssueBook SET Return_Date = @ReturnDate WHERE Book_Id = @BookId AND Roll_No = @rollnum And Return_Date IS NULL";
                using (SqlCommand returnCommand = new SqlCommand(returnQuery, con))
                {
                    returnCommand.Parameters.AddWithValue("@ReturnDate", DateTime.Today);
                    returnCommand.Parameters.AddWithValue("@BookId", bookId);
                    returnCommand.Parameters.AddWithValue("@rollnum", rollNo);
                    returnCommand.ExecuteNonQuery();
                }

                string returnBookQuery = "UPDATE Books SET IsIssued = 0 WHERE Id = @Id;UPDATE Students SET Issued = 0 WHERE RollNo = @rollnum;";
                using (SqlCommand Command = new SqlCommand(returnBookQuery, con))
                {
                    Command.Parameters.AddWithValue("@Id", bookId);
                    Command.Parameters.AddWithValue("@RollNum", rollNo);
                    int rowsAffected = Command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        AnsiConsole.MarkupLine($"[green]Book with ID {bookId} returned by student with Roll No {rollNo}.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[red]Book with ID {bookId} not found.[/]");
                    }
                    return rowsAffected;
                }
            }
        }

        public int SearchBooksByAuthorOrPublication(string searchText)
        {
            SqlConnection con = GetConnection();

            string query = "SELECT Id, Title, Author, Publication, IsIssued FROM Books WHERE Author LIKE @SearchText OR Publication LIKE @SearchText;";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
            SqlDataReader reader = command.ExecuteReader();

            var table = new Table();
            table.AddColumn("Book ID");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("Publication");
            table.AddColumn("Is Issued");
            int records = 0;

            while (reader.Read())
            {
                var bookId = reader.GetInt32(0);
                var title = reader.GetString(1);
                var author = reader.GetString(2);
                var publication = reader.GetString(3);
                var isIssued = reader.GetBoolean(4);
                table.AddRow(bookId.ToString(), title, author, publication, isIssued.ToString());
                records++;
            }
            AnsiConsole.Render(table);
            con.Close();
            return records;
        }

        public int SearchStudentByRollNo(int rollNo)
        {
            SqlConnection con = GetConnection();
            string query = "SELECT RollNo, Name, Issued FROM Students WHERE RollNo = @RollNo;";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@RollNo", rollNo);
            SqlDataReader reader = command.ExecuteReader();

            var table = new Table();
            table.AddColumn("Roll No");
            table.AddColumn("Name");
            int records = 0;    

            if (reader.Read())
            {
                var studentRollNo = reader.GetInt32(0);
                var studentName = reader.GetString(1);
                table.AddRow(studentRollNo.ToString(), studentName);
                records++;
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Student Not Found!!! [/]");
                return 0;
            }
            AnsiConsole.Render(table);
            con.Close();
            return records;
        }

        public int GetIssuedBook()
        {
            SqlConnection con = GetConnection();
            string query = "SELECT s.RollNo , s.Name ,b.Title, b.Author, b.Publication FROM Students s INNER JOIN IssueBook ib ON s.RollNo = ib.Roll_No INNER JOIN Books b ON ib.Book_Id = b.Id WHERE  ib.Return_Date IS NULL;";
            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader reader = command.ExecuteReader();

            var table = new Table();
            table.AddColumn("Roll Number");
            table.AddColumn("Student Name");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("Publication");

            int recordCount = 0;
            while (reader.Read())
            {
                var rollnum = reader.GetInt32(0);
                var name = reader.GetString(1);
                var title = reader.GetString(2);
                var author = reader.GetString(3);
                var publication = reader.GetString(4);
                table.AddRow(rollnum.ToString(), name, title, author, publication);
                recordCount++;
            }
            AnsiConsole.Render(table);
            AnsiConsole.MarkupLine($"[yellow]Number of Student Issued the book: {recordCount}[/]");

            con.Close();
            return recordCount;
        }
    }
}
