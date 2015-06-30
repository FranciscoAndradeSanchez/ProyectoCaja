using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CajaPopular.Models
{
    public class CotizacionResult
    {
        public CotizacionResult(int numAbono, double saldoInicial, double intereses, double abono, double saldorestante, double saldoTotal, double interesTotal)
        {
            NumAbono = numAbono;
            SaldoInicial = saldoInicial;
            Intereses = intereses;
            Abono = abono;
            Saldorestante = saldorestante;
            SaldoTotal = saldoTotal;
            InteresTotal = interesTotal;
        }

        public CotizacionResult()
        {
            
        }

        public int NumAbono { get; set; }

        public double SaldoInicial { get; set; }
        public double Intereses { get; set; }
        public double Abono { get; set; }
        public double Saldorestante { get; set; }
        public double SaldoTotal { get; set; }
        public double InteresTotal { get; set; }

        public virtual List<CotizacionResult> CotizacionResultssList { get; set; }
    }
}