create database LibraryMS;

use LibraryMS;

create table Books
(
Id int identity primary key,
Title varchar(50) not null,
Author Varchar(50) not null,
Publication varchar(50) not null,
IsIssued Bit not null default 0
);

create table Students
(
RollNo int identity primary key,
Name varchar(30) not null,
MobileNo bigint not null,
Issued Bit not null default 0
);

create table login
(
Username varchar(30) primary key,
Pass varchar(30) not null
);

create table IssueBook
(
IssueId int identity primary key,
Roll_No int references Students(RollNo),
Book_Id int references Books(Id),
Issue_Date date,
Return_Date Date
);

INSERT INTO login (Username, Pass) VALUES ('Kunal3010', 'Qwerty123');

select * from login;
select * from Books;
select * from Students;
select * from  IssueBook;
