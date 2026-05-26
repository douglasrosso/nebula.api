using nebula.api.src.Entities;

namespace nebula.api.src.Data.Mock
{
    public static class MockData
    {
        public static readonly List<GenreEntity> Genres;
        public static readonly List<GameEntity> Games;
        public static readonly List<UserEntity> Users;
        public static readonly List<ReviewEntity> Reviews = [];

        static MockData()
        {
            var now = new DateTime(2026, 5, 16, 13, 25, 52, DateTimeKind.Utc);

            Genres =
            [
                Genre("127c1edb-abe2-494f-8da9-7086b2aeecd0", "Mundo Aberto", "mundo-aberto", now),
                Genre("83f2abec-7b8f-45d6-8ed8-07dda0b6944a", "Acao",         "acao",         now),
                Genre("b0815b22-8470-4be0-bf1b-06aed624c697", "RPG",          "rpg",          now),
                Genre("8e0e4778-b1d2-4817-8f6c-9c3340f7c174", "FPS",          "fps",          now),
                Genre("ebd69131-ff81-40f5-b3ad-88584710d56c", "Aventura",     "aventura",     now),
                Genre("dc87d826-92b0-43d7-b850-5fee61bcfa22", "Indie",        "indie",        now),
                Genre("3bab1cb5-1f8e-4be3-a9b3-c614035a7928", "Simulador",    "simulador",    now),
                Genre("0c5f574c-9a13-4e35-87c4-985cc2348e3c", "Estrategia",   "estrategia",   now),
            ];

            GenreEntity G(string id) => Genres.First(g => g.Id == Guid.Parse(id));

            Games =
            [
                new GameEntity
                {
                    Id = Guid.Parse("29cffff9-2aa0-4357-9720-1b26a92e79bb"),
                    Title = "Elden Ring",
                    Description = "Um RPG de acao epico ambientado em um mundo de fantasia sombria criado por Hidetaka Miyazaki e George R.R. Martin.",
                    LongDescription = "Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between.",
                    Price = 249.90m, OriginalPrice = 299.90m, Discount = 17,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1245620/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1245620/ss_943bf6fe62352757d9070c1d33e50b92fe8539f1.1920x1080.jpg"],
                    Developer = "FromSoftware Inc.", Publisher = "Bandai Namco Entertainment",
                    ReleaseDate = new DateOnly(2022, 2, 25),
                    Tags = ["Souls-like", "Fantasia Sombria", "Dificil"],
                    Features = ["Um jogador", "Cooperativo Online", "PvP Online", "Conquistas"],
                    Rating = 4.80m, ReviewCount = 524789, PositivePercentage = 93, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 10",     Processor = "Intel Core i5-8400",  Memory = "12 GB RAM", Graphics = "GTX 1060 3GB",  Storage = "60 GB"     },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10/11",  Processor = "Intel Core i7-8700K", Memory = "16 GB RAM", Graphics = "GTX 1070 8GB",  Storage = "60 GB SSD" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("434abf02-1172-4d34-8e8d-b503ee4f72c4"),
                    Title = "Cyberpunk 2077",
                    Description = "Um RPG de mundo aberto ambientado em Night City, uma megalopole obcecada por poder, glamour e modificacoes corporais.",
                    LongDescription = "Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City.",
                    Price = 149.90m, OriginalPrice = 249.90m, Discount = 40,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1091500/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1091500/ss_2f649b68d579bf87011487d29bc4ccbfdd97d34f.1920x1080.jpg"],
                    Developer = "CD Projekt Red", Publisher = "CD Projekt",
                    ReleaseDate = new DateOnly(2020, 12, 10),
                    Tags = ["Cyberpunk", "Mundo Aberto", "Personalizado"],
                    Features = ["Um jogador", "Conquistas", "Suporte a controlador"],
                    Rating = 4.60m, ReviewCount = 389215, PositivePercentage = 84, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 10",     Processor = "Intel Core i7-6700K", Memory = "12 GB RAM", Graphics = "GTX 1060 6GB",    Storage = "70 GB SSD" },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10/11",  Processor = "Intel Core i7-8700K", Memory = "16 GB RAM", Graphics = "RTX 2080 Super",  Storage = "70 GB SSD" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("7b872d46-625f-4ce7-b27d-71819897c450"),
                    Title = "Red Dead Redemption 2",
                    Description = "Uma epica aventura no velho oeste americano.",
                    LongDescription = "America, 1899. Arthur Morgan and the Van der Linde gang are outlaws on the run.",
                    Price = 189.90m,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1174180/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1174180/ss_66b553f4c209476d3e4ce25fa4714002cc914c4f.1920x1080.jpg"],
                    Developer = "Rockstar Games", Publisher = "Rockstar Games",
                    ReleaseDate = new DateOnly(2019, 12, 5),
                    Tags = ["Velho Oeste", "Historia", "Sobrevivencia"],
                    Features = ["Um jogador", "Multijogador Online", "Conquistas"],
                    Rating = 4.90m, ReviewCount = 712456, PositivePercentage = 97, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 10", Processor = "Intel Core i5-2500K", Memory = "8 GB RAM",  Graphics = "GTX 770 2GB",  Storage = "150 GB SSD" },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10", Processor = "Intel Core i7-4770K", Memory = "16 GB RAM", Graphics = "GTX 1080 Ti", Storage = "150 GB SSD" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("f3105b0f-86bf-4684-a3a3-a20ef5e7e125"),
                    Title = "The Witcher 3: Wild Hunt",
                    Description = "Um RPG de mundo aberto premiado. Jogue como Geralt de Rivia.",
                    LongDescription = "You are Geralt of Rivia, mercenary monster slayer.",
                    Price = 59.90m, OriginalPrice = 119.90m, Discount = 50,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/292030/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/292030/ss_5710298af2318afd9aa72449ef29ac4a2ef64d8e.1920x1080.jpg"],
                    Developer = "CD Projekt Red", Publisher = "CD Projekt",
                    ReleaseDate = new DateOnly(2015, 5, 19),
                    Tags = ["Fantasia", "Historia", "Escolhas Morais"],
                    Features = ["Um jogador", "Conquistas", "Suporte a controlador"],
                    Rating = 4.90m, ReviewCount = 248931, PositivePercentage = 97, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 7/8/8.1/10", Processor = "Intel Core i5-2500K", Memory = "6 GB RAM", Graphics = "GTX 660", Storage = "35 GB" },
                        Recommended = new SystemRequirementSpec { Os = "Windows 7/8/8.1/10", Processor = "Intel Core i7-3770",  Memory = "8 GB RAM", Graphics = "GTX 770", Storage = "35 GB" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("eeef11b7-f01f-4c35-b12b-e72f4dd03728"),
                    Title = "Hollow Knight",
                    Description = "Um desafiador e belo metroidvania ambientado em um vasto reino subterraneo de insetos e herois.",
                    LongDescription = "Forge your own path in Hollow Knight! An epic action adventure through a vast ruined kingdom of insects and heroes.",
                    Price = 29.90m,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/367520/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/367520/ss_5384f9f8b96a0b9934b2bc35a4058376211636d2.1920x1080.jpg"],
                    Developer = "Team Cherry", Publisher = "Team Cherry",
                    ReleaseDate = new DateOnly(2017, 2, 24),
                    Tags = ["Metroidvania", "Plataforma", "Dificil"],
                    Features = ["Um jogador", "Conquistas", "Suporte a controlador"],
                    Rating = 4.90m, ReviewCount = 156789, PositivePercentage = 98, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 7",  Processor = "Intel Core 2 Duo E5200", Memory = "4 GB RAM", Graphics = "GeForce 9800GTX+", Storage = "9 GB" },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10", Processor = "Intel Core i5",         Memory = "8 GB RAM", Graphics = "GeForce GTX 560",   Storage = "9 GB" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("bb461beb-a1b1-41f7-872e-38115232b584"),
                    Title = "God of War",
                    Description = "Kratos, o Deus da Guerra grego, embarca em uma jornada com seu filho Atreus pelo reino nordico dos deuses.",
                    LongDescription = "His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods.",
                    Price = 199.90m,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1593500/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1593500/ss_6eccc970b5de2943546d93d319be1b5c0618f21b.1920x1080.jpg"],
                    Developer = "Santa Monica Studio", Publisher = "PlayStation PC LLC",
                    ReleaseDate = new DateOnly(2022, 1, 14),
                    Tags = ["Mitologia", "Combate", "Historia"],
                    Features = ["Um jogador", "Conquistas", "Suporte a controlador"],
                    Rating = 4.90m, ReviewCount = 89432, PositivePercentage = 96, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 10 64-bit", Processor = "Intel i5-6600k", Memory = "8 GB RAM",  Graphics = "NVIDIA GTX 1060 (6GB)", Storage = "70 GB SSD" },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10 64-bit", Processor = "Intel i9-9900K", Memory = "16 GB RAM", Graphics = "NVIDIA RTX 3080",        Storage = "70 GB SSD" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("9a410b66-4fab-4876-954c-893fe6e4e38b"),
                    Title = "Hades",
                    Description = "Um roguelike de acao onde voce encarna Zagreus, filho do deus do submundo.",
                    LongDescription = "Hades is a god-like rogue-like dungeon crawler that combines the best aspects of Supergiant's critically acclaimed titles.",
                    Price = 44.90m, OriginalPrice = 59.90m, Discount = 25,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1145360/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1145360/ss_c0fed447426b69981cf1721756acf75369801b31.1920x1080.jpg"],
                    Developer = "Supergiant Games", Publisher = "Supergiant Games",
                    ReleaseDate = new DateOnly(2020, 9, 17),
                    Tags = ["Roguelike", "Mitologia Grega", "Hack and Slash"],
                    Features = ["Um jogador", "Conquistas", "Suporte a controlador"],
                    Rating = 4.90m, ReviewCount = 97845, PositivePercentage = 98, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 7 SP1", Processor = "Dual Core 2.4 GHz",  Memory = "8 GB RAM",  Graphics = "1GB VRAM / DirectX 10+", Storage = "15 GB" },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10",    Processor = "Dual Core 3.0 GHz+", Memory = "16 GB RAM", Graphics = "2GB VRAM / DirectX 10+", Storage = "20 GB" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("79531bf0-66c5-466b-b12f-ce053a0bc7f6"),
                    Title = "Stardew Valley",
                    Description = "Voce herdou a fazenda do seu avo. Com algumas ferramentas usadas e algumas moedas, voce comeca uma nova vida.",
                    LongDescription = "You've inherited your grandfather's old farm plot in Stardew Valley.",
                    Price = 37.99m,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/413150/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/413150/ss_b887651a93b0525739049eb4194f633de2df75be.1920x1080.jpg"],
                    Developer = "ConcernedApe", Publisher = "ConcernedApe",
                    ReleaseDate = new DateOnly(2016, 2, 26),
                    Tags = ["Fazenda", "Relaxante", "Multiplayer"],
                    Features = ["Um jogador", "Cooperativo Online", "Conquistas"],
                    Rating = 4.90m, ReviewCount = 342156, PositivePercentage = 99, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows Vista ou superior", Processor = "2 Ghz",      Memory = "2 GB RAM", Graphics = "256mb video memory", Storage = "500 MB" },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10",                Processor = "Intel i5", Memory = "4 GB RAM", Graphics = "512mb video memory", Storage = "500 MB" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("aec28251-193a-4aed-ba77-6cca5e1f3910"),
                    Title = "Counter-Strike 2",
                    Description = "O shooter competitivo mais jogado do mundo.",
                    LongDescription = "CS2 is the largest technical leap in Counter-Strike's history.",
                    Price = 0.00m,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/730/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/730/ss_796601d9d67faf53486eeb26d0724347cea67ddc.1920x1080.jpg"],
                    Developer = "Valve", Publisher = "Valve",
                    ReleaseDate = new DateOnly(2023, 9, 27),
                    Tags = ["FPS Competitivo", "Multiplayer", "Free to Play"],
                    Features = ["Multijogador", "Cross-Platform Multijogador", "Conquistas"],
                    Rating = 4.30m, ReviewCount = 1250000, PositivePercentage = 78, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 10", Processor = "Intel Core i5 750", Memory = "8 GB RAM",  Graphics = "GTX 970",  Storage = "85 GB"     },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10", Processor = "Intel Core i7 4790", Memory = "16 GB RAM", Graphics = "GTX 1080", Storage = "85 GB SSD" },
                    },
                },
                new GameEntity
                {
                    Id = Guid.Parse("582e60ed-59aa-4c66-9533-65fa251e7dad"),
                    Title = "Baldur's Gate 3",
                    Description = "Reune seus companheiros e embarca em uma jornada em Faerun.",
                    LongDescription = "Gather your party and return to the Forgotten Realms in a tale of fellowship and betrayal.",
                    Price = 249.90m,
                    CoverImage = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/header.jpg",
                    Screenshots = ["https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/ss_c73bc54415178c07fef85f54ee26621728c77504.1920x1080.jpg"],
                    Developer = "Larian Studios", Publisher = "Larian Studios",
                    ReleaseDate = new DateOnly(2023, 8, 3),
                    Tags = ["D&D", "Turno", "Historia", "Co-op"],
                    Features = ["Um jogador", "Cooperativo Online", "Conquistas"],
                    Rating = 5.00m, ReviewCount = 214567, PositivePercentage = 96, IsActive = true,
                    CreatedAt = now, UpdatedAt = now,
                    SystemRequirements = new SystemRequirements
                    {
                        Minimum     = new SystemRequirementSpec { Os = "Windows 10 64-bit",     Processor = "Intel i7 8700K", Memory = "16 GB RAM", Graphics = "NVIDIA GTX 1060 6GB",       Storage = "150 GB SSD" },
                        Recommended = new SystemRequirementSpec { Os = "Windows 10/11 64-bit",   Processor = "Intel i7 8700K", Memory = "16 GB RAM", Graphics = "NVIDIA RTX 2060 Super 8GB", Storage = "150 GB SSD" },
                    },
                },
            ];

