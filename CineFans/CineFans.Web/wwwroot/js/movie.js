// movie.js

async function fetchMoviesWithComments() {
    try {
        const response = await fetch('https://localhost:7263/api/Movie/movies-with-comments');
        if (response.ok) {
            const movies = await response.json();
            console.log(movies);  // Verifica si los datos son correctos
            displayMoviesWithComments(movies);
        } else {
            console.error('Error fetching movies:', response.status);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

// Función para mostrar las películas con comentarios
function displayMoviesWithComments(movies) {
    const movieList = document.getElementById('movieList'); // Contenedor donde mostrar las películas
    movieList.innerHTML = ''; // Limpiar el contenedor antes de agregar nuevas películas

    movies.forEach(movie => {
        const movieItem = document.createElement('div');
        movieItem.classList.add('movie-item');  // Añadir una clase para estilizar

        // Construir el contenido HTML para cada película
        movieItem.innerHTML = `
            <h3>${movie.title}</h3>
            <p><strong>Director:</strong> ${movie.director}</p>
            <p><strong>Year:</strong> ${movie.year}</p>
            <p><strong>Genre:</strong> ${movie.genreName}</p>
            <p>${movie.description}</p>
            <img src="${movie.imageUrl}" alt="${movie.title}" style="max-width: 200px;"/>
            <h4>Comments:</h4>
            <ul>
                ${movie.comments.map(comment => `
                    <li>
                        <strong>User ${comment.userId}:</strong> ${comment.text}
                        <br><em>(${new Date(comment.date).toLocaleString()})</em>
                    </li>
                `).join('')}
            </ul>
        `;
        movieList.appendChild(movieItem);
    });
}

// Llamar a la función para cargar las películas con comentarios al cargar la página
document.addEventListener('DOMContentLoaded', fetchMoviesWithComments);
