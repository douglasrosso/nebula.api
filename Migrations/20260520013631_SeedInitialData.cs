using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nebula.api.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
INSERT INTO public."Users" ("Id","Name","Email","Password","CreatedAt","UpdatedAt","Avatar","Badges","Bio","Country","DisplayName","FriendCount","Level","Username","Xp") VALUES
    ('90129338-75d3-4e4e-a4da-140caf241b85','Douglas','douglas@gmail.com','AQAAAAIAAYagAAAAEJ+8pN4LJiLVdLswtRtS6xVLxFTm0hwPMwFv2qy7e95lW/t+AJtwQy2wg9wdA+9xQg==','2026-05-13 22:48:48.041406-03','2026-05-13 22:48:48.041406-03',NULL,'{}',NULL,NULL,'Douglas',0,0,'douglas',8000),
    ('b2cca143-e9a4-4987-b3b4-09198a7422de','Anderson','anderson@gmail.com','AQAAAAIAAYagAAAAEJ+8pN4LJiLVdLswtRtS6xVLxFTm0hwPMwFv2qy7e95lW/t+AJtwQy2wg9wdA+9xQg==','2026-05-13 22:48:48.041406-03','2026-05-13 22:48:48.041406-03',NULL,'{}',NULL,NULL,'Anderson',0,0,'anderson',0)
""");

            migrationBuilder.Sql("""
INSERT INTO public."Genres" ("Id","Name","Slug","CreatedAt","UpdatedAt") VALUES
    ('127c1edb-abe2-494f-8da9-7086b2aeecd0','Mundo Aberto','mundo-aberto','2026-05-16 13:25:52.70031-03','2026-05-16 13:25:52.70031-03'),
    ('83f2abec-7b8f-45d6-8ed8-07dda0b6944a','Acao','acao','2026-05-16 13:25:52.687027-03','2026-05-16 13:25:52.687027-03'),
    ('b0815b22-8470-4be0-bf1b-06aed624c697','RPG','rpg','2026-05-16 13:25:52.698319-03','2026-05-16 13:25:52.698319-03'),
    ('8e0e4778-b1d2-4817-8f6c-9c3340f7c174','FPS','fps','2026-05-16 13:26:21.466862-03','2026-05-16 13:26:21.466862-03'),
    ('ebd69131-ff81-40f5-b3ad-88584710d56c','Aventura','aventura','2026-05-16 13:26:21.52074-03','2026-05-16 13:26:21.52074-03'),
    ('dc87d826-92b0-43d7-b850-5fee61bcfa22','Indie','indie','2026-05-16 13:26:50.64343-03','2026-05-16 13:26:50.64343-03'),
    ('3bab1cb5-1f8e-4be3-a9b3-c614035a7928','Simulador','simulador','2026-05-16 13:27:16.2528-03','2026-05-16 13:27:16.2528-03'),
    ('0c5f574c-9a13-4e35-87c4-985cc2348e3c','Estrategia','estrategia','2026-05-16 13:27:16.302584-03','2026-05-16 13:27:16.302584-03');
""");

            migrationBuilder.Sql("""
