Public Class FileCounter
    Dim args As Queue(Of String())
    Dim files As String()
    Private Sub FileCounter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FileIndexer.RunWorkerAsync()
    End Sub
    Private Sub FileIndexer_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles FileIndexer.RunWorkerCompleted
        Dim root As String = Nothing
        For Each i In files
            If File.Exists(i) Then
                If root = Nothing Then
                    root = Path.GetFullPath(Path.Combine(root, "../"))
                End If
                args.Enqueue({i, Path.GetFileName(i)})
            ElseIf Directory.Exists(i) Then
                If root = Nothing Then
                    root = Path.GetFullPath(Path.Combine(root, "../"))
                End If
                DoIndexing(i, root)
            End If
        Next
    End Sub
    Public Overloads Sub ShowDialog(args As Queue(Of String()), files As String())
        Me.args = args
        Me.files = files
        Me.ShowDialog()
    End Sub
    Private Sub DoIndexing(dir As String, root As String)
        For Each i In Directory.GetFiles(dir)
            args.Enqueue({i, i.Substring(0, root.Length).TrimStart("\"c, "/"c)})
        Next
        For Each i In Directory.GetDirectories(dir)
            DoIndexing(i, root)
        Next
    End Sub
End Class