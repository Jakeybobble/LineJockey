using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineJockey
{
    class Things
    {
        public static string OpenFolder(string folderPath) {
            if (Directory.Exists(folderPath)) {
                ProcessStartInfo startInfo = new ProcessStartInfo {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
                return "LineJockey: Opening folder...";
            } else {
                //MessageBox.Show(string.Format("{0} Directory does not exist!", folderPath));
                Console.WriteLine(folderPath);
                return "Folder" +folderPath+ " does not exist.";
            }
        }

        public static string RandomText() {
            string[] texts = {
                "Weather's nice, isn't it? It always is.",
                "Here, let me grab you a drink.",
                "I hope I'm not in the way.",
                "At your service.",
                "Use the help command for guidance."
            };
            int r = new Random().Next(0,texts.Length);
            return texts[r];
        }

        public static string AddQuotesIfRequired(string path) {
            return !string.IsNullOrWhiteSpace(path) ?
                path.Contains(" ") && (!path.StartsWith("\"") && !path.EndsWith("\"")) ?
                    "\"" + path + "\"" : path :
                    string.Empty;
        }

        public static string helptext =
            "goto [id] - Open folder by id.\n" +
            "save [id] [path] - Add new id by path.\n" +
            "remove [id] - Remove an id.\n" +
            "show [id] - Shows path by its id.\n" +
            "showall - Shows all id's and paths.\n" +
            "help - Shows this text.\n";
    }
}
