Corporate Solutions tehniskais uzdevuma rezultāta apraksts
Taisīts uz Visual Studio 2022

Uzstādīšana:
1.	Atvērt projektu visual studio
2.	MS SQL servera datubāzē izlaist init_db.sql failu
3.	CstestdbContext.cs klasē izmainīt servera nosaukumu no localhost uz konkrētā datora servera nosaukumu.


Piezīmes:
Galvenā mājas lapa atrodas CSTest, datubāze CSTest.Db, unit tests CSTest.Test un webserviss CStest.WebAPI.

Lai palaistu webservisu, CStest.WebAPI jāuzliek kā startup projekts. Testēt to var no swaggerUI.

PVN vērtība atrodas CSTest projekta appsettings.json failā. Noklusētā vērtība ir “3,4”.

Login ar lomām neesmu taisījusi tāpēc uztaisīju vienkāršu login lapu kas pārbauda datus pēc datubāzes. Internetā meklēju bet nevarēju tik ātri iebraukt. Atstāju dažās vietās kometārus kā tas izskatītos ar Identity ielogošanos.

Zinu ka unit testa pārbaudīšana varētu būt labāka (tā, lai tā izmanto ne-statisku metodi), bet pieredze to rakstīšanā ir maza.

Neesmu strādājusi ar migrācijām (pieļauju ka te ir domāts ar Add-Migration), tāpēc izmantoju 
Scaffold-DbContext "Server=localhost;Database=cstestdb;persist security info=True;TrustServerCertificate=True;user id=csTest;password=testdb1" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –Force
lai ģenerētu datubāzi.
