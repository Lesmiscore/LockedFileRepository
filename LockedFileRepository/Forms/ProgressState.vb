Imports System.IO
Imports AnythingTools

Public Class ProgressState
    Public Const TwentyMegaBytes As Long = 20 * 1024 * 1024
    Public Const TwentyKyroBytes As Long = 20 * 1024
    Public Const OneGigaBytes As Long = 1024 * 1024 * 1024
    Public Const FiftyMegaBytes As Long = 50 * 1024 * 1024
    Public Const TwoHundredMegaBytes As Long = 200 * 1024 * 1024
    Public Const FiveHundredMegaBytes As Long = 500 * 1024 * 1024
    Dim mode As ProgressMode
    Dim args As Queue(Of String())
    Dim cancelled As Boolean = False
    Dim finished As Boolean = False
    Dim fms As FileManageSystem = FileManageSystem.Instance
    Private Sub Processor_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Processor.DoWork
        Try
            Select Case mode
                Case ProgressMode.Encrypt
                    SetProgress(ProgressSetMode.All_Max, args.Count, ProgressSetValueMode.Abosulete)
                    While args.Count <> 0 And Not Processor.CancellationPending
                        Dim arg = args.Dequeue
                        Dim c As New Counter
                        Try
                            Dim fileSize = Tools.GetFileSize(arg(0))
                            Using crpt = fms.AddFile(arg(1), IIf(fileSize > FiftyMegaBytes, IIf(fileSize > OneGigaBytes,
                                                                                              FileManageSystem.UsingZipEnum.SuperCompression,
                                                                                              FileManageSystem.UsingZipEnum.True),
                                                               FileManageSystem.UsingZipEnum.False))
                                Using file As New FileStream(arg(0), FileMode.Open, FileAccess.Read)
                                    SetProgress(ProgressSetMode.Part_Max, file.Length, ProgressSetValueMode.Abosulete)
                                    Dim bs As Byte()
                                    If fileSize < FiftyMegaBytes Then
                                        bs = New Byte(TwentyKyroBytes - 1) {} '20KBずつ
                                    Else
                                        bs = New Byte(FiftyMegaBytes - 1) {} '50MBずつ
                                    End If
                                    Dim readLen As Integer
                                    While True
                                        SetProgress(arg(1))
                                        readLen = file.Read(bs, 0, bs.Length)
                                        Debug.WriteLine("Encrypt readLen:" & readLen & "/internalCount:" & c.BackIncrement(readLen))
                                        If readLen = 0 Then
                                            Exit While
                                        End If
                                        crpt.Write(bs, 0, readLen)
                                        SetProgress(ProgressSetMode.Part_Val, readLen, ProgressSetValueMode.Relative)
                                    End While
                                    Erase bs
                                End Using
                            End Using
                        Catch ex As Exception
                            Tools.PrintException(ex)
                            fms.DeleteFile(arg(1))
                        End Try
                        SetProgress(ProgressSetMode.All_Val, 1, ProgressSetValueMode.Relative)
                        SetProgress(ProgressSetMode.Part_Val, , ProgressSetValueMode.Abosulete)
                    End While
                Case ProgressMode.Decrypt
                    SetProgress(ProgressSetMode.All_Max, args.Count, ProgressSetValueMode.Abosulete)
                    While args.Count <> 0 And Not Processor.CancellationPending
                        Dim arg = args.Dequeue
                        Dim c As New Counter
                        Try
                            Using crpt As CryptoStreamSelf = fms.GetFile(arg(1), False)
                                Using bufs As New BufferedStream(crpt, 1000000)
                                    Using file As New FileStream(arg(0), FileMode.Create, FileAccess.Write)
                                        SetProgress(ProgressSetMode.Part_Max, crpt.GetFileStream.Length, ProgressSetValueMode.Abosulete)
                                        Dim bs As Byte()
                                        If crpt.GetFileStream.Length < TwentyKyroBytes Then
                                            bs = New Byte(TwentyKyroBytes - 1) {} '20KBずつ
                                        Else
                                            bs = New Byte(FiftyMegaBytes - 1) {} '50MBずつ
                                        End If
                                        'fms.GetDisposePit.Add(bs)
                                        Dim readLen As Integer
                                        While True
                                            SetProgress(ProgressSetMode.Part_Val, crpt.GetFileStream.Position, ProgressSetValueMode.Abosulete)
                                            SetProgress(arg(1))
                                            readLen = bufs.Read(bs, 0, bs.Length)
                                            Debug.WriteLine("Decrypt readLen:" & readLen & "/internalCount:" & c.BackIncrement(readLen))
                                            If readLen = 0 Then
                                                Exit While
                                            End If
                                            file.Write(bs, 0, readLen)
                                        End While
                                        Erase bs
                                    End Using
                                End Using
                            End Using
                        Catch ex As Exception
                            Tools.PrintException(ex)
                            'fms.DeleteFile(arg(1))
                        End Try
                        SetProgress(ProgressSetMode.All_Val, 1, ProgressSetValueMode.Relative)
                        SetProgress(ProgressSetMode.Part_Val, , ProgressSetValueMode.Abosulete)
                    End While
                Case ProgressMode.Delete
                    SetProgress(ProgressSetMode.All_Max, args.Count, ProgressSetValueMode.Abosulete)
                    While args.Count <> 0 And Not Processor.CancellationPending
                        Dim arg = args.Dequeue
                        SetProgress(arg(1))
                        SetProgress(ProgressSetMode.All_Val, 1, ProgressSetValueMode.Relative)
                        SetProgress(ProgressSetMode.Part_Val, , ProgressSetValueMode.Abosulete)
                        Try
                            fms.DeleteFile(arg(1))
                        Catch ex As Exception
                            Tools.PrintException(ex)
                        End Try
                    End While
                Case ProgressMode.Upgrade
                    Dim tags = fms.GetXDocument.Root
                    Dim entries = tags...<FileEntry>
                    SetProgress(ProgressSetMode.All_Max, entries.Count + 1, ProgressSetValueMode.Abosulete)
                    SetProgress(ProgressSetMode.Part_Max, entries.Count + 1, ProgressSetValueMode.Abosulete)
                    SetProgress("プロファイルのルート")
                    tags.@Version = FileManageSystem.ProfileVersion
                    For Each i In entries
                        SetProgress(i.@VirtualFile)
                        SetProgress(ProgressSetMode.All_Val, 1, ProgressSetValueMode.Relative)
                        SetProgress(ProgressSetMode.Part_Val, 1, ProgressSetValueMode.Abosulete)
                        Dim zi = <UsingZip></UsingZip>
                        zi.Value = False
                        i.Add(zi)
                    Next
            End Select
            fms.Commit() 'これを呼び出さないと変更が保存されない
        Catch ex As Exception
            Tools.PrintException(ex)
            cancelled = True
        End Try
    End Sub
    Private Sub SetProgress(psm As ProgressSetMode, Optional value As Decimal = 0, Optional setmode As ProgressSetValueMode = ProgressSetValueMode.Abosulete)
        Invoke(Sub()
                   Select Case psm
                       Case ProgressSetMode.All_Max
                           AllProgress.Maximum = IIf(setmode = ProgressSetValueMode.Abosulete, value, AllProgress.Maximum + value)
                       Case ProgressSetMode.All_Val
                           AllProgress.Value = IIf(setmode = ProgressSetValueMode.Abosulete, value, AllProgress.Value + value)
                       Case ProgressSetMode.Part_Max
                           PartProgress.Maximum = IIf(setmode = ProgressSetValueMode.Abosulete, value, PartProgress.Maximum + value)
                       Case ProgressSetMode.Part_Val
                           PartProgress.Value = IIf(setmode = ProgressSetValueMode.Abosulete, value, PartProgress.Value + value)
                   End Select
               End Sub)
    End Sub
    Private Sub SetProgress(s As String)
        Invoke(Sub()
                   ProcessingFile.Text = s
               End Sub)
    End Sub
    Public Shadows Function ShowDialog(requestMode As ProgressMode, args As Queue(Of String())) As Boolean
        Me.args = args
        Me.mode = requestMode
        Select Case requestMode
            Case ProgressMode.Add
                Me.ProcessInfo.Text = "ファイルの追加"
            Case ProgressMode.Encrypt
                Me.ProcessInfo.Text = "ファイルの暗号化"
            Case ProgressMode.Decrypt
                Me.ProcessInfo.Text = "ファイルの復号"
            Case ProgressMode.Delete
                Me.ProcessInfo.Text = "ファイルの削除"
            Case ProgressMode.Upgrade
                Me.ProcessInfo.Text = "プロファイルのアップグレード"
        End Select
        AllProgress.Value = 0
        PartProgress.Value = 0
        AllProgress.Maximum = 100
        PartProgress.Maximum = 100
        cancelled = False
        finished = False
        MyBase.ShowDialog()
        Return cancelled
    End Function
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        cancelled = True
        finished = True
        Processor.CancelAsync()
        Me.Close()
    End Sub
    Private Sub ProgressState_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If finished Then
            Return
        End If
        cancelled = True
    End Sub
    Private Sub Processor_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Processor.RunWorkerCompleted
        finished = True
        If Processor.CancellationPending Then
            cancelled = True
        End If
        Me.Close()
    End Sub
    Private Sub ProgressState_Load(sender As Object, e As EventArgs) Handles Me.Load
        'While Processor.IsBusy
        'End While
        Processor.RunWorkerAsync()
    End Sub
End Class