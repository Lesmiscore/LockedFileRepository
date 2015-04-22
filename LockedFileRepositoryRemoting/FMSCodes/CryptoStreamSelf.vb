Imports System.IO
Imports System.Security.Cryptography

Public Class CryptoStreamSelf
    Inherits Stream
    Protected cs As CryptoStream, fs As FileStream, ict As ICryptoTransform

    Public Sub New(cs As CryptoStream, fs As FileStream, ict As ICryptoTransform)
        Me.cs = cs
        Me.fs = fs
        Me.ict = ict
    End Sub

    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return cs.CanRead
        End Get
    End Property

    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return cs.CanSeek
        End Get
    End Property

    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return cs.CanWrite
        End Get
    End Property

    Public Overrides Sub Flush()
        cs.Flush()
        fs.Flush()
    End Sub

    Public Overrides ReadOnly Property Length As Long
        Get
            Return cs.Length
        End Get
    End Property

    Public Overrides Property Position As Long
        Get
            Return cs.Position
        End Get
        Set(value As Long)
            cs.Position = value
        End Set
    End Property

    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Return cs.Read(buffer, offset, count)
    End Function

    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return cs.Seek(offset, origin)
    End Function

    Public Overrides Sub SetLength(value As Long)
        cs.SetLength(value)
    End Sub

    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        cs.Write(buffer, offset, count)
    End Sub

    Public Overrides Sub Close()
        MyBase.Close()
        cs.Close()
        fs.Close()
    End Sub

    Protected Shadows Sub Dispose()
        MyBase.Dispose()
        cs.Dispose()
        fs.Dispose()
        ict.Dispose()
    End Sub

    Public ReadOnly Property GetFileStream As FileStream
        Get
            Return fs
        End Get
    End Property

    Public ReadOnly Property GetCryptoStream As CryptoStream
        Get
            Return cs
        End Get
    End Property

    Public ReadOnly Property GetICryptoTransform As ICryptoTransform
        Get
            Return ict
        End Get
    End Property
End Class
