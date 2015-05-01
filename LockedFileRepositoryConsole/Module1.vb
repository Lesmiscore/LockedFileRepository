Imports System.Threading

Module Module1
    Dim repo As FileManageSystem
    Dim methods As IDictionary(Of String, Action(Of String())) = New Dictionary(Of String, Action(Of String()))
    Dim linkedText As IDictionary(Of String, String) = New Dictionary(Of String, String)
    Sub Main()
        methods("open") = Sub(args As String())
                              Dim repPath As String = Nothing
                              If args.Length >= 2 Then
                                  repPath = args(1)
                              End If
                              Dim pw = args(0)
                              repo = IIf(repPath = Nothing, New FileManageSystem(pw), New FileManageSystem(pw, repPath))
                              Console.WriteLine("Repository opened.")
                          End Sub
        methods("link") = Sub(args As String())
                              linkedText(args(0)) = Strings.Join(Shift(args), " ")
                              Console.WriteLine("Text linked.")
                          End Sub
        methods("copy") = Sub(args As String())
                              Dim proc = ConvertArray(args)
                              Dim ba(10000 - 1) As Byte
                              Using decoder = repo.GetFile(proc(0), False)
                                  Using fs As New FileStream(proc(1), FileMode.OpenOrCreate)
                                      Dim outfs = decoder.GetFileStream
                                      Dim lastPercent = 0
                                      Console.WriteLine((lastPercent * 10) & "% Copied...")
                                      While True
                                          Dim r = decoder.Read(ba, 0, 10000)
                                          outfs.Write(ba, 0, r)
                                          If lastPercent <> Math.Floor(outfs.Position / outfs.Length * 100) / 10 Then
                                              Console.WriteLine((lastPercent * 10) & "% Copied...")
                                              lastPercent = Math.Floor(outfs.Position / outfs.Length * 10)
                                          End If
                                      End While
                                  End Using
                              End Using
                              Console.WriteLine("C")
                          End Sub
        methods("exit") = Sub(args As String())
                              Process.GetCurrentProcess.Kill()
                          End Sub
        methods("files") = Sub(args As String())
                               For Each i In repo.GetFiles

                               Next
                           End Sub
        While True
            Console.Write("LockedFileRepository>")
            Dim cmd = Console.ReadLine().Split(" "c)
            Try
                methods(cmd(0))(Shift(cmd))
            Catch ex As Exception
                Tools.PrintException(ex)
            End Try
        End While
    End Sub
    Function ConvertArray(input As String()) As String()
        Dim output As String() = input.Clone()
        For i = 0 To output.Length - 1
            If output(i).StartsWith("%") And output(i).EndsWith("%") And linkedText.ContainsKey(output(i).Substring(1, output(i).Length - 2)) Then
                output(i) = linkedText(output(i).Substring(1, output(i).Length - 2))
            End If
        Next
        Return output
    End Function
    Function Shift(Of T)(array As T()) As T()
        Dim a(array.Length - 2) As T
        For i = 0 To array.Length - 2
            a(i) = array(i + 1)
        Next
        Return a
    End Function
End Module
