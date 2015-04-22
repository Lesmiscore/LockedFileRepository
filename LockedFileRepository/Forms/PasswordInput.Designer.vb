<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PasswordInput
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Password = New System.Windows.Forms.MaskedTextBox()
        Me.PromptText = New System.Windows.Forms.Label()
        Me.Apply = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Password
        '
        Me.Password.Location = New System.Drawing.Point(12, 28)
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.Password.Size = New System.Drawing.Size(187, 19)
        Me.Password.TabIndex = 0
        '
        'PromptText
        '
        Me.PromptText.AutoSize = True
        Me.PromptText.Location = New System.Drawing.Point(13, 13)
        Me.PromptText.Name = "PromptText"
        Me.PromptText.Size = New System.Drawing.Size(0, 12)
        Me.PromptText.TabIndex = 1
        '
        'Apply
        '
        Me.Apply.Location = New System.Drawing.Point(205, 24)
        Me.Apply.Name = "Apply"
        Me.Apply.Size = New System.Drawing.Size(75, 23)
        Me.Apply.TabIndex = 2
        Me.Apply.Text = "決定"
        Me.Apply.UseVisualStyleBackColor = True
        '
        'PasswordInput
        '
        Me.AcceptButton = Me.Apply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 53)
        Me.Controls.Add(Me.Apply)
        Me.Controls.Add(Me.PromptText)
        Me.Controls.Add(Me.Password)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "PasswordInput"
        Me.Text = "パスワード入力"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Password As System.Windows.Forms.MaskedTextBox
    Friend WithEvents PromptText As System.Windows.Forms.Label
    Friend WithEvents Apply As System.Windows.Forms.Button
End Class
