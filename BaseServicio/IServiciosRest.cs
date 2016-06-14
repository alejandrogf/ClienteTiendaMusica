using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServicio
{
    public interface IServiciosRest<TModelo>
    {
        Task<TModelo> Add(TModelo model);
        Task Update(TModelo model);
        Task Delete(int id);
        Task Delete(TModelo model);

        List<TModelo> Get(String paramUrl = null);
        List<TModelo> Get(Dictionary<String, String> param);
        TModelo Get(int id);

    }
}
