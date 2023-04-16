using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using proyecto.Models;
using System.Threading.Tasks;

namespace proyecto.Controllers
{
    public class SitiosController
    {
            readonly SQLiteAsyncConnection connection;


            // Constructor de clase
            public SitiosController(String dbpath)
            {
                // Crear una nueva conexion hacia la base de datos
                connection = new SQLiteAsyncConnection(dbpath);

                // Crear los objetos de base de datos que vamos a ocupar
                connection.CreateTableAsync<Sitios>().Wait();
            }
            // Creacion de las operaciones CRUD - DB
            // Create 

            public Task<int> Savesitio(Sitios sitios)
            {
                if (sitios.id != 0)
                    return connection.UpdateAsync(sitios);
                else
                    return connection.InsertAsync(sitios);
            }
            // Read
            public Task<List<Sitios>> GetListsitio()
            {
                return connection.Table<Sitios>().ToListAsync();
            }
            public Task<Sitios> Getsitio(int pid)
            {
                return connection.Table<Sitios>()
                    .Where(i => i.id == pid)
                    .FirstOrDefaultAsync();
            }
            // Delete
            public Task<int> Deletesitio(Sitios sitios)
            {
                return connection.DeleteAsync(sitios);
            }

        }
}
