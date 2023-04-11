-- phpMyAdmin SQL Dump
-- version 4.9.7
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Oct 27, 2022 at 06:45 PM
-- Server version: 5.7.40
-- PHP Version: 7.4.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";
/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sshiv021_TestDb`
--

CREATE TABLE `Roles`
(
  `role_id` int NOT NULL AUTO_INCREMENT,
  `role_name` varchar(20) DEFAULT 'Member',
  `desc` varchar(255) DEFAULT NULL,
  `role_value` int(20) DEFAULT '10',
  PRIMARY KEY(`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `Users`
--

CREATE TABLE `Users`
(
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(20) NOT NULL UNIQUE,
  `password` varchar(255) NOT NULL,
  `role_id` int NOT NULL DEFAULT '4',
  PRIMARY KEY(`user_id`),
  CONSTRAINT `FK_RolesUsers` FOREIGN KEY (`role_id`) REFERENCES `Roles` (`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


-- The TIMESTAMP value has a range from '1970-01-01 00:00:01' UTC to '2038-01-19 03:14:07' UTC.
CREATE TABLE `Messages`
(
  `MessageID` int(11) NOT NULL AUTO_INCREMENT,
  `Message` varchar(255) DEFAULT NULL,
  `Timestamp` TIMESTAMP DEFAULT CURRENT_TIMESTAMP 
  ON UPDATE CURRENT_TIMESTAMP,
  `user_id` int(11) NOT NULL,
  PRIMARY KEY(`MessageID`),
  FOREIGN KEY (`user_id`) REFERENCES `Users` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `Users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=101;

INSERT INTO `Roles` (`role_id`, `role_name`, `desc`, `role_value`) VALUES
('1', 'Root', 'The user with all the privilidges', '100'),
('2', 'Administrator', 'The user can modify all but not the root and admin', '50'),
('3', 'Moderator', 'Can only modify members', '25'),
('4', 'Member', 'The user with the least privilidges', '10');

INSERT INTO `Users` (`user_id`, `username`, `password`, `role_id`) VALUES
('100', 'shivansh21', '$2y$10$zF.q9/B1fUvgaP6fevMa2OLiQg9RyN/xtiMKrujUh1uT8JuWMcfqS', '1'),
('101', 'admin', '$2y$10$FYceOugEf5t8NmzGOzAOHuXN7XB/SsYq//NmuM3aHsBiWK6ZvR/Fq', '2'),
('102', 'iron', '$2y$10$lK4Rmqd.oshX7TNlKGVt2OONUlg6abnUQ7pjFTKFlLniJBH3GdINa', '3'),
('103', 'deep', '$2y$10$1xIcw9rBwnNtLY4mSR61ouThAwUgf9CYjY4ArzwRb5Jznn5uC5wo.', '4');

INSERT INTO `Messages` (`MessageID`, `Message`, `user_id`) VALUES
("1", "Don't try to mess with the system -- Root", "100"),
("2", "I am here to fix anyone who tries to mess with the system -- admin", "101"),
("3", "I am here to mess with the system and I can  -- moderator", "102"),
("4", "I am here to mess with the system but I cannot -- member", "103");

COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

