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