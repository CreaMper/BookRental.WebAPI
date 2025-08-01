namespace BookRental.WebAPI.Utils.Validators
{
    public static class ISBNValidator 
    {
        public static bool IsValid(string isbn)
        {
            switch (isbn.Length)
            {
                case 10:
                    return Validate10Numbers(isbn);
                case 13:
                    return Validate13Numbers(isbn);
                default:
                    return false;
            }
        }

        private static bool Validate10Numbers(string isbn)
        {
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                char c = isbn[i];
                int value;

                if (i == 9 && c == 'X')
                    value = 10;
                else if (!char.IsDigit(c))
                    return false;
                else
                    value = c - '0';

                sum += value * (10 - i);
            }
            return sum % 11 == 0;
        }

        private static bool Validate13Numbers(string isbn)
        {
            int sum = 0;
            for (int i = 0; i < 13; i++)
            {
                if (!char.IsDigit(isbn[i]))
                    return false;

                int value = isbn[i] - '0';
                int weight = (i % 2 == 0) ? 1 : 3;
                sum += value * weight;
            }
            return sum % 10 == 0;
        }
    }
}
