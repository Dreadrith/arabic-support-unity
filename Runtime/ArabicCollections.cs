using System.Collections.Generic;

namespace ArabicSupport
{
	public static class ArabicCollections
	{ 
		public static readonly Dictionary<char, char> GeneralToIsolatedMapping = new Dictionary<char, char>() {
			{(char) GeneralArabicLetters.Hamza, (char) IsolatedArabicLetters.Hamza},
			{(char) GeneralArabicLetters.Alef, (char) IsolatedArabicLetters.Alef},
			{(char) GeneralArabicLetters.AlefHamza, (char) IsolatedArabicLetters.AlefHamza},
			{(char) GeneralArabicLetters.AlefMad, (char) IsolatedArabicLetters.AlefMad},
			{(char) GeneralArabicLetters.AlefMaksoor, (char) IsolatedArabicLetters.AlefMaksoor},
			{(char) GeneralArabicLetters.AlefMagsora, (char) IsolatedArabicLetters.AlefMaksora},
			{(char) GeneralArabicLetters.WawHamza, (char) IsolatedArabicLetters.WawHamza},
			{(char) GeneralArabicLetters.HamzaNabera, (char) IsolatedArabicLetters.HamzaNabera},
			{(char) GeneralArabicLetters.Ba, (char) IsolatedArabicLetters.Ba},
			{(char) GeneralArabicLetters.Ta, (char) IsolatedArabicLetters.Ta},
			{(char) GeneralArabicLetters.Tha2, (char) IsolatedArabicLetters.Tha2},
			{(char) GeneralArabicLetters.Jeem, (char) IsolatedArabicLetters.Jeem},
			{(char) GeneralArabicLetters.H7aa, (char) IsolatedArabicLetters.H7aa},
			{(char) GeneralArabicLetters.Khaa2, (char) IsolatedArabicLetters.Khaa2},
			{(char) GeneralArabicLetters.Dal, (char) IsolatedArabicLetters.Dal},
			{(char) GeneralArabicLetters.Thal, (char) IsolatedArabicLetters.Thal},
			{(char) GeneralArabicLetters.Ra2, (char) IsolatedArabicLetters.Ra2},
			{(char) GeneralArabicLetters.Zeen, (char) IsolatedArabicLetters.Zeen},
			{(char) GeneralArabicLetters.Seen, (char) IsolatedArabicLetters.Seen},
			{(char) GeneralArabicLetters.Sheen, (char) IsolatedArabicLetters.Sheen},
			{(char) GeneralArabicLetters.S9a, (char) IsolatedArabicLetters.S9a},
			{(char) GeneralArabicLetters.Dha, (char) IsolatedArabicLetters.Dha},
			{(char) GeneralArabicLetters.T6a, (char) IsolatedArabicLetters.T6a},
			{(char) GeneralArabicLetters.T6ha, (char) IsolatedArabicLetters.T6ha},
			{(char) GeneralArabicLetters.Ain, (char) IsolatedArabicLetters.Ain},
			{(char) GeneralArabicLetters.Gain, (char) IsolatedArabicLetters.Gain},
			{(char) GeneralArabicLetters.Fa, (char) IsolatedArabicLetters.Fa},
			{(char) GeneralArabicLetters.Gaf, (char) IsolatedArabicLetters.Gaf},
			{(char) GeneralArabicLetters.Kaf, (char) IsolatedArabicLetters.Kaf},
			{(char) GeneralArabicLetters.Lam, (char) IsolatedArabicLetters.Lam},
			{(char) GeneralArabicLetters.Meem, (char) IsolatedArabicLetters.Meem},
			{(char) GeneralArabicLetters.Noon, (char) IsolatedArabicLetters.Noon},
			{(char) GeneralArabicLetters.Ha, (char) IsolatedArabicLetters.Ha},
			{(char) GeneralArabicLetters.Waw, (char) IsolatedArabicLetters.Waw},
			{(char) GeneralArabicLetters.Ya, (char) IsolatedArabicLetters.Ya},
			{(char) GeneralArabicLetters.TaMarboota, (char) IsolatedArabicLetters.TaMarboota},
			{(char) GeneralArabicLetters.PersianPe, (char) IsolatedArabicLetters.PersianPe},
			{(char) GeneralArabicLetters.PersianChe, (char) IsolatedArabicLetters.PersianChe},
			{(char) GeneralArabicLetters.PersianZe, (char) IsolatedArabicLetters.PersianZe},
			{(char) GeneralArabicLetters.PersianGaf, (char) IsolatedArabicLetters.PersianGaf},
			{(char) GeneralArabicLetters.PersianGaf2, (char) IsolatedArabicLetters.PersianGaf2},
			{(char) GeneralArabicLetters.PersianYeh, (char) IsolatedArabicLetters.PersianYeh},
		};

		public static readonly Dictionary<char, char> TashkeelToShadda = new Dictionary<char, char>()
		{
			{(char)Tashkeels.Fatha, (char)Tashkeels.ShaddaFatha},
			{(char)Tashkeels.Damma, (char)Tashkeels.ShaddaDamma},
			{(char)Tashkeels.Kasra, (char)Tashkeels.ShaddaKasra}
		};

