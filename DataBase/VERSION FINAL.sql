--SUPPRESSION DE LA BDD SI EXISTANTE PUIS CREATION
DROP DATABASE IF EXISTS `VeloMax` ;
CREATE DATABASE `VeloMax`;
USE `VeloMax`;

--CREATION DE NOS TABLES
CREATE TABLE `BICYCLETTE` (
 `ID_Bicyclette` int(30) NOT NULL AUTO_INCREMENT,
 `Nom_Bicyclette` varchar(255) NOT NULL,
 `Grandeur_Bicyclette` int(30) NOT NULL,
 `Prix_Bicyclette` double NOT NULL,
 `Type_Bicyclette` enum('VTT','Course','Classique','BMX') NOT NULL,
 `DateIntroduction_Bicyclette` date NOT NULL,
 `DateFin_Bicyclette` date NOT NULL,
 PRIMARY KEY (`ID_Bicyclette`)
);

CREATE TABLE `FIDELIO` (
 `NumProgramme_Fidelio` int(30) NOT NULL AUTO_INCREMENT,
 `Description_Fidelio` varchar(255) NOT NULL,
 `Cout_Fidelio` double NOT NULL,
 `Duree_Fidelio` int(30) NOT NULL,
 `Rabais_Fidelio` double NOT NULL,
 PRIMARY KEY (`NumProgramme_Fidelio`)
);

CREATE TABLE `FOURNISSEUR` (
 `Siret_Fournisseur` bigint NOT NULL,
 `NomEntreprise_Fournisseur` varchar(255) NOT NULL,
 `Contact_Fournisseur` varchar(255) NOT NULL,
 `Adresse_Fournisseur` varchar(255) NOT NULL,
 `Libelle_Fournisseur` enum('1','2','3','4') NOT NULL,
 PRIMARY KEY (`Siret_Fournisseur`)
);

CREATE TABLE `PIECE` (
 `NumProduit_Piece` int(30) NOT NULL AUTO_INCREMENT,
 `Description_Piece` varchar(255) NOT NULL,
 `DateDebut_Piece` date NOT NULL,
 `DateFin_Piece` date NOT NULL,
 PRIMARY KEY (`NumProduit_Piece`)
);

CREATE TABLE `CLIENT` (
 `ID_Client` int(30) NOT NULL AUTO_INCREMENT,
 `Type_Client` enum('Particulier','Entreprise') NOT NULL,
 `Tel_Client` varchar(255) NOT NULL,
 `Courriel_Client` varchar(255) NOT NULL,
 `Adresse_Client` varchar(255) NOT NULL,
 `Nom_Client` varchar(255) NOT NULL,
 `Prenom_Client` varchar(255) DEFAULT NULL,
 `NomCompagnie_Client` int(30) DEFAULT NULL,
 `RemiseCompagnie_Client` int(30) DEFAULT NULL,
 `NumProgramme_Fidelio` int(30) DEFAULT NULL,
 `DateDebut_Fidelio` date DEFAULT NULL,
 `DateFin_Fidelio` date DEFAULT NULL,
 PRIMARY KEY (`ID_Client`),
 KEY `NumProgramme_Fidelio` (`NumProgramme_Fidelio`),
 CONSTRAINT `CLIENT` FOREIGN KEY (`NumProgramme_Fidelio`) REFERENCES `FIDELIO` (`NumProgramme_Fidelio`)
);

CREATE TABLE `COMMANDE` (
 `ID_Commande` int(30) NOT NULL AUTO_INCREMENT,
 `Date_Commande` date NOT NULL,
 `AdresseLivraison_Commande` varchar(255) NOT NULL,
 `ID_Client` int(30) NOT NULL,
 PRIMARY KEY (`ID_Commande`),
 KEY `ID_Client` (`ID_Client`),
 CONSTRAINT `COMMANDE` FOREIGN KEY (`ID_Client`) REFERENCES `CLIENT` (`ID_Client`)
);


CREATE TABLE `ACHAT_BICYCLETTE` (
 `ID_Commande` int(30) NOT NULL,
 `ID_Bicyclette` int(30) NOT NULL,
 `NombreArticles` int(30) NOT NULL,
 `DateLivraison` date NOT NULL,
 FOREIGN KEY (`ID_Commande`) REFERENCES `COMMANDE` (`ID_Commande`),
 FOREIGN KEY (`ID_Bicyclette`) REFERENCES `BICYCLETTE` (`ID_Bicyclette`)
);

