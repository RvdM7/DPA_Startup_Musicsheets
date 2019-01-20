using DPA_Musicsheets.Converters;
using DPA_Musicsheets.Domain;
using System;
using System.Diagnostics;
using System.IO;

namespace DPA_Musicsheets
{
    public class MusicSaver
    {
        MusicList musicList;

        public MusicSaver(MusicList musicList)
        {
            this.musicList = musicList;
        }

        public void saveToMidi(string fileName)
        {
            IConverter<ISymbol> converter = new ConvertToMidi();
            Sanford.Multimedia.Midi.Sequence test = (Sanford.Multimedia.Midi.Sequence)converter.convert(musicList.Music);
            test.Save(fileName);
        }

        public void saveToLilypond(string fileName)
        {
            IConverter<ISymbol> converter = new ConvertToLilypond();
            var lilypondText = (string)converter.convert(musicList.Music);
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.Write(lilypondText);
                outputFile.Close();
            }
        }

        public void saveToPDF(string fileName)
        {
            string withoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string tmpFileName = $"{fileName}-tmp.ly";
            saveToLilypond(tmpFileName);

            string lilypondLocation = @"C:\Program Files (x86)\LilyPond\usr\bin\lilypond.exe";
            string sourceFolder = Path.GetDirectoryName(tmpFileName);
            string sourceFileName = Path.GetFileNameWithoutExtension(tmpFileName);
            string targetFolder = Path.GetDirectoryName(fileName);
            string targetFileName = Path.GetFileNameWithoutExtension(fileName);

            var process = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = sourceFolder,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = String.Format("--pdf \"{0}\\{1}.ly\"", sourceFolder, sourceFileName),
                    FileName = lilypondLocation
                }
            };

            process.Start();
            while (!process.HasExited)
            { /* Wait for exit */
            }
            if (sourceFolder != targetFolder || sourceFileName != targetFileName)
            {
                File.Move(sourceFolder + "\\" + sourceFileName + ".pdf", targetFolder + "\\" + targetFileName + ".pdf");
                File.Delete(tmpFileName);
            }
        }
    }
}
