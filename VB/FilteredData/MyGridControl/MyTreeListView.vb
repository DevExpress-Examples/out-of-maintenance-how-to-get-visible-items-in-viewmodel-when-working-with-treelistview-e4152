Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.Grid
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Collections.Specialized

Namespace FilteredData.MyGridControl
	Friend Class MyTreeListView
		Inherits TreeListView
		Private ChangedFromViewModel As Boolean
		Public ForceUnfiltered As ObservableCollection(Of Object)
		Public NodeToDelete As Object

        Public Shared ReadOnly VisibleDataProperty As DependencyProperty = DependencyProperty.Register("VisibleData", GetType(ObservableCollection(Of Object)), GetType(MyTreeListView), New UIPropertyMetadata(AddressOf FilteredDataPropertyChanged))
		Public Property VisibleData() As ObservableCollection(Of Object)
			Get
				Return CType(GetValue(VisibleDataProperty), ObservableCollection(Of Object))
			End Get
			Set(ByVal value As ObservableCollection(Of Object))
				SetValue(VisibleDataProperty, value)
			End Set
		End Property
		Private Shared Sub FilteredDataPropertyChanged(ByVal source As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			Dim view As MyTreeListView = TryCast(source, MyTreeListView)
			view.ChangedFromViewModel = True
			view.ForceUnfiltered = New ObservableCollection(Of Object)()
			Dim list As ObservableCollection(Of Object) = TryCast(e.NewValue, ObservableCollection(Of Object))
			view.DataControl.RefreshData()
			AddHandler list.CollectionChanged, AddressOf view.list_CollectionChanged
			AddHandler view.CustomNodeFilter, AddressOf view.view_CustomNodeFilter
		End Sub

		Public Sub view_CustomNodeFilter(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs)
			If ForceUnfiltered.Contains(e.Node.Content) Then
				e.Visible = True
			End If
			e.Handled = True
		End Sub

		Public Sub list_CollectionChanged(ByVal sender As Object, ByVal e As System.Collections.Specialized.NotifyCollectionChangedEventArgs)
			If (Not ChangedFromViewModel) Then
				Return
			End If
			If e.Action = NotifyCollectionChangedAction.Add AndAlso (Not ForceUnfiltered.Contains(e.NewItems(0))) Then
				ForceUnfiltered.Add(e.NewItems(0))
				Me.DataControl.RefreshData()
			End If
			If e.Action = NotifyCollectionChangedAction.Remove AndAlso ForceUnfiltered.Contains(e.OldItems(0)) Then
				ForceUnfiltered.Remove(e.OldItems(0))
				NodeToDelete = e.OldItems(0)
				Me.DataControl.RefreshData()
			End If
		End Sub

		Private Sub dataProvider_RefreshFilteredData(ByVal sender As Object, ByVal e As MyFilterEventArgs)
			Dim col As ObservableCollection(Of Object) = VisibleData
			If col Is Nothing Then
				Return
			End If
			Dim f As Boolean = False
			If f Then
				col.Clear()
			End If
			ChangedFromViewModel = False
			If e.IsVisible Then
				If col.Contains(e.Node.Content) Then
					ChangedFromViewModel = True
					Return
				Else
					col.Add(e.Node.Content)
				End If
			Else
				If col.Contains(e.Node.Content) OrElse NodeToDelete Is e.Node.Content Then
					Dim objectsToRemove As New List(Of Object)()
					Dim nodeIterator As New TreeListNodeIterator(e.Node.Nodes, False)
					Do While nodeIterator.MoveNext()
						objectsToRemove.Add(nodeIterator.Current.Content)
					Loop
					objectsToRemove.Add(e.Node.Content)
					For Each o As Object In objectsToRemove
						col.Remove(o)
					Next o
					NodeToDelete = Nothing
				End If
			End If
			ChangedFromViewModel = True
		End Sub

		Protected Overrides Function CreateDataProvider() As DevExpress.Xpf.Grid.TreeList.TreeListDataProvider
			Dim dataProvider As New MyTreeListDataProvider(Me)
			AddHandler dataProvider.FilteredData, AddressOf dataProvider_RefreshFilteredData
			Return dataProvider
		End Function

	End Class
End Namespace
