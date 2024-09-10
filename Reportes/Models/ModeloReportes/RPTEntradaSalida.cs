using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes.Models.ModeloReportes
{
    public class RPTEntradaSalida
    {

        public class Movimiento
        {
                    public string IdEmpresa { get; set; }
                    public string IdSede { get; set; }
                    public string IdProceso { get; set; }
                    public string NoDocumento { get; set; }
                    public string Empresa { get; set; }
                    public string Nit { get; set; }
                    public string Sede { get; set; }
                    public string Proceso { get; set; }
                    public DateTime FechaInicio { get; set; }
                    public string Observaciones { get; set; }
                    public string Placa { get; set; }
                    public string Remision { get; set; }
                    public string RazonSocial { get; set; }
                    public string NitCliente { get; set; }
                    public string Direccion { get; set; }
                    public string Telefono { get; set; }
                    public string Ciudad { get; set; }
                    public string Departamento { get; set; }
                    public string Pais { get; set; }
                    public string Usuario { get; set; }
                    public string NombreEmpleado { get; set; }
                    public string SubBodega { get; set; }
                    public string Conductor { get; set; }
                    public string ServicioSubProceso { get; set; }
                    public string CodigoDocumento { get; set; }
                    public string IdServicio { get; set; }
                    public string Cobrar { get; set; }
                    public string IdSubProceso { get; set; }
                    public DateTime FechaFinal { get; set; }
                    public string IdEstado { get; set; }
                    public string MotivoAnulacion { get; set; }
                    public string MotivoNoCobro { get; set; }
                    public string Temperatura { get; set; }
                    public string NombreEspecie { get; set; }
                    public string Sello { get; set; }
                    public string Color { get; set; }
                    public string Aroma { get; set; }
                    public string Muelle { get; set; }     
                    public string DireccionSede { get; set; }
                    public string TextoGrandeSede { get; set; }
                    public string PrefijoSede { get; set; }
                    public string ObservacionSede { get; set; }
                    public string Correo { get; set; }      
                    public string NoGuiaCarnica { get; set; }
                    public List< MovimientoItem>   ListDetalle { get; set; }
                    public List<Tarea> ListTarea { get; set; }
                    public int  CantidadPosiciones { get; set; }
                    public decimal TotaKilos { get; set; }

        }
    public class MovimientoItem
    {
        public string IdEmpresa { get; set; }
        public string IdSede { get; set; }
        public string IdProceso { get; set; }
        public string NoDocumento { get; set; }
        public string IdItem { get; set; }
        public string Posicion { get; set; }
        public string DescripcionProducto { get; set; }
        public string CodEscrito { get; set; }
        public string Descripcion { get; set; }
        public string Embalaje { get; set; }
        public string Lote { get; set; }
        public DateTime Vencimiento { get; set; }
        public double EntradaCantidades { get; set; }
        public double EntradaUnid { get; set; }
        public double EntredaKilo { get; set; }
        public decimal KilogramoProm { get; set; }
        public string IdServicio { get; set; }
        public string SalidaUnid { get; set; }
        public string SalidaKilo { get; set; }
        public string SalidaCantidades { get; set; }
        public decimal Temperatura { get; set; }
        public double KilogramoPromSale { get; set; }
    }


        public class Tarea
        {
            public string IdEmpresa { get; set; }
            public string IdSede { get; set; }
            public string IdProceso { get; set; }
            public string NoDocumento { get; set; }
            public string IdServicio { get; set; }
            public string IdEmpleado { get; set; }
            public string Empleado { get; set; }
            public string IdTarea { get; set; }
            public string NombreTarea { get; set; }
        }
    }
}
