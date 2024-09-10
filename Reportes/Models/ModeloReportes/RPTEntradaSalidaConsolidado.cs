using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes.Models.ModeloReportes
{
    public class RPTEntradaSalidaConsolidado
    {
        public class ListaConsolidado
        {
            public List<ConsolidadoDetalle> ListDetalle { get; set; }
        }
        public class ConsolidadoDetalle
        {
            public string RazonSocial { get; set; }
            public string Descripcion { get; set; }
            public string CodEscrito { get; set; }
            public int NoDocumento { get; set; }
            public string Lote { get; set; }
            public double EntradaUnid { get; set; }
            public double EntredaKilo { get; set; }
            public double EntradaCantidades { get; set; }
            public string IdVehiculo { get; set; }
            public string Remision { get; set; }
            public int IdTercero { get; set; }
            public DateTime FechaInicio { get; set; }
            public int IdItem { get; set; }
            public double IdProceso { get; set; }
            public double SalidaUnid { get; set; }
            public double SalidaKilo { get; set; }
            public double SalidaCantidades { get; set; }
            public string Sello { get; set; }
            public string NombreConductor { get; set; }
            public DateTime FechaVencimiento { get; set; }
            public int IdServicio { get; set; }
            public int IdSede { get; set; }
            public int IdEmpresa { get; set; }
            public int Indicador { get; set; }
        }
    }
}
