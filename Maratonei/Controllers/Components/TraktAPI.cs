using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraktApiSharp;
using TraktApiSharp.Enums;
using TraktApiSharp.Objects.Basic;
using TraktApiSharp.Objects.Get.Shows;
using TraktApiSharp.Requests.Params;

namespace Maratonei.Controllers.Components {
    public class TraktAPI {

        private string CLIENT_ID = "8b52de13c749647158ee90572d975e5b3a12af509a67a6448b89e1b3095a5081";
        private string CLIENT_SECRET = "f9ab8994b6fe8c52717173516a4962830d274f3e84cbae8093b1d4022c7b0027";

        TraktClient client;

        public TraktAPI() {
            client = new TraktClient( CLIENT_ID, CLIENT_SECRET );
        }

        public async Task<List<TraktShow>> GetTop10Trending() {
            var trendingShowsTop10 = await client.Shows.GetTrendingShowsAsync( new TraktExtendedInfo( ).SetFull( ), null, 10 );
            List<TraktShow> resp = new List<TraktShow>( );
            foreach(var trending in trendingShowsTop10) {
                resp.Add(trending.Show);
            }
            return resp;
        }

        public async Task<TraktShow> GetTVShow( string show ) {
            return await client.Shows.GetShowAsync( show.Replace( ' ', '-' ), new TraktExtendedInfo( ).SetFull( ) );
        }

        public async Task<List<TraktShow>> Search(string query) {
            List<TraktShow> resp = new List<TraktShow>( );
            var result =  await client.Search.GetTextQueryResultsAsync( TraktSearchResultType.Show, query );
            foreach(var show in result) {
                resp.Add( show.Show );
            }
            return resp;
        }
    }
}

