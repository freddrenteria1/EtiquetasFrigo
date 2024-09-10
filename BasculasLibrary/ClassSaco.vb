Imports System.Data.SqlClient
Imports System.Math
Imports System.Drawing.Image
Imports System.Drawing

Public Class ClassSaco

    Friend WithEvents Saco As New Drawing.Printing.PrintDocument

    Dim pro As String
    Dim cod As String
    Dim Unidades As Integer
    Dim PesoTeorico As Double
    Dim Servicio, Posicion, NombreCliente, NombProd, Origen, Lote As String
    Dim FechaVence, Embalaje, CodInt, InfoDocumento As String


    'Unidades=24&Kilos=22.25&Lote=B00121294&FechaVence=21/10/2022&Embalaje=SACO&Posicion
    'Double Unidades, Double Kilos, String Lote, String FechaVence, String Embalaje, String Posicion, String CodigoINT,

    Public Sub New(Producto As String, cod As String, posicion As String, unidades As Integer, pesoTeorico As Double,
                   lote As String, fechaVence As String, embalaje As String, codInt As String)


        Me.pro = Producto
        Me.cod = cod
        Me.Unidades = unidades
        Me.PesoTeorico = pesoTeorico
        Me.Origen = Origen
        Me.Lote = lote
        Me.FechaVence = fechaVence
        Me.Embalaje = embalaje
        Me.CodInt = codInt
        Me.Posicion = posicion

        Saco.Print()
    End Sub


    Private Sub Saco_PrintPage(sender As Object, e As Drawing.Printing.PrintPageEventArgs) Handles Saco.PrintPage

        Dim Fuente As Font
        'Dim Texto As String

        e.Graphics.PageUnit = GraphicsUnit.Millimeter
        Fuente = New Font("Arial", 10, FontStyle.Bold)


        e.Graphics.DrawString("AVIDESA MAC POLLO S.A (B003) ", Fuente, Brushes.Black, 4, 6)

        e.Graphics.DrawString("Producto: " + Me.pro, Fuente, Brushes.Black, 4, 10)
        e.Graphics.DrawString("Cod Producto: " + Me.cod.ToString() + "      " + " Kilos: " + Me.PesoTeorico.ToString(), Fuente, Brushes.Black, 4, 14)
        e.Graphics.DrawString("Unidades: " + Me.Unidades.ToString, Fuente, Brushes.Black, 4, 18)
        e.Graphics.DrawString("Embalaje: " + Embalaje, Fuente, Brushes.Black, 4, 22)
        e.Graphics.DrawString("Lote: " + Me.Lote, Fuente, Brushes.Black, 4, 26)
        e.Graphics.DrawString("Vence: " + Me.FechaVence, Fuente, Brushes.Black, 4, 30)

        Dim DireccionPlanta As String = "Km 7. Autopista Floridablanca - Santander"
        Dim NombreCentro As String = "Industria Colombiana"

        e.Graphics.DrawString(DireccionPlanta, Fuente, Brushes.Black, 4, 34)
        e.Graphics.DrawString(NombreCentro, Fuente, Brushes.Black, 4, 38)


        Try
            Dim posi() = Me.Posicion.Split(" ")

            Me.Posicion = posi(0) + "-" + posi(1) + "-" + posi(2) + "-" + posi(3)
        Catch ex As Exception

        End Try

        e.Graphics.DrawString("Posicion:  " & Me.Posicion, Fuente, Brushes.Black, 4, 42)

        e.HasMorePages = False
    End Sub


End Class
