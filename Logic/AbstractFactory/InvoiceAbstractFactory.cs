using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrzeplywDokumentowWFirmie.Models;

namespace PrzeplywDokumentowWFirmie.Logic.AbstractFactory
{
    public interface InvoiceAbstractFactory
    {
        Invoice GetInvoice();
        String getHTML(Order order);
    }
}
