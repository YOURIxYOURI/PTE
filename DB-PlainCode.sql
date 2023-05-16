DROP DATABASE IF EXISTS PTE;
CREATE DATABASE PTE;
USE PTE;

CREATE TABLE Admins(
	ID int PRIMARY KEY AUTO_INCREMENT,
    FirstName varchar(60),
    LastName varchar(70),
    Email varchar(90),
    Password text,
    IfMain boolean,
    Salt text
);

CREATE TABLE Users(
	ID int PRIMARY KEY AUTO_INCREMENT,
    FirstName varchar(60),
    LastName varchar(70),
    BirthDate varchar(11),
    BirthPlace varchar(120),
    Phone varchar(13),
    Email varchar(90),
    Adress varchar(150),
    Grade varchar(60),
    SchoolName varchar(255),
  	GraduationYear varchar(4),
    WorkPlace varchar(255),
    Password text,
    Confirmed boolean,
    Salt text
);

CREATE TABLE Benefits(
	ID int PRIMARY KEY AUTO_INCREMENT,
    Name varchar(255),
    Description text,
    QrKey text,
    EndDate varchar(11)
);

CREATE TABLE BenefitsToUser(
	ID int PRIMARY KEY AUTO_INCREMENT,
    BenefitID int,
    UserID int
);

CREATE INDEX idxB ON BenefitsToUser(BenefitID);
CREATE INDEX idxU ON BenefitsToUser(UserID);

ALTER TABLE BenefitsToUser ADD FOREIGN KEY (BenefitID) REFERENCES Benefits(ID);
ALTER TABLE BenefitsToUser ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

INSERT INTO Admins VALUES(null,'imie','nazwisko','admin@PTE.pl','7BDE1E20DB6026B36756C51B150513F622BA63F10BB7DAB96BA607440AC85CB186DEE7CA6CB435CF1D10D9509AC6ACE9C99A248E971015829FEABD60378EE39C1',1,'2hDeo9lM5cmQJBguzHhYabjIn3mEHv/q2mU649U5IX5Eh5bG/gkrGvguEQ3QQswoVNCMmcv8KIY1/6Qd6eyPhg==');
#Qwerty123


