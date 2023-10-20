using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio.IRepositorio
{
    //Hacemos a la interfaz generica para que trabaje con cualquier tipo de objeto mediante el codigo IRepositorio<T> where T : class
    public interface IRepositorio<T> where T : class
    {
        Task<T> Obtener(int id); //devuelve un objeto de tipo T con el parametro Id
                                 //Un segundo metodo que es una lista IEnumerable de tipo T cuyo nombre es ObtenerTodos
        Task<IEnumerable<T>> ObtenerTodos(
             Expression<Func<T, bool>> filtro = null, //Es un filtro 
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, //Para ordenar la lista
             string incluirPropiedades = null, //se encarga de hacer los enlaces con otros objetos
             bool isTracking = true //cuando queramos acceder un objeto y al mismo tiempo lo queremos modificar, esto es cuando es false
             );
        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );
        Task Agregar(T entidad);//Para agregar un nuevo registro a la base de datos
        void Remover(T entidad);//Para eliminar un registro de la base de datos
        void RemoverRango(IEnumerable<T> entidad);//Remueve un rango de objetos
    }
}
