Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Input
Imports System.Windows

Namespace FilteredData.Commands
	Public Class CommandReference
		Inherits Freezable
		Implements ICommand
		Public Sub New()
			' Blank
		End Sub

		Public Shared ReadOnly CommandProperty As DependencyProperty = DependencyProperty.Register("Command", GetType(ICommand), GetType(CommandReference), New PropertyMetadata(New PropertyChangedCallback(AddressOf OnCommandChanged)))

		Public Property Command() As ICommand
			Get
				Return CType(GetValue(CommandProperty), ICommand)
			End Get
			Set(ByVal value As ICommand)
				SetValue(CommandProperty, value)
			End Set
		End Property

		#Region "ICommand Members"

		Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
			If Command IsNot Nothing Then
				Return Command.CanExecute(parameter)
			End If
			Return False
		End Function

		Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
			Command.Execute(parameter)
		End Sub

		Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

		Private Shared Sub OnCommandChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)

		End Sub

		#End Region

		#Region "Freezable"

		Protected Overrides Function CreateInstanceCore() As Freezable
			Throw New NotImplementedException()
		End Function

		#End Region
	End Class
End Namespace
