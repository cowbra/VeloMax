DROP DATABASE IF EXISTS `VeloMax` ;
CREATE DATABASE `VeloMax`;
USE `VeloMax`;

CREATE TABLE `BICYCLETTE` (
 `ID_Bicyclette` int(30) NOT NULL AUTO_INCREMENT,
 `Nom_Bicyclette` varchar(255) NOT NULL,
 `Grandeur_Bicyclette` enum('Adultes','Jeunes','Hommes','Dames','Filles','Garçons') NOT NULL,
 `Prix_Bicyclette` double NOT NULL,
 `Type_Bicyclette` enum('VTT','Course','Classique','BMX') NOT NULL,
 `DateIntroduction_Bicyclette` date NOT NULL,
 `DateFin_Bicyclette` date NOT NULL,
 PRIMARY KEY (`ID_Bicyclette`)
);

CREATE TABLE `FIDELIO` (
 `NumProgramme_Fidelio` int(30) NOT NULL AUTO_INCREMENT,
 `Description_Fidelio` varchar(255) NOT NULL,
 `Cout_Fidelio` int(30) NOT NULL,
 `Duree_Fidelio` int(30) NOT NULL,
 `Rabais_Fidelio` double NOT NULL,
 PRIMARY KEY (`NumProgramme_Fidelio`)
);

CREATE TABLE `FOURNISSEUR` (
 `Siret_Fournisseur` bigint NOT NULL,
 `NomEntreprise_Fournisseur` varchar(255) NOT NULL,
 `Contact_Fournisseur` varchar(255) NOT NULL,
 `Adresse_Fournisseur` varchar(255) NOT NULL,
 `Libelle_Fournisseur` varchar(255) NOT NULL,
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
 `NomCompagnie_Client` varchar(255) DEFAULT NULL,
 `RemiseCompagnie_Client` double DEFAULT NULL,
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
