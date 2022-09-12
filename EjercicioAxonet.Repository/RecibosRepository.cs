using Domain.Common;
using EjercicioAxonet.DataBase;
using EjercicioAxonet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioAxonet.Repository
{
    public class RecibosRepository : Repository<Recibos>
    {
        public RecibosRepository(EjercicioAxonetContext context) : base(context) { }
    }
}
