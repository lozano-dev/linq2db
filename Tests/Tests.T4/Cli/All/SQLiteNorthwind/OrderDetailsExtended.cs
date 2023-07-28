// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Mapping;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.All.SQLiteNorthwind
{
	[Table("Order Details Extended", IsView = true)]
	public class OrderDetailsExtended
	{
		[Column("OrderID"      , DataType  = DataType.Int32 , DbType   = "int"           , Length    = 4            , Precision = 10, Scale     = 0           )] public int     OrderId       { get; set; } // int
		[Column("ProductID"    , DataType  = DataType.Int32 , DbType   = "int"           , Length    = 4            , Precision = 10, Scale     = 0           )] public int     ProductId     { get; set; } // int
		[Column("ProductName"  , CanBeNull = false          , DataType = DataType.VarChar, DbType    = "varchar(40)", Length    = 40, Precision = 0, Scale = 0)] public string  ProductName   { get; set; } = null!; // varchar(40)
		[Column("UnitPrice"    , DataType  = DataType.Double, DbType   = "float"         , Length    = 8            , Precision = 26, Scale     = 0           )] public double? UnitPrice     { get; set; } // float
		[Column("Quantity"     , DataType  = DataType.Int32 , DbType   = "int"           , Length    = 4            , Precision = 10, Scale     = 0           )] public int?    Quantity      { get; set; } // int
		[Column("Discount"     , DataType  = DataType.Double, DbType   = "float"         , Length    = 8            , Precision = 13, Scale     = 0           )] public double? Discount      { get; set; } // float
		[Column("ExtendedPrice", DbType    = "NUMERIC"      , Length   = 2147483647      , Precision = 0            , Scale     = 0                           )] public object? ExtendedPrice { get; set; } // NUMERIC
	}
}