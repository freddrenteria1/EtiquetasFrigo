using System;
using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Cors;

using BasculasLibrary;

namespace WebApiBascula.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EtiquetaController : ApiController
    {
        //string dominio = "frigometro";
        //string usuario = "Frc01OpeLogis08";
        //string contra = "Frigo2019";
        string dominio = "FRIGOMETRO";
        string usuario = "Frg01OpeLogis11";
        string contra = "Muelle01";
        //string dominio = "Frc01OpeLogis08";
        //string usuario = "Administrador";
        //string contra = "Frigometro2019*";
        //public Boolean GetPruebaEscribir()
        //{
        //    //if (impersonateValidUser("LOGISTICA_1", "FRIGORIFICO", ".Secretarias1."))
        //    //{


        //    //}
        //    return true;
        //}

        public string GetSaco()
        {

            try
            {

                BasculasLibrary.ClassSaco sa = new BasculasLibrary.ClassSaco("Nombre de Producto", "3 23 23", "0360", 24, 21.2, "B00012323", "01/10/2021", "Saco", "INT34234234");

                return "Ok";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string GetPrueba()
        {
            try
            {
                //if (impersonateValidUser("Logistica_4", "FRIGORIFICO", ".Congela2013."))
                //if (impersonateValidUser("Empaque1", "FRIGORIFICO", "Empaq01"))
                //if (impersonateValidUser("LOGISTICA_1", "FRIGORIFICO", ".Secretarias1."))
                //if (impersonateValidUser("CONSERVACION", "FRIGORIFICO", "Conserva-2019"))
                //if (impersonateValidUser(usuario, dominio, contra))
                //{


                BasculasLibrary.Etiqueta eti = new BasculasLibrary.Etiqueta("Conserva", "1 1 1", "1/1/2019", "moco moco", "000", "Prod", "Emba", 2, 1, 3, "1/1/2019", "1111", 1, 1, "fredd");


                //Inserte aquí el código que se ejecuta en el contexto de seguridad de un usuario específico.
                undoImpersonation();
                //}
                //else
                // {
                //     //Error en la suplantación. Por lo tanto, incluya aquí un mecanismo a prueba de errores.
                //     return "Error de Login: " + dominio.ToString() + " " + usuario.ToString() + " " + contra.ToString();
                // }

                return "ok";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }


        public Boolean GetEtiquetaprueba1()
        {
            //if (impersonateValidUser(usuario, dominio, contra))
            //{
            //Reportes.Dataset.DataSetMovimientosTableAdapters.TerceroTableAdapter Ter = new Reportes.Dataset.DataSetMovimientosTableAdapters.TerceroTableAdapter();
            //string Cliente = Ter.GetData(1, 1)[0].RazonSocial;

            BasculasLibrary.Cedula cedu = new BasculasLibrary.Cedula("Conservacion", "1 1 1", "Avidesa", "5008", "PECHUGA", 12, 12, "", "1234", "20/01/2020", "SACO", "INT123456", "234213", 1, 1, 1,"");
            //}
            return true;
        }







        // GET: api/Etiqueta
        public Boolean GetCedula(int idServicio, string posicion, string fecha, string cliente, string CodProd, string descProducto, string embajaje, double undEmb, double cantidad, double peso,
            string fechaVence, string lote, Int64 NoDocumento, int noItem, string usuario)
        {

            string Servicio = "";

            switch (idServicio)
            {
                case 1:
                    {
                        Servicio = "Refrigera";
                        break;
                    }
                case 2:
                    {
                        Servicio = "Seco"; break;
                    }
                case 3:
                    {
                        Servicio = "Congelación";
                        break;
                    }
                case 4:
                    {
                        Servicio = "Conservación";
                        break;
                    }
                default:
                    Servicio = "";
                    break;
            }

            //BasculasLibrary.Form1 f = new BasculasLibrary.Form1(Servicio, posicion, fecha, cliente, CodProd, descProducto, embajaje, undEmb, cantidad, peso, fechaVence,
            //    lote, NoDocumento, noItem, usuario);

            //Aqui consiguramos para las celulas de no las de empaque de uno en uno


            //if (impersonateValidUser("LOGISTICA_1", "FRIGORIFICO", ".Secretarias1."))
            //if (impersonateValidUser("Logistica_4", "FRIGORIFICO", ".Congela2013."))
            //if (impersonateValidUser("Empaque1", "FRIGORIFICO", "Empaq01"))
            //if (impersonateValidUser("CONSERVACION", "FRIGORIFICO", "Conserva-2019"))
            //if (impersonateValidUser("Camara", "FRIGORIFICO", "Conserva-2019"))
            //if (impersonateValidUser("Frg01OpeLogis10", "Frigometro", ".Congela2013."))
            //if (impersonateValidUser("Frg01OpeLogis10", "Frigometro", ".Congela2013."))
            //if (impersonateValidUser(usuario, dominio, contra))
            //{

            //BasculasLibrary.Etiqueta eti = new BasculasLibrary.Etiqueta("Conserva", "1 1 1", "1/1/2019", "moco moco", "000", "Prod", "Emba", 2, 1, 3, "1/1/2019", "1111", 1, 1, "fredd");
            BasculasLibrary.Etiqueta eti = new BasculasLibrary.Etiqueta(Servicio, posicion, fecha, cliente, CodProd, descProducto, embajaje, undEmb, cantidad, peso, fechaVence,
           lote, NoDocumento, noItem, usuario);


            //Inserte aquí el código que se ejecuta en el contexto de seguridad de un usuario específico.
            //undoImpersonation();
            //}


            //Reportes.Movimientos.RptCedula rpt = new Reportes.Movimientos.RptCedula();


            //rpt.SetParameterValue("Posicion", posicion);
            //rpt.SetParameterValue("Fecha", fecha);
            //rpt.SetParameterValue("Cliente", cliente);
            //rpt.SetParameterValue("Cod_Producto", CodProd);
            //rpt.SetParameterValue("Desc_Producto", descProducto);
            //rpt.SetParameterValue("Embalaje", embajaje);
            //rpt.SetParameterValue("Cantidad", cantidad);
            //rpt.SetParameterValue("Peso", peso);
            //rpt.SetParameterValue("FechaVence", fechaVence);
            //rpt.SetParameterValue("Lote", lote);

            //string NombreImpresora = "ImpreEtiqueta";


            //PrintDocument pdoc = new PrintDocument();
            //string defaultPrinter = string.Empty;
            //foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            //{
            //    if (strPrinter.Equals(NombreImpresora))
            //    {
            //        defaultPrinter = NombreImpresora;
            //        break;
            //    }
            //}
            //pdoc.PrinterSettings.PrinterName = defaultPrinter;

            //rpt.PrintOptions.PrinterName = NombreImpresora;
            //rpt.PrintToPrinter(1, false, 0, 0);

            return true;
        }


     

        public bool GetEtiquetaEspe(string Nombre1, string Nombre2, int i, int cantidad)
        {
            try
            {
                BasculasLibrary.Etiqueta eti = new BasculasLibrary.Etiqueta(Nombre1, Nombre2, i.ToString(), cantidad);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void PostEtiqueta([FromBody] Models.prodEtiqueta value)
        {
            int i;
            i = 0;

            BasculasLibrary.Cedula cedu = new BasculasLibrary.Cedula("Conservacion", value.posicion,
                value.razonSocial, value.CodProducto, value.Producto, value.Unidades, value.Kilos, "",
                value.lote, value.fechaVence, value.embalaje, value.CodigoINT, value.InformacionMovimiento,
                value.IdEmpresa, value.IdTercero, value.IdSede,"");

        }

        public Boolean GetSaco1(string Producto, string CodProducto, int Unidades, double Kilos, string Lote, string FechaVence, string Embalaje, string Posicion, string CodigoINT)
        {

            BasculasLibrary.ClassSaco cedu = new BasculasLibrary.ClassSaco(Producto, CodProducto, Posicion, Unidades, Kilos, Lote, FechaVence, Embalaje, CodigoINT);

            //"Conservacion", Posicion, Cliente, CodProducto, Producto, Convert.ToInt32(Unidades), Kilos, "", Lote, FechaVence, Embalaje, CodigoINT, InformacionMovimiento, IdEmpresa, IdSede, IdTercero);

            return true;


        }

        public Boolean GetEtiqueta(string Producto, string CodProducto, double Unidades, double Kilos, string Lote, string FechaVence, string Embalaje, string Posicion, string CodigoINT, string InformacionMovimiento, int IdEmpresa, int IdTercero, int IdSede)
        {
            //if (impersonateValidUser("LOGISTICA_1", "FRIGORIFICO", ".Secretarias1."))
            //if (impersonateValidUser("CONSERVACION", "FRIGORIFICO", "Conserva-2019"))
            //if (impersonateValidUser("Logistica_4", "FRIGORIFICO", ".Congela2013."))
            //if (impersonateValidUser("Empaque1", "FRIGORIFICO", "Empaq01"))
            //if (impersonateValidUser(usuario, dominio, contra))
            //{

            Console.WriteLine("Inicia Etiqueta");

            //Reportes.Dataset.DataSetMovimientosTableAdapters.TerceroTableAdapter Ter = new Reportes.Dataset.DataSetMovimientosTableAdapters.TerceroTableAdapter();
            //string Cliente = Ter.GetData(IdEmpresa, IdTercero)[0].RazonSocial;
            //string Direccion = Ter.GetData(IdEmpresa, IdTercero)[0].Direccion;
            string Cliente = "AVSA S.A (B003)";
            string Direccion = "CRA 34 # 19A-69";


            Console.WriteLine("Termina buscar Datos");
            BasculasLibrary.Cedula cedu = new BasculasLibrary.Cedula("Conservacion", Posicion, Cliente, CodProducto, Producto, Convert.ToInt32(Unidades), Kilos, "", Lote, FechaVence, Embalaje, CodigoINT, InformacionMovimiento, IdEmpresa, IdSede, IdTercero, Direccion);

            //}
            //Reportes.Movimientos.RptEtiqueta rpt = new Reportes.Movimientos.RptEtiqueta();

            //Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();

            //Reportes.Dataset.DataSetMovimientosTableAdapters.TerceroTableAdapter Ter = new Reportes.Dataset.DataSetMovimientosTableAdapters.TerceroTableAdapter();
            //Reportes.Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter sed = new Reportes.Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter();



            //Ter.Fill(data.Tercero, IdEmpresa, IdTercero);
            //sed.Fill(data.Sede, IdEmpresa, IdSede);


            //rpt.SetDataSource(data);

            //rpt.SetParameterValue("Producto", Producto);
            //rpt.SetParameterValue("CodProducto", CodProducto);
            //rpt.SetParameterValue("Unidades", Unidades);
            //rpt.SetParameterValue("Kilos", Kilos);
            //rpt.SetParameterValue("Lote", Lote);
            //rpt.SetParameterValue("FechaVence", FechaVence);
            //rpt.SetParameterValue("Embalaje", Embalaje);
            //rpt.SetParameterValue("Posicion", Posicion);
            //rpt.SetParameterValue("CodigoINT", CodigoINT);
            //rpt.SetParameterValue("InformacionMovimiento", InformacionMovimiento);


            //// rpt.SetParameterValue("Fecha", fecha);
            //// //rpt.SetParameterValue("Cliente", cliente);
            ////;


            //// rpt.SetParameterValue("Cantidad", cantidad);
            //// rpt.SetParameterValue("Peso", peso);

            //if (impersonateValidUser("Empaque1", "FRIGORIFICO", "Empaq01"))
            //{



            //    string NombreImpresora = "ImpreEtiqueta";


            //    PrintDocument pdoc = new PrintDocument();
            //    string defaultPrinter = string.Empty;
            //    foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            //    {
            //        if (strPrinter.Equals(NombreImpresora))
            //        {
            //            defaultPrinter = NombreImpresora;
            //            break;
            //        }
            //    }
            //    pdoc.PrinterSettings.PrinterName = defaultPrinter;

            //    rpt.PrintOptions.PrinterName = NombreImpresora;
            //    rpt.PrintToPrinter(1, false, 0, 0);
            //}
            return true;
        }


        //public Boolean EtiqutaEspecia(producto As Integer, descripcion As String, peso As Integer)
        public Boolean GetEtiquetaEspacial(int producto, string descripcion, int peso)
        {
            //BasculaLibrary.Etiqueta et = new BasculaLibrary.Etiqueta(producto, descripcion, peso);

            return true;


        }


        public Boolean GetEtiqueta(int producto, string descripcion, int peso)
        {
            try
            {
                //BasculaLibrary.Etiqueta et = new BasculaLibrary.Etiqueta(producto, descripcion, peso);


                return true;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }







        // GET: api/Etiqueta/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Etiqueta
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Etiqueta/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Etiqueta/5
        public void Delete(int id)
        {
        }


        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;


        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
        String lpszDomain,
        String lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
        int impersonationLevel,
        ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        //public void Page_Load(Object s, EventArgs e)
        //{

        //}

        private bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }
    }
}
