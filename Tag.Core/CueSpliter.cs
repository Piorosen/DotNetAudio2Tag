using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    public class CueSpliter
    {
        public void Test()
        {
            ATL.CatalogDataReaders
                .ICatalogDataReader reader = ATL.CatalogDataReaders
                                                        .CatalogDataReaderFactory
                                                        .GetInstance()
                                                        .GetCatalogDataReader(@"D:\data\Coe\MYTH&ROID - HYDRA.cue");
            ATL.Track track = reader.Tracks[0];
        }

    }
}