INSERT INTO public."Games" ("Id","Title","Description","LongDescription","Price","OriginalPrice","Discount","CoverImage","Screenshots","Developer","Publisher","ReleaseDate","Tags","Features","Rating","ReviewCount","PositivePercentage","IsActive","CreatedAt","UpdatedAt","SystemRequirements") VALUES
    ('29cffff9-2aa0-4357-9720-1b26a92e79bb','Elden Ring','Um RPG de acao epico ambientado em um mundo de fantasia sombria criado por Hidetaka Miyazaki e George R.R. Martin.','Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between. A vast world where open fields with a variety of situations and huge dungeons are seamlessly connected.',249.90,299.90,17,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1245620/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1245620/ss_943bf6fe62352757d9070c1d33e50b92fe8539f1.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1245620/ss_dcdac9e4b26ac0ee5248bfd2967d764fd00cdb42.1920x1080.jpg}','FromSoftware Inc.','Bandai Namco Entertainment','2022-02-25','{Souls-like,Fantasia Sombria,Dificil}','{Um jogador,Cooperativo Online,PvP Online,Conquistas}',4.80,524789,93,true,'2026-05-16 13:25:52.701153-03','2026-05-16 13:25:52.701153-03','{"Minimum": {"Os": "Windows 10", "Memory": "12 GB RAM", "Storage": "60 GB", "Graphics": "GTX 1060 3GB", "Processor": "Intel Core i5-8400"}, "Recommended": {"Os": "Windows 10/11", "Memory": "16 GB RAM", "Storage": "60 GB SSD", "Graphics": "GTX 1070 8GB", "Processor": "Intel Core i7-8700K"}}'),
    ('434abf02-1172-4d34-8e8d-b503ee4f72c4','Cyberpunk 2077','Um RPG de mundo aberto ambientado em Night City, uma megalopole obcecada por poder, glamour e modificacoes corporais.','Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City — a dangerous megalopolis obsessed with power, glamour and body modification. You play as V, a mercenary outlaw going after a one-of-a-kind implant that is the key to immortality.',149.90,249.90,40,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1091500/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1091500/ss_2f649b68d579bf87011487d29bc4ccbfdd97d34f.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1091500/ss_0e64170751e1ae20ff8fdb7001a8892fd48260e7.1920x1080.jpg}','CD Projekt Red','CD Projekt','2020-12-10','{Cyberpunk,Mundo Aberto,Personalizado}','{Um jogador,Conquistas,Suporte a controlador}',4.60,389215,84,true,'2026-05-16 13:26:21.467338-03','2026-05-16 13:26:21.467338-03','{"Minimum": {"Os": "Windows 10", "Memory": "12 GB RAM", "Storage": "70 GB SSD", "Graphics": "GTX 1060 6GB", "Processor": "Intel Core i7-6700K"}, "Recommended": {"Os": "Windows 10/11", "Memory": "16 GB RAM", "Storage": "70 GB SSD", "Graphics": "RTX 2080 Super", "Processor": "Intel Core i7-8700K"}}'),
    ('7b872d46-625f-4ce7-b27d-71819897c450','Red Dead Redemption 2','Uma epica aventura no velho oeste americano. Arthur Morgan e o bando Van der Linde tentam sobreviver em uma era que se fecha para foras da lei.','America, 1899. Arthur Morgan and the Van der Linde gang are outlaws on the run. With federal agents and the best bounty hunters in the nation massing on their heels, the gang must rob, steal and fight their way across the rugged heartland of America.',189.90,NULL,NULL,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1174180/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1174180/ss_66b553f4c209476d3e4ce25fa4714002cc914c4f.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1174180/ss_bac60bacbf5da8945103648c08d27d5e202444ca.1920x1080.jpg}','Rockstar Games','Rockstar Games','2019-12-05','{Velho Oeste,Historia,Sobrevivencia}','{Um jogador,Multijogador Online,Conquistas}',4.90,712456,97,true,'2026-05-16 13:26:21.521842-03','2026-05-16 13:26:21.521842-03','{"Minimum": {"Os": "Windows 10", "Memory": "8 GB RAM", "Storage": "150 GB SSD", "Graphics": "GTX 770 2GB", "Processor": "Intel Core i5-2500K"}, "Recommended": {"Os": "Windows 10", "Memory": "16 GB RAM", "Storage": "150 GB SSD", "Graphics": "GTX 1080 Ti", "Processor": "Intel Core i7-4770K"}}'),
    ('f3105b0f-86bf-4684-a3a3-a20ef5e7e125','The Witcher 3: Wild Hunt','Um RPG de mundo aberto premiado. Jogue como Geralt de Rivia, um caca-monstros mercenario em busca de sua filha adotiva.','You are Geralt of Rivia, mercenary monster slayer. Before you stands a war-torn, monster-infested continent you can explore at will. Your current contract? Tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.',59.90,119.90,50,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/292030/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/292030/ss_5710298af2318afd9aa72449ef29ac4a2ef64d8e.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/292030/ss_0901e64e9d4b8ebaea8348c194e7a3644d2d832d.1920x1080.jpg}','CD Projekt Red','CD Projekt','2015-05-19','{Fantasia,Historia,Escolhas Morais}','{Um jogador,Conquistas,Suporte a controlador}',4.90,248931,97,true,'2026-05-16 13:26:21.573883-03','2026-05-16 13:26:21.573883-03','{"Minimum": {"Os": "Windows 7/8/8.1/10", "Memory": "6 GB RAM", "Storage": "35 GB", "Graphics": "GTX 660", "Processor": "Intel Core i5-2500K"}, "Recommended": {"Os": "Windows 7/8/8.1/10", "Memory": "8 GB RAM", "Storage": "35 GB", "Graphics": "GTX 770", "Processor": "Intel Core i7-3770"}}'),
    ('eeef11b7-f01f-4c35-b12b-e72f4dd03728','Hollow Knight','Um desafiador e belo metroidvania ambientado em um vasto reino subterraneo de insetos e herois.','Forge your own path in Hollow Knight! An epic action adventure through a vast ruined kingdom of insects and heroes. Explore twisting caverns, battle tainted creatures and befriend bizarre bugs, all in a classic, hand-drawn 2D style.',29.90,NULL,NULL,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/367520/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/367520/ss_5384f9f8b96a0b9934b2bc35a4058376211636d2.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/367520/ss_d5b6edd94e77ba6db31c44d8a3c09d807ab27751.1920x1080.jpg}','Team Cherry','Team Cherry','2017-02-24','{Metroidvania,Plataforma,Dificil}','{Um jogador,Conquistas,Suporte a controlador}',4.90,156789,98,true,'2026-05-16 13:26:50.643598-03','2026-05-16 13:26:50.643598-03','{"Minimum": {"Os": "Windows 7", "Memory": "4 GB RAM", "Storage": "9 GB", "Graphics": "GeForce 9800GTX+", "Processor": "Intel Core 2 Duo E5200"}, "Recommended": {"Os": "Windows 10", "Memory": "8 GB RAM", "Storage": "9 GB", "Graphics": "GeForce GTX 560", "Processor": "Intel Core i5"}}'),
    ('bb461beb-a1b1-41f7-872e-38115232b584','God of War','Kratos, o Deus da Guerra grego, embarca em uma jornada com seu filho Atreus pelo reino nordico dos deuses.','His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods and monsters. It is in this harsh, unforgiving world that he must fight to survive and teach his son to do the same.',199.90,NULL,NULL,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1593500/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1593500/ss_6eccc970b5de2943546d93d319be1b5c0618f21b.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1593500/ss_f1bff24d3967a21d303d95e11ed892e3d9113057.1920x1080.jpg}','Santa Monica Studio','PlayStation PC LLC','2022-01-14','{Mitologia,Combate,Historia}','{Um jogador,Conquistas,Suporte a controlador}',4.90,89432,96,true,'2026-05-16 13:26:50.69266-03','2026-05-16 13:26:50.69266-03','{"Minimum": {"Os": "Windows 10 64-bit", "Memory": "8 GB RAM", "Storage": "70 GB SSD", "Graphics": "NVIDIA GTX 1060 (6GB)", "Processor": "Intel i5-6600k"}, "Recommended": {"Os": "Windows 10 64-bit", "Memory": "16 GB RAM", "Storage": "70 GB SSD", "Graphics": "NVIDIA RTX 3080", "Processor": "Intel i9-9900K"}}'),
    ('9a410b66-4fab-4876-954c-893fe6e4e38b','Hades','Um roguelike de acao onde voce encarna Zagreus, filho do deus do submundo, tentando escapar do reino de seu pai.','Hades is a god-like rogue-like dungeon crawler that combines the best aspects of Supergiant''s critically acclaimed titles, including the fast-paced action of Bastion, the rich atmosphere and depth of Transistor, and the character-driven storytelling of Pyre.',44.90,59.90,25,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1145360/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1145360/ss_c0fed447426b69981cf1721756acf75369801b31.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1145360/ss_8a9f0953e8a014bd3df2789c2835cb787cd3764d.1920x1080.jpg}','Supergiant Games','Supergiant Games','2020-09-17','{Roguelike,Mitologia Grega,Hack and Slash}','{Um jogador,Conquistas,Suporte a controlador}',4.90,97845,98,true,'2026-05-16 13:26:50.741559-03','2026-05-16 13:26:50.741559-03','{"Minimum": {"Os": "Windows 7 SP1", "Memory": "8 GB RAM", "Storage": "15 GB", "Graphics": "1GB VRAM / DirectX 10+", "Processor": "Dual Core 2.4 GHz"}, "Recommended": {"Os": "Windows 10", "Memory": "16 GB RAM", "Storage": "20 GB", "Graphics": "2GB VRAM / DirectX 10+", "Processor": "Dual Core 3.0 GHz+"}}'),
    ('79531bf0-66c5-466b-b12f-ce053a0bc7f6','Stardew Valley','Voce herdou a fazenda do seu avo. Com algumas ferramentas usadas e algumas moedas, voce comeca uma nova vida.','You''ve inherited your grandfather''s old farm plot in Stardew Valley. Armed with hand-me-down tools and a few coins, you set out to begin your new life. Can you learn to live off the land and turn these overgrown fields into a thriving home?',37.99,NULL,NULL,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/413150/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/413150/ss_b887651a93b0525739049eb4194f633de2df75be.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/413150/ss_9ac899fe2cda15d48b0549bba77ef8c4a090a71c.1920x1080.jpg}','ConcernedApe','ConcernedApe','2016-02-26','{Fazenda,Relaxante,Multiplayer}','{Um jogador,Cooperativo Online,Conquistas}',4.90,342156,99,true,'2026-05-16 13:27:16.254935-03','2026-05-16 13:27:16.254935-03','{"Minimum": {"Os": "Windows Vista ou superior", "Memory": "2 GB RAM", "Storage": "500 MB", "Graphics": "256mb video memory", "Processor": "2 Ghz"}, "Recommended": {"Os": "Windows 10", "Memory": "4 GB RAM", "Storage": "500 MB", "Graphics": "512mb video memory", "Processor": "Intel i5"}}'),
    ('aec28251-193a-4aed-ba77-6cca5e1f3910','Counter-Strike 2','O shooter competitivo mais jogado do mundo. CS2 eleva o padrao com graficos aprimorados e sistema de smoke volumetrico.','CS2 is the largest technical leap in Counter-Strike''s history, which ensures that the best-in-class competitive experiences will continue for years to come. Rebuilt on Source 2, CS2 takes the competitive FPS to the next level.',0.00,NULL,NULL,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/730/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/730/ss_796601d9d67faf53486eeb26d0724347cea67ddc.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/730/ss_d830cfd0550fbb64d80e803e93c929c3abb02056.1920x1080.jpg}','Valve','Valve','2023-09-27','{FPS Competitivo,Multiplayer,Free to Play}','{Multijogador,Cross-Platform Multijogador,Conquistas}',4.30,1250000,78,true,'2026-05-16 13:27:16.302635-03','2026-05-16 13:27:16.302635-03','{"Minimum": {"Os": "Windows 10", "Memory": "8 GB RAM", "Storage": "85 GB", "Graphics": "GTX 970", "Processor": "Intel Core i5 750"}, "Recommended": {"Os": "Windows 10", "Memory": "16 GB RAM", "Storage": "85 GB SSD", "Graphics": "GTX 1080", "Processor": "Intel Core i7 4790"}}'),
    ('582e60ed-59aa-4c66-9533-65fa251e7dad','Baldur''s Gate 3','Reune seus companheiros e embarca em uma jornada em Faerun. As suas escolhas e acoes moldarao o destino do mundo.','Gather your party and return to the Forgotten Realms in a tale of fellowship and betrayal, sacrifice and survival, and the lure of absolute power. Mysterious abilities are awakening inside you, drawn from a Mind Flayer parasite planted in your brain.',249.90,NULL,NULL,'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/header.jpg','{https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/ss_c73bc54415178c07fef85f54ee26621728c77504.1920x1080.jpg,https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/ss_73d93bea842b93914d966622104dcb8c0f42972b.1920x1080.jpg}','Larian Studios','Larian Studios','2023-08-03','{D&D,Turno,Historia,Co-op}','{Um jogador,Cooperativo Online,Conquistas}',5.00,214567,96,true,'2026-05-16 13:27:16.350179-03','2026-05-16 13:27:16.350179-03','{"Minimum": {"Os": "Windows 10 64-bit", "Memory": "16 GB RAM", "Storage": "150 GB SSD", "Graphics": "NVIDIA GTX 1060 6GB", "Processor": "Intel i7 8700K"}, "Recommended": {"Os": "Windows 10/11 64-bit", "Memory": "16 GB RAM", "Storage": "150 GB SSD", "Graphics": "NVIDIA RTX 2060 Super 8GB", "Processor": "Intel i7 8700K"}}');