            // Wire up GameGenres
            var gameGenreMap = new (string GameId, string GenreId)[]
            {
                ("29cffff9-2aa0-4357-9720-1b26a92e79bb", "127c1edb-abe2-494f-8da9-7086b2aeecd0"),
                ("29cffff9-2aa0-4357-9720-1b26a92e79bb", "83f2abec-7b8f-45d6-8ed8-07dda0b6944a"),
                ("29cffff9-2aa0-4357-9720-1b26a92e79bb", "b0815b22-8470-4be0-bf1b-06aed624c697"),
                ("434abf02-1172-4d34-8e8d-b503ee4f72c4", "127c1edb-abe2-494f-8da9-7086b2aeecd0"),
                ("434abf02-1172-4d34-8e8d-b503ee4f72c4", "83f2abec-7b8f-45d6-8ed8-07dda0b6944a"),
                ("434abf02-1172-4d34-8e8d-b503ee4f72c4", "8e0e4778-b1d2-4817-8f6c-9c3340f7c174"),
                ("434abf02-1172-4d34-8e8d-b503ee4f72c4", "b0815b22-8470-4be0-bf1b-06aed624c697"),
                ("7b872d46-625f-4ce7-b27d-71819897c450", "127c1edb-abe2-494f-8da9-7086b2aeecd0"),
                ("7b872d46-625f-4ce7-b27d-71819897c450", "83f2abec-7b8f-45d6-8ed8-07dda0b6944a"),
                ("7b872d46-625f-4ce7-b27d-71819897c450", "ebd69131-ff81-40f5-b3ad-88584710d56c"),
                ("f3105b0f-86bf-4684-a3a3-a20ef5e7e125", "127c1edb-abe2-494f-8da9-7086b2aeecd0"),
                ("f3105b0f-86bf-4684-a3a3-a20ef5e7e125", "b0815b22-8470-4be0-bf1b-06aed624c697"),
                ("f3105b0f-86bf-4684-a3a3-a20ef5e7e125", "ebd69131-ff81-40f5-b3ad-88584710d56c"),
                ("eeef11b7-f01f-4c35-b12b-e72f4dd03728", "83f2abec-7b8f-45d6-8ed8-07dda0b6944a"),
                ("eeef11b7-f01f-4c35-b12b-e72f4dd03728", "dc87d826-92b0-43d7-b850-5fee61bcfa22"),
                ("eeef11b7-f01f-4c35-b12b-e72f4dd03728", "ebd69131-ff81-40f5-b3ad-88584710d56c"),
                ("bb461beb-a1b1-41f7-872e-38115232b584", "83f2abec-7b8f-45d6-8ed8-07dda0b6944a"),
                ("bb461beb-a1b1-41f7-872e-38115232b584", "b0815b22-8470-4be0-bf1b-06aed624c697"),
                ("bb461beb-a1b1-41f7-872e-38115232b584", "ebd69131-ff81-40f5-b3ad-88584710d56c"),
                ("9a410b66-4fab-4876-954c-893fe6e4e38b", "83f2abec-7b8f-45d6-8ed8-07dda0b6944a"),
                ("9a410b66-4fab-4876-954c-893fe6e4e38b", "b0815b22-8470-4be0-bf1b-06aed624c697"),
                ("9a410b66-4fab-4876-954c-893fe6e4e38b", "dc87d826-92b0-43d7-b850-5fee61bcfa22"),
                ("79531bf0-66c5-466b-b12f-ce053a0bc7f6", "3bab1cb5-1f8e-4be3-a9b3-c614035a7928"),
                ("79531bf0-66c5-466b-b12f-ce053a0bc7f6", "b0815b22-8470-4be0-bf1b-06aed624c697"),
                ("79531bf0-66c5-466b-b12f-ce053a0bc7f6", "dc87d826-92b0-43d7-b850-5fee61bcfa22"),
                ("aec28251-193a-4aed-ba77-6cca5e1f3910", "0c5f574c-9a13-4e35-87c4-985cc2348e3c"),
                ("aec28251-193a-4aed-ba77-6cca5e1f3910", "83f2abec-7b8f-45d6-8ed8-07dda0b6944a"),
                ("aec28251-193a-4aed-ba77-6cca5e1f3910", "8e0e4778-b1d2-4817-8f6c-9c3340f7c174"),
                ("582e60ed-59aa-4c66-9533-65fa251e7dad", "0c5f574c-9a13-4e35-87c4-985cc2348e3c"),
                ("582e60ed-59aa-4c66-9533-65fa251e7dad", "b0815b22-8470-4be0-bf1b-06aed624c697"),
                ("582e60ed-59aa-4c66-9533-65fa251e7dad", "ebd69131-ff81-40f5-b3ad-88584710d56c"),
            };

