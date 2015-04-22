Imports System.IO

Public Class Tools
    Public Shared Sub PrintException(ex As Exception)
        Console.WriteLine(ex.GetType.Name)
        Console.WriteLine(ex.Message)
        Console.WriteLine(ex.StackTrace)
    End Sub
    Public Shared Sub CloseAndDispose(Of T As IDisposable)(ParamArray arr() As T)
        For Each i In arr
            Dim methodClose = (From j In i.GetType.GetMethods Where j.Name = "Close")
            If methodClose.Count <> 0 Then
                For Each j In methodClose
                    Try
                        j.Invoke(i, {})
                    Catch : End Try
                Next
            End If
            i.Dispose()
        Next
    End Sub
    Public Shared Function GetSelectedItem(lv As ListView) As ListViewItem
        Dim sels = GetSelectedItems(lv)
        If sels.Count = 0 Then
            Return Nothing
        End If
        Return sels(0)
    End Function
    Public Shared Function GetSelectedItems(lv As ListView) As ListViewItem()
        Dim q As New Queue(Of ListViewItem)
        For Each i As ListViewItem In lv.Items
            If i.Selected Then
                Console.WriteLine("Selection:" & i.Text)
                q.Enqueue(i)
            End If
        Next
        Return q.ToArray
    End Function
    Public Shared Function OpenFile(Optional prompt As String = "ファイルを開く",
                                    Optional filter As String = "全てのファイル|*.*",
                                    Optional defaultFile As String = "",
                                    Optional multiSelect As Boolean = False) As String()
        Dim ofd As New OpenFileDialog
        ofd.FileName = defaultFile
        ofd.Filter = filter
        ofd.Title = prompt
        ofd.Multiselect = multiSelect
        If ofd.ShowDialog() = DialogResult.Cancel Then
            Return {}
        End If
        Return ofd.FileNames
    End Function
    Public Shared Function SelectFolder(Optional prompt As String = "フォルダを選択",
                                        Optional defaultFile As String = "",
                                        Optional showNewFolderButton As Boolean = False) As String
        Dim ofd As New FolderBrowserDialog
        ofd.ShowNewFolderButton = showNewFolderButton
        ofd.Description = prompt
        ofd.SelectedPath = defaultFile
        If ofd.ShowDialog() = DialogResult.Cancel Then
            Return ""
        End If
        Return ofd.SelectedPath
    End Function
    Public Shared Function SaveFile(Optional prompt As String = "ファイルを保存",
                                    Optional filter As String = "全てのファイル|*.*",
                                    Optional defaultFile As String = "") As String()
        Dim sfd As New SaveFileDialog
        sfd.FileName = defaultFile
        sfd.Filter = filter
        sfd.Title = prompt
        If sfd.ShowDialog() = DialogResult.Cancel Then
            Return {}
        End If
        Return sfd.FileNames
    End Function
    Public Shared Function GetFileSize(path As String) As Long
        Try
            Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim l = fs.Length
            fs.Close()
            fs.Dispose()
            Return l
        Catch ex As Exception
            Return -1
        End Try
    End Function
End Class
