Imports System.Data.SqlClient
Imports System.Math
Imports System.Drawing.Image
Imports System.Drawing
Imports QRCoder

Public Class Cedula

    Friend WithEvents CedulaServicio As New Drawing.Printing.PrintDocument


    Dim version = "0.1"
    Dim Servicio, Posicion, NombreCliente, CodProd, NombProd, Origen, Lote As String
    Dim FechaVence, Embalaje, CodInt, InfoDocumento, Direccion As String
    'Private fechaVence As Object
    Dim Unidades As Integer
    'Private unidades As Integer
    Dim PesoTeorico As Double
    Dim idEmpresa, idSede, idTercero As Integer
    'Private v1 As String
    'Private codProducto As String
    'Private producto1 As String
    'Private producto2 As Object
    'Private v2 As Integer
    'Private kilos As Object
    'Private v3 As String
    'Private lote As Object
    'Private embalaje As Object
    'Private codigoINT As Object
    'Private informacionMovimiento As Object
    'Private idEmpresa1 As Object
    'Private idSede1 As Object
    'Private idTercero1 As Object

    Public Sub New(servicio As String, posicion As String, nombreCliente As String, codProd As String,
                   nombProd As String, unidades As Integer, pesoTeorico As Double, origen As String,
                   lote As String, fechaVence As String, embalaje As String, codInt As String,
infoDocumento As String, idEmpresa As Integer, idSede As Integer, idTercero As Integer, Direccion As String)

        Me.idEmpresa = idEmpresa
        Me.idSede = idSede
        Me.idTercero = idTercero
        Me.Servicio = servicio
        Me.Posicion = posicion
        Me.NombreCliente = nombreCliente
        Me.CodProd = codProd
        Me.NombProd = nombProd
        Me.NombreCliente = nombreCliente
        Me.Unidades = unidades
        Me.PesoTeorico = pesoTeorico
        Me.Origen = origen
        Me.Lote = lote
        Me.FechaVence = fechaVence
        Me.Embalaje = embalaje
        Me.CodInt = codInt
        Me.InfoDocumento = infoDocumento
        Me.Direccion = Direccion


        CedulaServicio.Print()
    End Sub

    'Public Sub New(v1 As String, codProducto As String, producto1 As String, unidades As Integer, producto2 As Object, v2 As Integer, kilos As Object, v3 As String, lote As Object, fechaVence As Object, embalaje As Object, codigoINT As Object, informacionMovimiento As Object, idEmpresa1 As Object, idSede1 As Object, idTercero1 As Object)
    '    Me.v1 = v1
    '    Me.codProducto = codProducto
    '    Me.producto1 = producto1
    '    Me.unidades = unidades
    '    Me.producto2 = producto2
    '    Me.v2 = v2
    '    Me.kilos = kilos
    '    Me.v3 = v3
    '    Me.lote = lote
    '    Me.fechaVence = fechaVence
    '    Me.embalaje = embalaje
    '    Me.codigoINT = codigoINT
    '    Me.informacionMovimiento = informacionMovimiento
    '    Me.idEmpresa1 = idEmpresa1
    '    Me.idSede1 = idSede1
    '    Me.idTercero1 = idTercero1
    'End Sub

    Private Sub CedulaServicio_PrintPage(sender As Object, e As Drawing.Printing.PrintPageEventArgs) Handles CedulaServicio.PrintPage
        Try

            Console.WriteLine("Inicia crear Etiquita")

            Dim Fuente As Font
            Dim Texto As String
            'Dim CodCli As Integer
            'Dim LogoCliente As Image

            'CodCli = Val(CodCliente.Text)

            'Dim data As DataSetEtiqueta.DatosEtiquetaDataTable
            'Dim ad As New DataSetEtiquetaTableAdapters.DatosEtiquetaTableAdapter
            Dim DireccionPlanta As String = ""
            Dim NombreCentro As String = ""

            'data = ad.GetData(Me.idTercero, Me.idEmpresa, Me.idSede)

            'If data.Count > 0 Then
            '    DireccionPlanta = data(0).DireccionPlanta
            '    NombreCentro = data(0).Centro
            'End If

            If Me.idTercero = 1 Then
                DireccionPlanta = "Km 7. Autopista Floridablanca - Santander"
                NombreCentro = "Industria Colombiana"

            End If


            'If CodCli <> 5 Then
            e.Graphics.PageUnit = GraphicsUnit.Millimeter
            Fuente = New Font("Arial", 10, FontStyle.Bold)
            If NombreCentro.Length > 0 Then
                Texto = Me.NombreCliente ' " - (" + NombreCentro + ")"
            Else
                Texto = Me.NombreCliente
            End If

            If idSede = 1 Then Texto = Texto & "  (B003)"
            If idSede = 2 Then Texto = Texto & "  (B004)"
            If idSede = 3 Then Texto = Texto & "  (B009)"
            If idSede = 3 Then Texto = Texto & "  N.A."

            e.Graphics.DrawString("Fabricado por : " + Texto, Fuente, Brushes.Black, 4, 8)

            'Fuente = New Font("Arial", 12, FontStyle.Bold)
            Fuente = New Font("Arial", 10, FontStyle.Bold)
            Dim adi As Integer = 0

            Texto = "Producto : " & Me.NombProd

            If Texto.Length > 50 Then
                Dim texto1 As String = Texto.Substring(0, 50)
                e.Graphics.DrawString(texto1, Fuente, Brushes.Black, 4, 12)

                adi = 6
                Dim texto2 As String = Texto.Substring(50, Texto.Length - 50)
                e.Graphics.DrawString(texto2, Fuente, Brushes.Black, 4, 16)


            Else
                e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 12)
            End If

            Fuente = New Font("Arial", 10)
            Texto = "Cod de Producto : " & Me.CodProd
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 16 + adi)

            Fuente = New Font("Arial", 10)
            Texto = "Unidades : " & Me.Unidades '& "   Kilos : " & Format(Me.PesoTeorico, "0.000")
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 20 + adi)

            Fuente = New Font("Arial", 10)
            Texto = "Kilos : " & Format(Me.PesoTeorico, "0.000")
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 24 + adi)
            'Fuente = New Font("Arial", 12, FontStyle.Bold)
            'Texto = "Embalaje: " & Me.Embalaje ' "Kilos:       " & Format(Me.PesoTeorico, "0.000")

            Fuente = New Font("Arial", 10, FontStyle.Regular)
            Texto = "Embalaje : " & Embalaje ' "Kilos:       " & Format(Me.PesoTeorico, "0.000")
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 28 + adi)
            Texto = "Lote : " & Me.Origen & Me.Lote
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 32 + adi)
            Texto = "Vence : " & Me.FechaVence
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 36 + adi)
            'Texto = "Vence: " & Me.FechaVence
            'e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 33 + adi)
            'If FechaVence.Checked Then Texto = Texto & FechaVence.Value.ToString("dd/MMM/yyyy").ToUpper

            'e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 10, 33)
            'Texto = "Embalaje:    " & Me.Embalaje
            'e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 10, 37)

            'If DireccionPlanta.Length > 0 Then
            Texto = "Industria Colombiana."
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 40 + adi)
            Texto = Direccion
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 44 + adi)
            Texto = "Conservar Congelado a -18°C."
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 48 + adi)
            Fuente = New Font("Arial", 10)

            Try
                Dim posi() = Me.Posicion.Split(" ")

                Me.Posicion = posi(0) + "-" + posi(1) + "-" + posi(2) + "-" + posi(3)
            Catch ex As Exception

            End Try


            Texto = "Posicion:  " & Me.Posicion
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 52 + adi)
            'Fuente = New Font("Code 128", 30)
            'Texto = "12323" 'Barr.Trim
            'e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 6, 56)
            'Fuente = New Font("SansSerif", 8, FontStyle.Regular)
            'Dim chain = "12323"
            'e.Graphics.DrawString(Chain, Fuente, Brushes.Black, 10, 67)
            'Fuente = New Font("Arial", 10, FontStyle.Regular)
            'Fuente = New Font("Code 128", 40)

            'Dim stringFormat As StringFormat = New StringFormat()
            'stringFormat.FormatFlags = StringFormatFlags.DirectionVertical

            'Dim eanINT As String = Etiqueta.Ean128(Me.CodInt)
            'e.Graphics.DrawString(eanINT, Fuente, Brushes.Black, 75, 2, stringFormat)
            'Fuente = New Font("Arial", 8, FontStyle.Bold)
            'Esto era lo origianl
            'e.Graphics.DrawString(Me.CodInt, Fuente, Brushes.Black, 89, 20, StringFormat)
            'e.Graphics.DrawString(Me.CodInt, Fuente, Brushes.Black, 89, 20)

            Dim f As DateTime = Now()
            Texto = Me.InfoDocumento + "-" + f.ToString("dd/MMM/yyyy").ToUpper + version
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 56 + adi)

            Fuente = New Font("Code 128", 30)
            Dim stringFormat As StringFormat = New StringFormat()
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical

            Dim eanINT As String = Etiqueta.Ean128(Me.CodInt)
            e.Graphics.DrawString(eanINT, Fuente, Brushes.Black, 75, 7, stringFormat)
            Fuente = New Font("Arial", 8, FontStyle.Bold)
            e.Graphics.DrawString(Me.CodInt, Fuente, Brushes.Black, 89, 25, stringFormat)




            Dim Unidadeschar As String

            If Unidades < 10000 And Unidades > 999 Then
                Unidadeschar = "0" + Unidades.ToString()
            End If
            If Unidades < 1000 And Unidades > 99 Then
                Unidadeschar = "00" + Unidades.ToString()
            End If
            If Unidades < 100 Then
                Unidadeschar = "000" + Unidades.ToString()
            End If
            If Unidades < 10 Then
                Unidadeschar = "0000" + Unidades.ToString()
            End If
            If Unidades > 999 Then
                Unidadeschar = Unidades.ToString()
            End If


            Dim Codigochar As String
            If CodProd.Length >= 5 Then
                Codigochar = CodProd.Substring(CodProd.Length - 5, 5)
            End If
            If CodProd.Length <= 4 And CodProd.Length > 3 Then
                Codigochar = "0" + CodProd.ToString()
            End If
            If CodProd.Length <= 3 And CodProd.Length > 2 Then
                Codigochar = "00" + CodProd.ToString()
            End If
            If CodProd.Length <= 2 And CodProd.Length > 1 Then
                Codigochar = "000" + CodProd.ToString()
            End If
            If CodProd.Length <= 1 Then
                Codigochar = "0000" + CodProd.ToString()
            End If
            If CodProd.Length > 9999 Then
                Codigochar = CodProd.ToString()
            End If




            Dim Num As Decimal = Format(PesoTeorico, "0.00")
            Dim NumString As String = Num.ToString()
            Dim textSplit() As String = NumString.Split(",")
            Dim entero As String = textSplit(0)
            Dim Deci As String = textSplit(1)


            If entero < 100 Then
                entero = "00" + entero.ToString()
            Else
                If entero > 100 & entero < 1000 Then
                    entero = "0" + entero.ToString()
                End If
            End If

            If Deci > 10 Then
                Deci = Deci.ToString().Substring(0, 2) + "0"
            Else
                Deci = Deci.ToString() + "00"
            End If


            Dim strEm As String = Me.Embalaje.ToUpper
            Dim emQR As String = ""


            'If Me.Ced > 10 Then
            '    strCantidad = Me.cantidad.ToString()
            'Else
            '    strCantidad = "0" + Me.cantidad.ToString()
            'End If

            Select Case strEm
                Case "CAJA"
                    emQR = "CJ"
                Case "CANASTA"
                    emQR = "CAN"
                Case "bulto"
                    emQR = "BTO"
                Case "canastilla"
                    emQR = "CLL"
                Case "SACO"
                    emQR = "BTO"
                Case Else
                    emQR = strEm
            End Select



            Dim a As String = Codigochar.ToString()
            Dim b As String = Unidadeschar.ToString().Substring(0, 5)
            'Dim c As String = PesoTeorico.ToString().Substring(0, 4)
            Dim d As String = Me.Lote.ToString()

            Dim qr As New QRCodeGenerator
            'Dim Data = qr.CreateQrCode(+"" + Unidadeschar.ToString().Substring(0, 4) + "" + PesoTeorico.ToString().Substring(0, 4) + "" + Me.Lote.ToString(), QRCodeGenerator.ECCLevel.Q)
            Dim Data = qr.CreateQrCode(a + b + "" + entero.ToString().Substring(0, 4) + "" + Deci.ToString().Substring(0, 3) + "" + d + "01" + emQR, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(Data)
            Dim codigo As Image = code.GetGraphic(3)
            Dim srcRect As New RectangleF(1, 1, 100, 100)
            Dim units As New GraphicsUnit
            e.Graphics.DrawImage(codigo, 50, 20, srcRect, units.Pixel)

            e.HasMorePages = False


            Console.WriteLine("Termina hacer la etiqueta")
        Catch ex As Exception
            Console.WriteLine("Error al imprimir " + ex.Message)
        End Try

    End Sub

    Private Sub CedulaServicio_Disposed(sender As Object, e As EventArgs) Handles CedulaServicio.Disposed

    End Sub
End Class
