using MediatRGen.Core.Base;

namespace MediatRGen.Core.Services
{
    public class ArgsService
    {
        public static ServiceResult<string[]> SplitArgs(string command)
        {
            List<string> parsedValue = new List<string>();
            int index = 0;
            string _temp = string.Empty;
            bool isquetobegin = false;

            char queto = (char)34;
            char space = (char)32;

            while (true)
            {

                if (command[index] != space && command[index] != queto)
                    _temp += command[index];

                if (isquetobegin == false && command[index] == space)
                {
                    parsedValue.Add(_temp);
                    _temp = string.Empty;
                }

                if (command[index] == queto)
                    isquetobegin = true;

                if (isquetobegin == true && command[index] == space)
                    _temp += command[index];

                if (command[index] == queto && isquetobegin == true)
                {
                    parsedValue.Add(_temp);
                    _temp = string.Empty;
                }

                index++;

                if (index == command.Length)
                {
                    parsedValue.Add(_temp);

                    break;
                }
            }


            var result = parsedValue.Where(x => x != "").ToList().ToArray();

            return new ServiceResult<string[]>(result, true, "");

        }
    }
}
