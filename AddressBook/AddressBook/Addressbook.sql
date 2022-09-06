----Create database----
create database AddressBookAdo


----Create Table-----
create table ContactDetails(
Id int Primary Key Identity (1,1),
FirstName VarChar (200),
LastName varchar(150),
Address varchar(250),
City varchar(25),
State varchar(60),
ZipCode int,
PhoneNumber Varchar(10),
Email varchar(150)
);

-----Insert procedure------
CREATE PROCEDURE spAddNewPersons
(
@FirstName VarChar (200),
@LastName varchar(150),
@Address varchar(250),
@City varchar(25),
@State varchar(60),
@ZipCode int,
@PhoneNumber Varchar(10),
@Email varchar(150)
)
as
begin
INSERT INTO ContactDetails(FirstName, LastName, Address, City, State, ZipCode, PhoneNumber, Email)
	VALUES (@FirstName, @LastName, @Address, @City, @State, @ZipCode, @PhoneNumber, @Email)
end


select * from ContactDetails