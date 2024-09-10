using System.Collections.Generic;

namespace Croquis
{

    public partial class JsonObject
    {
        public long Name { get; set; }
        public long Tamano { get; set; }
        public long Disponible { get; set; }
        public List<Product> Products { get; set; }
    }

    public partial class Product
    {
        public int IdAlistamientoOrden { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSede { get; set; }
        public int IdProceso { get; set; }
        public int NoDocumento { get; set; }
        public int IdServicio { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Posicion { get; set; }
        public int IdAlistamiento { get; set; }
        public int IdProducto { get; set; }
        public string Lote { get; set; }
        public int Cantidades { get; set; }
        public int Unidades { get; set; }
        public double Kilos { get; set; }
        public string EmbalajeE { get; set; }
    }
}
