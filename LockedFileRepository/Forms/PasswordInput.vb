Public Class PasswordInput
    Private pw As String = Nothing
    Private allowClose As Boolean = False
    Private Sub Apply_Click(sender As Object, e As EventArgs) Handles Apply.Click
        pw = Password.Text
        allowClose = True
        Me.Close()
    End Sub
    Private Sub PasswordInput_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If allowClose Then
            Return
        End If
        pw = Nothing
    End Sub
    Public Overloads Function ShowDialog(Optional prompt As String = "パスワードを入力して下さい。") As String
        Me.PromptText.Text = prompt
        Me.Password.Text = ""
        allowClose = False
        MyBase.ShowDialog()
        Return pw
    End Function
End Class