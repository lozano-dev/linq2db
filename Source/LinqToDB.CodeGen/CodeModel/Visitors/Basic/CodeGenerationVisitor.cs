﻿using System;
using System.Collections.Generic;

namespace LinqToDB.CodeGen.Model
{
	/// <summary>
	/// Base code generation visitor that contains public API for code generation.
	/// </summary>
	public abstract class CodeGenerationVisitor : CodeModelVisitor
	{
		private          IndentedWriter _writer;
		private readonly string         _newLine;
		private readonly string         _indent;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="newLine">Sequence of characters, used as new line by code writer.</param>
		/// <param name="indent">Sequence of characters, used as one level of indent.</param>
		protected CodeGenerationVisitor(string newLine, string indent)
		{
			_newLine = newLine;
			_indent  = indent;
			_writer  = new IndentedWriter(newLine, indent);
		}

		/// <summary>
		/// Character sequences, recognized by specific language as new line sequences.
		/// </summary>
		protected abstract string[] NewLineSequences { get; }

		/// <summary>
		/// Gets all code, generated by code generator.
		/// </summary>
		public string GetResult() => _writer.GetText();

		/// <summary>
		/// Emits provided code fragment into generated code as-is.
		/// Prepends fragment with indent if needed (only before first line if multiple lines passed).
		/// </summary>
		/// <param name="text">Code fragment to write to code generator.</param>
		protected void Write              (string text) => _writer.Write(text);
		/// <summary>
		/// Emits provided character into generated code as-is.
		/// Prepends it with indent if needed.
		/// </summary>
		/// <param name="chr">Single character to write to code generator.</param>
		protected void Write              (char   chr ) => _writer.Write(chr);
		/// <summary>
		/// Emits provided code fragment into generated code as-is and then emits new line.
		/// Prepends fragment with indent if needed (only before first line if multiple lines passed).
		/// </summary>
		/// <param name="text">Code fragment to write to code generator.</param>
		protected void WriteLine          (string text) => _writer.WriteLine(text);
		/// <summary>
		/// Emits provided character into generated code as-is and then emits new line.
		/// Prepends it with indent if needed (only before first line if multiple lines passed).
		/// </summary>
		/// <param name="chr">Single character to write to code generator.</param>
		protected void WriteLine          (char   chr ) => _writer.WriteLine(chr);
		/// <summary>
		/// Emits new line.
		/// Does not prepend new line with indent if it is missing.
		/// </summary>
		protected void WriteLine          (           ) => _writer.WriteLine();
		/// <summary>
		/// Emits provided code fragment into generated code as-is without automatic indent generation.
		/// </summary>
		/// <param name="text">Code fragment to write to code generator.</param>
		protected void WriteUnindented    (string text) => _writer.WriteUnindented(text);
		/// <summary>
		/// Emits provided code fragment into generated code as-is without automatic indent generation and then
		/// emits new line sequence.
		/// </summary>
		/// <param name="text">Code fragment to write to code generator.</param>
		protected void WriteUnindentedLine(string text) => _writer.WriteUnindentedLine(text);
		/// <summary>
		/// Increases current indent size by one level.
		/// </summary>
		protected void IncreaseIdent      (           ) => _writer.IncreaseIdent();
		/// <summary>
		/// Decreases current indent size by one level.
		/// Throws exception if indent level goes below zero.
		/// </summary>
		protected void DecreaseIdent      (           ) => _writer.DecreaseIdent();

		/// <summary>
		/// Splits provided text into fragments (lines) using language-specific new line detection logic.
		/// </summary>
		/// <param name="text">Text to split into lines.</param>
		/// <returns>Array of lines (without terminating newline sequences).</returns>
		protected string[] SplitByNewLine(string text) => text.Split(NewLineSequences, StringSplitOptions.None);

		/// <summary>
		/// Generates code for provided code elements with specified delimiter between elements.
		/// </summary>
		/// <typeparam name="T">Code element type.</typeparam>
		/// <param name="items">Code elements to convert to code.</param>
		/// <param name="delimiter">Sequence of characters used as delimiter between code elements.</param>
		/// <param name="newLine">Specify wether method should generate new line sequence after each delimiter.
		/// Also when <c>true</c>, adds new line sequence after last element (without delimiter).</param>
		protected void WriteDelimitedList<T>(IEnumerable<T> items, string delimiter, bool newLine)
			where T: ICodeElement
		{
			var first = true;

			foreach (var item in items)
			{
				if (!first)
				{
					Write(delimiter);
					if (newLine)
						WriteLine();
				}
				else
					first = false;

				Visit(item);
			}

			if (newLine && !first)
				WriteLine();
		}

