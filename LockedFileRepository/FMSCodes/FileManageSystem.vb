Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml

Public Class FileManageSystem
    Public Const ProfileVersion As Integer = 2
    'passwordの仮パスワード"#XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=-CURjTXpvD+o/UMss2LDKTQ==#"
    Public Shared ReadOnly SubPWRegexText As String = New StringBuilder().
        Append("^").
        Append(Regex.Escape("#")).
        Append("[a-zA-Z0-9").
        Append(Regex.Escape("=/+")).
        Append("]{44}").
        Append(Regex.Escape("-")).
        Append("[a-zA-Z0-9").
        Append(Regex.Escape("=/+")).
        Append("]{24}").
        Append(Regex.Escape("#")).
        Append("$").
        ToString '仮パスワードの正規表現文字列
    Public Shared ReadOnly SubPWRegex As New Regex(SubPWRegexText,
        RegexOptions.Compiled Or RegexOptions.Singleline)
    Dim config, pp, secretFolder As String
    Dim xd As XDocument
    Dim pass As String
    Dim gfResult As CryptoStreamSelf = Nothing
    Public Shared Instance As FileManageSystem
    Public Sub New(password As String, Optional profilePath As String = Nothing)
        If Instance IsNot Nothing Then
            Throw New ArgumentException("インスタンスはすでに生成されています")
        End If
        If profilePath = Nothing Then
            profilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly.Location)
        End If
        pp = profilePath
        pass = password
        config = Path.Combine(pp, "FLR.config")
        secretFolder = Path.Combine(pp, "Secret")
        MakeFolder(secretFolder)
        If File.Exists(config) Then
            Using cryptStrm = OpenFileDecrypt(config)
                Using reader = New StreamReader(cryptStrm)
                    xd = XDocument.Load(reader)
                End Using
            End Using
            If GetProfileVersion() < ProfileVersion Then
                ProgressState.ShowDialog(ProgressState.ProgressMode.Upgrade, Nothing)
            End If
        Else
            If UsingSubPassword Then
                Throw New ArgumentException("初期生成に仮パスワードは使用できません。", "password")
            End If
            xd = XDocument.Parse("<LockedFileRepository Version=""2""></LockedFileRepository>")
            Commit()
        End If
        Instance = Me
    End Sub
    Public ReadOnly Property ProfilePath As String
        Get
            Return pp
        End Get
    End Property
    Public ReadOnly Property SecretFilesFolder As String
        Get
            Return secretFolder
        End Get
    End Property
    Public ReadOnly Property GetXDocument As XDocument
        Get
            Return xd
        End Get
    End Property
    Public Shared Function ProfileAvaliable(Optional profilePath As String = Nothing) As Boolean
        If profilePath = Nothing Then
            profilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly.Location)
        End If
        Return File.Exists(Path.Combine(profilePath, "FLR.config"))
    End Function
    Public Sub Commit()
        Using css = OpenFileEncrypt(config)
            Using tw = New StreamWriter(css, New UTF8Encoding)
                Using xw As XmlWriter = XmlWriter.Create(tw)
                    xd.Save(xw)
                End Using
            End Using
        End Using
    End Sub
    Public Function GetProfileVersion() As Integer
        Try
            Dim t = xd.Root.@Version
            If t = "" Or t = Nothing Then
                Return 1
            Else
                Return Integer.Parse(t)
            End If
        Catch ex As Exception
            Return 1
        End Try
    End Function
    Public Function AddFile(path As String) As CryptoStreamSelf
        Return AddFile(path, UsingZipEnum.False)
    End Function
    'Public Sub CleanUpMemory()
    '    dp.Dispose()
    'End Sub
    Private Function EncodeStringSafety(s As String) As String
        Return Convert.ToBase64String(New UTF8Encoding().GetBytes(s))
    End Function
    Private Function DecodeStringSafety(s As String) As String
        Return New UTF8Encoding().GetString(Convert.FromBase64String(s))
    End Function
    Public Function ChangeName(name As String, dest As String) As Boolean
        If Not ExistsFile(name) Then
            Return False
        End If
        For Each i In xd...<FileEntry>
            If i.<VirtualFile>.Value = name Then
                i.<VirtualFile>.Value = dest
                Return True
            End If
        Next
        Return False
    End Function
    Public Function ExistsFile(path As String) As Boolean
        Return GetFiles.Contains(path)
    End Function
    Public Function GetRealFile(path As String) As String
        If Not ExistsFile(path) Then
            Return Nothing
        End If
        For Each i In xd...<FileEntry>
            If i.<VirtualFile>.Value = path Then
                Return i.<RealFile>.Value
            End If
        Next
        Return Nothing
    End Function
    Public Function GetFile(path As String, isEncrypt As Boolean) As CryptoStreamSelf
        gfResult = GetFileInternal(path, isEncrypt)
        Return gfResult
    End Function
    Public Function GetFile(path As String, isEncrypt As Boolean, closeCurrentFile As Boolean) As CryptoStreamSelf
        If closeCurrentFile Then
            CloseLatestGetFile()
        End If
        Return GetFile(path, isEncrypt)
    End Function
    Public Function CloseLatestGetFile() As Boolean
        If gfResult Is Nothing Then
            Return False
        End If
        Try
            gfResult.Close()
            gfResult.Dispose()
            Return True
        Catch ex As Exception
            AnythingTools.Tools.PrintExceptionD(ex)
            Return False
        End Try
        gfResult = Nothing
    End Function
    Public ReadOnly Property IsLatestGetFileOpening As Boolean
        Get
            If gfResult Is Nothing Then
                Return False
            End If
            Return gfResult.CanRead Or gfResult.CanWrite
        End Get
    End Property
    Private Function GetFileInternal(path As String, isEncrypt As Boolean) As CryptoStreamSelf
        If Not ExistsFile(path) Then
            Return Nothing
        Else
            Select Case GetUsingMode(path)
                Case UsingZipEnum.True
                    If isEncrypt Then
                        Return OpenFileEncryptZip(IO.Path.Combine(secretFolder, GetRealFile(path)))
                    End If
                    Return OpenFileDecryptZip(IO.Path.Combine(secretFolder, GetRealFile(path)))
                Case UsingZipEnum.False
                    If isEncrypt Then
                        Return OpenFileEncrypt(IO.Path.Combine(secretFolder, GetRealFile(path)))
                    End If
                    Return OpenFileDecrypt(IO.Path.Combine(secretFolder, GetRealFile(path)))
                Case UsingZipEnum.SuperCompression
                    If isEncrypt Then
                        Return OpenFileEncryptSC(IO.Path.Combine(secretFolder, GetRealFile(path)))
                    End If
                    Return OpenFileDecryptSC(IO.Path.Combine(secretFolder, GetRealFile(path)))
                Case Else
                    Return Nothing
            End Select
        End If
    End Function
    Public Sub DeleteFile(path As String)
        If Not ExistsFile(path) Then
            Return
        End If
        For Each i In xd...<FileEntry>.ToArray
            If i.<VirtualFile>.Value = path Then
                File.Delete(IO.Path.Combine(secretFolder, i.<RealFile>.Value))
                i.Remove()
            End If
        Next
    End Sub
    Public Function GetUsingZip(path As String) As Boolean
        If Not ExistsFile(path) Then
            Return False
        End If
        For Each i In xd...<FileEntry>.ToArray
            If i.<VirtualFile>.Value = path Then
                Return [Enum].Parse(GetType(UsingZipEnum), i.<UsingZip>.Value) = UsingZipEnum.True
            End If
        Next
        Return False
    End Function
    Public Function GetUsingMode(path As String) As UsingZipEnum
        If Not ExistsFile(path) Then
            Return False
        End If
        For Each i In xd...<FileEntry>.ToArray
            If i.<VirtualFile>.Value = path Then
                Return [Enum].Parse(GetType(UsingZipEnum), i.<UsingZip>.Value)
            End If
        Next
        Return False
    End Function
    Public Function GetUsingSC(path As String) As Boolean
        If Not ExistsFile(path) Then
            Return False
        End If
        For Each i In xd...<FileEntry>.ToArray
            If i.<VirtualFile>.Value = path Then
                Return [Enum].Parse(GetType(UsingZipEnum), i.<UsingZip>.Value) = UsingZipEnum.SuperCompression
            End If
        Next
        Return False
    End Function
    Public Function GetFiles() As String()
        Dim l As New List(Of String)
        For Each i In xd...<FileEntry>.ToArray
            l.Add(i.<VirtualFile>.Value)
        Next
        Return l.ToArray
    End Function
    Public Enum UsingZipEnum
        [True]
        [False]
        SuperCompression
    End Enum
    Private Shared ReadOnly passwordChars As String = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Private Function GenarateRandomString() As String
        Dim length = 30
        Dim sb As New System.Text.StringBuilder(length)
        Dim random() As Byte = New Byte(length - 1) {}
        'RNGCryptoServiceProviderクラスのインスタンスを作成
        Dim rng As New System.Security.Cryptography.RNGCryptoServiceProvider()
        rng.GetNonZeroBytes(random)
        For Each i In random
            sb.Append(passwordChars(i Mod passwordChars.Length))
        Next
        Return sb.ToString()
    End Function
    Public ReadOnly Property UnlockPassword As String
        Get '仮パスワードの生成コード
            If UsingSubPassword Then
                Return Nothing
            End If
            Return New StringBuilder().
                Append("#").
                Append(Convert.ToBase64String(GenerateKeyPrivate)).
                Append("-").
                Append(Convert.ToBase64String(GenerateIVPrivate)).
                Append("#").ToString()
        End Get
    End Property
    Public ReadOnly Property UsingSubPassword As Boolean
        Get
            Return SubPWRegex.IsMatch(pass)
        End Get
    End Property
    Private Function GenerateKeyPrivate() As Byte()
        If UsingSubPassword Then
            Dim b64 = pass.Substring(1, 44)
            Return Convert.FromBase64String(b64)
        End If
        Dim sha256 As New System.Security.Cryptography.SHA256CryptoServiceProvider()
        Dim bs As Byte() = sha256.ComputeHash(New UTF8Encoding().GetBytes(pass))
        sha256.Clear()
        Return bs
    End Function
    Private Function GenerateIVPrivate() As Byte()
        If UsingSubPassword Then
            Dim b64 = pass.Substring(1 + 44 + 1, 24)
            Return Convert.FromBase64String(b64)
        End If
        Dim sha256 As New System.Security.Cryptography.SHA256CryptoServiceProvider()
        Dim bs As Byte() = sha256.ComputeHash(New UTF8Encoding().GetBytes(pass.Reverse.ToArray))
        sha256.Clear()
        ReDim Preserve bs(15)
        Return bs.Reverse.ToArray
    End Function
    Private Function GenerateKeyPrivate(s As String) As Byte()
        If SubPWRegex.IsMatch(s) Then
            Dim b64 = pass.Substring(1, 44)
            Return Convert.FromBase64String(b64)
        End If
        Dim sha256 As New System.Security.Cryptography.SHA256CryptoServiceProvider()
        Dim bs As Byte() = sha256.ComputeHash(New UTF8Encoding().GetBytes(s))
        sha256.Clear()
        Return bs
    End Function
    Private Function GenerateIVPrivate(s As String) As Byte()
        If SubPWRegex.IsMatch(s) Then
            Dim b64 = pass.Substring(1 + 44 + 1, 24)
            Return Convert.FromBase64String(b64)
        End If
        Dim sha256 As New System.Security.Cryptography.SHA256CryptoServiceProvider()
        Dim bs As Byte() = sha256.ComputeHash(New UTF8Encoding().GetBytes(s.Reverse.ToArray))
        sha256.Clear()
        ReDim Preserve bs(15)
        Return bs.Reverse.ToArray
    End Function
    Public Function CheckPassword(s As String) As Boolean
        If Not Tools.IsArraySame(GenerateKeyPrivate(), GenerateKeyPrivate(s)) Then
            Return False
        End If
        If Not Tools.IsArraySame(GenerateIVPrivate(), GenerateIVPrivate(s)) Then
            Return False
        End If
        Return True
    End Function
    Public Function OpenFileEncrypt(path As String) As CryptoStreamSelf
        Try
            'RijndaelManagedオブジェクトを作成
            Dim rijndael As New System.Security.Cryptography.RijndaelManaged()

            '設定を変更するときは、変更する
            'rijndael.KeySize = 256
            'rijndael.BlockSize = 128
            'rijndael.FeedbackSize = 128
            'rijndael.Mode = System.Security.Cryptography.CipherMode.CBC
            'rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7

            '共有キーと初期化ベクタを作成
            'Key、IVプロパティがnullの時に呼びだすと、自動的に作成される
            '自分で作成するときは、GenerateKey、GenerateIVメソッドを使う
            rijndael.Key = GenerateKeyPrivate()
            rijndael.IV = GenerateIVPrivate()

            Dim encryptor As System.Security.Cryptography.ICryptoTransform = _
              rijndael.CreateEncryptor()
            '暗号化されたデータを書き出すためのCryptoStreamの作成
            Dim fileStream As New FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read)
            Dim cryptStrm As New System.Security.Cryptography.CryptoStream( _
                fileStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write)

            Return New CryptoStreamSelf(cryptStrm, fileStream, encryptor)
        Catch ex As Exception
            Tools.PrintException(ex)
            Return Nothing
        End Try
    End Function
    Public Function OpenFileDecrypt(path As String) As CryptoStreamSelf
        Try
            'RijndaelManagedオブジェクトの作成
            Dim rijndael As New System.Security.Cryptography.RijndaelManaged()

            '共有キーと初期化ベクタを設定
            rijndael.Key = GenerateKeyPrivate()
            rijndael.IV = GenerateIVPrivate()

            '対称復号化オブジェクトの作成
            Dim decryptor As System.Security.Cryptography.ICryptoTransform = _
                rijndael.CreateDecryptor()
            '暗号化されたデータを読み込むためのCryptoStreamの作成
            Dim fileStream As New FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read)
            Dim cryptStrm As New System.Security.Cryptography.CryptoStream( _
               fileStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read)

            Return New CryptoStreamSelf(cryptStrm, fileStream, decryptor)
        Catch ex As Exception
            Tools.PrintException(ex)
            Return Nothing
        End Try
    End Function
    Public Function OpenFileReadRaw(s As String) As FileStream
        Try
            Return New FileStream(s, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function OpenFileWriteRaw(s As String) As FileStream
        Try
            Return New FileStream(s, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub MakeFolder(path As String)
        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If
    End Sub
End Class
