Partial Public Class FileManageSystem
    Public Function OpenFileEncryptSC(path As String) As CryptoStreamSupCpm
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
            Dim scs As New SuperCompressionStream(cryptStrm, Compression.CompressionMode.Compress)
            Return New CryptoStreamSupCpm(cryptStrm, fileStream, encryptor, scs)
        Catch ex As Exception
            Tools.PrintException(ex)
            Return Nothing
        End Try
    End Function
    Public Function OpenFileDecryptSC(path As String) As CryptoStreamSupCpm
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
            Dim fileStream As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim cryptStrm As New System.Security.Cryptography.CryptoStream( _
               fileStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read)
            Dim scs As New SuperCompressionStream(cryptStrm, Compression.CompressionMode.Decompress)
            Return New CryptoStreamSupCpm(cryptStrm, fileStream, decryptor, scs)
        Catch ex As Exception
            Tools.PrintException(ex)
            Return Nothing
        End Try
    End Function
End Class
