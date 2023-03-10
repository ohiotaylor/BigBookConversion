////////////////// Example Code needs these using statements

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

using System.Diagnostics;

////////////////// End of using statements for example code 


public class BookContentWithExamples : MonoBehaviour
{
	private Dictionary<string, string> currentWordMapping;
	[SerializeField] TextAsset Alcohol;
	[SerializeField] TextAsset SAA; 
	[SerializeField] TextAsset Gambling;
	[SerializeField] TextAsset God;
	[SerializeField] TextAsset Jesus;
	[SerializeField] TextAsset Krishna;
	[SerializeField] TextAsset SpiritOfTheUniverse;
	[SerializeField] TextAsset Earth;
	[SerializeField] TextAsset Allah;
	[SerializeField] TextAsset Buddha;
	[SerializeField] TextAsset Male;
	[SerializeField] TextAsset Female;
	[SerializeField] TextAsset NonBionary;


	public enum EWordMapping
	{
		Alcohol,
		SAA,
		Gambling,
		God,
		Jesus,
		Krishna,
		SpiritOfTheUniverse,
		Earth,
		Allah,
		Buddha,
		Male,
		Female,
		NonBionary

	}

	/// <summary>
	/// This function will convert all replaceable words with the appropriate word provided by currentWordMapping.
	/// </summary>
	/// <param name="input">This is the string you want to convert.</param>
	/// <returns>The converted string.</returns>
	public string ConvertAllReplaceableWords(string input)
	{
		/// We're using regular expressions to find all words that start with [ and end with ] because these are the replaceable words.
		/// The @" and closing " means C# interprets the entire thing as a literal string without worrying about escape characters which is helpful when building regular expressions.
		/// The \b means a word boundary.
		/// We have to escape the [ with a \ so that the regular expression doesn't interpret it as a grouping metacharacter, only as a literal bracket.
		/// The \w means a word character, with the following + meaning one or more of the word characters.
		/// We have to escape the ] with a \ so that the regular expression doesn't interpret it as a grouping metacharacter, only as a literal bracket.
		/// /// The final \b also means a word boundary.
		string replaceableWordPattern = @"\$\$.*?\$\$";

		/// This creates a delegate that we will use to dynamically replace all the replaceable words
		MatchEvaluator ReplaceWordEvaluator = new MatchEvaluator(ReplaceWord);
		
		/// Finally, replace all the replaceable words
		return Regex.Replace(input, replaceableWordPattern, ReplaceWordEvaluator, RegexOptions.Compiled);
	}

	/// <summary>
	/// This function will dynamically evaluate each match and replace the matched word with the appropriate one.
	/// </summary>
	/// <param name="match">This is the matched replaceable word.</param>
	/// <returns>The new string with the replaced word.</returns>
	private string ReplaceWord(Match match)
	{
		try
		{
			/// This will look for the corresponding key in the dictionary for the replaceable word and return the new word (ie. it will look for "[Alcoholic]" and replace it with "Video Game Addict")
			return currentWordMapping[match.Value];
		}
		catch (KeyNotFoundException e) 
		{
			/// This should never happen during runtime. This is to catch any missing entries you may have forgotten during development.
			UnityEngine.Debug.LogError($"There is a missing entry for the word mapping: {match.Value}\nEnsure all replaceable words has a matching entry!");
            UnityEngine.Debug.LogException(e, this);
			throw;
		}
	}

	/// <summary>
	/// You will want to call this if you need to reset the word mapping completely. For example, you are about to switch from alcoholics to gambling addicts and you want to ensure there isn't leftover mapping from the alcoholics.
	/// </summary>
	public void ClearWordMapping()
	{
		currentWordMapping.Clear();
	}