		public static readonly Dictionary<char, char> LamCombinationMapping = new Dictionary<char, char>()
		{
			{(char) IsolatedArabicLetters.Alef, (char)LamCombinations.LamAlef},
			{(char)IsolatedArabicLetters.AlefHamza, (char)LamCombinations.LamAlefFatha},
			{(char) IsolatedArabicLetters.AlefMaksoor, (char)LamCombinations.LamAlefKasra},
			{(char)IsolatedArabicLetters.AlefMad, (char)LamCombinations.LamAlefMad},
		};

		public static readonly HashSet<int> disconnectedLetters = new HashSet<int>()
		{
			(int) IsolatedArabicLetters.Waw,
			(int) IsolatedArabicLetters.WawHamza,
			(int) IsolatedArabicLetters.Hamza,
			(int) IsolatedArabicLetters.Alef,
			(int) IsolatedArabicLetters.AlefMad,
			(int) IsolatedArabicLetters.AlefHamza,
			(int) IsolatedArabicLetters.AlefMaksoor,
			(int) IsolatedArabicLetters.Dal,
			(int) IsolatedArabicLetters.Thal,
			(int) IsolatedArabicLetters.Ra2,
			(int) IsolatedArabicLetters.Zeen,
			(int) IsolatedArabicLetters.PersianZe,
		};
	}
	
	/// <summary>
	/// Arabic Contextual forms - Isolated
	/// </summary>
	public enum GeneralArabicLetters
	{
		Hamza = 0x0621,
		Alef = 0x0627,
		AlefHamza = 0x0623,
		WawHamza = 0x0624,
		AlefMaksoor = 0x0625,
		AlefMagsora = 0x0649,
		HamzaNabera = 0x0626,
		Ba = 0x0628,
		Ta = 0x062A,
		Tha2 = 0x062B,
		Jeem = 0x062C,
		H7aa = 0x062D,
		Khaa2 = 0x062E,
		Dal = 0x062F,
		Thal = 0x0630,
		Ra2 = 0x0631,
		Zeen = 0x0632,
		Seen = 0x0633,
		Sheen = 0x0634,
		S9a = 0x0635,
		Dha = 0x0636,
		T6a = 0x0637,
		T6ha = 0x0638,
		Ain = 0x0639,
		Gain = 0x063A,
		Fa = 0x0641,
		Gaf = 0x0642,
		Kaf = 0x0643,
		Lam = 0x0644,
		Meem = 0x0645,
		Noon = 0x0646,
		Ha = 0x0647,
		Waw = 0x0648,
		Ya = 0x064A,
		AlefMad = 0x0622,
		TaMarboota = 0x0629,
		PersianPe = 0x067E, // Persian Letters;
		PersianChe = 0x0686,
		PersianZe = 0x0698,
		PersianGaf = 0x06AF,
		PersianGaf2 = 0x06A9,
		PersianYeh = 0x06CC,
	
	}
	
	/// <summary>
	/// Arabic Contextual forms General - Unicode
	/// </summary>
	public enum IsolatedArabicLetters
	{
		Hamza = 0xFE80,
		Alef = 0xFE8D,
		AlefHamza = 0xFE83,
		WawHamza = 0xFE85,
		AlefMaksoor = 0xFE87,
		AlefMaksora = 0xFBFC,
		HamzaNabera = 0xFE89,
		Ba = 0xFE8F,
		Ta = 0xFE95,
		Tha2 = 0xFE99,
		Jeem = 0xFE9D,
		H7aa = 0xFEA1,
		Khaa2 = 0xFEA5,
		Dal = 0xFEA9,
		Thal = 0xFEAB,
		Ra2 = 0xFEAD,
		Zeen = 0xFEAF,
		Seen = 0xFEB1,
		Sheen = 0xFEB5,
		S9a = 0xFEB9,
		Dha = 0xFEBD,
		T6a = 0xFEC1,
		T6ha = 0xFEC5,
		Ain = 0xFEC9,
		Gain = 0xFECD,
		Fa = 0xFED1,
		Gaf = 0xFED5,
		Kaf = 0xFED9,
		Lam = 0xFEDD,
		Meem = 0xFEE1,
		Noon = 0xFEE5,
		Ha = 0xFEE9,
		Waw = 0xFEED,
		Ya = 0xFEF1,
		AlefMad = 0xFE81,
		TaMarboota = 0xFE93,
		PersianPe = 0xFB56, // Persian Letters;
		PersianChe = 0xFB7A,
		PersianZe = 0xFB8A,
		PersianGaf = 0xFB92,
		PersianGaf2 = 0xFB8E,
		PersianYeh = 0xFBFC,
	}

	public enum LamCombinations
	{
		LamAlefFatha = 0xFEF7,
		LamAlefKasra = 0xFEF9,
		LamAlefMad = 0xFEF5,
		LamAlef = 0xFEFB,
	}
	
	public enum Tashkeels
	{
		Shadda = 0x0651,
		Sukun = 0x0652,
		Maddah = 0x0653,
		Fatha = 0x064E,
		Damma = 0x064F,
		Kasra = 0x0650,
		ShaddaFatha = 0xFC60,
		ShaddaDamma = 0xFC61,
		ShaddaKasra = 0xFC62,
		TanweenFatha = 0x064B,
		TanweenDamma = 0x064C,
		TanweenKasra = 0x064D,
	}

	public enum HinduNumerals
	{
		Zero = 1632,
		One = 1633,
		Two = 1634,
		Three = 1635,
		Four = 1636,
		Five = 1637,
		Six = 1638,
		Seven = 1639,
		Eight = 1640,
		Nine = 1641,
	}
}
