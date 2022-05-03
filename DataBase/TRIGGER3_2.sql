DELIMITER $$
create trigger QUANTITE_Totale_Piece_2
after update on FOURNIT
for each row begin
  declare quantity_totale int(11);

  SELECT SUM(Quantite_Fournisseur) into quantity_totale FROM FOURNIT WHERE Identifiant_Piece = UPDATED.Identifiant_Piece;
  UPDATE PIECE SET QuantiteTotale_Piece = quantity_totale WHERE Identifiant_Piece = UPDATED.Identifiant_Piece;
end$$
DELIMITER ;