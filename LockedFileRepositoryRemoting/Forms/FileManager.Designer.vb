<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileManager
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnEnd = New System.Windows.Forms.Button()
        Me.BtnChn = New System.Windows.Forms.Button()
        Me.BtnDele = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.FilesList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnUpd = New System.Windows.Forms.Button()
        Me.BtnCopy = New System.Windows.Forms.Button()
        Me.BtnGetSP = New System.Windows.Forms.Button()
        Me.FileSelecter = New System.Windows.Forms.OpenFileDialog()
        Me.FolderSelecter = New System.Windows.Forms.FolderBrowserDialog()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 8
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.Controls.Add(Me.BtnEnd, 8, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnChn, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnDele, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnAdd, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnUpd, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnCopy, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnGetSP, 5, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(735, 445)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'BtnEnd
        '
        Me.BtnEnd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnEnd.Location = New System.Drawing.Point(640, 3)
        Me.BtnEnd.Name = "BtnEnd"
        Me.BtnEnd.Size = New System.Drawing.Size(92, 34)
        Me.BtnEnd.TabIndex = 6
        Me.BtnEnd.Text = "終了"
        Me.BtnEnd.UseVisualStyleBackColor = True
        '
        'BtnChn
        '
        Me.BtnChn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnChn.Location = New System.Drawing.Point(185, 3)
        Me.BtnChn.Name = "BtnChn"
        Me.BtnChn.Size = New System.Drawing.Size(85, 34)
        Me.BtnChn.TabIndex = 5
        Me.BtnChn.Text = "名前変更"
        Me.BtnChn.UseVisualStyleBackColor = True
        '
        'BtnDele
        '
        Me.BtnDele.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnDele.Location = New System.Drawing.Point(94, 3)
        Me.BtnDele.Name = "BtnDele"
        Me.BtnDele.Size = New System.Drawing.Size(85, 34)
        Me.BtnDele.TabIndex = 4
        Me.BtnDele.Text = "削除"
        Me.BtnDele.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel2, 8)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.FilesList, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 43)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 399.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(729, 399)
        Me.TableLayoutPanel2.TabIndex = 2
        '
        'FilesList
        '
        Me.FilesList.AllowDrop = True
        Me.FilesList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.FilesList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilesList.FullRowSelect = True
        Me.FilesList.Location = New System.Drawing.Point(3, 3)
        Me.FilesList.Name = "FilesList"
        Me.FilesList.Size = New System.Drawing.Size(723, 393)
        Me.FilesList.TabIndex = 1
        Me.FilesList.UseCompatibleStateImageBehavior = False
        Me.FilesList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ファイル名"
        Me.ColumnHeader1.Width = 343
        '
        'BtnAdd
        '
        Me.BtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnAdd.Location = New System.Drawing.Point(3, 3)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(85, 34)
        Me.BtnAdd.TabIndex = 3
        Me.BtnAdd.Text = "追加"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'BtnUpd
        '
        Me.BtnUpd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnUpd.Location = New System.Drawing.Point(276, 3)
        Me.BtnUpd.Name = "BtnUpd"
        Me.BtnUpd.Size = New System.Drawing.Size(85, 34)
        Me.BtnUpd.TabIndex = 7
        Me.BtnUpd.Text = "更新"
        Me.BtnUpd.UseVisualStyleBackColor = True
        '
        'BtnCopy
        '
        Me.BtnCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnCopy.Location = New System.Drawing.Point(367, 3)
        Me.BtnCopy.Name = "BtnCopy"
        Me.BtnCopy.Size = New System.Drawing.Size(85, 34)
        Me.BtnCopy.TabIndex = 8
        Me.BtnCopy.Text = "コピー"
        Me.BtnCopy.UseVisualStyleBackColor = True
        '
        'BtnGetSP
        '
        Me.BtnGetSP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnGetSP.Location = New System.Drawing.Point(458, 3)
        Me.BtnGetSP.Name = "BtnGetSP"
        Me.BtnGetSP.Size = New System.Drawing.Size(85, 34)
        Me.BtnGetSP.TabIndex = 9
        Me.BtnGetSP.Text = "仮パスワード" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "の発行/確認"
        Me.BtnGetSP.UseVisualStyleBackColor = True
        '
        'FileSelecter
        '
        Me.FileSelecter.Filter = "全てのファイル|*.*"
        Me.FileSelecter.Multiselect = True
        '
        'FolderSelecter
        '
        '
        'FileManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 445)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "FileManager"
        Me.ShowIcon = False
        Me.Text = "ファイルロック金庫"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FilesList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BtnChn As System.Windows.Forms.Button
    Friend WithEvents BtnDele As System.Windows.Forms.Button
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents FileSelecter As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BtnEnd As System.Windows.Forms.Button
    Friend WithEvents BtnUpd As System.Windows.Forms.Button
    Friend WithEvents BtnCopy As System.Windows.Forms.Button
    Friend WithEvents BtnGetSP As System.Windows.Forms.Button
    Friend WithEvents FolderSelecter As System.Windows.Forms.FolderBrowserDialog

End Class
