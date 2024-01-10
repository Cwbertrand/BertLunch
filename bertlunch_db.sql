-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               11.3.0-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.1.0.6537
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for bertlunch
DROP DATABASE IF EXISTS `bertlunch`;
CREATE DATABASE IF NOT EXISTS `bertlunch` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `bertlunch`;

-- Dumping structure for table bertlunch.aspnetroleclaims
DROP TABLE IF EXISTS `aspnetroleclaims`;
CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.aspnetroleclaims: ~0 rows (approximately)

-- Dumping structure for table bertlunch.aspnetroles
DROP TABLE IF EXISTS `aspnetroles`;
CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.aspnetroles: ~2 rows (approximately)
INSERT INTO `aspnetroles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
	('c5ef91a9-8cfe-4e42-b7d2-ebb0ba745b0a', 'Admin', 'Admin', NULL),
	('c5ef91a9-8cfe-4e42-b7d2-ebb0ba745b0b', 'Gestionaire_cantine', 'Gestionaire_cantine', NULL);

-- Dumping structure for table bertlunch.aspnetuserclaims
DROP TABLE IF EXISTS `aspnetuserclaims`;
CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.aspnetuserclaims: ~0 rows (approximately)

-- Dumping structure for table bertlunch.aspnetuserlogins
DROP TABLE IF EXISTS `aspnetuserlogins`;
CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.aspnetuserlogins: ~0 rows (approximately)

-- Dumping structure for table bertlunch.aspnetuserroles
DROP TABLE IF EXISTS `aspnetuserroles`;
CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.aspnetuserroles: ~1 rows (approximately)
INSERT INTO `aspnetuserroles` (`UserId`, `RoleId`) VALUES
	('2ded877f-02d4-43bc-a3dd-82871fe2c778', 'c5ef91a9-8cfe-4e42-b7d2-ebb0ba745b0a');

-- Dumping structure for table bertlunch.aspnetusers
DROP TABLE IF EXISTS `aspnetusers`;
CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(255) NOT NULL,
  `Psuedo` longtext DEFAULT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext DEFAULT NULL,
  `SecurityStamp` longtext DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.aspnetusers: ~2 rows (approximately)
INSERT INTO `aspnetusers` (`Id`, `Psuedo`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
	('2ded877f-02d4-43bc-a3dd-82871fe2c778', 'Bert', 'bob@test.com', 'BOB@TEST.COM', 'bob@test.com', 'BOB@TEST.COM', 1, 'AQAAAAIAAYagAAAAEEF1oSCXjPrUhon03n8Qxws0YGi8Mh9xE0tpdXqV0YjAp7b/MSmnQ0w6TbUXw51Xvw==', 'MCJ2PYXLQ2W2LQHEQAO7N7GQGTIHMLOR', 'cd62474e-56b4-4dda-ac75-1f1196669855', NULL, 0, 0, NULL, 1, 0),
	('80363c06-7004-40f2-833d-a05f8b85fda2', 'Bertrand', 'cwbertrand8@gmail.com', 'CWBERTRAND8@GMAIL.COM', 'cwbertrand8@gmail.com', 'CWBERTRAND8@GMAIL.COM', 1, 'AQAAAAIAAYagAAAAEDvscghUNouVgrU8Bw5fu2XmwliDs4dJTHDkb1TSibOrPdBikhGNVD7qEi1ZvVT8qg==', 'QSIHFVRQL2436NC5TTGJFSH7PLZXNYHX', '7b5dae7d-96cb-4b30-8a9d-306b29b43ca1', NULL, 0, 0, NULL, 1, 0);

-- Dumping structure for table bertlunch.aspnetusertokens
DROP TABLE IF EXISTS `aspnetusertokens`;
CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Value` longtext DEFAULT NULL,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.aspnetusertokens: ~0 rows (approximately)

