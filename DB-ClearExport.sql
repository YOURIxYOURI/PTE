-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 28, 2023 at 03:10 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pte`
--

-- --------------------------------------------------------

--
-- Table structure for table `admins`
--

CREATE TABLE `admins` (
  `ID` int(11) NOT NULL,
  `FirstName` varchar(60) DEFAULT NULL,
  `LastName` varchar(70) DEFAULT NULL,
  `Email` varchar(90) DEFAULT NULL,
  `Password` text DEFAULT NULL,
  `IfMain` tinyint(1) DEFAULT NULL,
  `Salt` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `admins`
--

INSERT INTO `admins` (`ID`, `FirstName`, `LastName`, `Email`, `Password`, `IfMain`, `Salt`) VALUES
(1, 'imie', 'nazwisko', 'admin@PTE.pl', '7FD5FBB7F70939974C156238358FD0AF64C04EADC843321DDC5599F7AD2264F3314DA14CFC10A4DCA3FA300A8ADFFB1AC358A1D05DB191B4F52C3104A5AB265B', 1, 'B4A9FFF46D1E91061147A298850FED9B872E69FABEE7E722174990D2738B63412094E1FF5E66F6667CBBB9AE3C27F463D9C9CD54D5C9A19670A9530679CBE971');

-- --------------------------------------------------------

--
-- Table structure for table `benefits`
--

CREATE TABLE `benefits` (
  `ID` int(11) NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Description` text DEFAULT NULL,
  `QrKey` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `benefitstouser`
--

CREATE TABLE `benefitstouser` (
  `ID` int(11) NOT NULL,
  `BenefitID` int(11) DEFAULT NULL,
  `UserID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `FirstName` varchar(60) DEFAULT NULL,
  `LastName` varchar(70) DEFAULT NULL,
  `BirthDate` date DEFAULT NULL,
  `BirthPlace` varchar(120) DEFAULT NULL,
  `Phone` varchar(13) DEFAULT NULL,
  `Email` varchar(90) DEFAULT NULL,
  `Adress` varchar(150) DEFAULT NULL,
  `Grade` varchar(60) DEFAULT NULL,
  `SchoolName` varchar(255) DEFAULT NULL,
  `GraduationYear` varchar(4) DEFAULT NULL,
  `WorkPlace` varchar(255) DEFAULT NULL,
  `Password` text DEFAULT NULL,
  `Remember` tinyint(1) DEFAULT NULL,
  `Salt` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admins`
--
ALTER TABLE `admins`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `benefits`
--
ALTER TABLE `benefits`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `benefitstouser`
--
ALTER TABLE `benefitstouser`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `idxB` (`BenefitID`),
  ADD KEY `idxU` (`UserID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admins`
--
ALTER TABLE `admins`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `benefits`
--
ALTER TABLE `benefits`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `benefitstouser`
--
ALTER TABLE `benefitstouser`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `benefitstouser`
--
ALTER TABLE `benefitstouser`
  ADD CONSTRAINT `benefitstouser_ibfk_1` FOREIGN KEY (`BenefitID`) REFERENCES `benefits` (`ID`),
  ADD CONSTRAINT `benefitstouser_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
