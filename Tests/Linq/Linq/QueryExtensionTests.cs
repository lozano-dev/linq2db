﻿using System;
using System.Linq;
using System.Linq.Expressions;

using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Expressions;
using LinqToDB.Linq;

using NUnit.Framework;

namespace Tests.Linq
{
	using Model;

	[TestFixture]
	public class QueryExtensionTests : TestBase
	{
		[Test]
		public void EmptyTest([DataSources] string context)
		{
			using var db = GetDataContext(context);
			_ = db.Parent.Empty().ToList();
		}

		[Test]
		public void EmptyTest2([DataSources] string context)
		{
			using var db = GetDataContext(context);

			_ =
			(
				from p in db.Parent.Empty()
				from c in db.Child.John()
				where p.ParentID == c.ParentID
				select new { p, c }
			)
			.ToList();
		}

		[Test]
		public void TableTest([DataSources] string context)
		{
			using var db = GetDataContext(context);

			_ = db.Parent.Comment(t => t.ParentID, "oh yeah").ToList();
		}

		[Test]
		public void TableTest2([DataSources] string context)
		{
			using var db = GetDataContext(context);

			_ =
			(
				from p in db.Parent.Comment(t => t.ParentID, "oh yeah")
				join c in db.Child.Empty() on p.ParentID equals c.ParentID
				select new { p, c }
			)
			.ToList();
		}

		[Test]
		public void SqlServerWithTableHintTest([IncludeDataSources(true, TestProvName.AllSqlServer)] string context)
		{
			using var db = GetDataContext(context);

			var q =
				from p in db.Parent.With(TableHint.NoLock).With(TableHint.NoWait)
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring("WITH (NoLock, NoWait)"));
		}

