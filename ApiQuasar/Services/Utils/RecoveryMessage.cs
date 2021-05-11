using ApiQuasar.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Services.Utils
{
    public static class RecoveryMessage
    {
        public static string Recovery(List<string[]> listTransmissionMessage)
        {
            var zipMessage = listTransmissionMessage[0].Zip3(listTransmissionMessage[1], listTransmissionMessage[2], Tuple.Create);

            List<string[]> listMessage = tupleToList(zipMessage);
            List<string> listMessageNoDuplicates = listNoDuplicates(listMessage);
            string message = String.Join(" ", listMessageNoDuplicates.ToArray());

            return message;
        }


        private static List<string[]> tupleToList(IEnumerable<Tuple<string, string, string>> tuple)
        {

            List<string[]> listMessage = tuple.Select(t => string.Format("{0},{1},{2}", t.Item1, t.Item2, t.Item3)
                                           .Split(",", StringSplitOptions.RemoveEmptyEntries))
                                            .ToList();

            if (recoveryForColumn(listMessage))
            {
                return listMessage;
            }
            else
            {
                throw new HttpException("No se puede recuperar el mensaje. El mensaje está incompleto", HttpStatusCode.NotFound);
            }
        }


        private static bool recoveryForColumn(List<string[]> list)
        {
            if (list.Any(x => x.Length.Equals(0)))
            {
                return false;
            }
            return true;
        }


        private static List<string> listNoDuplicates(List<string[]> listMessage)
        {
            List<string> noRepeats = new List<string>();
            listMessage.ForEach(x =>
            {
                noRepeats.Add(removeDuplicates(x)[0]);
            });

            return noRepeats;
        }

        private static string[] removeDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }
    }
}
