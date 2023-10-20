using Microsoft.EntityFrameworkCore;
using Genesys.AccesoDatos.Data;
using Genesys.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repositorio(ApplicationDbContext _db)
        {
            _db = _db;
            this.dbSet = _db.Set<T>();
        }
        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad); //insert into table
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id); //select  * from (solo por id)

        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet; //Configruamos propiedad de tipo IQueryable llamada query
            if (filtro != null) //Si el filtro esta lleno entonces filtra la consutla 
            {
                query = query.Where(filtro); //select * from where
            }
            if (incluirPropiedades != null) //Si incluiPropiedades tiene valor recorre la cadena con un foreach
            {
                //hace la caderna de caracteres de tipo char mediante .Split lo transforma en new Char
                //lo separa por comas con { ','} y con RemoveEmptyEntries quita los espacios vacios
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //ejemplo incluye las propiedades de: "Catergoria,Marca,etc"
                }
            }
            if (orderBy != null) //ordena la lista de acuerdo al parametro que recibe
            {
                query = orderBy(query);
            }
            if (!isTracking) // si es falso entonces se aplica el AsNoTracking que no lo tracke porque lo vas a modificar
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync(); //Regresa la lista asincrona
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet; //Configruamos propiedad de tipo IQueryable llamada query
            if (filtro != null) //Si el filtro esta lleno entonces filtra la consutla 
            {
                query = query.Where(filtro); //select * from where
            }
            if (incluirPropiedades != null) //Si incluiPropiedades tiene valor recorre la cadena con un foreach
            {
                //hace la caderna de caracteres de tipo char mediante .Split lo transforma en new Char
                //lo separa por comas con { ','} y con RemoveEmptyEntries quita los espacios vacios
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //ejemplo incluye las propiedades de: "Catergoria,Marca,etc"
                }
            }
            if (!isTracking) // si es falso entonces se aplica el AsNoTracking que no lo trackee porque lo vas a modificar
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }
}
