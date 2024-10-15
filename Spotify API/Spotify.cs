using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_API
{
    internal class Spotify
    {
        // ----| Property |----
        private string clientId;
        private string clientSecret;
        private SpotifyClient spotifyClient;

        // ----| Constructor Fonksiyonu |----
        public Spotify(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            InitializeSpotifyClient().Wait(); // Spotify istemcisini başlatmayı burada çağırıyoruz
        }

        // ----| Spotify İstemcisini Başlat |----
        private async Task InitializeSpotifyClient()
        {
            var accessToken = await GetAccessToken();
            spotifyClient = new SpotifyClient(accessToken);
        }

        // ----| Access Token Almak İçin Fonksiyon |----
        private async Task<string> GetAccessToken()
        {
            var config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest(clientId, clientSecret);
            var response = await new OAuthClient(config).RequestToken(request);

            return response.AccessToken;
        }

        // ----| Sanatçının Adını Yazdıran Fonksiyon----
        public async Task<string> GetArtistName(string artistId)
        {
            if (spotifyClient == null)
            {
                throw new InvalidOperationException("Spotify client is not initialized.");
            }

            // Sanatçının bilgilerini al
            var artist = await spotifyClient.Artists.Get(artistId);
            return artist.Name;
        }

        // ----| Sanatçının Albümlerini Döndüren Fonksiyon |----
        public async Task<List<SimpleAlbum>> GetArtistAlbums(string artistId)
        {
            var albumsResponse = await spotifyClient.Artists.GetAlbums(artistId, new ArtistsAlbumsRequest());

            // Albümleri Filtrele Ve Döndür
            return albumsResponse.Items.FindAll(a => a.Type == "album");
        }
    }
}
