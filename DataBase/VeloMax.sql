-- phpMyAdmin SQL Dump
-- version 5.0.4deb2
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : lun. 09 mai 2022 à 20:14
-- Version du serveur :  10.5.15-MariaDB-0+deb11u1
-- Version de PHP : 7.4.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
CREATE DATABASE IF NOT EXISTS`VeloMax`;
USE `VeloMax`;
--
-- Base de données : `VeloMax`
--

-- --------------------------------------------------------

--
-- Structure de la table `ACHAT_BICYCLETTE`
--

CREATE TABLE `ACHAT_BICYCLETTE` (
  `ID_Commande` int(30) NOT NULL,
  `ID_Bicyclette` int(30) NOT NULL,
  `NombreArticles` int(30) NOT NULL,
  `DateLivraison` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `ACHAT_PIECE`
--

CREATE TABLE `ACHAT_PIECE` (
  `ID_Commande` int(30) NOT NULL,
  `Identifiant_Piece` varchar(15) NOT NULL,
  `NombreArticles` int(30) NOT NULL,
  `DateLivraison` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `ASSEMBLER_PAR`
--

CREATE TABLE `ASSEMBLER_PAR` (
  `ID_Bicyclette` int(30) NOT NULL,
  `Identifiant_Piece` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `BICYCLETTE`
--

CREATE TABLE `BICYCLETTE` (
  `ID_Bicyclette` int(30) NOT NULL,
  `Nom_Bicyclette` varchar(255) NOT NULL,
  `Grandeur_Bicyclette` enum('Adultes','Jeunes','Hommes','Dames','Filles','Garçons') NOT NULL,
  `Prix_Bicyclette` double NOT NULL,
  `Type_Bicyclette` enum('VTT','Course','Classique','BMX') NOT NULL,
  `DateIntroduction_Bicyclette` date NOT NULL,
  `DateFin_Bicyclette` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `CLIENT`
--

CREATE TABLE `CLIENT` (
  `ID_Client` int(30) NOT NULL,
  `Type_Client` enum('Particulier','Entreprise') NOT NULL,
  `Tel_Client` varchar(255) NOT NULL,
  `Courriel_Client` varchar(255) NOT NULL,
  `Adresse_Client` varchar(255) NOT NULL,
  `Nom_Client` varchar(255) NOT NULL,
  `Prenom_Client` varchar(255) DEFAULT NULL,
  `NomCompagnie_Client` varchar(255) DEFAULT NULL,
  `RemiseCompagnie_Client` double DEFAULT NULL,
  `NumProgramme_Fidelio` int(30) DEFAULT NULL,
  `DateDebut_Fidelio` date DEFAULT NULL,
  `DateFin_Fidelio` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `CLIENT`
--

INSERT INTO `CLIENT` (`ID_Client`, `Type_Client`, `Tel_Client`, `Courriel_Client`, `Adresse_Client`, `Nom_Client`, `Prenom_Client`, `NomCompagnie_Client`, `RemiseCompagnie_Client`, `NumProgramme_Fidelio`, `DateDebut_Fidelio`, `DateFin_Fidelio`) VALUES
(1, 'Particulier', '0685324585', 'hugo@test.fr', '41 rue Jobin\r\n13003 Marseille', 'bob', 'hugo', NULL, NULL, 3, '2022-04-07', '2024-04-07'),
(2, 'Particulier', '0632544489', 'rufus@gmail.com', '19 rue des Augustins\r\n33000 Bordeaux', 'Chemine', 'Rufus', NULL, NULL, 4, '2022-04-14', '2025-04-14'),
(3, 'Entreprise', '0147855472', 'Dekatlon@free.fr', '108, rue de rhodes\r\n34000 Montpellier', 'Ben', NULL, 'Dekatlon', 0.1, NULL, NULL, NULL),
(4, 'Entreprise', '0346580184', '1tersport@gmail.com', '14, rue de joncquilles\r\n34000 Lille', 'Philippe', NULL, '1tersp0rt', 0.15, NULL, NULL, NULL),
(5, 'Entreprise', '0784949901', 'g0sp0rt@gmail.com', '4, rue Carnot \r\n34000 Paris', 'Emmanuel', NULL, 'g0sp0rt', 0.12, NULL, NULL, NULL),
(6, 'Particulier', '0978440144', 'pierre@test.fr', '7 rue de l eglise\r\n13003 Cambray', 'Thalis', 'Pierre', NULL, NULL, 1, '2022-01-03', '2023-01-03');

--
-- Déclencheurs `CLIENT`
--
DELIMITER $$
CREATE TRIGGER `CLIENT_DateFin_Fidelio` BEFORE INSERT ON `CLIENT` FOR EACH ROW begin
  declare duree int(11);

  IF NEW.NumProgramme_Fidelio IS NOT NULL
  THEN
    select Duree_Fidelio into duree from FIDELIO
    where NumProgramme_Fidelio = NEW.NumProgramme_Fidelio;
    set NEW.DateFin_Fidelio = DATE_ADD(NEW.DateDebut_Fidelio, INTERVAL duree YEAR);
  END IF;
end
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `COMMANDE`
--

CREATE TABLE `COMMANDE` (
  `ID_Commande` int(30) NOT NULL,
  `Date_Commande` date NOT NULL,
  `AdresseLivraison_Commande` varchar(255) NOT NULL,
  `ID_Client` int(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `FIDELIO`
--

CREATE TABLE `FIDELIO` (
  `NumProgramme_Fidelio` int(30) NOT NULL,
  `Description_Fidelio` varchar(255) NOT NULL,
  `Cout_Fidelio` int(30) NOT NULL,
  `Duree_Fidelio` int(30) NOT NULL,
  `Rabais_Fidelio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `FIDELIO`
--

INSERT INTO `FIDELIO` (`NumProgramme_Fidelio`, `Description_Fidelio`, `Cout_Fidelio`, `Duree_Fidelio`, `Rabais_Fidelio`) VALUES
(1, 'Fidélio', 15, 1, 0.05),
(2, 'Fidélio Or', 25, 2, 0.08),
(3, 'Fidélio Platine', 60, 2, 0.1),
(4, 'Fidélio Max', 100, 3, 0.12);

-- --------------------------------------------------------

--
-- Structure de la table `FOURNISSEUR`
--

CREATE TABLE `FOURNISSEUR` (
  `Siret_Fournisseur` bigint(20) NOT NULL,
  `NomEntreprise_Fournisseur` varchar(255) NOT NULL,
  `Contact_Fournisseur` varchar(255) NOT NULL,
  `Adresse_Fournisseur` varchar(255) NOT NULL,
  `Libelle_Fournisseur` enum('1','2','3','4') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `FOURNISSEUR`
--

INSERT INTO `FOURNISSEUR` (`Siret_Fournisseur`, `NomEntreprise_Fournisseur`, `Contact_Fournisseur`, `Adresse_Fournisseur`, `Libelle_Fournisseur`) VALUES
(27382698187536, 'PiecesFabrique', 'piecesfabrique@gmail.com', '9 avenue Richelieu, Caen', '4'),
(27589698100017, 'Bikebuy', 'bikebuy@velo.com', '8 Avenue General Lecler, Calais', '1'),
(39982698100017, 'BricoVelo', 'bricovelo@velo.com', '14 rue Victor Hugo, Argentan', '1'),
(43282695524021, 'Bikeshop', 'bikeshop@gmail.com', '6 rue André, Paris', '2'),
(64126254620004, 'roadsport', 'roadsport@gmail.com', '6 rue St Louis, Paris', '2'),
(64189454720036, '2roues', '2roues@gmail.com', '45 rue du temple, Nogent', '2'),
(67822698100036, 'VeloFabrik', 'velofabrik@gmail.com', '4 rue Carnot, Lille', '3'),
(87551075487408, 'roadtogo', 'roadtogo@gmail.com', '2 avenue de la piscine, Caen', '4');

-- --------------------------------------------------------

--
-- Structure de la table `FOURNIT`
--

CREATE TABLE `FOURNIT` (
  `Siret_Fournisseur` bigint(20) NOT NULL,
  `Identifiant_Piece` varchar(15) NOT NULL,
  `Nom_Fournisseur` varchar(255) NOT NULL,
  `NumProduit_Fournisseur` int(30) NOT NULL,
  `Prix_Fournisseur` double NOT NULL,
  `Quantite_Fournisseur` int(30) NOT NULL,
  `Delai_Fournisseur` int(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `FOURNIT`
--

INSERT INTO `FOURNIT` (`Siret_Fournisseur`, `Identifiant_Piece`, `Nom_Fournisseur`, `NumProduit_Fournisseur`, `Prix_Fournisseur`, `Quantite_Fournisseur`, `Delai_Fournisseur`) VALUES
(27382698187536, 'C32', 'PiecesFabrique', 12, 100.3, 5, 20),
(27382698187536, 'C34', 'PiecesFabrique', 55, 80.5, 3, 15),
(39982698100017, 'C76', 'BricoVelo', 3, 92, 8, 30),
(64189454720036, 'C43', '2roues', 9, 150, 6, 40),
(64189454720036, 'C43F', '2roues', 777, 112.5, 2, 40),
(67822698100036, 'C44F', 'VeloFabrik', 20, 135, 10, 65),
(27589698100017, 'C02', 'Bikebuy', 854, 432.25, 1, 87),
(43282695524021, 'C15', 'Bikeshop', 458, 108, 2, 45),
(64126254620004, 'C87', 'roadsport', 82, 109, 6, 36),
(64126254620004, 'C87F', 'roadsport', 481, 72, 4, 70),
(64189454720036, 'C25', '2roues', 31, 154.12, 12, 20),
(64189454720036, 'C26', '2roues', 56466, 75.4, 8, 12),
(43282695524021, 'C01', 'Bikeshop', 27, 95, 3, 20),
(43282695524021, 'G7', 'Bikeshop', 143, 20, 15, 30),
(27589698100017, 'G9', 'Bikebuy', 547, 25, 20, 40),
(64189454720036, 'G12', '2roues', 869, 30, 10, 28),
(27382698187536, 'F3', 'PiecesFabrique', 41, 25, 30, 40),
(67822698100036, 'F9', 'VeloFabrik', 8, 40, 40, 50),
(67822698100036, 'S88', 'VeloFabrik', 333, 10, 100, 30),
(87551075487408, 'S37', 'roadtogo', 472, 15, 75, 45),
(67822698100036, 'S35', 'VeloFabrik', 1112, 12.5, 40, 20),
(39982698100017, 'S02', 'BricoVelo', 1113, 9.8, 3, 5),
(43282695524021, 'S03', 'Bikeshop', 1114, 7.9, 5, 10),
(39982698100017, 'S36', 'BricoVelo', 1116, 5.5, 50, 50),
(27589698100017, 'S34', 'Bikebuy', 111172, 15, 10, 40),
(27382698187536, 'S87', 'PiecesFabrique', 2221, 9, 14, 25),
(27382698187536, 'DV133', 'PiecesFabrique', 888, 30, 30, 25),
(27589698100017, 'DV17', 'Bikebuy', 7452, 37.2, 40, 50),
(39982698100017, 'DV87', 'BricoVelo', 874, 40, 5, 10),
(43282695524021, 'DV57', 'Bikeshop', 356, 22, 15, 20),
(64126254620004, 'DV15', 'roadsport', 96, 22, 30, 50),
(64189454720036, 'DV41', '2roues', 931, 10, 2, 5),
(67822698100036, 'DV132', 'VeloFabrik', 55554, 17, 1, 1),
(27382698187536, 'DR56', 'PiecesFabrique', 6666556, 40, 5, 10),
(27589698100017, 'DR87', 'Bikebuy', 275896, 45, 2, 5),
(39982698100017, 'DR86', 'BricoVelo', 3998, 35, 8, 15),
(43282695524021, 'DR23', 'Bikeshop', 24021, 28, 15, 20),
(64126254620004, 'DR76', 'roadsport', 67614, 45, 4, 20),
(64189454720036, 'DR52', '2roues', 572, 37.8, 7, 15),
(27589698100017, 'C01', 'Bikebuy', 47, 10, 4, 5),
(27382698187536, 'R45', 'PiecesFabrique', 14, 12, 5, 10),
(27589698100017, 'R48', 'Bikebuy', 45516, 20, 10, 20),
(43282695524021, 'R19', 'Bikeshop', 9845, 14.35, 4, 10),
(64126254620004, 'R1', 'roadsport', 641, 8, 1, 3),
(64189454720036, 'R11', '2roues', 20036, 13, 6, 14),
(67822698100036, 'R44', 'VeloFabrik', 213, 18, 4, 6),
(67822698100036, 'R02', 'VeloFabrik', 154, 14, 10, 22),
(87551075487408, 'R09', 'roadtogo', 95245, 12.5, 2, 5),
(27589698100017, 'R10', 'Bikebuy', 453, 20, 12, 40),
(39982698100017, 'P12', 'BricoVelo', 2165, 42.5, 12, 30),
(64126254620004, 'P34', 'roadsport', 354, 52, 7, 3),
(64189454720036, 'P1', '2roues', 5447, 32.75, 8, 13),
(67822698100036, 'P15', 'VeloFabrik', 3205, 27, 4, 7),
(67822698100036, 'O2', 'VeloFabrik', 859, 40, 5, 14),
(87551075487408, 'O4', 'roadtogo', 261, 72, 6, 20),
(87551075487408, 'S01', 'roadtogo', 84, 26, 3, 7),
(27589698100017, 'S05', 'Bikebuy', 172, 28, 2, 6),
(39982698100017, 'S74', 'BricoVelo', 24, 29.99, 3, 9),
(43282695524021, 'S73', 'Bikeshop', 3454, 68, 2, 7),
(27382698187536, 'R46', 'PiecesFabrique', 78, 25, 10, 14),
(27589698100017, 'R47', 'Bikebuy', 445, 31.5, 8, 16),
(39982698100017, 'R32', 'BricoVelo', 932457, 35, 8, 20),
(64126254620004, 'R18', 'roadsport', 5625, 45, 3, 7),
(64189454720036, 'R2', '2roues', 778, 22, 4, 12),
(67822698100036, 'R12', 'VeloFabrik', 542, 28.9, 2, 7),
(27382698187536, 'C01', 'PiecesFabrique', 123, 389.99, 5, 8),
(39982698100017, 'C01', 'BricoVelo', 5, 599.99, 14, 4),
(67822698100036, 'C01', 'VeloFabrik', 85, 700.01, 11, 9),
(67822698100036, 'C02', 'VeloFabrik', 78, 495.99, 14, 20),
(39982698100017, 'C02', 'BricoVelo', 95, 567.89, 7, 14),
(67822698100036, 'C15', 'VeloFabrik', 141, 989.7, 3, 4),
(39982698100017, 'C15', 'BricoVelo', 13, 666.67, 6, 11),
(87551075487408, 'C15', 'roadtogo', 1, 300, 21, 6),
(87551075487408, 'C25', 'roadtogo', 65, 171.5, 15, 6),
(64126254620004, 'C25', 'roadsport', 6459, 199, 28, 29),
(43282695524021, 'C26', 'Bikeshop', 752, 187, 19, 8),
(27382698187536, 'C26', 'PiecesFabrique', 71, 189.5, 7, 24),
(87551075487408, 'C32', 'roadtogo', 91265, 113.7, 8, 9),
(64126254620004, 'C32', 'roadsport', 69504, 99.95, 7, 4),
(87551075487408, 'C34', 'roadtogo', 42, 91.75, 12, 12),
(43282695524021, 'C32', 'Bikeshop', 6491079, 79.79, 4, 15),
(64126254620004, 'C34', 'roadsport', 6549184, 80.88, 13, 3),
(87551075487408, 'C43', 'roadtogo', 9842, 150, 13, 7),
(27382698187536, 'C43', 'PiecesFabrique', 90964, 139.95, 3, 15),
(87551075487408, 'C43F', 'roadtogo', 6540, 110, 7, 5),
(27382698187536, 'C43F', 'PiecesFabrique', 70, 124.85, 16, 19),
(27589698100017, 'C44F', 'Bikebuy', 25800, 140, 8, 6),
(87551075487408, 'C44F', 'roadtogo', 9719182, 139.98, 3, 5),
(43282695524021, 'C76', 'Bikeshop', 6519925, 93.17, 4, 14),
(64126254620004, 'C76', 'roadsport', 982525, 93.6, 1, 9),
(64189454720036, 'C87', '2roues', 65491, 109.99, 3, 7),
(43282695524021, 'C87', 'Bikeshop', 6985982, 119.98, 7, 3),
(87551075487408, 'C87F', 'roadtogo', 619, 80, 10, 7),
(27589698100017, 'C87F', 'Bikebuy', 9845, 75.7, 1, 6),
(64189454720036, 'DR23', '2roues', 9519, 30.16, 7, 4),
(39982698100017, 'DR23', 'BricoVelo', 981981, 75.74, 3, 4),
(39982698100017, 'DR52', 'BricoVelo', 817, 29, 4, 16),
(64126254620004, 'DR52', 'roadsport', 725, 28.74, 19, 7),
(64189454720036, 'DR56', '2roues', 614, 41.98, 7, 5),
(87551075487408, 'DR56', 'roadtogo', 6519, 30.18, 2, 14),
(87551075487408, 'DR76', 'roadtogo', 761, 49.99, 5, 17),
(43282695524021, 'DR76', 'Bikeshop', 51841, 39.98, 18, 7),
(27382698187536, 'S01', 'PiecesFabrique', 3248, 14.85, 10, 7),
(39982698100017, 'S01', 'BricoVelo', 117, 21.48, 35, 20),
(64126254620004, 'S01', 'roadsport', 45872, 17.6, 14, 10),
(27589698100017, 'S02', 'Bikebuy', 12, 27.99, 48, 12),
(64189454720036, 'S02', '2roues', 145, 29.35, 5, 3),
(87551075487408, 'S03', 'roadtogo', 345768, 34.52, 50, 7),
(64189454720036, 'S05', '2roues', 5552, 4, 15, 5),
(87551075487408, 'S05', 'roadtogo', 276, 2.82, 3, 9),
(39982698100017, 'S05', 'BricoVelo', 23, 6.72, 17, 2),
(27382698187536, 'S34', 'PiecesFabrique', 1166, 12.5, 26, 10),
(64189454720036, 'S34', '2roues', 765, 17.6, 30, 45),
(27589698100017, 'S35', 'Bikebuy', 6431, 17, 80, 30),
(64126254620004, 'S35', 'roadsport', 542, 14.27, 27, 12),
(67822698100036, 'S36', 'VeloFabrik', 121, 7.5, 60, 20),
(27589698100017, 'S37', 'Bikebuy', 7679, 9.99, 12, 38),
(67822698100036, 'S37', 'VeloFabrik', 5465, 17.12, 21, 14),
(39982698100017, 'S73', 'BricoVelo', 52218, 48, 10, 90),
(27382698187536, 'S73', 'PiecesFabrique', 39012, 72.56, 50, 12),
(43282695524021, 'S74', 'Bikeshop', 6839, 43, 25, 20),
(64126254620004, 'S74', 'roadsport', 98, 27.45, 18, 10),
(27382698187536, 'S74', 'PiecesFabrique', 4445, 27.89, 34, 12),
(64126254620004, 'S87', 'roadsport', 1897, 12, 15, 5),
(67822698100036, 'S87', 'VeloFabrik', 6667, 17.12, 20, 6),
(87551075487408, 'S88', 'roadtogo', 78678, 6.87, 40, 20),
(27382698187536, 'S88', 'PiecesFabrique', 56756, 12.87, 44, 3),
(39982698100017, 'S88', 'BricoVelo', 3221, 17.15, 60, 30),
(64189454720036, 'DR86', '2roues', 329, 29.99, 24, 12),
(43282695524021, 'DR86', 'Bikeshop', 6439, 40, 72, 21),
(64189454720036, 'DR87', '2roues', 17, 30, 20, 20),
(27382698187536, 'DV132', 'PiecesFabrique', 2457, 20, 12, 32),
(64189454720036, 'DV132', '2roues', 1294, 27, 56, 17),
(27589698100017, 'DV133', 'Bikebuy', 6783, 23, 50, 7),
(43282695524021, 'DV133', 'Bikeshop', 8734, 27.72, 28, 10),
(27589698100017, 'DV15', 'Bikebuy', 29, 19.99, 20, 7),
(39982698100017, 'DV17', 'BricoVelo', 1348, 40, 32, 12),
(87551075487408, 'DV17', 'roadtogo', 123, 27, 5, 17),
(67822698100036, 'DV41', 'VeloFabrik', 56943, 12, 32, 14),
(43282695524021, 'DV41', 'Bikeshop', 334, 12.5, 30, 12),
(27382698187536, 'DV41', 'PiecesFabrique', 82789, 18.67, 35, 2),
(27589698100017, 'DV57', 'Bikebuy', 8782, 25, 12, 7),
(64189454720036, 'DV57', '2roues', 87662, 32.57, 60, 40),
(67822698100036, 'DV87', 'VeloFabrik', 2862, 32.78, 12, 27),
(43282695524021, 'DV87', 'Bikeshop', 6454, 37.89, 5, 7),
(64189454720036, 'DV15', '2roues', 9521, 25.97, 7, 27),
(64189454720036, 'F3', '2roues', 951572, 29.97, 26, 16),
(43282695524021, 'F3', 'Bikeshop', 18927, 24.99, 7, 31),
(39982698100017, 'F3', 'BricoVelo', 91951, 27.01, 2, 10),
(64189454720036, 'F9', '2roues', 7537, 43.95, 75, 8),
(43282695524021, 'F9', 'Bikeshop', 737027025, 43.03, 83, 20),
(43282695524021, 'G12', 'Bikeshop', 7185, 32.99, 12, 15),
(27382698187536, 'G12', 'PiecesFabrique', 65, 29.99, 14, 4),
(64189454720036, 'G7', '2roues', 72, 19.99, 4, 5),
(87551075487408, 'G7', 'roadtogo', 5675670, 22.02, 72, 20),
(87551075487408, 'G9', 'roadtogo', 59154, 24.99, 55, 5),
(64189454720036, 'G9', '2roues', 93271012, 33.01, 7, 14),
(64189454720036, 'O2', '2roues', 9195, 255.95, 10, 18),
(43282695524021, 'O2', 'Bikeshop', 9871, 247.99, 13, 4),
(43282695524021, 'O4', 'Bikeshop', 614, 76.49, 6, 7),
(27382698187536, 'O4', 'PiecesFabrique', 241, 82.02, 16, 24),
(27382698187536, 'R19', 'PiecesFabrique', 199492, 15.01, 17, 6),
(43282695524021, 'P1', 'Bikeshop', 76578, 35, 42, 50),
(64126254620004, 'P12', 'roadsport', 6874332, 41.23, 8, 4),
(27589698100017, 'P1', 'Bikebuy', 567884, 42.78, 17, 23),
(87551075487408, 'P1', 'roadtogo', 2375, 32.17, 20, 30),
(27382698187536, 'P12', 'PiecesFabrique', 651, 23.72, 9, 9),
(27382698187536, 'P15', 'PiecesFabrique', 44024, 29.99, 7, 13),
(64126254620004, 'P15', 'roadsport', 12, 32.63, 12, 2),
(64189454720036, 'P15', '2roues', 752, 27.95, 4, 27),
(39982698100017, 'P15', 'BricoVelo', 6399, 27, 32, 30),
(43282695524021, 'P34', 'Bikeshop', 2025077, 55.59, 782, 28),
(87551075487408, 'P34', 'roadtogo', 6785, 52.1, 40, 12),
(27382698187536, 'P34', 'PiecesFabrique', 757, 53.01, 7, 7),
(67822698100036, 'R48', 'VeloFabrik', 73892, 26, 60, 21),
(39982698100017, 'R48', 'BricoVelo', 1657, 23.65, 10, 10),
(27382698187536, 'R02', 'PiecesFabrique', 420, 17.05, 244, 13),
(64126254620004, 'R47', 'roadsport', 376, 32, 40, 12),
(64189454720036, 'R02', '2roues', 805, 15.51, 75, 7),
(27382698187536, 'R47', 'PiecesFabrique', 54, 36.5, 3, 17),
(27589698100017, 'R46', 'Bikebuy', 4568, 27.98, 38, 32),
(64126254620004, 'R45', 'roadsport', 351, 11.99, 10, 5),
(64189454720036, 'R09', '2roues', 237, 12.99, 72, 14),
(87551075487408, 'R45', 'roadtogo', 1499, 15.67, 30, 15),
(27589698100017, 'R44', 'Bikebuy', 6865, 17, 90, 60),
(39982698100017, 'R44', 'BricoVelo', 7862, 22.12, 25, 24),
(64126254620004, 'R32', 'roadsport', 1111, 39.32, 22, 14),
(43282695524021, 'R09', 'Bikeshop', 2047, 13.99, 879, 9),
(27382698187536, 'R32', 'PiecesFabrique', 2458, 25.98, 35, 12),
(87551075487408, 'R32', 'roadtogo', 2387, 32.8, 50, 17),
(64189454720036, 'R46', '2roues', 514740, 14.99, 29, 9),
(27589698100017, 'R2', 'Bikebuy', 7873, 24.7, 5, 3),
(43282695524021, 'R2', 'Bikeshop', 450720, 23.01, 19, 19),
(67822698100036, 'R2', 'VeloFabrik', 6445, 32.1, 60, 20),
(43282695524021, 'R18', 'Bikeshop', 9000080, 49.99, 19, 5),
(39982698100017, 'R12', 'BricoVelo', 257, 35.78, 30, 8),
(27382698187536, 'R18', 'PiecesFabrique', 5001, 50.01, 35, 19),
(27382698187536, 'R12', 'PiecesFabrique', 902, 32.41, 20, 6),
(64189454720036, 'R12', '2roues', 8652000, 29.99, 41, 7),
(27382698187536, 'R11', 'PiecesFabrique', 3478, 15, 40, 8),
(64126254620004, 'R11', 'roadsport', 36247, 18.89, 25, 12),
(64189454720036, 'R1', '2roues', 98409, 9.99, 21, 7),
(67822698100036, 'R10', 'VeloFabrik', 678654, 17, 50, 43),
(43282695524021, 'R1', 'Bikeshop', 782038, 12.01, 9, 8),
(64189454720036, 'R19', '2roues', 78607070, 14.99, 19, 15);

--
-- Déclencheurs `FOURNIT`
--
DELIMITER $$
CREATE TRIGGER `FOURNIT_NomFournisseur` BEFORE INSERT ON `FOURNIT` FOR EACH ROW begin
  declare NomF Varchar(255);

  select NomEntreprise_Fournisseur into NomF from FOURNISSEUR
  where Siret_Fournisseur = NEW.Siret_Fournisseur;
  set NEW.Nom_Fournisseur = NomF;
end
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `PIECE`
--

CREATE TABLE `PIECE` (
  `Identifiant_Piece` varchar(15) NOT NULL,
  `Description_Piece` enum('Cadre','Guidon','Freins','Selle','Dérailleur Avant','Dérailleur Arrière','Roue Avant','Roue Arrière','Réflecteurs','Pédalier','Ordinateur','Panier') NOT NULL,
  `DateDebut_Piece` date NOT NULL,
  `DateFin_Piece` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `PIECE`
--

INSERT INTO `PIECE` (`Identifiant_Piece`, `Description_Piece`, `DateDebut_Piece`, `DateFin_Piece`) VALUES
('C01', 'Cadre', '2019-12-20', '2019-12-20'),
('C02', 'Cadre', '2022-04-06', '2022-12-02'),
('C15', 'Cadre', '2020-02-05', '2020-02-05'),
('C25', 'Cadre', '2021-09-14', '2021-09-14'),
('C26', 'Cadre', '2021-12-24', '2021-12-24'),
('C32', 'Cadre', '2022-01-31', '2022-01-31'),
('C34', 'Cadre', '2021-12-30', '2021-12-30'),
('C43', 'Cadre', '2022-03-10', '2022-03-10'),
('C43F', 'Cadre', '2021-06-16', '2021-06-16'),
('C44F', 'Cadre', '2022-04-29', '2022-04-29'),
('C76', 'Cadre', '2022-02-01', '2022-02-01'),
('C87', 'Cadre', '2021-08-10', '2021-08-10'),
('C87F', 'Cadre', '2022-03-31', '2022-03-31'),
('DR23', 'Dérailleur Arrière', '2022-02-02', '2022-02-02'),
('DR52', 'Dérailleur Arrière', '2022-05-07', '2022-05-07'),
('DR56', 'Dérailleur Arrière', '2021-03-19', '2021-03-19'),
('DR76', 'Dérailleur Arrière', '2022-02-03', '2022-02-03'),
('DR86', 'Dérailleur Arrière', '2022-02-10', '2022-02-10'),
('DR87', 'Dérailleur Arrière', '2022-03-04', '2022-03-04'),
('DV132', 'Dérailleur Avant', '2022-03-15', '2022-03-15'),
('DV133', 'Dérailleur Avant', '2021-12-29', '2021-12-29'),
('DV15', 'Dérailleur Avant', '2021-12-27', '2021-12-27'),
('DV17', 'Dérailleur Avant', '2022-04-29', '2022-04-29'),
('DV41', 'Dérailleur Avant', '2022-04-01', '2022-04-01'),
('DV57', 'Dérailleur Avant', '2022-03-31', '2022-03-31'),
('DV87', 'Dérailleur Avant', '2022-03-03', '2022-03-03'),
('F3', 'Freins', '2022-07-22', '2022-07-22'),
('F9', 'Freins', '2022-04-21', '2022-04-21'),
('G12', 'Guidon', '2020-01-29', '2020-01-29'),
('G7', 'Guidon', '2022-01-19', '2022-01-19'),
('G9', 'Guidon', '2022-02-03', '2022-02-03'),
('O2', 'Ordinateur', '2022-05-14', '2022-05-14'),
('O4', 'Ordinateur', '2022-01-11', '2022-01-11'),
('P1', 'Pédalier', '2022-04-29', '2022-04-29'),
('P12', 'Pédalier', '2022-03-11', '2022-03-11'),
('P15', 'Pédalier', '2022-02-02', '2022-02-02'),
('P34', 'Pédalier', '2022-05-12', '2022-05-12'),
('R02', 'Réflecteurs', '2022-01-05', '2022-01-05'),
('R09', 'Réflecteurs', '2022-05-05', '2022-05-05'),
('R1', 'Roue Avant', '2022-02-08', '2022-02-08'),
('R10', 'Réflecteurs', '2022-02-05', '2022-02-03'),
('R11', 'Roue Avant', '2022-04-09', '2022-04-09'),
('R12', 'Roue Arrière', '2022-06-16', '2022-06-16'),
('R18', 'Roue Arrière', '2020-02-13', '2020-02-13'),
('R19', 'Roue Avant', '2022-03-18', '2022-03-18'),
('R2', 'Roue Arrière', '2022-03-19', '2022-03-19'),
('R32', 'Roue Arrière', '2021-03-13', '2021-03-13'),
('R44', 'Roue Avant', '2022-05-07', '2022-05-07'),
('R45', 'Roue Avant', '2022-05-05', '2022-05-05'),
('R46', 'Roue Arrière', '2022-01-08', '2022-01-08'),
('R47', 'Roue Arrière', '2019-07-28', '2019-07-28'),
('R48', 'Roue Avant', '2022-05-03', '2022-05-03'),
('S01', 'Panier', '2020-02-13', '2020-02-13'),
('S02', 'Selle', '2022-04-02', '2022-04-02'),
('S03', 'Selle', '2022-04-06', '2022-04-06'),
('S05', 'Panier', '2020-01-29', '2020-01-29'),
('S34', 'Selle', '2022-02-03', '2022-02-03'),
('S35', 'Selle', '2022-02-22', '2022-02-22'),
('S36', 'Selle', '2022-02-02', '2022-02-02'),
('S37', 'Selle', '2022-04-01', '2022-04-01'),
('S73', 'Panier', '2022-02-24', '2022-02-24'),
('S74', 'Panier', '2019-07-18', '2019-07-18'),
('S87', 'Selle', '2022-02-10', '2022-02-10'),
('S88', 'Selle', '2022-03-30', '2022-03-30');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `ACHAT_BICYCLETTE`
--
ALTER TABLE `ACHAT_BICYCLETTE`
  ADD KEY `ID_Commande` (`ID_Commande`),
  ADD KEY `ID_Bicyclette` (`ID_Bicyclette`);

--
-- Index pour la table `ACHAT_PIECE`
--
ALTER TABLE `ACHAT_PIECE`
  ADD KEY `Identifiant_Piece` (`Identifiant_Piece`),
  ADD KEY `ID_Commande` (`ID_Commande`);

--
-- Index pour la table `ASSEMBLER_PAR`
--
ALTER TABLE `ASSEMBLER_PAR`
  ADD KEY `Identifiant_Piece` (`Identifiant_Piece`),
  ADD KEY `ID_Bicyclette` (`ID_Bicyclette`);

--
-- Index pour la table `BICYCLETTE`
--
ALTER TABLE `BICYCLETTE`
  ADD PRIMARY KEY (`ID_Bicyclette`);

--
-- Index pour la table `CLIENT`
--
ALTER TABLE `CLIENT`
  ADD PRIMARY KEY (`ID_Client`),
  ADD KEY `NumProgramme_Fidelio` (`NumProgramme_Fidelio`);

--
-- Index pour la table `COMMANDE`
--
ALTER TABLE `COMMANDE`
  ADD PRIMARY KEY (`ID_Commande`),
  ADD KEY `ID_Client` (`ID_Client`);

--
-- Index pour la table `FIDELIO`
--
ALTER TABLE `FIDELIO`
  ADD PRIMARY KEY (`NumProgramme_Fidelio`);

--
-- Index pour la table `FOURNISSEUR`
--
ALTER TABLE `FOURNISSEUR`
  ADD PRIMARY KEY (`Siret_Fournisseur`);

--
-- Index pour la table `FOURNIT`
--
ALTER TABLE `FOURNIT`
  ADD KEY `Identifiant_Piece` (`Identifiant_Piece`),
  ADD KEY `Siret_Fournisseur` (`Siret_Fournisseur`);

--
-- Index pour la table `PIECE`
--
ALTER TABLE `PIECE`
  ADD PRIMARY KEY (`Identifiant_Piece`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `BICYCLETTE`
--
ALTER TABLE `BICYCLETTE`
  MODIFY `ID_Bicyclette` int(30) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `CLIENT`
--
ALTER TABLE `CLIENT`
  MODIFY `ID_Client` int(30) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pour la table `COMMANDE`
--
ALTER TABLE `COMMANDE`
  MODIFY `ID_Commande` int(30) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `FIDELIO`
--
ALTER TABLE `FIDELIO`
  MODIFY `NumProgramme_Fidelio` int(30) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `ACHAT_BICYCLETTE`
--
ALTER TABLE `ACHAT_BICYCLETTE`
  ADD CONSTRAINT `ACHAT_BICYCLETTE_ibfk_1` FOREIGN KEY (`ID_Commande`) REFERENCES `COMMANDE` (`ID_Commande`) ON DELETE CASCADE,
  ADD CONSTRAINT `ACHAT_BICYCLETTE_ibfk_2` FOREIGN KEY (`ID_Bicyclette`) REFERENCES `BICYCLETTE` (`ID_Bicyclette`) ON DELETE CASCADE;

--
-- Contraintes pour la table `ACHAT_PIECE`
--
ALTER TABLE `ACHAT_PIECE`
  ADD CONSTRAINT `ACHAT_PIECE_ibfk_1` FOREIGN KEY (`Identifiant_Piece`) REFERENCES `PIECE` (`Identifiant_Piece`) ON DELETE CASCADE,
  ADD CONSTRAINT `ACHAT_PIECE_ibfk_2` FOREIGN KEY (`ID_Commande`) REFERENCES `COMMANDE` (`ID_Commande`) ON DELETE CASCADE;

--
-- Contraintes pour la table `ASSEMBLER_PAR`
--
ALTER TABLE `ASSEMBLER_PAR`
  ADD CONSTRAINT `ASSEMBLER_PAR_ibfk_1` FOREIGN KEY (`Identifiant_Piece`) REFERENCES `PIECE` (`Identifiant_Piece`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `ASSEMBLER_PAR_ibfk_2` FOREIGN KEY (`ID_Bicyclette`) REFERENCES `BICYCLETTE` (`ID_Bicyclette`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `CLIENT`
--
ALTER TABLE `CLIENT`
  ADD CONSTRAINT `CLIENT` FOREIGN KEY (`NumProgramme_Fidelio`) REFERENCES `FIDELIO` (`NumProgramme_Fidelio`);

--
-- Contraintes pour la table `COMMANDE`
--
ALTER TABLE `COMMANDE`
  ADD CONSTRAINT `COMMANDE` FOREIGN KEY (`ID_Client`) REFERENCES `CLIENT` (`ID_Client`);

--
-- Contraintes pour la table `FOURNIT`
--
ALTER TABLE `FOURNIT`
  ADD CONSTRAINT `FOURNIT_ibfk_1` FOREIGN KEY (`Identifiant_Piece`) REFERENCES `PIECE` (`Identifiant_Piece`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FOURNIT_ibfk_2` FOREIGN KEY (`Siret_Fournisseur`) REFERENCES `FOURNISSEUR` (`Siret_Fournisseur`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