	/// <summary>
	/// This will add mapping entries into the mapping context. It is cumulative. For example, if you add in a mapping context from alcoholics, it will not necessarily change any religious references. If you change the religious reference, it will add those changes on top of the alcoholics references.
	/// </summary>
	/// <param name="mappingContext">The mapping context you want to add for the replaceable words.</param>
	public void AddWordMapping(EWordMapping mappingContext)
	{
		switch (mappingContext)
		{

			case EWordMapping.Alcohol:
				ExtractWordMapping(Alcohol);
				break;
			case EWordMapping.SAA:
				ExtractWordMapping(SAA);
				break;
			case EWordMapping.Gambling:
				ExtractWordMapping(Gambling);
				break;
			case EWordMapping.God:
				ExtractWordMapping(God);
				break;
			case EWordMapping.Jesus:
				ExtractWordMapping(Jesus);
				break;
			case EWordMapping.Krishna:
				ExtractWordMapping(Krishna);
				break;
			case EWordMapping.SpiritOfTheUniverse:
				ExtractWordMapping(SpiritOfTheUniverse);
				break;
			case EWordMapping.Earth:
				ExtractWordMapping(Earth);
				break;
			case EWordMapping.Allah:
				ExtractWordMapping(Allah);
				break;
			case EWordMapping.Buddha:
				ExtractWordMapping(Buddha);
				break;
			case EWordMapping.Male:
				ExtractWordMapping(Male);
				break;
			case EWordMapping.Female:
				ExtractWordMapping(Female);
				break;
			case EWordMapping.NonBionary:
				ExtractWordMapping(NonBionary);
				break;
			default:
				/// This should never happen during runtime. This is to catch any missing mapping contexts you may have forgotten to add to the switch statement during development.
				UnityEngine.Debug.LogError($"There is a missing entry for the map context: {mappingContext}\nEnsure there is a mapping available for this context.");
				break;
		}
	}

	/// <summary>
	/// This will extract the word mapping from the text file given at the path and add it to the current word mapping. The expected format (without the quotations) is: "[String To Be Replaced] : New String".
	/// </summary>
	/// <param name="path">The path to the text file containing the word mapping.</param>
	private void ExtractWordMapping(TextAsset textAsset)
	{

		//TextAsset wordMapping = Resources.Load<TextAsset>(te);
		/// Need a stream reader to slurp in the text file.
		//StreamReader reader = new StreamReader(path);
		StreamReader reader = new StreamReader(new MemoryStream(textAsset.bytes));
		/// Read the entire text file at once.
		string mappingEntriesRaw = reader.ReadToEnd();
		/// Close the reader because we do not need it anymore.
		reader.Close();
		if (currentWordMapping == null)
			currentWordMapping = new Dictionary<string, string>();

		
		

		/// Create a regular expression to match each entry in the text file.
		/// The @" tells C# to treat the entire thing as a literal string so it does not interpret escape characters such as \ (since regular expressions need to use \ itself).
		/// The \b is a word boundary.
		/// The ( with a matching ) makes it a capture group. The regular expression will remember the contents of the capture group. Each capture group is numbered by appearance. So this one is capture group 0.
		/// The \ escapes the [ so that the regular expression will interpret it as a literal bracket instead of a grouping metacharacter.
		/// the \w means a word character. The + following it means one or more.
		/// The \ escapes the ] so that the regular expression will interpret it as a literal bracket instead of a grouping metacharacter.
		/// The final (\w+) is the second capture group (so index 1) that will match one or more word characters.
		Regex mappingEntryRegex = new Regex(@"(\$\$.*?\$\$) : (.*?)\r*$", RegexOptions.Compiled | RegexOptions.Multiline);
		/// Find all the matches in the string.
		MatchCollection mappingEntries = mappingEntryRegex.Matches(mappingEntriesRaw);
		/// Iterate through all the matches in the string.
		foreach (Match mappingEntry in mappingEntries)
		{
			/// Find all the capture groups in each match. We had 2 in the regular expression.
			GroupCollection groups = mappingEntry.Groups;
			/// Add the first capture group which contains [ReplaceableWord] as a key, and the second capture group which contains the new word as the value.
			
			if(!currentWordMapping.ContainsKey(groups[1].Value))
				currentWordMapping.Add(groups[1].Value, groups[2].Value);
            else
                currentWordMapping[groups[1].Value] = groups[2].Value;
		}
		
	}

    private void UpdateConversion()
    {
        
    }

    /////////////////////// End of Example Code ///////////////////////////////////
}
