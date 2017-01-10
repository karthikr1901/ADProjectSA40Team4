
Insert into Item values ('S042','Scissors',50,20,'Each',5,20,11)
Insert into Item values ('S043','Sharpener',50,20,'Each',5,20,13)
Insert into Item values ('T026','Trays In/Out',20,10,'Set',5,200,18)


update SupplyItem set ItemID = 'S042' where ItemID = 'S100'
update SupplyItem set ItemID = 'S043' where ItemID = 'S101'
update SupplyItem set ItemID = 'T026' where ItemID = 'T100'

DELETE FROM ITEM WHERE ItemID = 'S100'
DELETE FROM ITEM WHERE ItemID = 'S101'
DELETE FROM ITEM WHERE ItemID = 'T100'