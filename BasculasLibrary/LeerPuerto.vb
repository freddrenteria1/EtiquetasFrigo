Imports System.Drawing


Public Class LeerPuerto

    Friend WithEvents ps As New System.IO.Ports.SerialPort


    Public PuertoSerieBasculas As String = "COM1"

    Public Sub New()

        ps.PortName = PuertoSerieBasculas

    End Sub

    Public Function AbrirPuerto() As Double


        If Not ps.IsOpen Then
            Try
                ps.Open()
            Catch ex As Exception

            End Try


        End If


        System.Threading.Thread.Sleep(100)
        Return 0

        'ps.Open()
    End Function


    Public Sub CerrarPuerto()

        If ps.IsOpen Then
            Try
                ps.Close()
            Catch ex As Exception

            End Try

        End If


    End Sub


    Dim peso As Double
    Public Function leerDatos() As Double

        Dim p As Double = AbrirPuerto()



        Return peso
    End Function

    Private Sub PuertoSerie_DataReceived(sender As System.Object, e As System.IO.Ports.SerialDataReceivedEventArgs) Handles ps.DataReceived
        Dim i, Pos As Short
        Dim StrBascula As String

        StrBascula = ""
        Try
            StrBascula = ps.ReadLine
        Catch ex As Exception
            'Fallo de lectura
        End Try

        'Si las basculas envian dato menor a 4 bytes...no es el peso y se debe omitir
        Try
            If StrBascula.Length > 4 Then
                Pos = -1
                For i = 0 To Len(StrBascula) - 1
                    If IsNumeric(StrBascula(i)) Then
                        Pos = i
                        Exit For
                    End If
                Next
                If Pos >= 0 Then
                    peso = Val(StrBascula.Substring(Pos))
                End If

            End If
        Catch ex As Exception
            'Fallo de acceso ilegal txtpeso por Hilo diferente
        End Try
    End Sub


End Class