-- Dumping structure for table bertlunch.category
DROP TABLE IF EXISTS `category`;
CREATE TABLE IF NOT EXISTS `category` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Label` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.category: ~6 rows (approximately)
INSERT INTO `category` (`Id`, `Label`) VALUES
	(5, 'La bière'),
	(6, 'Le jus'),
	(7, 'Le vin'),
	(8, 'Le poulet'),
	(9, 'Le bœuf'),
	(10, 'Végétalien');

-- Dumping structure for table bertlunch.menucategories
DROP TABLE IF EXISTS `menucategories`;
CREATE TABLE IF NOT EXISTS `menucategories` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.menucategories: ~5 rows (approximately)
INSERT INTO `menucategories` (`Id`, `Name`) VALUES
	(3, 'Les assiettes'),
	(4, 'Les sandwiches'),
	(5, 'Les boissons'),
	(6, 'Burger'),
	(7, 'Dessert');

-- Dumping structure for table bertlunch.menuitem
DROP TABLE IF EXISTS `menuitem`;
CREATE TABLE IF NOT EXISTS `menuitem` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryId` int(11) DEFAULT NULL,
  `MenuCategoryId` int(11) DEFAULT NULL,
  `MenuName` longtext NOT NULL,
  `Ingredient` longtext NOT NULL,
  `Description` longtext NOT NULL,
  `Price` float NOT NULL DEFAULT 0,
  `Image` longtext DEFAULT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `EndedAt` datetime(6) NOT NULL,
  `IsWeek` tinyint(1) NOT NULL,
  `IsAvailable` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_MenuItem_CategoryId` (`CategoryId`),
  KEY `FK_MenuItem_MenuCategories_MenuCategoryId` (`MenuCategoryId`),
  CONSTRAINT `FK_MenuItem_Category_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`Id`) ON DELETE SET NULL,
  CONSTRAINT `FK_MenuItem_MenuCategories_MenuCategoryId` FOREIGN KEY (`MenuCategoryId`) REFERENCES `menucategories` (`Id`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.menuitem: ~24 rows (approximately)
INSERT INTO `menuitem` (`Id`, `CategoryId`, `MenuCategoryId`, `MenuName`, `Ingredient`, `Description`, `Price`, `Image`, `CreatedAt`, `EndedAt`, `IsWeek`, `IsAvailable`) VALUES
	(6, 9, 6, 'Smashburger', 'Bun moelleux et léger à base de pommes de terre, steak haché façon bouchère de 150g, salade, oignons, cornichons, cheddar rouge, sauce du smash. Accompagné de frites maison et de salade', 'A hot skillet and thin layer of burger meat create a rich combination of sizzle, steam, and smoke that cooks the meat quickly but doesn’t dry out, as it browns swiftly in butter to create a caramelized flavor layer. Because it’s smashed almost immediately, and cooks so fast, the taste is much different to dry, overcooked patties that we associate with being “well done.”', 12.5, '/img/menuImages/3a0df39b-6913-4ea8-8bcb-21d5aa2e10e1_vue-rapprochee-delicieux-sandwich-frites-plateau-couleur-sombre-surface-noire.jpg', '0001-01-01 00:00:00.000000', '2023-03-11 23:02:00.000000', 1, 1),
	(8, 8, 6, 'Crispy Chicken Burger', 'Burger élaboré avec une délicieuse escalope de poulet panée croustillante à souhait... (Bun moelleux et léger à base de pommes de terre, coleslaw, oignons frits, sauce cajun, roquette, cheddar) Accompagné de frites maison et de salade', 'A timeless favorite featuring a golden, crispy chicken breast, topped with fresh lettuce, ripe tomato, crunchy pickles, and creamy mayo, all nestled in a soft brioche bun.', 10.5, '/img/menuImages/170dd904-51c3-4c95-b0e5-c4740f0abc59_burger-poulet-juteux-laitue-fraiche-frites-croustillantes-planche-bois.jpg', '0001-01-01 00:00:00.000000', '2023-03-13 04:04:00.000000', 1, 1),
	(19, 8, 6, 'Spicy Buffalo', 'Buffalo chicken, blue cheese, celery slaw, spicy mayo, potato roll', 'A fiery delight with juicy buffalo-style chicken, tangy blue cheese, crisp celery slaw, and spicy mayo, served on a classic potato roll for a zesty kick.', 9.99, '/img/menuImages/c71c9e49-d9c2-4bc1-b349-58e2fe32edf1_chicken-burger-bun-laitue-tomate-meaf-cheese-frites-mayonnaise-ketchup-side-view.jpg', '0001-01-01 00:00:00.000000', '2024-02-20 01:01:00.000000', 1, 1),
	(20, 8, 6, 'BBQ Ranch', ' Grilled chicken, cheddar cheese, bacon, BBQ sauce, ranch dressing, onion rings, sesame bun', 'A mouth-watering combination of grilled chicken, melted cheddar, smoky bacon, and BBQ sauce, topped with ranch dressing and crispy onion rings, all on a sesame bun.', 11, '/img/menuImages/6ae19489-207c-4f79-99a1-3349e9f943ae_hamburger-sandwich-hamburgers-juteux-tomate-chou-rouge.jpg', '2023-12-30 08:52:43.989537', '2024-02-02 04:04:00.000000', 1, 1),
	(21, 9, 6, 'American Cheeseburger', 'Beef patty, American cheese, lettuce, tomato, onions, pickles, ketchup, mustard, sesame seed bun', 'An all-American classic with a juicy beef patty, melted American cheese, crisp lettuce, fresh tomato, onions, pickles, ketchup, and mustard, all in a toasted sesame seed bun.', 10, '/img/menuImages/0746914e-22f2-4209-b38c-4764f8794378_burger-boeuf-viande-laitue-fromage-tomate-frites-vue-laterale.jpg', '2023-12-30 08:55:30.265591', '2024-01-01 04:02:00.000000', 1, 1),
	(22, 9, 6, 'Smoky BBQ Bacon', 'Angus beef patty, smoked bacon, cheddar cheese, BBQ sauce, onion rings, lettuce, brioche bun', 'A smoky sensation featuring an Angus beef patty, crispy bacon, sharp cheddar cheese, tangy BBQ sauce, golden onion rings, and lettuce, served in a soft brioche bun.', 11, '/img/menuImages/330da6be-aba3-4924-809d-0ee9acd9f29f_gros-sandwich-hamburger-burger-boeuf-juteux-fromage-tomate-oignon-rouge-table-bois.jpg', '2023-12-30 08:57:26.544803', '2024-02-02 03:02:00.000000', 1, 1),
	(23, 10, 6, 'Vegan Delight Burger', 'Vegan patty (pea protein), vegan cheese, lettuce, tomato, vegan mayo, pickles, whole grain bun', 'A plant-based classic featuring a hearty pea protein patty, dairy-free cheese, crisp lettuce, ripe tomato, creamy vegan mayo, and tangy pickles, all nestled in a wholesome whole grain bun.', 12.5, '/img/menuImages/f6575502-782e-4d91-9ec0-69ff1bb26809_vue-avant-du-burger-vegetarien-assiette.jpg', '0001-01-01 00:00:00.000000', '2024-03-02 04:02:00.000000', 1, 1),
	(24, 10, 6, 'Spicy Black Bean', 'Black bean patty, avocado, jalapeños, vegan chipotle mayo, red onion, tomato, lettuce, ciabatta bun', 'A zesty choice with a flavorful black bean patty, creamy avocado, fiery jalapeños, smoky vegan chipotle mayo, red onion, tomato, and lettuce, served on a rustic ciabatta bun.', 11, '/img/menuImages/dc1181b1-d9a9-44cb-ba83-da8b5b16ef40_vue-face-burger-vegetarien-comptoir-tomates.jpg', '2023-12-30 09:01:23.183231', '2024-02-01 04:05:00.000000', 1, 1),
	(25, 8, 4, 'Sandwich Club au Poulet', ' Blanc de poulet grillé, avocat, bacon, laitue, tomate, mayo, pain multigrain grillé', 'Un club sandwich savoureux avec du blanc de poulet grillé, de l\'avocat crémeux, du bacon croustillant, de la laitue fraîche et de la tomate, garni de mayo sur du pain multigrain grillé.', 6, '/img/menuImages/9c16b0ae-b9f4-44d7-95d7-94d1c2b7adb6_vue-laterale-sandwich-pain-blanc-viande-grillee-escalope-fromage-laitue-frites-mayo-ketchup-planchejpg.jpg', '2023-12-30 09:08:01.049660', '2024-02-02 04:03:00.000000', 1, 1),
	(26, 8, 4, 'Wrap au Poulet Buffalo Épicé', ' Lanières de poulet buffalo, fromage bleu, laitue, tomate, tortilla de farine\r\n', 'Un wrap piquant garni de lanières de poulet buffalo épicées, de fromage bleu relevé, de laitue croquante et de tomate juteuse, enveloppé dans une tortilla de farine douce.', 5, '/img/menuImages/cab0d17d-6c18-47e9-828d-c0fcf3cce2a2_sandwich-au-poulet-laitue-fromage-oignon-tomate-pomme-terre-maison-vue-laterale.jpg', '2023-12-30 09:09:40.354207', '2024-02-02 23:03:00.000000', 1, 1),
	(27, 8, 4, 'Panini au Poulet', 'Tranches de poulet, sauce pesto, fromage mozzarella, poivrons rouges grillés, pain ciabatta\r\n', 'Un panini alléchant avec des tranches de poulet, de la sauce pesto aromatique, de la mozzarella fondue et des poivrons rouges grillés sur du pain ciabatta grillé.', 6, '/img/menuImages/de5d9f8c-f00b-4423-8bf6-3d8d7b8d9991_gros-sandwich-brochette-poulet-laitue.jpg', '2023-12-30 09:10:41.692959', '2024-02-03 04:04:00.000000', 1, 1),
	(28, 9, 4, 'Sandwich Classique au Rosbif', 'Rosbif tranché finement, fromage cheddar, sauce raifort, laitue, tomate, oignon, pain de seigle', 'Un sandwich classique composé de rosbif finement tranché, de fromage cheddar affiné, de sauce raifort piquante, de laitue, de tomate et d\'oignon sur du pain de seigle frais.', 6, '/img/menuImages/56895b2f-538f-4f32-8e76-0cfdb5168080_sandwich-au-pain-tandir-du-fromage-blanc-tomates-laitue-interieur.jpg', '2023-12-30 09:12:00.423484', '2024-02-02 04:04:00.000000', 1, 1),
	(29, 9, 4, 'Brisket de Bœuf BBQ', 'Brisket de bœuf lentement cuit, sauce BBQ, coleslaw, cornichons, pain brioche\r\n', 'Un sandwich copieux avec du brisket de bœuf tendre et lentement cuit, nappé de sauce BBQ, surmonté de coleslaw croquant et de cornichons dans un pain brioche moelleux.', 8, '/img/menuImages/a1022175-e635-45b5-a0c3-ec80e05c145a_vue-dessus-delicieux-sandwich-au-ham-interieur-plaque-sombre-frites-du-ketchup-surface-sombre.jpg', '2023-12-30 09:15:39.628683', '2024-02-02 23:03:00.000000', 1, 1),
	(30, 9, 4, 'Philly Cheesesteak', 'Steak en tranches, oignons et poivrons sautés, fromage provolone, pain hoagie\r\n', 'Un cheesesteak de Philadelphie classique avec du steak finement tranché, des oignons et des poivrons sautés, et du fromage provolone fondu dans un pain hoagie traditionnel.', 9, '/img/menuImages/e2267dc6-bfda-4212-83ea-364888d94e9c_sandwich-viande-vue-cote-frites-planche-min.jpg', '2023-12-30 09:18:43.144564', '2024-02-02 23:05:00.000000', 1, 1),
	(31, 10, 4, 'Wrap Méditerranéen', 'Houmous, concombre, tomate, olives, oignon rouge, épinards, tortilla de blé entier\r\n', 'Un wrap rafraîchissant rempli de houmous onctueux, de concombre croquant, de tomate mûre, d\'olives, d\'oignon rouge et d\'épinards frais, enveloppé dans une tortilla de blé entier.', 10, '/img/menuImages/8e8ff6a1-c454-4951-8d9c-52537dbb3a93_delicieux-sandwich-vegetalien-table-bois.jpg', '2023-12-30 09:30:47.648516', '2024-02-02 22:02:00.000000', 1, 1),
	(32, 10, 4, ' Sandwich Vegan Caprese', 'Mozzarella vegan, tomate, pesto de basilic, épinards, pain ciabatta\r\n', 'Une délicieuse version vegan du sandwich Caprese, avec de la mozzarella vegan, des tomates mûres, du pesto de basilic vegan et des épinards frais sur du pain ciabatta artisanal.', 9, '/img/menuImages/2f3575a4-8b15-48ae-b677-1c29abb76212_sandwich-vegetarien-salade-tomates-surface-table-bois.jpg', '2023-12-30 09:32:01.937971', '2024-02-02 04:03:00.000000', 1, 1),
	(33, 10, 4, 'Légumes Grillés et Houmous', 'Courgette grillée, aubergine, poivrons rouges, houmous, pain focaccia\r\n', 'Un sandwich savoureux garni de courgettes, d\'aubergines et de poivrons rouges grillés, tartiné de houmous crémeux sur un pain focaccia fraîchement cuit.', 9, '/img/menuImages/c3b0baec-d06d-49ad-af7d-7fcd6e358edc_vue-dessus-sandwichs-triangulaires-assiette-couverts-tranches-concombre.jpg', '2023-12-30 09:33:07.319122', '2024-02-02 03:04:00.000000', 1, 1),
	(34, 8, 3, 'Poulet Rôti à l\'Herbes de Provence', 'Cuisse de poulet rôtie, herbes de Provence, légumes de saison grillés, purée de pommes de terre à l\'ail\r\n', 'Un délicieux poulet rôti parfaitement assaisonné avec des herbes de Provence, accompagné de légumes de saison grillés et d\'une onctueuse purée de pommes de terre à l\'ail.', 14, '/img/menuImages/f393eb21-fc3c-4192-973d-74d666a5329f_5ccc06e0-b7eb-429c-a891-a53d6bddc204_R.jpg', '2023-12-30 09:35:22.950792', '2024-02-02 04:03:00.000000', 1, 1),
	(35, 8, 3, 'Escalope de Poulet à la Milanaise', 'Escalope de poulet panée, spaghetti, sauce tomate maison, basilic frais\r\n', 'Une escalope de poulet croustillante servie avec des spaghetti al dente, nappée d\'une sauce tomate maison et garnie de basilic frais.', 15, '/img/menuImages/b6fa62e0-5eda-4791-8b72-fe7d817aa3ff_freshly-grilled-meat-veggies-plate-generative-ai.jpg', '2023-12-30 09:36:36.805179', '2024-03-02 05:06:00.000000', 1, 1),
	(36, 8, 3, 'Poulet Tikka Masala', 'Morceaux de poulet marinés, sauce tikka masala riche, riz basmati, naan\r\n', 'Morceaux de poulet tendres et marinés, cuisinés dans une sauce tikka masala riche et épicée, servis avec du riz basmati parfumé et du pain naan chaud.', 15, '/img/menuImages/9db80f40-8368-4ff6-8347-e69a8c570bb6_mexican-quesadilla-wrap-with-chicken-corn-sweet-pepper-salsa.jpg', '2023-12-30 09:37:36.785695', '2024-02-02 04:05:00.000000', 1, 1),
	(37, 9, 3, 'Bœuf Bourguignon', 'Morceaux de bœuf braisés, légumes d\'hiver, sauce au vin rouge, purée de pommes de terre\r\n', 'Un classique français, avec des morceaux de bœuf lentement braisés dans une sauce au vin rouge riche, accompagnés de légumes d\'hiver et d\'une purée de pommes de terre crémeuse.', 13, '/img/menuImages/c22da328-ab4d-4a41-a642-bda1d640a7f7_delicious-goulash-ready-dinner.jpg', '2023-12-30 09:38:49.617094', '2024-02-02 04:05:00.000000', 1, 1),
	(38, 9, 3, 'Steak Grillé au Poivre', 'Steak de bœuf, sauce au poivre, frites maison, salade verte\r\n', 'Un steak de bœuf juteux grillé à la perfection, servi avec une sauce au poivre onctueuse, des frites croustillantes maison et une salade verte fraîche.', 14, '/img/menuImages/f4b54ad7-37b8-437a-b2fd-d9dcc0a1f09a_high-angle-asian-rice-dish-with-meat.jpg', '2023-12-30 09:40:03.681497', '2024-02-02 04:34:00.000000', 1, 1),
	(39, 9, 3, 'Lasagnes à la Bolognaise', 'Pâtes à lasagne, viande de bœuf hachée, sauce tomate, béchamel, fromage râpé\r\n', 'Couches de pâtes à lasagne alternées avec de la viande de bœuf hachée savoureuse, de la sauce tomate, de la béchamel crémeuse et du fromage râpé gratiné.', 14, '/img/menuImages/47aaa3b1-6ce8-4cea-a191-9c9808fdebfb_tender-juicy-veal-steak-medium-rare-with-french-fries.jpg', '2023-12-30 09:45:12.454757', '2024-03-03 04:45:00.000000', 1, 1),
	(40, 10, 3, 'Curry Vegan aux Légumes', 'Légumes de saison, sauce curry doux, lait de coco, riz basmati\r\n', 'Un mélange coloré de légumes de saison mijotés dans une sauce curry doux et crémeuse au lait de coco, servi avec du riz basmati parfumé.', 14, '/img/menuImages/d1e65c9b-773c-4894-a25a-ac7582ab8d2a_dietary-menu-healthy-vegan-salad-vegetables-broccoli-mushrooms-spinach-quinoa-bowl-flat-lay-top-view.jpg', '2023-12-30 09:46:25.614212', '2024-02-23 04:02:00.000000', 0, 1),
	(41, 10, 3, 'Risotto aux Champignons et Asperges', 'Riz arborio, champignons sauvages, asperges, bouillon de légumes, levure nutritionnelle\r\n', 'Un risotto onctueux et riche, préparé avec du riz arborio, des champignons sauvages, des asperges fraîches, et cuit lentement dans un bouillon de légumes savoureux.', 15, '/img/menuImages/c1a1ca70-f8a1-41e2-a59d-58056ee02f3e_vegan-person-holds-up-wooden-coconut-buddha-bowl-full-healthy-greens-vegetables-grains-chopsticks.jpg', '2023-12-30 09:47:42.891402', '2024-03-03 04:05:00.000000', 1, 0),
	(42, 10, 3, 'Haricots Noirs', 'Galette de haricots noirs, avocat, tomate, laitue, mayo vegan, pain burger vegan\r\n', 'Un burger nourrissant avec une galette de haricots noirs faite maison, garnie d\'avocat crémeux, de tomate fraîche, de laitue croquante, et de mayo vegan, dans un pain burger vegan moelleux.', 15, '/img/menuImages/4b400868-a81c-4732-9bf1-3964b78839f3_tasty-fresh-poke-bowl-with-avocado-quinoa-vegetables-top-view.jpg', '2023-12-30 09:48:56.157023', '2024-03-02 04:03:00.000000', 0, 1);

-- Dumping structure for table bertlunch.orderitems
DROP TABLE IF EXISTS `orderitems`;
CREATE TABLE IF NOT EXISTS `orderitems` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Quantity` int(11) NOT NULL,
  `MenuOrderHistories_Id` int(11) NOT NULL,
  `MenuOrderHistories_MenuOrderName` longtext NOT NULL,
  `MenuOrderHistories_CategoryName` longtext NOT NULL,
  `MenuOrderHistories_MenuCategory` longtext NOT NULL,
  `MenuOrderHistories_Image` longtext NOT NULL,
  `Price` float NOT NULL,
  `ReservationId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_OrderItems_ReservationId` (`ReservationId`),
  CONSTRAINT `FK_OrderItems_Reservations_ReservationId` FOREIGN KEY (`ReservationId`) REFERENCES `reservations` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.orderitems: ~2 rows (approximately)
INSERT INTO `orderitems` (`Id`, `Quantity`, `MenuOrderHistories_Id`, `MenuOrderHistories_MenuOrderName`, `MenuOrderHistories_CategoryName`, `MenuOrderHistories_MenuCategory`, `MenuOrderHistories_Image`, `Price`, `ReservationId`) VALUES
	(1, 3, 8, 'Crispy Chicken Burger', 'Le poulet', 'Les assiettes', 'img/menuImages/e280f247-89e0-41e9-952d-e686c1b52ca9_OIP.jpeg', 14.3, 1),
	(2, 2, 7, 'Carbonade de boeuf', 'Le bœuf', 'Les assiettes', 'img/menuImages/ba05f55c-60da-4977-abe2-3cf37a5d9752_th.jpeg', 16.6, 1);

-- Dumping structure for table bertlunch.reservations
DROP TABLE IF EXISTS `reservations`;
CREATE TABLE IF NOT EXISTS `reservations` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) DEFAULT NULL,
  `CreateDate` datetime(6) NOT NULL,
  `ReservationDate` datetime(6) NOT NULL,
  `Subtotal` float NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Reservations_UserId` (`UserId`),
  CONSTRAINT `FK_Reservations_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.reservations: ~1 rows (approximately)
INSERT INTO `reservations` (`Id`, `UserId`, `CreateDate`, `ReservationDate`, `Subtotal`) VALUES
	(1, NULL, '2023-12-17 14:59:09.787749', '2023-12-17 15:00:00.000000', 76.1);

-- Dumping structure for table bertlunch.__efmigrationshistory
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table bertlunch.__efmigrationshistory: ~3 rows (approximately)
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20231127172827_User', '7.0.12'),
	('20231129062334_CombinedMigration', '7.0.12'),
	('20231216101520_Orders', '7.0.12');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
