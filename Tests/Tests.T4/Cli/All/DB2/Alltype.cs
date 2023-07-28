// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using IBM.Data.DB2Types;
using LinqToDB;
using LinqToDB.Mapping;
using LinqToDB.Tools.Comparers;
using System;
using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.All.DB2
{
	[Table("ALLTYPES")]
	public class Alltype : IEquatable<Alltype>
	{
		[Column("ID"               , DataType = DataType.Int32    , DbType = "INTEGER"                 , IsPrimaryKey = true   , IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public DB2Int32         Id                { get; set; } // INTEGER
		[Column("BIGINTDATATYPE"   , DataType = DataType.Int64    , DbType = "BIGINT"                                                                                                       )] public DB2Int64?        Bigintdatatype    { get; set; } // BIGINT
		[Column("INTDATATYPE"      , DataType = DataType.Int32    , DbType = "INTEGER"                                                                                                      )] public DB2Int32?        Intdatatype       { get; set; } // INTEGER
		[Column("SMALLINTDATATYPE" , DataType = DataType.Int16    , DbType = "SMALLINT"                                                                                                     )] public DB2Int16?        Smallintdatatype  { get; set; } // SMALLINT
		[Column("DECIMALDATATYPE"  , DataType = DataType.Decimal  , DbType = "DECIMAL"                 , Precision    = 30                                                                  )] public DB2Decimal?      Decimaldatatype   { get; set; } // DECIMAL
		[Column("DECFLOATDATATYPE" , DataType = DataType.Decimal  , DbType = "DECFLOAT(16)"            , Precision    = 16                                                                  )] public DB2DecimalFloat? Decfloatdatatype  { get; set; } // DECFLOAT(16)
		[Column("REALDATATYPE"     , DataType = DataType.Single   , DbType = "REAL"                                                                                                         )] public DB2Real?         Realdatatype      { get; set; } // REAL
		[Column("DOUBLEDATATYPE"   , DataType = DataType.Double   , DbType = "DOUBLE"                                                                                                       )] public DB2Double?       Doubledatatype    { get; set; } // DOUBLE
		[Column("CHARDATATYPE"     , DataType = DataType.Char     , DbType = "CHARACTER(1)"            , Length       = 1                                                                   )] public DB2String?       Chardatatype      { get; set; } // CHARACTER(1)
		[Column("CHAR20DATATYPE"   , DataType = DataType.Char     , DbType = "CHARACTER(20)"           , Length       = 20                                                                  )] public DB2String?       Char20Datatype    { get; set; } // CHARACTER(20)
		[Column("VARCHARDATATYPE"  , DataType = DataType.VarChar  , DbType = "VARCHAR(20)"             , Length       = 20                                                                  )] public DB2String?       Varchardatatype   { get; set; } // VARCHAR(20)
		[Column("CLOBDATATYPE"     , DataType = DataType.Text     , DbType = "CLOB(1048576)"           , Length       = 1048576                                                             )] public DB2Clob?         Clobdatatype      { get; set; } // CLOB(1048576)
		[Column("DBCLOBDATATYPE"   , DataType = DataType.Text     , DbType = "DBCLOB(100)"             , Length       = 100                                                                 )] public DB2Clob?         Dbclobdatatype    { get; set; } // DBCLOB(100)
		[Column("BINARYDATATYPE"   , DataType = DataType.Binary   , DbType = "CHAR (5) FOR BIT DATA"   , Length       = 5                                                                   )] public DB2Binary?       Binarydatatype    { get; set; } // CHAR (5) FOR BIT DATA
		[Column("VARBINARYDATATYPE", DataType = DataType.VarBinary, DbType = "VARCHAR (5) FOR BIT DATA", Length       = 5                                                                   )] public DB2Binary?       Varbinarydatatype { get; set; } // VARCHAR (5) FOR BIT DATA
		[Column("BLOBDATATYPE"     , DataType = DataType.Blob     , DbType = "BLOB(1048576)"           , Length       = 1048576                                                             )] public DB2Blob?         Blobdatatype      { get; set; } // BLOB(1048576)
		[Column("GRAPHICDATATYPE"  , DataType = DataType.Text     , DbType = "GRAPHIC(10)"             , Length       = 10                                                                  )] public DB2String?       Graphicdatatype   { get; set; } // GRAPHIC(10)
		[Column("DATEDATATYPE"     , DataType = DataType.Date     , DbType = "DATE"                                                                                                         )] public DB2Date?         Datedatatype      { get; set; } // DATE
		[Column("TIMEDATATYPE"     , DataType = DataType.Time     , DbType = "TIME"                                                                                                         )] public DB2Time?         Timedatatype      { get; set; } // TIME
		[Column("TIMESTAMPDATATYPE", DataType = DataType.Timestamp, DbType = "TIMESTAMP"                                                                                                    )] public DB2TimeStamp?    Timestampdatatype { get; set; } // TIMESTAMP
		[Column("XMLDATATYPE"      , DataType = DataType.Xml      , DbType = "XML"                                                                                                          )] public DB2Xml?          Xmldatatype       { get; set; } // XML

		#region IEquatable<T> support
		private static readonly IEqualityComparer<Alltype> _equalityComparer = ComparerBuilder.GetEqualityComparer<Alltype>(c => c.Id);

		public bool Equals(Alltype? other)
		{
			return _equalityComparer.Equals(this, other!);
		}

		public override int GetHashCode()
		{
			return _equalityComparer.GetHashCode(this);
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as Alltype);
		}
		#endregion
	}
}