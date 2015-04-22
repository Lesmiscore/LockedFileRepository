Public Class FinishedSafetyException
    Inherits Exception
    Public Sub New()
        MyBase.New("この例外はエラーではなく、スレッドを終了させるための例外です。")
    End Sub
End Class
