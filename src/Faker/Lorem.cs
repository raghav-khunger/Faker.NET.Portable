using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Extensions;

namespace Faker
{
	/// <summary>
	///   A collection of Random sentences/words/paragraphs etc. related resources.
	/// </summary>
	/// <include file="Docs/CustomRemarks.xml" path="Comments/SatelliteResource/*" />
	/// <threadsafety static="true" />
	public static class Lorem
	{
		/// <summary>
		///   Generates random characters with the specified <paramref name="charCount">character count</paramref>.
		/// </summary>
		/// <param name="charCount">The character count.</param>
		/// <returns>The random characters.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   Character count must be more than zero.
		/// </exception>
		public static string Characters(int charCount = 255)
		{
			if (charCount <= 0)
			{
				throw new ArgumentOutOfRangeException("charCount", "Character count must be more than zero.");
			}

			byte[] characters = charCount.Times(count => (byte)RandomNumber.Next(33, 126)).ToArray();

			return Encoding.UTF8.GetString(characters, 0, charCount);
		}

		/// <summary>
		///   Gets a random paragraph.
		/// </summary>
		/// <returns>The random paragraph.</returns>
		/// <seealso cref="Paragraph(int)" />
		public static string Paragraph()
		{
			// ReSharper disable once ExceptionNotDocumentedOptional
			return Paragraph(3);
		}

		/// <summary>
		///   Gets a random paragraph.
		/// </summary>
		/// <param name="minSentenceCount">The minimum sentence count.</param>
		/// <returns>The random paragraph.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		///   Minimum sentence count must be greater than zero.
		/// </exception>
		/// <seealso cref="Paragraph()" />
		/// <seealso cref="Sentences(int)" />
		public static string Paragraph(int minSentenceCount)
		{
			if (minSentenceCount <= 0)
			{
				throw new ArgumentOutOfRangeException(
					"minSentenceCount",
					"Minimum sentence count must be greater than zero.");
			}

			return string.Join(" ", Sentences(minSentenceCount + RandomNumber.Next(3)));
		}

		/// <summary>
		///   Gets a collection of random paragraphs.
		/// </summary>
		/// <param name="paragraphCount">The paragraph count.</param>
		/// <returns>The collection of random paragraphs.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   Paragraph Count must be greater than zero
		/// </exception>
		/// <seealso cref="Paragraph()" />
		public static IEnumerable<string> Paragraphs(int paragraphCount)
		{
			if (paragraphCount <= 0)
			{
				throw new ArgumentOutOfRangeException("paragraphCount", "Paragraph Count must be greater than zero");
			}

			return paragraphCount.Times(x => Paragraph());
		}

		/// <summary>
		///   Gets a random sentence.
		/// </summary>
		/// <returns>The random sentence.</returns>
		/// <seealso cref="Sentence(int)" />
		public static string Sentence()
		{
			// ReSharper disable once ExceptionNotDocumentedOptional
			return Sentence(4);
		}

		/// <summary>
		///   Gets a random sentence.
		/// </summary>
		/// <param name="minWordCount">The minimum word count.</param>
		/// <returns>The random sentence</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		///   Minimum word count must be greater than zero
		/// </exception>
		/// <seealso cref="Sentence()" />
		/// <seealso cref="Words(int)" />
		public static string Sentence(int minWordCount)
		{
			if (minWordCount <= 0)
			{
				throw new ArgumentOutOfRangeException("minWordCount", "Minimum word count must be greater than zero");
			}

			return string.Join(" ", Words(minWordCount + RandomNumber.Next(6))).Capitalise() + ".";
		}

		/// <summary>
		///   Gets a collection of random sentences.
		/// </summary>
		/// <param name="sentenceCount">The sentence count.</param>
		/// <returns>A collection of random sentences.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   Sentence count must be greater than zero
		/// </exception>
		/// <seealso cref="Sentence()" />
		public static IEnumerable<string> Sentences(int sentenceCount)
		{
			if (sentenceCount <= 0)
			{
				throw new ArgumentOutOfRangeException("sentenceCount", "Sentence count must be greater than zero");
			}

			return sentenceCount.Times(x => Sentence());
		}

		/// <summary>
		///   Gets the specified <paramref name="count" /> of random words.
		/// </summary>
		/// <param name="count">The count of random words.</param>
		/// <returns>An Enumerable of random words.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Count must be greater than zero</exception>
		public static IEnumerable<string> Words(int count)
		{
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be greater than zero");
			}

			return count.Times(x => Resources.Lorem.Words.Split(Config.SEPARATOR).Random());
		}

		public static string ContentTitle()
		{
			return ContentTitle(0);
		}

		public static string ContentTitle(int numberOfWords)
		{
			if (numberOfWords <= 0)
			{
				numberOfWords = RandomNumber.Next(3, 8);
			}

			string contentTitle = string.Join(" ", Words(numberOfWords)).Capitalise();
			return contentTitle;
		}

		public static string ContentTags()
		{
			return ContentTags(0);
		}

		public static string ContentTags(int numberOfTags)
		{
			if (numberOfTags <= 0)
			{
				numberOfTags = RandomNumber.Next(2, 5);
			}

			string tags = string.Join(",", Words(numberOfTags).Cast<string>()
								.Where(c => !string.IsNullOrWhiteSpace(c))
								.Distinct());
			return tags;
		}
	}
}
