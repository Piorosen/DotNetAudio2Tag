using System;
using System.Text;
using System.IO;

namespace Tag.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            Tag.Core.CueSpliter spliter = new Core.CueSpliter();
            Tag.Core.Wav2Mp3Converter converter = new Core.Wav2Mp3Converter();
            Tag.Core.Mp3Tagging tagging = new Core.Mp3Tagging();
            Tag.Core.MusicBrainzLib lib = new Core.MusicBrainzLib();

            while (true)
            {
                var input = System.Console.ReadLine();
                var token = input.Split(' ');
                if (token[0] == "cue")
                {
                    if (token[1] == "add")
                    {
                        spliter.AddFile(input.Remove(0, 8).Trim());
                    }
                    else if (token[1] == "del")
                    {
                        int.TryParse(input, out int result);
                        spliter.Delete(result);
                    }
                    else if (token[1] == "list")
                    {
                        var list = spliter.List();
                        foreach (var value in list)
                        {
                            foreach (var track in value.Track)
                            {
                                System.Console.WriteLine($"Title : {track.Title}\tCue Path : {value.Path}");
                            }
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("add [\"Cue File Path\"]");
                        System.Console.WriteLine("del num");
                        System.Console.WriteLine("list");
                    }
                }
                else if (token[0] == "conv")
                {
                    if (token[1] == "add")
                    {
                        converter.AddFile(input.Remove(0,9).Trim());
                    }
                    else if (token[1] == "del")
                    {
                        int.TryParse(token[2], out int result);
                        converter.Delete(result);
                    }
                    else if (token[1] == "list")
                    {
                        var list = converter.List();
                        foreach (var value in list)
                        {
                            System.Console.WriteLine($"File Name : {Path.GetFileName(value)}\tPath : {value}");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("add [\"wav File Path\"]");
                        System.Console.WriteLine("del num");
                        System.Console.WriteLine("list");
                    }
                }
                else if (token[0] == "exec")
                {
                    if (token[1] == "cue")
                    {
                        foreach (var value in spliter.Execute())
                        {
                            System.Console.WriteLine($"[{value} / 100]...");
                        }
                    }
                    else if (token[1] == "conv")
                    {
                        foreach (var value in converter.Execute())
                        {
                            System.Console.WriteLine($"[{value} / 100]...");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("cue");
                        System.Console.WriteLine("conv");
                    }
                }
                else if (token[0] == "mb")
                {
                    lib.SetTrack(new Core.CueData() { Title = "Hydra", Artists = null, Comments = null });
                }
                else
                {
                    System.Console.WriteLine("cue [add/del/list]");
                    System.Console.WriteLine("conv [add/del/list]");
                    System.Console.WriteLine("exec [cue/conv]");
                }
            }
        }
    }
}
