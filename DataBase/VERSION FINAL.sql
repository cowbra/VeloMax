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
 `Libelle_Fournisseur` enum('1','2','3','4') NOT NULL,
 PRIMARY KEY (`Siret_Fournisseur`)
);

CREATE TABLE `PIECE` (
 `Identifiant_Piece` varchar(15) NOT NULL,
 `Description_Piece` enum('Cadre','Guidon','Freins','Selle','Dérailleur Avant','Dérailleur Arrière','Roue Avant','Roue Arrière','Réflecteurs','Pédalier','Ordinateur','Panier') NOT NULL,
 `DateDebut_Piece` date NOT NULL,
 `DateFin_Piece` date NOT NULL,
 PRIMARY KEY (`Identifiant_Piece`)
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
 `Identifiant_Piece` varchar(15) NOT NULL,
 `NombreArticles` int(30) NOT NULL,
 `DateLivraison` date NOT NULL,
 FOREIGN KEY (`Identifiant_Piece`) REFERENCES `PIECE` (`Identifiant_Piece`),
 FOREIGN KEY (`ID_Commande`) REFERENCES `COMMANDE` (`ID_Commande`)
);

CREATE TABLE `ASSEMBLER_PAR` (
 `ID_Bicyclette` int(30) NOT NULL,
 `Identifiant_Piece` varchar(15) NOT NULL,
 FOREIGN KEY (`Identifiant_Piece`) REFERENCES `PIECE` (`Identifiant_Piece`),
 FOREIGN KEY (`ID_Bicyclette`) REFERENCES `BICYCLETTE` (`ID_Bicyclette`)
);

CREATE TABLE `FOURNIT` (
 `Siret_Fournisseur` bigint NOT NULL,
 `Identifiant_Piece` varchar(15) NOT NULL,
 `Nom_Fournisseur` varchar(255) NOT NULL,
 `NumProduit_Fournisseur` int(30) NOT NULL,
 `Prix_Fournisseur` double NOT NULL,
 `Quantite_Fournisseur` int(30) NOT NULL,
 `Delai_Fournisseur` int(30) NOT NULL,
 FOREIGN KEY (`Identifiant_Piece`) REFERENCES `PIECE` (`Identifiant_Piece`),
 FOREIGN KEY (`Siret_Fournisseur`) REFERENCES `FOURNISSEUR` (`Siret_Fournisseur`)
);

DELIMITER $$
create trigger CLIENT_DateFin_Fidelio
before insert on CLIENT
for each row begin
  declare duree int(11);

  IF NEW.NumProgramme_Fidelio IS NOT NULL
  THEN
    select Duree_Fidelio into duree from FIDELIO
    where NumProgramme_Fidelio = NEW.NumProgramme_Fidelio;
    set NEW.DateFin_Fidelio = DATE_ADD(NEW.DateDebut_Fidelio, INTERVAL duree YEAR);
  END IF;
end$$
DELIMITER ;

DELIMITER $$
create trigger FOURNIT_NomFournisseur
before insert on FOURNIT
for each row begin
  declare NomF Varchar(255);

  select NomEntreprise_Fournisseur into NomF from FOURNISSEUR
  where Siret_Fournisseur = NEW.Siret_Fournisseur;
  set NEW.Nom_Fournisseur = NomF;
end$$
DELIMITER ;

INSERT INTO `FIDELIO` (`NumProgramme_Fidelio`, `Description_Fidelio`, `Cout_Fidelio`, `Duree_Fidelio`, `Rabais_Fidelio`) VALUES
(1, 'Fidélio', 15, 1, 0.05),
(2, 'Fidélio Or', 25, 2, 0.08),
(3, 'Fidélio Platine', 60, 2, 0.1),
(4, 'Fidélio Max', 100, 3, 0.12);

