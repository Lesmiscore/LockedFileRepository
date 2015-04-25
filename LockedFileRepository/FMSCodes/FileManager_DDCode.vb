Imports System.IO

Partial Public Class FileManager
    Private Sub FilesList_DragEnter(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DragEventArgs) _
        Handles FilesList.DragEnter
        'コントロール内にドラッグされたとき実行される
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            'ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
            e.Effect = DragDropEffects.Copy
        Else
            'ファイル以外は受け付けない
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub FilesList_DragDrop(ByVal sender As Object, _
            ByVal e As System.Windows.Forms.DragEventArgs) _
            Handles FilesList.DragDrop
        'コントロール内にドロップされたとき実行される
        'ドロップされたすべてのファイル名を取得する
        Dim fileName As String() = CType( _
            e.Data.GetData(DataFormats.FileDrop, False), _
            String())
        'ListBoxに追加する
        Dim q As New Queue(Of String())
        'For Each i In fileName
        '    q.Enqueue({i, Path.GetFileName(i)})
        'Next
        FileCounter.ShowDialog(q, fileName)
        ProgressState.ShowDialog(ProgressState.ProgressMode.Encrypt, q)
        UpdateFiles()
    End Sub
    'ListBox1でマウスボタンが押された時
    Private Sub FilesList_MouseDown( _
            ByVal sender As Object, ByVal e As MouseEventArgs) Handles FilesList.MouseDown
        'マウスの左ボタンだけが押されている時のみドラッグできるようにする
        If e.Button = MouseButtons.Left Then
            'ドラッグの準備
            Dim lbx As ListView = CType(sender, ListView)
            'マウスの押された位置を記憶
            If Tools.GetSelectedItem(lbx) IsNot Nothing Then
                mouseDownPoint = New Point(e.X, e.Y)
            End If
        Else
            mouseDownPoint = Point.Empty
        End If
    End Sub

    Private Sub FilesList_MouseUp( _
        ByVal sender As Object, ByVal e As MouseEventArgs) Handles FilesList.MouseUp
        mouseDownPoint = Point.Empty
    End Sub

    Private Sub FilesList_MouseMove_Reject( _
            ByVal sender As Object, ByVal e As MouseEventArgs)
        If mouseDownPoint.IsEmpty = False Then
            'ドラッグとしないマウスの移動範囲を取得する
            Dim moveRect As New Rectangle( _
                mouseDownPoint.X - SystemInformation.DragSize.Width, _
                mouseDownPoint.Y - SystemInformation.DragSize.Height, _
                SystemInformation.DragSize.Width, _
                SystemInformation.DragSize.Height)
            'ドラッグとする移動範囲を超えたか調べる
            If Not moveRect.Contains(e.X, e.Y) Then
                Dim files As New Queue(Of String)
                Dim request As New Queue(Of String())
                Dim sels = Tools.GetSelectedItems(FilesList)
                For Each i In sels
                    Dim path = IO.Path.Combine(IO.Path.GetTempPath, i.Text)
                    files.Enqueue(path)
                    request.Enqueue({path, i.Text})
                Next
                ProgressState.ShowDialog(ProgressState.ProgressMode.Decrypt, request)
                'DataObjectを作成する
                Dim dataObj As New DataObject(DataFormats.FileDrop, files.ToArray)

                'ドラッグを開始する
                Dim dde As DragDropEffects = _
                   FilesList.DoDragDrop(dataObj, DragDropEffects.Move)
            End If
        End If
    End Sub
    Private Sub FilesList_MouseMove( _
            ByVal sender As Object, ByVal e As MouseEventArgs) Handles FilesList.MouseMove
        If mouseDownPoint.IsEmpty = False Then
            'ドラッグとする移動範囲を超えたか調べる
            Console.WriteLine(Cursor.Position)
            Console.WriteLine(Me.DesktopBounds)
            Console.WriteLine(Me.DesktopBounds.Contains(Cursor.Position))
            If Not Me.DesktopBounds.Contains(Cursor.Position) Then
                Dim files As New Queue(Of String)
                Dim request As New Queue(Of String())
                Dim sels = Tools.GetSelectedItems(FilesList)
                For Each i In sels
                    Dim path = IO.Path.Combine(IO.Path.GetTempPath, i.Text)
                    files.Enqueue(path)
                    request.Enqueue({path, i.Text})
                Next
                ProgressState.ShowDialog(ProgressState.ProgressMode.Decrypt, request)
                'DataObjectを作成する
                Dim dataObj As New DataObject(DataFormats.FileDrop, files.ToArray)

                'ドラッグを開始する
                Dim dde As DragDropEffects = _
                   FilesList.DoDragDrop(dataObj, DragDropEffects.Move)
            End If
        End If
    End Sub
    'ドラッグをキャンセルする
    Private Sub FilesList_QueryContinueDrag( _
            ByVal sender As Object, _
            ByVal e As QueryContinueDragEventArgs) Handles FilesList.QueryContinueDrag
        'マウスの右ボタンが押されていればドラッグをキャンセル
        '"2"はマウスの右ボタンを表す
        If (e.KeyState And 2) = 2 Then
            e.Action = DragAction.Cancel
        End If
    End Sub
End Class
