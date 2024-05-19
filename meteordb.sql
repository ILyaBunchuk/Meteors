CREATE DATABASE `meteordb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

CREATE TABLE `meteors` (
  `Id` int NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `NameType` varchar(45) DEFAULT NULL,
  `Recclass` varchar(45) DEFAULT NULL,
  `Mass` double DEFAULT NULL,
  `Fall` varchar(45) DEFAULT NULL,
  `Year` datetime DEFAULT NULL,
  `Reclat` double DEFAULT NULL,
  `Reclong` double DEFAULT NULL,
  `GeolocationJson` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FILTER` (`Year`,`Recclass`,`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
