Imports AnythingTools.Servers

Partial Public Class FileManageSystem
    Dim server As HttpServer = New FMSHTTPSERVER(Me)
    <Obsolete>
    Public Sub StartRemoteCastHTTP(Optional port As Integer = 80)
        server.Ports.Remove(8080)
        server.Ports.Remove(8081)
        server.Ports.Add(port)
        server.StartServer()
    End Sub
    Protected Class FMSHTTPSERVER
        Inherits HttpServer

        Protected Friend Sub New(fms As FileManageSystem)

        End Sub
        Public Overrides Sub OnRespose(sender As Object, e As HttpServer.OnResponseEventArgs)
            Dim req = e.Request
            Dim res = e.Response
            Dim raw = req.RawUrl

        End Sub
    End Class
End Class
