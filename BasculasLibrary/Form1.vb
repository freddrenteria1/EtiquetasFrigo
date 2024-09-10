Imports System.Data.SqlClient
Imports System.Math
Imports System.Drawing.Image
Imports System.Drawing


Public Class Form1

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

    Public Sub New(servicios As String, posicion As String, fecha As String, nombreCliente As String, codProd As String,
                   nomProd As String, enbalajeNombre As String, UnidEmbalaje As Double, cantidad As Double, kilos As Double, fechaVen As String,
                   lote As String, noDocumento As Int64, noItem As Integer, UsuarioNombre As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CedulaCons_PrintPage(sender As Object, e As Drawing.Printing.PrintPageEventArgs) Handles CedulaCons.PrintPage
        Dim Fuente As Font
        Dim Texto As String

        e.Graphics.PageUnit = GraphicsUnit.Millimeter
        Fuente = New Font("Arial", 12)
        e.Graphics.DrawString(Me.servicio, Fuente, Brushes.Black, 8, 6)
        e.Graphics.DrawString("NIVEL     POSICION", Fuente, Brushes.Black, 18, 11)

        Fuente = New Font("Arial", 48)
        e.Graphics.DrawString(PosicionString, Fuente, Brushes.Black, 18, 13)

        Fuente = New Font("Arial", 10)
        e.Graphics.DrawString("FECHA:   " + Me.Fecha, Fuente, Brushes.Black, 8, 29)
        e.Graphics.DrawString("CLIENTE: " + Me.NombreCliente, Fuente, Brushes.Black, 8, 33)

        Fuente = New Font("Arial", 13, FontStyle.Bold)
        e.Graphics.DrawString("COD. PRODUCTO: " + Me.CodProd, Fuente, Brushes.Black, 8, 38)
        e.Graphics.DrawString(NombProd, Fuente, Brushes.Black, 8, 43)

        Texto = "EMBALAJE: " & Me.embalajeNombre & " X " & UnidEmbalaje & " Unidades"
        Fuente = New Font("Arial", 11)
        e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 8, 48)

        Fuente = New Font("Arial", 13)

        e.Graphics.DrawString("CANTIDAD:    " + Me.cantidad.ToString + " " + Me.embalajeNombre + "(S)", Fuente, Brushes.Black, 8, 53)
        ''Se quita se cree que uno es para uno a uno y el otro es la impresion final se deja uno solo como parametro
        'If CmbCantPesaje.SelectedIndex = 2 Then
        '    e.Graphics.DrawString("CANTIDAD:    " + TxtTcant.Text + " " + Embalajes.Text + "(S)", Fuente, Brushes.Black, 8, 53)
        'Else
        '    e.Graphics.DrawString("CANTIDAD:    " + TxtCantidades.Text + " " + Embalajes.Text + "(S)", Fuente, Brushes.Black, 8, 53)
        'End If


        Fuente = New Font("Arial", 13, FontStyle.Bold)

        e.Graphics.DrawString("PESO:    " + Kilos.ToString() + " Kgrs.", Fuente, Brushes.Black, 8, 58)

        'If Not FechaVence.Checked Then
        '    e.Graphics.DrawString("F.VENCE: NULO", Fuente, Brushes.Black, 8, 63)
        'Else
        '    e.Graphics.DrawString("F.VENCE: " + FechaVence.Value.ToString("dd/MMM/yyyy").ToUpper, Fuente, Brushes.Black, 8, 63)
        'End If
        e.Graphics.DrawString("F.VENCE: " + Me.FechaVen.ToString("dd/MMM/yyyy").ToUpper, Fuente, Brushes.Black, 8, 63)

        Fuente = New Font("Arial", 13)

        e.Graphics.DrawString("LOTE:    " + Me.Lote, Fuente, Brushes.Black, 8, 68)
        Dim eanEntrItem As String = Ean128(Me.NoItem.ToString())

        Fuente = New Font("Code 128", 40)
        e.Graphics.DrawString(eanEntrItem, Fuente, Brushes.Black, 8, 73)

        Fuente = New Font("Arial", 9)
        e.Graphics.DrawString(Me.NoItem, Fuente, Brushes.Black, 64, 77)

        Fuente = New Font("Arial", 8)
        Texto = Me.servicio + " # " & Me.NoDocumento.ToString & " Item # " & Me.NoItem.ToString & " Usuario: " & Me.UsuarioNombre
        e.Graphics.DrawString(Texto, Fuente, Brushes.Black, 8, 87)

        e.HasMorePages = False
    End Sub

    Private Function Ean128(ByVal strOriginal As String) As String
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


End Class