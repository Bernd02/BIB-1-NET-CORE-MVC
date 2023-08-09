-- Operatingsystemen
use BIB
go
insert into Operatingsystemen (naam) values 
('Android70'),
('Android60'),
('iOS'),
('Windows 10'),
('Windows 8.1')

insert into Leners (Voornaam, Familienaam, Adres, Telefoonnr, Geboortedatum) values
('Steven', 'Lucas', 'Acacialaan 1', '050-352687', '1969-12-21'),
('Hans', 'Desmet', 'Beukenhof 2', '051-215863', '1966-08-01'),
('Jan', 'Detavernier', 'Cederstraat 3', '052-548554', '1980-08-10'),
('Tom', 'Vanlerberghe', 'Dennendreef 4', '053-145541', '1982-11-26')

insert into Uitleenobjecten (Discriminator, Naam, Jaar, Kostprijs, ImageUrl, Status, Aantalpaginas, Auteur, ISBN, OperatingsysteemId, Schermgrootte) values
('Boek', 'Pro ASP.NET Core MVC', 2016, 44.99, '/images/9781484203972.jpg', 0, 700, 'Adam Freeman', '978-1-4842-0397-2', NULL, NULL),
('Boek', 'Programming ASP.NET Core', 2018, 49.99, '/images/9781509304417.jpg', 0, 496, 'Dino Esposito', '978-1-5093-0441-7', NULL, NULL),
('Device', 'Galaxy Tab S2', 2016, 365.00, '/images/galaxytabs2.jpg', 0, null, null, null, 1, 9.7),
('Device', 'Nokia Lumia 650', 2016, 180.00, '/images/lumia650.jpg', 0, null, null, null, 4, 5.0)