		[Test]
		public void SqlServerWith2005TableHintTest(
			[IncludeDataSources(true, TestProvName.AllSqlServer2005Plus)] string context,
			[Values(
				TableHint.HoldLock,
				TableHint.NoLock,
				TableHint.NoWait,
				TableHint.PagLock,
				TableHint.ReadCommitted,
				TableHint.ReadCommittedLock,
				TableHint.ReadPast,
				TableHint.ReadUncommitted,
				TableHint.RepeatableRead,
				TableHint.RowLock,
				TableHint.Serializable,
				TableHint.TabLock,
				TableHint.TabLockX,
				TableHint.UpdLock,
				TableHint.XLock
				)] TableHint hint)
		{
			using var db = GetDataContext(context);

			var q =
				from p in db.Parent.With(hint)
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring($"WITH ({hint})"));
		}

		[Test]
		public void SqlServerWith2012TableHintTest(
			[IncludeDataSources(true, TestProvName.AllSqlServer2012Plus)] string context,
			[Values(
				TableHint.ForceScan
//				TableHint.ForceSeek,
//				TableHint.Snapshot
				)] TableHint hint)
		{
			using var db = GetDataContext(context);

			var q =
				from p in db.Parent.With(hint)
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring($"WITH ({hint})"));
		}

		[Test]
		public void SqlServerWithSpatialWindowMaxCellsTableHintTest([IncludeDataSources(true, TestProvName.AllSqlServer2012Plus)] string context)
		{
			using var db = GetDataContext(context);

			var q =
				from p in db.Parent.With(TableHint.SpatialWindowMaxCells, 10)
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring("WITH (SPATIAL_WINDOW_MAX_CELLS=10)"));
		}

		[Test]
		public void SqlServerWithIndexTableHintTest([IncludeDataSources(true, TestProvName.AllSqlServer)] string context)
		{
			using var db = GetDataContext(context);

			var q =
				from p in db.Child.WithIndex("IX_ChildIndex").With(TableHint.NoLock)
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring("WITH (Index(IX_ChildIndex), NoLock)"));
		}

		[Test, Explicit]
		public void SqlServerWithForceSeekTableHintTest([IncludeDataSources(true, TestProvName.AllSqlServer2012Plus)] string context)
		{
			using var db = GetDataContext(context);

			var q =
				from p in db.Child.WithForceSeek("IX_ChildIndex", c => c.ParentID)
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring("WITH (ForceSeek (IX_ChildIndex (ParentID)))"));
		}

		[Test, Explicit]
		public void SqlServerWithForceSeekTableHintTest2([IncludeDataSources(true, TestProvName.AllSqlServer2012Plus)] string context)
		{
			using var db = GetDataContext(context);

			var q =
				from p in db.Child.WithForceSeek("IX_ChildIndex")
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring("WITH (ForceSeek (IX_ChildIndex))"));
		}

		[Test]
		public void SqlServerJoinHintTest(
			[IncludeDataSources(true, TestProvName.AllSqlServer)] string context,
			[Values(JoinHint.Loop, JoinHint.Hash, JoinHint.Merge, JoinHint.Remote)] JoinHint hint)
		{
			using var db = GetDataContext(context);

			var q =
				from c in db.Child
				join p in db.Parent.JoinHint(hint) on c.ParentID equals p.ParentID
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring($"INNER {hint.ToString().ToUpper()} JOIN"));
		}

		[Test]
		public void SqlServerSubQueryJoinHintTest(
			[IncludeDataSources(true, TestProvName.AllSqlServer)] string context,
			[Values(JoinHint.Loop, JoinHint.Hash, JoinHint.Merge, JoinHint.Remote)] JoinHint hint)
		{
			using var db = GetDataContext(context);

			var q =
				from c in db.Child
				join p in
				(
					from t in db.Parent
					where t.Children.Any()
					select new { t.ParentID, t.Children.Count }
				).JoinHint(hint) on c.ParentID equals p.ParentID
				select p;

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring($"INNER {hint.ToString().ToUpper()} JOIN"));
		}

		[Test]
		public void SqlServerJoinMethodHintTest(
			[IncludeDataSources(true, TestProvName.AllSqlServer)] string context,
			[Values(JoinHint.Loop, JoinHint.Hash, JoinHint.Merge)] JoinHint hint,
			[Values(SqlJoinType.Left, SqlJoinType.Full)] SqlJoinType joinType)
		{
			using var db = GetDataContext(context);

			var q = db.Child.Join(db.Parent.JoinHint(hint), joinType, (c, p) => c.ParentID == p.ParentID, (c, p) => p);

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring($"{joinType.ToString().ToUpper()} {hint.ToString().ToUpper()} JOIN"));
		}

		[Test]
		public void SqlServerInnerJoinMethodHintTest(
			[IncludeDataSources(true, TestProvName.AllSqlServer)] string context,
			[Values(JoinHint.Loop, JoinHint.Hash, JoinHint.Merge, JoinHint.Remote)] JoinHint hint)
		{
			using var db = GetDataContext(context);

			var q = db.Child.InnerJoin(db.Parent.JoinHint(hint), (c, p) => c.ParentID == p.ParentID, (c, p) => p);

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring($"INNER {hint.ToString().ToUpper()} JOIN"));
		}

		[Test]
		public void SqlServerRightJoinMethodHintTest(
			[IncludeDataSources(true, TestProvName.AllSqlServer)] string context,
			[Values(JoinHint.Hash, JoinHint.Merge)] JoinHint hint)
		{
			using var db = GetDataContext(context);

			var q = db.Child.RightJoin(db.Parent.JoinHint(hint), (c, p) => c.ParentID == p.ParentID, (c, p) => p);

			_ = q.ToList();

			Assert.That(q.ToString(), Contains.Substring($"RIGHT {hint.ToString().ToUpper()} JOIN"));
		}
	}

	public static class QueryExtensions
	{
		[Sql.QueryExtension(Sql.QueryExtensionScope.Table)]
		public static ITable<T> Empty<T>(this ITable<T> table)
			where T : notnull
		{
			table.Expression = Expression.Call(
				null,
				MethodHelper.GetMethodInfo(Empty, table),
				table.Expression);

			return table;
		}

		[Sql.QueryExtension(Sql.QueryExtensionScope.Join)]
		public static IQueryable<TSource> John<TSource>(this IQueryable<TSource> source)
			where TSource : notnull
		{
			var currentSource = LinqExtensions.ProcessSourceQueryable?.Invoke(source) ?? source;

			return currentSource.Provider.CreateQuery<TSource>(
				Expression.Call(
					null,
					MethodHelper.GetMethodInfo(John, source),
					currentSource.Expression));
		}

		[Sql.QueryExtension(Sql.QueryExtensionScope.Table)]
		public static ITable<TSource> Comment<TSource,TValue>(
			this ITable<TSource>             table,
			Expression<Func<TSource,TValue>> expr,
			[SqlQueryDependent] string       comment)
			where TSource : notnull
		{
			table.Expression = Expression.Call(
				null,
				MethodHelper.GetMethodInfo(Comment, table, expr, comment),
				table.Expression, Expression.Quote(expr), Expression.Constant(comment));

			return table;
		}
	}
}