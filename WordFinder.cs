namespace DeveloperChallengeApp
{
    public class WordFinder
    {
        /// <summary>
        /// The number of words to return when given a list of words to find for the matrix
        /// </summary>
        private const int LISTSIZE = 10;

        /// <summary>
        /// Represents the matrix of letters to find words within.
        /// </summary>
        private List<string> Matrix;
        public WordFinder(IEnumerable<string> matrix)
        {
            Matrix = matrix.ToList().ConvertAll(d => d.ToLower());
        }

        /// <summary>
        /// Finds the top ten words within the matrix of the list of words passed. 
        /// Time Complexity : Time complexity: (RowHeight * ColumnHeight * Number of Strings * 2 * Length of Strings)
        /// All the cells will be visited and traversed in two directions, the height and width of the matrix so time complexity is O(R * C) for each string.
        /// </summary>
        /// <param name="wordstream">The list of words to find within the matrix</param>
        /// <returns></returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            IEnumerable<string> distinctWords = wordstream.ToList().ConvertAll(d => d.ToLower()).Distinct();
            List<KeyValuePair<string, int>> foundWords = new List<KeyValuePair<string, int>>();
            foreach (var word in distinctWords)
            {
                int count = FindCount(word);
                if (count == 0)
                    continue;
                if (foundWords.Count < LISTSIZE)
                {
                    foundWords.Add(new KeyValuePair<string, int>(word, count));
                }
                else
                {
                    foundWords = foundWords.OrderByDescending(kvp => kvp.Value).ToList();
                    if(foundWords.Last().Value < count)
                    {
                        foundWords.Remove(foundWords.Last());
                        foundWords.Add(new KeyValuePair<string, int>(word, count));
                    }
                }
            }
            return from kvp in foundWords select kvp.Key;
        }

        /// <summary>
        /// Counts how many times the given word is found within the matrix
        /// </summary>
        /// <param name="word">The word to search for</param>
        /// <returns>The number of times that the word was found within the matrix</returns>
        private int FindCount(string word)
        {
            int count = 0;
            for (int rowIndex = 0; rowIndex < Matrix.Count(); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Matrix[0].Length; columnIndex++)
                {
                    if (Matrix[rowIndex].ElementAt(columnIndex).Equals(word[0]))
                    {
                        if (FindDown(word, columnIndex, rowIndex))
                            count++;
                        if (FindRight(word, columnIndex, rowIndex))
                            count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Checks if the given word can be found by searching right from a specific point in the matrix given by the column and row index.
        /// </summary>
        /// <param name="word"></param> The word to search for
        /// <param name="columnIndex"></param> The column index to begin searching from.
        /// <param name="rowIndex"></param> The row index to begin searching from.
        /// <returns> a boolean representing whether the word was successfully found or not.</returns>
        private bool FindRight(string word, int columnIndex, int rowIndex)
        {
            if (word.Length + columnIndex > Matrix[0].Length || Matrix[rowIndex].Substring(columnIndex, word.Length) != word)
                return false;
            return true;
        }

        /// <summary>
        /// Checks if the given word can be found by searching down from a specific point in the matrix given by the column and row index.
        /// </summary>
        /// <param name="word"></param> The word to search for
        /// <param name="columnIndex"></param> The column index to begin searching from.
        /// <param name="rowIndex"></param> The row index to begin searching from.
        /// <returns> a boolean representing whether the word was successfully found or not.</returns>
        private bool FindDown(string word, int columnIndex, int rowIndex)
        {
            if(rowIndex + word.Length > Matrix.Count) 
                return false;
            for (int charIndex = 0; charIndex < word.Length; charIndex++)
            {
                if (word[charIndex].Equals(Matrix[rowIndex + charIndex].ElementAt(columnIndex)))
                    continue;
                else return false;
            }
            return true;
        }
    }
}
