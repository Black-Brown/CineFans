using BCrypt.Net;
using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Data;

namespace CineFansApp.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(AppDbContext context)
        {
            // Verificar si ya hay datos
            if (context.Users.Any())
            {
                return; // La base de datos ya tiene datos
            }

            // Agregar géneros si no existen
            if (!context.Genres.Any())
            {
                var generos = new Genre[]
                {
                    new Genre { Nombre = "Acción" },
                    new Genre { Nombre = "Comedia" },
                    new Genre { Nombre = "Drama" },
                    new Genre { Nombre = "Ciencia Ficción" },
                    new Genre { Nombre = "Terror" },
                    new Genre { Nombre = "Romántica" },
                    new Genre { Nombre = "Animación" },
                    new Genre { Nombre = "Documental" }
                };

                foreach (var genero in generos)
                {
                    context.Genres.Add(genero);
                }
                await context.SaveChangesAsync();
            }

            // Agregar películas si no existen
            if (!context.Movies.Any())
            {
                var generos = await context.Genres.ToListAsync();

                var peliculas = new Movie[]
                {
                    new Movie {
                        Titulo = "Avengers: Endgame",
                        Descripcion = "Después de los devastadores eventos de Avengers: Infinity War, el universo está en ruinas.",
                        Anio = 2019,
                        Director = "Anthony Russo, Joe Russo",
                        GeneroId = generos.First(g => g.Nombre == "Acción").GeneroId,
                        ImagenUrl = "/images/movies/avengers-endgame.jpg"
                    },
                    new Movie {
                        Titulo = "Joker",
                        Descripcion = "Arthur Fleck, un hombre ignorado por la sociedad, solo quiere hacer reír a la gente.",
                        Anio = 2019,
                        Director = "Todd Phillips",
                        GeneroId = generos.First(g => g.Nombre == "Drama").GeneroId,
                        ImagenUrl = "/images/movies/joker.jpg"
                    },
                    new Movie {
                        Titulo = "Parasite",
                        Descripcion = "La familia Kim, todos desempleados, se interesa por el estilo de vida de la rica familia Park.",
                        Anio = 2019,
                        Director = "Bong Joon-ho",
                        GeneroId = generos.First(g => g.Nombre == "Drama").GeneroId,
                        ImagenUrl = "/images/movies/parasite.jpg"
                    },
                    new Movie {
                        Titulo = "Dune",
                        Descripcion = "Un viaje mítico y emocional de un héroe que nace en un planeta desértico.",
                        Anio = 2021,
                        Director = "Denis Villeneuve",
                        GeneroId = generos.First(g => g.Nombre == "Ciencia Ficción").GeneroId,
                        ImagenUrl = "/images/movies/dune.jpg"
                    }
                };

                foreach (var pelicula in peliculas)
                {
                    context.Movies.Add(pelicula);
                }
                await context.SaveChangesAsync();
            }

            // Agregar usuarios si no existen
            if (!context.Users.Any())
            {
                var usuarios = new User[]
                {
                    new User {
                        Nombre = "Admin User",
                        Email = "admin@cinesocial.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        FotoPerfil = "/images/users/admin.jpg",
                        FechaRegistro = DateTime.Now.AddMonths(-6)
                    },
                    new User {
                        Nombre = "María García",
                        Email = "maria@example.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                        FotoPerfil = "/images/users/maria.jpg",
                        FechaRegistro = DateTime.Now.AddMonths(-3)
                    },
                    new User {
                        Nombre = "Carlos López",
                        Email = "carlos@example.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                        FotoPerfil = "/images/users/carlos.jpg",
                        FechaRegistro = DateTime.Now.AddMonths(-2)
                    }
                };

                foreach (var usuario in usuarios)
                {
                    context.Users.Add(usuario);
                }
                await context.SaveChangesAsync();
            }

            // Agregar publicaciones si no existen
            if (!context.Posts.Any())
            {
                var usuarios = await context.Users.ToListAsync();
                var peliculas = await context.Movies.ToListAsync();

                var publicaciones = new Post[]
                {
                    new Post {
                        UsuarioId = usuarios[1].UserId,
                        PeliculaId = peliculas[0].PeliculaId,
                        Texto = "¡Acabo de ver Avengers: Endgame y es increíble! Recomendada 100%",
                        Fecha = DateTime.Now.AddDays(-15)
                    },
                    new Post {
                        UsuarioId = usuarios[2].UserId,
                        PeliculaId = peliculas[1].PeliculaId,
                        Texto = "Joker es una obra maestra. Joaquin Phoenix merece todos los premios.",
                        Fecha = DateTime.Now.AddDays(-10)
                    },
                    new Post {
                        UsuarioId = usuarios[1].UserId,
                        PeliculaId = peliculas[2].PeliculaId,
                        Texto = "Parasite me dejó sin palabras. Una película que todos deberían ver.",
                        Fecha = DateTime.Now.AddDays(-5)
                    }
                };

                foreach (var publicacion in publicaciones)
                {
                    context.Posts.Add(publicacion);
                }
                await context.SaveChangesAsync();
            }

            // Agregar comentarios si no existen
            if (!context.Comments.Any())
            {
                var usuarios = await context.Users.ToListAsync();
                var publicaciones = await context.Posts.ToListAsync();

                var comentarios = new Comment[]
                {
                    new Comment {
                        PublicacionId = publicaciones[0].PublicacionId,
                        UsuarioId = usuarios[2].UserId,
                        Texto = "¡Totalmente de acuerdo! Es la mejor película del MCU.",
                        Fecha = DateTime.Now.AddDays(-14)
                    },
                    new Comment {
                        PublicacionId = publicaciones[1].PublicacionId,
                        UsuarioId = usuarios[1].UserId,
                        Texto = "La actuación de Joaquin Phoenix es impresionante.",
                        Fecha = DateTime.Now.AddDays(-9)
                    }
                };

                foreach (var comentario in comentarios)
                {
                    context.Comments.Add(comentario);
                }
                await context.SaveChangesAsync();
            }

            // Agregar likes si no existen
            if (!context.Likes.Any())
            {
                var usuarios = await context.Users.ToListAsync();
                var publicaciones = await context.Posts.ToListAsync();

                var likes = new Like[]
                {
                    new Like { PublicacionId = publicaciones[0].PublicacionId, UsuarioId = usuarios[2].UserId },
                    new Like { PublicacionId = publicaciones[1].PublicacionId, UsuarioId = usuarios[1].UserId },
                    new Like { PublicacionId = publicaciones[2].PublicacionId, UsuarioId = usuarios[2].UserId }
                };

                foreach (var like in likes)
                {
                    context.Likes.Add(like);
                }
                await context.SaveChangesAsync();
            }

            // Agregar seguidores si no existen
            if (!context.Follows.Any())
            {
                var usuarios = await context.Users.ToListAsync();

                var seguidores = new Follow[]
                {
                    new Follow { SeguidorId = usuarios[1].UserId, SeguidoId = usuarios[2].UserId },
                    new Follow { SeguidorId = usuarios[2].UserId, SeguidoId = usuarios[1].UserId }
                };

                foreach (var seguidor in seguidores)
                {
                    context.Follows.Add(seguidor);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
