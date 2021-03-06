use master
drop database if exists PRS
create database PRS
use PRS 

Create table [User] (
	Id int not null primary key identity (1,1),
	UserName varchar(20) not null,
	Password varchar(10) not null,
	FirstName varchar(20) not null,
	LastName varchar(20) not null,
	Phone varchar(12) not null,
	Email varchar(75) not null,
	IsReviewer bit not null default 0, --the user is not necessarily a reviewer so default is 0,-- 
	--but if they are then we would add a value in our insert--
	IsAdmin bit not null default 0, --same as above--
	Active bit not null default 1,
	DateCreated datetime not null default getdate(), 
	DateUpdated datetime,
	UpdatedByUser int foreign key references [User](id)
)

-- to support unique UserNames
create unique index IX_Username
on [User](Username)

--
Insert into [User] 
	(Username, Password, FirstName, LastName, Phone, Email, IsReviewer, IsAdmin) values
	('gpdoud', 'password', 'Greg', 'Doud', '513-703-7315', 'gdoud@maxtrain.com', 1, 1)


Create table Vendor (
	Id int not null primary key identity (1,1),
	Code varchar(10) not null,
	Name varchar(255) not null,
	Address varchar (255) not null,
	City varchar (255) not null,
	State varchar (2) not null,
	Zip varchar (5) not null,
	Phone varchar (12) not null,
	Email varchar (100) not null,
	IsPreAppoved bit not null default 0,
	Active bit not null default 1,
	DateCreated datetime not null default getdate(), 
	DateUpdated datetime,
	UpdatedByUser int foreign key references [User](id)
	)
go

create unique index IUX_Code
on Vendor(Code)

go

Insert into Vendor 
	(Code, Name, Address, City, State, Zip, Phone, Email, IsPreAppoved) values
	('SAM', 'Sam''s Club', '1234 Main Street', 'Cincinnati', 'OH', '45202',
	'513-777-0022', 'info@samsclub.com', 1)

Insert into Vendor 
	(Code, Name, Address, City, State, Zip, Phone, Email, IsPreAppoved) values
	('WAL', 'WalMart', '202 South Street', 'Cincinnati', 'OH', '45202',
	'513-222-5588', 'main@walmart.com', 1)

Insert into Vendor 
	(Code, Name, Address, City, State, Zip, Phone, Email, IsPreAppoved) values
	('AMZ', 'Amazon', '111 Third Street', 'Cincinnati', 'OH', '45202',
	'513-581-8968', 'info@amazon.com', 1)
go

Create table Product (
	Id int not null primary key identity (1,1),
	VendorID int foreign key references Vendor(id),
	PartNumber varchar(50) not null,
	Name varchar(150) not null,
	Price Decimal(10,2) not null,
	Unit varchar(255) not null,
	PhotoPath varchar(255),
	Active bit not null default 1,
	DateCreated datetime not null default getdate(), 
	DateUpdated datetime,
	UpdatedByUser int foreign key references [User](id)
	)
go

Insert into Product 
	(PartNumber, Name, Price, Unit) values
	('OFCYU1', 'Office Desk', '599.00', '1')

Insert into Product 
	(PartNumber, Name, Price, Unit) values
	('TO9034', 'Printer', '148.00', '2')

Insert into Product 
	(PartNumber, Name, Price, Unit) values
	('PDHSI45', 'Office Chair', '299.00', '1')

Insert into Product 
	(PartNumber, Name, Price, Unit) values
	('CAWE532', 'Desk Organizer', '25.50', '1')

Insert into Product 
	(PartNumber, Name, Price, Unit) values
	('WER870', 'File Cabinet', '240.00', '1')

Insert into Product 
	(PartNumber, Name, Price, Unit) values
	('PGIDNZA2', 'Television', '1542.00', '1')

go


