using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryManagementClass _libraryManagementClass;

        public LibraryService(ILibraryManagementClass libraryManagementClass)
        {
            _libraryManagementClass = libraryManagementClass;
        }
        public bool loginUser(string user,string pass)
        {
            return _libraryManagementClass.loginUser(user,pass); 
        }
        public int AddBookDetail(Book book)
        {
            return _libraryManagementClass.AddBookDetails(book); 
        }
        public int EditBookDetails(Book book)
        {
            return _libraryManagementClass.EditBookDetails(book);   
        }
        public int DeleteBookDetails(int bookId)
        {
            return _libraryManagementClass.DeleteBookDetails(bookId);   
        }
        public int AddStudentDetails(Student student)
        {
            return _libraryManagementClass.AddStudentDetails(student);  
        }
        public int EditStudentDetails(Student student)
        {
            return _libraryManagementClass.EditStudentDetails(student); 
        }
        public int DeleteStudentDetails(int rollNo)
        {
            return _libraryManagementClass.DeleteStudentDetails(rollNo);    
        }
        public int IssueBookToStudent(int bookId, int rollNo)
        {
            return _libraryManagementClass.IssueBookToStudent(bookId, rollNo);
        }
        public int ReturnBookFromStudent(int bookId, int rollNo)
        {
            return _libraryManagementClass.ReturnBookFromStudent(bookId, rollNo);
        }
        public int SearchBooksByAuthorOrPublication(string searchText)
        {
            return _libraryManagementClass.SearchBooksByAuthorOrPublication(searchText);
        }
        public int SearchStudentByRollNo(int rollNo)
        {
            return _libraryManagementClass.SearchStudentByRollNo(rollNo);
        }
        public int GetIssuedBook()
        {
            return _libraryManagementClass.GetIssuedBook();
        }
    }
}
