using System;
using System.Collections.Generic;
using System.Data;
using CrystalDecisions.Shared;

namespace Reportes.Clases
{
    public class CargarReporte
    {

        public Reportes.Movimientos.RptEntradasSalidasGuia MovimientoGuia(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, int NoDocumento, int Item)
        {
            Reportes.Movimientos.RptEntradasSalidasGuia rpt = new Movimientos.RptEntradasSalidasGuia();
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();

            try
            {

                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter adMovimiento = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter adMovimientoItem = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter adSede = new Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasGuiaCarnicaTableAdapter adguia = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasGuiaCarnicaTableAdapter();


                adMovimiento.Fill(data.EntradasSalidas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adMovimientoItem.FillBySalida(data.EntradasSalidasItems, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adSede.Fill(data.Sede, IdEmpresa, IdSede);
                adguia.FillByGuia(data.EntradasSalidasGuiaCarnica, IdEmpresa, IdSede, NoDocumento, IdServicio, Item);

                string ano = data.EntradasSalidas[0].FechaFinal.Year.ToString().Substring(2, 2);

                string NoGuia = data.Sede[0].NoGuiaCarnica.ToString() + "-" + data.EntradasSalidasGuiaCarnica[0].NumeroGuia.ToString() + "-" + ano;


                //ReportDocument cryRpt = new ReportDocument();
                //cryRpt.Load(@"S:\Cod\Iglusoft\Iglusoft20\Reportes\Movimientos\RptEntradasSalidas.rpt");

                rpt.SetDataSource(data);
                rpt.SetParameterValue("NoGuia", NoGuia);
                rpt.SetParameterValue("Codigo", data.Sede[0].NoGuiaCarnica.ToString());
                return rpt;

            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;

            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }


            return rpt;
        }

        public bool MovimientoExportar(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, Int64 NoDocumento, string ruta)
        {
            bool exportado = false;
            Reportes.Movimientos.RptNEntradasSalidas rpt;


            rpt = Movimiento(IdEmpresa, IdSede, IdProceso, IdServicio, NoDocumento);
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ruta);
            //rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, rutaExcel);

            exportado = true;




            return exportado;
        }

        public bool MovimientoExportarDocExcel(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, Int64 NoDocumento, string rutaExcel)
        {
            bool exportado = false;
            Reportes.Movimientos.RptNEntradasSalidas rpt;


            rpt = Movimiento(IdEmpresa, IdSede, IdProceso, IdServicio, NoDocumento);
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, rutaExcel);

            exportado = true;




            return exportado;
        }

        public bool AlistamientoExportar(int IdEmpresa, int IdSede, int IdServicio, int NoDocumento, string ruta)
        {
            bool exportado = false;
            Reportes.Alistamiento.RptAlistamientoPosicion rpt;


            rpt = AlistamientoPosicionTipoAplicacion(IdEmpresa, IdSede, IdServicio, NoDocumento,2);
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);

            exportado = true;

            return exportado;
        }

