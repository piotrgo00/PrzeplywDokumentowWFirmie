using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzeplywDokumentowWFirmie.Logic
{
    public interface IItem
    {
        void Create();
        void Remove(int id);
        IItem getItemById(int id);
    }
}
