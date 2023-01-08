truncate table CarAds;
delete from Owners;
dbcc checkident ('[CarSelling.Data].dbo.Owners', RESEED, 0);