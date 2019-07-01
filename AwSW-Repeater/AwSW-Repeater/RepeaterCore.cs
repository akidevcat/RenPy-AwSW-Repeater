using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AwSW_Repeater
{
    public class RPYFile
    {
        public readonly string name;
        public readonly string path;
        public readonly string[] text;

        public RPYFile(string path)
        {
            if (!File.Exists(path))
                throw new Exception($"RPYFile doesn't exist! Path: {path}");

            if (Path.GetExtension(path).ToLower() != ".rpy")
                throw new Exception($"Wrong File Format! Path: {path}");

            this.name = Path.GetFileName(path);
            this.path = path;
            text = File.ReadAllLines(path);
        }
    }

    /// <summary>
    /// Some basic IO RPY functions
    /// </summary>
    public static class RPYHelper
    {
        /// <summary>
        /// Removes .rpy duplicates from the list
        /// </summary>
        /// <param name="list">Given .rpy list</param>
        public static void RemoveDuplicates(this List<RPYFile> list)
        {
            var paths = new HashSet<string>();
            for (int i = 0; i < list.Count; i++)
            {
                var rpy = list[i];
                if (paths.Contains(rpy.path))
                {
                    list.Remove(rpy);
                    i--;
                }
                else
                    paths.Add(rpy.path);
            }
        }

        /// <summary>
        /// Tryes to read the file with given path checking for extension
        /// </summary>
        /// <param name="path">possible .rpy file path</param>
        /// <returns>.rpy file if exists, otherwise null</returns>
        public static RPYFile ReadRPY(string path)
        {
            if (!File.Exists(path))
                return null;
            if (Path.GetExtension(path).ToLower() != ".rpy")
                return null;

            return new RPYFile(path);
        }

        /// <summary>
        /// Reads all .rpy files inside the path folder
        /// </summary>
        /// <param name="path">Folder to read</param>
        /// <returns>List of .rpy files read</returns>
        public static List<RPYFile> ReadAllFilesRecursively(string path)
        {
            if (!Directory.Exists(path))
                throw new Exception($"Directory doesn't exist! Path: {path}");

            var list = new List<RPYFile>();
            var paths = Directory.GetFiles(path);

            foreach (var p in paths)
            {
                if (Directory.Exists(p))
                {
                    //That's a directory
                    list.AddRange(ReadAllFilesRecursively(p));
                }
                else
                {
                    //That's a file
                    var rpy = ReadRPY(p);
                    if (rpy != null)
                        list.Add(rpy);
                }
            }

            return list;
        }

        public static string ParseRpyLine(string line)
        {
            line = line.Trim();
            if (line.Length <= 6)
                return string.Empty;

            if (line[0] != '#' &&
                line.Substring(0, 3) != "old")
                return string.Empty;

            if (line[0] == '#' && Settings.Filters_OldNewOnly)
                return string.Empty;

            var extraIdentifier = string.Empty;
            if (Settings.Filters_CheckCharacter && line[0] == '#')
                extraIdentifier += "Character=" + line.Split(' ')[1] + ";";

            bool fQuotesFound = false;
            int left = 0;
            int right = line.Length;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                {
                    fQuotesFound = true;
                    if (left == 0)
                    {
                        left = i + 1;
                    }
                    else
                    {
                        if (i == 0 || line[i - 1] != '\\')
                        {
                            right = i;
                            break;
                        }
                    }
                }
            }

            if (!fQuotesFound)
                return string.Empty;

            line = line.Substring(left, right - left);

            return extraIdentifier + line;
        }
    }

    public class RepeatingLine
    {
        public int line;
        public RPYFile file;

        public RepeatingLine(int line, RPYFile file)
        {
            this.line = line;
            this.file = file;
        }
    }

    public class RepeaterCore
    {
        public int filesCount
        {
            get => readFiles.Count;
        }
        private readonly List<RPYFile> readFiles;
        private readonly Dictionary<string, RepeatingLine> reviewedLines;
        private readonly Dictionary<string, List<RepeatingLine>> repeatingLines;


        public RepeaterCore()
        {
            readFiles = new List<RPYFile>();
            reviewedLines = new Dictionary<string, RepeatingLine>();
            repeatingLines = new Dictionary<string, List<RepeatingLine>>();
        }

        /// <summary>
        /// Clears the memory
        /// </summary>
        public void Reset()
        {
            readFiles.Clear();
            reviewedLines.Clear();
            repeatingLines.Clear();
        }

        /// <summary>
        /// Uploads rpy files
        /// </summary>
        /// <param name="paths">Files and Directories</param>
        /// <returns>Number of files read</returns>
        public int UploadFiles(string[] paths)
        {
            int initialCount = readFiles.Count;

            foreach (var p in paths) {
                if (Directory.Exists(p))
                {
                    //Current path is a folder, so we're reading all the files here recursively
                    readFiles.AddRange(RPYHelper.ReadAllFilesRecursively(p));
                } else
                {
                    //Current path is a file
                    var rpy = RPYHelper.ReadRPY(p);
                    if (rpy != null)
                        readFiles.Add(rpy);
                }
            }

            //Don't forget to remove duplicates
            readFiles.RemoveDuplicates();

            return (readFiles.Count - initialCount);
        }

        private string ProcessStringForCheck(string s)
        {
            var splitted = s.Split(' ');

            if (Settings.Filters_MinWordCount > 0)
            {
                if (splitted.Length < Settings.Filters_MinWordCount)
                {
                    return string.Empty;
                }
            }

            if (Settings.Filters_CheckFirstWords > 0)
            {
                splitted = splitted.Take(Settings.Filters_CheckFirstWords).ToArray();
            }

            return string.Join("", splitted);
        }

        private void AddRepeatingLine(string checkString, RepeatingLine initialLine, RepeatingLine rLine)
        {
            if (repeatingLines.ContainsKey(checkString))
            {
                repeatingLines[checkString].Add(rLine);
            } else
            {
                var list = new List<RepeatingLine>();
                list.Add(initialLine);
                list.Add(rLine);
                repeatingLines.Add(checkString, list);
            }
        }

        public void ProcessFiles()
        {
            reviewedLines.Clear();
            repeatingLines.Clear();

            foreach (var rpy in readFiles)
            {
                for (int i = 0; i < rpy.text.Length; i++)
                {
                    var line = rpy.text[i];
                    var linePhrase = RPYHelper.ParseRpyLine(line);
                    if (linePhrase == string.Empty)
                        continue;

                    var checkString = ProcessStringForCheck(linePhrase);
                    if (checkString == string.Empty)
                        continue;

                    var rLine = new RepeatingLine(i, rpy);

                    if (reviewedLines.ContainsKey(checkString))
                    {
                        AddRepeatingLine(checkString, reviewedLines[checkString], rLine);
                    } else
                    {
                        reviewedLines.Add(checkString, rLine);
                    }
                }
            }
        }

        private string EscapeString(string s)
        {
            s = s.Replace("\"", "\"\"");
            s = s.Replace("\\", "\\\\");
            return s;
        }

        private string[] GetCsvFile()
        {
            var csv = new List<string>
            {
                "Phrase,File,Line"
            };

            foreach (var rLine in repeatingLines)
            {
                foreach (var i in rLine.Value)
                {
                    var phrase = $"\"{EscapeString(i.file.text[i.line].Trim())}\"";
                    var file = $"\"{EscapeString(i.file.name)}\"";
                    var line = $"\"{i.line}\"";
                    csv.Add($"{phrase},{file},{line}");

                    if (Settings.Output_OnlyOnce)
                        break;
                }
            }

            return csv.ToArray();
        }

        public void SaveToCsvFile(string path)
        {
            var file = GetCsvFile();
            int counter = 0;
            string newPath = path;
            while (File.Exists(newPath))
                newPath = path + (counter++); //I was too lazy to make it without changing extension, sum'masen
            File.WriteAllLines(newPath, file);
        }
    }
}
