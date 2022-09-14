using Labo.BLL.Interfaces;
using System.Text;

namespace Labo.IL.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string Random(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
