using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IPrestadorRepository : IRepositoryBase<PRESTADOR>
    {
        PRESTADOR CheckExist(PRESTADOR item);
        PRESTADOR GetItemById(Int32 id);
        List<PRESTADOR> GetAllItens();
        List<PRESTADOR> GetAllItensAdm();
        List<PRESTADOR> ExecuteFilter(String nome, String razao, String cnpj, String email);
    }
}
