using Moq;
using LibraryManagementSystem;
using System.Xml.Linq;
using System.Reflection;

namespace LibraryManagementSystem.Tests
{
    public class Tests
    { 
        [Test]
        public void AddBookDetail_WhenRecordInserted_Returnsbookid()
        {
            ////Arrange
            var repo = new Mock<ILibraryManagementClass>();
            Book book = new Book
            {
                Title = "testtitle1",
                Author = "testauthor",
                Publication = "testpublisher",
                IsIssued = false
            };
            repo.Setup(x => x.AddBookDetails(book)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.AddBookDetail(book);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void EditBookDetail_whencalled_ReturnRowAffected()
        {
            ////Arrange
            var repo = new Mock<ILibraryManagementClass>();
            Book book = new Book
            {
                Title = "Updatedtitle1",
                Author = "Updatedauthor",
                Publication = "Updatedpublisher",
                IsIssued = false
            };
            repo.Setup(x => x.AddBookDetails(book)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.AddBookDetail(book);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void DeleteBookDetail_whencalled_ReturnRowAffected()
        {
            ////Arrange
            var repo = new Mock<ILibraryManagementClass>();
            repo.Setup(x => x.DeleteBookDetails(1)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.DeleteBookDetails(1);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddStudentDetaols_whencalled_ReturnRowAffected()
        {
            ////Arrange
            var repo = new Mock<ILibraryManagementClass>();
            Student student = new Student
            {
                Mobile = 9599633913,
                Name = "kunal",
                Issued = false
            };
            repo.Setup(x => x.AddStudentDetails(student)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.AddStudentDetails(student);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void EditStudentDetaols_whencalled_ReturnRowAffected()
        {
            ////Arrange
            var repo = new Mock<ILibraryManagementClass>();
            Student student = new Student
            {
                Mobile = 9818690181,
                Name = "Vikas",
                Issued = false
            };
            repo.Setup(x => x.EditStudentDetails(student)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.EditStudentDetails(student);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void DeleteStudentDetail_whencalled_ReturnRowAffected()
        {
            ////Arrange
            var repo = new Mock<ILibraryManagementClass>();
            repo.Setup(x => x.DeleteStudentDetails(1)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.DeleteStudentDetails(1);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void IssueBookToStudent_WhenCalled_ReturnsNoOfRecord()
        {
            ///Arrange
            var repo = new Mock<ILibraryManagementClass>();
            repo.Setup(x => x.IssueBookToStudent(1,12)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.IssueBookToStudent(1, 12);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void ReturnBookFromStudent_WhenCalled_ReturnsNoOfRecord()
        {
            ///Arrange
            var repo = new Mock<ILibraryManagementClass>();
            repo.Setup(x => x.ReturnBookFromStudent(2,4)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.ReturnBookFromStudent(2,4);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SearchBooksByAuthorOrPublication_WhenCalled_ReturnsNoOBooks()
        {
            ///Arrange
            var repo = new Mock<ILibraryManagementClass>();
            repo.Setup(x => x.SearchBooksByAuthorOrPublication("Test String")).Returns(5);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.SearchBooksByAuthorOrPublication("Test String");
            ////Assert
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void SearchStudentByRollNo_WhenCalled_ReturnsNoOfStudents()
        {
            ///Arrange
            var repo = new Mock<ILibraryManagementClass>();
            repo.Setup(x => x.SearchStudentByRollNo(4)).Returns(1);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.SearchStudentByRollNo(4);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void GetIssuedBook_WhenCalled_ReturnsCountofStudentWhoIssuedABook()
        {
            ///Arrange
            var repo = new Mock<ILibraryManagementClass>();
            repo.Setup(x => x.GetIssuedBook()).Returns(6);
            var service = new LibraryService(repo.Object);
            ////Act
            var result = service.GetIssuedBook();
            ////Assert
            Assert.That(result, Is.EqualTo(6));
        }

    }
}