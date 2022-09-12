using Domain.Common;
using EjercicioAxonet.DataBase;
using EjercicioAxonet.Domain;
using System;

namespace EjercicioAxonet.Repository
{
    public class ProveedoresRepository : Repository<Proveedores>
    {
        public ProveedoresRepository(EjercicioAxonetContext context) : base(context) { }
    }
}
