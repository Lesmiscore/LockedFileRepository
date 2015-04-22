Public Class ShowSubPassword

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pw = FileManageSystem.Instance.UnlockPassword
        If pw = Nothing Then
            TextBox1.Text = "仮パスワードを使用してアンロックしているため、表示されません。"
            Return
        End If
        TextBox1.Text = pw
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class