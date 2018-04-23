Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections.ObjectModel

Namespace FilteredData.Model
	Friend Class DataHelper
		Public Shared Function GetDataSource(ByVal count As Integer) As ObservableCollection(Of GridItem)
			Dim collection As New ObservableCollection(Of GridItem)()
			Dim rand As New Random()
			For i As Integer = 0 To count - 1
                collection.Add(New GridItem(DateTime.Now.AddMinutes(count * i).AddDays((i - count \ 2) * count), String.Format("Name{0}", i), i, (i Mod count) \ 2))
			Next i
			Return collection
		End Function
	End Class

	Public Class GridItem
		''' <summary>
		''' Initializes a new instance of the GridItem class.
		''' </summary>
		''' <param name="date"></param>
		''' <param name="name"></param>
		''' <param name="iD"></param>
		Public Sub New(ByVal [date] As DateTime, ByVal name As String, ByVal iD As Integer, ByVal parentID As Integer)
			_Date = [date]
			_Name = name
			_ID = iD
			_ParentID = parentID
		End Sub
		Public Sub New()

		End Sub

		Private _ID As Integer
		Public Property ID() As Integer
			Get
				Return _ID
			End Get
			Set(ByVal value As Integer)
				_ID = value
			End Set
		End Property

		Private _ParentID As Integer
		Public Property ParentID() As Integer
			Get
				Return _ParentID
			End Get
			Set(ByVal value As Integer)
				_ParentID = value
			End Set
		End Property

		Private _Name As String
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				_Name = value
			End Set
		End Property

		Private _Date As DateTime
		Public Property [Date]() As DateTime
			Get
				Return _Date
			End Get
			Set(ByVal value As DateTime)
				_Date = value

			End Set
		End Property
	End Class
End Namespace
