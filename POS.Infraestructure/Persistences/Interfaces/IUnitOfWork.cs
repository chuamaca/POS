using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Interfaces
{
    //Liberar objetos en memoria
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o matricula de nuestras interface a Nivel Repository

        ICategoryRepository Category {  get; }

        void SaveChanges();

        Task saveChangesAsync();

    }
}
