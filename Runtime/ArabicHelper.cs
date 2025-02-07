using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArabicSupport
{
	public static class ArabicHelper
	{
		public static int HandleInduNumber(int letterOrigin, int letterFinal)
		{
			if (letterOrigin >= 48 && letterOrigin <= 57)
				return 1632 + (letterOrigin - 48);
			return letterFinal;
		}

		public static bool DoCharCheck(char c, CharCheckConfig config)
		{
			if (config.disconnectedLetter && ArabicCollections.disconnectedLetters.Contains(c)) return true;
			if (config.number && char.IsNumber(c)) return true;
			if (config.symbol && char.IsSymbol(c)) return true;
			if (config.punctuation && char.IsPunctuation(c)) return true;
			if (config.lower && char.IsLower(c)) return true;
			if (config.upper && char.IsUpper(c)) return true;
			
			return false;
		}

		public struct CharCheckConfig
		{
			public bool number, symbol, punctuation, lower, upper, disconnectedLetter;
		}
	}
}
