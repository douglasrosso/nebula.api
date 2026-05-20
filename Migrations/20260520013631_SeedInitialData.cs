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
    ('90129338-75d3-4e4e-a4da-140caf241b85','Douglas','douglas@gmail.com','AQAAAAIAAYagAAAAEJ+8pN4LJiLVdLswtRtS6xVLxFTm0hwPMwFv2qy7e95lW/t+AJtwQy2wg9wdA+9xQg==','2026-05-13 22:48:48.041406-03','2026-05-13 22:48:48.041406-03',NULL,'{}',NULL,NULL,'',0,0,'',0),
    ('b2cca143-e9a4-4987-b3b4-09198a7422de','Admin Nebula','admin@nebula.com','AQAAAAIAAYagAAAAEEZjVebb6M7Ukw9mEODjyJT8FI638rWN/RYSLRP8TkbqmZVwDPRdC3x2ieTny9/oQQ==','2026-05-16 13:23:16.045388-03','2026-05-16 13:23:16.045388-03',NULL,'{}',NULL,NULL,'Admin Nebula',0,1,'admin',0),
    ('2ca3b621-e4db-498f-a95f-cb8cd3000677','Douglas Rosso','douglas@nebula.com','AQAAAAIAAYagAAAAEA2/K2Q8msLGdY1h7h+UqHHJzC30imU40jhsOAHd1d72K5EyD/u5SrMZll7GnCiE6Q==','2026-05-16 13:30:46.369934-03','2026-05-16 13:30:46.369934-03',NULL,'{}',NULL,NULL,'Douglas Rosso',0,1,'douglas',0),
    ('218203f8-a726-486d-9ea8-197eaaa8e649','Jogador Teste','jogador@nebula.com','AQAAAAIAAYagAAAAEGez8Zkj44uQDqTf++2NAL7PueULZbnL+it0je8Fxg2oL4Yb7Q3YFh+sUpjmK+L4kA==','2026-05-16 13:30:46.474018-03','2026-05-16 13:30:46.474018-03',NULL,'{}',NULL,NULL,'Jogador Teste',0,1,'jogador',0);
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
    ('29cffff9-2aa0-4357-9720-1b26a92e79bb','Elden Ring','Um RPG de acao epico ambientado em um mundo de fantasia sombria criado por Hidetaka Miyazaki e George R.R. Martin.','Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between. A vast world where open fields with a variety of situations and huge dungeons are seamlessly connected.',249.90,299.90,17,'https://cdn.cloudflare.steamstatic.com/steam/apps/1245620/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/1245620/ss_e80a907c2c43337e53316c71555c3c3035a1343e.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/1245620/ss_c372274833ae6e5437b952fa1979430546a43ad9.jpg}','FromSoftware Inc.','Bandai Namco Entertainment','2022-02-25','{Souls-like,Fantasia Sombria,Dificil}','{Um jogador,Cooperativo Online,PvP Online,Conquistas}',4.80,524789,93,true,'2026-05-16 13:25:52.701153-03','2026-05-16 13:25:52.701153-03','{"Minimum": {"Os": "Windows 10", "Memory": "12 GB RAM", "Storage": "60 GB", "Graphics": "GTX 1060 3GB", "Processor": "Intel Core i5-8400"}, "Recommended": {"Os": "Windows 10/11", "Memory": "16 GB RAM", "Storage": "60 GB SSD", "Graphics": "GTX 1070 8GB", "Processor": "Intel Core i7-8700K"}}'),
    ('434abf02-1172-4d34-8e8d-b503ee4f72c4','Cyberpunk 2077','Um RPG de mundo aberto ambientado em Night City, uma megalopole obcecada por poder, glamour e modificacoes corporais.','Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City — a dangerous megalopolis obsessed with power, glamour and body modification. You play as V, a mercenary outlaw going after a one-of-a-kind implant that is the key to immortality.',149.90,249.90,40,'https://cdn.cloudflare.steamstatic.com/steam/apps/1091500/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/1091500/ss_872d42f21b3d6a34360b6c3a75bf20e6ede9c776.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/1091500/ss_91b498e30a66e00ffd2867b5acf0dec4aa4c4cc0.jpg}','CD Projekt Red','CD Projekt','2020-12-10','{Cyberpunk,Mundo Aberto,Personalizado}','{Um jogador,Conquistas,Suporte a controlador}',4.60,389215,84,true,'2026-05-16 13:26:21.467338-03','2026-05-16 13:26:21.467338-03','{"Minimum": {"Os": "Windows 10", "Memory": "12 GB RAM", "Storage": "70 GB SSD", "Graphics": "GTX 1060 6GB", "Processor": "Intel Core i7-6700K"}, "Recommended": {"Os": "Windows 10/11", "Memory": "16 GB RAM", "Storage": "70 GB SSD", "Graphics": "RTX 2080 Super", "Processor": "Intel Core i7-8700K"}}'),
    ('7b872d46-625f-4ce7-b27d-71819897c450','Red Dead Redemption 2','Uma epica aventura no velho oeste americano. Arthur Morgan e o bando Van der Linde tentam sobreviver em uma era que se fecha para foras da lei.','America, 1899. Arthur Morgan and the Van der Linde gang are outlaws on the run. With federal agents and the best bounty hunters in the nation massing on their heels, the gang must rob, steal and fight their way across the rugged heartland of America.',189.90,NULL,NULL,'https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_bfe1b7b8610128a8af7f1df6ded9fefbc1a6cc31.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_01b06e7b7f8b463e5b7620d25c3c3e3fc1a4bf9c.jpg}','Rockstar Games','Rockstar Games','2019-12-05','{Velho Oeste,Historia,Sobrevivencia}','{Um jogador,Multijogador Online,Conquistas}',4.90,712456,97,true,'2026-05-16 13:26:21.521842-03','2026-05-16 13:26:21.521842-03','{"Minimum": {"Os": "Windows 10", "Memory": "8 GB RAM", "Storage": "150 GB SSD", "Graphics": "GTX 770 2GB", "Processor": "Intel Core i5-2500K"}, "Recommended": {"Os": "Windows 10", "Memory": "16 GB RAM", "Storage": "150 GB SSD", "Graphics": "GTX 1080 Ti", "Processor": "Intel Core i7-4770K"}}'),
    ('f3105b0f-86bf-4684-a3a3-a20ef5e7e125','The Witcher 3: Wild Hunt','Um RPG de mundo aberto premiado. Jogue como Geralt de Rivia, um caca-monstros mercenario em busca de sua filha adotiva.','You are Geralt of Rivia, mercenary monster slayer. Before you stands a war-torn, monster-infested continent you can explore at will. Your current contract? Tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.',59.90,119.90,50,'https://cdn.cloudflare.steamstatic.com/steam/apps/292030/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/292030/ss_aca3e8f6e85b5e9bef9f28b3567f5a5e2c7e5a7b.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/292030/ss_eda62f3a47ced69af04fbb4ad7527046dc6c8f7f.jpg}','CD Projekt Red','CD Projekt','2015-05-19','{Fantasia,Historia,Escolhas Morais}','{Um jogador,Conquistas,Suporte a controlador}',4.90,248931,97,true,'2026-05-16 13:26:21.573883-03','2026-05-16 13:26:21.573883-03','{"Minimum": {"Os": "Windows 7/8/8.1/10", "Memory": "6 GB RAM", "Storage": "35 GB", "Graphics": "GTX 660", "Processor": "Intel Core i5-2500K"}, "Recommended": {"Os": "Windows 7/8/8.1/10", "Memory": "8 GB RAM", "Storage": "35 GB", "Graphics": "GTX 770", "Processor": "Intel Core i7-3770"}}'),
    ('eeef11b7-f01f-4c35-b12b-e72f4dd03728','Hollow Knight','Um desafiador e belo metroidvania ambientado em um vasto reino subterraneo de insetos e herois.','Forge your own path in Hollow Knight! An epic action adventure through a vast ruined kingdom of insects and heroes. Explore twisting caverns, battle tainted creatures and befriend bizarre bugs, all in a classic, hand-drawn 2D style.',29.90,NULL,NULL,'https://cdn.cloudflare.steamstatic.com/steam/apps/367520/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/367520/ss_0518f9b1d3f0e5258e4eed6d4c96d913eebce2c5.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/367520/ss_8dba7ee4ce6e0e1a3a5bdf5aa42cb8c1bc0ee90c.jpg}','Team Cherry','Team Cherry','2017-02-24','{Metroidvania,Plataforma,Dificil}','{Um jogador,Conquistas,Suporte a controlador}',4.90,156789,98,true,'2026-05-16 13:26:50.643598-03','2026-05-16 13:26:50.643598-03','{"Minimum": {"Os": "Windows 7", "Memory": "4 GB RAM", "Storage": "9 GB", "Graphics": "GeForce 9800GTX+", "Processor": "Intel Core 2 Duo E5200"}, "Recommended": {"Os": "Windows 10", "Memory": "8 GB RAM", "Storage": "9 GB", "Graphics": "GeForce GTX 560", "Processor": "Intel Core i5"}}'),
    ('bb461beb-a1b1-41f7-872e-38115232b584','God of War','Kratos, o Deus da Guerra grego, embarca em uma jornada com seu filho Atreus pelo reino nordico dos deuses.','His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods and monsters. It is in this harsh, unforgiving world that he must fight to survive and teach his son to do the same.',199.90,NULL,NULL,'https://cdn.cloudflare.steamstatic.com/steam/apps/1593500/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/1593500/ss_ffd68c7aa7b53e4f6ff88edade96af25f42ef49f.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/1593500/ss_df18bfbe19fad5ed50e58374fbfe38e05d1a3b9a.jpg}','Santa Monica Studio','PlayStation PC LLC','2022-01-14','{Mitologia,Combate,Historia}','{Um jogador,Conquistas,Suporte a controlador}',4.90,89432,96,true,'2026-05-16 13:26:50.69266-03','2026-05-16 13:26:50.69266-03','{"Minimum": {"Os": "Windows 10 64-bit", "Memory": "8 GB RAM", "Storage": "70 GB SSD", "Graphics": "NVIDIA GTX 1060 (6GB)", "Processor": "Intel i5-6600k"}, "Recommended": {"Os": "Windows 10 64-bit", "Memory": "16 GB RAM", "Storage": "70 GB SSD", "Graphics": "NVIDIA RTX 3080", "Processor": "Intel i9-9900K"}}'),
    ('9a410b66-4fab-4876-954c-893fe6e4e38b','Hades','Um roguelike de acao onde voce encarna Zagreus, filho do deus do submundo, tentando escapar do reino de seu pai.','Hades is a god-like rogue-like dungeon crawler that combines the best aspects of Supergiant''s critically acclaimed titles, including the fast-paced action of Bastion, the rich atmosphere and depth of Transistor, and the character-driven storytelling of Pyre.',44.90,59.90,25,'https://cdn.cloudflare.steamstatic.com/steam/apps/1145360/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/1145360/ss_5f0d9e8d8e7a4b6e3fdce3ae4f9c70d2b28e0e9d.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/1145360/ss_6bb09b4a6f5b49ea5eebc74f94ddf8d7c50e3a34.jpg}','Supergiant Games','Supergiant Games','2020-09-17','{Roguelike,Mitologia Grega,Hack and Slash}','{Um jogador,Conquistas,Suporte a controlador}',4.90,97845,98,true,'2026-05-16 13:26:50.741559-03','2026-05-16 13:26:50.741559-03','{"Minimum": {"Os": "Windows 7 SP1", "Memory": "8 GB RAM", "Storage": "15 GB", "Graphics": "1GB VRAM / DirectX 10+", "Processor": "Dual Core 2.4 GHz"}, "Recommended": {"Os": "Windows 10", "Memory": "16 GB RAM", "Storage": "20 GB", "Graphics": "2GB VRAM / DirectX 10+", "Processor": "Dual Core 3.0 GHz+"}}'),
    ('79531bf0-66c5-466b-b12f-ce053a0bc7f6','Stardew Valley','Voce herdou a fazenda do seu avo. Com algumas ferramentas usadas e algumas moedas, voce comeca uma nova vida.','You''ve inherited your grandfather''s old farm plot in Stardew Valley. Armed with hand-me-down tools and a few coins, you set out to begin your new life. Can you learn to live off the land and turn these overgrown fields into a thriving home?',37.99,NULL,NULL,'https://cdn.cloudflare.steamstatic.com/steam/apps/413150/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/413150/ss_dd1321fba5f6db17d33df4e57f05e4b0cb7bb80c.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/413150/ss_29e26c6a2d9e1e5ea47c85d6a11be1fb33a1b94c.jpg}','ConcernedApe','ConcernedApe','2016-02-26','{Fazenda,Relaxante,Multiplayer}','{Um jogador,Cooperativo Online,Conquistas}',4.90,342156,99,true,'2026-05-16 13:27:16.254935-03','2026-05-16 13:27:16.254935-03','{"Minimum": {"Os": "Windows Vista ou superior", "Memory": "2 GB RAM", "Storage": "500 MB", "Graphics": "256mb video memory", "Processor": "2 Ghz"}, "Recommended": {"Os": "Windows 10", "Memory": "4 GB RAM", "Storage": "500 MB", "Graphics": "512mb video memory", "Processor": "Intel i5"}}'),
    ('aec28251-193a-4aed-ba77-6cca5e1f3910','Counter-Strike 2','O shooter competitivo mais jogado do mundo. CS2 eleva o padrao com graficos aprimorados e sistema de smoke volumetrico.','CS2 is the largest technical leap in Counter-Strike''s history, which ensures that the best-in-class competitive experiences will continue for years to come. Rebuilt on Source 2, CS2 takes the competitive FPS to the next level.',0.00,NULL,NULL,'https://cdn.cloudflare.steamstatic.com/steam/apps/730/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/730/ss_1e0543c7fb6f47ee7cb3e2ba90fc7e0f37b0e7b4.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/730/ss_b44d13f2f45a1b4c61dfe52e31df9a09bc8bb37d.jpg}','Valve','Valve','2023-09-27','{FPS Competitivo,Multiplayer,Free to Play}','{Multijogador,Cross-Platform Multijogador,Conquistas}',4.30,1250000,78,true,'2026-05-16 13:27:16.302635-03','2026-05-16 13:27:16.302635-03','{"Minimum": {"Os": "Windows 10", "Memory": "8 GB RAM", "Storage": "85 GB", "Graphics": "GTX 970", "Processor": "Intel Core i5 750"}, "Recommended": {"Os": "Windows 10", "Memory": "16 GB RAM", "Storage": "85 GB SSD", "Graphics": "GTX 1080", "Processor": "Intel Core i7 4790"}}'),
    ('582e60ed-59aa-4c66-9533-65fa251e7dad','Baldur''s Gate 3','Reune seus companheiros e embarca em uma jornada em Faerun. As suas escolhas e acoes moldarao o destino do mundo.','Gather your party and return to the Forgotten Realms in a tale of fellowship and betrayal, sacrifice and survival, and the lure of absolute power. Mysterious abilities are awakening inside you, drawn from a Mind Flayer parasite planted in your brain.',249.90,NULL,NULL,'https://cdn.cloudflare.steamstatic.com/steam/apps/1086940/header.jpg','{https://cdn.cloudflare.steamstatic.com/steam/apps/1086940/ss_8c3dfe1fbe793a9f9617ae23e2bcaf1843a8bd53.jpg,https://cdn.cloudflare.steamstatic.com/steam/apps/1086940/ss_f41d0b3c5fb6d5cefd7af7aac0af6fce97d6a1f9.jpg}','Larian Studios','Larian Studios','2023-08-03','{D&D,Turno,Historia,Co-op}','{Um jogador,Cooperativo Online,Conquistas}',5.00,214567,96,true,'2026-05-16 13:27:16.350179-03','2026-05-16 13:27:16.350179-03','{"Minimum": {"Os": "Windows 10 64-bit", "Memory": "16 GB RAM", "Storage": "150 GB SSD", "Graphics": "NVIDIA GTX 1060 6GB", "Processor": "Intel i7 8700K"}, "Recommended": {"Os": "Windows 10/11 64-bit", "Memory": "16 GB RAM", "Storage": "150 GB SSD", "Graphics": "NVIDIA RTX 2060 Super 8GB", "Processor": "Intel i7 8700K"}}');
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
