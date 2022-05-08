-- phpMyAdmin SQL Dump
-- version 5.0.4deb2
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : Dim 08 mai 2022 à 10:54
-- Version du serveur :  10.5.15-MariaDB-0+deb11u1
-- Version de PHP : 7.4.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `VeloMax`
--
CREATE DATABASE IF NOT EXISTS `VeloMax` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `VeloMax`;

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
(27589698100017, 'C02', 'Bikebuy', 854, 72.45, 1, 87),
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
(27589698100017, 'C01', 'Bikebuy', 47, 10, 4, 5);

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
('C02', 'Cadre', '2020-01-29', '2020-01-29'),
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
('S02', 'Selle', '2022-04-02', '2022-04-02'),
('S03', 'Selle', '2022-04-06', '2022-04-06'),
('S34', 'Selle', '2022-02-03', '2022-02-03'),
('S35', 'Selle', '2022-02-22', '2022-02-22'),
('S36', 'Selle', '2022-02-02', '2022-02-02'),
('S37', 'Selle', '2022-04-01', '2022-04-01'),
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
  MODIFY `ID_Bicyclette` int(30) NOT NULL AUTO_INCREMENT;

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
  ADD CONSTRAINT `ACHAT_BICYCLETTE_ibfk_1` FOREIGN KEY (`ID_Commande`) REFERENCES `COMMANDE` (`ID_Commande`),
  ADD CONSTRAINT `ACHAT_BICYCLETTE_ibfk_2` FOREIGN KEY (`ID_Bicyclette`) REFERENCES `BICYCLETTE` (`ID_Bicyclette`);

--
-- Contraintes pour la table `ACHAT_PIECE`
--
ALTER TABLE `ACHAT_PIECE`
  ADD CONSTRAINT `ACHAT_PIECE_ibfk_1` FOREIGN KEY (`Identifiant_Piece`) REFERENCES `PIECE` (`Identifiant_Piece`),
  ADD CONSTRAINT `ACHAT_PIECE_ibfk_2` FOREIGN KEY (`ID_Commande`) REFERENCES `COMMANDE` (`ID_Commande`);

--
-- Contraintes pour la table `ASSEMBLER_PAR`
--
ALTER TABLE `ASSEMBLER_PAR`
  ADD CONSTRAINT `ASSEMBLER_PAR_ibfk_1` FOREIGN KEY (`Identifiant_Piece`) REFERENCES `PIECE` (`Identifiant_Piece`),
  ADD CONSTRAINT `ASSEMBLER_PAR_ibfk_2` FOREIGN KEY (`ID_Bicyclette`) REFERENCES `BICYCLETTE` (`ID_Bicyclette`);

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
