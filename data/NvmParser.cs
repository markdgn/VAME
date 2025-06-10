using System.Collections.Generic;
using System.IO;
using System;

namespace VAME
{
    public static class NvmParser
    {
        public static List<float[]> LoadNavMesh(string path)
        {
            List<float[]> triangles = new();

            if (!File.Exists(path))
                return triangles;

            try
            {
                using var reader = new BinaryReader(File.OpenRead(path));
                reader.BaseStream.Seek(128, SeekOrigin.Begin); // header atla

                while (reader.BaseStream.Position + 36 <= reader.BaseStream.Length)
                {
                    float[] triangle = new float[9];
                    for (int i = 0; i < 9; i++)
                        triangle[i] = reader.ReadSingle();

                    triangles.Add(triangle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NVM parsing error: {ex.Message}");
            }

            return triangles;
        }
    }
}
