using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Xml;

namespace LineJockey
{
    class Program
    {
        static string datapath;
        static void Main(string[] args) {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            datapath = Path.Combine(appdata,@"JacobStuff\LineJockey\Data\");
            if (!Directory.Exists(datapath)) {
                Directory.CreateDirectory(datapath);
            }
            if (!File.Exists(datapath + "Paths.json")) {
                File.Copy(@"Data/Paths.json", datapath + "Paths.json", false);
            }

            Console.WriteLine("LineJockey at your service.");
            GoFunction();
        }

        public static void GoFunction() {
            
            //var i = Console.ReadKey(); // Good for choice input...
            var i = Console.ReadLine();
            Console.Clear();
            string[] args = i.Split();
            var paths = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(datapath+"Paths.json"));
            switch (args[0]) {
                case "goto":
                    if (args.Length < 2) break;
                    var p = args[1];
                    if (paths.ContainsKey(p)) {
                        string path = paths[p];
                        Uri uriResult;
                        bool result = Uri.TryCreate(path, UriKind.Absolute, out uriResult)
                            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                        if (result) { // Is URL...
                            Process.Start(new ProcessStartInfo {
                                FileName = path,
                                UseShellExecute = true
                            });
                            Console.WriteLine("Opening webpage...");
                        } else {
                            Console.WriteLine(Things.OpenFolder(path));
                        }
                        
                    } else {
                        Console.WriteLine("Item \"" + args[1] + "\" doesn't exist.");
                    }
                    break;
                case "save":
                    if (args.Length < 3) break;
                    if (args[1] == "") break;
                    var s = i.Substring(5+args[1].Length+1);
                    paths.Add(args[1], @s.Trim('"'));
                    var saved = JsonSerializer.Serialize(paths);
                    File.WriteAllText(datapath+"Paths.json",saved);
                    Console.WriteLine("Saved path \"" + args[1] + "\".");

                    break;
                case "remove":
                    if (args.Length < 2) break;
                    if (paths.ContainsKey(args[1])) {
                        paths.Remove(args[1]);
                        File.WriteAllText(datapath + "Paths.json", JsonSerializer.Serialize(paths));
                        Console.WriteLine("Removed \"" + args[1] + "\".");
                    } else {
                        Console.WriteLine("Could not find \"" + args[1] + "\".");
                    }
                        break;
                case "show":
                    if (args.Length < 2) break;
                    if (paths.ContainsKey(args[1])) {
                        Console.WriteLine("Name: " + args[1] + "\nPath: " + paths[args[1]]);
                    } else {
                        Console.WriteLine("Could not find \"" + args[1] + "\".");
                    }
                    break;
                case "showall":
                    foreach (KeyValuePair<string,string> str in paths) {
                        Console.WriteLine(str);
                    }
                    break;
                case "help":
                    Console.Write(Things.helptext);
                    break;
                default:
                    Console.WriteLine(Things.RandomText());
                    break;
            }
            
            //Console.WriteLine(i);
            GoFunction();

            
        }

    }
}
