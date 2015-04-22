<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressState
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
        Me.PartProgress = New System.Windows.Forms.DecimalProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProcessInfo = New System.Windows.Forms.Label()
        Me.AllProgress = New System.Windows.Forms.DecimalProgressBar()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.ProcessingFile = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Processor = New System.ComponentModel.BackgroundWorker()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 10
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PartProgress, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ProcessInfo, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AllProgress, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnCancel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ProcessingFile, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 10
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(534, 231)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'PartProgress
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.PartProgress, 10)
        Me.PartProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PartProgress.FixParameter = False
        Me.PartProgress.Location = New System.Drawing.Point(3, 187)
        Me.PartProgress.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.PartProgress.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.PartProgress.Name = "PartProgress"
        Me.TableLayoutPanel1.SetRowSpan(Me.PartProgress, 2)
        Me.PartProgress.Size = New System.Drawing.Size(528, 41)
        Me.PartProgress.TabIndex = 5
        Me.PartProgress.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 2)
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "処理内容:"
        '
        'ProcessInfo
        '
        Me.ProcessInfo.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.ProcessInfo, 2)
        Me.ProcessInfo.Location = New System.Drawing.Point(109, 0)
        Me.ProcessInfo.Name = "ProcessInfo"
        Me.ProcessInfo.Size = New System.Drawing.Size(0, 12)
        Me.ProcessInfo.TabIndex = 1
        '
        'AllProgress
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.AllProgress, 10)
        Me.AllProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AllProgress.FixParameter = False
        Me.AllProgress.Location = New System.Drawing.Point(3, 118)
        Me.AllProgress.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.AllProgress.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.AllProgress.Name = "AllProgress"
        Me.TableLayoutPanel1.SetRowSpan(Me.AllProgress, 2)
        Me.AllProgress.Size = New System.Drawing.Size(528, 40)
        Me.AllProgress.TabIndex = 4
        Me.AllProgress.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'BtnCancel
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.BtnCancel, 2)
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(3, 72)
        Me.BtnCancel.Name = "BtnCancel"
        Me.TableLayoutPanel1.SetRowSpan(Me.BtnCancel, 2)
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "キャンセル"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'ProcessingFile
        '
        Me.ProcessingFile.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.ProcessingFile, 8)
        Me.ProcessingFile.Location = New System.Drawing.Point(109, 23)
        Me.ProcessingFile.Name = "ProcessingFile"
        Me.ProcessingFile.Size = New System.Drawing.Size(0, 12)
        Me.ProcessingFile.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label2, 2)
        Me.Label2.Location = New System.Drawing.Point(3, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "処理中のファイル:"
        '
        'Processor
        '
        Me.Processor.WorkerReportsProgress = True
        Me.Processor.WorkerSupportsCancellation = True
        '
        'ProgressState
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(534, 231)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProgressState"
        Me.Text = "処理しています"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ProcessInfo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ProcessingFile As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PartProgress As DecimalProgressBar
    Friend WithEvents AllProgress As DecimalProgressBar
    Friend WithEvents Processor As System.ComponentModel.BackgroundWorker
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
End Class
