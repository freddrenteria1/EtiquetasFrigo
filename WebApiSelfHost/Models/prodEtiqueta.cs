using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBascula.Models
{
    public class prodEtiqueta
    {
  
        public string Producto { get; set; }
        public string CodProducto { get; set; }
        public int Unidades { get; set; }
        public int Kilos { get; set; }
        public string lote { get; set; }
        public string fechaVence { get; set; }
        public string embalaje { get; set; }
        public string posicion { get; set; }
        public string CodigoINT { get; set; }
        public string InformacionMovimiento { get; set; }
        public int IdTercero { get; set; }
        public int IdEmpresa { get; set; }
        public string razonSocial { get; set; }
        public int IdSede { get; set; }


    }
   
}