Partial Public Class ProgressState
    Public Enum ProgressMode
        Encrypt
        Decrypt
        Delete
        Upgrade
        Add
    End Enum
    Public Enum ProgressSetMode
        All_Max
        All_Val
        Part_Max
        Part_Val
    End Enum
    Public Enum ProgressSetValueMode
        Abosulete
        Relative
    End Enum
End Class
