﻿Public Class DelegateCommand
    Implements IDelegateCommand
    Private _execute As Action(Of Object)
    Private _canExecute As Func(Of Object, Boolean)

#Region "Constructors"
    Public Sub New(execute As Action(Of Object), canExecute As Func(Of Object, Boolean))
        Me._execute = execute
        Me._canExecute = canExecute
    End Sub

    Public Sub New(execute As Action(Of Object))
        Me._execute = execute
        Me._canExecute = AddressOf Me.AlwaysCanExecute
    End Sub
#End Region

#Region "IDelegateCommand"
    Private Function AlwaysCanExecute(param As Object) As Boolean
        Return True
    End Function


    Public Function CanExecute(parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
        Return _canExecute(parameter)
    End Function

    Public Event CanExecuteChanged(sender As Object, e As EventArgs) Implements System.Windows.Input.ICommand.CanExecuteChanged

    Public Sub Execute(parameter As Object) Implements System.Windows.Input.ICommand.Execute
        _execute(parameter)
    End Sub

    Public Sub RaiseCanExecuteChanged() Implements IDelegateCommand.RaiseCanExecuteChanged
        RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
    End Sub
#End Region

End Class
Public Interface IDelegateCommand
    Inherits ICommand
    Sub RaiseCanExecuteChanged()
End Interface
