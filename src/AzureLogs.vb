Imports System
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Configuration
Imports System.Data.SqlClient

Module AzureLogs

    Sub Main()
        'Reads the content in a DataSet
        Dim elDataSet As New DataSet
        elDataSet.ReadXml(DownloadXML(ConfigurationManager.AppSettings("ftpPath").ToString,
                                      ConfigurationManager.AppSettings("userName").ToString,
                                      ConfigurationManager.AppSettings("password").ToString))

        'This counters are for the below
        Dim contadorPrimario As Integer = 0, contadorSecundario As Integer = 1

        'Variables to store
        'Id is to check if the data was stored before
        'fullDate is when the error occurs
        'Name is the "error"
        'Description is the description of the error
        'Place is where it happened
        Dim id As Integer = Nothing, fullDate As String = Nothing, name As String = Nothing,
            description As String = Nothing, place As String = Nothing

        'Loop through the table data
        'I make 2 different counters, 1 is checking the EventData_Id
        'And the another one is checking the place we are
        For Each row As DataRow In elDataSet.Tables("Data").Rows
            If row("EventData_Id") = contadorPrimario Then
                Select Case contadorSecundario
                    Case 3
                        fullDate = row("Data_Text")
                    Case 18
                        name = row("Data_Text")
                    Case 19
                        description = row("Data_Text")
                    Case 21
                        id = contadorPrimario
                        place = row("Data_Text")
                End Select
                contadorSecundario += 1
            Else
                contadorPrimario += 1
                contadorSecundario = 2
                'Here we call the function to store the data
                SaveLog(id, fullDate, name, description, place)
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

    Function SaveLog(ByVal id As Integer, ByVal fullDate As String, ByVal name As String, ByVal description As String, ByVal place As String)
        Using con As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            con.Open()
            Using cmd As New SqlCommand
                With cmd
                    .Connection = con
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "spInsertErrorLog"
                    .Parameters.Add("@Id", SqlDbType.Int).Value = id
                    .Parameters.Add("@Date", SqlDbType.DateTime).Value = fullDate
                    .Parameters.Add("@Name", SqlDbType.NVarChar).Value = name
                    .Parameters.Add("@Description", SqlDbType.Text).Value = description
                    .Parameters.Add("@Place", SqlDbType.NVarChar).Value = place
                    .ExecuteNonQuery()
                End With
            End Using
            con.Close()
        End Using

        'It will be weird if it fails, so we return true
        Return True
    End Function

End Module
