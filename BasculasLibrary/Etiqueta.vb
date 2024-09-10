Imports System.Data.SqlClient
Imports System.Math
Imports System.Drawing.Image
Imports System.Drawing
Imports QRCoder

Public Class Etiqueta

    Dim PosicionString = ""
    Dim servicio As String
    Dim pasillo, nivel, posicion As Integer
    Dim Fecha As DateTime
    Dim NombreCliente As String
    Dim CodProd As String
    Dim NombProd As String
    Dim embalajeNombre As String
    Dim UnidEmbalaje As String
    Dim cantidad As Double
    Dim Kilos As Double
    Dim FechaVen As DateTime
    Dim Lote As String
    Dim NoItem As Integer
    Dim NoDocumento As Int64
    Dim UsuarioNombre As String


    Friend WithEvents CedulaCons As New Drawing.Printing.PrintDocument
    Friend WithEvents EtiqutaEspecia As New Drawing.Printing.PrintDocument



    Public Sub New(servicios As String, posicion As String, fecha As String, nombreCliente As String, codProd As String,
                   nomProd As String, enbalajeNombre As String, UnidEmbalaje As Double, cantidad As Double, kilos As Double, fechaVen As String,
                   lote As String, noDocumento As Int64, noItem As Integer, UsuarioNombre As String)

        ' Esta llamada es exigida por el diseñador.
        'InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        Me.servicio = servicios
        Me.PosicionString = posicion
        Me.Fecha = fecha
        Me.NombreCliente = nombreCliente
        Me.CodProd = codProd
        Me.NombProd = nomProd
        Me.embalajeNombre = enbalajeNombre
        Me.UnidEmbalaje = UnidEmbalaje
        Me.cantidad = cantidad
        Me.Kilos = kilos
        Me.FechaVen = fechaVen
        Me.Lote = lote
        Me.NoItem = noItem
        Me.NoDocumento = noDocumento
        Me.UsuarioNombre = UsuarioNombre

        CedulaCons.Print()
    End Sub


    Dim Nombre1, Nombre2, I As String

    Public Sub New(nombre1 As String, nombre2 As String, i As String, cantidad As Integer)
        Me.Nombre1 = nombre1
        Me.Nombre2 = nombre2
        i = i
        Me.cantidad = cantidad

    End Sub
    Private Sub EtiqutaEspecia_PrintPage(sender As Object, e As Drawing.Printing.PrintPageEventArgs) Handles EtiqutaEspecia.PrintPage

        Dim Fuente As Font


        e.Graphics.PageUnit = GraphicsUnit.Millimeter
        Fuente = New Font("Arial", 10, FontStyle.Bold)


        e.Graphics.DrawString(Nombre1, Fuente, Brushes.Black, 4, 6)

        e.Graphics.DrawString(Nombre2, Fuente, Brushes.Black, 4, 15)

        Dim Texto As String

        Texto = I + " de " + cantidad.ToString()

        e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 20)



        e.HasMorePages = False
    End Sub

    Private Sub CedulaCons_PrintPage(sender As Object, e As Drawing.Printing.PrintPageEventArgs) Handles CedulaCons.PrintPage
        Try
            Dim Fuente As Font
            Dim Texto As String

            e.Graphics.PageUnit = GraphicsUnit.Millimeter
            Fuente = New Font("Arial", 10, FontStyle.Bold)

            Dim leyenda1 As String = Me.servicio + "        FECHA:   " + Me.Fecha.ToString("dd/MMM/yyyy").ToUpper
            e.Graphics.DrawString(leyenda1, Fuente, Brushes.Black, 4, 6)
            e.Graphics.DrawString("CAMARA-PASI.-NIVEL-POSICION", Fuente, Brushes.Black, 10, 11)

            Fuente = New Font("Arial", 35)
            e.Graphics.DrawString(PosicionString, Fuente, Brushes.Black, 8, 13)

            Fuente = New Font("Arial", 10)
            'e.Graphics.DrawString(, Fuente, Brushes.Black, 8, 29)
            e.Graphics.DrawString(Me.NombreCliente, Fuente, Brushes.Black, 4, 27)

            Fuente = New Font("Arial", 10, FontStyle.Bold)
            e.Graphics.DrawString("COD. PRODUCTO: " + Me.CodProd, Fuente, Brushes.Black, 4, 31)

            If NombProd.Length < 31 Then
                e.Graphics.DrawString(NombProd, Fuente, Brushes.Black, 4, 35)
            Else
                NombProd = NombProd.Substring(0, 31)
                e.Graphics.DrawString(NombProd, Fuente, Brushes.Black, 4, 35)
            End If

            e.Graphics.DrawString("CANT: " + Me.cantidad.ToString + " " + Me.embalajeNombre + "(S)", Fuente, Brushes.Black, 4, 39)

            Texto = "EMBA: " & Me.embalajeNombre & " X " & UnidEmbalaje & " Unidades"
            Fuente = New Font("Arial", 10)
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 43)

            Fuente = New Font("Arial", 10)


            ''Se quita se cree que uno es para uno a uno y el otro es la impresion final se deja uno solo como parametro
            'If CmbCantPesaje.SelectedIndex = 2 Then
            '    e.Graphics.DrawString("CANTIDAD:    " + TxtTcant.Text + " " + Embalajes.Text + "(S)", Fuente, Brushes.Black, 8, 53)
            'Else
            '    e.Graphics.DrawString("CANTIDAD:    " + TxtCantidades.Text + " " + Embalajes.Text + "(S)", Fuente, Brushes.Black, 8, 53)
            'End If




            'If Not FechaVence.Checked Then
            '    e.Graphics.DrawString("F.VENCE: NULO", Fuente, Brushes.Black, 8, 63)
            'Else
            '    e.Graphics.DrawString("F.VENCE: " + FechaVence.Value.ToString("dd/MMM/yyyy").ToUpper, Fuente, Brushes.Black, 8, 63)
            'End If
            Fuente = New Font("Arial", 12, FontStyle.Bold)
            e.Graphics.DrawString("PESO: " + Format(Kilos, "0.000") + " Kgrs.  ", Fuente, Brushes.Black, 4, 46)

            Fuente = New Font("Arial", 10)
            e.Graphics.DrawString("LOTE: " + Me.Lote & "   F.VENCE: " + Me.FechaVen.ToString("dd/MMM/yyyy").ToUpper, Fuente, Brushes.Black, 4, 51)

            ' Fuente = New Font("Arial", 13)


            'e.Graphics.DrawString(, Fuente, Brushes.Black, 8, 50)
            Dim eanEntrItem As String = Ean128(Me.NoDocumento.ToString + " " + Me.NoItem.ToString())

            'StringFormat StringFormat = New StringFormat();
            'StringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            Dim stringFormat As StringFormat = New StringFormat()
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical

            Fuente = New Font("Code 128", 40)
            e.Graphics.DrawString(eanEntrItem, Fuente, Brushes.Black, 80, 3, stringFormat)
            'e.Graphics.DrawString(eanEntrItem, Fuente, Brushes.Black, 8, 68, stringFormat)

            'Fuente = New Font("Arial", 9)
            'e.Graphics.DrawString(Me.NoItem, Fuente, Brushes.Black, 64, 67)

            Fuente = New Font("Arial", 8, FontStyle.Bold)
            Texto = Me.servicio + " # " & Me.NoDocumento.ToString & " Item #" & Me.NoItem.ToString & " Usuario: " & Me.UsuarioNombre
            e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 4, 55)




            Dim Unidadeschar As String
            'If (UnidEmbalaje) < 1000 Then
            '    Unidadeschar = "0" + (UnidEmbalaje).ToString()
            'Else
            '    Unidadeschar = (UnidEmbalaje).ToString()
            'End If

            If UnidEmbalaje < 10000 And UnidEmbalaje > 999 Then
                Unidadeschar = "0" + UnidEmbalaje.ToString()
            End If
            If UnidEmbalaje < 1000 And UnidEmbalaje > 99 Then
                Unidadeschar = "00" + UnidEmbalaje.ToString()
            End If
            If UnidEmbalaje < 100 And UnidEmbalaje > 9 Then
                Unidadeschar = "000" + UnidEmbalaje.ToString()
            End If
            If UnidEmbalaje < 10 Then
                Unidadeschar = "0000" + UnidEmbalaje.ToString()
            End If
            If UnidEmbalaje > 999 Then
                Unidadeschar = UnidEmbalaje.ToString()
            End If

            Dim Codigochar As String
            If CodProd.Length > 5 Then
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

            Dim Num As Decimal = Format(Kilos, "0.000")
            Dim NumString As String = Num.ToString()
            Dim textSplit() As String = NumString.Split(",")
            Dim entero As String = textSplit(0)
            Dim Deci As String = textSplit(1)

            If entero < 10 Then
                entero = "000" + entero.ToString()
            ElseIf entero < 100 Then
                entero = "00" + entero.ToString()
            ElseIf entero > 100 & entero < 1000 Then
                entero = "0" + entero.ToString()
            ElseIf entero > 1000 & entero < 10000 Then
                entero = entero.ToString()
            End If

            If Deci > 100 Then
                Deci = Deci.ToString().Substring(0, 2) + "0"
            ElseIf Deci > 10 Then
                Deci = Deci.ToString() + "00"
            Else
                Deci = Deci.ToString() + "000"
            End If
            Dim strCantidad As String

            If Me.cantidad > 10 Then
                strCantidad = Me.cantidad.ToString()
            Else
                strCantidad = "0" + Me.cantidad.ToString()
            End If

            Dim strEm As String = Me.embalajeNombre.ToUpper
            Dim emQR As String = ""

            Select Case strEm
                Case "CAJA"
                    emQR = "CJ"
                Case "CANASTA"
                    emQR = "CAN"
                Case "BULTO"
                    emQR = "BTO"
                Case "CANASTILLA"
                    emQR = "CLL"
                Case "SACO"
                    emQR = "BTO"
                Case Else
                    emQR = strEm
            End Select


            Dim qr As New QRCodeGenerator
            Dim Data = qr.CreateQrCode(Codigochar.ToString() + "" + Unidadeschar.Substring(0, 5) + "" + entero.ToString().Substring(0, 4) + "" + Deci.ToString().Substring(0, 3) + "" + Me.Lote.ToString() + strCantidad + emQR, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(Data)
            Dim codigo As Image = code.GetGraphic(3)
            Dim srcRect As New RectangleF(1.5, 1.5, 100, 100)
            Dim units As New GraphicsUnit
            e.Graphics.DrawImage(codigo, 55, 25, srcRect, units.Pixel)





            e.HasMorePages = False
        Catch ex As Exception
            Console.WriteLine("Error al imprimir :" + ex.Message)
        End Try

    End Sub

    Public Shared Function Ean128(ByVal strOriginal As String) As String
        'Return : * a string which give the bar code when it is dispayed with CODE128.TTF font
        '         * an empty string if the supplied parameter is no good
        Dim i, mini, dummy As Integer
        Dim checksum As Long
        Dim tableB As Boolean
        Dim strFinal As String

        strFinal = ""
        If Len(strOriginal) > 0 Then
            'Check for valid characters
            For i = 1 To Len(strOriginal)
                Select Case Asc(Mid(strOriginal, i, 1))
                    Case 32 To 126, 203, 207
                    Case Else
                        i = 0
                        Exit For
                End Select
            Next
            'Calculation of the code string with optimized use of tables B and C
            strFinal = ""
            tableB = True
            If i > 0 Then
                i = 1 ' i become the string index
                Do While i <= Len(strOriginal)
                    If tableB Then
                        'See if interesting to switch to table C
                        'yes for 4 digits at start or end, else if 6 digits
                        mini = IIf(i = 1 Or i + 3 = Len(strOriginal), 4, 6)
                        '*************GOSUB TestNumOrFnc1:
                        'if the mini characters from i are numeric or Fnc1, then mini=0
                        mini = mini - 1
                        If i + mini <= Len(strOriginal) Then
                            Do While mini >= 0
                                If (Asc(Mid(strOriginal, i + mini, 1)) < 48 Or Asc(Mid(strOriginal, i + mini, 1)) > 57) And Asc(Mid(strOriginal, i + mini, 1)) <> 207 Then Exit Do
                                mini = mini - 1
                            Loop
                        End If
                        '**************
                        If mini < 0 Then 'Choice of table C
                            If i = 1 Then 'Starting with table C
                                strFinal = Chr(210)
                            Else 'Switch to table C
                                strFinal = strFinal & Chr(204)
                            End If
                            tableB = False
                        Else
                            If i = 1 Then strFinal = Chr(209) 'Starting with table B
                        End If
                    End If
                    If Not tableB Then
                        'We are on table C, try to process 2 digits or Ê
                        If Asc(Mid(strOriginal, i, 2)) = 207 Then
                            'We process the Fnc1 (Ê)
                            strFinal = strFinal & Mid(strOriginal, i, 1)
                            i = i + 1
                        Else
                            mini = 2
                            '****************GoSub TestNum:
                            'if the mini characters from i are numeric, then mini=0 
                            mini = mini - 1
                            If i + mini <= Len(strOriginal) Then
                                Do While mini >= 0
                                    If Asc(Mid(strOriginal, i + mini, 1)) < 48 Or Asc(Mid(strOriginal, i + mini, 1)) > 57 Then Exit Do
                                    mini = mini - 1
                                Loop
                            End If
                            '****************
                            If mini < 0 Then 'OK for 2 digits, process it
                                dummy = Val(Mid(strOriginal, i, 2))
                                dummy = IIf(dummy < 95, dummy + 32, dummy + 105)
                                strFinal = strFinal & Chr(dummy)
                                i = i + 2
                            Else 'We haven't 2 digits, switch to table B
                                strFinal = strFinal & Chr(205)
                                tableB = True
                            End If
                        End If
                    End If
                    If tableB Then
                        'Process 1 digit with table B
                        strFinal = strFinal & Mid(strOriginal, i, 1)
                        i = i + 1
                    End If
                Loop
                'Calculation of the checksum
                For i = 1 To Len(strFinal)
                    dummy = Asc(Mid(strFinal, i, 1))
                    dummy = IIf(dummy < 127, dummy - 32, dummy - 105)
                    If i = 1 Then checksum = dummy
                    checksum = (checksum + (i - 1) * dummy) Mod 103
                Next
                'Calculation of the checksum ASCII code
                checksum = IIf(checksum < 95, checksum + 32, checksum + 105)
                'Add the checksum and the STOP
                strFinal = strFinal & Chr(checksum) & Chr(211)
            End If
        End If

        Return strFinal

    End Function


    'Private Sub CedulaCons_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles CedulaCons.PrintPage
    '    Dim Fuente As Font
    '    Dim Texto As String

    '    e.Graphics.PageUnit = GraphicsUnit.Millimeter
    '    Fuente = New Font("Arial", 12)
    '    e.Graphics.DrawString("CONSERVACION", Fuente, Brushes.Black, 8, 6)
    '    e.Graphics.DrawString("NIVEL     POSICION", Fuente, Brushes.Black, 18, 11)

    '    Fuente = New Font("Arial", 48)
    '    e.Graphics.DrawString(NumNivel.Text + "-" + NumPosicion.Text, Fuente, Brushes.Black, 18, 13)

    '    Fuente = New Font("Arial", 10)
    '    e.Graphics.DrawString("FECHA:   " + txtFechaHora.Text, Fuente, Brushes.Black, 8, 29)
    '    e.Graphics.DrawString("CLIENTE: " + NomCliente.Text, Fuente, Brushes.Black, 8, 33)

    '    Fuente = New Font("Arial", 13, FontStyle.Bold)
    '    e.Graphics.DrawString("COD. PRODUCTO: " + TxtCodPto.Text, Fuente, Brushes.Black, 8, 38)
    '    e.Graphics.DrawString(TxtProducto.Text, Fuente, Brushes.Black, 8, 43)

    '    Texto = "EMBALAJE: " & Embalajes.Text & " X " & TxtUnidades.Text & " Unidades"
    '    Fuente = New Font("Arial", 11)
    '    e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 8, 48)

    '    Fuente = New Font("Arial", 13)

    '    If CmbCantPesaje.SelectedIndex = 2 Then
    '        e.Graphics.DrawString("CANTIDAD:    " + TxtTcant.Text + " " + Embalajes.Text + "(S)", Fuente, Brushes.Black, 8, 53)
    '    Else
    '        e.Graphics.DrawString("CANTIDAD:    " + TxtCantidades.Text + " " + Embalajes.Text + "(S)", Fuente, Brushes.Black, 8, 53)
    '    End If

    '    Fuente = New Font("Arial", 13, FontStyle.Bold)

    '    e.Graphics.DrawString("PESO:    " + TxtTkilos.Text + " Kgrs.", Fuente, Brushes.Black, 8, 58)

    '    If Not FechaVence.Checked Then
    '        e.Graphics.DrawString("F.VENCE: NULO", Fuente, Brushes.Black, 8, 63)
    '    Else
    '        e.Graphics.DrawString("F.VENCE: " + FechaVence.Value.ToString("dd/MMM/yyyy").ToUpper, Fuente, Brushes.Black, 8, 63)
    '    End If

    '    Fuente = New Font("Arial", 13)

    '    e.Graphics.DrawString("LOTE:    " + TxtLote.Text, Fuente, Brushes.Black, 8, 68)

    '    Fuente = New Font("Code 128", 40)
    '    e.Graphics.DrawString(eanEntrItem, Fuente, Brushes.Black, 8, 73)

    '    Fuente = New Font("Arial", 9)
    '    e.Graphics.DrawString(EntrItem, Fuente, Brushes.Black, 64, 77)

    '    Fuente = New Font("Arial", 8)
    '    Texto = "Entr.Conser # " & NumDoc & " Item # " & IdItem & " Usuario: " & UsuarioActual
    '    e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 8, 87)

    '    e.HasMorePages = False

    'End Sub

End Class