""");

            migrationBuilder.Sql("""
INSERT INTO public."GameGenres" ("GameId","GenreId") VALUES
    ('29cffff9-2aa0-4357-9720-1b26a92e79bb','127c1edb-abe2-494f-8da9-7086b2aeecd0'),
    ('29cffff9-2aa0-4357-9720-1b26a92e79bb','83f2abec-7b8f-45d6-8ed8-07dda0b6944a'),
    ('29cffff9-2aa0-4357-9720-1b26a92e79bb','b0815b22-8470-4be0-bf1b-06aed624c697'),
    ('434abf02-1172-4d34-8e8d-b503ee4f72c4','127c1edb-abe2-494f-8da9-7086b2aeecd0'),
    ('434abf02-1172-4d34-8e8d-b503ee4f72c4','83f2abec-7b8f-45d6-8ed8-07dda0b6944a'),
    ('434abf02-1172-4d34-8e8d-b503ee4f72c4','8e0e4778-b1d2-4817-8f6c-9c3340f7c174'),
    ('434abf02-1172-4d34-8e8d-b503ee4f72c4','b0815b22-8470-4be0-bf1b-06aed624c697'),
    ('7b872d46-625f-4ce7-b27d-71819897c450','127c1edb-abe2-494f-8da9-7086b2aeecd0'),
    ('7b872d46-625f-4ce7-b27d-71819897c450','83f2abec-7b8f-45d6-8ed8-07dda0b6944a'),
    ('7b872d46-625f-4ce7-b27d-71819897c450','ebd69131-ff81-40f5-b3ad-88584710d56c'),
    ('f3105b0f-86bf-4684-a3a3-a20ef5e7e125','127c1edb-abe2-494f-8da9-7086b2aeecd0'),
    ('f3105b0f-86bf-4684-a3a3-a20ef5e7e125','b0815b22-8470-4be0-bf1b-06aed624c697'),
    ('f3105b0f-86bf-4684-a3a3-a20ef5e7e125','ebd69131-ff81-40f5-b3ad-88584710d56c'),
    ('eeef11b7-f01f-4c35-b12b-e72f4dd03728','83f2abec-7b8f-45d6-8ed8-07dda0b6944a'),
    ('eeef11b7-f01f-4c35-b12b-e72f4dd03728','dc87d826-92b0-43d7-b850-5fee61bcfa22'),
    ('eeef11b7-f01f-4c35-b12b-e72f4dd03728','ebd69131-ff81-40f5-b3ad-88584710d56c'),
    ('bb461beb-a1b1-41f7-872e-38115232b584','83f2abec-7b8f-45d6-8ed8-07dda0b6944a'),
    ('bb461beb-a1b1-41f7-872e-38115232b584','b0815b22-8470-4be0-bf1b-06aed624c697'),
    ('bb461beb-a1b1-41f7-872e-38115232b584','ebd69131-ff81-40f5-b3ad-88584710d56c'),
    ('9a410b66-4fab-4876-954c-893fe6e4e38b','83f2abec-7b8f-45d6-8ed8-07dda0b6944a'),
    ('9a410b66-4fab-4876-954c-893fe6e4e38b','b0815b22-8470-4be0-bf1b-06aed624c697'),
    ('9a410b66-4fab-4876-954c-893fe6e4e38b','dc87d826-92b0-43d7-b850-5fee61bcfa22'),
    ('79531bf0-66c5-466b-b12f-ce053a0bc7f6','3bab1cb5-1f8e-4be3-a9b3-c614035a7928'),
    ('79531bf0-66c5-466b-b12f-ce053a0bc7f6','b0815b22-8470-4be0-bf1b-06aed624c697'),
    ('79531bf0-66c5-466b-b12f-ce053a0bc7f6','dc87d826-92b0-43d7-b850-5fee61bcfa22'),
    ('aec28251-193a-4aed-ba77-6cca5e1f3910','0c5f574c-9a13-4e35-87c4-985cc2348e3c'),
    ('aec28251-193a-4aed-ba77-6cca5e1f3910','83f2abec-7b8f-45d6-8ed8-07dda0b6944a'),
    ('aec28251-193a-4aed-ba77-6cca5e1f3910','8e0e4778-b1d2-4817-8f6c-9c3340f7c174'),
    ('582e60ed-59aa-4c66-9533-65fa251e7dad','0c5f574c-9a13-4e35-87c4-985cc2348e3c'),
    ('582e60ed-59aa-4c66-9533-65fa251e7dad','b0815b22-8470-4be0-bf1b-06aed624c697'),
    ('582e60ed-59aa-4c66-9533-65fa251e7dad','ebd69131-ff81-40f5-b3ad-88584710d56c');
