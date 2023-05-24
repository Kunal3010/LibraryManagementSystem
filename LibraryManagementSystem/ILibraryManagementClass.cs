using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public interface ILibraryManagementClass
    {
        bool loginUser(string user,string pass);
        int AddBookDetails(Book book);
        int EditBookDetails(Book book);
        int DeleteBookDetails(int bookId);
        int AddStudentDetails(Student student);
        int EditStudentDetails(Student student);
        int DeleteStudentDetails(int rollNo);
        int IssueBookToStudent(int bookId, int rollNo);
        int ReturnBookFromStudent(int bookId, int rollNo);
        int SearchBooksByAuthorOrPublication(string searchText);
        int SearchStudentByRollNo(int rollNo);
        int GetIssuedBook();

    }
}
