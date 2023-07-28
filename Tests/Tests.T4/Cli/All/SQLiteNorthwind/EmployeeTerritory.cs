// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Mapping;
using LinqToDB.Tools.Comparers;
using System;
using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.All.SQLiteNorthwind
{
	[Table("EmployeeTerritories")]
	public class EmployeeTerritory : IEquatable<EmployeeTerritory>
	{
		[Column("EmployeeID" , DataType  = DataType.Int32, DbType   = "int"           , Length = 4            , Precision = 10, Scale     = 0, IsPrimaryKey = true, PrimaryKeyOrder = 0                        )] public int    EmployeeId  { get; set; } // int
		[Column("TerritoryID", CanBeNull = false         , DataType = DataType.VarChar, DbType = "varchar(20)", Length    = 20, Precision = 0, Scale        = 0   , IsPrimaryKey    = true, PrimaryKeyOrder = 1)] public string TerritoryId { get; set; } = null!; // varchar(20)

		#region IEquatable<T> support
		private static readonly IEqualityComparer<EmployeeTerritory> _equalityComparer = ComparerBuilder.GetEqualityComparer<EmployeeTerritory>(c => c.EmployeeId, c => c.TerritoryId);

		public bool Equals(EmployeeTerritory? other)
		{
			return _equalityComparer.Equals(this, other!);
		}

		public override int GetHashCode()
		{
			return _equalityComparer.GetHashCode(this);
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as EmployeeTerritory);
		}
		#endregion
	}
}