""");

            migrationBuilder.Sql("""
INSERT INTO public."Cart" ("UserId","GameId","AddedAt") VALUES
    ('b2cca143-e9a4-4987-b3b4-09198a7422de','aec28251-193a-4aed-ba77-6cca5e1f3910','2026-05-16 13:53:09.240093-03');
""");

            migrationBuilder.Sql("""
INSERT INTO public."Wishlist" ("UserId","GameId","AddedAt") VALUES
    ('b2cca143-e9a4-4987-b3b4-09198a7422de','29cffff9-2aa0-4357-9720-1b26a92e79bb','2026-05-16 14:04:36.467929-03'),
    ('b2cca143-e9a4-4987-b3b4-09198a7422de','9a410b66-4fab-4876-954c-893fe6e4e38b','2026-05-16 14:04:37.980555-03');
""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
DELETE FROM public."Wishlist" WHERE ("UserId","GameId") IN (
    ('b2cca143-e9a4-4987-b3b4-09198a7422de','29cffff9-2aa0-4357-9720-1b26a92e79bb'),
    ('b2cca143-e9a4-4987-b3b4-09198a7422de','9a410b66-4fab-4876-954c-893fe6e4e38b')
);
""");

            migrationBuilder.Sql("""
DELETE FROM public."Cart" WHERE ("UserId","GameId") IN (
    ('b2cca143-e9a4-4987-b3b4-09198a7422de','aec28251-193a-4aed-ba77-6cca5e1f3910')
);
""");

            migrationBuilder.Sql("""
