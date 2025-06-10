using System;
using System.IO;

namespace VAME
{
    public static class O2Parser
    {
        public static void ParseO2File(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("O2 file not found.");
                return;
            }

            try
            {
                using var reader = new BinaryReader(File.OpenRead(path));
                // Bu alan ileride .o2 içeriği detaylı çözümlenince genişletilecek
                byte[] header = reader.ReadBytes(64); // örnek header okuma
                Console.WriteLine($"O2 file loaded: {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"O2 parsing error: {ex.Message}");
            }
        }
    }
}