		/// <summary>
		/// Generates code for provided code elements with specified delimiter between elements.
		/// </summary>
		/// <typeparam name="T">Code element type.</typeparam>
		/// <param name="items">Code elements to convert to code.</param>
		/// <param name="writer">Custom element code generation action.</param>
		/// <param name="delimiter">Sequence of characters used as delimiter between code elements.</param>
		/// <param name="newLine">Specify wether method should generate new line sequence after each delimiter.
		/// Also when <c>true</c>, adds new line sequence after last element (without delimiter).</param>
		protected void WriteDelimitedList<T>(IEnumerable<T> items, Action<T> writer, string delimiter, bool newLine)
		{
			var first = true;
			foreach (var item in items)
			{
				if (!first)
				{
					Write(delimiter);
					if (newLine)
						WriteLine();
				}
				else
					first = false;

				writer(item);
			}

			if (newLine && !first)
				WriteLine();
		}

		/// <summary>
		/// Generates code for provided code elements with new line between elements.
		/// In comparison to <see cref="WriteDelimitedList{T}(IEnumerable{T}, Action{T}, string, bool)"/> doesn't add
		/// new line sequence after last element.
		/// </summary>
		/// <typeparam name="T">Code element type.</typeparam>
		/// <param name="items">Code elements to convert to code.</param>
		protected void WriteNewLineDelimitedList<T>(IEnumerable<T> items)
			where T : ICodeElement
		{
			var first = true;

			foreach (var item in items)
			{
				if (!first)
					WriteLine();
				else
					first = false;

				Visit(item);
			}
		}

		/// <summary>
		/// Executes provided action and returns all code, generated by it.
		/// </summary>
		/// <param name="fragmentBuilder">Code generation action.</param>
		/// <returns>Code, generated by <paramref name="fragmentBuilder"/>.</returns>
		protected string BuildFragment(Action fragmentBuilder)
		{
			var mainWriter = _writer;

			try
			{
				var writer = _writer = new IndentedWriter(_newLine, _indent);
				fragmentBuilder();
				return writer.GetText();
			}
			finally
			{
				_writer = mainWriter;
			}
		}

		/// <summary>
		/// Emits specified code fragment and append spaces after it to match specified by <paramref name="fullLength"/> length (if fragment length is smaller than <paramref name="fullLength"/>).
		/// </summary>
		/// <param name="text">Code fragment to write.</param>
		/// <param name="fullLength">Minimal length of emitted string.</param>
		protected void PadWithSpaces(string text, int fullLength)
		{
			Write(text);
			while (fullLength > text.Length)
			{
				Write(' ');
				fullLength--;
			}
		}

		/// <summary>
		/// Emits provided code fragment and escape characters using XML escaping rules for inner text.
		/// </summary>
		/// <param name="text">Code fragment to write.</param>
		protected void WriteXmlText(string text)
		{
			// TODO: we do not filter/handle disallowed XML characters here yet
			foreach (var chr in text)
			{
				switch (chr)
				{
					case '&' : Write("&amp;" ); break;
					case '>' : Write("&gt;"  ); break;
					case '<' : Write("&lt;"  ); break;
					default  : Write(chr     ); break;
				}
			}
		}

		/// <summary>
		/// Emits provided code fragment and escape characters using XML escaping rules for attributes.
		/// Note that attribute should use " as quotation mark.
		/// </summary>
		/// <param name="text">Code fragment to write.</param>
		/// <param name="doubleQuote">Attribute use " as quotation mark. Otherwise ' used.</param>
		protected void WriteXmlAttribute(string text, bool doubleQuote = true)
		{
			// TODO: we do not filter/handle disallowed XML characters here yet
			foreach (var chr in text)
			{
				switch (chr)
				{
					case '\'' : if (!doubleQuote) Write("&apos;"); else Write(chr); break;
					case '"'  : if ( doubleQuote) Write("&quot;"); else Write(chr); break;

					case '&'  : Write("&amp;" ); break;
					case '>'  : Write("&gt;"  ); break;
					case '<'  : Write("&lt;"  ); break;
					default   : Write(chr     ); break;
				}
			}
		}
	}
}
