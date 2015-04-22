Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip

Partial Public Class FileManageSystem
    Public Function AddFile(path As String, useZip As Boolean) As CryptoStreamSelf
        If ExistsFile(path) Then
            Return GetFile(path, True)
        End If
        Dim data = <FileEntry></FileEntry>
        Dim rf = <RealFile></RealFile>
        rf.Value = GenarateRandomString()
        Dim vf = <VirtualFile></VirtualFile>
        vf.Value = path
        Dim zi = <UsingZip></UsingZip>
        zi.Value = useZip
        data.Add(rf, vf, zi)
        xd.Root.Add(data)
        Commit()
        If useZip Then
            Return OpenFileEncryptZip(IO.Path.Combine(secretFolder, rf.Value))
        Else
            Return OpenFileEncrypt(IO.Path.Combine(secretFolder, rf.Value))
        End If
    End Function
    Public Function OpenFileEncryptZip(path As String) As CryptoStreamZip
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
            dp.Add(fileStream)
            Dim cryptStrm As New System.Security.Cryptography.CryptoStream( _
                fileStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write)
            dp.Add(cryptStrm)
            Dim zs As New ZipOutputStream(cryptStrm)
            zs.UseZip64 = UseZip64.On
            zs.SetLevel(8)
            zs.PutNextEntry(New ZipEntry("tim"))
            dp.Add(zs)
            Return New CryptoStreamZip(cryptStrm, fileStream, encryptor, zs)
        Catch ex As Exception
            Tools.PrintException(ex)
            Return Nothing
        End Try
    End Function
    Public Function OpenFileDecryptZip(path As String) As CryptoStreamZip
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
            dp.Add(fileStream)
            Dim cryptStrm As New System.Security.Cryptography.CryptoStream( _
               fileStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read)
            dp.Add(cryptStrm)
            Dim zs As New ZipInputStream(cryptStrm)
            dp.Add(zs)
            zs.GetNextEntry()
            Return New CryptoStreamZip(cryptStrm, fileStream, decryptor, zs)
        Catch ex As Exception
            Tools.PrintException(ex)
            Return Nothing
        End Try
    End Function

End Class
