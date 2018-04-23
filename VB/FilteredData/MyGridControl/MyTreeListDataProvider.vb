Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.Grid.TreeList
Imports DevExpress.Xpf.Grid

Namespace FilteredData.MyGridControl
	Friend Class MyTreeListDataProvider
		Inherits TreeListDataProvider
		Public Sub New(ByVal view As TreeListView)
			MyBase.New(view)
		End Sub

		Protected Overrides Function CalcNodeVisibility(ByVal node As TreeListNode) As Boolean
			Dim visible As Boolean = MyBase.CalcNodeVisibility(node)
			OnRefreshFilteredData(New MyFilterEventArgs(node,visible))
			Return visible
		End Function

		Public Delegate Sub RefreshFilteredDataEventHandler(ByVal sender As Object, ByVal e As MyFilterEventArgs)
		Public Event FilteredData As RefreshFilteredDataEventHandler
		Protected Overridable Sub OnRefreshFilteredData(ByVal e As MyFilterEventArgs)
			RaiseEvent FilteredData(Me, e)
		End Sub
	End Class

	Public Class MyFilterEventArgs
		Inherits EventArgs
		Private privateNode As TreeListNode
		Public Property Node() As TreeListNode
			Get
				Return privateNode
			End Get
			Set(ByVal value As TreeListNode)
				privateNode = value
			End Set
		End Property
		Private privateIsVisible As Boolean
		Public Property IsVisible() As Boolean
			Get
				Return privateIsVisible
			End Get
			Set(ByVal value As Boolean)
				privateIsVisible = value
			End Set
		End Property
		Public Sub New(ByVal node As TreeListNode, ByVal visible As Boolean)
			Me.Node = node
			IsVisible = visible
		End Sub
	End Class
End Namespace