            foreach (var (gameId, genreId) in gameGenreMap)
            {
                var game  = Games.First(g => g.Id == Guid.Parse(gameId));
                var genre = G(genreId);
                var link  = new GameGenreEntity { GameId = game.Id, GenreId = genre.Id, Game = game, Genre = genre };
                game.GameGenres.Add(link);
                genre.GameGenres.Add(link);
            }

            Users =
            [
                new UserEntity
                {
                    Id          = Guid.Parse("90129338-75d3-4e4e-a4da-140caf241b85"),
                    Name        = "Douglas",
                    Email       = "douglas@gmail.com",
                    Password    = "AQAAAAIAAYagAAAAEJ+8pN4LJiLVdLswtRtS6xVLxFTm0hwPMwFv2qy7e95lW/t+AJtwQy2wg9wdA+9xQg==",
                    Username    = "douglas",
                    DisplayName = "Douglas",
                    Xp          = 8000, Level = 0,
                    CreatedAt   = now, UpdatedAt = now,
                },
                new UserEntity
                {
                    Id          = Guid.Parse("b2cca143-e9a4-4987-b3b4-09198a7422de"),
                    Name        = "Anderson",
                    Email       = "anderson@gmail.com",
                    Password    = "AQAAAAIAAYagAAAAEJ+8pN4LJiLVdLswtRtS6xVLxFTm0hwPMwFv2qy7e95lW/t+AJtwQy2wg9wdA+9xQg==",
                    Username    = "anderson",
                    DisplayName = "Anderson",
                    Xp          = 0, Level = 0,
                    CreatedAt   = now, UpdatedAt = now,
                },
            ];
        }

        private static GenreEntity Genre(string id, string name, string slug, DateTime now) =>
            new() { Id = Guid.Parse(id), Name = name, Slug = slug, CreatedAt = now, UpdatedAt = now };
    }
}
