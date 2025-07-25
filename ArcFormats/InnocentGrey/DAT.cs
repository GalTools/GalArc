using GalArc.Logs;
using GalArc.Strings;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utility;
using Utility.Exceptions;

namespace ArcFormats.InnocentGrey
{
    public class DAT : IGA
    {
        private const string Magic = "PACKDAT.";

        private class DatEntry : PackedEntry
        {
            public uint FileType { get; set; }
        }

        public override void Unpack(string filePath, string folderPath)
        {
            FileStream fs = File.OpenRead(filePath);
            BinaryReader br = new BinaryReader(fs);
            if (Encoding.ASCII.GetString(br.ReadBytes(8)) != Magic)
            {
                throw new InvalidArchiveException();
            }
            int fileCount = br.ReadInt32();
            br.BaseStream.Position += 4;

            Logger.InitBar(fileCount);
            List<DatEntry> entries = new List<DatEntry>();
            Directory.CreateDirectory(folderPath);

            for (int i = 0; i < fileCount; i++)
            {
                DatEntry entry = new DatEntry();
                entry.Name = ArcEncoding.Shift_JIS.GetString(br.ReadBytes(32)).TrimEnd('\0');
                entry.Offset = br.ReadUInt32();
                entry.FileType = br.ReadUInt32();
                entry.UnpackedSize = br.ReadUInt32();
                entry.Size = br.ReadUInt32();
                entry.IsPacked = entry.Size != entry.UnpackedSize;
                //if (entry.IsCompressed)     //skip compressed data for now
                //{
                //    throw new NotImplementedException("Compressed data detected. Temporarily not supported.");
                //}
                entries.Add(entry);
            }

            foreach (DatEntry entry in entries)
            {
                fs.Position = entry.Offset;
                byte[] data = br.ReadBytes((int)entry.UnpackedSize);
                if (UnpackOptions.DecryptScripts && Path.GetExtension(entry.Name) == ".s")
                {
                    Logger.Debug(string.Format(LogStrings.TryDecScr, entry.Name));
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] ^= 0xFF;
                    }
                }
                File.WriteAllBytes(Path.Combine(folderPath, entry.Name), data);
                data = null;
                Logger.UpdateBar();
            }
            br.Dispose();
            fs.Dispose();
        }

        public override void Pack(string folderPath, string filePath)
        {
            FileStream fw = File.Create(filePath);
            BinaryWriter bw = new BinaryWriter(fw);
            DirectoryInfo d = new DirectoryInfo(folderPath);
            FileInfo[] files = d.GetFiles();
            int fileCount = files.Length;
            Logger.InitBar(fileCount);
            bw.Write(Encoding.ASCII.GetBytes(Magic));
            bw.Write(fileCount);
            bw.Write(fileCount);
            uint dataOffset = 16 + (uint)fileCount * 48;

            foreach (FileInfo file in files)
            {
                bw.Write(Encoding.ASCII.GetBytes(file.Name.PadRight(32, '\0')));
                bw.Write(dataOffset);
                uint size = (uint)file.Length;
                dataOffset += size;
                bw.Write(0x20000000);
                bw.Write(size);
                bw.Write(size);
            }

            foreach (FileInfo file in files)
            {
                byte[] data = File.ReadAllBytes(file.FullName);
                if (PackOptions.EncryptScripts && file.Extension == ".s")
                {
                    Logger.Debug(string.Format(LogStrings.TryEncScr, file.Name));
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] ^= 0xFF;
                    }
                }
                bw.Write(data);
                data = null;
                Logger.UpdateBar();
            }
            bw.Dispose();
            fw.Dispose();
        }
    }
}