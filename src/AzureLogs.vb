Imports System
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Configuration

Module AzureLogs

    Sub Main()
        'Reads the content in a DataSet
        Dim elDataSet As New DataSet
        elDataSet.ReadXml(DownloadXML(ConfigurationManager.AppSettings("ftpPath").ToString, ConfigurationManager.AppSettings("userName").ToString, ConfigurationManager.AppSettings("password").ToString))

        'This counters are for the below
        Dim contadorPrimario As Integer = 0, contadorSecundario As Integer = 1

        'Loop through the table data
        'I make 2 different counters, 1 is checking the EventData_Id
        'And the another one is checking the place we are
        For Each row As DataRow In elDataSet.Tables("Data").Rows
            If row("EventData_Id") = contadorPrimario Then
                Select Case contadorSecundario
                    Case 3
                        'Dim fecha As String = row("Data_Text")
                        Console.WriteLine("Fecha: " & row("Data_Text"))
                    Case 18
                        'Dim nombre As String = row("Data_Text")
                        Console.WriteLine("Nombre: " & row("Data_Text"))
                    Case 19
                        'Dim descripcion As String = row("Data_Text")
                        Console.WriteLine("Descripción: " & row("Data_Text"))
                    Case 21
                        'Dim lugar As String = row("Data_Text")
                        Console.WriteLine("Lugar: " & row("Data_Text"))

                End Select
                contadorSecundario += 1
            Else
                contadorPrimario += 1
                contadorSecundario = 2
            End If
        Next

        Console.ReadKey()
    End Sub

    Private Function DownloadXML(ByVal ftpPath As String, ByVal userName As String, ByVal password As String) As StreamReader
        Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpPath), FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.DownloadFile
        request.KeepAlive = True
        request.UsePassive = True
        request.UseBinary = True
        request.Credentials = New NetworkCredential(userName, password)

        Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
        Dim responseStream As Stream = response.GetResponseStream
        Dim reader As New StreamReader(responseStream)

        Return reader
    End Function

    Function SaveLog(ByVal fullDate As String, ByVal name As String, ByVal description As String, ByVal place As String)

    End Function

End Module
