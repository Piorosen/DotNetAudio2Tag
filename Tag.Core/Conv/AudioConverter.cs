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

        [Obsolete()]
        public IEnumerable<int> Execute() { new NotImplementedException("사용하지 마세요."); yield break; }

        public event EventHandler<int> ChangeExecute;
        public event EventHandler<int> CompleteOfIndex;

        void OnChangeExecute(int data)
        {
            ChangeExecute?.Invoke(this, data);
        }

        void OnCompleteOfIndex(int index)
        {
            CompleteOfIndex?.Invoke(this, index);
        }
        
        public async Task<bool> Execute(ConvMode mode, int MultiTask)
        {
            return await Task.Run(async () =>
            {
                List<Task> Worker = new List<Task>();

                int percent = 0;
                int CreateID = 0;

                foreach (var value in AudioList)
                {
                    Task worker = new Task(() => { });

                    IConv Conv = null;
                    if (mode == ConvMode.NORMAL)
                    {
                        if (value.Type == AudioType.WAV)
                        {
                            Conv = new Wav2Mp3();
                        }
                        else if (value.Type == AudioType.FLAC)
                        {
                            Conv = new Flac2Mp3();
                        }
                        else if (value.Type == AudioType.NONE)
                        {
                            return false;
                        }
                    }
                    else if (mode == ConvMode.USER)
                    {
                        Conv = new User2Mp3();
                    }

                    worker = new Task(() =>
                    {
                        var id = CreateID++;
                        foreach (var status in Conv?.Execute(value))
                        {
                            OnChangeExecute(status + id * 10000);
                        }
                        OnCompleteOfIndex(id);
                    });


                    worker.Start();
                    Worker.Add(worker);

                    var Count = MultiTask > Worker.Count ? Worker.Count : MultiTask;
                    for (int i = 0; i < Count; i++)
                    {
                        if (Worker[i].Status == TaskStatus.RanToCompletion)
                        {
                            Worker.RemoveAt(i);
                        }
                    }

                    if (Worker.Count >= MultiTask)
                    {
                        await Task.WhenAny(Worker.ToArray());
                    }

                    percent += 100;
                }
                Task.WaitAll(Worker.ToArray());

                return true;
            });

        }

        public List<ConvInfo> List()
        {
            return AudioList;
        }
    }
}
