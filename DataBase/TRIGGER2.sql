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