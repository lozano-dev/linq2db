﻿using System.Text;
using LinqToDB.CodeModel;
using LinqToDB.DataModel;

namespace LinqToDB.Scaffold
{
	partial class DataModelLoader
	{
		/// <summary>
		/// Creates empty data context class model for current schema and initialize it with basic properties.
		/// </summary>
		/// <returns>Data context model instance.</returns>
		private DataContextModel BuildDataContext()
		{
			var className = _namingServices.NormalizeIdentifier(
				_options.DataModel.DataContextClassNameOptions,
				_options.DataModel.ContextClassName ?? _schemaProvider.DatabaseName ?? "MyDataContext");

			var dataContextClass = new ClassModel(className, className)
			{
				IsPartial = true,
				IsPublic  = true,
				Namespace = _options.CodeGeneration.Namespace
			};

			if (_options.DataModel.BaseContextClass != null)
				dataContextClass.BaseType = _languageProvider.TypeParser.Parse(_options.DataModel.BaseContextClass, false);
			else
				dataContextClass.BaseType = WellKnownTypes.LinqToDB.Data.DataConnection;

			if (_options.DataModel.IncludeDatabaseInfo)
			{
				var summary = new StringBuilder();
				if (_schemaProvider.DatabaseName != null)
					summary.AppendFormat("Database       : {0}", _schemaProvider.DatabaseName ).AppendLine();
				if (_schemaProvider.DataSource != null)
					summary.AppendFormat("Data Source    : {0}", _schemaProvider.DataSource   ).AppendLine();
				if (_schemaProvider.ServerVersion != null)
					summary.AppendFormat("Server Version : {0}", _schemaProvider.ServerVersion).AppendLine();

				if (summary.Length > 0)
					dataContextClass.Summary = summary.ToString();
			}

			var dataContext = new DataContextModel(dataContextClass);

			dataContext.HasDefaultConstructor        = _options.DataModel.HasDefaultConstructor;
			dataContext.HasConfigurationConstructor  = _options.DataModel.HasConfigurationConstructor;
			dataContext.HasUntypedOptionsConstructor = _options.DataModel.HasUntypedOptionsConstructor;
			dataContext.HasTypedOptionsConstructor   = _options.DataModel.HasTypedOptionsConstructor;

			return dataContext;
		}
	}
}
