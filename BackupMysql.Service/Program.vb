Imports System.ServiceProcess

Namespace Service
    Module Program
        Sub Main()
            ServiceBase.Run(New BackupWindowsService())
        End Sub
    End Module
End Namespace