CREATE TABLE `ACHAT_PIECE` (
 `ID_Commande` int(30) NOT NULL,
 `NumProduit_Piece` int(30) NOT NULL,
 `NombreArticles` int(30) NOT NULL,
 `DateLivraison` date NOT NULL,
 FOREIGN KEY (`NumProduit_Piece`) REFERENCES `PIECE` (`NumProduit_Piece`),
 FOREIGN KEY (`ID_Commande`) REFERENCES `COMMANDE` (`ID_Commande`)
);

CREATE TABLE `ASSEMBLER_PAR` (
 `ID_Bicyclette` int(30) NOT NULL,
 `NumProduit_Piece` int(30) NOT NULL,
 FOREIGN KEY (`NumProduit_Piece`) REFERENCES `PIECE` (`NumProduit_Piece`),
 FOREIGN KEY (`ID_Bicyclette`) REFERENCES `BICYCLETTE` (`ID_Bicyclette`)
);

CREATE TABLE `FOURNIT` (
 `Siret_Fournisseur` bigint NOT NULL,
 `NumProduit_Piece` int(30) NOT NULL,
 `Nom_Fournisseur` varchar(255) NOT NULL,
 `NumProduit_Fournisseur` int(30) NOT NULL,
 `Prix_Fournisseur` double NOT NULL,
 `Quantite_Fournisseur` int(30) NOT NULL,
 `Delai_Fournisseur` int(30) NOT NULL,
 FOREIGN KEY (`NumProduit_Piece`) REFERENCES `PIECE` (`NumProduit_Piece`),
 FOREIGN KEY (`Siret_Fournisseur`) REFERENCES `FOURNISSEUR` (`Siret_Fournisseur`)
);

--INSERTION DES VALEURS
INSERT INTO `FIDELIO` (`NumProgramme_Fidelio`, `Description_Fidelio`, `Cout_Fidelio`, `Duree_Fidelio`, `Rabais_Fidelio`) VALUES
(1, 'Fidélio', 15, 1, 0.05),
(2, 'Fidélio Or', 25, 2, 0.08),
(3, 'Fidélio Platine', 60, 2, 0.1),
(4, 'Fidélio Max', 100, 3, 0.12);

INSERT INTO `FOURNISSEUR` (`Siret_Fournisseur`, `NomEntreprise_Fournisseur`, `Contact_Fournisseur`, `Adresse_Fournisseur`, `Libelle_Fournisseur`) VALUES
(39982698100017, 'BricoVelo', 'bricovelo@velo.com', '14 rue Victor Hugo, Argentan', '1'),
(67822698100036, 'VeloFabrik', 'velofabrik@gmail.com', '4 rue Carnot, Lille', '3'),
(43282695524021, 'Bikeshop', 'bikeshop@gmail.com', '6 rue André, Paris', '12'),
(27382698187536, 'PiecesFabrique', 'piecesfabrique@gmail.com', '9 avenue Richelieu, Caen', '4');

INSERT INTO `CLIENT` (`ID_Client`, `Type_Client`, `Tel_Client`, `Courriel_Client`, `Adresse_Client`, `Nom_Client`, `Prenom_Client`, `NomCompagnie_Client`, `RemiseCompagnie_Client`, `NumProgramme_Fidelio`, `DateDebut_Fidelio`, `DateFin_Fidelio`) VALUES
(1, 'Particulier', '0685324585', 'hugo@test.fr', '41 rue Jobin\r\n13003 Marseille', 'bob', 'hugo', NULL, NULL, 1, '2022-04-07', NULL),
(2, 'Particulier', '0632544489', 'rufus@gmail.com', '19 rue des Augustins\r\n33000 Bordeaux', 'Chemine', 'Rufus', NULL, NULL, 4, '2022-04-14', NULL),
(3, 'Entreprise', '0147855472', 'Dekatlon@free.fr', '108, rue de rhodes\r\n34000 Montpellier', 'Ben', NULL, 'RentaBikke', 0.1, NULL, NULL, NULL);
