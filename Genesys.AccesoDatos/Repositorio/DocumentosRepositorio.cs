using Genesys.AccesoDatos.Data;
using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio
{
    public class DocumentosRepositorio : Repositorio<Documentos>, IDocumentosRepositorio
    {
        private readonly ApplicationDbContext _db;
        public DocumentosRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar (Documentos documentos)
        {
            var documentosBD = _db.Documentos.FirstOrDefault(b => b.IdDocumento == documentos.IdDocumento);
            if (documentosBD != null)
            {
                documentosBD.NombreDocumento = documentos.NombreDocumento;
                documentosBD.Archivo = documentos.Archivo;
                documentosBD.IdEmpleado = documentos.IdEmpleado;
                documentosBD.StatusDocumento = documentos.StatusDocumento;
                _db.SaveChanges(); //Guarda los cambios en la base de datos
            }
        }
    }
}
