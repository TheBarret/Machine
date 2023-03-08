Imports System.Text.RegularExpressions

Public Interface IDefinition
    ReadOnly Property Name As String
    ReadOnly Property Options As RegexOptions
    Property Rules As List(Of Rule)
End Interface