INSERT INTO `FOURNISSEUR` (`Siret_Fournisseur`, `NomEntreprise_Fournisseur`, `Contact_Fournisseur`, `Adresse_Fournisseur`, `Libelle_Fournisseur`) VALUES
(39982698100017, 'BricoVelo', 'bricovelo@velo.com', '14 rue Victor Hugo, Argentan', '1'),
(67822698100036, 'VeloFabrik', 'velofabrik@gmail.com', '4 rue Carnot, Lille', '3'),

(43282695524021, 'Bikeshop', 'bikeshop@gmail.com', '6 rue André, Paris', '2'),
(27382698187536, 'PiecesFabrique', 'piecesfabrique@gmail.com', '9 avenue Richelieu, Caen', '4'),
(27589698100017, 'Bikebuy', 'bikebuy@velo.com', '8 Avenue General Lecler, Calais', '1'),
(64189454720036, '2roues', '2roues@gmail.com', '45 rue du temple, Nogent', '2'),
(64126254620004, 'roadsport', 'roadsport@gmail.com', '6 rue St Louis, Paris', '2'),
(87551075487408, 'roadtogo', 'roadtogo@gmail.com', '2 avenue de la piscine, Caen', '4');

INSERT INTO `CLIENT` (`ID_Client`, `Type_Client`, `Tel_Client`, `Courriel_Client`, `Adresse_Client`, `Nom_Client`, `Prenom_Client`, `NomCompagnie_Client`, `RemiseCompagnie_Client`, `NumProgramme_Fidelio`, `DateDebut_Fidelio`, `DateFin_Fidelio`) VALUES
(1, 'Particulier', '0685324585', 'hugo@test.fr', '41 rue Jobin\r\n13003 Marseille', 'bob', 'hugo', NULL, NULL, 3, '2022-04-07', NULL),
(2, 'Particulier', '0632544489', 'rufus@gmail.com', '19 rue des Augustins\r\n33000 Bordeaux', 'Chemine', 'Rufus', NULL, NULL, 4, '2022-04-14', NULL),
(3, 'Entreprise', '0147855472', 'Dekatlon@free.fr', '108, rue de rhodes\r\n34000 Montpellier', 'Ben', NULL, 'Dekatlon', 0.1, NULL, NULL, NULL),
(4, 'Entreprise', '0346580184', '1tersport@gmail.com', '14, rue de joncquilles\r\n34000 Lille', 'Philippe', NULL, '1tersp0rt', 0.15, NULL, NULL, NULL),
(5, 'Entreprise', '0784949901', 'g0sp0rt@gmail.com', '4, rue Carnot \r\n34000 Paris', 'Emmanuel', NULL, 'g0sp0rt', 0.12, NULL, NULL, NULL),
(6, 'Particulier', '0978440144', 'pierre@test.fr', '7 rue de l eglise\r\n13003 Cambray', 'Thalis', 'Pierre', NULL, NULL, 1, '2022-01-03', NULL);

INSERT INTO `CLIENT` (`ID_Client`, `Type_Client`, `Tel_Client`, `Courriel_Client`, `Adresse_Client`, `Nom_Client`, `Prenom_Client`, `NomCompagnie_Client`, `RemiseCompagnie_Client`, `NumProgramme_Fidelio`, `DateDebut_Fidelio`, `DateFin_Fidelio`) VALUES
(1, 'Particulier', '0685324585', 'hugo@test.fr', '41 rue Jobin\r\n13003 Marseille', 'bob', 'hugo', NULL, NULL, 1, '2022-04-07', NULL),
(2, 'Particulier', '0632544489', 'rufus@gmail.com', '19 rue des Augustins\r\n33000 Bordeaux', 'Chemine', 'Rufus', NULL, NULL, 4, '2022-04-14', NULL),
(3, 'Entreprise', '0147855472', 'Dekatlon@free.fr', '108, rue de rhodes\r\n34000 Montpellier', 'Ben', NULL, 'RentaBikke', 0.1, NULL, NULL, NULL);