        //Exportar consolidado
        public bool ConsolidadoExportar(int idEmpresa, int idSede, int idServicio, int idProceso, string placa, string NoRemision, string rutaConsolidado)
        {
            //bool exportar = false;

            try
            {
                Reportes.Movimientos.RptConsolidado rptConsolidado;

                rptConsolidado = Consolidado(idEmpresa, idSede, idServicio, idProceso, placa, NoRemision);
                rptConsolidado.ExportToDisk(ExportFormatType.PortableDocFormat, rutaConsolidado);

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool ConsolidadoExportarExcel(int idEmpresa, int idSede, int idServicio, int idProceso, string placa, string NoRemision, string rutaExcelConsolidado)
        {
            //bool exportar = false;

            try
            {
                Reportes.Movimientos.RptConsolidado rptConsolidado;

                rptConsolidado = Consolidado(idEmpresa, idSede, idServicio, idProceso, placa, NoRemision);
                rptConsolidado.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, rutaExcelConsolidado);

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public static Reportes.Movimientos.RptProdInforme InformeProductos(int IdEmpresa, int idCliente)
        {
            Reportes.Movimientos.RptProdInforme rpt = new Reportes.Movimientos.RptProdInforme();

            Reportes.Dataset.DataSetProdInforme data = new Reportes.Dataset.DataSetProdInforme();

            try
            {

                Reportes.Dataset.DataSetProdInformeTableAdapters.ProdInformeTableAdapter ad = new Reportes.Dataset.DataSetProdInformeTableAdapters.ProdInformeTableAdapter();

                ad.Fill(data.ProdInforme, IdEmpresa, idCliente, 1);

                rpt.SetDataSource(data);


                return rpt;
            }
            catch (Exception)
            {



                //string mensaje;
                //mensaje = ex.Message;

            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }
            return rpt;

        }

        public static Reportes.WebClientes.RptComparativoJuegos ComparativoJuegos(int mes, int anio)
        {

            Reportes.WebClientes.RptComparativoJuegos rpt = new Reportes.WebClientes.RptComparativoJuegos();
            Reportes.Dataset.DataSetComparativosJugados data = new Reportes.Dataset.DataSetComparativosJugados();


            try
            {
                Reportes.Dataset.DataSetComparativosJugadosTableAdapters.ComparativoJuegosTableAdapter ad = new Reportes.Dataset.DataSetComparativosJugadosTableAdapters.ComparativoJuegosTableAdapter();
                Reportes.Dataset.DataSetComparativosJugadosTableAdapters.UsuariosNoJugaronTableAdapter add = new Reportes.Dataset.DataSetComparativosJugadosTableAdapters.UsuariosNoJugaronTableAdapter();


                ad.Fill(data.ComparativoJuegos, mes, anio);
                add.Fill(data.UsuariosNoJugaron, mes, anio);
                rpt.SetDataSource(data);

                rpt.Subreports[0].SetDataSource(data);
                DateTime fechaActual = new DateTime(anio, mes, 1);

                rpt.SetParameterValue("Mes", fechaActual.ToString("MMMM").ToUpperInvariant());

                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();

            }
            catch (Exception ex)
            {

                string mensaje;
                mensaje = ex.Message;
            }
            return rpt;
        }

        public static Reportes.Movimientos.RptSalidaCroquisAlistamiento croquis(int NoDocDef, int IdEmpresa, int IdSede)
        {
            Reportes.Movimientos.RptSalidaCroquisAlistamiento rptSalidaCroquisAlistamiento = new Reportes.Movimientos.RptSalidaCroquisAlistamiento();
            Reportes.Dataset.DataSetCroquisAlistamiento data = new Reportes.Dataset.DataSetCroquisAlistamiento();

            try
            {
                Reportes.Dataset.DataSetCroquisAlistamientoTableAdapters.AlistamientoCroquisTableAdapter adCroquis = new Reportes.Dataset.DataSetCroquisAlistamientoTableAdapters.AlistamientoCroquisTableAdapter();

                adCroquis.FillBy(data.AlistamientoCroquis, NoDocDef, IdEmpresa, IdSede);

                rptSalidaCroquisAlistamiento.SetDataSource(data);
            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;
            }
            return rptSalidaCroquisAlistamiento;
        }

        public static Reportes.Movimientos.RptConteoZebras2 conteoEtiquetas(DateTime fecha1, DateTime fecha2, int IdEmpresa, int IdSede, int IdImprimeTipo, string IdUsuario)
        {
            Reportes.Movimientos.RptConteoZebras2 rptConteo = new Reportes.Movimientos.RptConteoZebras2();
            Reportes.Dataset.DataSetImpresionZebras data = new Reportes.Dataset.DataSetImpresionZebras();
            //FUNCIONE

            try
            {

                Reportes.Dataset.DataSetImpresionZebrasTableAdapters.ImpresionZebrasTableAdapter ad = new Reportes.Dataset.DataSetImpresionZebrasTableAdapters.ImpresionZebrasTableAdapter();

                if (IdUsuario == "undefined" || IdUsuario == null)
                {
                    ad.Fill(data.ImpresionZebras, fecha1, fecha2, IdEmpresa, IdSede, IdImprimeTipo);
                }
                else
                {
                    ad.FillBy(data.ImpresionZebras, fecha1, fecha2, IdEmpresa, IdSede, IdUsuario);
                    //ad.GetCountEtiqFilterByUser(fecha1, fecha2, IdEmpresa, IdSede, IdImprimeTipo, IdUsuario);
                }


                rptConteo.SetDataSource(data);
            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;
            }
            return rptConteo;
        }

        public static Reportes.Movimientos.RptPesajesEmpaque reportePesajeEmpaque(string Fecha, int IdEmpresa, int IdSede)
        {
            Reportes.Movimientos.RptPesajesEmpaque rptPesajeEmpaque = new Movimientos.RptPesajesEmpaque();
            Reportes.Dataset.DataSetRptPesajeEmpaque data = new Dataset.DataSetRptPesajeEmpaque();

            int year = Int32.Parse(Fecha.Split('-')[0]);
            int month = Int32.Parse(Fecha.Split('-')[1]);
            int day = Int32.Parse(Fecha.Split('-')[2]);

            try
            {

                Reportes.Dataset.DataSetRptPesajeEmpaqueTableAdapters.PesajesEmpaqueTableAdapter ad = new Dataset.DataSetRptPesajeEmpaqueTableAdapters.PesajesEmpaqueTableAdapter();
                ad.Fill(data.PesajesEmpaque, year, month, day, IdEmpresa, IdSede);

                rptPesajeEmpaque.SetDataSource(data);
            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;
            }
            return rptPesajeEmpaque;
        }

        public Reportes.Movimientos.RptNEntradasSalidas Movimiento(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, Int64 NoDocumento)
        {
            Reportes.Movimientos.RptNEntradasSalidas rpt = new Movimientos.RptNEntradasSalidas();

            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            try
            {

                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter adMovimiento = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.TareasAsignadasTableAdapter adTareas = new Dataset.DataSetMovimientosTableAdapters.TareasAsignadasTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter adMovimientoItem = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter adSede = new Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter();


                adMovimiento.Fill(data.EntradasSalidas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adTareas.Fill(data.TareasAsignadas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                if (IdProceso == 1)
                {
                    adMovimientoItem.Fill(data.EntradasSalidasItems, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                }
                else
                {
                    adMovimientoItem.FillBySalida(data.EntradasSalidasItems, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);

                }
                adSede.Fill(data.Sede, IdEmpresa, IdSede);

                rpt.SetDataSource(data);
                rpt.Subreports[0].SetDataSource(data);

                DateTime fecha = Convert.ToDateTime("12/11/2021");
                rpt.SetParameterValue("VariableIng", "");

                if (data.EntradasSalidas[0].FechaFinal < fecha)
                {
                    //si es menor a la fecha no debe mostrar la temperatura
                    rpt.SetParameterValue("MostrarTempertura", false);
                    rpt.SetParameterValue("EncabezadoTemp", "false");
                }
                else
                {
                    rpt.SetParameterValue("MostrarTempertura", true);
                    rpt.SetParameterValue("EncabezadoTemp", "ok");
                }

                //ReportDocument cryRpt = new ReportDocument();
                //cryRpt.Load(@"S:\Cod\Iglusoft\Iglusoft20\Reportes\Movimientos\RptEntradasSalidas.rpt");



                if (data.TareasAsignadas.Count == 0)
                {
                    //rpt.ReportDefinition.ReportObjects[79].ObjectFormat.EnableSuppress = true;
                }

                return rpt;

            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;

            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }


            return rpt;
        }

        //NUEVO REPORTE DE ENTRADAS SALIDAS

        public Reportes.Movimientos.RptNEntradasSalidas Movimientos(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, Int64 NoDocumento)
        {
            Reportes.Movimientos.RptNEntradasSalidas rpt = new Movimientos.RptNEntradasSalidas();

            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            try
            {

                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter adMovimiento = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.TareasAsignadasTableAdapter adTareas = new Dataset.DataSetMovimientosTableAdapters.TareasAsignadasTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter adMovimientoItem = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter adSede = new Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter();


                adMovimiento.Fill(data.EntradasSalidas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);

                adTareas.Fill(data.TareasAsignadas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);



                adMovimientoItem.Fill(data.EntradasSalidasItems, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adSede.Fill(data.Sede, IdEmpresa, IdSede);



                //ReportDocument cryRpt = new ReportDocument();
                //cryRpt.Load(@"S:\Cod\Iglusoft\Iglusoft20\Reportes\Movimientos\RptEntradasSalidas.rpt");

                rpt.SetDataSource(data);
                rpt.Subreports[0].SetDataSource(data);

                if (data.TareasAsignadas.Count == 0)
                {
                    rpt.ReportDefinition.ReportObjects[79].ObjectFormat.EnableSuppress = true;
                }

                return rpt;

            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;

            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }


            return rpt;
        }

        //FIN

        public static WebClientes.RptResultadoJuegos ResultadoJuegos(int IdEmpresa, int IdSedeIni, int IdSedeFin, int IdJuego, int IdRechazado, int mes, int ano, int Top)
        {
            WebClientes.RptResultadoJuegos rpt = new WebClientes.RptResultadoJuegos();
            Dataset.DataSetResultadoJuegos data = new Dataset.DataSetResultadoJuegos();

            try
            {
                string IdUsuario = "nouser";

                Dataset.DataSetResultadoJuegosTableAdapters.ResultadoJuegosTableAdapter adResJuegos = new Dataset.DataSetResultadoJuegosTableAdapters.ResultadoJuegosTableAdapter();
                adResJuegos.FillResultadosJuegos(data.ResultadoJuegos, IdEmpresa, IdSedeIni, IdSedeFin, IdJuego, IdRechazado, mes, ano, IdRechazado, IdUsuario, Top);
                //adResJuegos.Fill(data.ResultadoJuegos);

                rpt.SetDataSource(data);
                return rpt;
            }

            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
                throw;
            }

            finally
            {
                data.Dispose();
            }
        }

        public Reportes.Movimientos.RptNEntradasSalidas MovimientoPosiActual(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, Int64 NoDocumento)
        {
            Reportes.Movimientos.RptNEntradasSalidas rpt = new Movimientos.RptNEntradasSalidas();
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            try
            {

                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter adMovimiento = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter adMovimientoItem = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter adSede = new Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter();

                adMovimiento.Fill(data.EntradasSalidas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adMovimientoItem.FillByPosiActual(data.EntradasSalidasItems, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adSede.Fill(data.Sede, IdEmpresa, IdSede);

                //ReportDocument cryRpt = new ReportDocument();
                //cryRpt.Load(@"S:\Cod\Iglusoft\Iglusoft20\Reportes\Movimientos\RptEntradasSalidas.rpt");








                rpt.SetDataSource(data);

                rpt.SetParameterValue("MostrarTempertura", true);
                rpt.SetParameterValue("EncabezadoTemp", "ok");
                rpt.SetParameterValue("VariableIng", "");
                

                return rpt;

            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;

            }
            //finally
            //{
            //    data.Dispose();
            //    //rpt.Dispose();
            //}
            return rpt;
        }

        public static Reportes.Movimientos.RptConsolidado Consolidado(int idEmpresa, int idSede, int idServicio, int idProceso, string placa, string noRemision)
        {
            Reportes.Movimientos.RptConsolidado rpt = new Movimientos.RptConsolidado();
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();

            try
            {
                Reportes.Dataset.DataSetMovimientosTableAdapters.RepConsolidadoTableAdapter adMov = new Reportes.Dataset.DataSetMovimientosTableAdapters.RepConsolidadoTableAdapter();
                adMov.FillRepConsolIng(data.RepConsolidado, noRemision, placa, idEmpresa, idSede, idServicio, idProceso);

                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptOptimizacionPosiciones OptimizacionPosiciones(int IdEmpresa, int IdSede, int idTercero, double kilos)
        {

            Reportes.WebClientes.RptOptimizacionPosiciones rpt = new WebClientes.RptOptimizacionPosiciones();
            Reportes.Dataset.DataSetOP data = new Dataset.DataSetOP();
            try
            {
                Reportes.Dataset.DataSetOPTableAdapters.posicionesOptimizarByKiloTableAdapter adOp = new Reportes.Dataset.DataSetOPTableAdapters.posicionesOptimizarByKiloTableAdapter();
                adOp.Fill(data.posicionesOptimizarByKilo, IdEmpresa, IdSede, idTercero, kilos);
                //rpt.SetParameterValue("IdEmpresa", IdEmpresa);
                //rpt.SetParameterValue("IdSede", IdSede);
                //rpt.SetParameterValue("IdCliente", idTercero);
                //rpt.SetParameterValue("kilos", kilos);
                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
            }
            return rpt;
        }

        public static Reportes.WebClientes.RptInventarioXPosicion WebClienteInvClientePorPosicion(int idEmpresa, int idSede, int idServicio, int idTercero, int idSubBodega, int IdCamaraIni, int IdCamaraFin)
        {
            Reportes.WebClientes.RptInventarioXPosicion rpt = new WebClientes.RptInventarioXPosicion();
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();

            try
            {
                Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter adInv = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();

                adInv.Fill(data.InventarioDetalladoProducto, idEmpresa, idSede, idTercero, idServicio, 1, idSubBodega, IdCamaraIni, IdCamaraFin);
                adTercero.Fill(data.Tercero, idEmpresa, idTercero);


                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", idServicio);
                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptInventarioDetalladoXProducto WebClienteInvClienteDetaXProducto(int idEmpresa, int idSede, int idTercero, int idSubBodega, int idServicio, int IdCamaraIni, int IdCamaraFin)
        {
            Reportes.WebClientes.RptInventarioDetalladoXProducto rpt = new WebClientes.RptInventarioDetalladoXProducto();
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();


            try
            {
                Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter adInv = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();

                int IdEstado = 1;


                adInv.Fill(data.InventarioDetalladoProducto, idEmpresa, idSede, idTercero, idServicio, IdEstado, idSubBodega, IdCamaraIni, IdCamaraFin);
                adTercero.Fill(data.Tercero, idEmpresa, idTercero);
                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", idServicio);

                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        //INVENTARIO DETALLADO POR PRODUCTO ORIGINAL

        public static Reportes.WebClientes.RptInventarioDetalladoXProductoOriginal WebClienteInvClienteDetaXProductoOrg(int idEmpresa, int idSede, int idTercero, int idSubBodega, int idServicio, int IdCamaraIni, int IdCamaraFin)
        {
            Reportes.WebClientes.RptInventarioDetalladoXProductoOriginal rpt = new WebClientes.RptInventarioDetalladoXProductoOriginal();
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();


            try
            {
                Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter adInv = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();

                int IdEstado = 1;


                adInv.Fill(data.InventarioDetalladoProducto, idEmpresa, idSede, idTercero, idServicio, IdEstado, idSubBodega, IdCamaraIni, IdCamaraFin);
                adTercero.Fill(data.Tercero, idEmpresa, idTercero);
                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", idServicio);

                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        //FIN INVENTARIO DETALLADO POR PRODUCTO ORIGINAL

        public static Reportes.WebClientes.RptInventarioServicios WebClienteInvResumido(int idEmpresa, int idSede, int idTercero, int idSubBodega, int idServicio, int IdCamaraIni, int IdCamaraFin)
        {
            Reportes.WebClientes.RptInventarioServicios rpt = new WebClientes.RptInventarioServicios();
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();

            try
            {
                Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioServiciosTableAdapter adInv = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioServiciosTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.EntradasSalidasItemsTableAdapter adTotPos = new Reportes.Dataset.DataSetWebClientesTableAdapters.EntradasSalidasItemsTableAdapter();

                int CantidadPosi = (int)adInv.CantidadPosi(idEmpresa, idSede, idServicio, idTercero);


                adInv.Fill(data.InventarioServicios, idEmpresa, idSede, idTercero, idServicio, 1, idSubBodega, IdCamaraIni, IdCamaraFin);
                adTotPos.Fill(data.EntradasSalidasItems, idEmpresa, idSede, idTercero, idServicio, 1, idSubBodega, IdCamaraIni, IdCamaraFin);
                adTercero.Fill(data.Tercero, idEmpresa, idTercero);


                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", idServicio);
                rpt.SetParameterValue("CantidadPosi", CantidadPosi);


                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptInventarioServiciosCong WebClienteInvResumidoCong(int idEmpresa, int idSede, int idServicio, int IdCamaraIni, int IdCamaraFin, int IdTerceroIni, int IdTerceroFin, int IdNivelIni, int IdNivelFin)
        {
            Reportes.WebClientes.RptInventarioServiciosCong rpt = new WebClientes.RptInventarioServiciosCong();
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();

            try
            {
                Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioServCongTableAdapter adInvCong = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioServCongTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.EntradasSalidasItemsTableAdapter adTotPos = new Reportes.Dataset.DataSetWebClientesTableAdapters.EntradasSalidasItemsTableAdapter();




                adInvCong.Fill(data.InventarioServCong, idEmpresa, idSede, idServicio, 1, 15, IdCamaraIni, IdCamaraFin, IdTerceroIni, IdTerceroFin, IdNivelIni, IdNivelFin);



                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", idServicio);



                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptInventarioServiciosCongTunel WebClienteInvResumidoCongTunel(int idEmpresa, int idSede, int idServicio, int IdCamaraIni, int IdCamaraFin, int IdTerceroIni, int IdTerceroFin, int IdNivelIni, int IdNivelFin)
        {
            Reportes.WebClientes.RptInventarioServiciosCongTunel rpt = new WebClientes.RptInventarioServiciosCongTunel();
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();

            try
            {
                Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioServCongTunelTableAdapter adInvCong = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioServCongTunelTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.EntradasSalidasItemsTableAdapter adTotPos = new Reportes.Dataset.DataSetWebClientesTableAdapters.EntradasSalidasItemsTableAdapter();

                adInvCong.Fill(data.InventarioServCongTunel, idEmpresa, idSede, idServicio, 1, 15, IdCamaraIni, IdCamaraFin, IdTerceroIni, IdTerceroFin, IdNivelIni, IdNivelFin);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", idServicio);

                return rpt;
            }

            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptCierreResumidoProductos CierreResumidoProductos(int idEmpresa, int idSede, int idTercero, int servicio, DateTime fecha, int idSubBodega, int IdCamaraIni, int IdCamaraFin)
        {
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();
            Reportes.Dataset.DataSetWebClientesTableAdapters.CierreResumidoProductosTableAdapter adCierre = new Reportes.Dataset.DataSetWebClientesTableAdapters.CierreResumidoProductosTableAdapter();
            Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();
            Reportes.WebClientes.RptCierreResumidoProductos rpt = new Reportes.WebClientes.RptCierreResumidoProductos();


            try
            {
                adCierre.FillBy1(data.CierreResumidoProductos, idEmpresa, idSede, idTercero, servicio, idSubBodega, IdCamaraIni, IdCamaraFin, fecha);
                adTercero.Fill(data.Tercero, idEmpresa, idTercero);


                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaCierre", fecha.Date);
                rpt.SetParameterValue("IdServicio", servicio);


                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptCierreResumidoCodigos CierreResumidoCodigos(int idEmpresa, int idSede, int idTercero, int servicio, DateTime fecha, int idSubBodega, int IdCamaraIni, int IdCamaraFin)
        {
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();
            Reportes.Dataset.DataSetWebClientesTableAdapters.CierreResumidoCodigosTableAdapter adCierre = new Reportes.Dataset.DataSetWebClientesTableAdapters.CierreResumidoCodigosTableAdapter();
            Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();

            Reportes.WebClientes.RptCierreResumidoCodigos rpt = new Reportes.WebClientes.RptCierreResumidoCodigos();


            try
            {
                //adCierre.Fill(data.CierreResumidoCodigos, idEmpresa, idSede, idTercero, servicio idSubBodega, IdCamaraIni, IdCamaraFin,fecha);
                adCierre.Fill(data.CierreResumidoCodigos, idEmpresa, idSede, idTercero, idSubBodega, servicio, fecha, IdCamaraIni, IdCamaraFin);

                adTercero.Fill(data.Tercero, idEmpresa, idTercero);


                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaCierre", fecha);
                rpt.SetParameterValue("IdServicio", servicio);


                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptCierreDetalladoXProducto CierreDetalladoXProducto(int idEmpresa, int idSede, int idTercero, int servicio, DateTime fecha, int idSubBodega, int IdCamaraIni, int IdCamaraFin)
        {
            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();
            Reportes.Dataset.DataSetWebClientesTableAdapters.CierreDetalladoProductoTableAdapter adInv = new Reportes.Dataset.DataSetWebClientesTableAdapters.CierreDetalladoProductoTableAdapter();
            Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();

            Reportes.WebClientes.RptCierreDetalladoXProducto rpt = new Reportes.WebClientes.RptCierreDetalladoXProducto();


            try
            {
                adInv.Fill(data.CierreDetalladoProducto, idEmpresa, idSede, idTercero, servicio, idSubBodega, IdCamaraIni, IdCamaraFin, fecha);
                adTercero.Fill(data.Tercero, idEmpresa, idTercero);



                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaCierre", fecha);
                rpt.SetParameterValue("IdServicio", servicio);


                return rpt;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                data.Dispose();
                //rpt.Dispose();
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptProdCongelacionVenciminento fechasVencimiento(int idEmpresa, int idSede, int servicio, int idSubBodega, int tercero, int IdCamaraIni, int IdCamaraFin)
        {

            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();
            Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter adInv = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter();
            Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();
            DateTime Fecha = DateTime.Now;

            Reportes.WebClientes.RptProdCongelacionVenciminento rpt = new Reportes.WebClientes.RptProdCongelacionVenciminento();

            try
            {
                adInv.FillByVencimiento(data.InventarioDetalladoProducto, idEmpresa, idSede, servicio, 1, idSubBodega, tercero, IdCamaraIni, IdCamaraFin);
                adTercero.Fill(data.Tercero, idEmpresa, tercero);




                rpt.SetDataSource(data);
                rpt.SetParameterValue("Fecha", Fecha.Date);
                rpt.SetParameterValue("IdServicio", servicio);
            }
            catch (Exception)
            {

                //throw;
            }

            return rpt;
        }

        public static Reportes.WebClientes.RptInventarioClientesXTuneles Tunel(int idEmpresa, int idSede, int servicio, int idNivelIni, int idNivelFin)
        {

            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();
            Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter adInv = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter();
            Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();

            Reportes.WebClientes.RptInventarioClientesXTuneles rpt = new Reportes.WebClientes.RptInventarioClientesXTuneles();
            try
            {

                //int IdEmpresa = Convert.ToInt32(textBoxEmpresa.Text);
                //int IdSede = Convert.ToInt32(textBoxSede.Text);
                //int Servicio = Convert.ToInt32(textBoxServ.Text);
                //int IdEstado = Convert.ToInt32(textBoxEstado.Text);
                //int IdSubBodega = Convert.ToInt32(textBoxSubBodega.Text);

                adInv.FillByTunel(data.InventarioDetalladoProducto, idEmpresa, idSede, servicio, idNivelIni, idNivelFin);
                adTercero.FillByInventarioXTunel(data.Tercero, idEmpresa, idSede, servicio);


                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", servicio);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.WebClientes.RptMovimientosTercero MovimientosTercero(int idEmpresa, int idSede, int servicio,
        int idTercero, int idSubBodega, int dia1, int mes1, int year1, int dia2, int mes2, int year2)
        {
            Reportes.WebClientes.RptMovimientosTercero rpt = new WebClientes.RptMovimientosTercero();


            try
            {
                Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();
                Reportes.Dataset.DataSetWebClientesTableAdapters.MovimientosTableAdapter adMovientos = new Reportes.Dataset.DataSetWebClientesTableAdapters.MovimientosTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();

                DateTime fecha1 = new DateTime(year1, mes1, dia1);
                DateTime fecha2 = new DateTime(year2, mes2, dia2);

                adMovientos.Fill(data.Movimientos, idEmpresa, idTercero, idSede, fecha1, fecha2, idSubBodega, servicio);
                adTercero.FillByTercerosEmpresa(data.Tercero, idEmpresa);


                //rpt = new Reportes.WebClientes.RptMovimientosTercero();
                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", servicio);

                rpt.SetParameterValue("Desde", fecha1.Date);
                rpt.SetParameterValue("Hasta", fecha2.Date);

                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }

        }

        //COPIA INFORME

        public static Reportes.WebClientes.RptMovimientosTerceroCopia MovimientosTerceroCopia(int idEmpresa, int idSede, int servicio,
        int idTercero, int idSubBodega, int dia1, int mes1, int year1, int dia2, int mes2, int year2)
        {
            Reportes.WebClientes.RptMovimientosTerceroCopia rpt = new WebClientes.RptMovimientosTerceroCopia();


            try
            {
                Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();
                Reportes.Dataset.DataSetWebClientesTableAdapters.MovimientosTableAdapter adMovientos = new Reportes.Dataset.DataSetWebClientesTableAdapters.MovimientosTableAdapter();
                Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();

                DateTime fecha1 = new DateTime(year1, mes1, dia1);
                DateTime fecha2 = new DateTime(year2, mes2, dia2);

                adMovientos.Fill(data.Movimientos, idEmpresa, idTercero, idSede, fecha1, fecha2, idSubBodega, servicio);
                adTercero.FillByTercerosEmpresa(data.Tercero, idEmpresa);


                //rpt = new Reportes.WebClientes.RptMovimientosTercero();
                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", servicio);

                rpt.SetParameterValue("Desde", fecha1.Date);
                rpt.SetParameterValue("Hasta", fecha2.Date);

                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }

        }

        //FIN COPIA INFORME     
        public static Reportes.Iglusoft.RptPosicionesDiariasConserva PosicionesDiariasConserva(int idEmpresa, int idSede, int idServicio, int idTercero, int idSubBodega, DateTime f1, DateTime f2, int camara)
        {
            Reportes.Dataset.DataSetPosiciones data = new Reportes.Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesDiaADiaTableAdapter AdposiDiaDia = new Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesDiaADiaTableAdapter();

            Reportes.Iglusoft.RptPosicionesDiariasConserva rpt = new Reportes.Iglusoft.RptPosicionesDiariasConserva();
            try
            {
                AdposiDiaDia.FillByTotalPosicionesFacturacion1(data.TotalPosicionesDiaADia, idEmpresa, idSede, idServicio, idSubBodega, idTercero, f1, f2, camara);

                //ReportDocument rpt = new ReportDocument();
                rpt = new Reportes.Iglusoft.RptPosicionesDiariasConserva();
                rpt.SetDataSource(data);

                return rpt;
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();

            }
            catch (Exception)
            {

                return rpt;
            }
        }

        public static Reportes.Dataset.DataSetPosiciones.TotalPosicionesDiaADiaDataTable PosicionesDiariasConservaNuevo(int idEmpresa, int idSede, int idServicio, int idTercero, int idSubBodega, DateTime f1, DateTime f2, int camara)
        {
            Reportes.Dataset.DataSetPosiciones.TotalPosicionesDiaADiaDataTable data = new Reportes.Dataset.DataSetPosiciones.TotalPosicionesDiaADiaDataTable();
            Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesDiaADiaTableAdapter AdposiDiaDia = new Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesDiaADiaTableAdapter();

            try
            {
                //AdposiDiaDia.FillTotalPosicionesFacturacion(data.TotalPosicionesDiaADia, idEmpresa, idSede, idServicio, idSubBodega, idTercero, f1, f2, camara);
                data = AdposiDiaDia.GetDataTotalPosicionesFacturacion(idEmpresa, idSede, idServicio, idSubBodega, idTercero, f1, f2, camara);
              
                return data;

            }
            catch (Exception)
            {

                return data;
            }
        }

        public static Reportes.Iglusoft.TotalPosiciones TotalPosiciones(int idEmpresa, int idSede, int idServicio, int IdCamaraIni, int IdCamaraFin)
        {
            Reportes.Dataset.DataSetPosiciones data = new Reportes.Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesTableAdapter AdToTpos = new Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesTableAdapter();

            Reportes.Iglusoft.TotalPosiciones rpt = new Reportes.Iglusoft.TotalPosiciones();

            try
            {

                AdToTpos.Fill(data.TotalPosiciones, idEmpresa, idSede, idServicio, IdCamaraIni, IdCamaraFin);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", idServicio);
                return rpt;
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();


            }
            catch (Exception)
            {

                return rpt;
            }
        }

        public static Reportes.Iglusoft.RptPosicionesDiariasConserva TotalPosicionesDias(int idEmpresa, int idSede, int idServicio, int idTercero, int idSubBodega, DateTime f1, DateTime f2, int camara)
        {
            Reportes.Dataset.DataSetPosiciones data = new Reportes.Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesDiaADiaTableAdapter AdposiDiaDia = new Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesDiaADiaTableAdapter();

            Reportes.Iglusoft.RptPosicionesDiariasConserva rpt = new Reportes.Iglusoft.RptPosicionesDiariasConserva();

            try
            {

                AdposiDiaDia.FillTotalPosicionesFacturacion(data.TotalPosicionesDiaADia, idEmpresa, idSede, idServicio, idSubBodega, idTercero, f1, f2, camara);


                rpt.SetDataSource(data);


                return rpt;





            }
            catch (Exception)
            {

                return rpt;
            }
        }

        //Alistamientos Activos
        public static Reportes.Alistamiento.RptAlistActivos AlistamientosActivos(int idEmpresa, int idSede, int idTercero)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientosActivosTableAdapter alistActivos = new Dataset.DataSetAlistamientoTableAdapters.AlistamientosActivosTableAdapter();

            Reportes.Alistamiento.RptAlistActivos rptActivos = new Alistamiento.RptAlistActivos();

            try
            {
                alistActivos.Fill(data.AlistamientosActivos, idEmpresa, idSede, idTercero);
                rptActivos.SetDataSource(data);
                return rptActivos;
            }
            catch (Exception)
            {
                throw;
            }

        }

        //Fin Alistamientos Activos

        //Alistamientos Activos Excel
        public static Reportes.Alistamiento.RptAlistActivosExcel AlistamientosActivosExcel(int idEmpresa, int idSede, int idTercero)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientosActivosTableAdapter alistActivos = new Dataset.DataSetAlistamientoTableAdapters.AlistamientosActivosTableAdapter();

            Reportes.Alistamiento.RptAlistActivosExcel rptActivos = new Alistamiento.RptAlistActivosExcel();

            try
            {
                alistActivos.Fill(data.AlistamientosActivos, idEmpresa, idSede, idTercero);
                rptActivos.SetDataSource(data);
                return rptActivos;
            }
            catch (Exception)
            {
                throw;
            }

        }

        //Fin Alistamientos Activos Excel

        // Alistamiento ordenado por posicion y pasillo para PiKing
        public static Reportes.Alistamiento.RptAlistamientoPosicion AlistamientoPosicion(int idEmpresa, int idSede, int idServicio, int noDocumento)
        {
            //Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            //Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter adAlistInfo = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter();
            //Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter adAlistDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter();




            //Reportes.Alistamiento.RptAlistamientoPosicion rpt = new Reportes.Alistamiento.RptAlistamientoPosicion();
            //try
            //{
            ////    adAlistInfo.FillByIdAlistamiento(data.AListamientoInfo, noDocumento, idEmpresa, idSede, idServicio);
            //    adAlistDet.FillByIdAlistamiento(data.AlistamientoDetalleInfo, noDocumento, idEmpresa, idSede, idServicio);

            //    //CONVERTIR EL NODOCUMENTO A EAN
            //    string eanNoDocumento = BasculasLibrary.Etiqueta.Ean128(noDocumento.ToString());

            //    rpt.SetDataSource(data);
            //    rpt.SetParameterValue("CodBarras", eanNoDocumento);
            //    //FormInformeMostrar f = new FormInformeMostrar(rpt);
            //    //f.ShowDialog();
            //    return rpt;
            //}
            //catch (Exception ex)
            //{
            //    return rpt;
            //}
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.consultaAlistamientoTipoAplicacionTableAdapter adAlistamiento = new Dataset.DataSetAlistamientoTableAdapters.consultaAlistamientoTipoAplicacionTableAdapter();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter adAlistDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter();


            Reportes.Alistamiento.RptAlistamientoPosicion rpt = new Reportes.Alistamiento.RptAlistamientoPosicion();
            try
            {
                adAlistamiento.Fill(data.consultaAlistamientoTipoAplicacion, idEmpresa, idSede, noDocumento, idServicio, 1);
                adAlistDet.FillByIdAlistamiento(data.AlistamientoDetalleInfo, noDocumento, idEmpresa, idSede, idServicio);

                //CONVERTIR EL NODOCUMENTO A EAN
                string eanNoDocumento = BasculasLibrary.Etiqueta.Ean128(noDocumento.ToString());

                rpt.SetDataSource(data);
                rpt.SetParameterValue("CodBarras", eanNoDocumento);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception ex)
            {

                return rpt;
            }

        }

        public static Reportes.Alistamiento.RptAlistamientoPosicion AlistamientoPosicionTipoAplicacion(int idEmpresa, int idSede, int idServicio, int noDocumento, int AplicacionAlistamiento)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.consultaAlistamientoTipoAplicacionTableAdapter adAlistamiento = new Dataset.DataSetAlistamientoTableAdapters.consultaAlistamientoTipoAplicacionTableAdapter();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter adAlistDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter();


            Reportes.Alistamiento.RptAlistamientoPosicion rpt = new Reportes.Alistamiento.RptAlistamientoPosicion();
            try
            {
                adAlistamiento.Fill(data.consultaAlistamientoTipoAplicacion, idEmpresa, idSede, noDocumento, idServicio, AplicacionAlistamiento);
                adAlistDet.FillByIdAlistamiento(data.AlistamientoDetalleInfo, noDocumento, idEmpresa, idSede, idServicio);

                //CONVERTIR EL NODOCUMENTO A EAN
                string eanNoDocumento = BasculasLibrary.Etiqueta.Ean128(noDocumento.ToString());

                rpt.SetDataSource(data);
                rpt.SetParameterValue("CodBarras", eanNoDocumento);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception ex)
            {
                
                return rpt;
            }
        }

        //INFORME ALISTAMIENTO VERIFICADO
        public static Reportes.Alistamiento.RptVerificacionAlistamiento AlistamientoVerificado(int idEmpresa, int idSede, int idServicio, int noDocumento, int AplicacionAlistamiento)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.consultaAlistamientoTipoAplicacionTableAdapter adAlistamiento = new Dataset.DataSetAlistamientoTableAdapters.consultaAlistamientoTipoAplicacionTableAdapter();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.verificacionAlistamientoDetalleTableAdapter adAlistDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.verificacionAlistamientoDetalleTableAdapter();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.VerificacionAlistamientoTableAdapter alistEncabezado = new Dataset.DataSetAlistamientoTableAdapters.VerificacionAlistamientoTableAdapter();

            Reportes.Alistamiento.RptVerificacionAlistamiento rpt = new Reportes.Alistamiento.RptVerificacionAlistamiento();
            try
            {
                adAlistamiento.Fill(data.consultaAlistamientoTipoAplicacion, idEmpresa, idSede, noDocumento, idServicio, AplicacionAlistamiento);
                alistEncabezado.Fill(data.VerificacionAlistamiento, idEmpresa, idSede, idServicio, noDocumento);
                adAlistDet.Fill(data.VerificacionAlistamientoDetalle, noDocumento, idEmpresa, idSede, idServicio);



                //CONVERTIR EL NODOCUMENTO A EAN
                string eanNoDocumento = BasculasLibrary.Etiqueta.Ean128(noDocumento.ToString());

                rpt.SetDataSource(data);
                rpt.SetParameterValue("CodBarras", eanNoDocumento);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        // Alistamiento agrupado por producto y ordenado por posicion y pasillo
        public static Reportes.Alistamiento.RptAlistamientoProducto AlistamientoProducto(int idEmpresa, int idSede, int idServicio, int noDocumento)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter adAlistInfo = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter adAlistDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter();

            Reportes.Alistamiento.RptAlistamientoProducto rpt = new Reportes.Alistamiento.RptAlistamientoProducto();
            try
            {
                adAlistInfo.FillByIdAlistamiento(data.AListamientoInfo, noDocumento, idEmpresa, idSede, idServicio);
                adAlistDet.FillByIdAlistamiento(data.AlistamientoDetalleInfo, noDocumento, idEmpresa, idSede, idServicio);

                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return rpt;
            }
        }

        // Alistamiento agrupado por producto y ordenado por posicion y pasillo consolidado
        public static Reportes.Alistamiento.RptAlistamientoProductoConsol AlistamientoProductoConsolidado(int idEmpresa, int idSede, int idServicio, int noDocumento)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter adAlistInfo = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter adAlistDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter();

            Reportes.Alistamiento.RptAlistamientoProductoConsol rpt = new Reportes.Alistamiento.RptAlistamientoProductoConsol();
            try
            {
                adAlistInfo.FillByIdAlistamiento(data.AListamientoInfo, noDocumento, idEmpresa, idSede, idServicio);
                adAlistDet.FillByIdAlistamiento(data.AlistamientoDetalleInfo, noDocumento, idEmpresa, idSede, idServicio);

                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        // Alistamiento agrupado por embalaje y ordenado por posicion y pasillo consolidado
        public static Reportes.Alistamiento.RptAlistamientoEmbalaje AlistamientoEmbalaje(int idEmpresa, int idSede, int idServicio, int noDocumento)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter adAlistInfo = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter adAlistDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleInfoTableAdapter();

            Reportes.Alistamiento.RptAlistamientoEmbalaje rpt = new Reportes.Alistamiento.RptAlistamientoEmbalaje();
            try
            {
                adAlistInfo.FillByIdAlistamiento(data.AListamientoInfo, noDocumento, idEmpresa, idSede, idServicio);
                adAlistDet.FillByIdAlistamiento(data.AlistamientoDetalleInfo, noDocumento, idEmpresa, idSede, idServicio);

                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.Alistamiento.RptAlistamientoDestino2 AlistamientoDestino(int idEmpresa, int idSede, int idServicio, int noDocumento)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter adAlistInfo = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoInfoTableAdapter();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDestino2TableAdapter adAlistDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDestino2TableAdapter();

            //Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleTableAdapter adAlistamientoDet = new Reportes.Dataset.DataSetAlistamientoTableAdapters.AlistamientoDetalleTableAdapter();

            Reportes.Alistamiento.RptAlistamientoDestino2 rpt = new Reportes.Alistamiento.RptAlistamientoDestino2();
            try
            {


                adAlistInfo.FillByIdAlistamiento(data.AListamientoInfo, noDocumento, idEmpresa, idSede, idServicio);
                adAlistDet.FillDestinos(data.AlistamientoDestino2, idSede, idEmpresa, noDocumento, idServicio);


                ////ReportDocument rpt = new ReportDocument();
                //rpt = new Reportes.Alistamiento.RptAlistamientoMercancia();

                rpt.SetDataSource(data);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        //clase para imprimir nota credito
        public static Reportes.Notas.RptNotas NotasCredito(int IdEmpresa, int IdSede, int IdNota)
        {
            Reportes.Dataset.DataSetNotasCredito data = new Reportes.Dataset.DataSetNotasCredito();
            Reportes.Dataset.DataSetNotasCreditoTableAdapters.NotasTableAdapter encabezado = new Reportes.Dataset.DataSetNotasCreditoTableAdapters.NotasTableAdapter();
            Reportes.Dataset.DataSetNotasCreditoTableAdapters.NotasDetalleTableAdapter detalle = new Reportes.Dataset.DataSetNotasCreditoTableAdapters.NotasDetalleTableAdapter();

            //creamos un objeto del reporte
            Reportes.Notas.RptNotas rpt = new Reportes.Notas.RptNotas();
            try
            {
                encabezado.FillByEncabezadoNota(data.Notas, IdNota, IdEmpresa, IdSede);
                detalle.FillByDetalleNota(data.NotasDetalle, IdEmpresa, IdSede, IdNota);


                ////ReportDocument rpt = new ReportDocument();
                //rpt = new Reportes.Alistamiento.RptAlistamientoMercancia();

                rpt.SetDataSource(data);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        //clase para imprimir resumen de facturacion
        public static Reportes.Factura.RptResumenFacturacion ResumenFacturacion(int Tipo, int IdSubBodega, int IdEmpresa, int IdSede, int IdTercero, string FechaInicial, string FechaFinal)
        {
            Reportes.Dataset.DataSetFacturacion data = new Reportes.Dataset.DataSetFacturacion();
            Reportes.Dataset.DataSetFacturacionTableAdapters.ResumenFacturacionTableAdapter encabezado = new Reportes.Dataset.DataSetFacturacionTableAdapters.ResumenFacturacionTableAdapter();

           

            string[] fecIni = FechaInicial.Split('-');
            string[] FecFinal = FechaFinal.Split('-');

            DateTime f1 = new DateTime(Convert.ToInt32(fecIni[0]), Convert.ToInt32(fecIni[1]), Convert.ToInt32(fecIni[2]));
            DateTime f2 = new DateTime(Convert.ToInt32(FecFinal[0]), Convert.ToInt32(FecFinal[1]), Convert.ToInt32(FecFinal[2]), 12, 59, 59);


            //creamos un objeto del reporte
            Reportes.Factura.RptResumenFacturacion rpt = new Reportes.Factura.RptResumenFacturacion();
            try
            {
                encabezado.FillResumenFacturacion(data.ResumenFacturacion, Tipo, IdEmpresa, IdSede, IdTercero, f1, f2);


                rpt = new Reportes.Factura.RptResumenFacturacion();
                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaIni", FechaInicial);
                rpt.SetParameterValue("FechaFin", FechaFinal);

                //rpt.SetDataSource(data);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        //clase para imprimir tarifas asignadas a un cliente
        public static Reportes.Tarifas.RptTarifas TarifasAsignadas(int IdEmpresa, int IdSede, int IdCamara, int IdTercero, string RazonSocial, string Sede, string Empresa)
        {
            Reportes.Dataset.DataSetTarifas data = new Reportes.Dataset.DataSetTarifas();
            Reportes.Dataset.DataSetTarifasTableAdapters.ConsultarTarifasTableAdapter detalle = new Reportes.Dataset.DataSetTarifasTableAdapters.ConsultarTarifasTableAdapter();


            //creamos un objeto del reporte
            Reportes.Tarifas.RptTarifas rpt = new Reportes.Tarifas.RptTarifas();
            try
            {
                detalle.FillConsultaTarifas(data.ConsultarTarifas, IdEmpresa, IdSede, IdCamara, IdTercero);


                rpt = new Reportes.Tarifas.RptTarifas();
                rpt.SetDataSource(data);
                rpt.SetParameterValue("Cliente", RazonSocial);
                rpt.SetParameterValue("Empresa", Empresa);
                rpt.SetParameterValue("NombreSede", Sede);
                rpt.SetParameterValue("Camara", IdCamara);


                //rpt.SetDataSource(data);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        //clase para imprimir tarifas asignadas a todos los clientes
        public static Reportes.Tarifas.RptTarifasTodos TarifasTodos(int IdEmpresa, int IdSede, int IdCamara, string NombreSede, string Empresa, int IdServicio, string Servicio)
        {
            Reportes.Dataset.DataSetTarifas data = new Reportes.Dataset.DataSetTarifas();
            Reportes.Dataset.DataSetTarifasTableAdapters.TarifasTodosTableAdapter detalle = new Reportes.Dataset.DataSetTarifasTableAdapters.TarifasTodosTableAdapter();


            //creamos un objeto del reporte
            Reportes.Tarifas.RptTarifasTodos rpt = new Reportes.Tarifas.RptTarifasTodos();
            try
            {
                detalle.FillTarifasTodos(data.TarifasTodos, IdEmpresa, IdSede, IdCamara, IdServicio);


                rpt = new Reportes.Tarifas.RptTarifasTodos();
                rpt.SetDataSource(data);

                rpt.SetParameterValue("Empresa", Empresa);
                rpt.SetParameterValue("NombreSede", NombreSede);
                rpt.SetParameterValue("Camara", IdCamara);
                rpt.SetParameterValue("Servicio", Servicio);


                //rpt.SetDataSource(data);
                //FormInformeMostrar f = new FormInformeMostrar(rpt);
                //f.ShowDialog();
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.Iglusoft.RptPosicionesDifParcial2 reporteDiferenciaPosiciones2(int IdEmpresa, int IdSede, int IdServicio, int IdTercero, DateTime FechaInicial,DateTime FechaFinal)
        {
            Reportes.Iglusoft.RptPosicionesDifParcial2 rptDiferenciaPosiciones = new Iglusoft.RptPosicionesDifParcial2();
            Reportes.Dataset.DataSetPosiciones data = new Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.CierreDiarioPosiTableAdapter cierreDiarioDiferencia = new Dataset.DataSetPosicionesTableAdapters.CierreDiarioPosiTableAdapter();
            string Mensaje;
            try
            {
                //cierreDiarioDiferencia.Fill(data.CierreDiario, FechaInicial, FechaFinal, IdSede, IdEmpresa, IdTercero, IdServicio);                
                cierreDiarioDiferencia.Fill(data.CierreDiarioPosi,IdTercero,IdSede,IdEmpresa,IdServicio,FechaInicial);                
                rptDiferenciaPosiciones = new Reportes.Iglusoft.RptPosicionesDifParcial2();
                rptDiferenciaPosiciones.SetDataSource(data);
                return rptDiferenciaPosiciones;
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                throw;
            }
        }

        public static Reportes.Iglusoft.RptSoporteJeronimo reporteSoporteJero(int IdEmpresa, int IdSede, int IdServicio, int IdTercero, DateTime FechaInicial, DateTime FechaFinal)
        {
            Reportes.Iglusoft.RptSoporteJeronimo rpt = new Iglusoft.RptSoporteJeronimo();
            Reportes.Dataset.DataSetSoportesFact data = new Dataset.DataSetSoportesFact();

            Reportes.Dataset.DataSetSoportesFactTableAdapters.SoporteJeronimoTableAdapter ad = new Reportes.Dataset.DataSetSoportesFactTableAdapters.SoporteJeronimoTableAdapter();

            string Mensaje;
            try
            {
                //cierreDiarioDiferencia.Fill(data.CierreDiario, FechaInicial, FechaFinal, IdSede, IdEmpresa, IdTercero, IdServicio);                
                ad.Fill(data.SoporteJeronimo, IdTercero, IdEmpresa,IdSede, IdServicio, FechaInicial,FechaFinal);

                rpt = new Reportes.Iglusoft.RptSoporteJeronimo();
                rpt.SetDataSource(data);
                return rpt;
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                throw;
            }
        }

        public static Reportes.Iglusoft.RptPosicionesDiferenciaParcial reporteDiferenciaPosiciones(int IdEmpresa, int IdSede, int IdServicio, int IdTercero, DateTime FechaInicial, DateTime FechaFinal)
        {
            Reportes.Iglusoft.RptPosicionesDiferenciaParcial rptDiferenciaPosiciones = new Iglusoft.RptPosicionesDiferenciaParcial();
            Reportes.Dataset.DataSetPosiciones data = new Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.CierreDiarioTableAdapter cierreDiarioDiferencia = new Dataset.DataSetPosicionesTableAdapters.CierreDiarioTableAdapter();
            string Mensaje;
            try
            {
                //cierreDiarioDiferencia.Fill(data.CierreDiario, FechaInicial, FechaFinal, IdSede, IdEmpresa, IdTercero, IdServicio);                
                cierreDiarioDiferencia.Fill(data.CierreDiario, IdTercero, IdSede, IdEmpresa, IdServicio, FechaInicial, FechaFinal);
                rptDiferenciaPosiciones = new Reportes.Iglusoft.RptPosicionesDiferenciaParcial();
                rptDiferenciaPosiciones.SetDataSource(data);
                return rptDiferenciaPosiciones;
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                throw;
            }
        }

        public static Reportes.Iglusoft.RptSoporteCongela SoporteCongela(int idEmpresa, int idSede, int idServicio, int idTercero, int idSubBodega, DateTime f1, DateTime f2, int tipo)
        {
            Reportes.Iglusoft.RptSoporteCongela rpt = new Reportes.Iglusoft.RptSoporteCongela();

            Reportes.Dataset.DataSetPosiciones data = new Reportes.Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.CongelacionSoporteTableAdapter AdCongSop = new Reportes.Dataset.DataSetPosicionesTableAdapters.CongelacionSoporteTableAdapter();

            try
            {

                AdCongSop.FillCongelacionSoporte(data.CongelacionSoporte, idEmpresa, idSede, idTercero, idServicio, f1, f2, tipo, idSubBodega);


                rpt.SetDataSource(data);


                return rpt;
            }
            catch (Exception)
            {

                return rpt;
            }

        }

        public static Reportes.Iglusoft.RptSoporteEmpaque SoporteEmpaque(int idEmpresa, int idSede, int idServicio, int idTercero, int idSubBodega, DateTime f1, DateTime f2)
        {
            Reportes.Iglusoft.RptSoporteEmpaque rpt = new Reportes.Iglusoft.RptSoporteEmpaque();

            Reportes.Dataset.DataSetPosiciones data = new Reportes.Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.RepSoporteEmpaqueTableAdapter AdSEmpaque = new Reportes.Dataset.DataSetPosicionesTableAdapters.RepSoporteEmpaqueTableAdapter();

            try
            {

                AdSEmpaque.FillSoporteEmpaque(data.RepSoporteEmpaque, idEmpresa, idSede, idTercero, idServicio, f1, f2, idSubBodega);


                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaIni", f1);
                rpt.SetParameterValue("FechaFin", f2);


                return rpt;
            }
            catch (Exception)
            {

                return rpt;
            }

        }

        public static Reportes.Iglusoft.RptSoporteMovimientos SoporteMovimientos(int idEmpresa, int idSede, int idServicio, int idTercero, int idSubBodega, DateTime f1, DateTime f2)
        {
            Reportes.Iglusoft.RptSoporteMovimientos rpt = new Reportes.Iglusoft.RptSoporteMovimientos();

            Reportes.Dataset.DataSetPosiciones data = new Reportes.Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.SoporteMovimientosTableAdapter AdMovSopor = new Reportes.Dataset.DataSetPosicionesTableAdapters.SoporteMovimientosTableAdapter();
            try
            {

                AdMovSopor.FillSoporteMovimientos(data.SoporteMovimientos, idEmpresa, idSede, idServicio,  idTercero, idSubBodega, f1, f2);

                rpt.SetDataSource(data);


                return rpt;
            }
            catch (Exception)
            {

                return rpt;
            }

        }

        public static Reportes.Iglusoft.RptSeleccionEmpaque EmpaqueDiario(int idEmpresa, int idSede, int idTercero, int idSubBodega, int idServicio, DateTime f1, DateTime f2)
        {

            Reportes.Dataset.DataSetRepIglusoft data = new Reportes.Dataset.DataSetRepIglusoft();
            Reportes.Dataset.DataSetRepIglusoftTableAdapters.SeleccionEmpaqueTableAdapter adInv = new Reportes.Dataset.DataSetRepIglusoftTableAdapters.SeleccionEmpaqueTableAdapter();

            Reportes.Iglusoft.RptSeleccionEmpaque rpt = new Reportes.Iglusoft.RptSeleccionEmpaque();

            try
            {

                adInv.Fill(data.SeleccionEmpaque, idEmpresa, idSede, idTercero, idServicio, f1, f2);


                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaInicial", f1);
                rpt.SetParameterValue("FechaFinal", f2);

                return rpt;
            }
            catch (Exception)
            {

                return rpt;
            }

        }

        public static Reportes.Iglusoft.RptTotalPosicionesUnDia TotPosUnDia(DateTime f1, int IdEmpresa, int IdSede, int IdServicio, int IdCamaraIni, int IdCamaraFin)
        {

            Reportes.Dataset.DataSetPosiciones data = new Reportes.Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesUnDiaTableAdapter TotPUnDia = new Reportes.Dataset.DataSetPosicionesTableAdapters.TotalPosicionesUnDiaTableAdapter();

            Reportes.Iglusoft.RptTotalPosicionesUnDia rpt = new Reportes.Iglusoft.RptTotalPosicionesUnDia();

            try
            {

                TotPUnDia.FillPosicionesCierre(data.TotalPosicionesUnDia, f1, IdEmpresa, IdSede, IdServicio, IdCamaraIni, IdCamaraFin);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("Fecha", f1);
                rpt.SetParameterValue("IdServicio", IdServicio);

                return rpt;
            }
            catch (Exception)
            {

                return rpt;
            }


        }

        public static Reportes.Iglusoft.RptPosicionesLibres PosLibres(int IdEmpresa, int IdSede, int IdServicio, int IdCamaraIni, int IdCamaraFin)
        {

            Reportes.Dataset.DataSetRepIglusoft data = new Reportes.Dataset.DataSetRepIglusoft();
            Reportes.Dataset.DataSetRepIglusoftTableAdapters.PosicionesTableAdapter PosLibres = new Reportes.Dataset.DataSetRepIglusoftTableAdapters.PosicionesTableAdapter();

            Reportes.Iglusoft.RptPosicionesLibres rpt = new Reportes.Iglusoft.RptPosicionesLibres();

            try
            {
                PosLibres.FillPosLibres(data.Posiciones, IdEmpresa, IdSede, IdServicio, IdCamaraIni, IdCamaraFin);

                rpt.SetDataSource(data);



                return rpt;

            }
            catch (Exception)
            {

                return rpt;
            }


        }

        public static Reportes.Movimientos.MovimientosPosicion MovPosicion(DateTime f1, DateTime f2, int IdEmpresa, int IdSede, int Pasillo, int Nivel, int Posicion, int IdServicio, int IdCamara)
        {
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            Reportes.Dataset.DataSetMovimientosTableAdapters.MovimientosXPosicionTableAdapter MovXpos = new Reportes.Dataset.DataSetMovimientosTableAdapters.MovimientosXPosicionTableAdapter();

            Reportes.Movimientos.MovimientosPosicion rpt = new Reportes.Movimientos.MovimientosPosicion();


            try
            {

                MovXpos.FillMovimientoPosicion(data.MovimientosXPosicion, IdEmpresa, IdSede, Nivel, IdServicio, f1, f2, Pasillo, Posicion, IdCamara);
                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaIni", f1);
                rpt.SetParameterValue("FechaFin", f2);


                return rpt;

            }
            catch (Exception ex)
            {
                string Mensaje;
                Mensaje = ex.Message;
                return rpt;

            }

        }

        public static Reportes.Movimientos.RptMovimientoxProducto MovProducto(string CodEscrito, string lote, string Embalaje, int IdServicio, int IdEmpresa, int IdTercero, int PasilloIni, int NivelIni, int PosicionIni, int PasilloFin, int NivelFin, int PosicionFin, DateTime f1, DateTime f2, Int64 EntradaIni, Int64 EntradaFin, int IdItemIni, int IdItemFin, int IdSede, int IdCamaraini, int IdCamaraFin)
        {
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            Reportes.Dataset.DataSetMovimientosTableAdapters.MovimientoXProductoTableAdapter MovProd = new Reportes.Dataset.DataSetMovimientosTableAdapters.MovimientoXProductoTableAdapter();

            Reportes.Movimientos.RptMovimientoxProducto rpt = new Reportes.Movimientos.RptMovimientoxProducto();

            try
            {

                // MovProd.FillMovProducto(data.MovimientoXProducto,CodEscrito,lote,Embalaje,IdServicio,IdEmpresa,IdTercero,PasilloIni,NivelIni,PosicionIni,PasilloFin,NivelFin,PosicionFin,f1,f2,EntradaIni,EntradaFin,IdItemIni,IdItemFin,IdSede);
                // generado para no errores de web clientes
                //MovProd.FillByMovP1(data.MovimientoXProducto, CodEscrito, lote, Embalaje, IdServicio, IdEmpresa, IdSede, IdTercero, PasilloIni, NivelIni, PosicionIni, PasilloFin, NivelFin, PosicionFin, f1, f2, EntradaIni, EntradaFin, IdItemIni, IdItemFin, IdCamaraini, IdCamaraFin);


                MovProd.FillByMovP(data.MovimientoXProducto, CodEscrito, lote, Embalaje, IdServicio, IdEmpresa, IdSede, IdTercero, PasilloIni, NivelIni, PosicionIni, PasilloFin, NivelFin, PosicionFin, f1, f2, EntradaIni, EntradaFin, IdItemIni, IdItemFin, IdCamaraini, IdCamaraFin);
                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaIni", f1);
                rpt.SetParameterValue("FechaFin", f2);


                return rpt;

            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;

            }

            return rpt;

        }

        public static Reportes.Movimientos.MovimientosPosicion MovPosicionxCliente(DateTime f1, DateTime f2, int IdEmpresa, int IdSede, int Pasillo, int Nivel, int Posicion, int IdServicio, int IdTercero, int IdCamara)
        {
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            Reportes.Dataset.DataSetMovimientosTableAdapters.MovimientosXPosicionTableAdapter MovXpos = new Reportes.Dataset.DataSetMovimientosTableAdapters.MovimientosXPosicionTableAdapter();

            Reportes.Movimientos.MovimientosPosicion rpt = new Reportes.Movimientos.MovimientosPosicion();


            try
            {

                MovXpos.FillByMovPosicionxTer(data.MovimientosXPosicion, IdEmpresa, IdSede, Nivel, IdServicio, f1, f2, Pasillo, Posicion, IdTercero, IdCamara);
                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaIni", f1);
                rpt.SetParameterValue("FechaFin", f2);


                return rpt;

            }
            catch (Exception)
            {
                return rpt;
            }

        }

        public static Reportes.Vehiculos.RptVehiculoXFecha MovVehiculoXFecha(DateTime f1, DateTime f2, int IdSede, int IdEmpresa, int Tareas)
        {

            Reportes.Dataset.DataSetVehiculos data = new Reportes.Dataset.DataSetVehiculos();
            Reportes.Dataset.DataSetVehiculos.VehiculosDataTable dataPrueba = new Reportes.Dataset.DataSetVehiculos.VehiculosDataTable();
            Reportes.Dataset.DataSetVehiculosTableAdapters.VehiculosTableAdapter adVehFec = new Reportes.Dataset.DataSetVehiculosTableAdapters.VehiculosTableAdapter();
            Reportes.Dataset.DataSetVehiculosTableAdapters.EntradasSalidasTareasAsignadasTableAdapter adTarea = new Reportes.Dataset.DataSetVehiculosTableAdapters.EntradasSalidasTareasAsignadasTableAdapter();
            Reportes.Dataset.DataSetVehiculos.EntradasSalidasTareasAsignadasDataTable dataTareas = new Reportes.Dataset.DataSetVehiculos.EntradasSalidasTareasAsignadasDataTable();

            Reportes.Vehiculos.RptVehiculoXFecha rpt = new Reportes.Vehiculos.RptVehiculoXFecha();

            try
            {

                //dataPrueba = adVehFec.GetDataByVehiculosFecha(IdSede, IdEmpresa, f1, f2);
                adVehFec.FillByVehiculosFecha(data.Vehiculos, IdSede, IdEmpresa, f1, f2);

                if (Tareas == 1)
                {

                    foreach (var item in data.Vehiculos)
                    {

                        dataTareas = adTarea.GetData(IdEmpresa, IdSede, item.NoDocumento);
                        foreach (var asignaciones in dataTareas)
                        {
                            item.Tareas = item.Tareas + "<b>" + asignaciones.NombreTarea + ":</b>" + asignaciones.Empleado + "<br>";
                            //item.Tareas = "hola mundo";
                        }


                    }
                }

                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaIni", f1);
                rpt.SetParameterValue("FechaFin", f2);

                return rpt;

            }
            catch (Exception)
            {
                return rpt;
            }


        }

        public static Reportes.Insumos.RptInsumosMovimientos InsumosMov(int IdEmpresa, int IdSede, int IdControl, int IdMovimientoInsumo)
        {

            Reportes.Dataset.DataSetInsumos data = new Reportes.Dataset.DataSetInsumos();
            Reportes.Dataset.DataSetInsumosTableAdapters.InformeInsumosTableAdapter dpInsu = new Reportes.Dataset.DataSetInsumosTableAdapters.InformeInsumosTableAdapter();


            Reportes.Insumos.RptInsumosMovimientos rpt = new Reportes.Insumos.RptInsumosMovimientos();

            try
            {

                dpInsu.FillByInformeInsumo(data.InformeInsumos, IdEmpresa, IdSede, IdControl, IdMovimientoInsumo);
                rpt.SetDataSource(data);

                return rpt;

            }
            catch (Exception ex)
            {

                string mensaje;
                mensaje = ex.Message;
            }


            return rpt;

        }

        public static Reportes.Traslados.MovimientosTraslados MovTraslados(DateTime f1, DateTime f2, int IdEmpresa, int IdSede, int IdServicio, int IdTerceroIni, int IdTerceroFin, int tipoTraslado)
        {

            Reportes.Dataset.DataSetTraslado data = new Reportes.Dataset.DataSetTraslado();
            Reportes.Dataset.DataSetTrasladoTableAdapters.TrasladosTableAdapter AdMovTras = new Reportes.Dataset.DataSetTrasladoTableAdapters.TrasladosTableAdapter();

            Reportes.Traslados.MovimientosTraslados rpt = new Reportes.Traslados.MovimientosTraslados();

            try
            {
                AdMovTras.Fill(data.Traslados, f1, f2, IdEmpresa, IdSede, IdServicio, IdTerceroIni, IdTerceroFin, tipoTraslado);
                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaIni", f1);
                rpt.SetParameterValue("FechaFin", f2);
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        //reporte traslado por documento
        public static Reportes.Traslados.RptNewTraslado GetTrasladosDoc(int IdEmpresa, int IdSede, int IdServicio, int NoDocumento)
        {

            Dataset.DataSetTraslado data = new Dataset.DataSetTraslado();
            Reportes.Traslados.RptNewTraslado rpt = new Traslados.RptNewTraslado();

            try
            {
                //TRASLADOS
                Dataset.DataSetTrasladoTableAdapters.TrasladoInfoTableAdapter adTraslado = new Dataset.DataSetTrasladoTableAdapters.TrasladoInfoTableAdapter();
                Dataset.DataSetTrasladoTableAdapters.TrasladoDetalleInfoTableAdapter adTrasladoDetalleInfo = new Dataset.DataSetTrasladoTableAdapters.TrasladoDetalleInfoTableAdapter();


                adTraslado.Fill(data.TrasladoInfo, NoDocumento, IdEmpresa, IdSede, IdServicio);

                adTrasladoDetalleInfo.Fill(data.TrasladoDetalleInfo, NoDocumento, IdEmpresa, IdSede, IdServicio);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", IdServicio);


                return rpt;

            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;
            }
            return rpt;
            //finally
            //{
            //    data.Dispose();
            //    rpt.Dispose();
            //}
        }

        public static Reportes.Iglusoft.RptSoporteTotalTraslados SoporTralados(DateTime f1, DateTime f2, int IdEmpresa, int IdSede, int IdServicio, int IdTercero, int IdSubBodega)
        {
            Reportes.Dataset.DataSetPosiciones data = new Reportes.Dataset.DataSetPosiciones();
            Reportes.Dataset.DataSetPosicionesTableAdapters.TotalTrasladosTableAdapter adSoporTrasl = new Reportes.Dataset.DataSetPosicionesTableAdapters.TotalTrasladosTableAdapter();

            Reportes.Iglusoft.RptSoporteTotalTraslados rpt = new Reportes.Iglusoft.RptSoporteTotalTraslados();

            try
            {

                adSoporTrasl.Fill(data.TotalTraslados, f1, f2, IdEmpresa, IdSede, IdServicio, IdTercero, IdSubBodega);
                rpt.SetDataSource(data);

                return rpt;

            }
            catch (Exception)
            {

                return rpt;
            }



        }

        public static Reportes.Iglusoft.RptProductosEmpacar ProductoEmpacar(int IdEmpresa, int IdSede, int IdTercero)
        {

            Reportes.Dataset.DataSetRepIglusoft data = new Reportes.Dataset.DataSetRepIglusoft();
            Reportes.Dataset.DataSetRepIglusoftTableAdapters.InfoProductosEmpacarTableAdapter adEmpacar = new Reportes.Dataset.DataSetRepIglusoftTableAdapters.InfoProductosEmpacarTableAdapter();

            Reportes.Iglusoft.RptProductosEmpacar rpt = new Reportes.Iglusoft.RptProductosEmpacar();


            try
            {

                adEmpacar.Fill(data.InfoProductosEmpacar, IdEmpresa, IdSede, IdTercero);
                rpt.SetDataSource(data);

                return rpt;

            }
            catch (Exception)
            {

                return rpt;
            }


        }

        //clase para imprimir entradas salidas vehiculos
        public static Reportes.MonitorControl.RptEntradaSalidaVehiculos EntradasSalidasVehiculos(int IdEmpresa, int IdSede)
        {
            Reportes.Dataset.DataSetMonitorControl data = new Reportes.Dataset.DataSetMonitorControl();
            Reportes.Dataset.DataSetMonitorControlTableAdapters.RegistroVehiculosTableAdapter ad = new Reportes.Dataset.DataSetMonitorControlTableAdapters.RegistroVehiculosTableAdapter();

            //creamos un objeto del reporte
            Reportes.MonitorControl.RptEntradaSalidaVehiculos rpt = new Reportes.MonitorControl.RptEntradaSalidaVehiculos();
            try
            {
                ad.Fill(data.RegistroVehiculos, IdEmpresa, IdSede);


                rpt = new Reportes.MonitorControl.RptEntradaSalidaVehiculos();
                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        //clase para imprimir entradas salidas documentos
        public static Reportes.MonitorControl.RptEntradaSalidaDocumentos EntradasSalidasDocumentos(int IdEmpresa, int IdSede)
        {
            Reportes.Dataset.DataSetMonitorControl data = new Reportes.Dataset.DataSetMonitorControl();
            Reportes.Dataset.DataSetMonitorControlTableAdapters.RegistroVehiculosTableAdapter ad = new Reportes.Dataset.DataSetMonitorControlTableAdapters.RegistroVehiculosTableAdapter();

            //creamos un objeto del reporte
            Reportes.MonitorControl.RptEntradaSalidaDocumentos rpt = new Reportes.MonitorControl.RptEntradaSalidaDocumentos();
            try
            {
                ad.FillByEntradaSalida(data.RegistroVehiculos, IdEmpresa, IdSede);


                rpt = new Reportes.MonitorControl.RptEntradaSalidaDocumentos();
                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        //clase para imprimir entradas salidas sedes
        public static Reportes.MonitorControl.RptEntradaSalidaSede EntradasSalidasSedes(int IdEmpresa)
        {
            Reportes.Dataset.DataSetMonitorControl data = new Reportes.Dataset.DataSetMonitorControl();
            Reportes.Dataset.DataSetMonitorControlTableAdapters.RegistroVehiculosTableAdapter ad = new Reportes.Dataset.DataSetMonitorControlTableAdapters.RegistroVehiculosTableAdapter();

            //creamos un objeto del reporte
            Reportes.MonitorControl.RptEntradaSalidaSede rpt = new Reportes.MonitorControl.RptEntradaSalidaSede();
            try
            {
                ad.FillByIOSede(data.RegistroVehiculos, IdEmpresa);


                rpt = new Reportes.MonitorControl.RptEntradaSalidaSede();
                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        //clase para imprimir movimientos diarios
        public static Reportes.Movimientos.RptMovimientosDiarios MovimientosDiarios(int IdEmpresa, int IdSede, DateTime FechaInicial, DateTime FechaFinal)
        {
            string Sede = "";
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            Reportes.Dataset.DataSetMovimientosTableAdapters.MovimientosDiariosEncTableAdapter add = new Reportes.Dataset.DataSetMovimientosTableAdapters.MovimientosDiariosEncTableAdapter();

            //creamos un objeto del reporte
            Reportes.Movimientos.RptMovimientosDiarios rpt = new Reportes.Movimientos.RptMovimientosDiarios();
            try
            {
                add.GetInfoMovDiarios(data.MovimientosDiariosEnc, IdEmpresa, IdSede, FechaInicial, FechaFinal);


                rpt = new Reportes.Movimientos.RptMovimientosDiarios();
                rpt.SetDataSource(data);
                //parametros

                if (IdSede == 1) Sede = "Giron";
                if (IdSede == 2) Sede = "Cartagena";
                if (IdSede == 3) Sede = "Funza";
                if (IdSede == 4) Sede = "Buga";

                rpt.SetParameterValue("Sede", Sede);
                rpt.SetParameterValue("FechaInicial", FechaInicial);
                rpt.SetParameterValue("FechaFinal", FechaFinal);
                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.Alistamiento.RptAlistamientoInventario AlistamientoInventario(int IdEmpresa, int IdSede, int IdServicio, int IdTercero)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.InventarioAlistamientoTableAdapter InvAlis = new Reportes.Dataset.DataSetAlistamientoTableAdapters.InventarioAlistamientoTableAdapter();

            Reportes.Alistamiento.RptAlistamientoInventario rpt = new Reportes.Alistamiento.RptAlistamientoInventario();
            try
            {
                InvAlis.Fill(data.InventarioAlistamiento, IdEmpresa, IdSede, IdServicio, IdTercero);
                rpt = new Reportes.Alistamiento.RptAlistamientoInventario();
                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", IdServicio);
                return rpt;


            }
            catch (Exception)
            {

                return rpt;
            }
        }

        //clase para imprimir alistamientos vencidos
        public static Reportes.Alistamiento.RptAlistamientoVencido AlistamientosVencidos(int IdEmpresa, int IdSede)
        {
            Reportes.Dataset.DataSetAlistamiento data = new Reportes.Dataset.DataSetAlistamiento();
            Reportes.Dataset.DataSetAlistamientoTableAdapters.DataTableAlistamientoVencidoTableAdapter ad = new Reportes.Dataset.DataSetAlistamientoTableAdapters.DataTableAlistamientoVencidoTableAdapter();

            //creamos un objeto del reporte
            Reportes.Alistamiento.RptAlistamientoVencido rpt = new Reportes.Alistamiento.RptAlistamientoVencido();
            try
            {
                ad.FillByAlistamientosVencidos(data.DataTableAlistamientoVencido, IdEmpresa, IdSede);

                rpt = new Reportes.Alistamiento.RptAlistamientoVencido();
                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.WebClientes.RptInventarioDetalladoXNivel IventarioxNivel(int IdEmpresa, int IdSede, int IdTercero, int IdTerceroFin, int IdServicio, int IdSubBodega, int IdSubBodegaFin, int IdCamaraIni, int IdCamaraFin, string IdProducto, int IdNivelIni, int IdNivelFin)
        {


            Reportes.Dataset.DataSetWebClientes data = new Reportes.Dataset.DataSetWebClientes();
            Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter invXnivel = new Reportes.Dataset.DataSetWebClientesTableAdapters.InventarioDetalladoProductoTableAdapter();
            Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter adTercero = new Reportes.Dataset.DataSetWebClientesTableAdapters.TerceroTableAdapter();




            int paramCliente;

            if (IdTercero != IdTerceroFin)
            {
                paramCliente = 0;

            }
            else
            {
                paramCliente = IdTercero;
            }

            Reportes.WebClientes.RptInventarioDetalladoXNivel rpt = new Reportes.WebClientes.RptInventarioDetalladoXNivel();


            try
            {
                invXnivel.FillByInventarioXNivel(data.InventarioDetalladoProducto, IdEmpresa, IdSede, IdTercero, IdServicio, IdSubBodega, IdCamaraIni, IdCamaraFin, IdTerceroFin, IdProducto, IdSubBodegaFin, IdNivelIni, IdNivelFin);



                rpt = new Reportes.WebClientes.RptInventarioDetalladoXNivel();

                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", IdServicio);
                rpt.SetParameterValue("Pciente", paramCliente);



                return rpt;

            }
            catch (Exception)
            {

                return rpt;
            }


        }

        public static Reportes.WebClientes.RptListadoInternos PlanillaInternos(int IdEmpresa, int IdSede, int IdServicio, int IdCamara, int Pasillo, int Nivel, int Posicion)
        {


            Reportes.Dataset.DataSetWebClientes data = new Dataset.DataSetWebClientes();
            Reportes.Dataset.DataSetWebClientesTableAdapters.ListadoInternosTableAdapter PlanillaInt = new Dataset.DataSetWebClientesTableAdapters.ListadoInternosTableAdapter();
            Reportes.WebClientes.RptListadoInternos rpt = new WebClientes.RptListadoInternos();

            try
            {

                PlanillaInt.Fill(data.ListadoInternos, IdEmpresa, IdSede, IdServicio, IdCamara, Pasillo, Nivel, Posicion);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", IdServicio);



                return rpt;


            }
            catch (Exception)
            {

                return rpt;
            }


        }

        public static Reportes.Iglusoft.RptInventarioRetenidos InvRetenidos(int IdEmpresa, int IdSede, int IdServicio, int IdTerceroIni, int IdTerceroFin, int IdSubBodegaIni, int IdSubBodegaFin)
        {

            Reportes.Iglusoft.RptInventarioRetenidos rpt = new Iglusoft.RptInventarioRetenidos();
            Reportes.Dataset.DataSetRepIglusoft data = new Dataset.DataSetRepIglusoft();
            Reportes.Dataset.DataSetRepIglusoftTableAdapters.InformeRetenidosTableAdapter InvRet = new Dataset.DataSetRepIglusoftTableAdapters.InformeRetenidosTableAdapter();
            try
            {

                InvRet.FillInventarioRetenidos(data.InformeRetenidos, IdEmpresa, IdSede, IdServicio, IdTerceroIni, IdTerceroFin, IdSubBodegaIni, IdSubBodegaFin);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("IdServicio", IdServicio);

                return rpt;
            }

            catch (Exception)
            {
                return rpt;
            }
        }

        //AQUI

        public static Reportes.Iglusoft.RptInformeObservacionesRetenidos InvObservaciones(int IdEmpresa, int IdSede, int IdServicio)
        {
            Reportes.Iglusoft.RptInformeObservacionesRetenidos rpt = new Iglusoft.RptInformeObservacionesRetenidos();
            Reportes.Dataset.DataSetRepIglusoft data = new Dataset.DataSetRepIglusoft();
            Reportes.Dataset.DataSetRepIglusoftTableAdapters.informeObservacionesTableAdapter obs = new Dataset.DataSetRepIglusoftTableAdapters.informeObservacionesTableAdapter();

            try
            {

                obs.Fill(data.informeObservaciones, IdEmpresa, IdSede, IdServicio);

                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {

                return rpt;
            }
        }

        public static Reportes.Iglusoft.RptInformeObservacionesRetenidos InvObservacionesPosicion(int IdEmpresa, int IdSede, int IdServicio, int Pasillo, int Nivel, int Posicion, int Camara)
        {
            Reportes.Iglusoft.RptInformeObservacionesRetenidos rpt = new Iglusoft.RptInformeObservacionesRetenidos();
            Reportes.Dataset.DataSetRepIglusoft data = new Dataset.DataSetRepIglusoft();
            Reportes.Dataset.DataSetRepIglusoftTableAdapters.informeObservacionesTableAdapter obs = new Dataset.DataSetRepIglusoftTableAdapters.informeObservacionesTableAdapter();

            try
            {

                obs.FillBy(data.informeObservaciones, IdEmpresa, IdSede, IdServicio, Pasillo, Nivel, Posicion, Camara);

                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {

                return rpt;
            }
        }

        public static Reportes.Iglusoft.RptInformeObservacionesRetenidos InvObservacionesDocumento(int IdEmpresa, int IdSede, int IdServicio, int Documento)
        {
            Reportes.Iglusoft.RptInformeObservacionesRetenidos rpt = new Iglusoft.RptInformeObservacionesRetenidos();
            Reportes.Dataset.DataSetRepIglusoft data = new Dataset.DataSetRepIglusoft();
            Reportes.Dataset.DataSetRepIglusoftTableAdapters.informeObservacionesTableAdapter obs = new Dataset.DataSetRepIglusoftTableAdapters.informeObservacionesTableAdapter();

            try
            {

                obs.FillBy1(data.informeObservaciones, IdEmpresa, IdSede, IdServicio, Documento);

                rpt.SetDataSource(data);

                return rpt;
            }
            catch (Exception)
            {

                return rpt;
            }
        }

        //FIN

        public static Factura.RptInformeFactAutoManual ReporteFacturacionAutMan(int IdEmpresa, int IdSede, int Ano, int Mes, int IdTipoAutMan)
        {
            Factura.RptInformeFactAutoManual rpt = new Factura.RptInformeFactAutoManual();
            Dataset.DataSetInformeFactura data = new Dataset.DataSetInformeFactura();

            try
            {
                Dataset.DataSetInformeFacturaTableAdapters.ReporteFacturacionAutoManualTableAdapter adRes = new Dataset.DataSetInformeFacturaTableAdapters.ReporteFacturacionAutoManualTableAdapter();
                adRes.FillReporteFacturacionAutoManual(data.ReporteFacturacionAutoManual, IdEmpresa, IdSede, Ano, Mes, 1, IdTipoAutMan);

                rpt.SetDataSource(data);
                return rpt;
            }

            catch (Exception)
            {
                return rpt;
            }
        }

        public static Factura.RptInformeNotasAutoManual ReporteNotasAutMan(int IdEmpresa, int IdSede, int Ano, int Mes, int IdTipoAutMan)
        {
            Factura.RptInformeNotasAutoManual rpt = new Factura.RptInformeNotasAutoManual();
            Dataset.DataSetInformeFactura data = new Dataset.DataSetInformeFactura();

            try
            {
                Dataset.DataSetInformeFacturaTableAdapters.ReporteNotasAutoManualTableAdapter adRes = new Dataset.DataSetInformeFacturaTableAdapters.ReporteNotasAutoManualTableAdapter();
                adRes.FillReporteNotasAutoManual(data.ReporteNotasAutoManual, IdEmpresa, IdSede, Ano, Mes, 2, IdTipoAutMan);

                rpt.SetDataSource(data);
                return rpt;
            }

            catch (Exception)
            {
                return rpt;
            }
        }

        public static Factura.RptInformeClientesActivos ReporteClientesActivos(int IdEmpresa, int IdSede)
        {
            Factura.RptInformeClientesActivos rpt = new Factura.RptInformeClientesActivos();
            Dataset.DataSetInformeFactura data = new Dataset.DataSetInformeFactura();

            try
            {
                Dataset.DataSetInformeFacturaTableAdapters.ReporteClientesActivosTableAdapter adRes = new Dataset.DataSetInformeFacturaTableAdapters.ReporteClientesActivosTableAdapter();
                adRes.FillClientesActivos(data.ReporteClientesActivos, IdEmpresa, IdSede);

                rpt.SetDataSource(data);
                return rpt;
            }

            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.Iglusoft.RptHorariosAdicionales InformeHorariosAdicionales(int IdEmpresa, int IdSede, int IdServicio, int IdTercero, int IdSubBodega, DateTime f1, DateTime f2)
        {

            Iglusoft.RptHorariosAdicionales rpt = new Iglusoft.RptHorariosAdicionales();
            Dataset.DataSetPosiciones data = new Dataset.DataSetPosiciones();

            try
            {
                Dataset.DataSetPosicionesTableAdapters.HorariosAdicionalesTableAdapter adRes = new Dataset.DataSetPosicionesTableAdapters.HorariosAdicionalesTableAdapter();
                adRes.FillHorariosAdicionales(data.HorariosAdicionales, IdEmpresa, IdSede, IdServicio, f1, f2, IdTercero, IdSubBodega);

                rpt.SetDataSource(data);

                return rpt;
            }

            catch (Exception)
            {
                return rpt;
            }
        }

        public static Factura.RptFactura InformeFactura(int IdEmpresa, int IdSede, int NoFactura, int Tipo)
        {
            Factura.RptFactura rpt = new Factura.RptFactura();

            Reportes.Dataset.DataSetFacturacion data = new Reportes.Dataset.DataSetFacturacion();
            Reportes.Dataset.DataSetFacturacionTableAdapters.FacturaEncabezadoTableAdapter adEncabezado = new Reportes.Dataset.DataSetFacturacionTableAdapters.FacturaEncabezadoTableAdapter();
            Reportes.Dataset.DataSetFacturacionTableAdapters.FacturaDetalleTableAdapter adDetalle = new Reportes.Dataset.DataSetFacturacionTableAdapters.FacturaDetalleTableAdapter();
            Reportes.Dataset.DataSetFacturacionTableAdapters.SedeTableAdapter adSede = new Reportes.Dataset.DataSetFacturacionTableAdapters.SedeTableAdapter();

            try
            {
                adEncabezado.Fill(data.FacturaEncabezado, IdEmpresa, IdSede, NoFactura, Tipo);
                adDetalle.Fill(data.FacturaDetalle, IdEmpresa, IdSede, NoFactura, Tipo);
                adSede.Fill(data.Sede, IdEmpresa, IdSede);
                string CifraEnLetras = "SON: " + adEncabezado.CifraEnLetras(Convert.ToDecimal(data.FacturaEncabezado[0].Total));

                rpt.SetDataSource(data);
                rpt.SetParameterValue("ValorTotalEnLetras", CifraEnLetras);

                return rpt;

            }
            catch (Exception)
            {
                return rpt;
                //global::System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        public static Reportes.Iglusoft.RptTotalesCongelacion informeCongelacionDiaria(int IdSede, DateTime f1, DateTime f2, int IdEmpresa, int IdTerceroIni, int IdTerceroFin)
        {
            Reportes.Dataset.DataSetRepIglusoft data = new Reportes.Dataset.DataSetRepIglusoft();
            Reportes.Dataset.DataSetRepIglusoftTableAdapters.CongelacionDiariaTableAdapter adInv = new Reportes.Dataset.DataSetRepIglusoftTableAdapters.CongelacionDiariaTableAdapter();

            Reportes.Iglusoft.RptTotalesCongelacion rpt = new Reportes.Iglusoft.RptTotalesCongelacion();

            try
            {
                adInv.Fill(data.CongelacionDiaria, IdSede, f1, f2, IdEmpresa, IdTerceroIni, IdTerceroFin);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("FechaInicial", f1);
                rpt.SetParameterValue("FechaFinal", f2);

                return rpt;

            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.AlistamientoAutomatico.RptAlistamientoAutomaticoLogs AlistamientoAutomaticoLogs(int IdSede, int IdEmpresa, int IdLog)
        {
            Reportes.Dataset.DataSetAlistamientoAutomatico1 data = new Reportes.Dataset.DataSetAlistamientoAutomatico1();
            Reportes.Dataset.DataSetAlistamientoAutomatico1TableAdapters.AlistamientoAutomaticoLogsTableAdapter ad = new Reportes.Dataset.DataSetAlistamientoAutomatico1TableAdapters.AlistamientoAutomaticoLogsTableAdapter();

            Reportes.AlistamientoAutomatico.RptAlistamientoAutomaticoLogs rpt = new Reportes.AlistamientoAutomatico.RptAlistamientoAutomaticoLogs();

            try
            {
                ad.Fill(data.AlistamientoAutomaticoLogs, IdLog, IdEmpresa, IdSede);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("TituloInfore", "Logs del Alistamiento");

                return rpt;

            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.AlistamientoAutomatico.RptAlistamientoAutomaticoConteo AlistamientoAutomaticoConteo(DateTime fecha1, DateTime fecha2)
        {
            Reportes.Dataset.DataSetAlistamientoAutomatico1 data = new Reportes.Dataset.DataSetAlistamientoAutomatico1();
            Reportes.Dataset.DataSetAlistamientoAutomatico1TableAdapters.AlistamientoAutomaticoTableAdapter ad = new Reportes.Dataset.DataSetAlistamientoAutomatico1TableAdapters.AlistamientoAutomaticoTableAdapter();

            Reportes.AlistamientoAutomatico.RptAlistamientoAutomaticoConteo rpt = new Reportes.AlistamientoAutomatico.RptAlistamientoAutomaticoConteo();

            try
            {
                ad.Fill(data.AlistamientoAutomatico, fecha1, fecha2);

                rpt.SetDataSource(data);
                rpt.SetParameterValue("TituloInforme", "Informe Alistamiento Automatico");

                return rpt;

            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.Movimientos.RptCrossDockingPorUno ReporteCrossDockingPorUno(int IdEmpresa, int IdSede, int NoDocumento)
        {
            Reportes.Dataset.DataSetCrossDocking data = new Reportes.Dataset.DataSetCrossDocking();
            Reportes.Dataset.DataSetCrossDockingTableAdapters.DataTableCrossDockingTableAdapter adCabecero = new Reportes.Dataset.DataSetCrossDockingTableAdapters.DataTableCrossDockingTableAdapter();
            Reportes.Dataset.DataSetCrossDockingTableAdapters.CrossDockingDetalleTableAdapter adDetalle = new Reportes.Dataset.DataSetCrossDockingTableAdapters.CrossDockingDetalleTableAdapter();


            Reportes.Movimientos.RptCrossDockingPorUno rpt = new Reportes.Movimientos.RptCrossDockingPorUno();

            try
            {
                adCabecero.Fill(data.DataTableCrossDocking, NoDocumento, IdEmpresa, IdSede);
                adDetalle.FillBy(data.CrossDockingDetalle, NoDocumento, IdEmpresa, IdSede);
                rpt.SetDataSource(data);
                rpt.SetParameterValue("TituloInforme", "Informe Alistamiento Automatico");

                return rpt;

            }
            catch (Exception)
            {
                return rpt;
            }
        }

        public static Reportes.Factura.RptSoporteCamaraTotal InformeSoporteCamaraTotal(int idEmpresa, int idSede, int idServicio, int idTercero, int idSubBodega, int idCamara, DateTime f1, DateTime f2)
        {
            Reportes.Dataset.DataSetFacturacion data = new Reportes.Dataset.DataSetFacturacion();
            Reportes.Dataset.DataSetFacturacionTableAdapters.CamaraTotalTableAdapter ad = new Reportes.Dataset.DataSetFacturacionTableAdapters.CamaraTotalTableAdapter();
            Reportes.Factura.RptSoporteCamaraTotal rpt = new Reportes.Factura.RptSoporteCamaraTotal();


            ad.Fill(data.CamaraTotal, idEmpresa, idSede, idServicio, idCamara, idTercero, idSubBodega, f1, f2);
            rpt.SetDataSource(data);

            return rpt;
        }

        public static Reportes.Movimientos.RptDocTempNoTerminados InformeDocTempNoTerminados(int idEmpresa, int idSede, int idServicio)
        {
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            Reportes.Dataset.DataSetMovimientosTableAdapters.DocTempTableAdapter ad = new Reportes.Dataset.DataSetMovimientosTableAdapters.DocTempTableAdapter();
            Reportes.Movimientos.RptDocTempNoTerminados rpt = new Reportes.Movimientos.RptDocTempNoTerminados();
            DateTime Date = DateTime.Today;
            DateTime FullDate = DateTime.Now;
            if (FullDate.Hour < 6)
            {
                Date = Date.AddDays(-1);
            }
            Date = Date.AddHours(6);
            ad.FillBy(data.DocTemp, idEmpresa, idSede, idServicio, Date);
            rpt.SetDataSource(data);

            return rpt;
        }

        public static Reportes.Movimientos.RptInfoGuiaCrossDocking MovimientoCrossDockingGuia(int IdEmpresa, int IdSede, int NoDocumento, int Item)
        {
            Reportes.Movimientos.RptInfoGuiaCrossDocking rpt = new Reportes.Movimientos.RptInfoGuiaCrossDocking();
            try
            {



                Reportes.Dataset.DataSetCrossDocking data = new Reportes.Dataset.DataSetCrossDocking();
                Reportes.Dataset.DataSetCrossDockingTableAdapters.DataTableCrossDockingTableAdapter adCrossCabecero = new Reportes.Dataset.DataSetCrossDockingTableAdapters.DataTableCrossDockingTableAdapter();
                Reportes.Dataset.DataSetCrossDockingTableAdapters.CrossDockingDetalleTableAdapter adCross = new Reportes.Dataset.DataSetCrossDockingTableAdapters.CrossDockingDetalleTableAdapter();
                Reportes.Dataset.DataSetCrossDockingTableAdapters.CrossDockingProductosTableAdapter adProd = new Reportes.Dataset.DataSetCrossDockingTableAdapters.CrossDockingProductosTableAdapter();
                Reportes.Dataset.DataSetCrossDockingTableAdapters.EntradasSalidasGuiaCarnicaTableAdapter adGuia = new Reportes.Dataset.DataSetCrossDockingTableAdapters.EntradasSalidasGuiaCarnicaTableAdapter();
                Dataset.DataSetCrossDockingTableAdapters.SedeTableAdapter adSede = new Dataset.DataSetCrossDockingTableAdapters.SedeTableAdapter();
                Dataset.DataSetCrossDockingTableAdapters.EspecieTableAdapter adEspecie = new Dataset.DataSetCrossDockingTableAdapters.EspecieTableAdapter();

                adCrossCabecero.Fill(data.DataTableCrossDocking, NoDocumento, IdEmpresa, IdSede);
                adCross.Fill(data.CrossDockingDetalle, NoDocumento, IdEmpresa, IdSede, Item);
                adProd.Fill(data.CrossDockingProductos, NoDocumento, IdEmpresa, IdSede, Item);
                adGuia.Fill(data.EntradasSalidasGuiaCarnica, IdEmpresa, IdSede, NoDocumento, Item);

                adSede.Fill(data.Sede, IdEmpresa, IdSede);

                rpt.SetDataSource(data);

                String especie1 = adEspecie.Get(data.CrossDockingDetalle[0].IdEspecie1, IdEmpresa);
                String especie2 = adEspecie.Get(data.CrossDockingDetalle[0].IdEspecie2, IdEmpresa);

                if (especie2 != null && especie1 != null)
                {
                    especie1 = especie1 + ", ";
                }


                rpt.SetParameterValue("Especies", especie1 + especie2);
                string ano = data.CrossDockingDetalle[0].Fecha.Year.ToString().Substring(2, 2);
                string NoGuia = data.Sede[0].NoGuiaCarnica.ToString() + "-" + data.EntradasSalidasGuiaCarnica[0].NumeroGuia.ToString() + "-" + ano;
                rpt.SetParameterValue("NoGuia", NoGuia);
                rpt.SetParameterValue("Codigo", data.Sede[0].NoGuiaCarnica.ToString());
                return rpt;

            }
            catch (Exception)
            {
                return rpt;
            }

        }

        //Jonathan Reportes
        public Models.ModeloReportes.RPTEntradaSalida.Movimiento MovimientoReporteNGEnca(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, Int64 NoDocumento)
        {
            Models.ModeloReportes.RPTEntradaSalida.Movimiento ModeloDatosMovi = new Models.ModeloReportes.RPTEntradaSalida.Movimiento();
            try
            {
                Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
                Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter adSede = new Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter adMovimiento = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.CantidadPosicionesTableAdapter adCantPosiciones = new Dataset.DataSetMovimientosTableAdapters.CantidadPosicionesTableAdapter();
                //adTareas.Fill(data.TareasAsignadas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adMovimiento.Fill(data.EntradasSalidas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adSede.Fill(data.Sede, IdEmpresa, IdSede);
                ModeloDatosMovi.IdEmpresa = data.EntradasSalidas[0].IdEmpresa.ToString();
                ModeloDatosMovi.IdSede = data.EntradasSalidas[0].IdSede.ToString();
                ModeloDatosMovi.IdProceso = data.EntradasSalidas[0].IdProceso.ToString();
                ModeloDatosMovi.NoDocumento = data.EntradasSalidas[0].NoDocumento.ToString();
                ModeloDatosMovi.Empresa = data.EntradasSalidas[0].Empresa.ToString();
                ModeloDatosMovi.Nit = data.EntradasSalidas[0].Nit.ToString();
                ModeloDatosMovi.Sede = data.EntradasSalidas[0].Sede.ToString();
                ModeloDatosMovi.Proceso = data.EntradasSalidas[0].Proceso.ToString();
                ModeloDatosMovi.FechaInicio = data.EntradasSalidas[0].FechaInicio;
                ModeloDatosMovi.Observaciones = data.EntradasSalidas[0].Observaciones.ToString();
                ModeloDatosMovi.Placa = data.EntradasSalidas[0].Placa.ToString();
                ModeloDatosMovi.Remision = data.EntradasSalidas[0].Remision.ToString();
                ModeloDatosMovi.RazonSocial = data.EntradasSalidas[0].RazonSocial.ToString();
                ModeloDatosMovi.NitCliente = data.EntradasSalidas[0].NitCliente.ToString();
                ModeloDatosMovi.Direccion = data.EntradasSalidas[0].Direccion.ToString();
                ModeloDatosMovi.Telefono = data.EntradasSalidas[0].Telefono.ToString();
                ModeloDatosMovi.Ciudad = data.EntradasSalidas[0].Ciudad.ToString();
                ModeloDatosMovi.Departamento = data.EntradasSalidas[0].Departamento.ToString();
                ModeloDatosMovi.Pais = data.EntradasSalidas[0].Pais.ToString();
                ModeloDatosMovi.Usuario = data.EntradasSalidas[0].Usuario.ToString();
                ModeloDatosMovi.NombreEmpleado = data.EntradasSalidas[0].NombreEmpleado.ToString();
                ModeloDatosMovi.SubBodega = data.EntradasSalidas[0].SubBodega.ToString();
                ModeloDatosMovi.Conductor = data.EntradasSalidas[0].Conductor.ToString();
                ModeloDatosMovi.ServicioSubProceso = data.EntradasSalidas[0].ServicioSubProceso.ToString();
                ModeloDatosMovi.CodigoDocumento = data.EntradasSalidas[0].CodigoDocumento.ToString();
                ModeloDatosMovi.IdServicio = data.EntradasSalidas[0].IdServicio.ToString();
                ModeloDatosMovi.Cobrar = data.EntradasSalidas[0].Cobrar.ToString();
                ModeloDatosMovi.IdSubProceso = data.EntradasSalidas[0].IdSubProceso.ToString();
                ModeloDatosMovi.FechaFinal = data.EntradasSalidas[0].FechaFinal;
                ModeloDatosMovi.IdEstado = data.EntradasSalidas[0].IdEstado.ToString();
                ModeloDatosMovi.MotivoAnulacion = data.EntradasSalidas[0].MotivoAnulacion.ToString();
                ModeloDatosMovi.MotivoNoCobro = data.EntradasSalidas[0].MotivoNoCobro.ToString();
                ModeloDatosMovi.Temperatura = data.EntradasSalidas[0].Temperatura.ToString();
                ModeloDatosMovi.NombreEspecie = data.EntradasSalidas[0].NombreEspecie.ToString();
                ModeloDatosMovi.Sello = data.EntradasSalidas[0].Sello.ToString();
                ModeloDatosMovi.Color = data.EntradasSalidas[0].Color.ToString();
                ModeloDatosMovi.Aroma = data.EntradasSalidas[0].Aroma.ToString();
                ModeloDatosMovi.Muelle = data.EntradasSalidas[0].Muelle.ToString();
                ModeloDatosMovi.IdEmpresa = data.Sede[0].IdEmpresa.ToString();
                ModeloDatosMovi.IdSede = data.Sede[0].IdSede.ToString();
                ModeloDatosMovi.Sede = data.Sede[0].Sede.ToString();
                ModeloDatosMovi.DireccionSede = data.Sede[0].DireccionSede.ToString();
                ModeloDatosMovi.TextoGrandeSede = data.Sede[0].TextoGrandeSede.ToString();
                ModeloDatosMovi.PrefijoSede = data.Sede[0].PrefijoSede.ToString();
                ModeloDatosMovi.ObservacionSede = data.Sede[0].ObservacionSede.ToString();
                ModeloDatosMovi.Correo = data.Sede[0].Correo.ToString();
                ModeloDatosMovi.Telefono = data.Sede[0].Telefono.ToString();
                ModeloDatosMovi.Empresa = data.Sede[0].Empresa.ToString();
                ModeloDatosMovi.NoGuiaCarnica = data.Sede[0].NoGuiaCarnica.ToString();
                ModeloDatosMovi.CantidadPosiciones = Convert.ToInt32(adCantPosiciones.CantPosiciones(IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio));
                return ModeloDatosMovi;
            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;
            }
            return ModeloDatosMovi;
        }

        public List<Models.ModeloReportes.RPTEntradaSalida.MovimientoItem> MovimientoReporteNGDeta(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, Int64 NoDocumento)
        {
            List<Models.ModeloReportes.RPTEntradaSalida.MovimientoItem> ModeloDatosMovimientoItem = new List<Models.ModeloReportes.RPTEntradaSalida.MovimientoItem>();
            try
            {
                Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter adMovimientoItem = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter();
                adMovimientoItem.Fill(data.EntradasSalidasItems, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                foreach (DataRow Valor in data.EntradasSalidasItems.Rows)
                {
                    if (IdProceso == 1)
                    {


                        ModeloDatosMovimientoItem.Add
                            (
                                new Models.ModeloReportes.RPTEntradaSalida.MovimientoItem()
                                {
                                    IdEmpresa = Valor["IdEmpresa"].ToString(),
                                    IdSede = Valor["IdSede"].ToString(),
                                    IdProceso = Valor["IdProceso"].ToString(),
                                    NoDocumento = Valor["NoDocumento"].ToString(),
                                    IdItem = Valor["IdItem"].ToString(),
                                    Posicion = Valor["Posicion"].ToString(),
                                    DescripcionProducto = Valor["DescripcionProducto"].ToString(),
                                    CodEscrito = Valor["CodEscrito"].ToString(),
                                    Descripcion = Valor["Descripcion"].ToString(),
                                    Embalaje = Valor["Embalaje"].ToString(),
                                    Lote = Valor["Lote"].ToString(),
                                    Vencimiento = Convert.ToDateTime(Valor["Vencimiento"]),
                                    EntradaCantidades = Math.Round(Convert.ToDouble(Valor["EntradaCantidades"]), 2),
                                    EntradaUnid = Math.Round(Convert.ToDouble(Valor["EntradaUnid"]), 2),
                                    EntredaKilo = Math.Round(Convert.ToDouble(Valor["EntredaKilo"]), 2),
                                    KilogramoProm = decimal.Round(Convert.ToDecimal(Valor["KilogramoProm"]), 2),
                                    IdServicio = Valor["IdServicio"].ToString(),
                                    //SalidaUnid = Valor["SalidaUnid"].ToString(),
                                    //SalidaKilo = Valor["SalidaKilo"].ToString(),
                                    //SalidaCantidades = Valor["SalidaCantidades"].ToString(),
                                    Temperatura = decimal.Round(Convert.ToDecimal(Valor["Temperatura"]), 2),
                                    KilogramoPromSale = Math.Round(Convert.ToDouble(Valor["KilogramoPromSale"]), 2),
                                }
                            );
                    }
                    else
                    {
                        ModeloDatosMovimientoItem.Add
                      (
                          new Models.ModeloReportes.RPTEntradaSalida.MovimientoItem()
                          {
                              IdEmpresa = Valor["IdEmpresa"].ToString(),
                              IdSede = Valor["IdSede"].ToString(),
                              IdProceso = Valor["IdProceso"].ToString(),
                              NoDocumento = Valor["NoDocumento"].ToString(),
                              IdItem = Valor["IdItem"].ToString(),
                              Posicion = Valor["Posicion"].ToString(),
                              DescripcionProducto = Valor["DescripcionProducto"].ToString(),
                              CodEscrito = Valor["CodEscrito"].ToString(),
                              Descripcion = Valor["Descripcion"].ToString(),
                              Embalaje = Valor["Embalaje"].ToString(),
                              Lote = Valor["Lote"].ToString(),
                              Vencimiento = Convert.ToDateTime(Valor["Vencimiento"]),
                              EntradaCantidades = Math.Round(Convert.ToDouble(Valor["SalidaCantidades"]), 2),
                              EntradaUnid = Math.Round(Convert.ToDouble(Valor["SalidaUnid"]), 2),
                              EntredaKilo = Math.Round(Convert.ToDouble(Valor["SalidaKilo"]), 2),
                              KilogramoProm = decimal.Round(Convert.ToDecimal(Valor["KilogramoProm"]), 2),
                              IdServicio = Valor["IdServicio"].ToString(),
                              //SalidaUnid = Valor["SalidaUnid"].ToString(),
                              //SalidaKilo = Valor["SalidaKilo"].ToString(),
                              //SalidaCantidades = Valor["SalidaCantidades"].ToString(),
                              Temperatura = decimal.Round(Convert.ToDecimal(Valor["Temperatura"]), 2),
                              KilogramoPromSale = Math.Round(Convert.ToDouble(Valor["KilogramoPromSale"]), 2),
                          }
                      );
                    }
                }
                DateTime fecha = Convert.ToDateTime("12/11/2021");
                return ModeloDatosMovimientoItem;
            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;
            }
            return ModeloDatosMovimientoItem;
        }


        public List<Models.ModeloReportes.RPTEntradaSalida.Tarea> MovimientoReporteNGTareas(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, Int64 NoDocumento)
        {
            List<Models.ModeloReportes.RPTEntradaSalida.Tarea> ModeloDatosTareas = new List<Models.ModeloReportes.RPTEntradaSalida.Tarea>();
            try
            {
                Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
                Dataset.DataSetMovimientosTableAdapters.TareasAsignadasTableAdapter adTareas = new Dataset.DataSetMovimientosTableAdapters.TareasAsignadasTableAdapter();
                adTareas.Fill(data.TareasAsignadas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);

                foreach (DataRow Valor in data.TareasAsignadas.Rows)
                {
                    ModeloDatosTareas.Add
                        (
                            new Models.ModeloReportes.RPTEntradaSalida.Tarea()
                            {
                                IdEmpresa = Valor["IdEmpresa"].ToString(),
                                IdSede = Valor["IdSede"].ToString(),
                                IdProceso = Valor["IdProceso"].ToString(),
                                NoDocumento = Valor["NoDocumento"].ToString(),
                                IdServicio = Valor["IdServicio"].ToString(),
                                IdEmpleado = Valor["IdEmpleado"].ToString(),
                                Empleado = Valor["Empleado"].ToString(),
                                IdTarea = Valor["IdTarea"].ToString(),
                                NombreTarea = Valor["NombreTarea"].ToString(),
                            }
                        );
                }
                return ModeloDatosTareas;
            }
            catch (Exception ex)
            {
                string mensaje;
                mensaje = ex.Message;
            }
            return ModeloDatosTareas;
        }

        public List<Models.ModeloReportes.RPTEntradaSalidaConsolidado.ConsolidadoDetalle> ConsolidadoNuevo(int idEmpresa, int idSede, int idServicio, int idProceso, string placa, string noRemision)
        {

            List<Models.ModeloReportes.RPTEntradaSalidaConsolidado.ConsolidadoDetalle> ModeloDatosConsolidado = new List<Models.ModeloReportes.RPTEntradaSalidaConsolidado.ConsolidadoDetalle>();

            try
            {
                Dataset.DataSetMovimientos.RepConsolidadoDataTable data = new Reportes.Dataset.DataSetMovimientos.RepConsolidadoDataTable();
                Dataset.DataSetMovimientosTableAdapters.RepConsolidadoTableAdapter adMov = new Dataset.DataSetMovimientosTableAdapters.RepConsolidadoTableAdapter();
                data = adMov.GetDataRepConsolIng(noRemision, placa, idEmpresa, idSede, idServicio, idProceso);
                //adMov.FillRepConsolIng(data.RepConsolidado, noRemision, placa, idEmpresa, idSede, idServicio, idProceso);
                foreach (DataRow Valor in data.Rows)
                {


                    if (idProceso == 1)
                    {

                        ModeloDatosConsolidado.Add
                       (
                           new Models.ModeloReportes.RPTEntradaSalidaConsolidado.ConsolidadoDetalle()
                           {
                               RazonSocial = Valor["RazonSocial"].ToString(),
                               Descripcion = Valor["Descripcion"].ToString(),
                               CodEscrito = Valor["CodEscrito"].ToString(),
                               NoDocumento = Convert.ToInt32(Valor["NoDocumento"].ToString()),
                               Lote = Valor["Lote"].ToString(),
                               EntradaUnid = Math.Round(Convert.ToDouble(Valor["EntradaUnid"]), 2),
                               EntredaKilo = Math.Round(Convert.ToDouble(Valor["EntredaKilo"]), 2),
                               EntradaCantidades = Math.Round(Convert.ToDouble(Valor["EntradaCantidades"]), 2),
                               IdVehiculo = Valor["IdVehiculo"].ToString(),
                               Remision = Valor["Remision"].ToString(),
                               IdTercero = Convert.ToInt32(Valor["IdTercero"].ToString()),
                               FechaInicio = Convert.ToDateTime(Valor["FechaInicio"].ToString()),
                               IdItem = Convert.ToInt32(Valor["IdItem"].ToString()),
                               IdProceso = Convert.ToDouble(Valor["IdProceso"].ToString()),
                               //SalidaUnid = Math.Round(Convert.ToDouble(Valor["SalidaUnid"]), 2),
                               //SalidaKilo = Math.Round(Convert.ToDouble(Valor["SalidaKilo"]), 2),
                               //SalidaCantidades = Math.Round(Convert.ToDouble(Valor["SalidaCantidades"]), 2),
                               Sello = Valor["Sello"].ToString(),
                               NombreConductor = Valor["NombreConductor"].ToString(),
                               FechaVencimiento = Convert.ToDateTime(Valor["FechaVencimiento"].ToString()),
                               IdServicio = Convert.ToInt32(Valor["IdServicio"].ToString()),
                               IdSede = Convert.ToInt32(Valor["IdSede"].ToString()),
                               Indicador = 1,
                           }
                       );

                    }
                    else
                    {
                        ModeloDatosConsolidado.Add
                                              (
                                                  new Models.ModeloReportes.RPTEntradaSalidaConsolidado.ConsolidadoDetalle()
                                                  {
                                                      RazonSocial = Valor["RazonSocial"].ToString(),
                                                      Descripcion = Valor["Descripcion"].ToString(),
                                                      CodEscrito = Valor["CodEscrito"].ToString(),
                                                      NoDocumento = Convert.ToInt32(Valor["NoDocumento"].ToString()),
                                                      Lote = Valor["Lote"].ToString(),
                                                      EntradaUnid = Math.Round(Convert.ToDouble(Valor["SalidaUnid"]), 2),
                                                      EntredaKilo = Math.Round(Convert.ToDouble(Valor["SalidaKilo"]), 2),
                                                      EntradaCantidades = Math.Round(Convert.ToDouble(Valor["SalidaCantidades"]), 2),
                                                      IdVehiculo = Valor["IdVehiculo"].ToString(),
                                                      Remision = Valor["Remision"].ToString(),
                                                      IdTercero = Convert.ToInt32(Valor["IdTercero"].ToString()),
                                                      FechaInicio = Convert.ToDateTime(Valor["FechaInicio"].ToString()),
                                                      IdItem = Convert.ToInt32(Valor["IdItem"].ToString()),
                                                      IdProceso = Convert.ToDouble(Valor["IdProceso"].ToString()),
                                                      //SalidaUnid = Math.Round(Convert.ToDouble(Valor["SalidaUnid"]), 2),
                                                      //SalidaKilo = Math.Round(Convert.ToDouble(Valor["SalidaKilo"]), 2),
                                                      //SalidaCantidades = Math.Round(Convert.ToDouble(Valor["SalidaCantidades"]), 2),
                                                      Sello = Valor["Sello"].ToString(),
                                                      NombreConductor = Valor["NombreConductor"].ToString(),
                                                      FechaVencimiento = Convert.ToDateTime(Valor["FechaVencimiento"].ToString()),
                                                      IdServicio = Convert.ToInt32(Valor["IdServicio"].ToString()),
                                                      IdSede = Convert.ToInt32(Valor["IdSede"].ToString()),
                                                      Indicador = 1,
                                                  }
                                              );
                    }

                }
                return ModeloDatosConsolidado;
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show(ex.Message);
                return ModeloDatosConsolidado;
            }

        }


        public Reportes.Models.ModeloReportes.RPTGuiaCarnica.GuiaCarnicaEntradaSalida MovimientoGuiaNuevo(int IdEmpresa, int IdSede, int IdProceso, int IdServicio, int NoDocumento, int Item)
        {
            Reportes.Dataset.DataSetMovimientos data = new Reportes.Dataset.DataSetMovimientos();
            Reportes.Models.ModeloReportes.RPTGuiaCarnica.GuiaCarnicaEntradaSalida Datos = new Models.ModeloReportes.RPTGuiaCarnica.GuiaCarnicaEntradaSalida();


            try
            {

                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter adMovimiento = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter adMovimientoItem = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter adSede = new Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.EntradasSalidasGuiaCarnicaTableAdapter adguia = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasGuiaCarnicaTableAdapter();
                Dataset.DataSetMovimientosTableAdapters.CantidadPosicionesTableAdapter adCantPosiciones = new Dataset.DataSetMovimientosTableAdapters.CantidadPosicionesTableAdapter();

                adMovimiento.Fill(data.EntradasSalidas, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adMovimientoItem.Fill(data.EntradasSalidasItems, IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio);
                adSede.Fill(data.Sede, IdEmpresa, IdSede);
                adguia.FillByGuia(data.EntradasSalidasGuiaCarnica, IdEmpresa, IdSede, NoDocumento, IdServicio, Item);

                List<Models.ModeloReportes.RPTGuiaCarnica.EncabezadoEntradaSalidaDetalle> ListEntradasSalidasItemsInicial = new List<Models.ModeloReportes.RPTGuiaCarnica.EncabezadoEntradaSalidaDetalle>();

                #region MyRegion ES
                Datos.IdEmpresa = data.EntradasSalidas[0].IdEmpresa;
                Datos.IdSede = data.EntradasSalidas[0].IdSede;
                Datos.IdProceso = data.EntradasSalidas[0].IdProceso;
                Datos.NoDocumento = data.EntradasSalidas[0].NoDocumento;
                Datos.Empresa = data.EntradasSalidas[0].Empresa;
                Datos.Nit = data.EntradasSalidas[0].Nit;
                Datos.Sede = data.EntradasSalidas[0].Sede;
                Datos.Proceso = data.EntradasSalidas[0].Proceso;
                Datos.FechaInicio = data.EntradasSalidas[0].FechaInicio;
                Datos.Observaciones = data.EntradasSalidas[0].Observaciones;
                Datos.Placa = data.EntradasSalidas[0].Placa;
                Datos.Remision = data.EntradasSalidas[0].Remision;
                Datos.RazonSocial = data.EntradasSalidas[0].RazonSocial;
                Datos.NitCliente = data.EntradasSalidas[0].NitCliente;
                Datos.Direccion = data.EntradasSalidas[0].Direccion;
                Datos.Telefono = data.EntradasSalidas[0].Telefono;
                Datos.Ciudad = data.EntradasSalidas[0].Ciudad;
                Datos.Departamento = data.EntradasSalidas[0].Departamento;
                Datos.Pais = data.EntradasSalidas[0].Pais;
                Datos.Usuario = data.EntradasSalidas[0].Usuario;
                Datos.NombreEmpleado = data.EntradasSalidas[0].NombreEmpleado;
                Datos.SubBodega = data.EntradasSalidas[0].SubBodega;
                Datos.Conductor = data.EntradasSalidas[0].Conductor;
                Datos.ServicioSubProceso = data.EntradasSalidas[0].ServicioSubProceso;
                Datos.CodigoDocumento = data.EntradasSalidas[0].CodigoDocumento;
                Datos.IdServicio = data.EntradasSalidas[0].IdServicio;
                Datos.Cobrar = data.EntradasSalidas[0].Cobrar;
                Datos.IdSubProceso = data.EntradasSalidas[0].IdSubProceso;
                Datos.FechaFinal = data.EntradasSalidas[0].FechaFinal;
                Datos.IdEstado = data.EntradasSalidas[0].IdEstado;
                Datos.MotivoAnulacion = data.EntradasSalidas[0].MotivoAnulacion;
                Datos.MotivoNoCobro = data.EntradasSalidas[0].MotivoNoCobro;
                Datos.Temperatura = data.EntradasSalidas[0].Temperatura;
                Datos.NombreEspecie = data.EntradasSalidas[0].NombreEspecie;
                Datos.Sello = data.EntradasSalidas[0].Sello;
                Datos.Color = data.EntradasSalidas[0].Color;
                Datos.Aroma = data.EntradasSalidas[0].Aroma;
                Datos.Muelle = data.EntradasSalidas[0].Muelle;


                Datos.CantidadPosiciones = Convert.ToInt32(adCantPosiciones.CantPosiciones(IdEmpresa, IdSede, IdProceso, NoDocumento, IdServicio));

                #endregion

                #region MyRegion Sede
                Datos.DatosSede.IdEmpresa = data.Sede[0].IdEmpresa;
                Datos.DatosSede.IdSede = data.Sede[0].IdSede;
                Datos.DatosSede.Sede = data.Sede[0].Sede;
                Datos.DatosSede.DireccionSede = data.Sede[0].DireccionSede;
                Datos.DatosSede.TextoGrandeSede = data.Sede[0].TextoGrandeSede;
                Datos.DatosSede.PrefijoSede = data.Sede[0].PrefijoSede;
                Datos.DatosSede.ObservacionSede = data.Sede[0].ObservacionSede;
                Datos.DatosSede.Correo = data.Sede[0].Correo;
                Datos.DatosSede.Telefono = data.Sede[0].Telefono;
                Datos.DatosSede.Empresa = data.Sede[0].Empresa;
                Datos.DatosSede.NoGuiaCarnica = data.Sede[0].NoGuiaCarnica;
                #endregion

                #region MyRegion Carnica
                Datos.DatosGuia.IdEmpresa = data.EntradasSalidasGuiaCarnica[0].IdEmpresa;
                Datos.DatosGuia.IdSede = data.EntradasSalidasGuiaCarnica[0].IdSede;
                Datos.DatosGuia.NoDocumento = data.EntradasSalidasGuiaCarnica[0].NoDocumento;
                Datos.DatosGuia.IdServicio = data.EntradasSalidasGuiaCarnica[0].IdServicio;
                Datos.DatosGuia.Item = data.EntradasSalidasGuiaCarnica[0].Item;
                Datos.DatosGuia.Destino = data.EntradasSalidasGuiaCarnica[0].Destino;
                Datos.DatosGuia.Departamento = data.EntradasSalidasGuiaCarnica[0].Departamento;
                Datos.DatosGuia.Municipio = data.EntradasSalidasGuiaCarnica[0].Municipio;
                Datos.DatosGuia.DireccionDestino = data.EntradasSalidasGuiaCarnica[0].DireccionDestino;
                Datos.DatosGuia.DepartamentoNombre = data.EntradasSalidasGuiaCarnica[0].DepartamentoNombre;
                Datos.DatosGuia.MunicipioNombre = data.EntradasSalidasGuiaCarnica[0].MunicipioNombre;
                Datos.DatosGuia.NumeroGuia = data.EntradasSalidasGuiaCarnica[0].NumeroGuia;
                #endregion

                #region MyRegion Detalle
                foreach (DataRow Valor in data.EntradasSalidasItems.Rows)
                {
                    ListEntradasSalidasItemsInicial.Add
                        (
                            new Models.ModeloReportes.RPTGuiaCarnica.EncabezadoEntradaSalidaDetalle()
                            {
                                IdEmpresa = Valor["IdEmpresa"].ToString(),
                                IdSede = Valor["IdSede"].ToString(),
                                IdProceso = Valor["IdProceso"].ToString(),
                                NoDocumento = Valor["NoDocumento"].ToString(),
                                IdItem = Valor["IdItem"].ToString(),
                                Posicion = Valor["Posicion"].ToString(),
                                DescripcionProducto = Valor["DescripcionProducto"].ToString(),
                                CodEscrito = Valor["CodEscrito"].ToString(),
                                Descripcion = Valor["Descripcion"].ToString(),
                                Embalaje = Valor["Embalaje"].ToString(),
                                Lote = Valor["Lote"].ToString(),
                                Vencimiento = Convert.ToDateTime(Valor["Vencimiento"]),
                                EntradaCantidades = Math.Round(Convert.ToDouble(Valor["SalidaCantidades"]), 2),
                                EntradaUnid = Math.Round(Convert.ToDouble(Valor["SalidaUnid"]), 2),
                                EntredaKilo = Math.Round(Convert.ToDouble(Valor["SalidaKilo"]), 2),
                                KilogramoProm = decimal.Round(Convert.ToDecimal(Valor["KilogramoProm"]), 2),
                                IdServicio = Valor["IdServicio"].ToString(),
                                //SalidaUnid = Valor["SalidaUnid"].ToString(),
                                //SalidaKilo = Valor["SalidaKilo"].ToString(),
                                //SalidaCantidades = Valor["SalidaCantidades"].ToString(),
                                Temperatura = decimal.Round(Convert.ToDecimal(Valor["Temperatura"]), 2),
                                KilogramoPromSale = Math.Round(Convert.ToDouble(Valor["KilogramoPromSale"]), 2),
                                //IdEstado = Valor["IdEmpresa"].ToString(),
                            }
                        );

                }


                string ano = data.EntradasSalidas[0].FechaFinal.Year.ToString().Substring(2, 2);

                Datos.NoGuia = data.Sede[0].NoGuiaCarnica.ToString() + "-" + data.EntradasSalidasGuiaCarnica[0].NumeroGuia.ToString() + "-" + ano;
                
                Datos.ListDetalle = ListEntradasSalidasItemsInicial;
                #endregion

                return Datos;
            }
            catch (Exception ex)
            {
                return Datos;
            }
        }
    }
}


