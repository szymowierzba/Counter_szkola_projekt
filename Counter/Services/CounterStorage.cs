
using System.Collections.Generic;
using System.IO;
using Counter.Models;

namespace Counter.Services
{
    public class CounterStorage
    {
        string filePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "counters.txt"
        );

        public void SaveCounters(List<CounterData> counters)
        {
            List<string> lines = new List<string>();

            foreach (CounterData counter in counters)
            {
                lines.Add(counter.Name + "|" + counter.Value);
            }

            File.WriteAllLines(filePath, lines);
        }

        public List<CounterData> LoadCounters()
        {
            List<CounterData> counters = new List<CounterData>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    if (parts.Length == 2 &&
                        int.TryParse(parts[1], out int value))
                    {
                        CounterData counter = new CounterData();

                        counter.Name = parts[0];
                        counter.Value = value;

                        counters.Add(counter);
                    }
                }
            }

            return counters;
        }
    }
}
