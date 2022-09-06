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



----Select procedure----
CREATE PROCEDURE spViewContacts
as
begin
select * from ContactDetails
end

----Update procedure---
create procedure SPUpdateDetails
(
@Id int,
@PhoneNumber varchar(10)
)
as
begin
UPDATE ContactDetails set PhoneNumber=@PhoneNumber where Id = @Id
end

----Delete procedure---
create procedure spDeletePersonById
(
@Id int
)
as
begin
Delete from ContactDetails where Id=@Id 
end


--- Retrieve Data using City name-----
CREATE PROCEDURE spViewContactsUsingCityName
(
@City varchar(25),
@State varchar(60)
)
as
begin 
select * from ContactDetails where City = @City and State = @State;
end



