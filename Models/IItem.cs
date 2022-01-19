using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzeplywDokumentowWFirmie.Logic
{
    public interface IItem
    {
        void delete(int id);
        void add(IItem item);
        IItem findItem(int id);
    }
}
