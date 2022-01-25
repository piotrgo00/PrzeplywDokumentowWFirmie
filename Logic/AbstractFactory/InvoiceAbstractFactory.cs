using System;
using PrzeplywDokumentowWFirmie.Models;

namespace PrzeplywDokumentowWFirmie.Logic.AbstractFactory
{
    public interface InvoiceAbstractFactory
    {
        String getHTML(Order order);
    }
}
