using ApiQuasar.Exceptions;
using ApiQuasar.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ApiQuasar.Adapters
{
    public class AdapterRecoveryMessage : IAdapterRecoveryMessage
    {
        public string Recovery(List<string[]> listSatellitesMessage)
        {
            var zipMessage = listSatellitesMessage[0].Zip3(listSatellitesMessage[1], listSatellitesMessage[2], Tuple.Create);

            List<string[]> listMessage = TupleToList(zipMessage);
            List<string> listMessageNoDuplicates = ListNoDuplicates(listMessage);
            string message = String.Join(" ", listMessageNoDuplicates.ToArray());

            return message;
        }



        public static List<string[]> TupleToList(IEnumerable<Tuple<string, string, string>> tuple)
        {

            List<string[]> listMessage = tuple.Select(t => string.Format("{0},{1},{2}", t.Item1, t.Item2, t.Item3)
                                           .Split(",", StringSplitOptions.RemoveEmptyEntries))
                                            .ToList();

            if (RecoveryForColumn(listMessage))
            {
                return listMessage;
            }
            else
            {
                throw new HttpException("No se puede recuperar el mensaje. El mensaje está incompleto", HttpStatusCode.NotFound);
            }
        }


        public static bool RecoveryForColumn(List<string[]> list)
        {
            if (list.Any(x => x.Length.Equals(0)))
            {
                return false;
            }
            return true;
        }


        public static List<string> ListNoDuplicates(List<string[]> listMessage)
        {
            List<string> noRepeats = new List<string>();
            listMessage.ForEach(x =>
            {
                noRepeats.Add(RemoveDuplicates(x)[0]);
            });

            return noRepeats;
        }

        public static string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

    }
}
