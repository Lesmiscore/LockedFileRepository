Imports System.ComponentModel
Imports System.Text
Imports System.IO
Imports AnythingTools

Partial Public Class FileManageSystem
    WithEvents rc As New BackgroundWorker
    Public Function StartRemoteCastTCP() As Integer
        Dim random = Tools.StrictRandom.GetRandomInt(1024, 65536)
        rc.RunWorkerAsync(random)
        Return random
    End Function
    Private Sub rc_DoWork(sender As Object, e As DoWorkEventArgs) Handles rc.DoWork
        'ListenするIPアドレスを決める
        Dim host As String = "localhost"
        Dim ipAdd As System.Net.IPAddress = _
            System.Net.Dns.GetHostEntry(host).AddressList(0)
        '.NET Framework 1.1以前では、以下のようにする
        'Dim ipAdd As System.Net.IPAddress = _
        '    System.Net.Dns.Resolve(host).AddressList(0)

        'Listenするポート番号
        Dim port As Integer = e.Argument
        While Not rc.CancellationPending
            Try
                'TcpListenerオブジェクトを作成する
                Dim listener As New System.Net.Sockets.TcpListener(ipAdd, port)

                'Listenを開始する
                listener.Start()
                Console.WriteLine("Listenを開始しました({0}:{1})。", _
                    DirectCast(listener.LocalEndpoint, System.Net.IPEndPoint).Address, _
                    DirectCast(listener.LocalEndpoint, System.Net.IPEndPoint).Port)

                '接続要求があったら受け入れる
                Dim client As System.Net.Sockets.TcpClient = listener.AcceptTcpClient()
                Console.WriteLine("クライアント({0}:{1})と接続しました。", _
                    DirectCast(client.Client.RemoteEndPoint, System.Net.IPEndPoint).Address, _
                    DirectCast(client.Client.RemoteEndPoint, System.Net.IPEndPoint).Port)

                'NetworkStreamを取得
                Dim ns As System.Net.Sockets.NetworkStream = client.GetStream()

                'クライアントから送られたデータを受信する
                Dim enc As System.Text.Encoding = System.Text.Encoding.UTF8
                Dim disconnected As Boolean = False
                Dim needDownload As Boolean = False
                Dim needSend As Boolean = False
                Dim sendData As Stream = Nothing
                Dim writeStream As Stream = Nothing
                Dim ms As New System.IO.MemoryStream()
                Dim resBytes As Byte() = New Byte(255) {}
                Do
                    'データの一部を受信する
                    Dim resSize As Integer = ns.Read(resBytes, 0, resBytes.Length)
                    'Readが0を返した時はクライアントが切断したと判断
                    If resSize = 0 Then
                        Console.WriteLine("クライアントが切断しました。")
                        Exit Do
                    End If
                    '受信したデータを蓄積する
                    ms.Write(resBytes, 0, resSize)
                Loop While ns.DataAvailable
                '受信したデータを文字列に変換
                Dim resMsg As String = enc.GetString(ms.ToArray())
                ms.Close()
                Console.WriteLine(resMsg)

                'クライアントにデータを送信する
                'クライアントに送信する文字列を作成
                Dim sendMsg As String = "RESPONSE_ERROR"
                Try
                    If resMsg = "CONNECTION_TEST" Then
                        sendMsg = resMsg
                    ElseIf resMsg.StartsWith("VALIDATE_PASSWORD") Then
                        Dim pw = New UTF8Encoding().GetString(Convert.FromBase64String(resMsg.Split(";"c)(1)))
                        If CheckPassword(pw) Then
                            sendMsg = "PASSWORD_IS_CORRECT"
                        Else
                            sendMsg = "PASSWORD_IS_WRONG"
                        End If
                    ElseIf resMsg.StartsWith("UPLOAD_FILE") Then
                        Dim filename = New UTF8Encoding().GetString(Convert.FromBase64String(resMsg.Split(";"c)(1)))
                        If Not Tools.ValidateFileName(filename) Then
                            sendMsg = "FILENAME_IS_INVALIDATE"
                            Throw New FinishedSafetyException
                        End If
                        Try
                            writeStream = AddFile(filename)
                            If writeStream Is Nothing Then
                                sendMsg = "UPLOAD_NG"
                            Else
                                sendMsg = "UPLOAD_OK"
                                needDownload = True
                            End If
                        Catch ex As Exception
                            sendMsg = "UPLOAD_NG"
                        End Try
                    ElseIf resMsg.StartsWith("DOWNLOAD_FILE") Then
                        Dim filename = New UTF8Encoding().GetString(Convert.FromBase64String(resMsg.Split(";"c)(1)))
                        If Not Tools.ValidateFileName(filename) Then
                            sendMsg = "FILENAME_IS_INVALIDATE"
                            Throw New FinishedSafetyException
                        End If
                        If Not ExistsFile(filename) Then
                            sendMsg = "FILE_DOES_NOT_EXIST"
                            Throw New FinishedSafetyException
                        End If
                        Dim fs = OpenFileReadRaw(Path.Combine(ProfilePath, GetRealFile(filename)))
                        Dim bws As New Base64WriteStream("DOWNLOAD_OK")
                        Dim bs = New Byte(ProgressState.TwentyMegaBytes - 1) {}
                        While True
                            Dim readLen = fs.Read(bs, 0, bs.Length)
                            If readLen = 0 Then
                                Exit While
                            End If
                            bws.Write(bs, 0, readLen)
                        End While
                        fs.Close()
                        fs.Dispose()
                        sendMsg = bws.ToString
                    ElseIf resMsg.StartsWith("RENAME_FILE") Then
                        Dim fromfilename = New UTF8Encoding().GetString(Convert.FromBase64String(resMsg.Split(";"c)(1)))
                        Dim destfilename = New UTF8Encoding().GetString(Convert.FromBase64String(resMsg.Split(";"c)(2)))
                        If Not Tools.ValidateFileName(fromfilename) Then
                            sendMsg = "FILENAME_IS_INVALIDATE"
                            Throw New FinishedSafetyException
                        End If
                        If Not ExistsFile(fromfilename) Then
                            sendMsg = "FILE_DOES_NOT_EXIST"
                            Throw New FinishedSafetyException
                        End If
                        If ChangeName(fromfilename, destfilename) Then
                            sendMsg = "FILENAME_CHANGED_COMPLETELY"
                        Else
                            sendMsg = "FILENAME_CHANGE_FAILED"
                        End If
                    ElseIf resMsg.StartsWith("DELETE_FILE") Then
                        Dim fromfilename = New UTF8Encoding().GetString(Convert.FromBase64String(resMsg.Split(";"c)(1)))
                        If Not Tools.ValidateFileName(fromfilename) Then
                            sendMsg = "FILENAME_IS_INVALIDATE"
                            Throw New FinishedSafetyException
                        End If
                        If Not ExistsFile(fromfilename) Then
                            sendMsg = "FILE_DOES_NOT_EXIST"
                            Throw New FinishedSafetyException
                        End If
                        Try
                            DeleteFile(fromfilename)
                            sendMsg = "FILE_DELETE_COMPLETELY"
                        Catch ex As Exception
                            sendMsg = "FILE_DELETE_FAILED"
                        End Try
                    End If
                Catch ex As Exception
                    Tools.PrintException(ex)
                End Try
                '文字列をByte型配列に変換
                Dim sendBytes As Byte() = enc.GetBytes(sendMsg)
                'データを送信する
                ns.Write(sendBytes, 0, sendBytes.Length)
                Console.WriteLine(sendMsg)
                If needDownload Then
                    Do
                        'データの一部を受信する
                        Dim resSize As Integer = ns.Read(resBytes, 0, resBytes.Length)
                        'Readが0を返した時はクライアントが切断したと判断
                        If resSize = 0 Then
                            Exit Do
                        End If
                        '受信したデータを蓄積する
                        writeStream.Write(resBytes, 0, resSize)
                    Loop While ns.DataAvailable
                    writeStream.Close()
                    writeStream.Dispose()
                End If
                '閉じる
                ns.Close()
                client.Close()
                Console.WriteLine("クライアントとの接続を閉じました。")

                'リスナを閉じる
                listener.[Stop]()
                Console.WriteLine("Listenerを閉じました。")
            Catch ex As Exception
                Tools.PrintException(ex)
            End Try
        End While
    End Sub
End Class
