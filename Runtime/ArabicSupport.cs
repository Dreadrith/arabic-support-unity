/// <summary>
/// Arabic-Support-Unity forked, optimized and modified by Dreadrith.
/// License excluded from comments as its already part of the repository.
/// Rest is same as original
/// ---
/// This is an Open Source File Created by: Abdullah Konash. Twitter: @konash
/// This File allow the users to use arabic text in XNA and Unity platform.
/// It flips the characters and replace them with the appropriate ones to connect the letters in the correct way.
/// 
/// The project is available on GitHub here: https://github.com/Konash/arabic-support-unity
/// Unity Asset Store link: https://www.assetstore.unity3d.com/en/#!/content/2674
/// Please help in improving the plugin. 
/// 
/// I would love to see the work you use this plugin for. Send me a copy at: abdullah.konash[at]gmail[dot]com
/// </summary>

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace ArabicSupport
{
	public static class ArabicFixer
	{
		// public static string Fix(string str, bool rtl)
		// {
		// 	if (rtl) return Fix(str);
		//
		// 	string[] words = str.Split(' ');
		// 	string result = "";
		// 	string arabicToIgnore = "";
		// 	foreach (string word in words)
		// 	{
		// 		if (char.IsLower(word.ToLower()[word.Length / 2]))
		// 		{
		// 			result += Fix(arabicToIgnore) + word + " ";
		// 			arabicToIgnore = "";
		// 		}
		// 		else arabicToIgnore += word + " ";
		// 	}
		//
		// 	if (arabicToIgnore != "") result += Fix(arabicToIgnore);
		//
		// 	return result;
		// }

		public static string Fix(string str) => Fix(str, true, false, true);
		public static string Fix(string str, bool showTashkeel, bool useHinduNumbers) => Fix(str, showTashkeel, false, useHinduNumbers);
		public static string Fix(string str, bool showTashkeel, bool combineTashkeel, bool useHinduNumbers)
		{
			if (str.Contains("\n")) str = str.Replace("\n", Environment.NewLine);
			string[] strSplit = str.Split( new [] {Environment.NewLine}, StringSplitOptions.None);
			return string.Join(Environment.NewLine, strSplit.Select(s => ArabicFixerTool.FixLine(s, showTashkeel, combineTashkeel, useHinduNumbers)));
		}
	}
	
	internal static class ArabicFixerTool
	{
		public static char Convert(char toBeConverted) => ArabicCollections.GeneralToIsolatedMapping.GetValueOrDefault(toBeConverted, toBeConverted);
		
		internal static readonly StringBuilder internalStringBuilder = new StringBuilder();
		
		internal static string FixLine(string str, bool showTashkeel = true, bool combineTashkeel = true, bool useHinduNumbers = true)
		{
			str = RemoveTashkeel(str, out var tashkeelLocations, combineTashkeel);

			char[] lettersOrigin = new char[str.Length];
			char[] lettersFinal = str.ToCharArray();

			for (int i = 0; i < lettersOrigin.Length; i++)
				lettersOrigin[i] = Convert(str[i]);
			
			for (int i = 0; i < lettersOrigin.Length; i++)
			{
				bool skip = false;

				// For special Lam Letter connections.
				if (lettersOrigin[i] == (char) IsolatedArabicLetters.Lam)
				{
					if (i < lettersOrigin.Length - 1)
					if (ArabicCollections.LamCombinationMapping.TryGetValue(lettersOrigin[i + 1], out var v))
					{
						lettersOrigin[i] = v;
						lettersFinal[i + 1] = (char) 0xFFFF;
						skip = true;
					}
				}


				if (!IsIgnoredCharacter(lettersOrigin[i]))
				{
					if (IsMiddleLetter(lettersOrigin, i))
						lettersFinal[i] = (char) (lettersOrigin[i] + 3);
					else if (IsFinishingLetter(lettersOrigin, i))
						lettersFinal[i] = (char) (lettersOrigin[i] + 1);
					else if (IsLeadingLetter(lettersOrigin, i))
						lettersFinal[i] = (char) (lettersOrigin[i] + 2);
				}

				if (skip) i++;

				//changing numbers to hindu
				if (useHinduNumbers) lettersFinal[i] = (char) ArabicHelper.HandleInduNumber(lettersOrigin[i], lettersFinal[i]);
			}

			//Return the Tashkeel to their places.
			if (showTashkeel && tashkeelLocations.Count > 0)
				ReturnTashkeel(ref lettersFinal, tashkeelLocations);

			internalStringBuilder.Clear();
			internalStringBuilder.EnsureCapacity(lettersFinal.Length);

			List<char> numbersList = new List<char>();

			void AppendNumbers()
			{
				if (numbersList.Count > 0)
				{
					numbersList.Reverse();
					internalStringBuilder.Append(new string(numbersList.ToArray()));
					numbersList.Clear();
				}
			}

			for (int i = lettersFinal.Length - 1; i >= 0; i--)
			{
				if (char.IsPunctuation(lettersFinal[i]) && i > 0 && i < lettersFinal.Length - 1 &&
				    (char.IsPunctuation(lettersFinal[i - 1]) || char.IsPunctuation(lettersFinal[i + 1])))
				{
					switch (lettersFinal[i])
					{
						case '(': internalStringBuilder.Append(')'); break;
						case ')': internalStringBuilder.Append('('); break;
						case '<': internalStringBuilder.Append('>'); break;
						case '>': internalStringBuilder.Append('<'); break;
						case '[': internalStringBuilder.Append(']'); break;
						case ']': internalStringBuilder.Append('['); break;
						default:
						{
							if (lettersFinal[i] != 0xFFFF)
								internalStringBuilder.Append(lettersFinal[i]);
							break;
						}
					}
				}
				// For cases where english words and arabic are mixed. This allows for using arabic, english and numbers in one sentence.
				else if (lettersFinal[i] == ' ' && i > 0 && i < lettersFinal.Length - 1 &&
				         (char.IsLower(lettersFinal[i - 1]) || char.IsUpper(lettersFinal[i - 1]) || char.IsNumber(lettersFinal[i - 1])) &&
				         (char.IsLower(lettersFinal[i + 1]) || char.IsUpper(lettersFinal[i + 1]) || char.IsNumber(lettersFinal[i + 1])))

				{
					numbersList.Add(lettersFinal[i]);
				}

				else if (char.IsNumber(lettersFinal[i]) || char.IsLower(lettersFinal[i]) ||
				         char.IsUpper(lettersFinal[i]) || char.IsSymbol(lettersFinal[i]) ||
				         char.IsPunctuation(lettersFinal[i])) // || lettersFinal[i] == '^') //)
				{
					switch (lettersFinal[i])
					{
						case '(': numbersList.Add(')'); break;
						case ')': numbersList.Add('('); break;
						case '<': numbersList.Add('>'); break;
						case '>': numbersList.Add('<'); break;
						case '[': internalStringBuilder.Append(']'); break;
						case ']': internalStringBuilder.Append('['); break;
						default:
							numbersList.Add(lettersFinal[i]);
							break;
					}
				}
				else if ((lettersFinal[i] >= (char) 0xD800 && lettersFinal[i] <= (char) 0xDBFF) ||
				         (lettersFinal[i] >= (char) 0xDC00 && lettersFinal[i] <= (char) 0xDFFF))
				{
					numbersList.Add(lettersFinal[i]);
				}
				else
				{
					AppendNumbers();
					if (lettersFinal[i] != 0xFFFF)
						internalStringBuilder.Append(lettersFinal[i]);

				}
			}

			AppendNumbers();

			return internalStringBuilder.ToString();
		}
		
		#region Tashkeel
		internal static string RemoveTashkeel(string text, out List<(char, int)> tashkeelLocations, bool combineTashkeel = true)
		{
			var tls = tashkeelLocations = new List<(char, int)>();
			var str = text;
			var lastSplitIndex = 0;
			internalStringBuilder.Clear();
			internalStringBuilder.EnsureCapacity(str.Length);

			int index = 0;

			void IncrementSB(int i)
			{
				if (i - lastSplitIndex > 0)
					internalStringBuilder.Append(str, lastSplitIndex, i - lastSplitIndex);
				lastSplitIndex = i + 1;
			}

			for (int i = 0; i < str.Length; i++)
			{
				void AddTashkeelLetter()
				{
					tls.Add((str[i], i));
					index++;
					IncrementSB(i);
				}
				
				switch (str[i])
				{
					case (char) Tashkeels.Fatha:
					case (char) Tashkeels.Damma:
					case (char) Tashkeels.Kasra:
					{
						if (index == 0 || !combineTashkeel) AddTashkeelLetter();
						else
						{
							var (t, ind) = tashkeelLocations[index - 1];
							if (t == (char) Tashkeels.Shadda)
							{
								tashkeelLocations[index - 1] = (ArabicCollections.TashkeelToShadda[str[i]], ind);
								IncrementSB(i);
							}
						}
						break;
					}
					case (char) Tashkeels.Shadda:
					{
						if (index == 0 || !combineTashkeel) AddTashkeelLetter();
						else
						{
							var (t, ind) = tashkeelLocations[index - 1];
							if (ArabicCollections.TashkeelToShadda.TryGetValue(t, out var value))
							{
								tashkeelLocations[index - 1] = (value, ind);
								IncrementSB(i);

							}
						}
						break;
					}
					
					case (char) Tashkeels.ShaddaFatha:
					case (char) Tashkeels.ShaddaDamma:
					case (char) Tashkeels.ShaddaKasra:
						IncrementSB(i);
						break;
					case (char) Tashkeels.TanweenFatha:
					case (char) Tashkeels.TanweenDamma:
					case (char) Tashkeels.TanweenKasra:
					case (char) Tashkeels.Sukun:
					case (char) Tashkeels.Maddah:
						AddTashkeelLetter();
						break;
				}
			}

			if (lastSplitIndex != 0)
			{
				IncrementSB(str.Length);
				str = internalStringBuilder.ToString();
			}
			return str;
		}
		internal static void ReturnTashkeel(ref char[] letters, List<(char, int)> tashkeelLocation)
		{
			Array.Resize(ref letters, letters.Length + tashkeelLocation.Count);

			foreach (var tl in tashkeelLocation)
			{
				for (int j = letters.Length - 1; j > tl.Item2; j--)
					letters[j] = letters[j - 1];
				
				if (tl.Item2 >= 0 && tl.Item2 < letters.Length) 
					letters[tl.Item2] = tl.Item1;
			}
		}
		#endregion
		
		#region Character Checks
		/// <summary>
		/// English letters, numbers and punctuation characters are ignored. This checks if the ch is an ignored character.
		/// </summary>
		/// <param name="ch">The character to be checked for skipping</param>
		/// <returns>True if the character should be ignored, false if it should not be ignored.</returns>
		internal static bool IsIgnoredCharacter(char ch)
		{
			if (ArabicHelper.DoCharCheck(ch, new ArabicHelper.CharCheckConfig() {number = true, symbol = true, punctuation = true, lower = true, upper = true,})) return true;
			
			bool isPersianCharacter = ch == (char) 0xFB56 || ch == (char) 0xFB7A || ch == (char) 0xFB8A || ch == (char) 0xFB92 || ch == (char) 0xFB8E;
			bool isPresentationFormB = (ch <= (char) 0xFEFF && ch >= (char) 0xFE70);
			bool isAcceptableCharacter = isPresentationFormB || isPersianCharacter || ch == (char) 0xFBFC;

			return  !isAcceptableCharacter || ch == 'a' || ch == '>' || ch == '<' || ch == (char) 0x061B;
		}
		
		/// <summary>
		/// Checks if the letter at index value is a leading character in Arabic or not.
		/// </summary>
		/// <param name="letters">The whole word that contains the character to be checked</param>
		/// <param name="index">The index of the character to be checked</param>
		/// <returns>True if the character at index is a leading character, else, returns false</returns>
		internal static bool IsLeadingLetter(char[] letters, int index)
		{
			if (index >= letters.Length) return false;
			bool isFirstLetter = index == 0;
			bool isLastLetter = index == letters.Length - 1;
			bool lettersThatCannotBeBeforeALeadingLetter = isFirstLetter;
			bool lettersThatCannotBeALeadingLetter = false;
			bool lettersThatCannotBeAfterLeadingLetter = false;
			
			if (!isFirstLetter)
			{
				var c = letters[index - 1];
				lettersThatCannotBeBeforeALeadingLetter =
					ArabicHelper.DoCharCheck(c, new ArabicHelper.CharCheckConfig()
					{
						punctuation = true,
						disconnectedLetter = true
					})
					|| c == ' '
					|| c == '*' // ??? Remove?
					|| c == 'A' // ??? Remove?
					|| c == '>'
					|| c == '<';

			}
			
			{
				var c = letters[index];
				lettersThatCannotBeALeadingLetter = c != ' ' && !ArabicCollections.disconnectedLetters.Contains(c);
			}
			
			if (!isLastLetter)
			{
				var c = letters[index + 1];
				lettersThatCannotBeAfterLeadingLetter =
					!ArabicHelper.DoCharCheck(c, new ArabicHelper.CharCheckConfig() {punctuation = true, number = true, symbol = true, upper = true, lower = true})
					&& c != (int) IsolatedArabicLetters.Hamza
					&& c != ' '
					&& c != '\n'
					&& c != '\r';
			}

			return lettersThatCannotBeBeforeALeadingLetter && lettersThatCannotBeALeadingLetter && lettersThatCannotBeAfterLeadingLetter;
		}

		/// <summary>
		/// Checks if the letter at index value is a finishing character in Arabic or not.
		/// </summary>
		/// <param name="letters">The whole word that contains the character to be checked</param>
		/// <param name="index">The index of the character to be checked</param>
		/// <returns>True if the character at index is a finishing character, else, returns false</returns>
		internal static bool IsFinishingLetter(char[] letters, int index)
		{
			if (index >= letters.Length) return false;
			bool isFirstLetter = index == 0;
			bool lettersThatCannotBeBeforeAFinishingLetter = false;
			bool lettersThatCannotBeFinishingLetters = false;
			
			if (!isFirstLetter)
			{
				var c = letters[index - 1];
				lettersThatCannotBeBeforeAFinishingLetter =
					!ArabicHelper.DoCharCheck(c, new ArabicHelper.CharCheckConfig() {disconnectedLetter = true, punctuation = true, symbol = true})
					&& c != ' '
					&& c != '>'
					&& c != '<';
			}

			{
				var c = letters[index];
				lettersThatCannotBeFinishingLetters = c != ' ' && c != (int) IsolatedArabicLetters.Hamza;
			}
			return lettersThatCannotBeBeforeAFinishingLetter && lettersThatCannotBeFinishingLetters;
		}

		/// <summary>
		/// Checks if the letter at index value is a middle character in Arabic or not.
		/// </summary>
		/// <param name="letters">The whole word that contains the character to be checked</param>
		/// <param name="index">The index of the character to be checked</param>
		/// <returns>True if the character at index is a middle character, else, returns false</returns>
		internal static bool IsMiddleLetter(char[] letters, int index)
		{
			if (index >= letters.Length) return false;
			char c;
			bool isFirstLetter = index == 0;
			bool isLastLetter = index == letters.Length - 1;
			bool lettersThatCannotBeMiddleLetters = false;
			bool lettersThatCannotBeBeforeMiddleCharacters = false;
			bool lettersThatCannotBeAfterMiddleCharacters = false;

			if (!isFirstLetter)
			{
				lettersThatCannotBeMiddleLetters = !ArabicCollections.disconnectedLetters.Contains(letters[index]);
		
				c = letters[index - 1];
				lettersThatCannotBeBeforeMiddleCharacters =
					!ArabicCollections.disconnectedLetters.Contains(c)
					&& !char.IsPunctuation(c)
					&& c != '>'
					&& c != '<'
					&& c != ' '
					&& c != '*';
			}

			if (!isLastLetter)
			{
				c = letters[index + 1];
				lettersThatCannotBeAfterMiddleCharacters =
					!ArabicHelper.DoCharCheck(c, new ArabicHelper.CharCheckConfig() {punctuation = true, number = true, symbol = true})
					&& c != ' '
					&& c != '\r'
					&& c != (int) IsolatedArabicLetters.Hamza;
			}
			
			return lettersThatCannotBeAfterMiddleCharacters &&
			       lettersThatCannotBeBeforeMiddleCharacters &&
			       lettersThatCannotBeMiddleLetters;
		}
		
		#endregion
	}
}
