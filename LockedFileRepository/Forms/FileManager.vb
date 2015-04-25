Imports System.IO

Public Class FileManager
    Dim fms As FileManageSystem
    Private mouseDownPoint As Point = Point.Empty
    Private Sub FileManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If FileManageSystem.ProfileAvaliable Then
ask:
            Dim pw = PasswordInput.ShowDialog
            Try
                If pw = Nothing Then
                    If MessageBox.Show("終了しますか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                        Tools.CloseAndDispose(Me)
                        Return
                    Else
                        GoTo ask
                    End If
                ElseIf FileManageSystem.SubPWRegex.IsMatch(pw) Then
                    MessageBox.Show("仮パスワードは仕様上推奨されません。" & vbCrLf &
                                    "このパスワードが外部に漏れると、" & vbCrLf &
                                    "あなたがロックしているファイルの内容を見られる可能性があります。" & vbCrLf &
                                    "今後設定したパスワードを使用できる(思い出せる)なら、そのパスワードを使用して下さい。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                fms = New FileManageSystem(pw)
            Catch ex As Exception
                Tools.PrintException(ex)
                FileManageSystem.Instance = Nothing
                MessageBox.Show("パスワードが間違っています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo ask
            End Try
        Else
ask2:
            Dim pw = PasswordInput.ShowDialog("初期パスワードを入力して下さい。")
            If pw = Nothing Then
                If MessageBox.Show("終了しますか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                    Tools.CloseAndDispose(Me)
                    Return
                End If
            End If
            If pw <> PasswordInput.ShowDialog("確認のため、もう一回入力して下さい。") Then
                MessageBox.Show("2回のパスワードが違います。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo ask2
            End If
            Try
                fms = New FileManageSystem(pw)
            Catch ex As ArgumentException
                Tools.PrintException(ex)
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo ask2
            Catch ex As Exception
                Tools.PrintException(ex)
                FileManageSystem.Instance = Nothing
                MessageBox.Show("パスワードが間違っています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo ask2
            End Try
        End If
        UpdateFiles()
    End Sub
    Public Sub UpdateFiles()
        fms.Commit()
        FilesList.Items.Clear()
        For Each i In fms.GetFiles
            Console.WriteLine(i)
            FilesList.Items.Add(i)
        Next
        'For Each i In [Enum].GetNames(GetType(FileManageSystem.UsingZipEnum))
        '    Console.WriteLine("ENUM:" & i)
        'Next
    End Sub
    Private Sub BtnEnd_Click(sender As Object, e As EventArgs) Handles BtnEnd.Click
        fms.Commit()
        Me.Close()
    End Sub
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        FileSelecter.ShowDialog()
    End Sub
    Private Sub FileSelecter_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles FileSelecter.FileOk
        Dim q As New Queue(Of String())
        For Each i In FileSelecter.FileNames
            q.Enqueue({i, Path.GetFileName(i)})
        Next
        ProgressState.ShowDialog(ProgressState.ProgressMode.Encrypt, q)
        UpdateFiles()
    End Sub
    Private Sub BtnDele_Click(sender As Object, e As EventArgs) Handles BtnDele.Click
        Dim sel = Tools.GetSelectedItems(FilesList)
        If sel.Count = 0 Then
            MessageBox.Show("アイテムを選択して下さい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim q As New Queue(Of String())
        For Each i In sel
            q.Enqueue({"", i.Text})
        Next
        ProgressState.ShowDialog(ProgressState.ProgressMode.Delete, q)
        UpdateFiles()
    End Sub
    Private Sub BtnChn_Click(sender As Object, e As EventArgs) Handles BtnChn.Click
        Dim sel = Tools.GetSelectedItem(FilesList)
        If sel Is Nothing Then
            MessageBox.Show("アイテムを選択して下さい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim dest = InputBox("変更先のファイル名を入力して下さい。", "名前変更", sel.Text)
        If dest = Nothing Then
            Return
        End If
        fms.ChangeName(sel.Text, dest)
        UpdateFiles()
    End Sub
    Private Sub BtnUpd_Click(sender As Object, e As EventArgs) Handles BtnUpd.Click
        UpdateFiles()
    End Sub
    Private Sub BtnCopy_Click(sender As Object, e As EventArgs) Handles BtnCopy.Click
        Dim sel = Tools.GetSelectedItems(FilesList)
        If sel.Count = 0 Then
            MessageBox.Show("アイテムを選択して下さい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim q As New Queue(Of String())
        If sel.Count = 1 Then
            Dim result = Tools.SaveFile(, , )
            If result.Length = 0 Then
                Return
            End If
            q.Enqueue({result(0), sel(0).Text})
        Else
            Dim result = Tools.SelectFolder(, , True)
            If result = Nothing Or result = "" Then
                Return
            End If
            For Each i In sel
                q.Enqueue({Path.Combine(result, i.Text), i.Text})
            Next
        End If
        ProgressState.ShowDialog(ProgressState.ProgressMode.Decrypt, q)
        UpdateFiles()
    End Sub
    Private Sub BtnGetSP_Click(sender As Object, e As EventArgs) Handles BtnGetSP.Click
        ShowSubPassword.ShowDialog()
    End Sub
    Private Sub FilesList_DoubleClick(sender As Object, e As EventArgs) Handles FilesList.DoubleClick
        Dim sel = Tools.GetSelectedItem(FilesList)
        If sel Is Nothing Then
            MessageBox.Show("アイテムを選択して下さい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim q As New Queue(Of String())
        Dim file = Path.Combine(Path.GetTempPath, sel.Text)
        q.Enqueue({file, sel.Text})
        If Not ProgressState.ShowDialog(ProgressState.ProgressMode.Decrypt, q) Then
            Try
                Process.Start(file)
            Catch ex As Exception
                Tools.PrintExceptionD(ex)
            End Try
        End If
        UpdateFiles()
    End Sub
    'ドラッグ・アンド・ドロップのコードは移動しました。
End Class
