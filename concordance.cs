using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main() 
    {
        Console.WriteLine("Enter text for Concordance:"); //Asks for input
        string concord = Console.ReadLine(); //Reads user input
        if (concord != null) //tests if input
        {
            Console.WriteLine(Program.returnConcordance(concord)); //calls returnConcordance method and passes user input
        }
    }

    public static string returnConcordance(string text)
    {
        int index = 0; //index of each sentence
        SortedDictionary<string, List<int>> _dictionary = new SortedDictionary<string, List<int>>(); //class that automatically sorts Keys
        /* 
         * This is a Dictionary of both strings and a list of the integers that we need for the count of each word, 
         * and the index where the word was in each respective sentence
         */
        string noBreaks = Regex.Replace(text, @"\r\n?|\n", "");
        string[] sentences = Regex.Split(noBreaks, @"(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?)\s"); //Regex pattern for splitting each sentence into an array
        Regex rgx = new Regex("[,:?!;()]");   //trim pattern for each word without '.' due to abbreviations.
        foreach (string sentence in sentences) //loops over each sentence in the sentence array
        {
            index += 1; //adds 1 after each loop
            var trimSentence = sentence.TrimEnd(new Char[] { '!', '?', '.' }).ToLower(); //trims the end of the sentence.
            foreach (string word in trimSentence.Split(' ')) //splits into words
            {
                string newWord;
                newWord = rgx.Replace(word, ""); //uses regex patter above to replace characters which aren't needed.
                List<int> intList = new List<int>(); //instantiate a new list of integers
                if (_dictionary.TryGetValue(newWord, out intList)) //testing if the value already exists in the Dictionary.
                {
                    int oldVal = intList.First(); //set the old value
                    int newVal = oldVal + 1; //increment it
                    intList.RemoveAt(0); //remove old value
                    intList.Insert(0, newVal); //insert at index 0
                    intList.Add(index); //add index to list
                    _dictionary[newWord] = intList; //replace
                }
                else //if it is a new word in the Dictionary
                {
                    List<int> intList2 = new List<int>(); //instantiate new list of integers. (If lists are empty when they are called in a loop, you get a null reference error.)
                    intList2.Add(1); //add item
                    intList2.Add(index); //add index to list
                    _dictionary.Add(newWord, intList2); //add to dictionary
                }
            }
        }
        string returnVal = ""; int wordCount = 0; StringBuilder builder = new StringBuilder(); //return value and count to increment 
        foreach (KeyValuePair<string, List<int>> pairs in _dictionary) //for each pair in the Dictionary
        {
            var valString = ""; 
            for (int i = 0; i < pairs.Value.Count; i++) //incrementing over the count of pairs
            {
                if (i == 0) //if it is zero, we know it is the first value
                {
                    valString += pairs.Value[i] + ":";
                }
                else if (i != pairs.Value.Count - 1) //if it is not the first or last value, add a comma
                {
                    valString += pairs.Value[i] + ",";
                }
                if (i == pairs.Value.Count - 1) //finds the last value
                {
                    valString += pairs.Value[i];
                }
            }
            builder.Append(Program.alphaConversion(wordCount++) + ". " + pairs.Key + "  {" + valString + "}" + Environment.NewLine); //increment alpha and format the text for output
        }
        returnVal = builder.ToString();
        return returnVal;
    }
    private static string alphaConversion(int wordCount)
    {
        string alpha = "abcdefghijklmnopqrstuvwxyz"; //alphabet string;
        string _value = "";
        int modulo = 0;
        do
        {
            modulo = wordCount % 26;   //get remainder
            _value = alpha.Substring(modulo, 1) + _value + _value; //gets the substring of alpha based on the modulo plus itself
            wordCount = (wordCount / 26) - 1;
        } while ((wordCount + 1) > 0); //while there are integers
        if (_value.Length >= 3)
        {
            return _value.Substring(1);
        }
        return _value; //returns formatted alpha count
    }
}