DELETE FROM public."GameGenres" WHERE "GameId" IN (
    '29cffff9-2aa0-4357-9720-1b26a92e79bb',
    '434abf02-1172-4d34-8e8d-b503ee4f72c4',
    '7b872d46-625f-4ce7-b27d-71819897c450',
    'f3105b0f-86bf-4684-a3a3-a20ef5e7e125',
    'eeef11b7-f01f-4c35-b12b-e72f4dd03728',
    'bb461beb-a1b1-41f7-872e-38115232b584',
    '9a410b66-4fab-4876-954c-893fe6e4e38b',
    '79531bf0-66c5-466b-b12f-ce053a0bc7f6',
    'aec28251-193a-4aed-ba77-6cca5e1f3910',
    '582e60ed-59aa-4c66-9533-65fa251e7dad'
);
""");

            migrationBuilder.Sql("""
DELETE FROM public."Games" WHERE "Id" IN (
    '29cffff9-2aa0-4357-9720-1b26a92e79bb',
    '434abf02-1172-4d34-8e8d-b503ee4f72c4',
    '7b872d46-625f-4ce7-b27d-71819897c450',
    'f3105b0f-86bf-4684-a3a3-a20ef5e7e125',
    'eeef11b7-f01f-4c35-b12b-e72f4dd03728',
    'bb461beb-a1b1-41f7-872e-38115232b584',
    '9a410b66-4fab-4876-954c-893fe6e4e38b',
    '79531bf0-66c5-466b-b12f-ce053a0bc7f6',
    'aec28251-193a-4aed-ba77-6cca5e1f3910',
    '582e60ed-59aa-4c66-9533-65fa251e7dad'
);
""");

            migrationBuilder.Sql("""
DELETE FROM public."Genres" WHERE "Id" IN (
    '127c1edb-abe2-494f-8da9-7086b2aeecd0',
    '83f2abec-7b8f-45d6-8ed8-07dda0b6944a',
    'b0815b22-8470-4be0-bf1b-06aed624c697',
    '8e0e4778-b1d2-4817-8f6c-9c3340f7c174',
    'ebd69131-ff81-40f5-b3ad-88584710d56c',
    'dc87d826-92b0-43d7-b850-5fee61bcfa22',
    '3bab1cb5-1f8e-4be3-a9b3-c614035a7928',
    '0c5f574c-9a13-4e35-87c4-985cc2348e3c'
);
""");

            migrationBuilder.Sql("""
DELETE FROM public."Users" WHERE "Id" IN (
    '90129338-75d3-4e4e-a4da-140caf241b85',
    'b2cca143-e9a4-4987-b3b4-09198a7422de',
    '2ca3b621-e4db-498f-a95f-cb8cd3000677',
    '218203f8-a726-486d-9ea8-197eaaa8e649'
);
""");
        }
    }
}
