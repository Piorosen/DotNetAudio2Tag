using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv.Library;
using Tag.Core.Tagging;

namespace Tag.Core.Conv
{
    class DataBus
    {
        public ConvInfo convInfo;
        public int ID;
        public IConv conv;
    }

    public class AudioConverter : ICore<ConvInfo>
    {
        readonly List<ConvInfo> AudioList = new List<ConvInfo>();

        public ConvInfo this[int index] => AudioList[index];
        public int MultiTask = 1;
        public bool AddFile(ConvInfo file)
        {
            if (file.ResultPath == string.Empty)
            {
                file.ResultPath = Path.Combine(file.Directory, file.FileName);
                file.ResultPath += ".mp3";
            }
            AudioList.Add(file);
            return true;
        }

        public bool Delete(int at)
        {
            if (0 <= at && at < AudioList.Count)
            {
                AudioList.RemoveAt(at);
                return true;
            }
            return false;
        }
        public bool Delete(ConvInfo item)
        {
            return AudioList.Remove(item);
        }

        public IEnumerable<int> Execute() => Execute(ConvMode.NORMAL, 1);

        public event EventHandler<int> ChangeExecute;

        void OnChangeExecute(int data)
        {
            ChangeExecute?.Invoke(this, data);
        }
        
        public IEnumerable<int> Execute(ConvMode mode, int MultiTask)
        {
            List<Task> Worker = new List<Task>();
            
            int percent = 0;

            int CreateID = 0;

            foreach (var value in AudioList)
            {
                Task worker = new Task(() => { });

                if (mode == ConvMode.NORMAL)
                {
                    if (value.Type == AudioType.WAV)
                    {
                        Wav2Mp3 conv = new Wav2Mp3();
                        worker = new Task(() =>
                        {
                            var id = CreateID++;
                            foreach (var status in conv.Execute(value))
                            {
                                OnChangeExecute(status + id);
                            }
                        });
                        worker.Start();
                    }
                    else if (value.Type == AudioType.FLAC)
                    {
                        Flac2Mp3 conv = new Flac2Mp3();
                        worker = new Task(() =>
                        {
                            var id = CreateID++;
                            foreach (var status in conv.Execute(value))
                            {
                                OnChangeExecute(status + id);
                            }
                        });
                        worker.Start();
                    }
                    else if (value.Type == AudioType.NONE)
                    {
                        yield return 0;
                        yield break;
                    }
                }
                else if (mode == ConvMode.USER)
                {
                    User2Mp3 conv = new User2Mp3();
                    worker = new Task(() =>
                    {
                        var id = CreateID++;
                        foreach (var status in conv.Execute(value))
                        {
                            OnChangeExecute(status + id);
                        }
                    });
                   
                }
                
                Worker.Add(worker);


                percent += 100;
            }
            yield return 100;
        }

        public List<ConvInfo> List()
        {
            return AudioList;
        }
    }
}
