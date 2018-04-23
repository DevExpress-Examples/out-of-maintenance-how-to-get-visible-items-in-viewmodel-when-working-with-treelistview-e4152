Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.Grid
Imports FilteredData.Model
Imports FilteredData.Commands
Imports System.Windows.Input
Imports System.Collections.ObjectModel

Namespace FilteredData.ViewModel
	Friend Class ViewModel
		Public Sub New()
            FilteredData = New ObservableCollection(Of Object)()
		End Sub

		' Fields...
		Private privateFilteredData As ObservableCollection(Of Object)
		Public Property FilteredData() As ObservableCollection(Of Object)
			Get
				Return privateFilteredData
			End Get
			Set(ByVal value As ObservableCollection(Of Object))
				privateFilteredData = value
			End Set
		End Property

		Private _DataSource As ObservableCollection(Of GridItem)
		Public Property DataSource() As ObservableCollection(Of GridItem)
			Get
				If _DataSource Is Nothing Then
					_DataSource = DataHelper.GetDataSource(20)
				End If
				Return _DataSource
			End Get
			Set(ByVal value As ObservableCollection(Of GridItem))
				_DataSource = value

			End Set
		End Property

		Private addFilteredCommand_Renamed As DelegateCommand
		Public ReadOnly Property AddFilteredCommand() As ICommand
			Get
				If addFilteredCommand_Renamed Is Nothing Then
					addFilteredCommand_Renamed = New DelegateCommand(AddressOf AddFiltered)
				End If
				Return addFilteredCommand_Renamed
			End Get
		End Property

		Private Sub AddFiltered()
            If (Not FilteredData.Contains(_DataSource(1))) Then
                FilteredData.Add(_DataSource(1))
            End If
		End Sub


		Private removeFilteredCommand_Renamed As DelegateCommand
		Public ReadOnly Property RemoveFilteredCommand() As ICommand
			Get
				If removeFilteredCommand_Renamed Is Nothing Then
					removeFilteredCommand_Renamed = New DelegateCommand(AddressOf RemoveFiltered)
				End If
				Return removeFilteredCommand_Renamed
			End Get
		End Property

		Private Sub RemoveFiltered()
            FilteredData.Remove(_DataSource(1))
		End Sub
	End Class
End Namespace
