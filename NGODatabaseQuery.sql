Create database dbNGO
use dbNGO


-- table for Admins who will control the admin side.
Create table tbl_Admins(
AdminID int primary key identity,
AdminFirstName varchar(50) not null,
AdminLastName varchar(50) not null,
AdminUsername varchar(50) not null,
AdminEmail varchar(50) not null,
AdminPassword varchar(50) not null 
)

ALTER TABLE tbl_Admins
ADD AdminImage varchar(250);

--table for volunteer types 
create table tbl_VolunteerTypes(
VolunteerTypeID int primary key identity,
VolunteerTypeName varchar(50) not null
)



--table for donation causes when user wants to donate, he should have to select what cause he wants to donate to.
Create table tbl_DonationCauses(
CauseID int primary key identity,
CauseName varchar(50) not null,
CauseDiscription varchar(50) not null,
CauseImage varchar(1000) not null,
AdminID int foreign key references tbl_Admins(AdminID)
)



-- table for associative NGO if a user wants to donate to any other ngo we have mention in our website
create table tbl_AssociativeNGO (
NgoID int primary key identity,
NgoName varchar(50) not null,
NgoEmail varchar(50) not null,
NgoLogo varchar(1000) not null,
NgoPhone varchar(50) not null,
NgoWebsite varchar(100) not null,
NgoDetails varchar(max) not null,
AdminID int foreign key references tbl_Admins(AdminID)
)

ALTER TABLE tbl_AssociativeNGO
ADD NGOStatus int ;




create table tbl_Users(
UserID int primary key identity,
UserFirstName varchar(50) not null,
UserLastName varchar(50) not null,
UserName varchar(50) not null,
UserGender varchar(50) not null,
UserEmail varchar(50) not null,
UserPassword varchar(50) not null,
UserConfirmPassword varchar(50) not null
)



create table tbl_Donations (
DonationID int primary key identity,
Username varchar(50) not null,
Email varchar(50) not null,
DonationAmount int not null,
DonationDateTime datetime,
CauseID int foreign key references tbl_DonationCauses(CauseID),
)

ALTER TABLE tbl_Donations ADD CardName varchar(50);
ALTER TABLE tbl_Donations ADD CardNumber varchar(50);
ALTER TABLE tbl_Donations ADD CardOwner varchar(50);
ALTER TABLE tbl_Donations ADD CExpirydate datetime;
ALTER TABLE tbl_Donations ADD CVCode varchar(50);


create table tbl_TeamMemberTitles (
TitleID int primary key identity,
TitleName varchar(50) not null,
AdminID int foreign key references tbl_Admins(AdminID)
)

 

create table tbl_TeamMembers(
MemberID int primary key identity,
MemberName varchar(50) not null,
MemberImage varchar(1000) not null,
MemberDescription varchar(500) not null,
TitleID int foreign key references tbl_TeamMemberTitles(TitleID),
AdminID int foreign key references tbl_Admins(AdminID)
)




create table tbl_Events(
EventID int primary key identity,
EventTitle varchar(100) not null,
EventDescription varchar(Max) not null,
EventDateTime datetime not null,
EventLocation varchar(50) not null,
EventImage varchar(1000) not null,
AdminID int foreign key references tbl_Admins(AdminID)
)

ALTER TABLE tbl_Events
ADD EventStatus int;

create table tbl_Volunteers(
VolunteerID int primary key identity,
VolunteerName varchar(50) not null,
VolunteerReason varchar(250) not null,
VolunteerEmail varchar(50) not null,
VolunteerTypeID int foreign key references tbl_VolunteerTypes(VolunteerTypeID)
)




create table tbl_Achivements (
AchiveID int primary key identity,
AchiveTitle varchar(100) not null,
AchiveImage varchar(1000) not null,
AchiveDescription varchar(2500) not null,
AchiveDateTime datetime not null,
AdminID int foreign key references tbl_Admins(AdminID)
)

ALTER TABLE tbl_Achivements
ADD AchiveStatus int;

create table tbl_Blogs (
BlogID int primary key identity,
BlogTitle varchar(100) not null,
BlogImage varchar(1000) not null,
BlogDescription varchar(2500) not null,
BlogDateTime datetime not null,
AdminID int foreign key references tbl_Admins(AdminID)
)

ALTER TABLE tbl_Blogs
ADD BlogStatus int;

create table tbl_AskQuesions (
QuesionID int primary key identity,
QuestionTitle varchar(100) not null,
QuestionDateTime datetime ,
UserID int foreign key references tbl_Users(UserID)
)


drop table tbl_QNAs
drop table tbl_contactus


create table tbl_contactus(
ContactID int primary key identity,
ContactName varchar(50) not null,
ContactEmail varchar(50) not null,
ContactSubject varchar(50) not null,
ContactMessage varchar(Max) not null,
UserID int foreign key references tbl_Users(UserID)
)


create table tbl_Gallaries (
Gid int primary key identity,
GallaryTitle varchar(100) not null,
GallaryImage varchar(1000) not null,
AdminID int Foreign key references tbl_Admins(AdminID)
)

ALTER TABLE tbl_Gallaries
ADD ImageStatus int;

create table tbl_Projects(
ProjectID int primary key identity,
ProjectTitle varchar(150) not null,
ProjectDescription varchar(max) not null,
ProjectImage varchar(1000) not null,
ProjectDateTime datetime not null,
MemberID int foreign key references tbl_TeamMembers(MemberID),
AdminID int Foreign key references tbl_Admins(AdminID)
)

ALTER TABLE tbl_Projects
ADD ProjectStatus int;

create table tbl_aboutus (
AboutID int primary key identity,
WhatWeDo varchar(max) not null,
CareerWithUs varchar(max) not null,
ReadAboutUS varchar(max) not null,
OurMission varchar(max) not null,
AdminID int Foreign key references tbl_Admins(AdminID)
)


create table tbl_invitation(
InviteID int primary key identity,
InviteMessage varchar(250) not null,
InviteToMail varchar(50) not null,
UserID int foreign key references tbl_Users(UserID)
)




