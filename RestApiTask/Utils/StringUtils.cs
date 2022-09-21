namespace RestApiTask.Utils
{
    public class StringUtils
    {
        private static Logger Log = Logger.Instance;

        public static string StringGenerator(int lettersCount)
        {
            char[] letters = "ABCD EFGHI_JKLMN-OPQRS!TUVWXYZabc,defghigk lmnopqr stuvwxyz".ToCharArray();

            Random rand = new Random();

            string word = "";

            for (int j = 1; j <= lettersCount; j++)
            {
                int letter = rand.Next(0, letters.Length - 1);

                word += letters[letter];
            }
            Log.Info(string.Format("generated random text = {0}", word));
            return word;
        }
    }
}
