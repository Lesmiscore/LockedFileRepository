<Obsolete>
Public Class DisposePit
    Dim ids As New Queue(Of IDisposable)
    Dim arrays As New Queue(Of Array)
    Public Function Add(id As IDisposable) As DisposePit
        ids.Enqueue(id)
        Return Me
    End Function
    Public Function Add(ParamArray id() As IDisposable) As DisposePit
        For Each i In id
            ids.Enqueue(i)
        Next
        Return Me
    End Function
    Public Function Add(array As Array) As DisposePit
        arrays.Enqueue(array)
        For Each i In array
            If i.GetType.IsArray Then
                Add(CType(i, Array))
            ElseIf GetType(IDisposable).IsInstanceOfType(i) Then
                Add(CType(i, IDisposable))
            End If
        Next
        Return Me
    End Function
    Public Function Add(ParamArray array() As Array) As DisposePit
        arrays.Enqueue(array)
        For Each i In array
            If i.GetType.IsArray Then
                Add(CType(i, Array))
            ElseIf GetType(IDisposable).IsInstanceOfType(i) Then
                Add(CType(i, IDisposable))
            End If
        Next
        Return Me
    End Function
    Public Function Dispose() As DisposePit
        While ids.Count <> 0
            Try
                CloseAndDispose(ids.Dequeue)
            Catch ex As Exception
                Tools.PrintException(ex)
            End Try
        End While
        While arrays.Count <> 0
            Try
                EraseArray(Of Object)(arrays.Dequeue)
            Catch ex As Exception
                Tools.PrintException(ex)
            End Try
        End While
        Return Me
    End Function
    Private Sub CloseAndDispose(Of T As IDisposable)(arr As T)
        Dim methodClose = (From j In arr.GetType.GetMethods Where j.Name = "Close")
        If methodClose.Count <> 0 Then
            For Each j In methodClose
                Try
                    j.Invoke(arr, {})
                Catch : End Try
            Next
        End If
        arr.Dispose()
    End Sub
    Private Sub EraseArray(Of T)(array As T())
        System.Array.Resize(array, 0)
        Erase array
    End Sub
End Class
