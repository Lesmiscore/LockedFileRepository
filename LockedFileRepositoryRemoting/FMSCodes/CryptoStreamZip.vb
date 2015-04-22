Imports System.IO
Imports System.Security.Cryptography
Imports ICSharpCode.SharpZipLib.Zip

Public Class CryptoStreamZip
    Inherits CryptoStreamSelf
    Dim zs As Stream

    Public Sub New(cs As CryptoStream, fs As FileStream, ict As ICryptoTransform, zs As ZipInputStream)
        MyBase.New(cs, fs, ict)
        Me.zs = zs
    End Sub

    Public Sub New(cs As CryptoStream, fs As FileStream, ict As ICryptoTransform, zs As ZipOutputStream)
        MyBase.New(cs, fs, ict)
        Me.zs = zs
    End Sub

    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return cs.CanRead And zs.CanRead And fs.CanRead
        End Get
    End Property

    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return cs.CanSeek And zs.CanSeek And zs.CanSeek
        End Get
    End Property

    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return cs.CanWrite And zs.CanWrite And zs.CanWrite
        End Get
    End Property

    Public Overrides Sub Flush()
        cs.Flush()
        fs.Flush()
        zs.Flush()
    End Sub

    Public Overrides ReadOnly Property Length As Long
        Get
            Return zs.Length
        End Get
    End Property

    Public Overrides Property Position As Long
        Get
            Return zs.Position
        End Get
        Set(value As Long)
            zs.Position = value
        End Set
    End Property

    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Return zs.Read(buffer, offset, count)
    End Function

    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return zs.Seek(offset, origin)
    End Function

    Public Overrides Sub SetLength(value As Long)
        zs.SetLength(value)
    End Sub

    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        zs.Write(buffer, offset, count)
    End Sub

    Public Overrides Sub Close()
        MyBase.Close()
        zs.Close()
        cs.Close()
        fs.Close()
    End Sub

    Protected Shadows Sub Dispose()
        MyBase.Dispose()
        zs.Dispose()
        cs.Dispose()
        fs.Dispose()
        ict.Dispose()
    End Sub

    Public ReadOnly Property GetZipInputStream As ZipInputStream
        Get
            Return zs
        End Get
    End Property
    Public ReadOnly Property GetZipOutputStream As ZipOutputStream
        Get
            Return zs
        End Get
    End Property
End Class
