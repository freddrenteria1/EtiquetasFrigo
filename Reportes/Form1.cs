using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;


namespace Reportes
{
    public partial class Form1 : Form
    {

        //int IdEmpresa = 1;
        //int IdSede = 1;
        //int IdProceso = 2;
        //int NoDocumento = 2;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dataset.DataSetMovimientos data = new Dataset.DataSetMovimientos();
            //Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter adMovimiento = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasTableAdapter();
            //Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter adMovimientoItem = new Dataset.DataSetMovimientosTableAdapters.EntradasSalidasItemsTableAdapter();
            //Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter adSede = new Dataset.DataSetMovimientosTableAdapters.SedeTableAdapter();

            Dataset.DataSetMovimientosTableAdapters.RepConsolidadoTableAdapter adRepCong = new Dataset.DataSetMovimientosTableAdapters.RepConsolidadoTableAdapter();


            try
            {
                //adMovimiento.Fill(data.EntradasSalidas, IdEmpresa, IdSede, IdProceso, NoDocumento,4);
                //adMovimientoItem.Fill(data.EntradasSalidasItems, IdEmpresa, IdSede, IdProceso, NoDocumento,4);
                //adSede.Fill(data.Sede, IdEmpresa, IdSede);

                adRepCong.FillRepConsolIng(data.RepConsolidado,"08/05/19","2TNL3", 1,1,4,1);


                ReportDocument rpt = new ReportDocument();

                rpt = new Movimientos.RptConsolidado();

                rpt.SetDataSource(data);

                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();

                //rpt = New RepCongIngrConsol
                //        Dim adRepCong As New DataSetRepCongTableAdapters.RepCongIngConsolTableAdapter
                //        adRepCong.Fill(data.RepCongIngConsol, RemisionTextBox.Text, PlacaTextBox.Text)


            }
            catch (Exception )
            {

            }


        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
